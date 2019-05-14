using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace BitalinoGui
{   /*
        A chart control buildl by the user each depending on how many analog channels he/she picks
        It is used for the visual represantation of the data the sensor gives.
     */
    public partial class ChartControl : UserControl
    {
        private int pointsInChart = 25;

        public ChartControl()
        {
            InitializeComponent();
        }

        public Chart getChart()
        {
            return this.chartVisualization;
        }

        public int getWantedNumberOfPoints()
        {
            return this.pointsInChart;
        }

        public void removeUnnecessary()
        {
            while (chartVisualization.Series["Series1"].Points.Count > pointsInChart)
            {
                chartVisualization.Series["Series1"].Points.RemoveAt(0);
            }
        }


    }
}
