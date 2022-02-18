using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PCS.Service.Services.Abstract
{
    public interface IUploadService
    {
        Task<List<string>> UploadImagesAsync(List<IFormFile> imageFiles);
    }
}
