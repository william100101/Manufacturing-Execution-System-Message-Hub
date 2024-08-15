using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

using MessageHub;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Web.Services.Description;


var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    WebRootPath = "wwwroot"
});

builder.WebHost.UseStaticWebAssets();

builder.Configuration.AddJsonFile($"appsettings.json"); 
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json");

// Add services to the container.
builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();

builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy.
    options.FallbackPolicy = options.DefaultPolicy;
});

//builder.Services.AddHttpContextAccessor();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddTelerikBlazor();


string MeltConnection = builder.Configuration.GetConnectionString("MeltConnection");
builder.Services.AddDbContextFactory<MessageHub.MeltModels.MeltContext>(
    options => options.UseSqlServer(MeltConnection));


string ESRConnection = builder.Configuration.GetConnectionString("ESRConnection");
builder.Services.AddDbContextFactory<MessageHub.ESRModels.ESRContext>(
    options => options.UseSqlServer(ESRConnection));

string NDTConnection = builder.Configuration.GetConnectionString("NDTConnection");
builder.Services.AddDbContextFactory<MessageHub.NDTModels.NDTContext>(
    options => options.UseSqlServer(NDTConnection));

string SX32Connection = builder.Configuration.GetConnectionString("SX32Connection");
builder.Services.AddDbContextFactory<MessageHub.SX32Models.SX32Context>(
    options => options.UseSqlServer(SX32Connection));

string VerticalAnnealingConnection = builder.Configuration.GetConnectionString("VerticalAnnealingConnection");
builder.Services.AddDbContextFactory<MessageHub.VerticalAnnealingModels.VerticalAnnealingContext>(
    options => options.UseSqlServer(VerticalAnnealingConnection));

string WetGrinderConnection = builder.Configuration.GetConnectionString("WetGrinderConnection");
builder.Services.AddDbContextFactory<MessageHub.WetGrinderModels.WetGrinderContext>(
    options => options.UseSqlServer(WetGrinderConnection));


string _4500TonPressConnection = builder.Configuration.GetConnectionString("_4500TonPressConnection");
builder.Services.AddDbContextFactory<MessageHub._4500TonPressModels._4500TonPressContext>(
    options => options.UseSqlServer(_4500TonPressConnection));

string BarProcessingConnection = builder.Configuration.GetConnectionString("BarProcessingConnection");
builder.Services.AddDbContextFactory<MessageHub.BarProcessingModels.BarProcessingContext>(
    options => options.UseSqlServer(BarProcessingConnection));



builder.Services.AddScoped<MessageHubServiceMelt>();
builder.Services.AddScoped<MessageHubServiceESR>();
builder.Services.AddScoped<MessageHubServiceNDT>();
builder.Services.AddScoped<MessageHubServiceSX32>();
builder.Services.AddScoped<MessageHubServiceVerticalAnnealing>();
builder.Services.AddScoped<MessageHubServiceWetGrinder>();
builder.Services.AddScoped<MessageHubServiceBarProcessing>();
builder.Services.AddScoped<MessageHubService_4500TonPress>();
builder.Services.AddScoped<DatabaseSettingsService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
