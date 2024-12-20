using chatApp.Database;
using chatApp.Entities;
using chatApp.Services;
using issuetracker.Api.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// add controllers
builder.Services.AddControllers();

// if i want to configure auth options.
// builder.Services.AddAuthentication();
// builder.Services.AddAuthorization();

// connect to database
builder.Services.AddDbContextPool<PostgresContext>(options =>
{
  options.UseNpgsql(builder.Configuration.GetSection("Postgres").Get<PostgresConnection>()!.ConnectionString)
  .UseSnakeCaseNamingConvention()
  .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
  .EnableSensitiveDataLogging();
});

// add identity
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
  options.Password.RequireUppercase = false;
  options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<PostgresContext>()
.AddDefaultTokenProviders();

// database services to inject it to controllers
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IParticipantService, ParticipantService>();
builder.Services.AddScoped<IUserService, UserService>();

// mapping profiles
builder.Services.AddAutoMapper(typeof(MappingProfiles));


// handle un authorized requists
builder.Services.ConfigureApplicationCookie(options =>
{
  options.Cookie.HttpOnly = false;
  options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
  options.Cookie.SameSite = SameSiteMode.None;


  options.Events.OnRedirectToLogin = context =>
  {
    context.Response.Redirect("/api/account/notloggedin");
    return Task.CompletedTask;
  };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
}


app.UseHttpsRedirection();

// in case of role authorization
// app.UseAuthentication();
// app.UseAuthorization();


app.MapControllers();

app.Run();

