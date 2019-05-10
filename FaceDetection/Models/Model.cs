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
            //tutaj czarujemy ze zdjęciem photos[index]
            CascadeClassifier haarFace = new CascadeClassifier("haarcascade_frontalface_default.xml");
            CascadeClassifier haarEyes = new CascadeClassifier("haarcascade_eye.xml");

            Image<Bgr, Byte> imageCV = new Image<Bgr, byte>(photos[index].Btm);
            Mat mat = imageCV.Mat;
            var faces = haarFace.DetectMultiScale(mat, 1.1, 10, new Size(20, 20));
            MCvScalar RED = new MCvScalar(0, 0, 255);
            MCvScalar GREEN = new MCvScalar(0, 255, 0);
            foreach (var face in faces)
            {
                //Console.WriteLine($"{face.Left} : {face.Top} \n {face.Width}x{face.Height}");
                CvInvoke.Rectangle(imageCV, face, RED, 2);
                imageCV.ROI = face;
                var eyes = haarEyes.DetectMultiScale(imageCV, 1.1, 10, new Size(20, 20));
                foreach(var eye in eyes)
                {
                    CvInvoke.Rectangle(imageCV, eye, GREEN, 2);
                }
                imageCV.Save(@"../../Changed Photos/" + photos[index].FileName + @"_changed.jpeg");
            }
        }

    }
}
