using AskAHuman.Components;
using AskAHuman.Services;
using AskAHuman.Services.Interfaces;
using DatabaseLayer;
using DatabaseLayer.Context;
using DatabaseLayer.UnitOfWork;
using Microsoft.AspNetCore.DataProtection;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();

builder.Services.AddTransient<IAskAHumanDbContextFactory, AskAHumanDbContextFactory>();
builder.Services.AddTransient<IUnitOfWorkFactory, UnitOfWorkFactory>();
builder.Services.AddTransient<IDbService, DbService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
builder.Services.AddTransient<IChatService, ChatService>();
builder.Services.AddTransient<IMessageService, MessageService>();
builder.Services.AddTransient<ILiveMessageService, LiveMessageService>();
builder.Services.AddSingleton<ILiveMessageCoordinatorService, LiveMessageCoordinatorService>();

if (builder.Environment.IsProduction())
{
    builder.Services.AddDataProtection()
        .PersistKeysToFileSystem(new DirectoryInfo("/DataProtection-Keys"))
        .SetApplicationName("AskAHuman");
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsProduction())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// auto apply migrations at startup
var db = app.Services.GetRequiredService<IDbService>();
db.ApplyMigrations();

app.Run();