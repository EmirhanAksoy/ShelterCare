namespace ShelterCare.Application;

public class CreateAnimalSpecieCommand : IRequest<Response<AnimalSpecie>>
{
    public string Name { get; set; } = string.Empty;
}
