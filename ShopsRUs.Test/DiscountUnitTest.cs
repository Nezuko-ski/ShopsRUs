/*using Moq;
using ShopsRUs.Controllers;
using ShopsRUs.Core.Interfaces;
using ShopsRUs.Domain.Models;

namespace ShopsRUs.Test
{
    public class DiscountUnitTest
    {

        public Mock<IDiscountService> moq = new Mock<IDiscountService>();
        [Fact]
        public async void GetSpecificDiscount()
        {
            moq.Setup(v => v.GetSpecificDiscount("34")).Returns(Task.FromResult(new Discount()
            {
                Name = "Affiliate",
                Percentage = "10"
            }));
            var x = new DiscountController(moq.Object);
            var res = await x.GetDiscountType("34");
            var ans = new Discount()
            {
                Name = "Affiliate",
                Percentage = "10"
            };
             Assert.Equal( ,res.ToString());
        }

    }

}

*/