using Riok.Mapperly.Abstractions;
using ShelterCare.API.Contracts.Requests;
using ShelterCare.Application;

namespace ShelterCare.API.Mapper.AnimalSpecieMapper;

[Mapper]
public partial class AnimalSpecieCreateRequestMapper
{
    public partial CreateAnimalSpecieCommand CreateRequestToCommand(AnimalSpecieCreateRequest animalSpecieCreateRequest);
}
