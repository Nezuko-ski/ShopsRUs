using System.ComponentModel.DataAnnotations;

namespace ShopsRUs.Core.DTOs
{
    public class CreateCustomerDTO
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Kindly input your email address")]
        [EmailAddress]
        [RegularExpression("^[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?\\.)+[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?", ErrorMessage = "Invalid email format!")]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@$!%*?&])[A-Za-z0-9@$!%*?&]{6,}", ErrorMessage = "Invalid password format! Password must be alphanumeric and must contain at least one symbol")]
        public string Password { get; set; }
        
        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Kindly confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not tally!")]
        public string ConfirmPassword { get; set; }
        
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name is required!")]
        [RegularExpression("^[A-Z][a-z]+ [A-Z][a-z]+$", ErrorMessage = "First and last name must begin with uppercase!")]
        public string FullName { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address is required!")]
        public string Address { get; set; }
    }
}
