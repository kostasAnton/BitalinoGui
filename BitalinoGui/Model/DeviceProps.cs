using System;
namespace BitalinoGui
{
    //this class displays the properties of a bitalino device.
    class DeviceProps
    {
        private String description;
        private String firmwareVersion;
        private String mac;
        private String productID;
        private String battery;
        public DeviceProps(String description,String firmwareVersion,String mac, String productID,String battery)
        {
            this.description = description;
            this.firmwareVersion = firmwareVersion;
            this.mac = mac;
            this.productID = productID;
            this.battery = battery;
        }

        public void setBattery(String battery)
        {
            this.battery = battery;
        }

        public String getBattery()
        {
            return this.battery;
        }

        public String getDescription()
        {
            return this.description;
        }

        public String getFirmware()
        {
            return this.firmwareVersion;
        }

        public String getMac()
        {
            return this.mac;
        }

        public String getProductID()
        {
            return this.productID;
        }

        public String toString()
        {
            return "Description:" + this.description + System.Environment.NewLine+"Firmware Version:"+this.firmwareVersion
                +System.Environment.NewLine+"Mac adress:"+this.mac+System.Environment.NewLine+"ProductID:"+this.productID+System.Environment.NewLine+"Battery(%)"+this.battery;
        }

    }
}
