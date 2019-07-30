using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.Media;
using System.Collections;
using System.Windows.Forms.DataVisualization.Charting;
using BitalinoGui.Model;
using BitalinoGui.View;
using BitalinoGui.Controls;
using BitalinoGui.Controller;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;

namespace BitalinoGui
{
    public partial class SensorForm : Form
    {
        //macSelected:the mac adress which user selects to connect
        private String macSelected;
        //lesState:describes the digital input to channel O1 of bitalino
        private int ledState = 1;
        //store of the playlist
        String[] arraySongs;
        //a soundplayer which will play the songs
        private SoundPlayer player = new SoundPlayer();
        //a list with the channels which the sensor hear
        private List<System.Int32> channelsList;
        //store the tapbpages of the chars for accessing reasons for furhter editing
        private List<TabPage> chartPages = new List<TabPage>();
        //a simple counter for songs
        private short counter_for_songs = 0;
        //Sam form in case we want to have real time emotion's assesment
        private Sam_Form samForm = new Sam_Form();
        //this controller is responsible for devices ..Bitalino/Joystick
        private DeviceController dev_controller;
        //A controller who knows the appropriate detective to get the job done
        //The mighty detective will detect bitalino and joystick devices around you.
        private DetectiveController det_controller;
        //A controller which is responsible for camera handling
        private CameraController camcontroller;
        //Information about the ena of a song

        public SensorForm()
        {
            InitializeComponent();
           
            fillFreqComboBox();
            saveExportedFileDialog.Filter = "Text File | *.txt";
            saveExportedFileDialog.Title = "Choose path to save sensor's output.";
            button3.Enabled = true;
            button1.Enabled = false;
            button2.Enabled = false;
      
            mucisDialog.Multiselect = true;
            det_controller = new DetectiveController(this, new splashForm());
            det_controller.initialDetection();
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
                    MessageBox.Show("Please select channels", "An error has occured",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    buildCharts();
                    macSelected = macListBox.SelectedItem.ToString();
                    int frequency = Convert.ToInt32(frequenciesComboBox.SelectedItem);
                    camcontroller = new CameraController(this);
                    MyDevice dev = new MyDevice(macSelected, channelsList, frequency);
                    dev_controller = new DeviceController(dev, deviceWorker,camcontroller);
                    check_Joystick_Annotation_Capability();
                    button2.Enabled = true;
                    button3.Enabled = true;
                    if (intelCameraCbox.Checked && det_controller.getDetectiveMonk().intelRealSenseCam_Connected())
                    {
                        camcontroller.getCamera().setSavePath(getPathToSaveCamFrames());
                        camcontroller.setrecordedBitamps(new ArrayList());
                        Console.WriteLine("Output from camera before starting it with bitalino" + camcontroller.getRecorderBitamaps().Count.ToString());
                        camcontroller.loopCamera_Frames();
                    }
                    else if(intelCameraCbox.Checked && !det_controller.getDetectiveMonk().intelRealSenseCam_Connected())
                    {
                        MessageBox.Show("Intel Real Sense Camera not detected.Please connect it.", "An error has occured",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    deviceWorker.RunWorkerAsync();
                }
                catch (Exception exc)
                {
                    if (exc is NullReferenceException)
                    {
                        MessageBox.Show("You propably forgot to choose something(device,joystick,etc).Please try again","An error has occured",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    if (exc is PluxDotNet.Exception.ContactingDevice)
                    {
                        MessageBox.Show("Lost communication with the device.Please try again", "An error has occured",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    if (exc is PluxDotNet.Exception.AdapterNotFound)
                    {
                        MessageBox.Show("Bluetooth adapter not found.Try to reconnect bluetooth adapter.","An error has occured",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                foreach (DeviceProps dp in Storekeeper.getDevices())
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
            //dev.Start(dev.getFreq(), dev.getChannelsList(), 16);
            //dev.loop();
            //Researcher has 1 minute to leave the room.

            dev_controller.loop();
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
            if (e.UserState is FrameAdapter_Joystick)
            {
                outPutListBox.Items.Add(((FrameAdapter_Joystick)e.UserState).toSrting());
                samForm.placeAxisXY(((FrameAdapter_Joystick)e.UserState).getX_axis(), ((FrameAdapter_Joystick)e.UserState).getY_axis());
                int[] data = ((FrameAdapter_Joystick)e.UserState).getBitalinoFrame().getData();
                int i = data.Length - 1;
                foreach (TabPage page in chartsTab.TabPages) 
                {
                    try
                    {
                        ChartControl chart = ((ChartControl)page.Controls["ChartControl"]);
                        Chart c = chart.getChart();
                        
                        c.Series["Wave"].Points.AddY(data[i]);
                        if (c.Series["Wave"].Points.Count > chart.getWantedNumberOfPoints())
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
                    dev_controller.sendInterrupt(true);
                    ledState = 0;
                }
                else
                {
                    dev_controller.sendInterrupt(false);
                    ledState = 1;
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                MessageBox.Show("Invalid Operation", "An error has occured",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                dev_controller.stopDevice();
            }
            exportOutputToolStripMenuItem.Visible = true;
        }
        /*
            This event is triggered when the deviceWorker is completed. 
        */
        private void deviceWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Completed");
            //exportTextFile();
            camcontroller.cancelCameraRecording();
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
        /*
         This button starts a different thread which marks the output based on playlist using 
         bitalino's led light
        */
        private void button3_Click(object sender, EventArgs e)
        {
            ledButton.Enabled = false;
            if (!backgroundWorker1.IsBusy)
            {
                if (musicListBox.Items.Count > 0)
                {
                    arraySongs = new String[musicListBox.Items.Count];
                    musicListBox.Items.CopyTo(arraySongs, 0);
                }
                else
                {
                    MessageBox.Show("Please select at least one song", "An error has occured",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (deviceWorker.IsBusy)
                {
                    backgroundWorker1.RunWorkerAsync();
                }
                else
                {
                    MessageBox.Show("In order to tag the outpout the sensor must be on recording mode", "An error has occured",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                dev_controller.sendInterrupt(true);
                if (backgroundWorker1.CancellationPending)
                {
                    player.Stop();
                    e.Cancel = true;
                }
                player.PlaySync();
                bool load = player.IsLoadCompleted;
                Stopwatch st = new Stopwatch();
                st.Start();
                dev_controller.sendInterrupt(false);
                counter_for_songs++;
                while (st.ElapsedMilliseconds<45000)
                {
                    if (st.ElapsedMilliseconds>45000)
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
                ledButton.Enabled = true;
                return;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            outPutListBox.SelectedIndex = musicListBox.Items.Count - 1;
        }

        private void exportTextFile()
        {
            ArrayList toExport = dev_controller.getExportValues();
            int[,] exportValues = new int[toExport.Count, 11];
            int count_ExportedValues = 0;
            int counter_data = 0;
            foreach (FrameAdapter_Joystick frame in toExport)
            {
                //place the values of joystick first
                exportValues[count_ExportedValues, 9] = frame.getX_axis();
                exportValues[count_ExportedValues, 10] = frame.getY_axis();
                exportValues[count_ExportedValues, 1] = frame.getBitalinoFrame().getLed();
                int[] data = frame.getBitalinoFrame().getData();
                exportValues[count_ExportedValues, 0] = count_ExportedValues;
                for (int i = data.Length - 1; i >= 0; i--)
                {
                    exportValues[count_ExportedValues, channelsList[i] + 1] = data[counter_data];
                    counter_data++;
                }
                count_ExportedValues++;
                counter_data = 0;
            }
            exportTxtFile(exportValues);
        }

        private void exportOutputToolStripMenuItem_Click(object sender, EventArgs e)
        {

            exportTextFile();
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
            for (int i = 0; i <= toExport.GetUpperBound(0); i++)
            {
                String space = "\t";
                for (int j = 0; j <= toExport.GetUpperBound(1); j++)
                {
                    output += toExport[i, j].ToString()+space;
                }
                streamWriter.WriteLine(output);
                output = ""; 
            }
            streamWriter.Close();
            
            if (camcontroller.getRecorderBitamaps().Count>0)
            {
                exportIntelRealsenseCamFrames();
            }
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
            joystickListBox.Items.Clear();
            propsListbox.Items.Clear();
            Storekeeper.emptyDevicesList();
            det_controller.refreshDevicesList();
        }

        private void aboutBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBoxItem item = new AboutBoxItem();
            item.Show();
        }

        public void fillMacListbox(String item) {
            macListBox.Items.Add(item);
        }

        public void fillJoystickListbox(String item)
        {
            joystickListBox.Items.Add(item);
        }

        public void clearMac_Joystick_ListBox()
        {
            macListBox.Items.Clear();
            joystickListBox.Items.Clear();
        }

        private void check_Joystick_Annotation_Capability()
        {
            if (joystickCbox.Checked && joystickListBox.SelectedItem != null)
            {
                String joystick_name = Convert.ToString(joystickListBox.SelectedItem);
                /*   if (Storekeeper.returnJoystickByName(joystick_name).getMaximumValue()==0)
                   {
                       MessageBox.Show("Please assign maximum value for selected joystick before you proceed");
                       return; 
                   }*/
                dev_controller.setJoystickGuid(Storekeeper.returnJoystickByName(joystick_name));
                Console.WriteLine(dev_controller.getDevice().getJoystick().getMaximumValue());
                samForm.Show();
            }
            else if (joystickCbox.Checked && joystickListBox.SelectedItem.ToString().Equals(""))
            {

                MessageBox.Show("Please select joystick", "An error has occured",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                dev_controller.setJoystickGuid(null);
            }
        }

        private void joystickListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
            try
            {
                ListBox reference_to_OutputListbox = outPutListBox;
                String joystick_name = Convert.ToString(joystickListBox.SelectedItem);
                Joystick_Max_Value_Control maximum = new Joystick_Max_Value_Control(reference_to_OutputListbox, dynamicPanel, Storekeeper.returnJoystickByName(joystick_name));
                maximum.Enabled = true;
                maximum.Visible = true;
                maximum.Dock = DockStyle.Fill;
                dynamicPanel.Controls.Clear();
                dynamicPanel.Controls.Add(maximum);

            }
            catch (Exception exc)
            {
                Console.WriteLine("Missclick of user");
            }
           */
        }

        public void setPictureBoxRgb(Bitmap bitmap)
        {
            this.pictureBox_rgb.Image = bitmap;
        }

        public void setPictureBoxDepth(Bitmap bitmap)
        {
            this.pictureBox_depth.Image = bitmap;
        }

        private DialogResult informUserAboutCamera()
        {
            DialogResult result = MessageBox.Show("Intel real sense camera is not connected on this device.Do you want to start bitalino recording without the camera?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            return result;
        }

        private void intelCamCbox_CheckedChanged(object sender, EventArgs e)
        {
           // camcontroller = new CameraController(this);
        }
        private void exportIntelRealsenseCamFrames()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Choose path to save camera's output";
            dialog.Filter = "Text File | *.txt";
            // Show the FolderBrowserDialog.
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string folderName = dialog.FileName;
                int seq = 0;
                System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(folderName);
                string output = "";
                foreach (String frame in camcontroller.getRecorderBitamaps())
                {
                    output= frame;
                    streamWriter.WriteLine(output);
                    seq++;
                }
                streamWriter.Close();
            }
        }

        private void startCamBttn_Click(object sender, EventArgs e)
        {
            if (camcontroller==null)
            {
                camcontroller = new CameraController(this);
            }
            camcontroller.loopCamera_Frames();
            stopCamBttn.Enabled = true;
        }

        private void stopCamBttn_Click(object sender, EventArgs e)
        {
            camcontroller.cancelCameraRecording();
        }

        private String getPathToSaveCamFrames()
        {
            String path = "";
            SaveFileDialog videoDialog = new SaveFileDialog();
            //videoDialog.Filter= "Avi File | *.avi";
            DialogResult result = videoDialog.ShowDialog();
            if (result==DialogResult.OK)
            {
                path = videoDialog.FileName;
            }
            return path;
        }



    }
}

    

    


    
