using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private liblsl.StreamInfo lslStreamInfo;
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
                // This is How I Link the Output Stream!
                lslStreamInfo = new liblsl.StreamInfo(lslStreamName, lslStreamType, lslChannelCount, sampling_rate, lslChannelFormat, guid + "-" + "P1");
                lslOutlet = new liblsl.StreamOutlet(lslStreamInfo);
            }
        }

        public void push(int[,] sample)
        {
            lslOutlet.push_chunk(sample);
        }

    }
}
