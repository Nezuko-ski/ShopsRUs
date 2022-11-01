using Microsoft.AspNetCore.Identity;
using ShopsRUs.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopsRUs.Domain.Models
{
    public class Customer : IdentityUser
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public CustomerType CustomerType { get; set; }
        public List<Invoice> Invoices { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
