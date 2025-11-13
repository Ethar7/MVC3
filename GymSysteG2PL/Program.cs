using GymSystemG2AL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using GymSystemG2AL.Repositories.Classes;
using GymSystemG2AL.Data.DataSeed;
using GymSystemBLL;
using GymSystemG2AL.Entities;
using System.ComponentModel.Design.Serialization;
using GymSystemBLL.Services.Interfaces;
using GymSystemBLL.Services.Classes;
using GymSystemBLL.Services.AttachmentService;
using Microsoft.AspNetCore.Identity;


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
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ISessionRepository, SessionRepository>();
builder.Services.AddAutoMapper(X => X.AddProfile(new MappingProfiles()));
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IAnalyticService, AnalyticService>();
builder.Services.AddScoped<ITrainerService, TrainerService>();
builder.Services.AddScoped<IPlanService, PlanService>();
builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<IAttachmentService, AttachmentService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddIdentity<ApplicationUser,IdentityRole>(Config =>
{
    Config.Password.RequiredLength = 6;
    Config.Password.RequireLowercase = true;
    Config.Password.RequireUppercase = true;
    Config.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<GymSystemDBContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});
var app = builder.Build();

#region  Data Seed
var Scope = app.Services.CreateScope();
var dBContext = Scope.ServiceProvider.GetRequiredService<GymSystemDBContext>();

// check migrations

var PendingMigrations = dBContext.Database.GetPendingMigrations();

if (PendingMigrations?.Any() ?? false)

    dBContext.Database.Migrate();

GymDbContextSeeding.SeedData(dBContext);


var RoleManager = Scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

var UserManager = Scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

IdentityDbContextSeeding.SeedData(RoleManager, UserManager);
#endregion



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();




