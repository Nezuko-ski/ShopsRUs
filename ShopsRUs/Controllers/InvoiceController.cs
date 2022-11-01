using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopsRUs.Core.DTOs;
using ShopsRUs.Core.Interfaces;

namespace ShopsRUs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _service;
        public InvoiceController(IInvoiceService service)
        {
            _service = service;
        }
        /// <summary>
        /// Gets total invoice amount given a bill
        /// </summary>
        /// <param name="dTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetInvoiceAmount([FromBody] InvoiceRequestDTO dTO)
        {
            var response = new ResponseDTO<InvoiceResponseDTO>();
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _service.GetTotalInvoiceAmount(dTO.InvoiceItems, dTO.CustomerId);
                    response.StatusCode = StatusCodes.Status200OK;
                    response.Data = new InvoiceResponseDTO
                    {
                        InvoiceAmount = result
                    };
                    response.Message = "Invoice Amount Generated Successfully";
                    return StatusCode(response.StatusCode, response);
                }
                else
                {
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.Message = "Invalid Request";
                    return StatusCode(response.StatusCode, response);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
