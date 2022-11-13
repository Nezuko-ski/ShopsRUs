using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopsRUs.Core.Interfaces;
using ShopsRUs.Core.Services;
using ShopsRUs.Core.Utilities;
using ShopsRUs.Domain.Models;
using ShopsRUs.Infrastructure;
using ShopsRUs.Infrastructure.Repository;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(v => v.SerializerSettings.ReferenceLoopHandling = Newtonsoft
    .Json.ReferenceLoopHandling.Ignore);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(v =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    v.IncludeXmlComments(xmlPath);
});
builder.Services.AddDbContext<ShopsRUsDbContext>(options => options.UseSqlServer(config
    .GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<Customer, IdentityRole>().AddEntityFrameworkStores<ShopsRUsDbContext>();

builder.Services.AddCors(policyBuilder =>
    policyBuilder.AddDefaultPolicy(policy =>
        policy.WithOrigins("*").AllowAnyHeader().AllowAnyHeader())
);

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IDiscountService, DiscountService>();


builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

//builder.Services.AddAutoMapper(typeof(ShopsRUsProfile));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//builder.WebHost.ConfigureKestrel(options =>
//{
//    options.ListenAnyIP(5000); // to listen for incoming http connection on port 5000
//    options.ListenAnyIP(5001, configure => configure.UseHttps()); // to listen for incoming https connection on port 5001
//    // http://localhost:5000/swagger/index.html
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

ShopsRUsDbInitializer.Seed(app);
ShopsRUsDbInitializer.SeedUsersAndRolesAsync(app).Wait();

app.Run();
