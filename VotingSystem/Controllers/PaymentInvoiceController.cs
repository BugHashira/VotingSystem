using Microsoft.AspNetCore.Mvc;
using VotingSystem.Dto.PaymentInvoices;
using VotingSystem.Services.Interface;

namespace VotingSystem.Controllers
{
    [Route("payment-invoice")]
    public class PaymentInvoiceController : Controller
    {
        private readonly IPaymentInvoiceService _paymentInvoiceService;

        public PaymentInvoiceController(IPaymentInvoiceService paymentInvoiceService)
        {
            _paymentInvoiceService = paymentInvoiceService;
        }

        [HttpGet("create-payment-invoice")]
        public IActionResult CreatePaymentInvoice()
        {
            return View();
        }

        [HttpPost("create-payment-invoice")]
        public async Task<IActionResult> CreatePaymentInvoice([FromForm] CreatePaymentInvoiceDto request)
        {
            var result = await _paymentInvoiceService.AddPaymentInvoiceAsync(request);

            if (result.IsSuccessful)
            {
                return RedirectToAction("PaymentInvoices");
            }
            return RedirectToAction("CreatePaymentInvoice");
        }

        [HttpGet("edit-payment-invoice/{id}")]
        public async Task<IActionResult> EditPaymentInvoice(Guid id)
        {
            var result = await _paymentInvoiceService.GetPaymentInvoiceByIdAsync(id);

            if (result.IsSuccessful)
            {
                return View(result.Data);
            }

            return RedirectToAction("PaymentInvoices");
        }

        [HttpPost("edit-payment-invoice/{id}")]
        public async Task<IActionResult> EditPaymentInvoice(UpdatePaymentInvoiceDto request, Guid id)
        {
            var result = await _paymentInvoiceService.UpdatePaymentInvoiceAsync(id, request);

            if (result.IsSuccessful)
            {
                return RedirectToAction("PaymentInvoices");
            }
            return RedirectToAction("EditPaymentInvoice", new { id = id });
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> PaymentInvoiceDetail(Guid id)
        {
            var result = await _paymentInvoiceService.GetPaymentInvoiceByIdAsync(id);

            if (result.IsSuccessful)
            {
                return View(result.Data);
            }

            return RedirectToAction("PaymentInvoices");
        }

        [HttpGet("payment-invoices")]
        public async Task<IActionResult> PaymentInvoices()
        {
            var result = await _paymentInvoiceService.GetAllPaymentInvoicesAsync();

            if (result.IsSuccessful)
            {
                return View(result.Data);
            }

            return View(result.Data);
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> DeletePaymentInvoice(int id)
        {
            var result = await _paymentInvoiceService.DeletePaymentInvoiceAsync(id);

            if (result.IsSuccessful)
            {
                return RedirectToAction("PaymentInvoices");
            }

            return RedirectToAction("PaymentInvoices");
        }
    }
}
