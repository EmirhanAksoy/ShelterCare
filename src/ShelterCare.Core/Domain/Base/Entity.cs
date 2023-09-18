namespace ShelterCare.Core.Domain.Base;

public interface IEntity { }
public class Entity : IEntity
{
    public string Id { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreateDate { get; set; }
    public string CreateUserId { get; set; }
    public DateTime UpdateDate { get; set; }
    public string UpdateUserId { get; set; }
}


