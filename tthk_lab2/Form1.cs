using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tthk_lab2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void _new_Click(object sender, EventArgs e)
        {
            Bitmap pic = new Bitmap(946,481);
            picDrawingSurface.Image = pic;

            if (picDrawingSurface.Image != null)
            {
                var result = MessageBox.Show("Сохранить текущее изображение перед созданием нового?", "Предупреждение", MessageBoxButtons.YesNoCancel);
                
                switch (result)
                {
                    case DialogResult.No: break;
                    case DialogResult.Yes: _save_Click(sender, e); break;
                    case DialogResult.Cancel: return;

                }
            }

        }

        private void _open_Click(object sender, EventArgs e)
        {

        }

        private void picDrawingSurface_Click(object sender, EventArgs e)
        {
            if (picDrawingSurface.Image == null)
            {
                MessageBox.Show("Сначала создай новый файл!");
                return;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Bitmap pic = new Bitmap(946, 481);
            picDrawingSurface.Image = pic;

            if (picDrawingSurface.Image != null)
            {
                var result = MessageBox.Show("Сохранить текущее изображение перед созданием нового?", "Предупреждение", MessageBoxButtons.YesNoCancel);

                switch (result)
                {
                    case DialogResult.No: break;
                    case DialogResult.Yes: _save_Click(sender, e); break;
                    case DialogResult.Cancel: return;

                }
            }
        }

        private void _save_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveDlg = new SaveFileDialog();
            SaveDlg.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|GIF Image|*.gif|PNG Image|*.png";
            SaveDlg.Title = "Save An Image File";
            SaveDlg.FilterIndex = 4; //Default format is .PNG
            SaveDlg.ShowDialog();

            if(SaveDlg.FileName!="") //Если введено не путсое имя
            {
                System.IO.FileStream fs = (System.IO.FileStream)SaveDlg.OpenFile();

                switch (SaveDlg.FilterIndex)
                {
                    case 1:
                        this.picDrawingSurface.Image.Save(fs, ImageFormat.Jpeg);
                        break;
                    case 2:
                        this.picDrawingSurface.Image.Save(fs, ImageFormat.Bmp);
                        break;
                    case 3:
                        this.picDrawingSurface.Image.Save(fs, ImageFormat.Gif);
                        break;
                    case 4:
                        this.picDrawingSurface.Image.Save(fs, ImageFormat.Png);
                        break;
                }
                fs.Close();
            }
        }
    }
}
