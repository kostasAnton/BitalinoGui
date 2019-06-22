using System.Collections.Generic;

namespace BitalinoGui
{
    class Storekeeper
    {
        //devices:a list of devices. We detect bitalino devices around as then we pass device's properties in that list
        //for each device we detect
        private static List<DeviceProps> devices = new List<DeviceProps>();
        //a list which saves the joysticks
        private static List<Joystick> joysticks = new List<Joystick>();
        public static void addDeviceProps(DeviceProps properties)
        {
            devices.Add(properties);
        }

        public static void addJoystick(Joystick joyStick)
        {
            joysticks.Add(joyStick);
        }

        public static void emptyDevicesList()
        {
            devices.Clear();
        }

        public static void emptyJoysticks()
        {
            devices.Clear();
        }

        public static List<DeviceProps> getDevices()
        {
            return devices;
        }

        public static List<Joystick> getJoysticks()
        {
            return joysticks;
        }

        public static Joystick returnJoystickByName(string name)
        {
            Joystick wantedJoystick=null;
            foreach (Joystick joystick in joysticks)
            {
                if (joystick.getProductName().Equals(name))
                {
                    wantedJoystick = joystick;
                    break;
                }
            }
            return wantedJoystick;
        }
    }
}
