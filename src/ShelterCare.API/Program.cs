using ShelterCare.Core.Abstractions.Repository;
using ShelterCare.Infrastructure.Repository;
using ShelterCare.Infrastructure.Repository.Extensions;
using ShelterCare.Application.Extensions;
using ShelterCare.Infrastructure.Logger.Extensions;
using ShelterCare.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// For test loggings using IWebHost
builder.Logging.AddWebHostSerilog(builder.Configuration);
builder.Services.AddHealthChecks();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFluentValidation();
string? dbConnectionString = builder.Configuration.GetConnectionString("ShelterCare");
builder.Services.AddNpgsqlConnection(dbConnectionString);
builder.Services.AddTransient<IShelterRepository, ShelterRepository>();
builder.Services.AddTransient<IAnimalSpecieRepository, AnimalSpecieRepository>();
builder.Services.AddTransient<IAnimalOwnerRepository, AnimalOwnerRepository>();
builder.Services.AddMediatR();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.MapHealthChecks("/healthz");

app.UseHttpsRedirection();
app.MapControllers();

app.UseMiddleware<ValidationExceptionMiddleware>();

app.Run();