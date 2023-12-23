using Blanche.Domain.Exceptions;
using Blanche.Domain.Invoices;
using Blanche.Domain.Reservations;
using Blanche.Mappers.Invoices;
using Blanche.Mappers.Reservations;
using Blanche.Server.Persistence;
using Blanche.Shared.Invoices;
using Blanche.Shared.Reservations;
using Microsoft.EntityFrameworkCore;

namespace Blanche.Server.Services.Invoices
{
    public class InvoiceService : IInvoiceService
    {
        protected readonly IUnitOfWork _unitOfWork;

        public InvoiceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<InvoiceDto> GetInvoiceById(Guid invoiceId)
        {
            var invoice = await _unitOfWork.Invoices
            .Queryable()
            .Where(i => i.Id == invoiceId)
            .Include(i => i.Reservation)
            .Include(i => i.Reservation.Customer)
            .Include(i => i.Reservation.Formula)
            .Include(i => i.Reservation.TypeOfBeer)
            .Include(i => i.Reservation.Items)
            .ThenInclude(item => item.Product)
            .SingleOrDefaultAsync();

            var invoiceDto = InvoiceMapper.InvoiceToInvoiceDto(invoice);

            return invoiceDto is null ? throw new EntityNotFoundException(nameof(Invoices), invoice.Id) : invoiceDto;
        }

        public Task CreateInvoicePdf(Guid reservationId)
        {
            throw new NotImplementedException();
        }

        public async Task<InvoiceDto> CreateInvoiceForReservationAsync(ReservationDto reservation)
        {
            var invoiceExist = await _unitOfWork.Invoices
            .Queryable()
            .Where(i => i.ReservationId == reservation.Id && i.IsQuote == false)
            .Include(i => i.Reservation)
            .Include(i => i.Reservation.Customer)
            .Include(i => i.Reservation.Formula)
            .SingleOrDefaultAsync();

            if (invoiceExist != null)
            {
                return InvoiceMapper.InvoiceToInvoiceDto(invoiceExist);
            }

            Invoice invoice = new Invoice()
            {
                InvoiceNumber = GenerateInvoiceNumber(false),
                ConfirmationDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddDays(14),
                CreatedAt = DateTime.Now,
                IsPayed = false,
                IsQuote = false,
                ReservationId = reservation.Id,
                Comments = reservation.Notes

            };

            _unitOfWork.Invoices.Add(invoice);
            await _unitOfWork.CommitAsync();

            // adding this before the commit gives errors on the id of customer
            invoice.Reservation = ReservationMapper.ReservationDtoToReservation(reservation);

            return InvoiceMapper.InvoiceToInvoiceDto(invoice);
        }

        public async Task<InvoiceDto> CreateQuoteForReservationAsync(ReservationDto reservation)
        {
            var invoiceExist = await _unitOfWork.Invoices
            .Queryable()
            .Where(i => i.ReservationId == reservation.Id && i.IsQuote == true)
            .Include(i => i.Reservation)
            .Include(i => i.Reservation.Customer)
            .Include(i => i.Reservation.Formula)
            .SingleOrDefaultAsync();

            if (invoiceExist != null)
            {
                return InvoiceMapper.InvoiceToInvoiceDto(invoiceExist);
            }

            Invoice invoice = new Invoice()
            {
                InvoiceNumber = GenerateInvoiceNumber(true),
                ConfirmationDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddDays(14),
                CreatedAt = DateTime.Now,
                IsPayed = false,
                IsQuote = true,
                ReservationId = reservation.Id,
                Comments = reservation.Notes
            };

            _unitOfWork.Invoices.Add(invoice);
            await _unitOfWork.CommitAsync();

            // adding this before the commit gives errors on the id of customer
            invoice.Reservation = ReservationMapper.ReservationDtoToReservation(reservation);

            return InvoiceMapper.InvoiceToInvoiceDto(invoice);
        }


        private string GenerateInvoiceNumber(bool isQuote)
        {
            int year = DateTime.Now.Year;

            int invoiceCount = GetInvoiceCountForYear(year);

            if (isQuote)
                return $"OF-{year}{invoiceCount + 1:D4}";
            else
                return $"FA-{year}{invoiceCount + 1:D4}";

        }

        private int GetInvoiceCountForYear(int year)
        {

            int count = _unitOfWork.Invoices
                .Queryable()
                .Where(i => i.CreatedAt.Year == year)
                .Count();

            return count;
        }

        public Task<InvoiceDto> CreateQuoteForReservation(Guid reservationId)
        {
            throw new NotImplementedException();
        }

        public Task CreateQuotePdf(Guid reservationId)
        {
            throw new NotImplementedException();
        }

        public Task SendPaymentConfirmation(Guid reservationId)
        {
            throw new NotImplementedException();
        }

        public async Task<InvoiceDto> GetInvoiceByReservationId(Guid reservationId)
        {

            var invoice = await _unitOfWork.Invoices
          .Queryable()
          .Where(i => i.ReservationId == reservationId && i.IsQuote == false)
          .Include(i => i.Reservation)
          .Include(i => i.Reservation.Customer)
          .Include(i => i.Reservation.Formula)
          .Include(i => i.Reservation.TypeOfBeer)
          .Include(i => i.Reservation.Items)
          .ThenInclude(item => item.Product)
          .SingleOrDefaultAsync();

            return InvoiceMapper.InvoiceToInvoiceDto(invoice);
        }
    }
}
