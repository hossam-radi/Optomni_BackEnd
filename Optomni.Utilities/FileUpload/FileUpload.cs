using Microsoft.Extensions.Options;
using Optomni.Utilities.Settings;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Optomni.Utilities.FileUpload
{
    public class FileUpload : IFileUpload
    {
        private readonly OptmniSettings _settings;

        public FileUpload(  IOptions<OptmniSettings> settings)
        {
           // _s3Client = s3Client;
            _settings = settings.Value;
        }
        public async Task<string> UploadImage(byte[] file, string directoryPath, string fileName)
        {
            if (CheckIfImageFile(file))
            {
                Directory.CreateDirectory(directoryPath);

                return await WriteFile(file, directoryPath, fileName);
            }

            return "Invalid image file";
        }

        public async Task<string> UploadFile(byte[] file, string directoryPath, string fileName)
        {
            Directory.CreateDirectory(directoryPath);

            return await WriteFile(file, directoryPath, fileName);
        }

        /// <summary>
        /// Method to check if file is image file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool CheckIfImageFile(byte[] file)
        {
            return ImageHelper.GetImageFormat(file) != ImageHelper.ImageFormat.unknown;
        }

        /// <summary>
        /// Method to write file onto the disk
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<string> WriteFile(byte[] file, string directoryPath, string fileName)
        {
            try
            {
                var extension = "." + ImageHelper.GetImageFormat(file);
                string filePath = Path.Combine(directoryPath, fileName);

                File.WriteAllBytes(filePath, file);

            }
            catch (Exception e)
            {
                return e.Message;
            }

            return fileName;
        }

        public async Task<bool> DeleteImage( string imagePath)
        {
            //var path = Path.Combine("Uploads", imageName);

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
                return true;
            }
            else return false;
        }

    
    }
}
