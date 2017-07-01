using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roglaza.Forms
{
    public partial class ImagePreviewer : Form
    {
        public ImagePreviewer()
        {
            InitializeComponent();
        }

        public string Imagepath="";

        private void ImagePreviewer_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = comboBox1.Items.Count-1;
            try{
                pictureBox1.Image = Image.FromFile(this.Imagepath);
            }
            catch{}
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(comboBox1.SelectedItem.ToString().ToLower())
            {
                case "autosize": this.pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize; break;
                case "center": this.pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage; break;
                case "normal": this.pictureBox1.SizeMode = PictureBoxSizeMode.Normal; break;
                case "stretch": this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage; break;
                case "zoom": this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom; break;

        }
        }
    }
}
