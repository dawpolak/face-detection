using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FaceDetection
{
    interface IView
    {
        Bitmap PhotoBtm { set; }
        string Status { set; }
        event Action<string> LoadPhotos;
        event Action NextPhoto;

    }
}
