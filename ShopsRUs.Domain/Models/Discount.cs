using ShopsRUs.Domain.Interface;

namespace ShopsRUs.Domain.Models
{
    public class Discount : EntityBase
    {
        public string Name { get; set; }
        public string Percentage { get; set; }
       
    }
}
