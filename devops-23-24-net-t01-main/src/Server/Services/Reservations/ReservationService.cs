using System.Text.Json;
using Blanche.Domain.Reservations;
using Blanche.Mappers.Products;
using Blanche.Mappers.Reservations;
using Blanche.Server.Persistence;
using Blanche.Shared.Exceptions;
using Blanche.Shared.Reservations;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Blanche.Server.Services.Reservations;

public class ReservationService : IReservationService
{
    protected readonly IUnitOfWork _unitOfWork;
    protected IReservationItemService _reservationItemService;

    public ReservationService(IUnitOfWork unitOfWork, IReservationItemService reservationItemService)
    {
        _unitOfWork = unitOfWork;
        _reservationItemService = reservationItemService;
    }

    public async Task<Guid> CreateReservationAsync(ReservationDto reservationDto)
    {
        var reservation = ReservationMapper.ReservationDtoToReservation(reservationDto);

        var duplicateReservation = _unitOfWork.Reservations
            .Queryable()
            .SingleOrDefault(r => r.StartDate == reservationDto.StartDate && r.EndDate == reservationDto.EndDate);

        if (duplicateReservation != null)
        {
            throw new DuplicateReservationException();
        }

        if (reservation.TypeOfBeer != null)
        {
            _unitOfWork.Beers.Update(reservation.TypeOfBeer);
        }
        _unitOfWork.Formulas.Update(reservation.Formula);

        foreach (var item in reservation.Items)
        {
            _unitOfWork.Products.Add(item.Product = null);
        }

        _unitOfWork.Reservations.Add(reservation);

        await _unitOfWork.CommitAsync();

        return reservation.Customer.Id;
    }

    public async Task<ReservationDto?> UpdateReservationAsync(ReservationDto reservationDto)
    {

        var result = await GetReservationById(reservationDto.Id);

        result.StartDate = reservationDto.StartDate;
        result.EndDate = reservationDto.EndDate;
        result.TotalPrice = reservationDto.TotalPrice;
        result.State = reservationDto.State;
        result.Formula = reservationDto.Formula;
        result.CustomerId = reservationDto.CustomerId;
        result.NumberOfPersons = reservationDto.NumberOfPersons;
        result.TypeOfBeer = reservationDto.TypeOfBeer;

        foreach (var item in result.Items)
        {
            if (!reservationDto.Items.Any(i => i.Id == item.Id))
            {
                var itemToDelete = ReservationItemMapper.ReservationItemDtoToReservationItem(item);
                await _reservationItemService.DeleteAsync(itemToDelete.Id);
            }
        }

        var reservation = ReservationMapper.ReservationDtoToReservation(result);

        _unitOfWork.Reservations.Update(reservation);
        await _unitOfWork.CommitAsync();

        return ReservationMapper.ReservationToReservationDto(reservation);
    }

    public async Task<List<ReservationDto>> GetReservationsByCustomerId(Guid customerId)
    {
        List<ReservationDto> reservationDtos = new();

        var reservations = await _unitOfWork.Reservations
            .Queryable()
            .Where(r => r.Customer.Id == customerId)
            .Include(r => r.Customer)
            .Include(r => r.Formula)
            .ToListAsync();

        foreach (var reservation in reservations)
        {
            await GetReservationItems(reservation);
            var reservationDto = ReservationMapper.ReservationToReservationDto(reservation);
            reservationDtos.Add(reservationDto);
        }

        return reservationDtos;
    }

    public async Task<List<ReservationDto>> GetReservationsByState(ReservationState state)
    {
        List<ReservationDto> reservationDtos = new();

        var reservations = await _unitOfWork.Reservations
            .Queryable()
            .Where(r => r.State == state)
            .Include(r => r.Customer)
            .Include(r => r.Formula)
            .Include(r => r.TypeOfBeer)
            .ToListAsync();

        foreach (var reservation in reservations)
        {
            await GetReservationItems(reservation);
            var reservationDto = ReservationMapper.ReservationToReservationDto(reservation);
            reservationDtos.Add(reservationDto);
        }

        return reservationDtos;
    }

    public async Task<ReservationDto> GetReservationById(Guid reservationId)
    {
        List<ReservationItem> items = new();

        Reservation reservation = await _unitOfWork.Reservations
            .Queryable()
            .AsNoTracking()
            .Where(r => r.Id == reservationId)
            .Include(r => r.Customer)
            .Include(r => r.Formula)
            .SingleOrDefaultAsync() ?? throw new EntityNotFoundException();

        items = await _unitOfWork.ReservationItems
            .Queryable()
            .AsNoTracking()
            .Where(r => r.ReservationId == reservation.Id)
            .Include(r => r.Product)
            .ToListAsync();

        reservation.AddItemsToReservation(items);

        var reservationDto = ReservationMapper.ReservationToReservationDto(reservation);
        return reservationDto;
    }

    public async Task<PopularDateDto?> AddPopularDate(PopularDateDto popularDateDto)
    {
        var popularDate = PopularDateMapper.PopularDateDtoToPopularDate(popularDateDto);

        _unitOfWork.PopularDates.Add(popularDate);
        await _unitOfWork.CommitAsync();

        return PopularDateMapper.PopularDateToPopularDateDto(popularDate);
    }

    public async Task<IEnumerable<DateTime>> GetPopularDates()
    {
        var popularDates = await _unitOfWork.PopularDates
            .Queryable()
            .ToListAsync();

        var popularDatesDtos = popularDates.Select(PopularDateMapper.PopularDateToPopularDateDto);

        return popularDatesDtos.Select(p => p.Date).ToList();
    }

    public async Task<IEnumerable<DateTime>> GetAlreadyBookedDates()
    {
        List<DateTime> alreadyBookedDates = new();

        // Get all reservations
        var reservations = await _unitOfWork.Reservations
            .Queryable()
            .Where(r => r.StartDate > DateTime.Today)
            .ToListAsync();

        // Get start and end date of each reservation + dates in between
        GetReservationDates(alreadyBookedDates, reservations);

        // Return all dates
        return alreadyBookedDates;
    }

    public async Task<IEnumerable<DateTime>> GetBookedByClientDates(Guid customerId)
    {
        List<DateTime> bookedByClientDates = new();

        // Get all reservations
        var reservations = await _unitOfWork.Reservations
            .Queryable()
            .Where(r => r.Customer.Id == customerId)
            .ToListAsync();

        // Get start and end date of each reservation + dates in between
        GetReservationDates(bookedByClientDates, reservations);

        // Return all dates
        return bookedByClientDates;
    }

    private static void GetReservationDates(List<DateTime> dateTimes, List<Reservation> reservations)
    {
        foreach (var reservation in reservations)
        {
            var startDate = reservation.StartDate;
            var endDate = reservation.EndDate;
            int daysInBetween = (endDate - startDate).Days + 1;

            List<DateTime> range = Enumerable.Range(0, daysInBetween)
                .Select(d => startDate.AddDays(d))
                .ToList();

            foreach (var day in range)
            {
                dateTimes.Add(day);
            }
        }
    }

    private async Task<Reservation> GetReservationItems(Reservation reservation)
    {
        List<ReservationItem> items = new();

        items = await _unitOfWork.ReservationItems
            .Queryable()
            .Where(r => r.ReservationId == reservation.Id)
            .Include(r => r.Product)
            .ToListAsync();

        reservation.AddItemsToReservation(items);
        return reservation;
    }
}
