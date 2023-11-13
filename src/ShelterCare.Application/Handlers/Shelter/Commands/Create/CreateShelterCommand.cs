﻿namespace ShelterCare.Application;

public class CreateShelterCommand : IRequest<Response<Shelter>>
{
    public string Name { get; set; } = string.Empty;
    public string OwnerFullName { get; set; } = string.Empty;
    public DateTime FoundationDate { get; set; } = DateTime.MinValue;
    public double TotalAreaInSquareMeters { get; set; } = double.MinValue;
    public string Address { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
}
