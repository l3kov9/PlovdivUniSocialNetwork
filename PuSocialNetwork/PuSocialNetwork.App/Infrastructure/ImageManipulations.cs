namespace PuSocialNetwork.App.Infrastructure
{
    using ImageMagick;

    public static class ImageManipulations
    {
        public static void Resize(string inputPath)
        {
            const int size = 150;
            const int quality = 75;

            var outputPath = string.Empty;

            using (var image = new MagickImage(inputPath))
            {
                image.Resize(size, size);
                image.Strip();
                image.Quality = quality;
                image.Write(outputPath);
            }
        }

    }
}