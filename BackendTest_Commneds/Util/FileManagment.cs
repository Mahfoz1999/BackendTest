using BackendTest_Commneds.Exceptions;
using Microsoft.AspNetCore.Http;

namespace BackendTest_Commneds.Util;

public class FileManagment
{
    public static async Task<List<string>> SaveFiles(IFormFileCollection files)
    {
        var fileUrls = new List<string>(); foreach (var file in files)
        {
            ValidateFileExtension(file, new string[] { ".jpg", ".jpeg" }); ValidateFileSize(file, 1 * 1024 * 1024); // 1 MB (1 * 1024 * 1024 bytes)

            // Generate a unique file name
            string folder = $"Portfolios/" + Guid.NewGuid().ToString() + "_" + Path.ChangeExtension(file.FileName, file.FileName.ToLower());
            string serverFolder = Path.Combine("wwwroot/", folder);

            // Check if the file name already exists and generate a new one if needed
            while (File.Exists(serverFolder))
            {
                folder = $"Portfolios/" + Guid.NewGuid().ToString() + "_" + Path.ChangeExtension(file.FileName, file.FileName.ToLower());
                serverFolder = Path.Combine("wwwroot/", folder);
            }

            // Asynchronously load and process the image
            using (var stream = new FileStream(serverFolder, FileMode.Create))
            {
                await file.CopyToAsync(stream);

                // Reset the stream position for reading
                stream.Seek(0, SeekOrigin.Begin);

                using (var image = await SixLabors.ImageSharp.Image.LoadAsync(stream))
                {
                    // Compress image by 25%
                    int compressedWidth = (int)(image.Width * 0.75);
                    int compressedHeight = (int)(image.Height * 0.75);
                    var resizeOptions = new SixLabors.ImageSharp.Processing.ResizeOptions
                    {
                        Mode = SixLabors.ImageSharp.Processing.ResizeMode.Max,
                        Sampler = SixLabors.ImageSharp.Processing.KnownResamplers.Lanczos3
                    };
                    image.Mutate(x => x.Resize(compressedWidth, compressedHeight, true));

                    // Asynchronously save the compressed image to the stream
                    stream.Position = 0;
                    await image.SaveAsync(stream, new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder());
                }
            }

            fileUrls.Add(folder);
        }
        return fileUrls;
    }
    public static async Task<string> SaveFile(IFormFile file)
    {

        ValidateFileExtension(file, new string[] { ".jpg", ".jpeg" });
        ValidateFileSize(file, 1 * 1024 * 1024); // 1 MB (1 * 1024 * 1024 bytes)

        // Generate a unique file name
        string folder = $"Portfolios/" + Guid.NewGuid().ToString() + "_" + Path.ChangeExtension(file.FileName, file.FileName.ToLower());
        string serverFolder = Path.Combine("wwwroot/", folder);

        // Check if the file name already exists and generate a new one if needed
        while (File.Exists(serverFolder))
        {
            folder = $"Portfolios/" + Guid.NewGuid().ToString() + "_" + Path.ChangeExtension(file.FileName, file.FileName.ToLower());
            serverFolder = Path.Combine("wwwroot/", folder);
        }

        // Asynchronously load and process the image
        using (var stream = new FileStream(serverFolder, FileMode.Create))
        {
            await file.CopyToAsync(stream);

            // Reset the stream position for reading
            stream.Seek(0, SeekOrigin.Begin);

            using (var image = await SixLabors.ImageSharp.Image.LoadAsync(stream))
            {
                // Compress image by 25%
                int compressedWidth = (int)(image.Width * 0.75);
                int compressedHeight = (int)(image.Height * 0.75);
                var resizeOptions = new SixLabors.ImageSharp.Processing.ResizeOptions
                {
                    Mode = SixLabors.ImageSharp.Processing.ResizeMode.Max,
                    Sampler = SixLabors.ImageSharp.Processing.KnownResamplers.Lanczos3
                };
                image.Mutate(x => x.Resize(compressedWidth, compressedHeight, true));

                // Asynchronously save the compressed image to the stream
                stream.Position = 0;
                await image.SaveAsync(stream, new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder());
            }
        }

        return folder;
    }
    public static void ValidateFileExtension(IFormFile file, string[] allowedExtensions)
    {
        string extension = Path.GetExtension(file.FileName);
        if (!allowedExtensions.Contains(extension.ToLower()))
        {
            throw new ValidException("Invalid file format. Only the following file extensions are allowed: " + string.Join(", ", allowedExtensions));
        }
    }
    public static void ValidateFileSize(IFormFile file, long maxFileSize)
    {
        if (file.Length > maxFileSize)
        {
            throw new ValidException("File size exceeds the maximum allowed limit of " + maxFileSize + " bytes.");
        }
    }
    public static void DeleteFile(string url)
    {
        File.Delete("wwwroot/" + url);
    }

    public static void DeleteFiles(List<string> urls)
    {
        urls.ForEach(e => File.Delete("wwwroot/" + e));
    }
}
