using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace BLL.Helpers
{
    public interface IFileValidate
    {
        (bool Valid, string ErrorMessage) ValidateFile(IFormFile filetoValidate);
    }
    
    public class FileValidate: IFileValidate 
    {
        
        public (bool Valid, string ErrorMessage) ValidateFile(IFormFile fileToValidate)
        {
            if (fileToValidate == null)
            {
                return (false, $"Provide Valid Image File");
            }
            if (!CheckIfImageFile(fileToValidate))
            {
                return (false, $"Provide Valid Image File");
            }
            return (true, $"Sample error message");

        }

        private bool CheckIfImageFile(IFormFile file)
        {
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            return GetImageFormat(fileBytes) != ImageFormat.unknown;
        }

        public  ImageFormat GetImageFormat(byte[] bytes)
        {
            var bmp = Encoding.ASCII.GetBytes("BM");     // BMP
            var gif = Encoding.ASCII.GetBytes("GIF");    // GIF
            var png = new byte[] { 137, 80, 78, 71 };              // PNG
            var tiff = new byte[] { 73, 73, 42 };                  // TIFF
            var tiff2 = new byte[] { 77, 77, 42 };                 // TIFF
            var jpeg = new byte[] { 255, 216, 255, 224 };          // jpeg
            var jpeg2 = new byte[] { 255, 216, 255, 225 };         // jpeg canon



            if (png.SequenceEqual(bytes.Take(png.Length)))
                return ImageFormat.png;

            if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
                return ImageFormat.jpeg;

            if (jpeg2.SequenceEqual(bytes.Take(jpeg2.Length)))
                return ImageFormat.jpeg;

            return ImageFormat.unknown;
        }

        public enum ImageFormat
        {

            jpeg,
            png,
            unknown
        }
    }
}