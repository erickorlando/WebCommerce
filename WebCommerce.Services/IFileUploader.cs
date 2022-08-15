namespace WebCommerce.Services;

public interface IFileUploader
{
    Task<string> UploadFileAsync(string base64, string fileName);
}