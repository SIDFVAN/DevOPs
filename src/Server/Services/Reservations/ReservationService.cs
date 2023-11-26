using Blanche.Domain.Reservations;
using Blanche.Mappers.Reservations;
using Blanche.Server.Persistence;
using Blanche.Shared.Exceptions;
using Blanche.Shared.Reservations; 
using Microsoft.EntityFrameworkCore;

namespace Blanche.Server.Services.Reservations;

public class ReservationService : IReservationService
{
    protected readonly IUnitOfWork _unitOfWork;

    public ReservationService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> CreateReservationAsync(ReservationDto reservationDto)
    {
        var reservation = ReservationMapper.ReservationDtoToReservation(reservationDto);

        var duplicateReservation = _unitOfWork.Reservations
            .Queryable()
            .Where(r => r.StartDate == reservationDto.StartDate && r.EndDate == reservationDto.EndDate)
            .SingleOrDefault();

        if (duplicateReservation != null)
        {
            throw new DuplicateReservationException();
        }

        if (reservation.TypeOfBeer != null)
        {
            _unitOfWork.Beers.Update(reservation.TypeOfBeer);
        }
        _unitOfWork.Formulas.Update(reservation.Formula);
        _unitOfWork.Reservations.Add(reservation);
        await _unitOfWork.CommitAsync();

        return reservation.Customer.Id;
    }

    public async Task<ReservationDto?> UpdateReservationAsync(ReservationDto reservationDto)
    {
        var reservation = ReservationMapper.ReservationDtoToReservation(reservationDto);

        var reservationToUpdate = await _unitOfWork.Reservations.GetAsync(r => r.Id == reservationDto.Id) ?? throw new EntityNotFoundException();

        reservationToUpdate.StartDate = reservation.StartDate;
        reservationToUpdate.EndDate = reservation.EndDate;
        reservationToUpdate.TotalPrice = reservation.TotalPrice;
        reservationToUpdate.IsConfirmed = reservation.IsConfirmed;
        reservationToUpdate.NumberOfPersons = reservation.NumberOfPersons;
        reservationToUpdate.Formula = reservation.Formula;
        reservationToUpdate.Customer = reservation.Customer;

        _unitOfWork.Reservations.Update(reservationToUpdate);
        await _unitOfWork.CommitAsync();

        return ReservationMapper.ReservationToReservationDto(reservationToUpdate);
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
            var reservationDto = ReservationMapper.ReservationToReservationDto(reservation);
            reservationDtos.Add(reservationDto);
        }

        return reservationDtos;
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

    public async Task<List<ReservationDto>> GetOpenReservations()
    {
        List<ReservationDto> reservationDtos = new();

        var reservations = await _unitOfWork.Reservations
        .Queryable()
            .Where(r => r.IsConfirmed == false)
            .Include(r => r.Customer)
            .Include(r => r.Formula)
            .Include(r => r.Lines)
            .ToListAsync();

        foreach (var reservation in reservations)
        {
            var reservationDto = ReservationMapper.ReservationToReservationDto(reservation);
            reservationDtos.Add(reservationDto);
        }

        return reservationDtos;
    }

    public async Task<ReservationDto> GetReservationById(Guid reservationId)
    {
        Reservation reservation = await _unitOfWork.Reservations
            .Queryable()
            .Where(r => r.Id == reservationId)
            .Include(r => r.Customer)
            .Include(r => r.Formula)
            .Include(r => r.Lines)
            .SingleOrDefaultAsync() ?? throw new EntityNotFoundException();

        var reservationDto = ReservationMapper.ReservationToReservationDto(reservation);
        return reservationDto;
         
    }
}
