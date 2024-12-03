using Microsoft.AspNetCore.Http;

namespace ECommerce.Application.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(IFormFile file);
        Task DeleteFileAsync(string fileUrl);
    }
} 