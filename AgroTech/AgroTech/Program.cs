using AgroTech.BusinessLogicLayer.Contracts;
using AgroTech.BusinessLogicLayer;
using AgroTech.Components;
using AgroTech.DataAccessLayer;
using AgroTech.DataAccessLayer.Contracts;
using AgroTech.DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using AgroTech.Client.Services;
using AgroTech.Client.Services.Contracts;
using AgroTech.Components.Layout;

var builder = WebApplication.CreateBuilder(args);

CommonServices.ConfigureCommonServices(builder.Services);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
});

builder.Services.AddMemoryCache();

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme);

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<AgroTechContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    options.SignIn.RequireConfirmedAccount = false;

    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
});

builder.Services.AddDbContext<AgroTechContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AgroTech"))
    .LogTo(Console.WriteLine);
});

builder.Services.AddTransient<IControlPanelBLL, ControlPanelBLL>();

builder.Services.AddTransient<IDataBaseDAL, DataBaseDAL>();

builder.Services.AddTransient<IAnimalDAL, AnimalDAL>();

builder.Services.AddScoped(http => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration["BaseAddress"] ?? "")
});

builder.Services.AddScoped<NavMenu>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(AgroTech.Client._Imports).Assembly);

app.Run();
