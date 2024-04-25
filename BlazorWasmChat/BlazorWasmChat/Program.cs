using BlazorWasmChat.Authentication;
using BlazorWasmChat.ChatHubs;
using BlazorWasmChat.Client.ChatServices;
using BlazorWasmChat.Components;
using BlazorWasmChat.Data;
using BlazorWasmChat.Repositories;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddSignalR();
builder.Services.AddScoped<ChatService>();
builder.Services.AddDbContext<AppDbContext>(op => 
    op.UseSqlite(builder.Configuration.GetConnectionString("BlazorWasmChat")));
builder.Services.AddScoped<ChatRepo>();
builder.Services.AddControllers();

builder.Services.AddIdentityCore<AppUser>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(op =>
{
    op.DefaultScheme = IdentityConstants.ApplicationScheme;
    op.DefaultSignInScheme = IdentityConstants.ExternalScheme;
}).AddIdentityCookies();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingServerAuthenticationStateProvider>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorWasmChat.Client._Imports).Assembly);

app.MapHub<ChatHub>("/ChatHub");
app.MapControllers();
app.MapAdditionalIdentityEndpoints();

app.Run();