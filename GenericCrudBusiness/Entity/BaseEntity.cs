namespace GenericCrud.Business;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set;}
    public DateTime UpdatedAt { get; set;}
    public DateTime? DeletedAt { get; set;}

    public bool IsActive => DeletedAt is null;

    public BaseEntity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;        
    }

}
