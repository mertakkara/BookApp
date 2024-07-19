using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BookApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HTMLTOPDFController : ControllerBase
    {
        private readonly IConverter _converter;
        public HTMLTOPDFController(IConverter converter)
        {
            _converter = converter;
        }
        [HttpPost("create-pdf")]
        public IActionResult GeneratePdfReport()
        {
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                PaperSize = PaperKind.A4,
                Orientation = Orientation.Portrait
            },
                Objects = {
                new ObjectSettings() {
                    PagesCount = true,
                    HtmlContent = "<h1>Book Receipt</h1>",
                    WebSettings = { DefaultEncoding = "utf-8" }
                }
            }
            };
            var pdf = _converter.Convert(doc);
            return File(pdf, "application/pdf", "receipt.pdf");
        }
    }
}
