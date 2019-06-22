using BitalinoGui.Controller;
using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
 


namespace BitalinoGui
{
    /*
        A mighty detective who detects Bitalino devices and brings to the light their properties. 
    */
    public class DetectiveMonk
    {
        private DetectiveController graph_controller;
        public DetectiveMonk(DetectiveController graphController)
        {
            this.graph_controller = graphController;
        }
        /*
            This method detects bitalino devices after the sensor form has been loaded. We use it to refresh the 
            device list the user sees.Note that this method search devices on a seperate thread 
        */
        public void detectionThread()
        {
            ThreadStart start_detect = new ThreadStart(detectDevices);
            Thread detectiveMonk = new Thread(start_detect);
            detectiveMonk.Start();
        }
        
        private void detectDevices()
        {
            try
            {
                joystickDetection(true);
                List<PluxDotNet.DevInfo> devs = PluxDotNet.BaseDev.FindDevices();
                foreach (PluxDotNet.DevInfo devInfo in devs)
                {
                    System.Console.WriteLine("{0} - {1}", devInfo.path, devInfo.description);
                    MyDevice dev = new MyDevice(devInfo.path);
                    Dictionary<string, object> props = dev.GetProperties();
                    Storekeeper.addDeviceProps(new DeviceProps(props["description"].ToString(), props["fwVersion"].ToString(),
                        props["path"].ToString(), props["productID"].ToString(), Convert.ToString(dev.GetBattery())));
                   
                    graph_controller.updateMacListBox(props["path"].ToString());
                    
                    dev.Dispose();
                }

                graph_controller.updateCursor_Form();
                
                return;
            }
            catch (PluxDotNet.Exception.PluxException e)
            {
                System.Console.WriteLine("Exception: {0}", e.Message);

                Storekeeper.emptyDevicesList();
                graph_controller.update_Empty_Mac_Joystick();
                if (e.Message.Contains("lost"))
                {
                    detectDevices();
                }
            }
        }
        /*
            A method that detect bitalino device synchronously on the current thread.
        */
        public void _detectDevices()
        {
            try
            {
                joystickDetection(false);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            try
            {
                List<PluxDotNet.DevInfo> devs = PluxDotNet.BaseDev.FindDevices();
                foreach (PluxDotNet.DevInfo devInfo in devs)
                {
                    System.Console.WriteLine("{0} - {1}", devInfo.path, devInfo.description);
                    graph_controller.reportToSplash("Detected:" + devInfo.path + "\t" + devInfo.description);
                    MyDevice dev = new MyDevice(devInfo.path);
                    Dictionary<string, object> props = dev.GetProperties();
                    Storekeeper.addDeviceProps(new DeviceProps(props["description"].ToString(), props["fwVersion"].ToString(),
                        props["path"].ToString(), props["productID"].ToString(), Convert.ToString(dev.GetBattery())));
                    
                    graph_controller.fillSyncMacListBox(props["path"].ToString());
                   
                    dev.Dispose();
                }
                return;
            }
            catch (PluxDotNet.Exception.PluxException e)
            {
                System.Console.WriteLine("Exception: {0}", e.Message);
                Storekeeper.emptyDevicesList();
                Storekeeper.emptyJoysticks();
                graph_controller.clearMac_Joystick_ListBoxes();
                graph_controller.reportToSplash(e.Message);
                _detectDevices();
            }
        }
        /*
           Usinghthe sharpDX library to detect the availble joysticks which are connected to pc
        */
        private void joystickDetection(bool threaded)
        {

            var directInput = new DirectInput();
            foreach (var deviceInstance in directInput.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AllDevices))
            {
                Storekeeper.addJoystick(new Joystick(deviceInstance.InstanceGuid, directInput, deviceInstance.ProductName));
                Console.WriteLine(deviceInstance.InstanceGuid.ToString() + "--" + deviceInstance.ProductName.ToString());
                if (threaded)
                {
                    graph_controller.updatJoystickListBox(new Joystick(deviceInstance.InstanceGuid, directInput, deviceInstance.ProductName).toString());
                }
                else
                {
                    graph_controller.fillSyncJoystickListBox(new Joystick(deviceInstance.InstanceGuid, directInput, deviceInstance.ProductName).toString());

                }
            }
        }
    }
}
