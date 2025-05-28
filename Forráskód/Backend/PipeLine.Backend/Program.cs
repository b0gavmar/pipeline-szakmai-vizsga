using PipeLine.Backend.Extensions;
using PipeLine.Backend.Services.Authentication;
using PipeLine.HttpService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddSingleton<JwtService>();

builder.Services.ConfigureIdentity();

builder.ConfigureWebHost();
builder.Services.AddBackend();


var app = builder.Build();

app.UseWebSocket();

app.InitializeDatabase();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

/*app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Lax,
    Secure = CookieSecurePolicy.None 
});*/

app.MapControllers();

app.ConfigureWebApp();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
