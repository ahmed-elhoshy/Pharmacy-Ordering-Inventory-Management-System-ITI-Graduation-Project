using E_Commerce.DomainLayer;
using E_Commerce.DomainLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using PharmacySystem.ApplicationLayer.IServiceInterfaces;
using PharmacySystem.ApplicationLayer.MappingConfig;
using PharmacySystem.ApplicationLayer.Services;
using PharmacySystem.DomainLayer.Interfaces;
using PharmacySystem.InfastructureLayer.Data.DBContext;
using PharmacySystem.InfastructureLayer.Data.InterfacesImplementaion;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region DbContext Configuration
builder.Services.AddDbContext<PharmaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion


#region Services Registration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();
builder.Services.AddScoped<WarehouseService>();
builder.Services.AddScoped<MedicineService, MedicineService>();
builder.Services.AddScoped<IRepresentativeService, RepresentativeService>();
builder.Services.AddScoped<IPharmacyRepository, PharmacyRepository>();
builder.Services.AddScoped<IPharmacyService, PharmacyService>();
builder.Services.AddScoped<IGovernateService, GovernateService>();
builder.Services.AddScoped<IAreaRepository, AreaRepository>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
#endregion


#region AutoMapper Configuration
builder.Services.AddAutoMapper(typeof(MapperConfig));

#endregion

#region Add Cors 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()  // ?? Use WithOrigins("http://localhost:3000") for production
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

// Add a default route for the root URL
app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();
