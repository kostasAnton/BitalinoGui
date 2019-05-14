using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Globalization;
using System.Collections;
using System.Threading;

namespace BitalinoGui
{
    class MyDevice : PluxDotNet.SignalsDev
    {

        //Varaibles for Device-Bitalino
        private int freq;//sample rate
        private BackgroundWorker bw;//background to report the progress on the GUI
        private List<System.Int32> channels = new List<System.Int32>();//A list of channels from which we will record data from bitalono
        private bool ledLight = false;//led light field will be edited run time(ON-LIGHT UP/OFF-LIGHT OFF)
        private int countLights_Up = 0;//how many times we have lighted up the led?
        private ArrayList exportValues = new ArrayList();
        //Lab recorder varaibles
        private int[,] sample;//send samples by 100 to labrecorder
        private int prevSeq = -1; int manualCounter = 0;//varaibles for passing values to sample table
        private int countSample = 0;//the counter of array sample
        LabRecorderWrapper lrWrapper;//the responsible wrapper for passing data to labrecorder
        //a standar header for exporting the output of the sensor.
        private static String header = "#HEADER:(COLLUMN1:Seq_number)-(COLLUMN2:O1)-(COLLUMN3:A1)-(COLLUMN4:A2)-(COLLUMN5:A3)-(COLLUMN6:A4)-(COLLUMN7:A5)-(COLLUMN8:A6)";
        public MyDevice(string path, BackgroundWorker bw, List<System.Int32> channels, int freq) : base(path)
        {
            this.freq = freq;
            this.bw = bw;
            this.channels = channels;
            this.lrWrapper = new LabRecorderWrapper(channels.Count, freq);
            lrWrapper.LinkLabStreamingLayer();
            sample = new int[100, channels.Count];
        }
        public MyDevice(string path, BackgroundWorker bw) : base(path)
        {
            this.bw = bw;
        }

        public override bool OnRawFrame(int nSeq, int[] data)
        {
            int counterChannels = 0;
            System.Console.Write("{0} -", nSeq);
            exportValues.Add(new Frame(data,nSeq ,getLedLight()));
            Frame frame = new Frame(data,nSeq,getLedLight());
           bw.ReportProgress(-100, frame);
           // Thread.Sleep(120);
            foreach (int val in data)
            {
               System.Console.Write("{0}", val);
                if (bw.CancellationPending)
                {
                    this.Stop();
                    this.Dispose();
                    return true;
                }
                //the following section of  "if" places the values of bitalino sensor in order to send them
                //to lab recorder for synchronization
                if (countSample < 100)
                {
                    if (prevSeq == nSeq)
                    {
                        manualCounter++;
                        sample[countSample, manualCounter] = val;
                        countSample++;
                    }
                    else
                    {
                        manualCounter = 0;
                        prevSeq = nSeq;
                        sample[countSample, 0] = val;
                    }
                }
                else
                {
                    this.lrWrapper.push(sample);
                    countSample = 0;
                }

                //bw.ReportProgress(nSeq, val);
                counterChannels++;
                System.Console.WriteLine();
            }
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
                System.Console.WriteLine("Digital state event(upd)" + evtDigitalUpd.ToString());

                return false;
            }
            System.Console.WriteLine(evt);
            return false;
        }

        // we use interupts for lighting up the led.
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

        public int getFreq()
        {
            return freq;
        }

        public int getCountLights()
        {
            return this.countLights_Up;
        }

        public ArrayList getExportValues()
        {
            return this.exportValues;
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

    }
}
    

