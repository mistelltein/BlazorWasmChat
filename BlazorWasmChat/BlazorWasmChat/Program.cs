using BlazorWasmChat.ChatHubs;
using BlazorWasmChat.Client.ChatServices;
using BlazorWasmChat.Components;
using BlazorWasmChat.Data;
using BlazorWasmChat.Repositories;
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

app.Run();