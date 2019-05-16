using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

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
                    //Console.WriteLine(file);
                }
            }
        }

        public Bitmap NextImage()
        {
            CzaryMary();

            //zapisanie zdjecia do folderu "Changed Photos" znajdujacego sie w projekcie"
            if (!System.IO.Directory.Exists(@"../../Changed Photos/"))System.IO.Directory.CreateDirectory(@"../../Changed Photos/");
            //photos[index].Btm.Save(@"../../Changed Photos/"  + photos[index].FileName + @"_changed.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);

            return photos[index++].Btm;
             
        }

        public void CzaryMary()
        {
            MCvScalar RED = new MCvScalar(0, 0, 255);
            MCvScalar GREEN = new MCvScalar(0, 255, 0);
            MCvScalar BLUE = new MCvScalar(255, 0, 0);

            //klasyfikatory
            CascadeClassifier haarFace = new CascadeClassifier("haarcascade_frontalface_default.xml");
            CascadeClassifier haarEyes = new CascadeClassifier("haarcascade_eye.xml");
            CascadeClassifier haarMouth = new CascadeClassifier("haarcascade_smile.xml");

            //tutaj bitmapa jest konwertowana na obiekt Image z opencv
            Image<Bgr, Byte> imageCV = new Image<Bgr, byte>(photos[index].Btm);

            // faces to jest kolekcja rectangli z twarzami, czyli współrzędne wszystkich twarz znalezionych na zdjęciu
            var faces = haarFace.DetectMultiScale(imageCV, 1.1, 10, new Size(20, 20));
            foreach (var face in faces)
            {
                // to niżej po prostu rysuje kwadrat na prostokącie z twarzą
                CvInvoke.Rectangle(imageCV, face, RED, 2);

                //obrazek jest zawężany tylko do mordy, a potem w tym obszarze szuka oczu no i rysuje na nich zielone prostokąty
                imageCV.ROI = face;
                var eyes = haarEyes.DetectMultiScale(imageCV, 1.1, 10, new Size(20, 20));
                var smiles = haarMouth.DetectMultiScale(imageCV, 1.1, 20, new Size(20, 20));

                var imageparts = new List<Image<Bgr, byte>>();

                int i = 1;
                foreach (var eye in eyes.Reverse())
                {
                    if (i > 2) break;
                    var xd = eye;
                    CvInvoke.Rectangle(imageCV, eye, GREEN, 2);

                    xd.X += face.X;
                    xd.Y += face.Y;

                    imageCV.ROI = xd;

                    var ziomek = imageCV.HoughCircles(new Bgr(), new Bgr(), 60, 60);
                    ziomek.Save(@"../../Changed Photos/chuj.jpeg");

                    imageCV.Save(@"../../Changed Photos/" + photos[index].FileName + @"_eye" +i +".jpeg");

                    imageCV.ROI = face;

                    i++;

                }
                i = 1;
                foreach (var smile in smiles)
                {
                    if (i > 1) break;
                    CvInvoke.Rectangle(imageCV, smile, BLUE, 2);
                    i++;
                }
                imageCV.Save(@"../../Changed Photos/" + photos[index].FileName + @"_changed.jpeg");
            }
        }

    }
}
