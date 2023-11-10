
namespace ShelterCare.Infrastructure.ExternalApis;
internal class AnimalUniqueIdentifier
{
    public string Name { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
    public string OwnerId { get; set; } = string.Empty;
}
internal class AnimalConfirmSuccessResponse
{
    public bool Success { get; set; }
    public AnimalUniqueIdentifier Data { get; set; }
}

internal class AnimalOwnerConfirmResponse : NationalIdConfirmResponse
{

}
