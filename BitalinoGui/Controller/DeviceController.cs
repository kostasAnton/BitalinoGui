using BitalinoGui.Model;
using System;
using System.Collections;
using System.ComponentModel;

namespace BitalinoGui.Controller
{
    class DeviceController
    {
        private MyDevice device;
        private BackgroundWorker worker;
        private ArrayList exportValues = new ArrayList();
        public DeviceController(MyDevice device,BackgroundWorker worker)
        {
            this.device = device;
            this.worker = worker;
            device.setController(this);
        }

        
        public void setDevice(MyDevice device)
        {
            this.device = device;
        }

        public MyDevice getDevice()
        {
            return this.device;
        }

        public BackgroundWorker getWorker()
        {
            return this.worker;
        }

        /*
     This method starts the message loop of bitalino in order to obtain the data the sensor gives
    */
        public void loop()
        {
            try
            {
                device.Start(device.getFreq(), device.getChannelsList(), 16);
                this.device.Loop();
            }
            catch (Exception exc)
            {
                Console.WriteLine("Exc:" + exc.Message + " --on device worker do work");
            }
        }

        public void reportProgress(FrameAdapter_Joystick frame)
        {
            exportValues.Add(frame);
            worker.ReportProgress(-100, frame);
        }

        public void stopDevice()
        {
            worker.CancelAsync();
        }

        public void sendInterrupt(bool state)
        {
            device.Interrupt(state);
        }

        public ArrayList getExportValues()
        {
            return this.exportValues;
        }

        public void setJoystickGuid(Joystick joystick)
        {
            device.setJoystickGuid(joystick);
        }
    }
}
