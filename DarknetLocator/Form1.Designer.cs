namespace DarknetLocator
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.chkMinSize = new System.Windows.Forms.CheckBox();
            this.btn_end = new System.Windows.Forms.Button();
            this.btn_next = new System.Windows.Forms.Button();
            this.btn_prev = new System.Windows.Forms.Button();
            this.btn_last = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btn_search = new System.Windows.Forms.Button();
            this.lstRect = new System.Windows.Forms.ListBox();
            this.lstImg = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ctx_images = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Timer_clear = new System.Windows.Forms.Timer(this.components);
            this.ExpandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CropImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkTextFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveFileNotAnnotatedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeClassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
            this.RectangleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblLstImg = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_addClass = new System.Windows.Forms.Button();
            this.cb_classes = new System.Windows.Forms.ComboBox();
            this.numMinHeight = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numMinWidth = new System.Windows.Forms.NumericUpDown();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.Label10 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.progressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.lblMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.ErrorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.bgLoadFile = new System.ComponentModel.BackgroundWorker();
            this.timerLoading = new System.Windows.Forms.Timer(this.components);
            this.timerClear = new System.Windows.Forms.Timer(this.components);
            this.timerAutoSave = new System.Windows.Forms.Timer(this.components);
            this.bgCrop = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.ctx_images.SuspendLayout();
            this.MenuStrip1.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMinHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinWidth)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureBox1
            // 
            this.PictureBox1.Location = new System.Drawing.Point(0, 0);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(800, 600);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox1.TabIndex = 4;
            this.PictureBox1.TabStop = false;
            this.PictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureBox1_Paint);
            this.PictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseClick);
            this.PictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseDown);
            this.PictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseMove);
            this.PictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseUp);
            // 
            // ToolTip1
            // 
            this.ToolTip1.AutomaticDelay = 1000;
            this.ToolTip1.ForeColor = System.Drawing.Color.DarkOrange;
            this.ToolTip1.IsBalloon = true;
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectFolder.Location = new System.Drawing.Point(308, 16);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(34, 26);
            this.btnSelectFolder.TabIndex = 29;
            this.btnSelectFolder.TabStop = false;
            this.btnSelectFolder.Text = "...";
            this.ToolTip1.SetToolTip(this.btnSelectFolder, "Chọn folder chứa ảnh");
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // txtFolder
            // 
            this.txtFolder.Location = new System.Drawing.Point(3, 19);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(299, 20);
            this.txtFolder.TabIndex = 28;
            this.txtFolder.TabStop = false;
            this.ToolTip1.SetToolTip(this.txtFolder, "Folder chứa ảnh");
            this.txtFolder.TextChanged += new System.EventHandler(this.txtFolder_TextChanged);
            // 
            // chkMinSize
            // 
            this.chkMinSize.AutoSize = true;
            this.chkMinSize.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMinSize.Location = new System.Drawing.Point(3, 15);
            this.chkMinSize.Name = "chkMinSize";
            this.chkMinSize.Size = new System.Drawing.Size(90, 23);
            this.chkMinSize.TabIndex = 50;
            this.chkMinSize.TabStop = false;
            this.chkMinSize.Text = "Min width";
            this.ToolTip1.SetToolTip(this.chkMinSize, "If checked will draw rectangle with aspect you type");
            this.chkMinSize.UseVisualStyleBackColor = true;
            // 
            // btn_end
            // 
            this.btn_end.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_end.Location = new System.Drawing.Point(224, 45);
            this.btn_end.Name = "btn_end";
            this.btn_end.Size = new System.Drawing.Size(43, 30);
            this.btn_end.TabIndex = 33;
            this.btn_end.TabStop = false;
            this.btn_end.Text = ">>";
            this.ToolTip1.SetToolTip(this.btn_end, "Go to image not annotation");
            this.btn_end.UseVisualStyleBackColor = true;
            this.btn_end.Click += new System.EventHandler(this.btn_end_Click);
            // 
            // btn_next
            // 
            this.btn_next.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_next.Location = new System.Drawing.Point(179, 45);
            this.btn_next.Name = "btn_next";
            this.btn_next.Size = new System.Drawing.Size(43, 30);
            this.btn_next.TabIndex = 32;
            this.btn_next.TabStop = false;
            this.btn_next.Text = ">";
            this.ToolTip1.SetToolTip(this.btn_next, "Next image");
            this.btn_next.UseVisualStyleBackColor = true;
            this.btn_next.Click += new System.EventHandler(this.btn_next_Click);
            // 
            // btn_prev
            // 
            this.btn_prev.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_prev.Location = new System.Drawing.Point(133, 45);
            this.btn_prev.Name = "btn_prev";
            this.btn_prev.Size = new System.Drawing.Size(43, 30);
            this.btn_prev.TabIndex = 31;
            this.btn_prev.TabStop = false;
            this.btn_prev.Text = "<";
            this.ToolTip1.SetToolTip(this.btn_prev, "Previous image");
            this.btn_prev.UseVisualStyleBackColor = true;
            this.btn_prev.Click += new System.EventHandler(this.btn_prev_Click);
            // 
            // btn_last
            // 
            this.btn_last.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_last.Location = new System.Drawing.Point(87, 45);
            this.btn_last.Name = "btn_last";
            this.btn_last.Size = new System.Drawing.Size(43, 30);
            this.btn_last.TabIndex = 30;
            this.btn_last.TabStop = false;
            this.btn_last.Text = "<<";
            this.ToolTip1.SetToolTip(this.btn_last, "Go to image not annotation");
            this.btn_last.UseVisualStyleBackColor = true;
            this.btn_last.Click += new System.EventHandler(this.btn_last_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(34, 83);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(225, 20);
            this.txtSearch.TabIndex = 35;
            this.txtSearch.TabStop = false;
            this.ToolTip1.SetToolTip(this.txtSearch, "Folder chứa ảnh");
            // 
            // btn_search
            // 
            this.btn_search.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_search.Location = new System.Drawing.Point(265, 79);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(66, 26);
            this.btn_search.TabIndex = 36;
            this.btn_search.TabStop = false;
            this.btn_search.Text = "Search";
            this.ToolTip1.SetToolTip(this.btn_search, "Search");
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // lstRect
            // 
            this.lstRect.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lstRect.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstRect.FormattingEnabled = true;
            this.lstRect.HorizontalScrollbar = true;
            this.lstRect.ItemHeight = 16;
            this.lstRect.Location = new System.Drawing.Point(3, 411);
            this.lstRect.Name = "lstRect";
            this.lstRect.ScrollAlwaysVisible = true;
            this.lstRect.Size = new System.Drawing.Size(346, 164);
            this.lstRect.TabIndex = 6;
            this.lstRect.TabStop = false;
            this.lstRect.SelectedIndexChanged += new System.EventHandler(this.lstRect_SelectedIndexChanged);
            this.lstRect.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstRect_KeyDown);
            // 
            // lstImg
            // 
            this.lstImg.AllowDrop = true;
            this.lstImg.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lstImg.ContextMenuStrip = this.ctx_images;
            this.lstImg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstImg.FullRowSelect = true;
            this.lstImg.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstImg.HideSelection = false;
            this.lstImg.Location = new System.Drawing.Point(3, 149);
            this.lstImg.MultiSelect = false;
            this.lstImg.Name = "lstImg";
            this.lstImg.Size = new System.Drawing.Size(346, 185);
            this.lstImg.TabIndex = 2;
            this.lstImg.TabStop = false;
            this.lstImg.UseCompatibleStateImageBehavior = false;
            this.lstImg.View = System.Windows.Forms.View.Details;
            this.lstImg.SelectedIndexChanged += new System.EventHandler(this.lstImg_SelectedIndexChanged);
            this.lstImg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstImg_KeyDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 337;
            // 
            // ctx_images
            // 
            this.ctx_images.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeImageToolStripMenuItem,
            this.deleteImageToolStripMenuItem,
            this.copyPathToolStripMenuItem,
            this.openImageToolStripMenuItem});
            this.ctx_images.Name = "ctx_images";
            this.ctx_images.Size = new System.Drawing.Size(154, 92);
            // 
            // removeImageToolStripMenuItem
            // 
            this.removeImageToolStripMenuItem.Name = "removeImageToolStripMenuItem";
            this.removeImageToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.removeImageToolStripMenuItem.Text = "Remove image";
            this.removeImageToolStripMenuItem.Click += new System.EventHandler(this.removeImageToolStripMenuItem_Click);
            // 
            // deleteImageToolStripMenuItem
            // 
            this.deleteImageToolStripMenuItem.Name = "deleteImageToolStripMenuItem";
            this.deleteImageToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.deleteImageToolStripMenuItem.Text = "Delete image";
            this.deleteImageToolStripMenuItem.Click += new System.EventHandler(this.deleteImageToolStripMenuItem_Click);
            // 
            // copyPathToolStripMenuItem
            // 
            this.copyPathToolStripMenuItem.Name = "copyPathToolStripMenuItem";
            this.copyPathToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.copyPathToolStripMenuItem.Text = "Copy path";
            this.copyPathToolStripMenuItem.Click += new System.EventHandler(this.copyPathToolStripMenuItem_Click);
            // 
            // openImageToolStripMenuItem
            // 
            this.openImageToolStripMenuItem.Name = "openImageToolStripMenuItem";
            this.openImageToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.openImageToolStripMenuItem.Text = "Open image";
            this.openImageToolStripMenuItem.Click += new System.EventHandler(this.openImageToolStripMenuItem_Click);
            // 
            // Timer_clear
            // 
            this.Timer_clear.Enabled = true;
            this.Timer_clear.Interval = 4000;
            // 
            // ExpandToolStripMenuItem
            // 
            this.ExpandToolStripMenuItem.Name = "ExpandToolStripMenuItem";
            this.ExpandToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.ExpandToolStripMenuItem.Text = "Expand all rectangles";
            this.ExpandToolStripMenuItem.Click += new System.EventHandler(this.ExpandToolStripMenuItem_Click);
            // 
            // CropImageToolStripMenuItem
            // 
            this.CropImageToolStripMenuItem.Name = "CropImageToolStripMenuItem";
            this.CropImageToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.CropImageToolStripMenuItem.Text = "Crop and save all images";
            this.CropImageToolStripMenuItem.Click += new System.EventHandler(this.CropImageToolStripMenuItem_Click);
            // 
            // ToolStripMenuItem
            // 
            this.ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CropImageToolStripMenuItem,
            this.checkTextFileToolStripMenuItem,
            this.moveFileNotAnnotatedToolStripMenuItem,
            this.removeClassToolStripMenuItem});
            this.ToolStripMenuItem.Name = "ToolStripMenuItem";
            this.ToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.ToolStripMenuItem.Text = "Tool";
            // 
            // checkTextFileToolStripMenuItem
            // 
            this.checkTextFileToolStripMenuItem.Name = "checkTextFileToolStripMenuItem";
            this.checkTextFileToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.checkTextFileToolStripMenuItem.Text = "Check text file";
            this.checkTextFileToolStripMenuItem.Click += new System.EventHandler(this.checkTextFileToolStripMenuItem_Click);
            // 
            // moveFileNotAnnotatedToolStripMenuItem
            // 
            this.moveFileNotAnnotatedToolStripMenuItem.Name = "moveFileNotAnnotatedToolStripMenuItem";
            this.moveFileNotAnnotatedToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.moveFileNotAnnotatedToolStripMenuItem.Text = "Move image has not object";
            this.moveFileNotAnnotatedToolStripMenuItem.Click += new System.EventHandler(this.moveFileNotAnnotatedToolStripMenuItem_Click);
            // 
            // removeClassToolStripMenuItem
            // 
            this.removeClassToolStripMenuItem.Name = "removeClassToolStripMenuItem";
            this.removeClassToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.removeClassToolStripMenuItem.Text = "Remove class";
            this.removeClassToolStripMenuItem.Click += new System.EventHandler(this.removeClassToolStripMenuItem_Click);
            // 
            // MenuStrip1
            // 
            this.MenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem,
            this.RectangleToolStripMenuItem});
            this.MenuStrip1.Location = new System.Drawing.Point(3, 16);
            this.MenuStrip1.Name = "MenuStrip1";
            this.MenuStrip1.Size = new System.Drawing.Size(346, 24);
            this.MenuStrip1.TabIndex = 36;
            this.MenuStrip1.Text = "MenuStrip1";
            // 
            // RectangleToolStripMenuItem
            // 
            this.RectangleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExpandToolStripMenuItem});
            this.RectangleToolStripMenuItem.Name = "RectangleToolStripMenuItem";
            this.RectangleToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.RectangleToolStripMenuItem.Text = "Rectangle";
            // 
            // GroupBox1
            // 
            this.GroupBox1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.GroupBox1.Controls.Add(this.lstImg);
            this.GroupBox1.Controls.Add(this.groupBox3);
            this.GroupBox1.Controls.Add(this.groupBox2);
            this.GroupBox1.Controls.Add(this.lstRect);
            this.GroupBox1.Controls.Add(this.statusStrip1);
            this.GroupBox1.Controls.Add(this.MenuStrip1);
            this.GroupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.GroupBox1.Location = new System.Drawing.Point(800, 0);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(352, 600);
            this.GroupBox1.TabIndex = 5;
            this.GroupBox1.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn_search);
            this.groupBox3.Controls.Add(this.txtSearch);
            this.groupBox3.Controls.Add(this.btn_end);
            this.groupBox3.Controls.Add(this.btn_next);
            this.groupBox3.Controls.Add(this.btn_prev);
            this.groupBox3.Controls.Add(this.btn_last);
            this.groupBox3.Controls.Add(this.btnSelectFolder);
            this.groupBox3.Controls.Add(this.txtFolder);
            this.groupBox3.Controls.Add(this.lblLstImg);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(3, 40);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(346, 109);
            this.groupBox3.TabIndex = 39;
            this.groupBox3.TabStop = false;
            // 
            // lblLstImg
            // 
            this.lblLstImg.AutoSize = true;
            this.lblLstImg.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLstImg.ForeColor = System.Drawing.Color.Red;
            this.lblLstImg.Location = new System.Drawing.Point(3, 54);
            this.lblLstImg.Name = "lblLstImg";
            this.lblLstImg.Size = new System.Drawing.Size(36, 17);
            this.lblLstImg.TabIndex = 34;
            this.lblLstImg.Text = "0 / 0";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_addClass);
            this.groupBox2.Controls.Add(this.cb_classes);
            this.groupBox2.Controls.Add(this.chkMinSize);
            this.groupBox2.Controls.Add(this.numMinHeight);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.numMinWidth);
            this.groupBox2.Controls.Add(this.txtCount);
            this.groupBox2.Controls.Add(this.Label10);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(3, 334);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(346, 77);
            this.groupBox2.TabIndex = 38;
            this.groupBox2.TabStop = false;
            // 
            // btn_addClass
            // 
            this.btn_addClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_addClass.Location = new System.Drawing.Point(137, 41);
            this.btn_addClass.Name = "btn_addClass";
            this.btn_addClass.Size = new System.Drawing.Size(32, 26);
            this.btn_addClass.TabIndex = 52;
            this.btn_addClass.TabStop = false;
            this.btn_addClass.Text = "+";
            this.btn_addClass.UseVisualStyleBackColor = true;
            this.btn_addClass.Click += new System.EventHandler(this.btn_addClass_Click);
            // 
            // cb_classes
            // 
            this.cb_classes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_classes.FormattingEnabled = true;
            this.cb_classes.Location = new System.Drawing.Point(3, 43);
            this.cb_classes.Name = "cb_classes";
            this.cb_classes.Size = new System.Drawing.Size(131, 23);
            this.cb_classes.TabIndex = 51;
            this.cb_classes.TabStop = false;
            this.cb_classes.SelectedIndexChanged += new System.EventHandler(this.cb_classes_SelectedIndexChanged);
            // 
            // numMinHeight
            // 
            this.numMinHeight.Location = new System.Drawing.Point(265, 15);
            this.numMinHeight.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numMinHeight.Name = "numMinHeight";
            this.numMinHeight.Size = new System.Drawing.Size(61, 21);
            this.numMinHeight.TabIndex = 49;
            this.numMinHeight.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(192, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 15);
            this.label2.TabIndex = 48;
            this.label2.Text = "Min height";
            // 
            // numMinWidth
            // 
            this.numMinWidth.Location = new System.Drawing.Point(92, 15);
            this.numMinWidth.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numMinWidth.Name = "numMinWidth";
            this.numMinWidth.Size = new System.Drawing.Size(57, 21);
            this.numMinWidth.TabIndex = 46;
            this.numMinWidth.TabStop = false;
            // 
            // txtCount
            // 
            this.txtCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCount.ForeColor = System.Drawing.Color.Red;
            this.txtCount.Location = new System.Drawing.Point(297, 42);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(40, 23);
            this.txtCount.TabIndex = 41;
            this.txtCount.TabStop = false;
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label10.Location = new System.Drawing.Point(251, 45);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(42, 15);
            this.Label10.TabIndex = 40;
            this.Label10.Text = "Count:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar1,
            this.lblMessage});
            this.statusStrip1.Location = new System.Drawing.Point(3, 575);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(346, 22);
            this.statusStrip1.TabIndex = 37;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // progressBar1
            // 
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // lblMessage
            // 
            this.lblMessage.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 17);
            // 
            // ErrorProvider1
            // 
            this.ErrorProvider1.ContainerControl = this;
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // bgLoadFile
            // 
            this.bgLoadFile.WorkerReportsProgress = true;
            this.bgLoadFile.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgLoadFile_DoWork);
            this.bgLoadFile.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgLoadFile_ProgressChanged);
            this.bgLoadFile.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgLoadFile_RunWorkerCompleted);
            // 
            // timerLoading
            // 
            this.timerLoading.Interval = 50;
            this.timerLoading.Tick += new System.EventHandler(this.timerLoading_Tick);
            // 
            // timerClear
            // 
            this.timerClear.Interval = 2000;
            this.timerClear.Tick += new System.EventHandler(this.timerClear_Tick);
            // 
            // timerAutoSave
            // 
            this.timerAutoSave.Enabled = true;
            this.timerAutoSave.Interval = 300000;
            this.timerAutoSave.Tick += new System.EventHandler(this.timerAutoSave_Tick);
            // 
            // bgCrop
            // 
            this.bgCrop.WorkerReportsProgress = true;
            this.bgCrop.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgCrop_DoWork);
            this.bgCrop.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgCrop_ProgressChanged);
            this.bgCrop.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgCrop_RunWorkerCompleted);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(1152, 600);
            this.Controls.Add(this.PictureBox1);
            this.Controls.Add(this.GroupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Darknet locator";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.ctx_images.ResumeLayout(false);
            this.MenuStrip1.ResumeLayout(false);
            this.MenuStrip1.PerformLayout();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMinHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinWidth)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.PictureBox PictureBox1;
        internal System.Windows.Forms.ToolTip ToolTip1;
        internal System.Windows.Forms.ListBox lstRect;
        internal System.Windows.Forms.ListView lstImg;
        internal System.Windows.Forms.Timer Timer_clear;
        internal System.Windows.Forms.ToolStripMenuItem ExpandToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem CropImageToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem;
        internal System.Windows.Forms.MenuStrip MenuStrip1;
        internal System.Windows.Forms.ToolStripMenuItem RectangleToolStripMenuItem;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.ErrorProvider ErrorProvider1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblMessage;
        private System.ComponentModel.BackgroundWorker bgLoadFile;
        private System.Windows.Forms.ToolStripProgressBar progressBar1;
        private System.Windows.Forms.Timer timerLoading;
        private System.Windows.Forms.ToolStripMenuItem checkTextFileToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox3;
        internal System.Windows.Forms.Button btnSelectFolder;
        internal System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.GroupBox groupBox2;
        internal System.Windows.Forms.TextBox txtCount;
        internal System.Windows.Forms.Label Label10;
        internal System.Windows.Forms.Label lblLstImg;
        private System.Windows.Forms.Timer timerClear;
        private System.Windows.Forms.NumericUpDown numMinHeight;
        internal System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numMinWidth;
        internal System.Windows.Forms.CheckBox chkMinSize;
        private System.Windows.Forms.Timer timerAutoSave;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button btn_last;
        private System.Windows.Forms.Button btn_end;
        private System.Windows.Forms.Button btn_next;
        private System.Windows.Forms.Button btn_prev;
        private System.Windows.Forms.ComboBox cb_classes;
        private System.Windows.Forms.Button btn_addClass;
        private System.Windows.Forms.ContextMenuStrip ctx_images;
        private System.Windows.Forms.ToolStripMenuItem removeImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteImageToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker bgCrop;
        private System.Windows.Forms.ToolStripMenuItem copyPathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openImageToolStripMenuItem;
        internal System.Windows.Forms.Button btn_search;
        internal System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ToolStripMenuItem moveFileNotAnnotatedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeClassToolStripMenuItem;
    }
}

