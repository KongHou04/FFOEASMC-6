using System.Text;

namespace Restaurant.Helpers
{
    public class ImgHelper(IWebHostEnvironment webHostEnvironment)
    {
        // Upload and save a new image
        public string? SaveImage(IFormFile imageFile, string folderPath)
        {
            if (imageFile == null || imageFile.Length == 0)
                return null;

            var uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, folderPath);
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }

            return fileName;
        }

        // Replace an existing image
        public string? ReplaceImage(IFormFile newImageFile, string oldImageFileName, string folderPath)
        {
            // Delete the old image
            if (!string.IsNullOrEmpty(oldImageFileName))
            {
                DeleteImage(oldImageFileName, folderPath);
            }

            // Save the new image
            return SaveImage(newImageFile, folderPath);
        }

        // Delete an image
        public void DeleteImage(string fileName, string folderPath)
        {
            var filePath = Path.Combine(webHostEnvironment.WebRootPath, folderPath, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
