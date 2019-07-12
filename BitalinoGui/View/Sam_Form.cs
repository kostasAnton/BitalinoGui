using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BitalinoGui.View
{
    public partial class Sam_Form : Form
    {
        public Sam_Form()
        {
            InitializeComponent();
        }

        public void setMaxMinValue_XY_Axis(int maximum)
        {
            self_assesment_chart.ChartAreas[0].AxisX.Maximum = maximum;
            self_assesment_chart.ChartAreas[0].AxisY.Maximum = maximum;
            self_assesment_chart.ChartAreas[0].AxisX.Minimum = 0;
            self_assesment_chart.ChartAreas[0].AxisY.Minimum = 0;
        }

        public void placeAxisXY(int X,int Y)
        {
            clearSelfAssesment_Chart();
            self_assesment_chart.Series["Self Assesment Marking"].Points.AddXY(X, Y);
        }

        public void clearSelfAssesment_Chart()
        {
            self_assesment_chart.Series["Self Assesment Marking"].Points.Clear();
        }

        private void Sam_Form_Load(object sender, EventArgs e)
        {

        }
    }
}
