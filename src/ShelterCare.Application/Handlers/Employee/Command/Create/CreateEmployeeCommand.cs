using MediatR;
namespace ShelterCare.Application;
public class CreateEmployeeCommand : IRequest<Response<Employee>>
{
    public Guid ShelterId { get; set; }
    public string Fullname { get; set; }
    public string NationalId { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public Boolean IsOwner { get; set; }
    public DateTime DateOfStart { get; set; }
    public DateTime? DismissalDate { get; set; }
}
