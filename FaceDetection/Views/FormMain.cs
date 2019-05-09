using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FaceDetection
{
    public partial class FormMain : Form,IView
    {
        public FormMain()
        {
            InitializeComponent();
        }

        public Bitmap PhotoBtm { set => pictureBoxFace.Image = value; }
        public string Status { set => labelStatus.Text = value; }
        #region IView
        public event Action<string> LoadPhotos;
        public event Action NextPhoto;
        #endregion

        private void buttonNext_Click(object sender, EventArgs e)
        {
            //Image img1 = new Image(@"..\..\astra.jpg");
            //pictureBoxFace.Image =img1.Btm;
            NextPhoto?.Invoke();
        }

        private void buttonLearn_Click(object sender, EventArgs e)
        {
            //FolderBrowserDialog fbd = new FolderBrowserDialog();
            //fbd.RootFolder = Environment.SpecialFolder.MyComputer;
            //fbd.Description = "Wybierz folder ze zdjeciami do nauki";
            //fbd.ShowNewFolderButton = false;

            //if(fbd.ShowDialog()==DialogResult.OK)
            //{
            //    LoadImages?.Invoke(fbd.SelectedPath);
            //}

            LoadPhotos?.Invoke(@"..\..\Images");
            buttonNext.Visible = true;
            labelStatus.Visible = true;
        }
    }
}
