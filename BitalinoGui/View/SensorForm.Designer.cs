﻿namespace BitalinoGui
{
    
    partial class SensorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SensorForm));
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.macListBox = new System.Windows.Forms.ListBox();
            this.outPutListBox = new System.Windows.Forms.ListBox();
            this.deviceWorker = new System.ComponentModel.BackgroundWorker();
            this.propsListbox = new System.Windows.Forms.ListBox();
            this.channelsPannel = new System.Windows.Forms.Panel();
            this.channelLabel = new System.Windows.Forms.Label();
            this.freqLabel = new System.Windows.Forms.Label();
            this.ledButton = new System.Windows.Forms.Button();
            this.frequenciesComboBox = new System.Windows.Forms.ComboBox();
            this.cboxA6 = new System.Windows.Forms.CheckBox();
            this.cboxA5 = new System.Windows.Forms.CheckBox();
            this.cboxA4 = new System.Windows.Forms.CheckBox();
            this.cboxA3 = new System.Windows.Forms.CheckBox();
            this.cboxA2 = new System.Windows.Forms.CheckBox();
            this.cboxA1 = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MenuObject = new System.Windows.Forms.ToolStripMenuItem();
            this.loadMusicBttn_menu = new System.Windows.Forms.ToolStripMenuItem();
            this.exportOutputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshDeviceListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutBoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mucisDialog = new System.Windows.Forms.OpenFileDialog();
            this.sensorGroupBox = new System.Windows.Forms.GroupBox();
            this.intelCameraCbox = new System.Windows.Forms.CheckBox();
            this.avaiableJoystickLabel = new System.Windows.Forms.Label();
            this.dynamicPanel = new System.Windows.Forms.Panel();
            this.joystickCbox = new System.Windows.Forms.CheckBox();
            this.joystickListBox = new System.Windows.Forms.ListBox();
            this.button3 = new System.Windows.Forms.Button();
            this.mainTab = new System.Windows.Forms.TabControl();
            this.sensorTab = new System.Windows.Forms.TabPage();
            this.musicTab = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.musicListBox = new System.Windows.Forms.ListBox();
            this.chartsTab = new System.Windows.Forms.TabControl();
            this.cameraTabPage = new System.Windows.Forms.TabPage();
            this.stopCamBttn = new System.Windows.Forms.Button();
            this.startCamBttn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox_depth = new System.Windows.Forms.PictureBox();
            this.pictureBox_rgb = new System.Windows.Forms.PictureBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.saveExportedFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.channelsPannel.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.sensorGroupBox.SuspendLayout();
            this.dynamicPanel.SuspendLayout();
            this.mainTab.SuspendLayout();
            this.sensorTab.SuspendLayout();
            this.musicTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.cameraTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_depth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_rgb)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 231);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bitalino Plux Devices";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(582, 247);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Connect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // macListBox
            // 
            this.macListBox.FormattingEnabled = true;
            this.macListBox.Location = new System.Drawing.Point(9, 247);
            this.macListBox.Name = "macListBox";
            this.macListBox.Size = new System.Drawing.Size(230, 108);
            this.macListBox.TabIndex = 2;
            this.macListBox.SelectedValueChanged += new System.EventHandler(this.macListBox_SelectedValueChanged);
            // 
            // outPutListBox
            // 
            this.outPutListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outPutListBox.FormattingEnabled = true;
            this.outPutListBox.HorizontalScrollbar = true;
            this.outPutListBox.Location = new System.Drawing.Point(0, 0);
            this.outPutListBox.Name = "outPutListBox";
            this.outPutListBox.Size = new System.Drawing.Size(495, 209);
            this.outPutListBox.TabIndex = 7;
            // 
            // deviceWorker
            // 
            this.deviceWorker.WorkerReportsProgress = true;
            this.deviceWorker.WorkerSupportsCancellation = true;
            this.deviceWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.deviceWorker_DoWork);
            this.deviceWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.deviceWorker_ProgressChanged);
            this.deviceWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.deviceWorker_RunWorkerCompleted);
            // 
            // propsListbox
            // 
            this.propsListbox.FormattingEnabled = true;
            this.propsListbox.Location = new System.Drawing.Point(245, 247);
            this.propsListbox.Name = "propsListbox";
            this.propsListbox.Size = new System.Drawing.Size(143, 108);
            this.propsListbox.TabIndex = 8;
            // 
            // channelsPannel
            // 
            this.channelsPannel.Controls.Add(this.channelLabel);
            this.channelsPannel.Controls.Add(this.freqLabel);
            this.channelsPannel.Controls.Add(this.ledButton);
            this.channelsPannel.Controls.Add(this.frequenciesComboBox);
            this.channelsPannel.Controls.Add(this.cboxA6);
            this.channelsPannel.Controls.Add(this.cboxA5);
            this.channelsPannel.Controls.Add(this.cboxA4);
            this.channelsPannel.Controls.Add(this.cboxA3);
            this.channelsPannel.Controls.Add(this.cboxA2);
            this.channelsPannel.Controls.Add(this.cboxA1);
            this.channelsPannel.Location = new System.Drawing.Point(504, 19);
            this.channelsPannel.Name = "channelsPannel";
            this.channelsPannel.Size = new System.Drawing.Size(157, 147);
            this.channelsPannel.TabIndex = 9;
            // 
            // channelLabel
            // 
            this.channelLabel.AutoSize = true;
            this.channelLabel.Location = new System.Drawing.Point(1, 0);
            this.channelLabel.Name = "channelLabel";
            this.channelLabel.Size = new System.Drawing.Size(51, 13);
            this.channelLabel.TabIndex = 10;
            this.channelLabel.Text = "Channels";
            // 
            // freqLabel
            // 
            this.freqLabel.AutoSize = true;
            this.freqLabel.Location = new System.Drawing.Point(3, 104);
            this.freqLabel.Name = "freqLabel";
            this.freqLabel.Size = new System.Drawing.Size(57, 13);
            this.freqLabel.TabIndex = 9;
            this.freqLabel.Text = "Frequency";
            // 
            // ledButton
            // 
            this.ledButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.ledButton.Location = new System.Drawing.Point(78, 61);
            this.ledButton.Name = "ledButton";
            this.ledButton.Size = new System.Drawing.Size(47, 44);
            this.ledButton.TabIndex = 11;
            this.ledButton.Text = "light up led";
            this.ledButton.UseVisualStyleBackColor = true;
            this.ledButton.Click += new System.EventHandler(this.ledButton_Click);
            // 
            // frequenciesComboBox
            // 
            this.frequenciesComboBox.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.frequenciesComboBox.FormattingEnabled = true;
            this.frequenciesComboBox.Location = new System.Drawing.Point(4, 120);
            this.frequenciesComboBox.Name = "frequenciesComboBox";
            this.frequenciesComboBox.Size = new System.Drawing.Size(121, 21);
            this.frequenciesComboBox.TabIndex = 8;
            // 
            // cboxA6
            // 
            this.cboxA6.AutoSize = true;
            this.cboxA6.Location = new System.Drawing.Point(78, 38);
            this.cboxA6.Name = "cboxA6";
            this.cboxA6.Size = new System.Drawing.Size(39, 17);
            this.cboxA6.TabIndex = 5;
            this.cboxA6.Text = "A6";
            this.cboxA6.UseVisualStyleBackColor = true;
            // 
            // cboxA5
            // 
            this.cboxA5.AutoSize = true;
            this.cboxA5.Location = new System.Drawing.Point(78, 13);
            this.cboxA5.Name = "cboxA5";
            this.cboxA5.Size = new System.Drawing.Size(39, 17);
            this.cboxA5.TabIndex = 4;
            this.cboxA5.Text = "A5";
            this.cboxA5.UseVisualStyleBackColor = true;
            // 
            // cboxA4
            // 
            this.cboxA4.AutoSize = true;
            this.cboxA4.Location = new System.Drawing.Point(3, 84);
            this.cboxA4.Name = "cboxA4";
            this.cboxA4.Size = new System.Drawing.Size(39, 17);
            this.cboxA4.TabIndex = 3;
            this.cboxA4.Text = "A4";
            this.cboxA4.UseVisualStyleBackColor = true;
            // 
            // cboxA3
            // 
            this.cboxA3.AutoSize = true;
            this.cboxA3.Location = new System.Drawing.Point(3, 61);
            this.cboxA3.Name = "cboxA3";
            this.cboxA3.Size = new System.Drawing.Size(39, 17);
            this.cboxA3.TabIndex = 2;
            this.cboxA3.Text = "A3";
            this.cboxA3.UseVisualStyleBackColor = true;
            // 
            // cboxA2
            // 
            this.cboxA2.AutoSize = true;
            this.cboxA2.Location = new System.Drawing.Point(3, 38);
            this.cboxA2.Name = "cboxA2";
            this.cboxA2.Size = new System.Drawing.Size(39, 17);
            this.cboxA2.TabIndex = 1;
            this.cboxA2.Text = "A2";
            this.cboxA2.UseVisualStyleBackColor = true;
            // 
            // cboxA1
            // 
            this.cboxA1.AutoSize = true;
            this.cboxA1.Location = new System.Drawing.Point(3, 15);
            this.cboxA1.Name = "cboxA1";
            this.cboxA1.Size = new System.Drawing.Size(39, 17);
            this.cboxA1.TabIndex = 0;
            this.cboxA1.Text = "A1";
            this.cboxA1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(582, 287);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "Stop";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuObject});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(679, 24);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MenuObject
            // 
            this.MenuObject.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadMusicBttn_menu,
            this.exportOutputToolStripMenuItem,
            this.refreshDeviceListToolStripMenuItem,
            this.aboutBoxToolStripMenuItem});
            this.MenuObject.Name = "MenuObject";
            this.MenuObject.Size = new System.Drawing.Size(50, 20);
            this.MenuObject.Text = "Menu";
            // 
            // loadMusicBttn_menu
            // 
            this.loadMusicBttn_menu.Name = "loadMusicBttn_menu";
            this.loadMusicBttn_menu.Size = new System.Drawing.Size(172, 22);
            this.loadMusicBttn_menu.Text = "Load Music";
            this.loadMusicBttn_menu.Click += new System.EventHandler(this.loadMusicBttn_menu_Click);
            // 
            // exportOutputToolStripMenuItem
            // 
            this.exportOutputToolStripMenuItem.Name = "exportOutputToolStripMenuItem";
            this.exportOutputToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.exportOutputToolStripMenuItem.Text = "Export Output";
            this.exportOutputToolStripMenuItem.Visible = false;
            this.exportOutputToolStripMenuItem.Click += new System.EventHandler(this.exportOutputToolStripMenuItem_Click);
            // 
            // refreshDeviceListToolStripMenuItem
            // 
            this.refreshDeviceListToolStripMenuItem.Name = "refreshDeviceListToolStripMenuItem";
            this.refreshDeviceListToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.refreshDeviceListToolStripMenuItem.Text = "Refresh Device List";
            this.refreshDeviceListToolStripMenuItem.Click += new System.EventHandler(this.refreshDeviceListToolStripMenuItem_Click);
            // 
            // aboutBoxToolStripMenuItem
            // 
            this.aboutBoxToolStripMenuItem.Name = "aboutBoxToolStripMenuItem";
            this.aboutBoxToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.aboutBoxToolStripMenuItem.Text = "About Box";
            this.aboutBoxToolStripMenuItem.Click += new System.EventHandler(this.aboutBoxToolStripMenuItem_Click);
            // 
            // sensorGroupBox
            // 
            this.sensorGroupBox.BackColor = System.Drawing.Color.LightSteelBlue;
            this.sensorGroupBox.Controls.Add(this.intelCameraCbox);
            this.sensorGroupBox.Controls.Add(this.avaiableJoystickLabel);
            this.sensorGroupBox.Controls.Add(this.dynamicPanel);
            this.sensorGroupBox.Controls.Add(this.joystickCbox);
            this.sensorGroupBox.Controls.Add(this.joystickListBox);
            this.sensorGroupBox.Controls.Add(this.button3);
            this.sensorGroupBox.Controls.Add(this.channelsPannel);
            this.sensorGroupBox.Controls.Add(this.button2);
            this.sensorGroupBox.Controls.Add(this.button1);
            this.sensorGroupBox.Controls.Add(this.propsListbox);
            this.sensorGroupBox.Controls.Add(this.label1);
            this.sensorGroupBox.Controls.Add(this.macListBox);
            this.sensorGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sensorGroupBox.Location = new System.Drawing.Point(3, 3);
            this.sensorGroupBox.Name = "sensorGroupBox";
            this.sensorGroupBox.Size = new System.Drawing.Size(665, 359);
            this.sensorGroupBox.TabIndex = 15;
            this.sensorGroupBox.TabStop = false;
            this.sensorGroupBox.Text = "Sensor Manager";
            // 
            // intelCameraCbox
            // 
            this.intelCameraCbox.AutoSize = true;
            this.intelCameraCbox.Location = new System.Drawing.Point(504, 196);
            this.intelCameraCbox.Name = "intelCameraCbox";
            this.intelCameraCbox.Size = new System.Drawing.Size(143, 17);
            this.intelCameraCbox.TabIndex = 19;
            this.intelCameraCbox.Text = "Intel Real Sense Camera";
            this.intelCameraCbox.UseVisualStyleBackColor = true;
            this.intelCameraCbox.CheckedChanged += new System.EventHandler(this.intelCamCbox_CheckedChanged);
            // 
            // avaiableJoystickLabel
            // 
            this.avaiableJoystickLabel.AutoSize = true;
            this.avaiableJoystickLabel.Location = new System.Drawing.Point(394, 231);
            this.avaiableJoystickLabel.Name = "avaiableJoystickLabel";
            this.avaiableJoystickLabel.Size = new System.Drawing.Size(96, 13);
            this.avaiableJoystickLabel.TabIndex = 18;
            this.avaiableJoystickLabel.Text = "Available Joysticks";
            // 
            // dynamicPanel
            // 
            this.dynamicPanel.Controls.Add(this.outPutListBox);
            this.dynamicPanel.Location = new System.Drawing.Point(3, 19);
            this.dynamicPanel.Name = "dynamicPanel";
            this.dynamicPanel.Size = new System.Drawing.Size(495, 209);
            this.dynamicPanel.TabIndex = 17;
            // 
            // joystickCbox
            // 
            this.joystickCbox.AutoSize = true;
            this.joystickCbox.Location = new System.Drawing.Point(504, 172);
            this.joystickCbox.Name = "joystickCbox";
            this.joystickCbox.Size = new System.Drawing.Size(153, 17);
            this.joystickCbox.TabIndex = 16;
            this.joystickCbox.Text = "Annotate data with joystick";
            this.joystickCbox.UseVisualStyleBackColor = true;
            // 
            // joystickListBox
            // 
            this.joystickListBox.FormattingEnabled = true;
            this.joystickListBox.Location = new System.Drawing.Point(394, 248);
            this.joystickListBox.Name = "joystickListBox";
            this.joystickListBox.Size = new System.Drawing.Size(185, 108);
            this.joystickListBox.TabIndex = 15;
            this.joystickListBox.SelectedValueChanged += new System.EventHandler(this.joystickListBox_SelectedValueChanged);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(582, 330);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = " Tag Output";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // mainTab
            // 
            this.mainTab.Controls.Add(this.sensorTab);
            this.mainTab.Controls.Add(this.musicTab);
            this.mainTab.Controls.Add(this.cameraTabPage);
            this.mainTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTab.Location = new System.Drawing.Point(0, 24);
            this.mainTab.Name = "mainTab";
            this.mainTab.SelectedIndex = 0;
            this.mainTab.Size = new System.Drawing.Size(679, 391);
            this.mainTab.TabIndex = 14;
            // 
            // sensorTab
            // 
            this.sensorTab.BackColor = System.Drawing.Color.LavenderBlush;
            this.sensorTab.Controls.Add(this.sensorGroupBox);
            this.sensorTab.Location = new System.Drawing.Point(4, 22);
            this.sensorTab.Name = "sensorTab";
            this.sensorTab.Padding = new System.Windows.Forms.Padding(3);
            this.sensorTab.Size = new System.Drawing.Size(671, 365);
            this.sensorTab.TabIndex = 0;
            this.sensorTab.Text = "Sensor Manager";
            // 
            // musicTab
            // 
            this.musicTab.Controls.Add(this.splitContainer1);
            this.musicTab.Location = new System.Drawing.Point(4, 22);
            this.musicTab.Name = "musicTab";
            this.musicTab.Padding = new System.Windows.Forms.Padding(3);
            this.musicTab.Size = new System.Drawing.Size(671, 365);
            this.musicTab.TabIndex = 1;
            this.musicTab.Text = "Music Manager";
            this.musicTab.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.musicListBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.chartsTab);
            this.splitContainer1.Size = new System.Drawing.Size(665, 359);
            this.splitContainer1.SplitterDistance = 178;
            this.splitContainer1.TabIndex = 5;
            // 
            // musicListBox
            // 
            this.musicListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.musicListBox.FormattingEnabled = true;
            this.musicListBox.Location = new System.Drawing.Point(0, 0);
            this.musicListBox.Name = "musicListBox";
            this.musicListBox.Size = new System.Drawing.Size(665, 178);
            this.musicListBox.TabIndex = 0;
            // 
            // chartsTab
            // 
            this.chartsTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartsTab.Location = new System.Drawing.Point(0, 0);
            this.chartsTab.Name = "chartsTab";
            this.chartsTab.SelectedIndex = 0;
            this.chartsTab.Size = new System.Drawing.Size(665, 177);
            this.chartsTab.TabIndex = 0;
            // 
            // cameraTabPage
            // 
            this.cameraTabPage.Controls.Add(this.stopCamBttn);
            this.cameraTabPage.Controls.Add(this.startCamBttn);
            this.cameraTabPage.Controls.Add(this.label3);
            this.cameraTabPage.Controls.Add(this.label2);
            this.cameraTabPage.Controls.Add(this.pictureBox_depth);
            this.cameraTabPage.Controls.Add(this.pictureBox_rgb);
            this.cameraTabPage.Location = new System.Drawing.Point(4, 22);
            this.cameraTabPage.Name = "cameraTabPage";
            this.cameraTabPage.Size = new System.Drawing.Size(671, 365);
            this.cameraTabPage.TabIndex = 2;
            this.cameraTabPage.Text = "Camera Manager";
            this.cameraTabPage.UseVisualStyleBackColor = true;
            // 
            // stopCamBttn
            // 
            this.stopCamBttn.Enabled = false;
            this.stopCamBttn.Location = new System.Drawing.Point(593, 253);
            this.stopCamBttn.Name = "stopCamBttn";
            this.stopCamBttn.Size = new System.Drawing.Size(75, 23);
            this.stopCamBttn.TabIndex = 5;
            this.stopCamBttn.Text = "Stop";
            this.stopCamBttn.UseVisualStyleBackColor = true;
            this.stopCamBttn.Click += new System.EventHandler(this.stopCamBttn_Click);
            // 
            // startCamBttn
            // 
            this.startCamBttn.Location = new System.Drawing.Point(11, 253);
            this.startCamBttn.Name = "startCamBttn";
            this.startCamBttn.Size = new System.Drawing.Size(84, 23);
            this.startCamBttn.TabIndex = 4;
            this.startCamBttn.Text = "Start Camera";
            this.startCamBttn.UseVisualStyleBackColor = true;
            this.startCamBttn.Click += new System.EventHandler(this.startCamBttn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(424, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Check the corresponding checkbox on Sensor Manager to start the camera with bital" +
    "ino.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(395, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Here you can see the frames which is captured by Intel Real Sense Depth Camera";
            // 
            // pictureBox_depth
            // 
            this.pictureBox_depth.Location = new System.Drawing.Point(345, 44);
            this.pictureBox_depth.Name = "pictureBox_depth";
            this.pictureBox_depth.Size = new System.Drawing.Size(323, 203);
            this.pictureBox_depth.TabIndex = 1;
            this.pictureBox_depth.TabStop = false;
            // 
            // pictureBox_rgb
            // 
            this.pictureBox_rgb.Location = new System.Drawing.Point(8, 44);
            this.pictureBox_rgb.Name = "pictureBox_rgb";
            this.pictureBox_rgb.Size = new System.Drawing.Size(331, 203);
            this.pictureBox_rgb.TabIndex = 0;
            this.pictureBox_rgb.TabStop = false;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // SensorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(679, 415);
            this.Controls.Add(this.mainTab);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SensorForm";
            this.Text = "BitalinoGUI";
            this.channelsPannel.ResumeLayout(false);
            this.channelsPannel.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.sensorGroupBox.ResumeLayout(false);
            this.sensorGroupBox.PerformLayout();
            this.dynamicPanel.ResumeLayout(false);
            this.mainTab.ResumeLayout(false);
            this.sensorTab.ResumeLayout(false);
            this.musicTab.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.cameraTabPage.ResumeLayout(false);
            this.cameraTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_depth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_rgb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox macListBox;
        private System.Windows.Forms.ListBox outPutListBox;
        private System.ComponentModel.BackgroundWorker deviceWorker;
        private System.Windows.Forms.ListBox propsListbox;
        private System.Windows.Forms.Panel channelsPannel;
        private System.Windows.Forms.CheckBox cboxA6;
        private System.Windows.Forms.CheckBox cboxA5;
        private System.Windows.Forms.CheckBox cboxA4;
        private System.Windows.Forms.CheckBox cboxA3;
        private System.Windows.Forms.CheckBox cboxA2;
        private System.Windows.Forms.CheckBox cboxA1;
        private System.Windows.Forms.ComboBox frequenciesComboBox;
        private System.Windows.Forms.Label channelLabel;
        private System.Windows.Forms.Label freqLabel;
        private System.Windows.Forms.Button ledButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuObject;
        private System.Windows.Forms.ToolStripMenuItem loadMusicBttn_menu;
        private System.Windows.Forms.OpenFileDialog mucisDialog;
        private System.Windows.Forms.GroupBox sensorGroupBox;
        private System.Windows.Forms.TabControl mainTab;
        private System.Windows.Forms.TabPage sensorTab;
        private System.Windows.Forms.TabPage musicTab;
        private System.Windows.Forms.ListBox musicListBox;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ToolStripMenuItem exportOutputToolStripMenuItem;
        private System.Windows.Forms.TabControl chartsTab;
        private System.Windows.Forms.SaveFileDialog saveExportedFileDialog;
        private System.Windows.Forms.ToolStripMenuItem refreshDeviceListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutBoxToolStripMenuItem;
        private System.Windows.Forms.ListBox joystickListBox;
        private System.Windows.Forms.CheckBox joystickCbox;
        private System.Windows.Forms.Panel dynamicPanel;
        private System.Windows.Forms.Label avaiableJoystickLabel;
        private System.Windows.Forms.TabPage cameraTabPage;
        private System.Windows.Forms.PictureBox pictureBox_depth;
        private System.Windows.Forms.PictureBox pictureBox_rgb;
        private System.Windows.Forms.CheckBox intelCameraCbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button stopCamBttn;
        private System.Windows.Forms.Button startCamBttn;
    }
}

