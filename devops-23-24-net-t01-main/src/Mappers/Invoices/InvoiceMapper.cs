using Blanche.Domain.Invoices;
using Blanche.Domain.Reservations;
using Blanche.Shared.Invoices;
using Blanche.Shared.Reservations;
using Riok.Mapperly.Abstractions;
 

namespace Blanche.Mappers.Invoices
{ 

    [Mapper]
    public static partial class InvoiceMapper
    {
         
        public static partial InvoiceDto InvoiceToInvoiceDto(Invoice invoice);

        [MapperIgnoreTarget(nameof(ReservationDto.Invoices))]
        public static partial ReservationDto InvoiceReservationToInvoiceDtoReservationDto(Reservation reservation);

        public static partial Invoice InvoiceDtoToInvoice(InvoiceDto reservationDto);
    }
}
