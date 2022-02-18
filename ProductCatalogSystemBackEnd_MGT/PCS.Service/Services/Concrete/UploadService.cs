using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using PCS.Core.Settings;
using PCS.Service.Services.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCS.Service.Services.Concrete
{
    public class UploadService : IUploadService
    {
        private readonly CloudinarySettings cloudinarySettings;

        public UploadService(IOptions<CloudinarySettings> cloudinarySettingsOptions)
        {
            this.cloudinarySettings = cloudinarySettingsOptions.Value;
        }

        //Image uploading by cloudinary
        public async Task<List<string>> UploadImagesAsync(List<IFormFile> imageFiles)
        {
            if (imageFiles is not null)
            {
                Account account = new(cloudinarySettings.CloudName, cloudinarySettings.ApiKey, cloudinarySettings.ApiSecret);

                var cloudinary = new Cloudinary(account);
                var uploadResult = new ImageUploadResult();
                var ImagePaths = new List<string>();

                if (imageFiles.Any())
                {
                    foreach (var image in imageFiles)
                    {
                        using (var fileStream = image.OpenReadStream())
                        {
                            var uploadParams = new ImageUploadParams
                            {
                                File = new FileDescription(image.FileName, fileStream)
                            };
                            uploadResult = await cloudinary.UploadAsync(uploadParams);
                        }

                        if (uploadResult is null)
                        {
                            return null;
                        }

                        ImagePaths.Add(uploadResult.Url.ToString());
                    }
                    return ImagePaths;
                }
            }
            return null;
        }
    }
}
