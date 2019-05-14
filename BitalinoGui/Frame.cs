using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitalinoGui
{
    /*
        This Frame class is used for building instances of frames bitalino gives.  
    */
    class Frame
    {
        private int sequence_number;
        private int[] data;
        private int led;

        public Frame(int[] data,int sequence_number,bool led)
        {
            this.data = data;
            this.sequence_number = sequence_number;
            if(led)
                this.led = 1;
            else
                this.led = 0;
        }

        public void setData(int[] data)
        {
            this.data = data;
        }

        public void setLed(int led)
        {
            this.led = led;
        }

        public int[] getData()
        {
            return this.data;
        }

        public int getLed()
        {
            return this.led;
        }

        public String toString()
        {
            String dataToString = Convert.ToString(sequence_number)+"\t"+"led-state:"+Convert.ToString(led);

            foreach (int val in data)
            {
                dataToString += "\t" + Convert.ToString(val);
            }
            return dataToString;
        }
    }
}
