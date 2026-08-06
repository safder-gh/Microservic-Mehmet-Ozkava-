namespace Catalog.Api.Model;

public class Product
    {
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public List<string> Categories {  get; set; } = new List<string>();
    public string ImageFile { get; set; } = default!; 
    public decimal Price { get; set; }
    }

