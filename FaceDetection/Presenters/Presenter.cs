using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceDetection
{
    class Presenter
    {
        IView view;
        Model model;
        public Presenter(IView view, Model model)
        {
            this.view = view;
            this.model = model;
            this.view.LoadPhotos += View_LoadImages;
            this.view.NextPhoto += View_NextImage;
        }

        private void View_LoadImages(string path)
        {
            model.LoadImages(path);
            view.PhotoBtm = model.NextImage();
            view.Status = model.Index + "/" + model.NumberOfPhotos;
        }

        private void View_NextImage()
        {
            if (model.Index != model.NumberOfPhotos)
            {
                view.PhotoBtm = model.NextImage();
                view.Status = model.Index + "/" + model.NumberOfPhotos;
            }
        }

        
    }
}
