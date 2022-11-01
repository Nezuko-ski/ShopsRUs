using ShopsRUs.Core.DTOs;

namespace ShopsRUs.Core.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseDTO<bool>> CreateCustomerAsync(CreateCustomerDTO dTO);
        Task<ResponseDTO<UserDTO>> GetCustomerByIdAsync(string Id);
        Task<ResponseDTO<UserDTO>> GetCustomerByNameAsync(string name);
        Task<ResponseDTO<IEnumerable<UserDTO>>> GetAllCustomers();

    }
}
