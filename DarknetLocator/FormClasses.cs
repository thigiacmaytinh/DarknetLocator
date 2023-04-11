using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TGMTcs;

namespace DarknetLocator
{
    public partial class FormClasses : Form
    {
        List<string> m_classesToKeep;
        List<string> m_classesToRemove;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        public FormClasses()
        {
            InitializeComponent();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void FormClasses_Load(object sender, EventArgs e)
        {

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

        private void txtFolder_TextChanged(object sender, EventArgs e)
        {
            string path = txtFolder.Text;
            if (path.Contains(" "))
            {
                MessageBox.Show("Folder path do not contain spaces");
                return;
            }

            if (!Directory.Exists(path))
            {
                lstKeep.Items.Clear();
                lstRemove.Items.Clear();
            }

            this.Enabled = false;
            timerLoading.Start();
            lblMessage.Text = "Loading file...";
            TGMTregistry.GetInstance().SaveValue("folderPath", path);

            LoadClasses();
            this.Enabled = true;
        }

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

        void LoadClasses()
        {
            if (!Directory.Exists(txtFolder.Text))
                return;

            lstKeep.Items.Clear();
            lstRemove.Items.Clear();
            string classPath = TGMTutil.CorrectPath(txtFolder.Text) + "classes.txt";
            if (File.Exists(classPath))
            {
                string[] classes = File.ReadAllLines(classPath);
                m_classesToKeep = new List<string>(classes);
                for (int i = 0; i < classes.Length; i++)
                {
                    lstKeep.Items.Add(classes[i]);
                }
                lstKeep.SelectedIndex = 0;
                PrintSuccess("Loaded " + classes.Length + " classes");
                lbl_keep.Text = "Keep (" + classes.Length + ")";
            }
            else
            {
                PrintError("File classes.txt not found");
            }            
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btn_moveRightAll_Click(object sender, EventArgs e)
        {
            m_classesToRemove.AddRange(lstKeep.Items.OfType<string>().ToList());
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btn_moveRight_Click(object sender, EventArgs e)
        {

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btn_moveLeft_Click(object sender, EventArgs e)
        {

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btn_moveLeftAll_Click(object sender, EventArgs e)
        {
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btn_remove_Click(object sender, EventArgs e)
        {
            bgLoadFile.RunWorkerAsync();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void bgLoadFile_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!Directory.Exists(txtFolder.Text))
                return;

            var allowedExtensions = new[] { ".txt"};
            var fileList = Directory.GetFiles(txtFolder.Text)
                .Where(file => allowedExtensions.Any(file.ToLower().EndsWith)).ToList();

            int[] keep = { 0, 3 };

            int countSuccess = 0;
            foreach (string filePath in fileList)
            {
                string[] lines = File.ReadAllLines(filePath);
                if (lines.Length == 0)
                    continue;

                List<string> linesFiltered = new List<string>();
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    string[] lineSplit = line.Split(' ');
                    if (lineSplit.Length == 5)
                    {
                        int classID = int.Parse(lineSplit[0]);                       
                        if(keep.Contains(classID))
                        {
                            linesFiltered.Add(line);
                        }
                    }
                }

                if(lines.Length != linesFiltered.Count)
                {
                    File.WriteAllLines(filePath, linesFiltered.ToArray());
                    countSuccess++;
                }                
            }
        }
    }
}
