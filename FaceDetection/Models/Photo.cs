using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace FaceDetection
{
    class Photo
    {
        public string FileName { get; set; }

        public Bitmap Btm{get; set;}

        public int[,] R { get; set; }
        public int[,] G { get; set; }
        public int[,] B { get; set; }
        
        public Photo(string path)
        {
            FileName = Path.GetFileNameWithoutExtension(path);
            Btm = new Bitmap(path);
            R = new int[Btm.Width, Btm.Height];
            G = new int[Btm.Width, Btm.Height];
            B = new int[Btm.Width, Btm.Height];
        }
    }
}
