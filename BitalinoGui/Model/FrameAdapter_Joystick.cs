using System;
using System.Drawing;

namespace BitalinoGui.Model
{
    /*
     Instances of this class are the combined represantation of joystick and bitalino devices
    */
    class FrameAdapter_Joystick
    {
        private Frame bitalinoFrame;
        private int[] joystickAxisXY=new int[2];

        public FrameAdapter_Joystick(Frame bitalinoFrame)
        {
            this.bitalinoFrame = bitalinoFrame;
        }

        public void setBitalinoFrame(Frame bitalinoFrame)
        {
            this.bitalinoFrame = bitalinoFrame;
        }

        public void setJoystickAxisXY(int[] joystickAxisXY)
        {
            this.joystickAxisXY = joystickAxisXY;
        }

        public int[] getJoystickAxisXY()
        {
            return this.joystickAxisXY;
        }

        public int getX_axis()
        {
            return this.joystickAxisXY[0];
        }

        public int getY_axis()
        {
            return this.joystickAxisXY[1];
        }

        public Frame getBitalinoFrame()
        {
            return this.bitalinoFrame;
        }

        public String toSrting()
        {
            String frameAdatper_string_representation = bitalinoFrame.toString();
            for (int i=0; i<joystickAxisXY.Length;i++)
            {
                frameAdatper_string_representation += Convert.ToString(joystickAxisXY[i])+"\t";
            }
            return frameAdatper_string_representation;
        }



    }
}
