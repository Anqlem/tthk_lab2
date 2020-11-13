﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tthk_lab2
{
    public partial class Form1 : Form
    {
        bool drawing;
        GraphicsPath currentPath;
        Point oldLocation;
        Pen currentPen;
        public Form1()
        {
            InitializeComponent();
            drawing = false;
            currentPen = new Pen(Color.Black);

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
            OpenFileDialog OP = new OpenFileDialog(); 
            OP.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|GIF Image|*.gif|PNG Image|*.png";

            OP.Title = "Open an Image File"; OP.FilterIndex = 1;

            if (OP.ShowDialog() != DialogResult.Cancel) 
                picDrawingSurface.Load(OP.FileName);

            picDrawingSurface.AutoSize = true;
        }

        private void picDrawingSurface_MouseDown(object sender, MouseEventArgs e)
        {
            if (picDrawingSurface.Image == null)
            {
                MessageBox.Show("Сначала создай новый файл!");
                return;
            }

            if(e.Button == MouseButtons.Left)
            {
                drawing = true;
                oldLocation = e.Location;
                currentPath = new GraphicsPath();
            }
        }

        private void picDrawingSurface_MouseUp(object sender, MouseEventArgs e)
        {
            drawing = false;
            try
            {
                currentPath.Dispose();
            }
            catch { };
        }

        private void picDrawingSurface_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawing)
            {
                Graphics g = Graphics.FromImage(picDrawingSurface.Image);
                currentPath.AddLine(oldLocation, e.Location);
                g.DrawPath(currentPen, currentPath);
                oldLocation = e.Location;
                g.Dispose();
                picDrawingSurface.Invalidate();
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
