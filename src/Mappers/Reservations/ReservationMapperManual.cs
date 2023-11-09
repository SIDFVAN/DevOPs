using Blanche.Domain.Customers.Mappers;
using Blanche.Domain.Formulas;
using Blanche.Domain.Reservations;
using Blanche.Shared.Formulas;
using Blanche.Shared.Reservations;
using Mappers.Customer;
 

namespace Mappers.Reservations
{
    public class ReservationMapperManual
    {
        public static ReservationDto MapToDto(Reservation reservation)
        {
            return ReservationDto.Builder()
                .WithId(reservation.Id)
                .WithStartDate(reservation.StartDate)
                .WithEndDate(reservation.EndDate)
                .WithTotalPrice(reservation.TotalPrice)
                .WithSubTotalPrice(reservation.TotalPrice - (reservation.TotalPrice * 0.21))
                .WithIsConfirmed(reservation.IsConfirmed)
                .WithNumberOfPersons(reservation.NumberOfPersons)
                .WithCustomer(CustomerMapperManual.MapToDto(reservation.Customer))
                //.WithFormula(FormulaMapper.MapToDto(reservation.Formula))
                //.WithItems(ReservationItemDtoMapper.MapToDto(reservation.Items))
                .WithFormula(new FormulaDto("test", "test2", 2.5))
                .Build();
        }

        public static Reservation MapToEntity(ReservationDto reservationDto)
        {
            return Reservation.Builder()
                .WithId(reservationDto.Id)
                .WithStartDate(reservationDto.StartDate)
                .WithEndDate(reservationDto.EndDate)
                .WithTotalPrice((double)(reservationDto.Items.Sum(p => p.Product.Price * p.Quantity) + reservationDto.Formula.Price)) 
                .WithIsConfirmed(reservationDto.IsConfirmed)
                .WithNumberOfPersons(reservationDto.NumberOfPersons)
                .WithCustomer(reservationDto.Customer == null ? null : CustomerMapperManual.MapToEntity(reservationDto.Customer))
                //.WithFormula(FormulaDtoMapper.MapToEntity(reservationDto.Formula))
                //.WithItems(ReservationItemDtoMapper.MapToEntity(reservationDto.Items))
                .WithFormula(new Formula("test", "test2", 2, 2.5))
                .Build();
        }         
    }
}
