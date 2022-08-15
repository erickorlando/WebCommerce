using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebCommerce.Entities;

namespace WebCommerce.Services;

public class FileUploader : IFileUploader
{
    private readonly IOptions<AppSettings> _options;
    private readonly ILogger<FileUploader> _logger;

    public FileUploader(IOptions<AppSettings> options, ILogger<FileUploader> logger)
    {
        _options = options;
        _logger = logger;
    }
    
    public async Task<string> UploadFileAsync(string base64, string fileName)
    {
        try
        {
            var bytes = Convert.FromBase64String(base64);
            // product.jpg
            
            var path = Path.Combine(_options.Value.StorageConfiguration.Path, fileName);

            await using var fileStream = new FileStream(path, FileMode.Create);
            await fileStream.WriteAsync(bytes, 0, bytes.Length);

            return $"{_options.Value.StorageConfiguration.PublicUrl}{fileName}";
        }
        catch (Exception ex)
        {
            _logger.LogCritical("Error en FileUploader: {message}", ex.Message);
            return string.Empty;
        }
    }
}