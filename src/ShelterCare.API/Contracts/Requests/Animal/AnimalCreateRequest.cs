﻿namespace ShelterCare.API.Contracts.Requests;

public class AnimalCreateRequest
{
    public Guid ShelterId { get; set; }
    public Guid? AreaId { get; set; }
    public Guid AnimalSpecieId { get; set; }
    public string OwnerId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string UniqueIdentifier { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public DateTime JoiningDate { get; set; }
    public bool IsNeutered { get; set; }
    public bool IsDisabled { get; set; }
}
