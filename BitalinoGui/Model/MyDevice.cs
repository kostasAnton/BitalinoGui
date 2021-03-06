﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using BitalinoGui.Model;
using BitalinoGui.Controller;
using System.Drawing;
using LSL;
using System.Globalization;

namespace BitalinoGui
{
    class MyDevice : PluxDotNet.SignalsDev
    {/*
        This is a custom device which consists of bitalino and joystick. The recording of bitalino and the use
        of jiystick are independable events.User can use only biltaino for recording data or combine bitalino's
        recording with real time annotaion of data with a joystick.
        */
        //unique proceess id for lab recorder
        private string guid = "98FF4C8E-5C2D-42E9-8F1B-8505643EAC2C";
        //Varaibles for Device-Bitalino
        private int freq;//sample rate
        private DeviceController controller;//background to report the progress on the GUI
        private List<System.Int32> channels = new List<System.Int32>();//A list of channels from which we will record data from bitalono
        private bool ledLight = false;//led light field will be edited run time(ON-LIGHT UP/OFF-LIGHT OFF)
        private int countLights_Up = 0;//how many times we have lighted up the led?
        private ArrayList exportValues = new ArrayList();
        //**************************************************************************************************
        //Lab recorder varaibles
        private int[,] sample;//send samples by 100 to labrecorder
        private int prevSeq = -1; int manualCounter = 0;//varaibles for passing values to sample table
        private int countSample = 0;//the counter of array sample
        LabRecorderWrapper lrWrapper;//the responsible wrapper for passing data to labrecorder
        

        //a standar header for exporting the output of the sensor.
        private static String header = "#HEADER:(COLLUMN1:Seq_number)-(COLLUMN2:O1)-(COLLUMN3:A1)-(COLLUMN4:A2)-(COLLUMN5:A3)-(COLLUMN6:A4)-(COLLUMN7:A5)-(COLLUMN8:A6)";
        
        //***************************************************************************************************
        //Joystick section
        private Joystick connectedJoystick;
        private CameraController camcontroller;
        public MyDevice(string path, List<System.Int32> channels, int freq) : base(path)
        {
            this.freq = freq;
            this.channels = channels;
            this.lrWrapper = new LabRecorderWrapper(channels.Count+3, freq,"Bitalino Sensor","Physiological Signals",guid,liblsl.channel_format_t.cf_int32);
            lrWrapper.LinkLabStreamingLayer();
            //we are pushing samples of bitalino per hundred(100).
            //100 rows and number of collumns = number of selected channels+(joystick's axis X and joystick's axis Y and the O1 state)
            sample = new int[100,channels.Count+3];
        }

        //this constructor produces instances for detective monk who needs just the macAdress of device.
        public MyDevice(string path) : base(path)
        {
           
        }
        /*
         This event occurs when a new bitalino frame is obtained.
             */
        double clockStart;
        public override bool OnRawFrame(int nSeq, int[] data)
        {
            System.Console.Write("{0} -", nSeq);
            FrameAdapter_Joystick frame = new FrameAdapter_Joystick(new Frame(data, nSeq, getLedLight()));
            //assign the O1 state to the sample before procced  with further installation of the sample
            if (connectedJoystick != null)
            {
                connectedJoystick.Acquire();
                int[] axisTable = connectedJoystick.getAxisXY();
                if (countSample<100)
                { 
                    sample[countSample,channels.Count+1] = axisTable[1];
                    sample[countSample,channels.Count] = axisTable[0];
                }
                frame.setJoystickAxisXY(axisTable);
            }
            controller.reportProgress(frame);
            //get start time of chunk proccesing.
            if (countSample == 0)
            {
                clockStart = liblsl.local_clock();
            }
            for (int i= 0; i< data.Length; i++)
            {
               System.Console.Write("{0}", data[i]);
                if (controller.getWorker().CancellationPending)
                {
                    this.Stop();
                    this.Dispose();
                    return true;
                }
                sample[countSample,i] = data[i];
                sample[countSample, channels.Count + 2] = Convert.ToInt32(ledLight);
               
            }
            countSample++;
            if (countSample==100)
            {
                //calculate the avg value of timestamps
                double endCLlock = liblsl.local_clock();
                double avgClock = (clockStart + endCLlock) / 2.0;
                this.lrWrapper.push(sample, avgClock);
                countSample = 0;
            }
            System.Console.WriteLine();
            return false;
        }

        public override bool OnEvent(PluxDotNet.Event.Event evt)
        {
            PluxDotNet.Event.Battery evtBat = evt as PluxDotNet.Event.Battery;
            if (evtBat != null)
            {
                System.Console.WriteLine("Battery event - voltage: {0} V ; charge remaining: {1} %",
                   evtBat.voltage, evtBat.percentage);
                return false;
            }

            PluxDotNet.Event.SignalGood evtSigGood = evt as PluxDotNet.Event.SignalGood;
            if (evtSigGood != null)
            {
                System.Console.WriteLine("Signal state event - port: {0} ; is good: {1}",
                   evtSigGood.port, evtSigGood.isGood);
                return false;
            }


            PluxDotNet.Event.DigInUpdate evtDigitalUpd = evt as PluxDotNet.Event.DigInUpdate;
            if (evtDigitalUpd != null)
            {
                System.Console.WriteLine("Digital state event(upd) {0}" + evtDigitalUpd.ToString()+"{1}");

                return false;
            }
            System.Console.WriteLine(evt);
            return false;
        }

        // we use interupts for lighting up the led
        //interrupts can be used to access bitalino in general.
        //For example you can use them if you have a button sensor connected.
        public override bool OnInterrupt(object param)
        {
            bool state = (bool)param;
            if (state == true)
            {
                countLights_Up++;
            }
            this.setLedLight(state);
            this.SetDOut(state);
          
            return false;
        }

        public void setLedLight(bool light)
        {
            this.ledLight = light;
        }

        public bool getLedLight()
        {
            return this.ledLight;
        }

        public List<System.Int32> getChannelsList()
        {
            return channels;
        }

        public void setChannelsList(List<System.Int32> channelList)
        {
            this.channels=channelList;
        }

        public int getFreq()
        {
            return freq;
        }

        public void setController(DeviceController controller)
        {
            this.controller = controller;
        }

        public void setFreq(int freq)
        {
            this.freq = freq;
        }

        public void setJoystickGuid(Joystick joystick)
        {
            this.connectedJoystick = joystick;
        }

        public int getCountLights()
        {
            return this.countLights_Up;
        }
        
        public static String getHeader()
        {
            return header;
        }
        /*
         This method starts the message loop of bitalino in order to obtain the data the sensor gives
        */
        public void loop()
        {
            try
            {
                this.Loop();
            }
            catch (Exception exc)
            {
                Console.WriteLine("Exc:" + exc.Message + " --on device worker do work");
            }
        }

        public void setCamController(CameraController camcontroller)
        {
            this.camcontroller = camcontroller;
        }

        public Joystick getJoystick()
        {
            return this.connectedJoystick;
        }
    }
}
    

