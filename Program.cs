
using System.Collections.Generic;

public class MyDevice : PluxDotNet.SignalsDev
{
   public MyDevice(string path) : base(path) {}

   public override bool OnRawFrame(int nSeq, int[] data)
   {
      if (nSeq % freq == 0)
      {  // here once per second
         // print one frame
         System.Console.Write("{0} -", nSeq);

         foreach (int val in data)
            System.Console.Write(" {0}", val);

         System.Console.WriteLine();

         if (System.Console.KeyAvailable)
            return true;   // if a key was pressed, exit loop
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

      System.Console.WriteLine(evt);
      return false;
   }
   /*
   // only needed if timeouts are used
   public override bool OnTimeout()
   {
      return true;
   }

   // only needed if interrupts are used
   public override bool OnInterrupt(object param)
   {
      return true;
   }
   */
   public int freq;
}

class Program
{
   static void Main(string[] args)
   {
      try
      {
         // uncomment following block to search for Plux devices in range
         /*List<PluxDotNet.DevInfo> devs = PluxDotNet.BaseDev.FindDevices();
         foreach (PluxDotNet.DevInfo devInfo in devs)
            System.Console.WriteLine("{0} - {1}", devInfo.path, devInfo.description);
         return;*/

         // Example without "using" keyword - need to call Dispose() at the end
         MyDevice dev = new MyDevice("BLE88:6B:0F:94:4B:93");  // replace with your device MAC address
         Dictionary<string, object> props = dev.GetProperties();
         System.Console.WriteLine("Device description: {0}", props["description"]);

         dev.freq = 1000;  // acquisition base frequency of 1000 Hz

         PluxDotNet.Source src_emg = new PluxDotNet.Source();
         src_emg.port = 1; // EMG source port
         src_emg.freqDivisor = 20;   // divide 1000 Hz by 20 to send EMG envelope at 50 Hz
         // src_emg.chMask kept at default value of 1 (channel 1 only)
         // src_emg.nBits kept at default value of 16

         PluxDotNet.Source src_acc = new PluxDotNet.Source();
         src_acc.port = 2; // ACC source port
         src_acc.freqDivisor = 20;   // divide 1000 Hz by 20 to send ACC data at 50 Hz
         src_acc.chMask = 0x07;  // bitmask for channels 1,2,3
         // src_acc.nBits kept at default value of 16

         List<PluxDotNet.Source> srcs = new List<PluxDotNet.Source>() { src_emg, src_acc };

         dev.Start(dev.freq, srcs);  // start acquisition

         System.Console.WriteLine("Acquisition started. Press a key to stop.");

         dev.Loop(); // run message loop

         dev.Stop(); // stop acquisition
         
         dev.Dispose(); // disconnect from device

         // Example with "using" keyword - Dispose() is automatically called at the end, no need to call it explicitly
         /*using (MyDevice d = new MyDevice("00:07:80:79:6F:EF"))  // replace with your device MAC address
         {
            System.Console.WriteLine("Device description: {0}", d.GetProperties()["description"]);
         }*/
      }
      catch (PluxDotNet.Exception.PluxException e)
      {
         System.Console.WriteLine("Exception: {0}", e.Message);
      }
   }
}
