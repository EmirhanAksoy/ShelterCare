using Riok.Mapperly.Abstractions;
using ShelterCare.API.Contracts.Requests;
using ShelterCare.Application;

namespace ShelterCare.API.Mapper;

[Mapper]
public partial class AnimalUpdateRequestMapper
{
    public partial UpdateAnimalCommand UpdateRequestToCommand(AnimalUpdateRequest animalUpdateRequest);
}
