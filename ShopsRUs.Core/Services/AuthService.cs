using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopsRUs.Core.DTOs;
using ShopsRUs.Core.Interfaces;
using ShopsRUs.Domain.Enums;
using ShopsRUs.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ShopsRUs.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<Customer> _userManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public AuthService(UserManager<Customer> userManager, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDTO<bool>> CreateCustomerAsync(CreateCustomerDTO dTO)
        {
            var response = new ResponseDTO<bool>();
            var checkEmail = await _userManager.FindByEmailAsync(dTO.Email);
            if (checkEmail == null)
            {
                var customer = new Customer()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = dTO.Email,
                    FullName = dTO.FullName,
                    Address = dTO.Address,
                    EmailConfirmed = true,
                    UserName = dTO.FullName.Split().First()
                };
                var result = await _userManager.CreateAsync(customer, dTO.Password);
                if (result.Succeeded)
                {
                    var res = await _userManager.AddToRoleAsync(customer, UserRoles.Customer.ToString());
                    if (res.Succeeded)
                    {
                        response.Data = true;
                        response.Message = "User Creation Successfull";
                        response.StatusCode = (int)HttpStatusCode.OK;
                        return response;
                    }
                }
                response.Message = "User Creation Unsuccessfull";
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                return response;
            }
            response.Message = $"The Email {checkEmail.Email} already exist";
            response.StatusCode = (int)HttpStatusCode.Forbidden;
            return response;
        }

        public async Task<ResponseDTO<IEnumerable<UserDTO>>> GetAllCustomers()
        {
            var response = new ResponseDTO<IEnumerable<UserDTO>>();
            var customers = await _unitOfWork.CustomerRepository.GetAllCustomersAsync();
            if (customers != null)
            {
                var result = new List<UserDTO>();
                foreach (var item in customers)
                {
                    result.Add(_mapper.Map<UserDTO>(item));
                }
                
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Data = result.ToList();
                response.Message = "Get session Successful";
                return response;
            }
            response.StatusCode = (int)HttpStatusCode.NotFound;
            response.Data = null;
            response.Message = "UnSuccessful";
            return response;
        }

        public async Task<ResponseDTO<UserDTO>> GetCustomerByIdAsync(string Id)
        {
            var response = new ResponseDTO<UserDTO>();
            var user = await _unitOfWork.CustomerRepository.GetAsync(v => v.Id == Id);
            //var user = await _userManager.FindByIdAsync(Id);

            if (user != null)
            {
                var result = _mapper.Map<UserDTO>(user);
                response.Message = "Successfull";
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Data = result;
                return response;
            }
            response.Message = "User not found";
            response.StatusCode = (int)HttpStatusCode.NotFound;
            response.Data = null;
            return response;
        }

        public async Task<ResponseDTO<UserDTO>> GetCustomerByNameAsync(string name)
        {
            var response = new ResponseDTO<UserDTO>();
            var user = await _unitOfWork.CustomerRepository.GetAsync(v => v.UserName == name || v.FullName == name);
            // var user = await _userManager.FindByNameAsync(name);
            if (user != null)
            {
                var result = _mapper.Map<UserDTO>(user);
                response.Message = "Successfull";
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Data = result;
                return response;
            }
            response.Message = "User not found";
            response.StatusCode = (int)HttpStatusCode.NotFound;
            response.Data = null;
            return response;
        }
    }
}
