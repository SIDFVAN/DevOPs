﻿using System.Text.Json;
using Blanche.Domain.Reservations;
using Blanche.Mappers.Reservations;
using Blanche.Server;
using Blanche.Server.Persistence;
using Blanche.Server.Services.Reservations;
using Moq;
using MockQueryable.Moq;
using Shouldly;
using tests.Fakers.Reservations;
using Xunit.Abstractions;

namespace tests.Reservations;

public class ReservationService_Should
{
    private readonly ITestOutputHelper _output;
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly ReservationItemService _reservationItemService;
    protected readonly BlancheDbContext _dbContext;

    public ReservationService_Should(ITestOutputHelper output)
    {
        _output = output;
        _unitOfWork = new UnitOfWork(_dbContext);
        _reservationItemService = new ReservationItemService(_unitOfWork);
    }

    [Fact]
    public async Task CreateReservationAsync_should_add_reservation_when_not_duplicate()
    {
        // Arrange
        Reservation reservation = new ReservationFaker();
        _output.WriteLine(JsonSerializer.Serialize(reservation, new JsonSerializerOptions { WriteIndented = true }));
        var reservationDto = ReservationMapper.ReservationToReservationDto(reservation);
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(x => x.Beers.Update(reservation.TypeOfBeer));
        mockUnitOfWork.Setup(x => x.Formulas.Update(reservation.Formula));
        mockUnitOfWork.Setup(x => x.Reservations.Add(reservation));
        var reservationService = new ReservationService(mockUnitOfWork.Object, _reservationItemService);

        // Act
        var result = await reservationService.CreateReservationAsync(reservationDto);
        _output.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true }));

        // Assert
        result.ShouldBeOfType<Guid>();
    }

    [Fact]
    public async Task Return_valid_dates_when_GetAlreadyBookedDates_called()
    {
        // Arrange
        var fakeReservations = new List<Reservation>
        {
            new ReservationFaker(),
            new ReservationFaker()
        }; 
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(x => x.Reservations.Queryable()).Returns(fakeReservations.AsQueryable().BuildMock());
        var reservationService = new ReservationService(mockUnitOfWork.Object, _reservationItemService);

        // Act
        var result = await reservationService.GetAlreadyBookedDates();
        result.ToList().ForEach(d => _output.WriteLine(d.ToShortDateString()));

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeOfType<List<DateTime>>();
    }

    [Fact]
    public void Return_valid_dates_when_GetPopularDates_called()
    {

    }

    [Fact]
    public async Task Return_valid_date_When_GetBookedByClientDates_calledAsync()
    {
        //Arrange
        var customerId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afb6");
        var fakeReservations = new List<Reservation>
        {
            new ReservationFaker(),
            new ReservationFaker()
        };
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(x => x.Reservations.Queryable()).Returns(fakeReservations.AsQueryable().BuildMock());
        var reservationService = new ReservationService(mockUnitOfWork.Object, _reservationItemService);

        // Act
        var result = await reservationService.GetBookedByClientDates(customerId);
        result.ToList().ForEach(d => _output.WriteLine(d.ToShortDateString()));

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeOfType<List<DateTime>>();
    }

    [Fact]
    public void Throw_exception_when_start_date_booking_in_the_past()
    {

    }

}
