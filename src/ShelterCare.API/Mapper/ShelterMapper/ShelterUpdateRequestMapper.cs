using Riok.Mapperly.Abstractions;
using ShelterCare.API.Contracts.Requests;
using ShelterCare.Application;

namespace ShelterCare.API.Mapper.ShelterMapper;

[Mapper]
public partial class ShelterUpdateRequestMapper
{
    public partial UpdateShelterCommand UpdateRequestToCommand(ShelterUpdateRequest shelterUpdateRequest);
}
