using System;
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
        public static Color cValmain;
        public int defColor;
        bool drawing;
        int historyCounter;
        GraphicsPath currentPath;
        Point oldLocation;
        public static Pen currentPen;
        Color historyColor;
        List<Image> History;
        Form2 newForm;
        
        public Form1()
        {
            InitializeComponent();
            drawing = false;
            currentPen = new Pen(Color.Black);
            History = new List<Image>();
            newForm = new Form2(cValmain);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void _new_Click(object sender, EventArgs e)
        {
            Bitmap pic = new Bitmap(709, 377);
            picDrawingSurface.Image = pic;
            picDrawingSurface.BackColor = Color.White;

            Graphics g = Graphics.FromImage(picDrawingSurface.Image);
            g.Clear(Color.White);
            g.Dispose();
            picDrawingSurface.Invalidate();

            History.Clear();
            historyCounter = 0;
            History.Add(new Bitmap(picDrawingSurface.Image));
            if (picDrawingSurface.Image == null)
            {
                MessageBox.Show("Сначала создайте новый файл");
                return;
            }
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

            if(e.Button == MouseButtons.Right)
            {
                drawing = true;
                oldLocation = e.Location;
                currentPath = new GraphicsPath();
                Console.WriteLine(historyColor);
                currentPen.Color = System.Drawing.Color.White;
            }
            if (e.Button == MouseButtons.Left)
            {
                drawing = true;
                oldLocation = e.Location;
                currentPath = new GraphicsPath();
                historyColor = currentPen.Color;
                currentPen.Color = Form2.colorResult;
            }
        }

        private void picDrawingSurface_MouseUp(object sender, MouseEventArgs e)
        {
            History.Add(new Bitmap(picDrawingSurface.Image));
            historyCounter++;

            if (historyCounter > 10)
            {
                History.RemoveAt(0);
                historyCounter--;
            }

            drawing = false;
            currentPen.Color = historyColor;
            try
            {
                currentPath = new GraphicsPath();
                currentPath.Dispose();
            }
            catch { };
        }

        private void picDrawingSurface_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Text = e.X.ToString() + ", " + e.Y.ToString();
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
            Bitmap pic = new Bitmap(709, 377);
            picDrawingSurface.Image = pic;
            picDrawingSurface.BackColor = Color.White;

            Graphics g = Graphics.FromImage(picDrawingSurface.Image);
            g.Clear(Color.White);
            g.Dispose();
            picDrawingSurface.Invalidate();

            History.Clear();
            historyCounter = 0;
            History.Add(new Bitmap(picDrawingSurface.Image));
            if (picDrawingSurface.Image == null)
            {
                MessageBox.Show("Сначала создайте новый файл");
                return;
            }

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


        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(cValmain);
            Form2.pictureBox1.BackColor = Form2.colorResult;
            form2.Show();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            currentPen.Width = trackBar1.Value;
        }

        private void dotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentPen.DashStyle = DashStyle.Dot;

            solidToolStripMenuItem.Checked = false;
            dotToolStripMenuItem.Checked = true;
            dashDotDotToolStripMenuItem.Checked = false;
        }

        private void dashDotDotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentPen.DashStyle = DashStyle.DashDotDot;
            solidToolStripMenuItem.Checked = false;
            dotToolStripMenuItem.Checked = false;
            dashDotDotToolStripMenuItem.Checked = true;
        }

        private void solidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentPen.DashStyle = DashStyle.Solid;

            solidToolStripMenuItem.Checked = true;
            dotToolStripMenuItem.Checked = false;
            dashDotDotToolStripMenuItem.Checked = false;
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (History.Count != 0 && historyCounter != 0)
            {
                picDrawingSurface.Image = new Bitmap(History[--historyCounter]);
            }
            else MessageBox.Show("История пуста");
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (++historyCounter < History.Count)
            {
                historyCounter--;
                picDrawingSurface.Image = new Bitmap(History[++historyCounter]);
            }
            else
            {
                MessageBox.Show("История пуста");
            }
        }
    }
}
