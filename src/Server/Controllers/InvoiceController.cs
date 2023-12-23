using Blanche.Server.Pages.Templates; 
using Blanche.Server.Services.Util;
using Blanche.Server.Services.Util.Mail;
using Blanche.Shared.Invoices;
using Blanche.Shared.Reservations;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Blanche.Server.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    { 
        private readonly IInvoiceService _invoiceService;
        private readonly IEmailService _emailService;
        private readonly ILogger<InvoiceController> _logger;
        private readonly IPdfGeneratorService _pdfGenerationService;
        private readonly IHtmlGenerationService _htmlGenerationService;

        public InvoiceController( 
            IInvoiceService invoiceService,
            IEmailService emailService,
            IPdfGeneratorService pdfGenerationService,
            IHtmlGenerationService htmlGenerationService,
            ILogger<InvoiceController> logger)
        {
            _invoiceService = invoiceService;
            _htmlGenerationService = htmlGenerationService;
            _pdfGenerationService = pdfGenerationService;
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [SwaggerOperation("Generates quotation")]
        [HttpPost("quote")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateQuotationForReservation(ReservationDto reservationDto)
        {
            var invoice = await _invoiceService.CreateQuoteForReservationAsync(reservationDto);
            return Ok(invoice);
        }
         

        [SwaggerOperation("Generates invoice")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateInvoiceForReservation(ReservationDto reservationDto)
        {

            var invoice = await _invoiceService.CreateInvoiceForReservationAsync(reservationDto);
            return Ok(invoice);
        }

        [SwaggerOperation("Sends prepayment confirmed")]
        [HttpPost("{reservationId}/prepayed")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SendPrepaymentConfirmed(Guid reservationId)
        {

            try
            {

                var model = _invoiceService.GetInvoiceByReservationId(reservationId).Result;
                if (model != null)
                {
                     
                    _emailService.SendReservationPrePayedMailAsync(model);

                    return Ok();
                }
                else
                    return StatusCode(400, "Invoice not found!");

            }
            catch (Exception ex)
            { 
                _logger.LogError(ex, "Error sending mail");
                return StatusCode(500, ex.Message);
            }
        }


        [SwaggerOperation("Generates quotation PDF and mails it")]
        [HttpGet("quote/{quoteId}/send")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateQuotationPdf(Guid quoteId)
        {
            try
            {
                 
                var model = _invoiceService.GetInvoiceById(quoteId).Result; 
                if (model != null)
                {
                    _logger.LogInformation("In try sending quotation PDF");
                    var pdf = await _pdfGenerationService.Generate(nameof(InvoiceTemplate), model);
                    _logger.LogInformation("after  _pdfGenerationService.Generate sending quotation PDF");
                    _logger.LogInformation(nameof(InvoiceTemplate));

                    var pdfFileName = $"{model.InvoiceNumber}.pdf";
                    _logger.LogInformation("Before _htmlGenerationService.Generate");
                    _logger.LogInformation(nameof(QuoteMailTemplate));
                    string htmlBody = await _htmlGenerationService.Generate(nameof(QuoteMailTemplate), model);

                    _logger.LogInformation("After _htmlGenerationService.Generate");
                    _emailService.SendInvoiceMailWithPdf(model, pdf, htmlBody, pdfFileName);

                    return Ok(pdf);
                }
                else
                    return StatusCode(400, "Reservation not found!");

            }
            catch (Exception ex)
            { 
                _logger.LogError(ex, "Error creating and sending quotation PDF");
                return StatusCode(500, ex.Message);
            }
        }

        [SwaggerOperation("Generates quotation PDF and mails it")]
        [HttpGet("{invoiceId}/send")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateInvoicePdf(Guid invoiceId)
        {
            try
            {

                var model = _invoiceService.GetInvoiceById(invoiceId).Result;

                if (model != null)
                {
                    var pdf = await _pdfGenerationService.Generate(nameof(InvoiceTemplate), model);
                    var pdfFileName = $"{model.InvoiceNumber}.pdf";

                    string htmlBody = await _htmlGenerationService.Generate(nameof(InvoiceMailTemplate), model);
                    _emailService.SendInvoiceMailWithPdf(model, pdf, htmlBody, pdfFileName);

                    return Ok(pdf);
                }
                else
                    return StatusCode(400, "Reservation not found!");

            }
            catch (Exception ex)
            {
                // Log the exception and return an error response
                _logger.LogError(ex, "Error creating and sending quotation PDF");
                return StatusCode(500, ex.Message);
            }
        }

    }
}
