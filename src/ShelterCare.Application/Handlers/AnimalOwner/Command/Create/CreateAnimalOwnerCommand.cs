namespace ShelterCare.Application;
public class CreateAnimalOwnerCommand : IRequest<Response<AnimalOwner>>
{
    public string Fullname { get; set; }
    public string NationalId { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
}
