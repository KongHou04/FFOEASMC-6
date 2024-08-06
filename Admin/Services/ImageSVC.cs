namespace Customer.Services
{
    public class ImageSVC
    {
        private string? ImgPath { get; set; } = "https://localhost:7032/products";

        public string GetProductImg(string imgName)
        {
            return ImgPath + "/" + imgName;
        }

    }
}
