using Jlw.Utilities.DisableEndpointMiddleware;
using SampleWebApplication;
using TUser = Jlw.Extensions.Identity.Stores.ModularBaseUser;


var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseStaticWebAssets();

builder.Services.AddMockedUsers<TUser>();
builder.Services.AddIdentityMocking<TUser>();


var mvcBuilder = builder.Services.AddControllersWithViews();



var app = builder.Build();

app.UseMiddleware<DisableEndpointMiddleware<DisableIdentityEndpointOptions>>();

app.UseExceptionHandler("/Error");

app.UseHttpsRedirection();


app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();    // Initializes MS Identity Authentication Middleware 
app.UseAuthorization();     // Initialized MS Identity Authorization Middleware 

app.MapGet("/", () => "Hello World!");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    endpoints.MapRazorPages();
    endpoints.MapControllers();
});


app.Run();
