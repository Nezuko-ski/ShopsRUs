using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopsRUs.Core.DTOs;
using ShopsRUs.Core.Interfaces;

namespace ShopsRUs.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public AuthController(IAuthService service)
        {
            _service = service;
        }
        /// <summary>
        /// Create a new customer
        /// </summary>
        /// <param name="dTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create-customer")]
        public async Task<IActionResult> Register(CreateCustomerDTO dTO)
        {
            var result = await _service.CreateCustomerAsync(dTO);
            return StatusCode(result.StatusCode, result);
        }
        /// <summary>
        /// Get All Customers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var result = await _service.GetAllCustomers();
            return StatusCode(result.StatusCode, result);

        }
        /// <summary>
        /// Get Customer By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{Id}/byId")]
        public async Task<IActionResult> GetCustomerById([FromRoute] string Id)
        {
            var result = await _service.GetCustomerByIdAsync(Id);
            return StatusCode(result.StatusCode, result);
        }
        /// <summary>
        /// Get Customer By Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{name}/byName")]
        public async Task<IActionResult> GetCustomerByName([FromRoute] string name)
        {
            var result = await _service.GetCustomerByNameAsync(name);
            return StatusCode(result.StatusCode, result);
        }
    }
}
