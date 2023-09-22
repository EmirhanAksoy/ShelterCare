using ShelterCare.Infrastructure.Repository.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();
builder.Services.AddControllers();
// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? dbConnectionString = builder.Configuration.GetConnectionString("ShelterCare");
builder.Services.AddNpgsqlConnection(dbConnectionString);

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