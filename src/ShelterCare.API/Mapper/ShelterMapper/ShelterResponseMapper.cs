using Riok.Mapperly.Abstractions;
using ShelterCare.API.Contracts.Responses;

namespace ShelterCare.API.Mapper.ShelterMapper;

[Mapper]
public partial class ShelterCreatedResponseMapper
{
    public partial ShelterCreatedResponse ShelterToShelterCreatedResponse(ShelterCare.Core.Domain.Shelter shelter);
}
