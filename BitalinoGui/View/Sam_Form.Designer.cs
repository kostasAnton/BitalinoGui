namespace BitalinoGui.View
{
    partial class Sam_Form
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
            System.Windows.Forms.DataVisualization.Charting.LineAnnotation lineAnnotation1 = new System.Windows.Forms.DataVisualization.Charting.LineAnnotation();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.self_assesment_chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.self_assesment_chart)).BeginInit();
            this.SuspendLayout();
            // 
            // self_assesment_chart
            // 
            lineAnnotation1.Name = "arousal";
            lineAnnotation1.X = 32767D;
            lineAnnotation1.Y = 65535D;
            this.self_assesment_chart.Annotations.Add(lineAnnotation1);
            chartArea1.AxisX.Maximum = 65535D;
            chartArea1.AxisX.Minimum = 0D;
            chartArea1.AxisY.Maximum = 65535D;
            chartArea1.AxisY.Minimum = 0D;
            chartArea1.BackImage = "C:\\Users\\Kostas\\Desktop\\Two-dimensional-valence-arousal-space.png";
            chartArea1.BackImageWrapMode = System.Windows.Forms.DataVisualization.Charting.ChartImageWrapMode.Scaled;
            chartArea1.Name = "ChartArea1";
            this.self_assesment_chart.ChartAreas.Add(chartArea1);
            this.self_assesment_chart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.self_assesment_chart.Legends.Add(legend1);
            this.self_assesment_chart.Location = new System.Drawing.Point(0, 0);
            this.self_assesment_chart.Name = "self_assesment_chart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series1.Legend = "Legend1";
            series1.MarkerColor = System.Drawing.Color.Red;
            series1.MarkerSize = 15;
            series1.Name = "Self Assesment Marking";
            this.self_assesment_chart.Series.Add(series1);
            this.self_assesment_chart.Size = new System.Drawing.Size(627, 318);
            this.self_assesment_chart.TabIndex = 1;
            this.self_assesment_chart.Text = "Self Assesment Chart";
            // 
            // Sam_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 318);
            this.Controls.Add(this.self_assesment_chart);
            this.Name = "Sam_Form";
            this.Text = "Real Time Self Assesment Manikin";
            this.Load += new System.EventHandler(this.Sam_Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.self_assesment_chart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart self_assesment_chart;
    }
}