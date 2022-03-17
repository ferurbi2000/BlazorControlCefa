using BlazorControlCefa.Application.Constants;
using BlazorControlCefa.Areas.Identity;
using BlazorControlCefa.Data;
using BlazorControlCefa.Data.Enums;
using BlazorControlCefa.Data.ScopeFactory;
using BlazorControlCefa.Data.Seeders;
using BlazorControlCefa.Data.Seeders.Identity;
using BlazorControlCefa.Repositories.Department;
using BlazorControlCefa.Repositories.Person;
using BlazorControlCefa.Repositories.PersonType;
using BlazorControlCefa.Repositories.Position;
using BlazorControlCefa.Repositories.Vehicle;
using BlazorControlCefa.Repositories.VehicleBrand;
using BlazorControlCefa.Repositories.VehicleType;
using BlazorControlCefa.Repositories.Reason;
using BlazorControlCefa.Services;
using Havit.Blazor.Components.Web;
using Havit.Blazor.Components.Web.Bootstrap;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BlazorControlCefa.Repositories.VisitType;
using BlazorControlCefa.Repositories.Visit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//var connectionString = builder.Configuration.GetConnectionString("BlazorControlCefaConnection");
var connectionString = builder.Configuration.GetConnectionString("BlazorControlCefaConnection");

// AddDbContextFactory nos permite autocontruirse nuevo contexto en cada componente para evitar errores de concurrencia
// que es muy comun en Blazor.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString,
    //options.UseSqlServer(connectionString,
    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)),
    ServiceLifetime.Transient
    );

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(
    options => options.SignIn.RequireConfirmedAccount = true
    )
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages(config =>
{
    var policy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();    
});

builder.Services.AddRazorPages();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;

    // Default SignIn settings
    options.SignIn.RequireConfirmedAccount = true;
    options.SignIn.RequireConfirmedEmail = true;
    options.SignIn.RequireConfirmedPhoneNumber = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.LoginPath = "/Identity/Account/Login";

    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});

builder.Services.AddAuthorizationCore(options =>
{
    // Politicas Pagina Persons
    options.AddPolicy("personView-Policy", policy =>
    {
        policy.RequireClaim(CustomClaimTypes.Permission,
            Permissions.Person.View);
    });
    options.AddPolicy("personCreate-Policy", policy =>
    {
        policy.RequireClaim(CustomClaimTypes.Permission,
            Permissions.Person.Create);
    });
    options.AddPolicy("personEdit-Policy", policy =>
    {
        policy.RequireClaim(CustomClaimTypes.Permission,
            Permissions.Person.Edit);
    });
    options.AddPolicy("personDelete-Policy", policy =>
    {
        policy.RequireRole(Roles.SuperAdmin.ToString(), Roles.Admin.ToString()).RequireClaim(CustomClaimTypes.Permission,
            Permissions.Person.Delete);
    });
    options.AddPolicy("personSpecial-Policy", policy =>
    {
        policy.RequireClaim(CustomClaimTypes.Permission,
            Permissions.Person.Special);
    });
    // FIN Politicas Pagina Persons


    options.AddPolicy("personTypeView-policy", policy =>
    {
        policy.RequireRole(Roles.SuperAdmin.ToString());
    });

    options.AddPolicy("departmentView-policy", policy =>
    {
        policy.RequireRole(Roles.SuperAdmin.ToString(), Roles.Admin.ToString());
    });

    options.AddPolicy("positionView-policy", policy =>
    {
        policy.RequireRole(Roles.SuperAdmin.ToString(), Roles.Admin.ToString());
    });

    options.AddPolicy("vehicleTypeView-policy", policy =>
    {
        policy.RequireRole(Roles.SuperAdmin.ToString(), Roles.Admin.ToString());
    });

    options.AddPolicy("vehicleBrandView-policy", policy =>
    {
        policy.RequireRole(Roles.SuperAdmin.ToString(), Roles.Admin.ToString());
    });

    options.AddPolicy("reasonsView-policy", policy =>
    {
        policy.RequireRole(Roles.SuperAdmin.ToString(), Roles.Admin.ToString());
    });
    
    options.AddPolicy("visitTypesView-policy", policy =>
    {
        policy.RequireRole(Roles.SuperAdmin.ToString(), Roles.Admin.ToString());
    });

});

builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
//builder.Services.AddTransient<IAuthenticatedUserService, AuthenticatedUserService>();

builder.Services.AddTransient<IDateTimeService, DateTimeService>();
builder.Services.AddSingleton(typeof(IServiceScopeFactory<>), typeof(ServiceScopeFactory<>));

builder.Services.AddSingleton<WeatherForecastService>();

//Dependency Injection for Repositories
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPersonTypeRepository, PersonTypeRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IPositionRepository, PositionRepository>();
builder.Services.AddScoped<IVehicleTypeRepository, VehicleTypeRepository>();
builder.Services.AddScoped<IVehicleBrandRepository, VehicleBrandRepository>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IReasonRepository, ReasonRepository>();
builder.Services.AddScoped<IVisitTypeRepository, VisitTypeRepository>();
builder.Services.AddScoped<IVisitRepository, VisitRepository>();
builder.Services.AddScoped<IVehicleVisitDetailRepository, VehicleVisitDetailRepository>();

builder.Services.AddHxServices();
builder.Services.AddHxMessenger();
builder.Services.AddHxMessageBoxHost();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    var logger = loggerFactory.CreateLogger("app");

    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    using (var context = services.GetService<ApplicationDbContext>())
    {
        //context.Database.Migrate();
        context.Database.EnsureCreated();

        InitialDataSeeder.SeedInitialData(context);

        await DefaultRoles.SeedAsync(userManager, roleManager);
        await DefaultSuperAdminUser.SeedAsync(userManager, roleManager);
        await DefaultAdminUser.SeedAsync(userManager, roleManager);
        await DefaultModeratorUser.SeedAsync(userManager, roleManager);
        await DefaultBasicUser.SeedAsync(userManager, roleManager);

    }
    logger.LogInformation("Finished Seeding Default Data");
};

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
