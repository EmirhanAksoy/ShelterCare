using ShelterCare.Core.Abstractions.Repository;
using ShelterCare.Infrastructure.Repository;
using ShelterCare.Infrastructure.Repository.Extensions;
using ShelterCare.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string? dbConnectionString = builder.Configuration.GetConnectionString("ShelterCare");
builder.Services.AddNpgsqlConnection(dbConnectionString);
builder.Services.AddTransient<IShelterRepository, ShelterRepository>();
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

app.Run();