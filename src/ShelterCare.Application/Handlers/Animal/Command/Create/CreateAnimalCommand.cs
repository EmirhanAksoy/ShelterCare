using MediatR;
namespace ShelterCare.Application;

public class CreateAnimalCommand : IRequest<Response<Animal>>
{
    public Guid ShelterId { get; set; }
    public Guid? AreaId { get; set; }
    public Guid AnimalSpecieId { get; set; }
    public Guid OwnerId { get; set; }
    public string Name { get; set; }
    public string UniqueIdentifier { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime JoiningDate { get; set; }
    public Boolean IsNeutered { get; set; }
    public Boolean IsDisabled { get; set; }

}
