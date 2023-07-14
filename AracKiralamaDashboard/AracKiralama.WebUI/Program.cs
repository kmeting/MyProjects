using AracKiralama.Data;
using AracKiralama.Services.Abstract;
using AracKiralama.Services.Concreate;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DatabaseContext>();
builder.Services.AddTransient(typeof(IService<>),typeof(Service<>));
builder.Services.AddScoped<IMusteriService, MusteriService>();


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
           name: "admin",
           pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
         );

app.MapAreaControllerRoute(
    name:"default",
    areaName:"Admin",
    pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
    );

app.Run();
