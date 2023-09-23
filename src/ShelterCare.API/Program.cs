using ShelterCare.Core.Abstractions.Repository;
using ShelterCare.Infrastructure.Repository;
using ShelterCare.Infrastructure.Repository.Extensions;
using ShelterCare.Infrastructure.Repository.Init;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();
builder.Services.AddControllers();
// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? dbConnectionString = builder.Configuration.GetConnectionString("ShelterCare");
builder.Services.AddNpgsqlConnection(dbConnectionString);


builder.Services.AddTransient<IShelterRepository, ShelterRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
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