
using Blanche.Server.Pages.Templates.Invoice;
using Blanche.Server.Services.Util;
using Blanche.Server.Util;
using Blanche.Shared.Invoice;
using Blanche.Shared.Reservations;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Blanche.Server.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IInvoiceService _invoiceService;
        //private readonly IEmailService _emailService;
        private readonly ILogger<InvoiceController> _logger;

        private readonly IPdfGeneratorService _pdfGenerationService;
        private readonly IHtmlGenerationService _htmlGenerationService;

        public InvoiceController(
            IReservationService reservationService,
            IInvoiceService _invoiceService,
            // IEmailService emailService,
            IPdfGeneratorService pdfGenerationService,
            IHtmlGenerationService htmlGenerationService,
            ILogger<InvoiceController> logger)
        {
            _htmlGenerationService = htmlGenerationService;
            _pdfGenerationService = pdfGenerationService;
            _reservationService = reservationService ?? throw new ArgumentNullException(nameof(reservationService));
            //_emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [SwaggerOperation("Generates quotation PDF and sends to customer by email.")]
        [HttpGet("quotation/{reservationId}/send")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateQuotationPdfAndSendToCustomer(Guid reservationId)
        {
            try
            {
                // TODO: will create a invoice and use that as model
                var model = _reservationService.GetReservationById(reservationId).Result;
                if (model != null)
                {
                    var result = await _pdfGenerationService.Generate($"Invoice/{typeof(InvoiceTemplate).Name}", model);
                    return File(result, "application/pdf", $"Quote - {model.Id}.pdf");
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


        [SwaggerOperation("Generates quotation PDF and returns it")]
        [HttpGet("quotation/{reservationId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateQuotationPdf(Guid reservationId)
        {
            try
            {
                // TODO: will create a invoice and use that as model
                var model = _reservationService.GetReservationById(reservationId).Result;
                if (model != null)
                {
                    var result = await _pdfGenerationService.Generate($"Invoice/{typeof(InvoiceTemplate).Name}", model);
                    return File(result, "application/pdf", $"Quote - {model.Id}.pdf");
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
