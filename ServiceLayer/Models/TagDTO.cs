namespace ServiceLayer.Models;

public class TagDTO
{
    public int TagId { get; set; }

    public required string TagName { get; set; }

    public required string Note { get; set; }
}
