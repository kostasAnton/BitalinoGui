using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.Media;
using System.IO;
using System.Collections;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;

namespace BitalinoGui
{
    public partial class SensorForm : Form
    {
        //macSelected:the mac adress which user selects to connect
        private String macSelected;
        //dev:reference to the device he has chosen
        private volatile MyDevice dev;
        //devices:a list of devices. We detect bitalino devices around as then we pass device's properties in that list
        //for each device we detect
        private List<DeviceProps> devices = new List<DeviceProps>();
        //lesState:describes the digital input to channel O1 of bitalino
        private int ledState = 1;
        String[] arraySongs;
        private SoundPlayer player = new SoundPlayer();
        private List<System.Int32> channelsList;
        private List<TabPage> chartPages = new List<TabPage>();
        private short counter_for_songs = 0;
        private DetectiveMonk monk;
        public SensorForm()
        {
            InitializeComponent();
           
            fillFreqComboBox();
            saveExportedFileDialog.Filter = "Text File | *.txt";
            button3.Enabled = true;
            button1.Enabled = false;
            button2.Enabled = false;
            mucisDialog.Multiselect = true;
            monk=new DetectiveMonk(deviceWorker, this, macListBox, devices);
            monk._detectDevices();
        }
        /* 
         A method which just fills the combobox which corresponds to frequencies.
             */
        private void fillFreqComboBox()
        {
            frequenciesComboBox.Items.Add("10");
            frequenciesComboBox.Items.Add("100");
            frequenciesComboBox.Items.Add("1000");

            frequenciesComboBox.SelectedIndex = 1;
        }

        /*
            The button click event for start button.We build up an instance of the device the user chose in order 
            to establish a connection before starting the recording. The we call the deviceWorker's RunWorkerAsync
            because we want the the recording on a seperate loop in order to maintain active the User Interface(we dont
            want it to freezze)
        */
        private void button1_Click(object sender, EventArgs e)
        {
            if (!deviceWorker.IsBusy)
            {
                outPutListBox.Items.Clear();
                chartsTab.TabPages.Clear();
                channelsList = getSelectedChannels();
                if (channelsList.Count == 0)
                {
                    errorBox.Text = "Please select channels";
                    return;
                }
                try
                {
                    buildCharts();
                    macSelected = macListBox.SelectedItem.ToString();
                    int frequency = Convert.ToInt32(frequenciesComboBox.SelectedItem);
                    dev = new MyDevice(macSelected, deviceWorker, channelsList, frequency);
                    button2.Enabled = true;
                    button3.Enabled = true;
                    errorBox.Text = "";
                    deviceWorker.RunWorkerAsync();
                }
                catch (Exception exc)
                {
                    if (exc is NullReferenceException)
                    {
                        errorBox.Text="You propably forgot to choose something(device,etc).Please try again";
                        
                    }
                    if (exc is PluxDotNet.Exception.ContactingDevice)
                    {
                        errorBox.Text = "Lost communication with the device.Please try again";
                        
                    }
                    chartsTab.TabPages.Clear();
                    Console.WriteLine(exc.GetType().ToString()+"--"+exc.Message);
                }
            }
            else
            {
                MessageBox.Show("Recording process in progress");
            }
        }
        /*
            If the user changes the selected mac adress then update the the properties of device on propsListBox
        */
        private void macListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            propsListbox.Items.Clear();
            try
            {
                if (propsListbox.Items.Count > 0)
                {
                    propsListbox.Items.Clear();
                }
                foreach (DeviceProps dp in devices)
                {
                    if (dp.getMac().Equals(macListBox.SelectedItem.ToString()))
                    {
                        propsListbox.Items.Add("Description:" + dp.getDescription());
                        propsListbox.Items.Add("Firmware Version:" + dp.getFirmware());
                        propsListbox.Items.Add("Product ID:" + dp.getProductID());
                        propsListbox.Items.Add("Battery(%):"+dp.getBattery());
                        button1.Enabled = true;
                    }
                }
            }
            catch (Exception exc)
            {//sometimes occurs null ref exception due to the missclick of user..it this happens just return ..do nothing
             //give the user the chance to ckick correctly
                Console.WriteLine("Missclick of user:" + exc.ToString());
                return;
            }

        }
        /*
            This method is called when we call the deviceWorker.RunWorkerAsync(); . It actually starts a seperate thread
            for the recording proccess from bitalino.
        */
        private void deviceWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            dev.Start(dev.getFreq(), dev.getChannelsList(), 16);
            dev.loop();
            e.Cancel = true;
        }
        /*
            This method is called when we call the report progress method fo background worker(deviceworker).
             We actuallly need this in order to update the Graphical User Interface which has been started by the 
             RunWrkerAsync method
        */
        private void deviceWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //if I have a report with a Frame and e.percentage=-100 the update the graphs
            //the percentage is still reported because of reportProgress method's signature
            //as you can see we are not using as as far as it concerns the report on the graph
            if (e.UserState is Frame)
            {

                outPutListBox.Items.Add(((Frame)e.UserState).toString());
                int[] data = ((Frame)e.UserState).getData();
                int i = data.Length - 1;
                foreach (TabPage page in chartsTab.TabPages) 
                {
                    try
                    {

                        ChartControl chart = ((ChartControl)page.Controls["ChartControl"]);
                        Chart c = chart.getChart();
                        
                        c.Series["Series1"].Points.AddY(data[i]);
                        if (c.Series["Series1"].Points.Count > chart.getWantedNumberOfPoints())
                        {
                            chart.removeUnnecessary();
                        }
                        i--;
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine("exc:" + exc.Message + "--on device worker report progress on Charts");
                        
                    }
                }
                return;
            }
            
        }


        /*
            A method for getting the selected channels by the user.        
        */
        private List<System.Int32> getSelectedChannels()
        {
            List<System.Int32> channels = new List<System.Int32>();
            foreach (Control cbox in channelsPannel.Controls)
            {
                if (cbox is CheckBox && ((CheckBox)cbox).Checked)
                {   //the same work can be done with regex:[^0-9]
                    String nameWithNumber = cbox.Text;
                    int channelNum = Convert.ToInt32(nameWithNumber.Replace("A", ""));
                    channels.Add(channelNum);
                }
            }
            return channels;
        }
        /*
         The onclick event of led button.We send an interrupt to bitalino which corresponds to light up/off.
        */

        private void ledButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (ledState == 1)
                {
                    dev.Interrupt(true);
                    ledState = 0;
                }
                else
                {
                    dev.Interrupt(false);
                    ledState = 1;
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                errorBox.Text = "";
                errorBox.Text = "Invalid Operation";
            }
        }

        /*
            This on click event is connected to the stop button . We send a signal to background worker (deviceWorker)
            to stop the corresponding event(recording procedure).
        */
        private void button2_Click(object sender, EventArgs e)
        {
            if (deviceWorker.IsBusy)
            { 
                deviceWorker.CancelAsync();
            }
            exportOutputToolStripMenuItem.Visible = true;
        }
        /*
            This event is triggered when the deviceWorker is completed. 
        */
        private void deviceWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Completed");
            exportOutputToolStripMenuItem.Visible = true;
        }

        private void loadMusicBttn_menu_Click(object sender, EventArgs e)
        {
            if (mucisDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    String[] songs = mucisDialog.FileNames;
                    foreach (string song in songs)
                    {
                        musicListBox.Items.Add(song);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            if (deviceWorker.IsBusy)
            {
                button3.Enabled = true;
            }
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                if (musicListBox.Items.Count > 0)
                {
                    arraySongs = new String[musicListBox.Items.Count];
                    musicListBox.Items.CopyTo(arraySongs, 0);
                }
                else
                {
                    errorBox.Text = "Please select at least one song";
                    return;
                }
                if (deviceWorker.IsBusy)
                {
                    errorBox.Text = "";
                    backgroundWorker1.RunWorkerAsync();
                }
                else
                {
                    errorBox.Text = "In order to tag the outpout the sensor must be on recording mode";
                }
            }
            else
            {
                MessageBox.Show("Sound player is busy with the music on Music Manager");
            }
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (String item in arraySongs)
            {
                backgroundWorker1.ReportProgress(counter_for_songs);
                player.SoundLocation = item;
                dev.Interrupt(true);
                if (backgroundWorker1.CancellationPending)
                {
                    player.Stop();
                    e.Cancel = true;
                }
                player.PlaySync();
                bool load = player.IsLoadCompleted;
                Stopwatch st = new Stopwatch();
                st.Start();
                dev.Interrupt(false);
                counter_for_songs++;
                while (st.ElapsedMilliseconds<5000)
                {
                    if (st.ElapsedMilliseconds>5000)
                    {
                        st.Reset();
                        break;
                    }
                }
            }
            counter_for_songs = 0;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            musicListBox.SelectedItem = musicListBox.Items[e.ProgressPercentage];
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
                //when the playlist is finished stop the sensor            
                deviceWorker.CancelAsync();
                musicListBox.SelectedItem = musicListBox.Items[0];
                exportOutputToolStripMenuItem.Visible = true;
                return;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            outPutListBox.SelectedIndex = musicListBox.Items.Count - 1;
        }

        private void exportOutputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            ArrayList toExport = dev.getExportValues();
            int[,] exportValues = new int[toExport.Count,8];
            int count_ExportedValues = 0;
            int counter_data = 0;
            foreach(Frame frame in toExport)
            {
                exportValues[count_ExportedValues, 1] = frame.getLed();
                int[] data = frame.getData();
                exportValues[count_ExportedValues, 0] = count_ExportedValues;
                for (int i= data.Length-1; i>=0; i--)
                {
                  exportValues[count_ExportedValues, channelsList[i]+1] = data[counter_data];
                  counter_data++;
                }
                count_ExportedValues++;
                counter_data = 0;
             
            }
            exportTxtFile(exportValues);
        }

        private void exportTxtFile(int[,] toExport)
        {
            saveExportedFileDialog.ShowDialog();
            String path = "";
            if (!saveExportedFileDialog.FileName.Equals(""))
            {
                path = saveExportedFileDialog.FileName;
            }
            else
            {
                MessageBox.Show("Please select a location to save the exported file");
                return;
            }
            System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(path);
            string output = "";
            streamWriter.WriteLine(MyDevice.getHeader());
            for (int i = 0; i < toExport.GetUpperBound(0); i++)
            {
                String space = "\t";
                for (int j = 0; j < toExport.GetUpperBound(1); j++)
                {
                    output += toExport[i, j].ToString()+space;
                }
                streamWriter.WriteLine(output);
                output = ""; 
            }
            streamWriter.Close();
        }

        
        private void buildCharts()
        {
            foreach (int channel in channelsList)
            {
                TabPage page = new TabPage();
                page.Text = "A"+Convert.ToString(channel);
                ChartControl chart = new ChartControl();
               
                chart.Dock = DockStyle.Fill;
                page.Controls.Add(chart);
                chartsTab.TabPages.Add(page);
                chartPages.Add(page);
            }
        }
        private void refreshDeviceListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            macListBox.Items.Clear();
            propsListbox.Items.Clear();
            devices = new List<DeviceProps>();//recreate an empty list
            monk.setDeviceList(devices);
            monk.detectionThread();
        }

        private void aboutBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBoxItem item = new AboutBoxItem();
            item.Show();
        }
    }
}

    

    


    
