namespace HeroesAndVillains.Tests.Common.Resources.Helpers
{
    public class ImagesHelper
    {
        private string _imagesDirectory;

        public ImagesHelper()
        {
            _imagesDirectory = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\Images");
        }

        public byte[] Invincible() => LoadImage(_imagesDirectory, "invincible.jpg");

        public byte[] RexSplode() => LoadImage(_imagesDirectory, "rex-splode.jpg");

        private byte[] LoadImage(string path, string imageName) 
        {
            var imagePath = Path.Combine(path, imageName);
            return File.ReadAllBytes(imagePath);
        }
    }
}
