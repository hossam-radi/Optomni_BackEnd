using System.Threading.Tasks;

namespace Optomni.Utilities.FileUpload
{
    public interface IFileUpload
    {
        Task<string> UploadImage(byte[] file, string directoryPath, string fileName);
        Task<string> UploadFile(byte[] file, string directoryPath, string fileName);
        Task<bool> DeleteImage( string fullImagePath);
    }
}
