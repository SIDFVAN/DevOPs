using Blanche.Shared.Reservations; 
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Blanche.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [SwaggerOperation("Creates a new reservation.")]
        [HttpPost]
        [ProducesResponseType(typeof(ReservationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ReservationDto> CreateReservationAsync(ReservationDto reservationDto)
        {
            var reservation = await _reservationService.CreateReservationAsync(reservationDto);
            return reservation!;
        }

        [SwaggerOperation("Updates a reservation.")]
        [HttpPut]
        [ProducesResponseType(typeof(ReservationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ReservationDto> UpdateReservationAsync(ReservationDto reservationDto)
        {
            var reservation = await _reservationService.UpdateReservationAsync(reservationDto);
            return reservation!;
        }

        [SwaggerOperation("Retrieves one or more reservations from a client.")]
        [HttpGet("{reservationId}")]
        [ProducesResponseType(typeof(ReservationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ReservationDto> GetReservationsByCustomerId(Guid reservationId)
        {
            var reservation = await _reservationService.GetReservationById(reservationId);
            return reservation;
        }

        [SwaggerOperation("Retrieves all reservations and applies filtering.")]
        [HttpGet]
        [ProducesResponseType(typeof(List<ReservationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<List<ReservationDto>> GetReservations()
        {
            var reservations = await _reservationService.GetOpenReservations();
            return reservations;
        }

        [SwaggerOperation("Adds new popular date.")]
        [HttpPost("popularDate")]
        [ProducesResponseType(typeof(PopularDateDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<PopularDateDto> AddPopularDate(PopularDateDto popularDateDto)
        {
            var popularDate = await _reservationService.AddPopularDate(popularDateDto);
            return popularDate!;
        }

        [SwaggerOperation("Retrieves all popular dates.")]
        [HttpGet("popularDate")]
        [ProducesResponseType(typeof(PopularDateDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<List<DateTime>> GetPopularDate()
        {
            var popularDate = await _reservationService.GetPopularDates();
            return popularDate.ToList();
        }

        [SwaggerOperation("Retrieves already booked dates.")]
        [HttpGet("alreadyBookedDates")]
        [ProducesResponseType(typeof(List<DateTime>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<List<DateTime>> GetAlreadyBookedDates()
        {
            var alreadyBookedDates = await _reservationService.GetAlreadyBookedDates();
            return alreadyBookedDates.ToList();
        }

        [SwaggerOperation("Retrieves dates booked by the client.")]
        [HttpGet("bookedByClient/{customerId}")]
        [ProducesResponseType(typeof(List<DateTime>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<List<DateTime>> GetBookedByClientDates(Guid customerId)
        {
            var bookedByClientDates = await _reservationService.GetBookedByClientDates(customerId);
            return bookedByClientDates.ToList();
        }
    }
}
