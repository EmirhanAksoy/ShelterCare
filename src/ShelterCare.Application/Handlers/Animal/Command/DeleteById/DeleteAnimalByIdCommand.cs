namespace ShelterCare.Application;

public class DeleteAnimalByIdCommand : IRequest<Response<bool>>
{
    public Guid Id { get; set; }
    public DeleteAnimalByIdCommand(Guid id)
    {
        Id = id;
    }
}
