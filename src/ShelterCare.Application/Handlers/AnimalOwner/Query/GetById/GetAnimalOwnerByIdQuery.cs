namespace ShelterCare.Application;
public class GetAnimalOwnerByIdQuery : IRequest<Response<AnimalOwner>>
{
    public Guid Id { get; set; }

    public GetAnimalOwnerByIdQuery(Guid id)
    {
        Id = id;
    }
}
