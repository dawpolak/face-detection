using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace FaceDetection
{
    class Model
    {
        private List<Photo> photos = new List<Photo>();
        private int index = 0;

        public int Index { get => index; }
        public int NumberOfPhotos { get => photos.Count(); }

        public void LoadImages(string path)
        {
            //pobranie do tablicy listy zdjec (.jpg i .jpeg) znajdujacych sie w sciezce z argumentu
            //narazie jest sztywno w kodzie podany folder "Photos" znajdujacy sie w projekcie
            string[] files = Directory.GetFiles(path);
            photos.Clear();
            index = 0;
            foreach (var file in files)
            {
                if ((Path.GetExtension(file) == ".jpg") || (Path.GetExtension(file) == ".jpeg"))
                {
                    photos.Add(new Photo(file));
                    Console.WriteLine(file);
                }
            }
        }

        public Bitmap NextImage()
        {
            CzaryMary();

            //zapisanie zdjecia do folderu "Changed Photos" znajdujacego sie w projekcie"
            if (!System.IO.Directory.Exists(@"../../Changed Photos/"))System.IO.Directory.CreateDirectory(@"../../Changed Photos/");
            photos[index].Btm.Save(@"../../Changed Photos/"  + photos[index].FileName + @"_changed.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);

            return photos[index++].Btm;
             
        }

        public void CzaryMary()
        {
            //tutaj czarujemy ze zdjęciem photos[index]

        }

    }
}
