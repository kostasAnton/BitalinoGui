using LSL;
namespace BitalinoGui
{
    class LabRecorderWrapper
    {
        /**
        * Identifying Variables: Process ID; Stream Name; Type of Data; Sampling Rate
        * **/
        private string guid; // Unique Process ID -- Pre-Generated

        private string lslStreamName ;
        private string lslStreamType ;
        private int sampling_rate = 0; // Default Value

        private liblsl.StreamInfo lslStreamInfo;//the information of instance's stream
        private liblsl.StreamOutlet lslOutlet = null; // The Streaming Outlet
        private int lslChannelCount = 0; // Number of Channels to Stream by Default

        private const liblsl.channel_format_t lslChannelFormat = liblsl.channel_format_t.cf_int16; // Stream Variable Format

        public LabRecorderWrapper(int channelCount,int freq,string lslStreamName,string lslStreamType,string guid)
        {
            this.lslChannelCount = channelCount;
            this.sampling_rate = freq;
            this.lslStreamName = lslStreamName;
            this.lslStreamType = lslStreamType;
            this.guid = guid;
        }

        public LabRecorderWrapper(int channelCount,string lslStreamName, string lslStreamType, string guid)
        {
            this.lslChannelCount = channelCount;
            this.lslStreamName = lslStreamName;
            this.lslStreamType = lslStreamType;
            this.guid = guid;
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
        public void push(int[,] sample,double clock)
        {
            lslOutlet.push_chunk(sample);
        }

        public void push(string[] sample)
        {
            lslOutlet.push_sample(sample,liblsl.local_clock());
        }

    }
}
