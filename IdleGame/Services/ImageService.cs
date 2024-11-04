using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdleGame.Properties;

namespace IdleGame.Services
{
    internal static class ImageService
    {
        public static Image GetItemImage(string imageName)
        {
            if (imageName == null) return Resources.NoAsset;

            Console.WriteLine($"Getting image '{imageName}'");
            var path = PathService.GetItemImagePath(imageName);
            var image = Image.FromFile(path);

            if (image == null)
            {
                throw new Exception($"Image '{imageName}' not found");
            }

            return image; 
        }
    }
}
