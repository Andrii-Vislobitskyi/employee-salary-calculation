using System.Globalization;
using EMP.Common.Configuration;
using EMP.DataAccess.Repositories;
using Employee_API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "EMPLOYEE_API",
        Version = "v1",
        Description = "Description",
    });
});

var url = builder.Configuration["External_API_URL"];

builder.Services.AddHttpClient("ExternalAPI", client =>
{
    client.BaseAddress = new Uri(url);
});

var connectionString = builder.Configuration.GetConnectionString("DBConnection")
                                   ?? Environment.GetEnvironmentVariable("DATABASE_URL");
string? key = builder.Configuration["app_secret_key"] ??
                            Environment.GetEnvironmentVariable("APP_SECRET_KEY");

EmployeeAPIConfig apiConfig = new EmployeeAPIConfig();
apiConfig.ConnectionString = connectionString;
apiConfig.TokenExpires = builder.Configuration.GetValue<int>("TokenExpires");
apiConfig.SecretKey = key;
apiConfig.BonusRate = float.Parse(builder.Configuration.GetValue<string>("BonusRate"), CultureInfo.InvariantCulture);

builder.Services.AddServices();
builder.Services.AddRepositories();
builder.Services.AddSingleton(apiConfig);
builder.Services.AddAuth(apiConfig);

//Add log to console
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Information);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//CREATE TABLE IN DATA BASE IF NOT EXISTS
using (var scope = app.Services.CreateScope())
{
    var sqlFilePath = Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles", "Create_table.sql");
    string sqlQuery = await File.ReadAllTextAsync(sqlFilePath);

    var services = scope.ServiceProvider;
    var repository = services.GetRequiredService<IUserRepository>();

    await repository.Create_Table_Async(sqlQuery);
}

app.Run();
