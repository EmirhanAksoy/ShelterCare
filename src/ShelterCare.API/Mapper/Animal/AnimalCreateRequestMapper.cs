using Riok.Mapperly.Abstractions;
using ShelterCare.API.Contracts.Requests;
using ShelterCare.Application;

namespace ShelterCare.API.Mapper;

[Mapper]
public partial class AnimalCreateRequestMapper
{
    public partial CreateAnimalCommand CreateRequestToCommand(AnimalCreateRequest animalCreateRequest);
}
