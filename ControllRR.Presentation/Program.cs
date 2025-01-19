using ControllRR.Application.Interfaces;
using ControllRR.Application.Services;
using ControllRR.Domain.Interfaces;
using ControllRR.Infrastructure.Data.Context;
using ControllRR.Infrastructure.Data.Seeding;
using ControllRR.Infrastructure.Repositories;
using ControllRR.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Adicionar o DbContext com MySQL
builder.Services.AddEntityFrameworkMySQL()
    .AddDbContext<ControllRRContext>(options =>
    {
        options.UseMySQL(builder.Configuration.GetConnectionString("ControlContext"));
    });

builder.Services.AddAutoMapper(typeof(MaintenanceMappingProfile));
builder.Services.AddAutoMapper(typeof(DeviceMappingProfile));
builder.Services.AddAutoMapper(typeof(SectorMappingProfile));
builder.Services.AddAutoMapper(typeof(DocumentMappingProfile));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMaintenanceService, MaintenanceService>();
builder.Services.AddScoped<IMaintenanceRepository, MaintenanceRepository>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddScoped<IFileStorageService, FileStorageService>();
builder.Services.AddScoped<ISectorService, SectorService>();
builder.Services.AddScoped<ISectorRepository, SectorRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedingService.Initialize(services);
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
