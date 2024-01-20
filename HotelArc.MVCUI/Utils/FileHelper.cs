namespace HotelArc.MVCUI.Utils
{
    public class FileHelper
    {
        public static async Task<string> FileLoaderAsync(IFormFile formFile, string filePath = "/wwwroot/img/")
        {
            string fileName = "";

            if (formFile != null && formFile.Length > 0)
            {
                fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), filePath, fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
            }
            return fileName;
        }
    }
}
