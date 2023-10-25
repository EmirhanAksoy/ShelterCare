using Riok.Mapperly.Abstractions;
using ShelterCare.API.Contracts.Requests;
using ShelterCare.Application;

namespace ShelterCare.API.Mapper.AnimalOwner;

[Mapper]
public partial class AnimalOwnerCreateRequestMapper
{
    public partial CreateAnimalOwnerCommand CreateRequestToCommand(AnimalOwnerCreateRequest animalOwnerCreateRequest);
}
