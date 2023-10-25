using Riok.Mapperly.Abstractions;
using ShelterCare.API.Contracts.Requests;
using ShelterCare.Application;

namespace ShelterCare.API.Mapper.AnimalOwner;

[Mapper]
public partial class AnimalOwnerUpdateRequestMapper
{
    public partial UpdateAnimalOwnerCommand UpdateRequestToCommand(AnimalOwnerUpdateRequest animalOwnerUpdateRequest);
}
