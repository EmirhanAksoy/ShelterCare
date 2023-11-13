namespace ShelterCare.Application;
public class DeleteAnimalOwnerByIdCommand : IRequest<Response<bool>>
{
    public Guid Id { get; set; }

    public DeleteAnimalOwnerByIdCommand(Guid id)
    {
        Id = id;
    }
}
