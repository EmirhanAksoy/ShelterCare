namespace ShelterCare.Application;

public class DeleteAnimalSpecieByIdCommand : IRequest<Response<bool>>
{
    public Guid Id { get; set; }
    public DeleteAnimalSpecieByIdCommand(Guid id)
    {
        Id = id;
    }
}
