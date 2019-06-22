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

namespace BitalinoGui.Controls
{
    public partial class Joystick_Max_Value_Control : UserControl
    {
        //the joystick which we want to detect its maximum value
        private Joystick joystick_to_get_maximum_value;
        //a list which will contain the maximum value of joystick.
        List<DataPoint> points = new List<DataPoint>();
        //varaible which indicates the state of joystick's maxximum value detection
        private bool max_state = true;
        //reference to the ListBox which will replace this controll
        ListBox outputListBox;
        Panel dynamicPnael;

        public Joystick_Max_Value_Control(ListBox outputListBox,Panel panel,Joystick joystick_to_get_maximum_value)
        {
            this.joystick_to_get_maximum_value = joystick_to_get_maximum_value;
            this.dynamicPnael = panel;
            this.outputListBox = outputListBox;
            InitializeComponent();
        }

        private void retrieveMaxBttn_Click(object sender, EventArgs e)
        {
            var collection = maxChart.Series.Select(series => series.Points.Where(point => point.XValue != 0).ToList()).ToList();
            collection.ForEach(series => series.ForEach(points.Add));
            getMaximumValueWorker.CancelAsync();
        }

        public void findJoystickMaximumValue(Joystick joystick_to_get_maximum_value)
        {
            this.joystick_to_get_maximum_value = joystick_to_get_maximum_value;
        }

        private void accessJoystickBttn_Click(object sender, EventArgs e)
        {

            retrieveMaxBttn.Enabled = true;
            accessJoystickBttn.Enabled = false;
            getMaximumValueWorker.RunWorkerAsync();
        }

        private void getMaximumValueWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            joystick_to_get_maximum_value.Acquire();
            while (true)
            {
                if (getMaximumValueWorker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                var state = joystick_to_get_maximum_value.GetCurrentState();
                /*this is a bad techinque to report progress when you use a backgroundworker
                However in that use case joystick reports progress so fast that chart's axis 
                values dont have the time to adjust its values.As a result if you try to update
                the GUI via the classic way of reportProgress everything is going to freeze.
                So we end up having a backgroundworker's message queue full all the time and GUI
                does not have the appropriate time to update itself.*/
                
                maxChart.Invoke(new Action(() =>
                maxChart.Series["Joystick Value"].Points.AddXY(state.X, state.Y)
                ));
            }
        }

        private void getMaximumValueWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            maxChart.Series["Joystick Value"].Points.Clear();
            maxChart.Series["Joystick Value"].Points.AddXY(e.ProgressPercentage, Convert.ToInt32(e.UserState));

        }

        public int getMaximum()
        {
            List<int> simpleInts = new List<int>();
            foreach (DataPoint dp in points)
            {
                simpleInts.Add(Convert.ToInt32(dp.XValue));
            }
            return simpleInts.Max();
        }

        public bool getMaxDet_State()
        {
            return max_state;
        }

        private void getMaximumValueWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            joystick_to_get_maximum_value.setMaximumValue(getMaximum());
            returnGuiToInitialState();
            max_state = false;
        }

        public void returnGuiToInitialState()
        {
            dynamicPnael.Controls.Clear();
            dynamicPnael.Controls.Add(outputListBox);
        }
    }
}
