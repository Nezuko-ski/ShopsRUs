using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopsRUs.Core.DTOs;
using ShopsRUs.Core.Interfaces;
using ShopsRUs.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ShopsRUs.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _service;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public DiscountController(IDiscountService service)
        {
            _service = service;
        }
        /// <summary>
        /// Adds new Discount type
        /// </summary>
        /// <param name="dTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddDiscountType([FromBody] DiscountResponseDTO dTO)
        {
            var response = new ResponseDTO<bool>();
            try
            {
                if (ModelState.IsValid)
                {
                    var discount = new Discount
                    {
                        Name = dTO.Name,
                        Percentage = dTO.Percentage,
                        DateCreated = DateTime.Now
                    };
                    var result = await _service.CreateDiscount(discount);
                    response.StatusCode = StatusCodes.Status200OK;
                    response.Message = "Discount type created successfully";
                    response.Data = result;
                    return Ok(response);
                }
                else
                {
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.Message = "Invalid request";
                    response.Data = false;
                    return BadRequest(response);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Gets a list of all discounts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllDiscounts()
        {
            var response = new ResponseDTO<List<DiscountResponseDTO>>();
            try
            {
                var discounts = await _service.GetAllDiscountsAsync();
                var result = discounts.Select(v => new DiscountResponseDTO
                {
                    Name = v.Name,
                    Percentage = v.Percentage
                }).ToList();
                response.StatusCode = StatusCodes.Status200OK;
                response.Message = "Get session successful";
                response.Data = result;
                return Ok(response);
            }
            catch (Exception)
            {
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Message = "An error occured";
                response.Data = null;
                throw;
            }
        }
        /// <summary>
        /// Gets specific discount percentage by type
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetDiscountType([FromRoute] string Id)
        {
            var response = new ResponseDTO<DiscountResponseDTO>();
            try
            {
                var discount = await _service.GetSpecificDiscount(Id);
                if (discount != null)
                {
                    var result = new DiscountResponseDTO();
                    result.Name = discount.Name;
                    result.Percentage = discount.Percentage;
                    response.Data = result;
                }
                else
                {
                    response.Data = null;
                }
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Message = "Successful";
                return Ok(response);
            }
            catch (Exception)
            {
                response.Message = "Not found";
                response.StatusCode = (int)HttpStatusCode.NotFound;
                response.Data = null;
                throw;
            }
        }

    }
}
