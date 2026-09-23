using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Grad_Project.Services
{
    public class AttachmentService
    {
        private readonly int _maxImageSize = 5 * 1024 * 1024; // 5MB
        private readonly string[] _validExtensions = [".png", ".jpg", ".jpeg"];
        private readonly IWebHostEnvironment _env;

        public AttachmentService(IWebHostEnvironment env)
        {
            _env = env;
        }

        private string GetRootPath()
        {
            return _env.WebRootPath ?? Path.Combine(_env.ContentRootPath, "wwwroot");
        }

        public bool Delete(string fileName, string folderName)
        {
            if (string.IsNullOrWhiteSpace(fileName) || string.IsNullOrWhiteSpace(folderName)) return false;

            var filePath = Path.Combine(GetRootPath(), folderName, fileName);

            try
            {
                if (!File.Exists(filePath)) return false;
                File.Delete(filePath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public (Stream stream, string contentType)? GetFile(string fileName, string folderName)
        {
            if (string.IsNullOrWhiteSpace(fileName) || string.IsNullOrWhiteSpace(folderName)) return null;

            var filePath = Path.Combine(GetRootPath(), folderName, fileName);
            if (!File.Exists(filePath)) return null;

            var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var extension = Path.GetExtension(filePath).ToLower();

            var contentType = extension switch
            {
                ".png" => "image/png",
                ".jpg" or ".jpeg" => "image/jpeg",
                _ => "application/octet-stream"
            };

            return (stream, contentType);
        }

        public async Task<string?> UploadAsync(Stream stream, string fileName, string folderName, CancellationToken ct = default)
        {
            if (stream == null || !stream.CanRead) return null;
            if (stream.Length == 0 || stream.Length > _maxImageSize) return null;

            var extension = Path.GetExtension(fileName).ToLower();
            if (string.IsNullOrWhiteSpace(extension) || !_validExtensions.Contains(extension)) return null;

            var uploadFolder = Path.Combine(GetRootPath(), folderName);
            Directory.CreateDirectory(uploadFolder); // Automatically creates wwwroot and your target folder if they don't exist

            var storedFileName = $"{Guid.NewGuid()}{fileName}";
            var filePath = Path.Combine(uploadFolder, storedFileName);

            try
            {
                using var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                await stream.CopyToAsync(fs, ct);
                return storedFileName;
            }
            catch
            {
                return null;
            }
        }
    }
}