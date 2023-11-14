using ShelterCare.Core.Abstractions.Repository;
using ShelterCare.Infrastructure.Repository;
using ShelterCare.Infrastructure.Repository.Extensions;
using ShelterCare.Application.Extensions;
using ShelterCare.Infrastructure.Logger.Extensions;
using ShelterCare.API.Middlewares;
using ShelterCare.Infrastructure.ExternalAPIs;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddHostSerilog(builder.Configuration);
builder.Services.AddHealthChecks();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFluentValidation();
builder.Services.AddNpgsqlConnection(builder.Configuration.GetConnectionString("ShelterCare"));
builder.Services.AddTransient<IShelterRepository, ShelterRepository>();
builder.Services.AddTransient<IAnimalSpecieRepository, AnimalSpecieRepository>();
builder.Services.AddTransient<IAnimalOwnerRepository, AnimalOwnerRepository>();
builder.Services.AddTransient<IAnimalRepository, AnimalRepository>();
builder.Services.AddMediatR();
builder.Services.AddConfirmApi(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.MapHealthChecks("/health");

app.UseHttpsRedirection();
app.MapControllers();

app.UseMiddleware<ValidationExceptionMiddleware>();

app.Run();