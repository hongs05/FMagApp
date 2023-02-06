using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using mvc.Services.Helpers.Implementation;
using mvc.Services.Helpers.Interface;
using mvc.Services.Helpers.Options;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddTransient<IUploadHelper, UploadHelper>();
services.Configure<AzureOptions>(builder.Configuration.GetSection("AzureBlob"));
services.Configure<CosmosOptions>(builder.Configuration.GetSection("AzureCosmosOptions"));
builder.Services.AddSingleton<ICosmosFileHistoryService>(options =>
{
    string? url = builder.Configuration.GetSection("AzureCosmosOptions")
    .GetValue<string>("URL");
    string? primaryKey = builder.Configuration.GetSection("AzureCosmosOptions")
    .GetValue<string>("PrimaryKey");
    string? dbName = builder.Configuration.GetSection("AzureCosmosOptions")
    .GetValue<string>("DatabaseName");
    string? containerName = builder.Configuration.GetSection("AzureCosmosOptions")
    .GetValue<string>("ContainerName");

    var cosmosClient = new CosmosClient(
        url,
        primaryKey
    );

    return new CosmosFileHistoryServices(cosmosClient, dbName, containerName);
});
// Add services to the container.
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAdB2C"));
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages()
    .AddMicrosoftIdentityUI();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
