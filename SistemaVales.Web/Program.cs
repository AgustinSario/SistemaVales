using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SistemaVales.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ValesDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ValesDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

// Dependency Injection
builder.Services.AddScoped<SistemaVales.DAL.UnitOfWork.IUnitOfWork, SistemaVales.DAL.UnitOfWork.UnitOfWork>();
builder.Services.AddScoped<SistemaVales.BLL.Services.IExpedienteService, SistemaVales.BLL.Services.ExpedienteService>();
builder.Services.AddScoped<SistemaVales.BLL.Services.IValeService, SistemaVales.BLL.Services.ValeService>();
builder.Services.AddScoped<SistemaVales.BLL.Services.IMedicamentoService, SistemaVales.BLL.Services.MedicamentoService>();
builder.Services.AddScoped<SistemaVales.BLL.Services.IRecetaService, SistemaVales.BLL.Services.RecetaService>();
builder.Services.AddScoped<SistemaVales.BLL.Services.IPacienteService, SistemaVales.BLL.Services.PacienteService>();
builder.Services.AddScoped<SistemaVales.BLL.Services.IHospitalService, SistemaVales.BLL.Services.HospitalService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ValesDbContext>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    // Seed Roles
    string[] roles = { "Admin", "Operador" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // Seed Admin User
    var adminEmail = "admin@sistemavales.com";
    if (await userManager.FindByEmailAsync(adminEmail) == null)
    {
        var user = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
        var result = await userManager.CreateAsync(user, "Admin123!");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }

    if (!context.Hospitales.Any())
    {
        var hospital = new SistemaVales.Models.Hospital
        {
            Nombre = "Hospital Central",
            Direccion = "Av. Principal 100",
            Telefono = "555-0001",
            Ciudad = "Capital"
        };
        context.Hospitales.Add(hospital);
        context.SaveChanges();

        if (!context.Pacientes.Any())
        {
            context.Pacientes.Add(new SistemaVales.Models.Paciente
            {
                Nombre = "Juan Perez",
                DNI = "12345678",
                Direccion = "Calle Falsa 123",
                Telefono = "555-1234",
                HospitalId = hospital.Id
            });
            context.SaveChanges();
        }
    }
}

app.Run();
