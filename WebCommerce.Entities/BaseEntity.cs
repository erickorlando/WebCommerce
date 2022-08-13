namespace WebCommerce.Entities;

public class BaseEntity
{
    public int Id { get; set; }
    
    public bool Status { get; set; }

    protected BaseEntity()
    {
        Status = true;
    }
}