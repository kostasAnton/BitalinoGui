using System;
using SharpDX.DirectInput;

namespace BitalinoGui
{
    /*
     A joystick class which represents a joystick which will be used for the annotation of bitalino's data
         */
    public class Joystick:SharpDX.DirectInput.Joystick
    {
        private String productName;
        private int maximum_Value=0;
        public Joystick(Guid instanceGuid,DirectInput di,String productName):base(di,instanceGuid)
        {
            this.productName = productName;
        }

        public void setProductName(String productName)
        {
            this.productName = productName;
        }

        public String getProductName()
        {
            return this.productName;
        }
        /*
         Returns the current joystick's coordinates(X,Y) 
             */
        public int[] getAxisXY()
        {
            int[] currentAxisTable = new int[2];
            var state = this.GetCurrentState();
            currentAxisTable[0] = state.X;
            currentAxisTable[1] = adjustX_Axis(state.Y);
            return currentAxisTable;
        }

        public int getMaximumValue()
        {
            return maximum_Value;
        }

        public void setMaximumValue(int max)
        {
            maximum_Value = max;
        }
        public String toString()
        {
            return productName;
        }

        private int adjustX_Axis(int Y)
        {
            int adjsusted_Y = 0;
            if (Y > 32767)
            {
                adjsusted_Y = Y - 2 * (Y - 32767);
                return adjsusted_Y;
            }
            else if (Y < 32767)
            {
                adjsusted_Y = Y + 2 * (32767-Y);
                return adjsusted_Y;
            }
            else
            {
                return Y;
            }
        }
    }
}
