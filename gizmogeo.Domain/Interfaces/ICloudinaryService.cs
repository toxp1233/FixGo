using Microsoft.AspNetCore.Http;

namespace gizmogeo.Domain.Interfaces;

public interface ICloudinaryService
{
    Task<(string Url, string PublicId)> UploadFileAsync(IFormFile file);
    Task DeleteFileAsync(string publicId);
}
