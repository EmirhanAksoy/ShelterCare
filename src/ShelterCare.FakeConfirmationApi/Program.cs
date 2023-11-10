using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

List<NationalIdentifier>? nationalIdentifiers = builder.Configuration.GetSection("NationalIdentifiers").Get<List<NationalIdentifier>>();
List<AnimalUniqueIdentifier>? animalUniqueIdentifiers = builder.Configuration.GetSection("AnimalUniqueIdentifiers").Get<List<AnimalUniqueIdentifier>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapGet("/national-id/confirm/{nationalId}", ([FromRoute] string nationalId) =>
{
    if (nationalIdentifiers is not null && !string.IsNullOrEmpty(nationalId))
    {
        return Results.Ok(new
        {
            Success = nationalIdentifiers.Find(x => x.NationalId.Equals(nationalId)) is not null,
            Message = string.Empty
        });
    }

    return Results.NotFound(new
    {
        Success = false,
        Message = "Not Found"
    });
})
.WithName("Confirm National Id")
.WithOpenApi();


app.MapGet("/animal/id/confirm/{uniqueId}", ([FromRoute] string uniqueId) =>
{
    if (animalUniqueIdentifiers is not null && !string.IsNullOrEmpty(uniqueId))
    {
        AnimalUniqueIdentifier? animalInfo = animalUniqueIdentifiers.Find(x => x.Id.Equals(uniqueId));
        return Results.Ok(new
        {
            Success = animalInfo is not null,
            Data = animalInfo
        });
    }

    return Results.NotFound(new
    {
        Success = false,
        Message = "Animal not found"
    });
})
.WithName("Confirm Animal Unique Id")
.WithOpenApi();

app.MapGet("/animal/owner/confirm/{nationalId}/{uniqueAnimalId}", ([FromRoute] string nationalId, [FromRoute] string uniqueAnimalId) =>
{
    NationalIdentifier? nationalIdentifier = nationalIdentifiers?.Find(x => x.NationalId.Equals(nationalId));
    AnimalUniqueIdentifier? animalInfo = animalUniqueIdentifiers?.Find(x => x.Id.Equals(uniqueAnimalId));

    if (animalInfo?.OwnerId != nationalIdentifier?.NationalId)
    {
        return Results.NotFound(new
        {
            Success = false,
            Message = "Not found"
        });
    }

    return Results.Ok(new
    {
        Success = true,
        Message = "Matching"
    });
})
.WithName("Confirm Animal Owner")
.WithOpenApi();

app.MapPost("/set-source", ([FromBody] Source source) =>
{
    if (source is null || string.IsNullOrEmpty(source.JSON))
    {
        return Results.BadRequest(new
        {
            Success = false,
            Message = "Invalid source data"
        });
    }

    try
    {
        SourceModel? sourceResult = JsonSerializer.Deserialize<SourceModel>(source.JSON);
        if (sourceResult is null)
        {
            return Results.BadRequest(new
            {
                Success = false,
                Message = "Invalid source data"
            });
        }

        nationalIdentifiers = sourceResult.NationalIdentifiers;
        animalUniqueIdentifiers = sourceResult.AnimalUniqueIdentifiers;

    }
    catch (Exception ex)
    {

        return Results.BadRequest(new
        {
            Success = false,
            Message = $"Invalid source data - {ex.Message}"
        });
    }

    return Results.Ok(new
    {
        Success = true,
        Message = string.Empty
    });
})
.WithName("Set Source")
.WithOpenApi();

app.Run();



public class NationalIdentifier
{
    public string FullName { get; set; } = string.Empty;
    public string NationalId { get; set; } = string.Empty;
}

public class AnimalUniqueIdentifier
{
    public string Name { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
    public string OwnerId { get; set; } = string.Empty;
}

public class SourceModel
{
    public List<AnimalUniqueIdentifier> AnimalUniqueIdentifiers { get; set; } = new();
    public List<NationalIdentifier> NationalIdentifiers { get; set; } = new();
}


public class Source
{
    public string JSON { get; set; }
}



