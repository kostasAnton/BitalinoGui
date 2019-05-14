using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BitalinoGui
{
    /*
        A mighty detective whon detects Bitalino devices and brings to the light their properties. 
        This class detects bitalino devices and depending the method which has been called it updates the 
        graphical user interface.
    */
    class DetectiveMonk
    {

        private List<DeviceProps> devices;
        private BackgroundWorker deviceWorker;
        private SensorForm form;
        private ListBox macListBox;
        public DetectiveMonk(BackgroundWorker deviceWorker,SensorForm form,ListBox macListBox, List<DeviceProps> devices)
        {
            this.deviceWorker = deviceWorker;
            this.form = form;
            this.macListBox = macListBox;
            this.devices = devices;
        }

        public void setDeviceList(List<DeviceProps> devices)
        {
            this.devices = devices;
        }

        public List<DeviceProps> getDevices()
        {
            return this.devices;
        }

        public void setForm(SensorForm form)
        {
            this.form = form;
        }

        public SensorForm getSensorForm()
        {
            return this.form;
        }

        public void setListBox(ListBox macListBox)
        {
            this.macListBox = macListBox;
        }

        public ListBox getListBox()
        {
            return this.macListBox;
        }

        public void detectionThread()
        {
            ThreadStart start_detect = new ThreadStart(detectDevices);
            Thread detectiveMonk = new Thread(start_detect);
            detectiveMonk.Start();
        }

        /*
            This method detects bitalino devices after the form has loaded. We use it to refresh the 
            device list the user sees. It actually updates the macListBox Control.
        */
        private void detectDevices()
        {
            try
            {
                List<PluxDotNet.DevInfo> devs = PluxDotNet.BaseDev.FindDevices();
                foreach (PluxDotNet.DevInfo devInfo in devs)
                {
                    System.Console.WriteLine("{0} - {1}", devInfo.path, devInfo.description);
                    MyDevice dev = new MyDevice(devInfo.path, deviceWorker);
                    Dictionary<string, object> props = dev.GetProperties();
                    devices.Add(new DeviceProps(props["description"].ToString(), props["fwVersion"].ToString(),
                        props["path"].ToString(), props["productID"].ToString(), Convert.ToString(dev.GetBattery())));

                    this.form.Invoke(
                        (MethodInvoker)delegate
                        {
                            macListBox.Items.Add(props["path"].ToString());
                            form.Cursor = Cursors.Default;
                        }
                    );
                    dev.Dispose();
                }
                return;
            }
            catch (PluxDotNet.Exception.PluxException e)
            {
                System.Console.WriteLine("Exception: {0}", e.Message);
                if (e.Message.Contains("lost"))
                {
                    detectDevices();
                }
            }
        }

        /*
            A method for detecting bitalino devices before we load up the graphical user interface  
        */
        public void _detectDevices()
        {
            try
            {
                List<PluxDotNet.DevInfo> devs = PluxDotNet.BaseDev.FindDevices();
                foreach (PluxDotNet.DevInfo devInfo in devs)
                {
                    System.Console.WriteLine("{0} - {1}", devInfo.path, devInfo.description);
                    MyDevice dev = new MyDevice(devInfo.path, deviceWorker);
                    Dictionary<string, object> props = dev.GetProperties();
                    devices.Add(new DeviceProps(props["description"].ToString(), props["fwVersion"].ToString(),
                        props["path"].ToString(), props["productID"].ToString(), Convert.ToString(dev.GetBattery())));
                    macListBox.Items.Add(props["path"].ToString());
                    dev.Dispose();
                }
                return;
            }
            catch (PluxDotNet.Exception.PluxException e)
            {
                System.Console.WriteLine("Exception: {0}", e.Message);
                if (e.Message.Contains("lost"))
                {
                    _detectDevices();
                }
            }
        }
    }
}
