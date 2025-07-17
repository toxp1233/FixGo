using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using gizmogeo.Domain.Entities;
using gizmogeo.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace gizmogeo.Infrastructure.Services;

public class CloudinaryService : ICloudinaryService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryService(IOptions<CloudinarySettings> options)
    {
        var settings = options.Value;
        var account = new Account(settings.CloudName, settings.ApiKey, settings.ApiSecret);
        _cloudinary = new Cloudinary(account);
    }


    public async Task<(string Url, string PublicId)> UploadFileAsync(IFormFile file)
    {
        using var stream = file.OpenReadStream();
        string publicId = Guid.NewGuid().ToString();

        var fileDesc = new FileDescription(file.FileName, stream);

        if (file.ContentType.StartsWith("image"))
        {
            var uploadParams = new ImageUploadParams
            {
                File = fileDesc,
                PublicId = publicId
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return (uploadResult.SecureUrl.ToString(), uploadResult.PublicId);
        }
        else if (file.ContentType.StartsWith("video"))
        {
            var uploadParams = new VideoUploadParams
            {
                File = fileDesc,
                PublicId = publicId
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return (uploadResult.SecureUrl.ToString(), uploadResult.PublicId);
        }
        else
        {
            var uploadParams = new RawUploadParams
            {
                File = fileDesc,
                PublicId = publicId
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return (uploadResult.SecureUrl.ToString(), uploadResult.PublicId);
        }
    }


    public async Task DeleteFileAsync(string publicId)
    {
        var deletionParams = new DeletionParams(publicId);
        var result = await _cloudinary.DestroyAsync(deletionParams);
        if (result.Result != "ok")
        throw new Exception($"Failed to delete file: {result.Error?.Message}");
    }

}
