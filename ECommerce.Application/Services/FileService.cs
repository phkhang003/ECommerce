using ECommerce.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Application.Services
{
    public class FileService : IFileService
    {
        public async Task<string> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return string.Empty;

            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine("wwwroot", "uploads", fileName);

            Directory.CreateDirectory(Path.Combine("wwwroot", "uploads"));
            
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/uploads/{fileName}";
        }

        public Task DeleteFileAsync(string fileUrl)
        {
            if (string.IsNullOrEmpty(fileUrl))
                return Task.CompletedTask;

            var fileName = fileUrl.Split('/').Last();
            var filePath = Path.Combine("wwwroot", "uploads", fileName);

            if (File.Exists(filePath))
                File.Delete(filePath);

            return Task.CompletedTask;
        }
    }
} 