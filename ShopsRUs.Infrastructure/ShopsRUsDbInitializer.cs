using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ShopsRUs.Domain.Enums;
using ShopsRUs.Domain.Models;

namespace ShopsRUs.Infrastructure
{
    public class ShopsRUsDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ShopsRUsDbContext>();
                context.Database.EnsureCreated();
                if (!context.Invoices.Any())
                {
                    context.Discounts.AddRange(new List<Discount>()
                    {
                        new Discount
                        {
                            Name = "Affiliate",
                            Percentage = "10",
                            DateCreated = DateTime.Now
                        },
                        new Discount
                        {
                            Name = "Employee",
                            Percentage = "30",
                            DateCreated = DateTime.Now
                        },
                        new Discount
                        {
                            Name = "CustomerForOver2Years",
                            Percentage = "5",
                            DateCreated = DateTime.Now
                        }
                    });
                    context.SaveChanges();
                }
            }
        }
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                if (!await roleManager.RoleExistsAsync(UserRoles.Admin.ToString()))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin.ToString()));
                if (!await roleManager.RoleExistsAsync(UserRoles.Customer.ToString()))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Customer.ToString()));

                //User
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<Customer>>();
                var adminUser = await userManager.FindByEmailAsync("admin@shopsrus.com");
                if (adminUser == null)
                {
                    var newAdminUser = new Customer()
                    {
                        FullName = "Ainz Oaol Gown",
                        UserName = "Ainz-sama",
                        Email = "admin@shopsrus.com",
                        Address = "Ainz Ooal Gown's Castle",
                        EmailConfirmed = true,
                        DateCreated = DateTime.Now.AddYears(-3),
                        CustomerType = CustomerType.Employee
                    };
                    await userManager.CreateAsync(newAdminUser, "Nezuko@slayer4");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin.ToString());
                }

                var appUser = await userManager.FindByEmailAsync("user@goanime.com");
                if (appUser == null)
                {
                    var newAppUser = new Customer()
                    {
                        FullName = "Hinata Sakaguchi",
                        UserName = "Hinata",
                        Email = "user@shopsrus.com",
                        Address = "Holy Empire Ruberios",
                        EmailConfirmed = true,
                        DateCreated = DateTime.Now.AddYears(-2),
                        CustomerType = CustomerType.Customer
                    };
                    await userManager.CreateAsync(newAppUser, "Hinata@rimuru4");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.Customer.ToString());
                }
            }
        }
    }
}
