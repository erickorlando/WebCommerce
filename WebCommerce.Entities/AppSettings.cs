namespace WebCommerce.Entities;

public class AppSettings
{
    public StorageConfiguration StorageConfiguration { get; set; } = null!;
}

public class StorageConfiguration
{
    public string Path { get; set; } = null!;
    public string PublicUrl { get; set; } = null!;
}