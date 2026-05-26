using TripWise.Mvc.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register HttpClient
builder.Services.AddHttpClient("TripWiseApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7038/api/"); 
    
});

//Add cookie authentication
builder.Services.AddAuthentication("TripWiseCookie")
    .AddCookie("TripWiseCookie", options =>
    {
        options.LoginPath = "/Account/Login"; // Redirect to login page if not authenticated
        options.AccessDeniedPath = "/Account/AccessDenied"; // Redirect to access denied page
    });

// Add Authorizaton
builder.Services.AddAuthorization();

// add MVC
builder.Services.AddControllersWithViews();

// Add HttpContextAccessor for accessing cookies in ApiClientService
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ApiClientService>();

// Add TripService
builder.Services.AddScoped<TripService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // JWT cookie
app.UseAuthorization(); // [Authorize]

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
