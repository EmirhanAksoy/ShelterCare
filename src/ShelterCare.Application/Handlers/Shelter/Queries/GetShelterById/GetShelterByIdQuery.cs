namespace ShelterCare.Application;
public class GetShelterByIdQuery : IRequest<Response<Shelter>>
{
    public Guid Id { get; set; }

    public GetShelterByIdQuery(Guid id)
    {
        Id = id;
    }
}
