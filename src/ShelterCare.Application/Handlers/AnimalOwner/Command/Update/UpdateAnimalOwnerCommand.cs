using MediatR;
namespace ShelterCare.Application;
public class UpdateAnimalOwnerCommand : IRequest<Response<AnimalOwner>>
{
    public Guid Id { get; set; }
    public string Fullname { get; set; }
    public string NationalId { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
}
