﻿namespace ShelterCare.Core.Abstractions.Repository;

public interface IShelterRepository : IRepository<Shelter>
{
    Task<bool> CheckIfShelterNameExists(string shelterName);
}
