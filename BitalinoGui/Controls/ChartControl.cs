
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace BitalinoGui
{   /*  <<Reapetable User Control>>
        A chart control build by the user depending on how many analog channels he/she picks
        It is used for the visual represantation of the data the sensor gives.
     */
    public partial class ChartControl : UserControl
    {
        //the points which the chart fits so it will not be composed.
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
        //We need to remove the expentable data from the chart in order not to have visualization issues
        public void removeUnnecessary()
        {
            while (chartVisualization.Series["Wave"].Points.Count > pointsInChart)
            {
                chartVisualization.Series["Wave"].Points.RemoveAt(0);
            }
        }


    }
}
