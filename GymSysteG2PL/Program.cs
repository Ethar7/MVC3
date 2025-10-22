using GymSystemG2AL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using GymSystemG2AL.Repositories.Classes;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Dependency Injection
builder.Services.AddDbContext<GymSystemDBContext>(options =>
{
    // options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"]);
    // options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

#endregion


builder.Services.AddScoped(typeof(IGenaricRepository<>), typeof(GenaricRepository<>));
builder.Services.AddScoped<IPlanRepository, PlanRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
