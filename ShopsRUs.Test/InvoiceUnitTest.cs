using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopsRUs.Controllers;
using ShopsRUs.Core.DTOs;
using ShopsRUs.Core.Interfaces;
using ShopsRUs.Domain.Models;

namespace ShopsRUs.Test
{
    public class InvoiceUnitTest
    {
        public Mock<IInvoiceService> moq = new Mock<IInvoiceService>();

        [Fact]
        public async void GetTotalInvoiceAmount()
        {
            var items = new List<InvoiceItem>()
            {
                new InvoiceItem
                {
                    Id = 2,
                    Amount = "450",
                    Category = "Underwear",
                    Name = "G-string",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now
                }
            };
            moq.Setup(v => v.GetTotalInvoiceAmount(items, "e8e5a287-64a6-4484-bda5-81ee30c1d49d")).ReturnsAsync(StatusCodes.Status200OK.ToString());
            var x = new InvoiceController(moq.Object);
            var response = new InvoiceRequestDTO()
            {
                CustomerId = "e8e5a287-64a6-4484-bda5-81ee30c1d49d",
                InvoiceItems = items
            };
            var res  = await x.GetInvoiceAmount(response);
            Assert.Equal(StatusCodes.Status200OK.ToString(), res.ToString());
        }
    }
}
