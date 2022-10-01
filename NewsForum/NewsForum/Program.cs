using Microsoft.Extensions.Azure;
using NewsForum.BusinessLogic;
using NewsForum.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddMigrationsDll(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddBusinessLogic();
builder.Services.AddAuthentication("Cookies").AddCookie(options => options.LoginPath = "/login");
builder.Services.AddAuthorization();
builder.Services.AddAzureClients(clientBuilder =>
{
    clientBuilder.AddBlobServiceClient(builder.Configuration["StorageConnectionString:blob"], true);
    clientBuilder.AddQueueServiceClient(builder.Configuration["StorageConnectionString:queue"], true);
});


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.UseAuthentication();
app.UseAuthorization();

app.Run();