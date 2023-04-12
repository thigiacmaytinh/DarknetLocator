using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.ComponentModel;
using TGMTcs;
using System.Threading;
using System.Linq;
using System.Drawing.Imaging;

namespace DarknetLocator
{
    public partial class Form1 : Form
    {

        public enum Colision
        {
            None,
            NewRect,
            All,
            Top,
            Right,
            Bottom,
            Left,
            TopLeft,
            TopRight,
            BotLeft,
            BotRight
        }

        Size MAX_PICTURE_BOX_SIZE = new Size(800, 600);

        int ANCHOR_WIDTH = 6;
        Size ANCHOR_SIZE;
        Size mDistanceCurrentRectToMouse;
        Point mCurrentRectBottomRight;

        //public string textFilePath;
        Point currentPoint;
        Point startPoint;
        bool g_isMouseDown = false;
        double g_scaleX = 0;
        double g_scaleY = 0;
        double g_aspect = 0;
        Colision g_colisionState = Colision.None;
        int newRectIdx = -1;

        string mCurrentImgName = "";
        int mTotalRects = 0;
        bool m_isFirstLoading = true;

        //use for draw on picture box
        List<Rectangle> mRects = new List<Rectangle>();
        Image m_img;

        Font myFont = new Font("Arial", 14);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush blueBrush = new SolidBrush(Color.Blue);

        string[] m_classes;
        string m_saveDir = "";
        int m_lastSearchIndex = 0;
        string m_lastSearch = "";

        bool m_isTextboxFocused = false;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        #region COMMON

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        Colision CheckColision(Point p, Rectangle rect)
        {
            int distanceLeft = Math.Abs(p.X - rect.X);
            int distanceTop = Math.Abs(p.Y - rect.Y);
            int distanceRight = Math.Abs(p.X - (rect.X + rect.Width));
            int distanceBottom = Math.Abs(p.Y - (rect.Y + rect.Height));

            double offset = ANCHOR_WIDTH / 2;

            if (distanceLeft <= offset && distanceTop <= offset)
            {
                return Colision.TopLeft;
            }
            else if (distanceTop < 3 && distanceRight < 3)
            {
                return Colision.TopRight;
            }
            else if (distanceBottom < 3 && distanceRight < 3)
            {
                return Colision.BotRight;
            }
            else if (distanceBottom < 3 && distanceLeft < 3)
            {
                return Colision.BotLeft;
            }
            else if (distanceTop <= offset && p.X > rect.X + rect.Width / 2 - offset && p.X < rect.X + rect.Width / 2 + offset)
            {
                return Colision.Top;
            }
            else if (distanceRight <= offset && p.Y > rect.Y + rect.Height / 2 - offset && p.Y < rect.Y + rect.Height / 2 + offset)
            {
                return Colision.Right;
            }
            else if (distanceBottom <= offset && p.X > rect.X + rect.Width / 2 - offset && p.X < rect.X + rect.Width / 2 + offset)
            {
                return Colision.Bottom;
            }
            else if (distanceLeft <= offset && p.Y > rect.Y + rect.Height / 2 - offset && p.Y < rect.Y + rect.Height / 2 + offset)
            {
                return Colision.Left;
            }
            else if ((distanceTop <= offset && p.X > rect.X && p.X < (rect.X + rect.Width)) ||
                    (distanceRight <= offset && p.Y > rect.Y && p.Y < (rect.Y + rect.Height)) ||
                    (distanceBottom <= offset && p.X > rect.X && p.X < (rect.X + rect.Width)) ||
                    (distanceLeft <= offset && p.Y > rect.Y && p.Y < (rect.Y + rect.Height)))
            {
                return Colision.All;
            }
            else
            {
                return Colision.None;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        Rectangle GetCurrentRect()
        {
            if (lstRect.SelectedIndex == -1 && lstRect.Items.Count > 0)
                lstRect.SelectedIndex = 0;
            return GetRect(lstRect.SelectedIndex);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        Rectangle GetRect(int index)
        {
            return mRects[index];
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        void SetCurrentRect(Rectangle rect)
        {
            mRects[lstRect.SelectedIndex] = rect;

            double x = rect.X * g_scaleX / m_img.Width;
            double y = rect.Y * g_scaleY / m_img.Height;
            double w = rect.Width * g_scaleX / m_img.Width;
            double h = rect.Height * g_scaleY / m_img.Height;
            double cx = x + w / 2;
            double cy = y + h / 2;
            lstRect.Items[lstRect.SelectedIndex] = cb_classes.SelectedIndex + " " + cx + " " + cy + " " + w + " " + h;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        void ChangeCursor(Point p)
        {
            if (lstRect.Items.Count == 0)
                g_colisionState = Colision.None;
            if (p.X > PictureBox1.Width || p.Y > PictureBox1.Height)
            {
                this.Cursor = Cursors.Default;
                return;
            }

            for (int i = 0; i < lstRect.Items.Count; i++)
            {
                g_colisionState = CheckColision(p, GetRect(i));
                if (g_colisionState == Colision.All)
                    this.Cursor = Cursors.SizeAll;
                else if (g_colisionState == Colision.TopLeft || g_colisionState == Colision.BotRight)
                    this.Cursor = Cursors.SizeNWSE;
                else if (g_colisionState == Colision.TopRight || g_colisionState == Colision.BotLeft)
                    this.Cursor = Cursors.SizeNESW;
                else if (g_colisionState == Colision.Left || g_colisionState == Colision.Right)
                    this.Cursor = Cursors.SizeWE;
                else if (g_colisionState == Colision.Top || g_colisionState == Colision.Bottom)
                    this.Cursor = Cursors.SizeNS;
                else if (g_colisionState == Colision.NewRect)
                    this.Cursor = Cursors.Hand;
                else
                    this.Cursor = Cursors.Default;


                if (g_colisionState != Colision.None)
                {
                    if (i != lstRect.SelectedIndex)
                    {
                        this.Cursor = Cursors.SizeAll;
                    }
                    newRectIdx = i;
                    return;
                }
            }
            this.Cursor = Cursors.Default;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void MoveRect(Point mouseLocation)
        {
            Rectangle rect = GetCurrentRect();

            rect.X = mouseLocation.X - mDistanceCurrentRectToMouse.Width;
            rect.Y = mouseLocation.Y - mDistanceCurrentRectToMouse.Height;

            SetCurrentRect(rect);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void MoveRect(int dx, int dy)
        {
            Rectangle rect = GetCurrentRect();

            rect.X += dx;
            rect.Y += dy;

            SetCurrentRect(rect);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        void ResizeRect(Point mouseLocation)
        {
            //Debug.WriteLine("ResizeRect");
            Rectangle rect = GetCurrentRect();

            switch (g_colisionState)
            {
                case Colision.Left:
                    rect.Width = mCurrentRectBottomRight.X - rect.X;
                    rect.X = mouseLocation.X;
                    break;
                case Colision.TopLeft:
                    rect.Location = mouseLocation;
                    rect.Width = mCurrentRectBottomRight.X - rect.X;
                    rect.Height = mCurrentRectBottomRight.Y - rect.Y;
                    break;
                case Colision.Top:
                    rect.Y = mouseLocation.Y;
                    rect.Height = mCurrentRectBottomRight.Y - rect.Y;
                    break;
                case Colision.TopRight:
                    rect.Width = mouseLocation.X - rect.X;
                    rect.Height = mCurrentRectBottomRight.Y - rect.Y;
                    rect.Y = mouseLocation.Y;
                    break;
                case Colision.Right:
                    rect.Width = mouseLocation.X - rect.X;
                    break;
                case Colision.BotRight:
                    rect.Width = mouseLocation.X - rect.X;
                    rect.Height = mouseLocation.Y - rect.Y;
                    break;
                case Colision.Bottom:
                    rect.Height = mouseLocation.Y - rect.Y;
                    break;
                case Colision.BotLeft:
                    rect.X = mouseLocation.X;
                    rect.Width = mCurrentRectBottomRight.X - rect.X;
                    rect.Height = mouseLocation.Y - rect.Y;
                    break;

            }

            SetCurrentRect(rect);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        void ResizeRect(int dx, int dy)
        {
            Rectangle rect = GetCurrentRect();

            rect.Width += dx;
            rect.Height += dy;

            SetCurrentRect(rect);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        void CountTotalRect()
        {
            mTotalRects = lstRect.Items.Count;
            //for (int i = 0; i < lstImg.Items.Count; i++)
            //{
            //    string line = lstImg.Items[i].ToString();
            //    string[] lineSplit = line.Split(' ');
            //    if (lineSplit.Length > 1)
            //    {
            //        int count = 0;
            //        if (int.TryParse(lineSplit[1], out count))
            //            mTotalRects += count;
            //    }
            //}

            //lblTotalRect.Text = mTotalRects.ToString();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        Point Clamp(Point p, Point min, Point max)
        {
            if (p.X > max.X)
            {
                p.X = max.X;
            }
            else if (p.X < min.X)
            {
                p.X = min.X;
            }

            if (p.Y > max.Y)
            {
                p.Y = max.Y;
            }
            else if (p.Y < min.Y)
            {
                p.Y = min.Y;
            }
            return p;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        void PrintError(string message)
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Text = message;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        void PrintSuccess(string message)
        {
            lblMessage.ForeColor = Color.Green;
            lblMessage.Text = message;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        void PrintMessage(string message)
        {
            lblMessage.ForeColor = Color.Black;
            lblMessage.Text = message;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        void CompleteEdit()
        {
            //write rect to list image
            if (lstImg.SelectedItems.Count == 0)
                return;

            lstImg.Items[lstImg.SelectedIndices[0]].ForeColor = Color.Black;
            txtCount.Text = lstRect.Items.Count.ToString();
            CountTotalRect();
            SaveToFile();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        //write list image to file
        void SaveToFile()
        {
            if (lstImg.Items.Count == 0 || lstImg.SelectedIndices.Count == 0)
                return;

            TGMTregistry.GetInstance().SaveValue("imageIdx", lstImg.SelectedIndices[0]);

            lblMessage.Text = "Saving...";
            Thread.Sleep(1);
            string content = "";
            foreach (string item in lstRect.Items)
            {
                content += item + "\n";
            }
            if (content.Length > 0)
                content = content.Substring(0, content.Length - 1);

            if (content != "")
            {
                string txtPath = TGMTutil.CorrectPath(txtFolder.Text) + mCurrentImgName.Replace(Path.GetExtension(mCurrentImgName), ".txt");
                File.WriteAllText(txtPath, content);
            }

            PrintSuccess("Saved");
            timerClear.Start();
        }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        #region BACKGROUND_WORKER

        private void bgLoadFile_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

            if (!Directory.Exists(txtFolder.Text))
                return;

            var allowedExtensions = new[] { ".jpg", ".png", ".bmp", ".JPG", ".PNG", ".BMP" };
            var fileList = Directory.GetFiles(txtFolder.Text)
                .Where(file => allowedExtensions.Any(file.ToLower().EndsWith)).ToList();


            lstImg.Items.Clear();
            List<int> listNoTxt = new List<int>();
            int index = 0;

            lstImg.BeginUpdate();
            foreach (string filePath in fileList)
            {

                //if (!TGMTimage.IsImage(filePath))
                //continue;



                string fileName = Path.GetFileName(filePath);
                lstImg.Items.Add(fileName);

                string txtPath = filePath.Replace(Path.GetExtension(filePath), ".txt");

                if (!File.Exists(txtPath))
                {
                    listNoTxt.Add(index);
                    lstImg.Items[index].ForeColor = Color.Red;
                }
                index++;
            }


            lstImg.EndUpdate();

            //if (fileNotAdd.Count > 0)
            //{
            //    lblMessage.Text = "add new images in directory";
            //    string files = "";
            //    for (int i = 0; i < Math.Min(10, fileNotAdd.Count); i++)
            //    {
            //        files += fileNotAdd[i] + "\n";
            //    }
            //    if (fileNotAdd.Count > 10)
            //        files += "...";
            //    if (MessageBox.Show("Do you want add " + fileNotAdd.Count + " new images in folder " + txtFolder.Text + " ? \n" + files, "Detected new images in folder", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            //    {
            //        int count = lstImg.Items.Count;
            //        lstImg.Items.AddRange(fileNotAdd.ToArray());
            //        lblLstImg.Text = lstImg.SelectedIndex + 1 + " / " + lstImg.Items.Count;
            //        SaveToFile();
            //        lstImg.TopIndex = count - 3;
            //        lstImg.SelectedIndex = count;
            //        MessageBox.Show("Added " + fileNotAdd.Count + " new images");
            //    }
            //    else
            //    {
            //        string currentDir = TGMTutil.CorrectPath(txtFolder.Text);
            //        string newdir = currentDir + "notmarked\\";
            //        if (!Directory.Exists(newdir))
            //            Directory.CreateDirectory(newdir);

            //        int count = 0;
            //        string error = "";
            //        foreach (string filePath in fileNotAdd)
            //        {
            //            try
            //            {
            //                File.Move(currentDir + filePath, newdir + filePath);
            //                count++;
            //            }
            //            catch (Exception ex)
            //            {
            //                Debug.WriteLine(ex.Message);
            //                error = ex.Message;
            //            }
            //        }
            //        string message = "Moved " + count + " images to folder " + newdir;
            //        if (error != "")
            //            message += ".\n\n Something else has error: " + error;
            //        MessageBox.Show(message);
            //    }
            //}

            if (lstImg.SelectedIndices.Count > 0)
            {
                lblLstImg.Text = lstImg.SelectedIndices[0] + 1 + " / " + lstImg.Items.Count;
            }
            else
            {
                lblLstImg.Text = "0 / " + lstImg.Items.Count;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void bgLoadFile_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            CountTotalRect();
            this.Enabled = true;
            timerLoading.Stop();
            progressBar1.Value = progressBar1.Minimum;
            lblMessage.Text = "";

            if (m_isFirstLoading && lstImg.Items.Count > 0)
            {
                int imageIdx = TGMTregistry.GetInstance().ReadInt("imageIdx");
                if (imageIdx >= 0 && imageIdx <= lstImg.Items.Count)
                {
                    lstImg.Items[imageIdx].Selected = true;
                    lstImg.EnsureVisible(imageIdx);
                }
                m_isFirstLoading = false;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void bgLoadFile_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {

        }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        #region FORM

        public Form1()
        {
            InitializeComponent();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            ANCHOR_SIZE = new Size(ANCHOR_WIDTH, ANCHOR_WIDTH);


            
            txtFolder.Text = TGMTregistry.GetInstance().ReadString("folderPath");


            this.KeyPreview = true;
            this.Location = new Point(10, 10);

            this.Text += " " + TGMTutil.GetVersion();

#if DEBUG
            this.Text += " (Debug)";
#endif


            checkTextFileToolStripMenuItem.Visible = false;
            RectangleToolStripMenuItem.Visible = false;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        void LoadClasses()
        {
            if (!Directory.Exists(txtFolder.Text))
                return;

            cb_classes.Items.Clear();
            string classPath = TGMTutil.CorrectPath(txtFolder.Text) + "classes.txt";
            if (File.Exists(classPath))
            {
                m_classes = File.ReadAllLines(classPath);
                for (int i = 0; i < m_classes.Length; i++)
                {
                    cb_classes.Items.Add(m_classes[i]);
                }
            }
            else
            {
                m_classes = new string[]{"dog", "person", "cat", "tv", "car", "meatballs", "marinara sauce",
                    "tomato soup", "chicken noodle soup", "french onion soup", "chicken breast", "ribs", "pulled pork", "hamburger", "cavity"};
                cb_classes.Items.AddRange(m_classes);

                File.WriteAllLines(classPath, m_classes);
            }
            cb_classes.SelectedIndex = 0;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveToFile();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (m_isTextboxFocused)
                return;
            

            if (e.Control && e.KeyCode == Keys.S)
            {
                SaveToFile();
            }
            else if (e.KeyCode == Keys.Space)
            {
                if (lstRect.Items.Count >= 1)
                {
                    lstRect.SelectedIndex = 0;
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if(lstRect.SelectedIndex > -1)
                {
                    lstRect_KeyDown(sender, e);
                    return;
                }
            }
            else if ((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9))
            {
                if (lstRect.SelectedIndex > -1)
                {
                    int newClass = (int)e.KeyCode - 48;
                    if (newClass < cb_classes.Items.Count && newClass != cb_classes.SelectedIndex)
                    {
                        cb_classes.SelectedIndex = newClass;
                    }
                }
            }
            else if ((e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9))
            {
                if (lstRect.SelectedIndex > -1)
                {
                    int newClass = (int)e.KeyCode - 96;
                    if (newClass < cb_classes.Items.Count)
                    {
                        cb_classes.SelectedIndex = newClass;
                    }
                }
            }
            else if (e.KeyCode == Keys.A)
            {
                if(lstImg.SelectedIndices.Count > 0)
                {
                    int currentIdx = lstImg.SelectedIndices[0];
                    if (currentIdx > 0)
                    {

                        lstImg.Items[currentIdx - 1].Selected = true;
                        lstImg.EnsureVisible(currentIdx - 1);
                    }
                }                
            }
            else if (e.KeyCode == Keys.D)
            {
                if (lstImg.SelectedIndices.Count > 0)
                {
                    int currentIdx = lstImg.SelectedIndices[0];
                    if (currentIdx < lstImg.Items.Count - 1)
                    {

                        lstImg.Items[currentIdx + 1].Selected = true;
                        lstImg.EnsureVisible(currentIdx + 1);
                    }
                }                    
            }
            else if (e.KeyCode == Keys.Q)
            {
                GotoLastNotAnnotated();
            }
            else if (e.KeyCode == Keys.E)
            {
                GotoNextNotAnnotated();
            }
            else if (e.KeyCode == Keys.F)
            {
                if (e.Control)
                {
                    SearchFile();
                }
            }
            else if (lstRect.SelectedIndex > -1)
            {
                if (e.KeyCode == Keys.Up)
                {
                    if (e.Control) ResizeRect(0, -2); else MoveRect(0, -2);
                }
                else if (e.KeyCode == Keys.Down)
                {
                    if (e.Control) ResizeRect(0, 2); else MoveRect(0, 2);
                }
                else if (e.KeyCode == Keys.Left)
                {
                    if (e.Control) ResizeRect(-2, 0); else MoveRect(-2, 0);
                }
                else if (e.KeyCode == Keys.Right)
                {
                    if (e.Control) ResizeRect(2, 0); else MoveRect(2, 0);
                }
                else if (e.KeyCode == Keys.I)
                {
                    ResizeRect(0, -2);
                }
                else if (e.KeyCode == Keys.K)
                {
                    ResizeRect(0, 2);
                }
                else if (e.KeyCode == Keys.J)
                {
                    ResizeRect(-2, 0);
                }
                else if (e.KeyCode == Keys.L)
                {
                    ResizeRect(2, 0);
                }
            }
            else
            {
                e.Handled = false;
            }            
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Tab)
            {
                lstRect.SelectedIndex = 0;
                lstImg.EnsureVisible(0);                
            }
        }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        #region CONTROL

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            OpenFileDialog folderBrowser = new OpenFileDialog();
            folderBrowser.ValidateNames = false;
            folderBrowser.CheckFileExists = false;
            folderBrowser.CheckPathExists = true;
            // Always default to Folder Selection.
            folderBrowser.FileName = "Select folder contain image";
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                string folderPath = Path.GetDirectoryName(folderBrowser.FileName);
                txtFolder.Text = folderPath;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void txtFolder_TextChanged(object sender, EventArgs e)
        {
            string path = txtFolder.Text;
            if(path.Contains(" "))
            {
                MessageBox.Show("Folder path do not contain spaces");
                return;
            }

            if(!Directory.Exists(path))
            {
                lstImg.Items.Clear();
                lstRect.Items.Clear();
            }

            this.Enabled = false;
            timerLoading.Start();
            lblMessage.Text = "Loading file...";
            TGMTregistry.GetInstance().SaveValue("folderPath", path);

            LoadClasses();

            bgLoadFile.RunWorkerAsync();            
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void lstImg_SelectedIndexChanged(object sender, EventArgs e)
        {
            ErrorProvider1.Clear();
            if (lstImg.Items.Count == 0 || lstImg.SelectedIndices.Count == 0)
                return;

            if (string.IsNullOrEmpty(txtFolder.Text))
            {
                ErrorProvider1.SetError(btnSelectFolder, "Must select folder contain both text file and images file");
                return;
            }


            string imgName = lstImg.SelectedItems[0].Text.ToString();
            if (imgName == mCurrentImgName)
                return;

            mCurrentImgName = imgName;


            string imgPath = TGMTutil.CorrectPath(txtFolder.Text) + mCurrentImgName;
            if (!File.Exists(imgPath))
            {
                ErrorProvider1.SetError(lblLstImg, "Image selected not exist");
                PictureBox1.Image = null;
                return;
            }

            
            

            //clear
            lstRect.Items.Clear();
            mRects.Clear();


            m_img = TGMTimage.LoadBitmapWithoutLock(imgPath);
            PictureBox1.Image = m_img;
            lblLstImg.Text = (lstImg.SelectedIndices[0] + 1) + " / " + lstImg.Items.Count;


            g_aspect = (double)m_img.Width / (double)m_img.Height;

            //resize
            if (g_aspect > 4.0 / 3.0)
            {
                PictureBox1.Width = MAX_PICTURE_BOX_SIZE.Width;
                PictureBox1.Height = (int)(MAX_PICTURE_BOX_SIZE.Width / g_aspect);
            }
            else if (g_aspect < 4.0 / 3.0)
            {
                PictureBox1.Height = MAX_PICTURE_BOX_SIZE.Height;
                PictureBox1.Width = (int)(MAX_PICTURE_BOX_SIZE.Height * g_aspect);
            }
            g_scaleX = (double)m_img.Width / PictureBox1.Width;
            g_scaleY = (double)m_img.Height / PictureBox1.Height;

            string txtPath = TGMTutil.CorrectPath(txtFolder.Text) + mCurrentImgName.Replace(Path.GetExtension(mCurrentImgName), ".txt");
            if (File.Exists(txtPath))
            {
                string[] lines = File.ReadAllLines(txtPath);

                foreach (string line in lines)
                {
                    lstRect.Items.Add(line);

                    string[] lineSplit = line.Split(' ');
                    //add rect to lsrect
                    if (lineSplit.Length == 5)
                    {
                        int classID = int.Parse(lineSplit[0]);
                        
                        double cx = double.Parse(lineSplit[1]);
                        double cy = double.Parse(lineSplit[2]);
                        double w = double.Parse(lineSplit[3]);
                        double h = double.Parse(lineSplit[4]);
                        double x = cx - w / 2;
                        double y = cy - h / 2;

                        mRects.Add(new Rectangle(
                            (int)(x * m_img.Width / g_scaleX),
                            (int)(y * m_img.Height / g_scaleY),
                            (int)(w * m_img.Width / g_scaleX),
                            (int)(h * m_img.Height / g_scaleY)));

                        
                    }
                    txtCount.Text = lstRect.Items.Count.ToString();
                }
            }
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void lstImg_KeyDown(object sender, KeyEventArgs e)
        {
            int selectedIndex = lstImg.SelectedIndices[0];
            if (e.KeyCode == Keys.Delete)
            {
                if (lstImg.SelectedIndices.Count > 0 && lstRect.SelectedIndex == -1)
                {
                    int index = lstImg.SelectedIndices[0];
                    string imagePath = TGMTutil.CorrectPath(txtFolder.Text) + lstImg.Items[index].Text;
                    string txtPath = imagePath.Replace(Path.GetExtension(imagePath), ".txt");


                    lstImg.Items.RemoveAt(lstImg.SelectedIndices[0]);
                    lstRect.Items.Clear();

                    TGMTfile.MoveFileToRecycleBin(imagePath);
                    TGMTfile.MoveFileToRecycleBin(txtPath);

                    if (index < lstImg.Items.Count)
                    {
                        lstImg.Items[index].Selected = true;
                    }
                    else
                    {
                        lstImg.Items[lstImg.Items.Count - 1].Selected = true;
                    }
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (lstRect.SelectedIndex > -1)
                {
                    e.Handled = true;
                    return;
                }

                if (selectedIndex > 0)
                {
                    lstImg.Items[selectedIndex - 1].Selected = true;
                    lstImg.EnsureVisible(selectedIndex - 1);
                    e.Handled = true;
                }
                return;
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (lstRect.SelectedIndex > -1)
                {
                    e.Handled = true;
                    return;
                }                    

                if (selectedIndex < lstImg.Items.Count - 1)
                {
                    lstImg.Items[selectedIndex + 1].Selected = true;
                    lstImg.EnsureVisible(selectedIndex + 1);
                    e.Handled = true;
                }
                return;
            }
            else
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
                
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void lstRect_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                e.Handled = true;
            }
            else if(e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                if (lstRect.Items.Count > 0 && lstRect.SelectedIndex > -1)
                {
                    int index = lstRect.SelectedIndex;
                    lstRect.Items.RemoveAt(index);
                    mRects.RemoveAt(index);
                    txtCount.Text = lstRect.Items.Count.ToString();
                    if (index > -1 & index < lstRect.Items.Count)
                    {
                        lstRect.SelectedIndex = index;
                    }
                    else if (index == lstRect.Items.Count)
                    {
                        lstRect.SelectedIndex = lstRect.Items.Count - 1;
                    }
                    CompleteEdit();
                    PictureBox1.Refresh();
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                CompleteEdit();
                lstRect.SelectedIndex = -1;
                if (lstImg.SelectedIndices.Count > 0)
                {
                    int nextIndex = lstImg.SelectedIndices[0] + 1;
                    if (nextIndex < lstImg.Items.Count)
                    {
                        lstImg.Items[nextIndex].Selected = true;
                        lstImg.EnsureVisible(nextIndex);
                    }
                    lstImg.Focus();
                    e.Handled = true;
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void lstRect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstRect.SelectedIndex < 0 || lstRect.SelectedIndex > lstRect.Items.Count)
                return;

            if (PictureBox1.Image == null)
                return;

            string[] elements = lstRect.SelectedItem.ToString().Split(' ');
            int classIdx = Convert.ToInt32(elements[0]);
            if(classIdx < cb_classes.Items.Count)
                cb_classes.SelectedIndex = classIdx;

            PictureBox1.Refresh();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void checkTextFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtFolder.Text == "")
                return;
            if (!Directory.Exists(txtFolder.Text))
                return;

            lblMessage.Text = "Checking...";
            timerLoading.Start();
            List<string> fileList = new List<string>(Directory.GetFiles(txtFolder.Text));
            List<string> newList = new List<string>();
            string dir = TGMTutil.CorrectPath(txtFolder.Text);
            foreach (string line in lstImg.Items)
            {
                string[] split = line.Split(' ');
                if (split.Length < 6)
                    continue;
                string filePath = dir + split[0];
                if (!fileList.Contains(filePath))
                    continue;

                bool isValidObject = true;
                int numObject = int.Parse(split[1]);
                for (int i=0; i< numObject; i+=4 )
                {
                    int x = int.Parse(split[i + 2]);
                    int y = int.Parse(split[i+3]);
                    int w = int.Parse(split[i+4]);
                    int h = int.Parse(split[i+5]);

                    if (x < 0 || y < 0)
                    {
                        isValidObject = false;
                    }                        
                    else if (w * h < 4)
                    {
                        isValidObject = false;                        
                    }                        
                    else
                    {
                        Bitmap bmp = TGMTimage.LoadBitmapWithoutLock(filePath);
                        if (x + w > bmp.Width || y + h > bmp.Height)
                        {
                            isValidObject = false;                            
                        }
                        bmp.Dispose();
                    }

                    if (!isValidObject)
                        break;
                }
                if(isValidObject)
                    newList.Add(line);
            }

            if (newList.Count < lstImg.Items.Count)
            {
                //if (MessageBox.Show("Detected " + (lstImg.Items.Count - newList.Count) + " images is not valid. Do you want remove it?", "File location.txt is invalid", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                //{
                //    lstImg.Items.Clear();
                //    lstImg.Items.AddRange(newList.ToArray());
                //    if (lstImg.Items.Count > 0)
                //        lstImg.SelectedIndex = 0;
                //    SaveToFile();
                //}
            }
            else
            {
                MessageBox.Show("Text file is valid");
            }


            timerLoading.Stop();
            progressBar1.Value = progressBar1.Minimum;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        private void removeClassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormClasses fm = new FormClasses();
            fm.ShowDialog();
        }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        #region PICTURE_BOX


        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //draw new rect
            if (g_isMouseDown)
            {
                if (g_colisionState == Colision.None)
                {
                    int w = currentPoint.X - startPoint.X;
                    int h = currentPoint.Y - startPoint.Y;
                    if (w > 0 && h > 0)
                    {
                        e.Graphics.DrawRectangle(Pens.Blue, startPoint.X, startPoint.Y, w, h);
                    }
                }
            }
            else
            {
                e.Graphics.DrawLine(Pens.Black, new Point(0, currentPoint.Y), new Point(PictureBox1.Width - 1, currentPoint.Y));
                e.Graphics.DrawLine(Pens.Black, new Point(currentPoint.X, 0), new Point(currentPoint.X, PictureBox1.Height - 1));
            }
            

            int offset = ANCHOR_WIDTH / 2;
            for (int i = 0; i < lstRect.Items.Count; i++)
            {
                string elements = lstRect.Items[i].ToString();
                int spaceIdx = elements.IndexOf(" ");
                int classID = Convert.ToInt32(elements.Substring(0, spaceIdx));                
                string className = cb_classes.Items[classID].ToString();
                Rectangle r = GetRect(i);
                if (i == lstRect.SelectedIndex)
                {

                    e.Graphics.DrawRectangle(Pens.Red, r);
                    //draw 8 anchor point


                    Rectangle topLeft = new Rectangle(r.Location - new Size(offset, offset), ANCHOR_SIZE);
                    Rectangle top = new Rectangle(r.Location + new Size(r.Width / 2 - offset, -offset), ANCHOR_SIZE);
                    Rectangle topRight = new Rectangle(r.Location + new Size(r.Width - offset, -offset), ANCHOR_SIZE);
                    Rectangle midRight = new Rectangle(r.Location + new Size(r.Width - offset, r.Height / 2 - offset), ANCHOR_SIZE);
                    Rectangle botRight = new Rectangle(r.Location + new Size(r.Width - offset, r.Height - offset), ANCHOR_SIZE);
                    Rectangle bot = new Rectangle(r.Location + new Size(r.Width / 2 - offset, r.Height - offset), ANCHOR_SIZE);
                    Rectangle botLeft = new Rectangle(r.Location + new Size(-offset, r.Height - offset), ANCHOR_SIZE);
                    Rectangle left = new Rectangle(r.Location + new Size(-offset, r.Height / 2 - offset), ANCHOR_SIZE);
                    e.Graphics.FillRectangle(redBrush, topLeft);
                    e.Graphics.FillRectangle(redBrush, top);
                    e.Graphics.FillRectangle(redBrush, topRight);
                    e.Graphics.FillRectangle(redBrush, midRight);
                    e.Graphics.FillRectangle(redBrush, botRight);
                    e.Graphics.FillRectangle(redBrush, bot);
                    e.Graphics.FillRectangle(redBrush, botLeft);
                    e.Graphics.FillRectangle(redBrush, left);

                    e.Graphics.DrawString(className, myFont, redBrush, r.X, r.Y - 20);
                }
                else
                {
                    e.Graphics.DrawRectangle(Pens.Blue, r);
                    e.Graphics.DrawString(className, myFont, blueBrush, r.X, r.Y - 20);
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void PictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.Cursor == Cursors.SizeAll && newRectIdx < lstRect.Items.Count)
            {
                lstRect.SelectedIndex = newRectIdx;
                lstRect.Focus();
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (PictureBox1.Image == null)
                return;

            timer1.Start();
            startPoint = e.Location;
            g_isMouseDown = true;
            if (lstRect.SelectedIndex > -1)
            {
                Rectangle rect = GetCurrentRect();
                mDistanceCurrentRectToMouse = new Size(e.Location.X - rect.X, e.Location.Y - rect.Y);
                mCurrentRectBottomRight = new Point(rect.X + rect.Width, rect.Y + rect.Height);
            }

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (PictureBox1.Image == null)
                return;

            timer1.Stop();

            currentPoint = Clamp(e.Location, new Point(0, 0), new Point(PictureBox1.Width, PictureBox1.Height));
            g_isMouseDown = false;
            //draw new rect
            if (g_colisionState == Colision.None)
            {
                int x = startPoint.X;
                int y = startPoint.Y;
                int w = Math.Abs(currentPoint.X - startPoint.X);
                int h = Math.Abs(currentPoint.Y - startPoint.Y);
                int cx = x + w / 2;
                int cy = y + h / 2;

                if (w <= 1 || h <= 1)
                {
                    return;
                }


                if ((chkMinSize.Checked && Convert.ToInt32(w * g_scaleX) > numMinWidth.Value && Convert.ToInt32(h * g_scaleY) > numMinHeight.Value)
                    || !chkMinSize.Checked)
                {
                    mRects.Add(new Rectangle(x, y, w, h));
                    lstRect.Items.Add(
                        cb_classes.SelectedIndex + " " +
                        cx * g_scaleX / m_img.Width + " " +
                        cy * g_scaleY / m_img.Height + " " +
                        w * g_scaleX / m_img.Width + " " + 
                        h * g_scaleY / m_img.Height);
                    lstRect.SelectedIndex = lstRect.Items.Count - 1;
                }
                CompleteEdit();
            }            
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (PictureBox1.Image == null)
                return;

            currentPoint = Clamp(e.Location, new Point(0, 0), new Point(PictureBox1.Width, PictureBox1.Height));
            int dx = currentPoint.X - startPoint.X;
            int dy = currentPoint.Y - startPoint.Y;


            if (g_isMouseDown)
            {
                
                if (g_colisionState == Colision.None)
                {

                }
                else if (g_colisionState == Colision.All)
                {
                    MoveRect(currentPoint);
                }
                else
                {
                    ResizeRect(currentPoint);
                }

            }
            else //not mouse down
            {
                ChangeCursor(e.Location);
                PictureBox1.Refresh();
            }
        }

#endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

#region TIMER

        private void timer1_Tick(object sender, EventArgs e)
        {
            PictureBox1.Refresh();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void timerLoading_Tick(object sender, EventArgs e)
        {
            progressBar1.Value++;
            if (progressBar1.Value >= progressBar1.Maximum)
                progressBar1.Value = progressBar1.Minimum;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void timerClear_Tick(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            timerClear.Stop();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void timerAutoSave_Tick(object sender, EventArgs e)
        {
            SaveToFile();
        }

#endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

#region MENU


        private void CropImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog folderBrowser = new OpenFileDialog();
            folderBrowser.ValidateNames = false;
            folderBrowser.CheckFileExists = false;
            folderBrowser.CheckPathExists = true;
            // Always default to Folder Selection.
            folderBrowser.FileName = "Select folder";
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                m_saveDir = Path.GetDirectoryName(folderBrowser.FileName);
            }
            if (m_saveDir == "")
                return;
            m_saveDir = TGMTutil.CorrectPath(m_saveDir);

            bgCrop.RunWorkerAsync();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void ExpandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmExpand frm = new frmExpand();
            frm.FormClosed += frmExpand_FormClosed;
            frm.Show();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        void frmExpand_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (lstImg.Items.Count == 0)
                return;
            if (Program.expandLeft == 0 && Program.expandDown == 0 && Program.expandRight == 0 && Program.expandTop == 0)
                return;

            int count = 0;
            for (int i = 0; i < lstImg.Items.Count; i++)
            {
                string[] lineSplit = lstImg.Items[i].ToString().Split(' ');
                if (lineSplit.Length == 1)
                    continue;

                for (int j = 0; j < int.Parse(lineSplit[1]); j++)
                {
                    int x = int.Parse(lineSplit[j * 4 + 2]) - Program.expandLeft;
                    int y = int.Parse(lineSplit[j * 4 + 3]) - Program.expandTop;
                    int w = int.Parse(lineSplit[j * 4 + 4]) + Program.expandLeft + Program.expandRight;
                    int h = int.Parse(lineSplit[j * 4 + 5]) + Program.expandTop + Program.expandDown;

                    lineSplit[j * 4 + 2] = x.ToString();
                    lineSplit[j * 4 + 3] = y.ToString();
                    lineSplit[j * 4 + 4] = w.ToString();
                    lineSplit[j * 4 + 5] = h.ToString();

                    count++;
                }

                //lstImg.Items[i] = string.Join(" ", lineSplit);
            }
            int index = lstImg.SelectedIndices[0];
            //lstImg.SelectedIndex = 0;
            //lstImg.SelectedIndex = index;

            lblMessage.Text = "Expanded " + count + " rectangles";

        }

#endregion

        private void btn_last_Click(object sender, EventArgs e)
        {
            GotoLastNotAnnotated();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        void GotoLastNotAnnotated()
        {
            if (lstImg.Items.Count == 0)
                return;

            int startIndex = lstImg.Items.Count - 1;
            if (lstImg.SelectedIndices.Count > 0)
                startIndex = lstImg.SelectedIndices[0] - 1;

            for (int i = startIndex; i > 0; i--)
            {
                if (lstImg.Items[i].ForeColor == Color.Red)
                {
                    lstImg.Items[i].Selected = true;
                    lstImg.EnsureVisible(i);
                    return;
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btn_prev_Click(object sender, EventArgs e)
        {
            if (lstImg.SelectedIndices.Count == 0)
                return;

            int index = lstImg.SelectedIndices[0] + 1;
            lstImg.Items[index].Selected = true;
            lstImg.EnsureVisible(index);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btn_next_Click(object sender, EventArgs e)
        {
            if (lstImg.SelectedIndices.Count == 0)
                return;

            int index = lstImg.SelectedIndices[0] + 1;
            lstImg.Items[index].Selected = true;
            lstImg.EnsureVisible(index);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btn_end_Click(object sender, EventArgs e)
        {
            GotoNextNotAnnotated();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        void GotoNextNotAnnotated()
        {            
            int startIndex = 0;
            if (lstImg.SelectedIndices.Count > 0)
                startIndex = lstImg.SelectedIndices[0] + 1;
            for (int i = startIndex; i < lstImg.Items.Count; i++)
            {
                if (lstImg.Items[i].ForeColor == Color.Red)
                {
                    lstImg.Items[i].Selected = true;
                    lstImg.EnsureVisible(i);
                    return;
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void cb_classes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lstRect.SelectedIndex > -1)
            {                
                string elements = lstRect.Items[lstRect.SelectedIndex].ToString();
                int spaceIdx = elements.IndexOf(" ");

                string currentClass = elements.Substring(0, spaceIdx);
                string newClass = cb_classes.SelectedIndex.ToString();
                if(currentClass != newClass)
                {
                    elements = newClass + elements.Substring(spaceIdx);
                    lstRect.Items[lstRect.SelectedIndex] = elements;
                    CompleteEdit();
                }
                                
                lstRect.Focus();
            }            
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btn_addClass_Click(object sender, EventArgs e)
        {
            string newClass = InputBox.Show("Add new class");
            if (newClass == "")
            {
                cb_classes.Items.Add(newClass);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void removeImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(lstImg.Items.Count == 0 || lstImg.SelectedIndices.Count == 0)
                return;
            lstImg.Items[lstImg.SelectedIndices[0]].Remove();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void deleteImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteFile();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        void DeleteFile()
        {
            if (lstImg.Items.Count == 0 || lstImg.SelectedIndices.Count == 0)
                return;

            int index = lstImg.SelectedIndices[0];

            string filePath = TGMTutil.CorrectPath(txtFolder.Text) + lstImg.Items[index].Text;
            string txtPath = filePath.Replace(Path.GetExtension(filePath), ".txt");
            TGMTfile.MoveFileToRecycleBin(filePath);
            TGMTfile.MoveFileToRecycleBin(txtPath);

            lstImg.Items[index].Remove();
            lstImg.Items[index].Selected = true;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void bgCrop_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                for (int i = 0; i < m_classes.Length; i++)
                {
                    string classDir = m_saveDir + m_classes[i];
                    if (!Directory.Exists(classDir))
                        Directory.CreateDirectory(classDir);
                }

                string folderPath = TGMTutil.CorrectPath(txtFolder.Text);


                int count = 0;
                int totalImage = lstImg.Items.Count;
                for (int i = 0; i < totalImage; i++)
                {
                    string fileName = lstImg.Items[i].Text;
                    string filePath = folderPath + fileName;
                    string txtPath = filePath.Replace(Path.GetExtension(filePath), ".txt");

                    if (!File.Exists(txtPath))
                    {
                        continue;
                    }
                    string[] lines = File.ReadAllLines(txtPath);


                    Bitmap bmp = TGMTimage.LoadBitmapWithoutLock(filePath);

                    for (int j = 0; j < lines.Length; j++)
                    {
                        string line = lines[j];
                        string[] elements = line.Split(' ');
                        if (elements.Length != 5)
                            continue;

                        int classID = Convert.ToInt32(elements[0]);

                        if (classID >= m_classes.Length)
                        {
                            if (MessageBox.Show("File " + txtPath + " wrong class ID", "Open text file?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                Process.Start(txtPath);
                            }
                            return;
                        }
                        string className = m_classes[classID];



                        double cx = Convert.ToDouble(elements[1]);
                        double cy = Convert.ToDouble(elements[2]);
                        double w = Convert.ToDouble(elements[3]);
                        double h = Convert.ToDouble(elements[4]);
                        double x = cx - w / 2;
                        double y = cy - h / 2;

                        int ix = Convert.ToInt32(x * bmp.Width);
                        int iy = Convert.ToInt32(y * bmp.Height);
                        int iw = Convert.ToInt32(w * bmp.Width);
                        int ih = Convert.ToInt32(h * bmp.Height);

                        
                        if (iw > bmp.Width || ih > bmp.Height || ix + iw > bmp.Width || iy + ih > bmp.Height)
                        {
                            if (MessageBox.Show("File " + txtPath + " wrong size", "Open text file?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                Process.Start(txtPath);
                            }
                            continue;
                        }

                        Rectangle cropRect = new Rectangle(ix, iy, iw, ih);
                        Bitmap cropBmp = TGMTimage.CropBitmap(bmp, cropRect);
                        string savePath = String.Format("{0}{1}\\{2}_{3}.jpg", m_saveDir, className, Path.GetFileNameWithoutExtension(fileName), j);
                        cropBmp.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        cropBmp.Dispose();
                        cropBmp = null;
                        count++;
                    }

                    bmp.Dispose();
                    bmp = null;

                    int percentComplete =  (int)((float)i / (float)totalImage * 100);
                    bgCrop.ReportProgress(percentComplete);
                    
                }
                e.Result = "Crop " + count + " objects on " + totalImage + " total image";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void bgCrop_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            PrintMessage(e.ProgressPercentage + "%");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void bgCrop_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Value = progressBar1.Minimum;
            PrintMessage("");
            MessageBox.Show(e.Result.ToString(), "Crop success");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        void SearchFile()
        {
            
            
            if (txtSearch.Text == "")
                return;

            if (txtSearch.Text != m_lastSearch)
                m_lastSearchIndex = 0;
            m_lastSearch = txtSearch.Text;
            if (m_lastSearchIndex >= lstImg.Items.Count)
                m_lastSearchIndex = 0;

            bool found = false;
            for(int i= m_lastSearchIndex; i<lstImg.Items.Count; i++)
            {
                if(lstImg.Items[i].Text.Contains(txtSearch.Text))
                {
                    lstImg.Items[i].Selected = true;
                    lstImg.EnsureVisible(i);
                    found = true;
                    m_lastSearchIndex = i + 1;
                    break;
                }
            }

            if(!found)
            {
                m_lastSearchIndex = 0;
                MessageBox.Show("Not found");
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void copyPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem focusedItem = lstImg.FocusedItem;
            Clipboard.SetText(TGMTutil.CorrectPath(txtFolder.Text) + focusedItem.Text);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void openImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem focusedItem = lstImg.FocusedItem;
            Process.Start(TGMTutil.CorrectPath(txtFolder.Text) + focusedItem.Text);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btn_search_Click(object sender, EventArgs e)
        {
            SearchFile();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void moveFileNotAnnotatedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog folderBrowser = new OpenFileDialog();
            folderBrowser.ValidateNames = false;
            folderBrowser.CheckFileExists = false;
            folderBrowser.CheckPathExists = true;
            // Always default to Folder Selection.
            folderBrowser.Title = "Select target folder";
            if (folderBrowser.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string saveDir = Path.GetDirectoryName(folderBrowser.FileName);
            saveDir = TGMTutil.CorrectPath(saveDir);

            string currentDir = TGMTutil.CorrectPath(txtFolder.Text);

            int count = 0;
            foreach (ListViewItem f in lstImg.Items)
            {

                string imgPath = currentDir + f.Text;
                string txtPath = imgPath.Replace(Path.GetExtension(imgPath), ".txt");
                if(!File.Exists(txtPath))
                {                   
                    string newImgPath = saveDir + f.Text;
                    TGMTfile.MoveFileAsync(imgPath, newImgPath);

                    count++;
                }
                else
                {
                    string[] lines = File.ReadAllLines(txtPath);
                    if(lines.Length == 0)
                    {
                        string newImgPath = saveDir + f.Text;
                        TGMTfile.MoveFileAsync(imgPath, newImgPath);

                        string newTxtPath = newImgPath.Replace(Path.GetExtension(newImgPath), ".txt");
                        TGMTfile.MoveFileAsync(txtPath, newTxtPath);

                        count += 2;
                    }
                }
            }
            MessageBox.Show("Moved " + count + " files");

        }

        
    }
}
