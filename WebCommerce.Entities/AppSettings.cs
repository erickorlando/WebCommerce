using System.Security.Cryptography;

namespace WebCommerce.Entities;

public class AppSettings
{
    public StorageConfiguration StorageConfiguration { get; set; } = null!;

    public Jwt Jwt { get; set; } = default!;
}

public class StorageConfiguration
{
    public string Path { get; set; } = null!;
    public string PublicUrl { get; set; } = null!;
}

public class Jwt
{
    public string Issuer { get; set; } = default!;
    public string Audience { get; set; } = default!;
    public string SecretKey { get; set; } = default!;
}