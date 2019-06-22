using LSL;
namespace BitalinoGui
{
    class LabRecorderWrapper
    {
        /**
        * Identifying Variables: Process ID; Stream Name; Type of Data; Sampling Rate
        * **/
        private const string guid = "98FF4C8E-5C2D-42E9-8F1B-8505643EAC2C"; // Unique Process ID -- Pre-Generated

        private string lslStreamName = "Bitalino Streamer";
        private string lslStreamType = "Physiological-Signals";
        private int sampling_rate = 0; // Default Value

        private liblsl.StreamInfo lslStreamInfo;//the information of instance's stream
        private liblsl.StreamOutlet lslOutlet = null; // The Streaming Outlet
        private int lslChannelCount = 0; // Number of Channels to Stream by Default

        private const liblsl.channel_format_t lslChannelFormat = liblsl.channel_format_t.cf_int16; // Stream Variable Format

        public LabRecorderWrapper(int channelCount,int freq)
        {
            this.lslChannelCount = channelCount;
            this.sampling_rate = freq;
        }


        public void LinkLabStreamingLayer()
        {
            if (lslOutlet == null)
            {
                // linking the stream to lab recorder
                lslStreamInfo = new liblsl.StreamInfo(lslStreamName, lslStreamType, lslChannelCount, sampling_rate, lslChannelFormat, guid + "-" + "P1");
                lslOutlet = new liblsl.StreamOutlet(lslStreamInfo);
            }
        }
        //a method for pushing samples of data to labrecorder
        public void push(int[,] sample)
        {
            lslOutlet.push_chunk(sample);
        }

    }
}
