namespace BitalinoGui.Controls
{
    partial class Joystick_Max_Value_Control
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Joystick_Max_Value_Control));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.informationToUserRtxtBox = new System.Windows.Forms.RichTextBox();
            this.accessJoystickBttn = new System.Windows.Forms.Button();
            this.retrieveMaxBttn = new System.Windows.Forms.Button();
            this.getMaximumValueWorker = new System.ComponentModel.BackgroundWorker();
            this.maxChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.maxChart)).BeginInit();
            this.SuspendLayout();
            // 
            // informationToUserRtxtBox
            // 
            this.informationToUserRtxtBox.Location = new System.Drawing.Point(0, 0);
            this.informationToUserRtxtBox.Name = "informationToUserRtxtBox";
            this.informationToUserRtxtBox.Size = new System.Drawing.Size(125, 209);
            this.informationToUserRtxtBox.TabIndex = 1;
            this.informationToUserRtxtBox.Text = resources.GetString("informationToUserRtxtBox.Text");
            // 
            // accessJoystickBttn
            // 
            this.accessJoystickBttn.Location = new System.Drawing.Point(131, 177);
            this.accessJoystickBttn.Name = "accessJoystickBttn";
            this.accessJoystickBttn.Size = new System.Drawing.Size(75, 23);
            this.accessJoystickBttn.TabIndex = 1;
            this.accessJoystickBttn.Text = "Access";
            this.accessJoystickBttn.UseVisualStyleBackColor = true;
            this.accessJoystickBttn.Click += new System.EventHandler(this.accessJoystickBttn_Click);
            // 
            // retrieveMaxBttn
            // 
            this.retrieveMaxBttn.Location = new System.Drawing.Point(417, 177);
            this.retrieveMaxBttn.Name = "retrieveMaxBttn";
            this.retrieveMaxBttn.Size = new System.Drawing.Size(75, 23);
            this.retrieveMaxBttn.TabIndex = 0;
            this.retrieveMaxBttn.Text = "Retrieve";
            this.retrieveMaxBttn.UseVisualStyleBackColor = true;
            this.retrieveMaxBttn.Click += new System.EventHandler(this.retrieveMaxBttn_Click);
            // 
            // getMaximumValueWorker
            // 
            this.getMaximumValueWorker.WorkerReportsProgress = true;
            this.getMaximumValueWorker.WorkerSupportsCancellation = true;
            this.getMaximumValueWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.getMaximumValueWorker_DoWork);
            this.getMaximumValueWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.getMaximumValueWorker_ProgressChanged);
            this.getMaximumValueWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.getMaximumValueWorker_RunWorkerCompleted);
            // 
            // maxChart
            // 
            chartArea1.AxisX.Minimum = 0D;
            chartArea1.AxisY.Minimum = 0D;
            chartArea1.Name = "ChartArea1";
            this.maxChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.maxChart.Legends.Add(legend1);
            this.maxChart.Location = new System.Drawing.Point(131, 0);
            this.maxChart.Name = "maxChart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series1.Legend = "Legend1";
            series1.Name = "Joystick Value";
            this.maxChart.Series.Add(series1);
            this.maxChart.Size = new System.Drawing.Size(361, 171);
            this.maxChart.TabIndex = 0;
            this.maxChart.Text = "maxChart";
            // 
            // Joystick_Max_Value_Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.accessJoystickBttn);
            this.Controls.Add(this.maxChart);
            this.Controls.Add(this.retrieveMaxBttn);
            this.Controls.Add(this.informationToUserRtxtBox);
            this.Name = "Joystick_Max_Value_Control";
            this.Size = new System.Drawing.Size(495, 209);
            ((System.ComponentModel.ISupportInitialize)(this.maxChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox informationToUserRtxtBox;
        private System.Windows.Forms.Button accessJoystickBttn;
        private System.Windows.Forms.Button retrieveMaxBttn;
        private System.ComponentModel.BackgroundWorker getMaximumValueWorker;
        private System.Windows.Forms.DataVisualization.Charting.Chart maxChart;
    }
}
