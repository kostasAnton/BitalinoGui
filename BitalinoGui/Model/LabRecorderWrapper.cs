﻿using LSL;
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

        private liblsl.channel_format_t lslChannelFormat; // Stream Variable Format

        public LabRecorderWrapper(int channelCount,int freq,string lslStreamName,string lslStreamType,string guid, liblsl.channel_format_t format)
        {
            this.lslChannelCount = channelCount;
            this.sampling_rate = freq;
            this.lslStreamName = lslStreamName;
            this.lslStreamType = lslStreamType;
            this.guid = guid;
            this.lslChannelFormat = format;
        }

        public LabRecorderWrapper(int channelCount,string lslStreamName, string lslStreamType, string guid, liblsl.channel_format_t format)
        {
            this.lslChannelCount = channelCount;
            this.lslStreamName = lslStreamName;
            this.lslStreamType = lslStreamType;
            this.guid = guid;
            this.lslChannelFormat = format;
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
        public void push(int[,] sample,double tstamp)
        {
            //lslOutlet.push_chunk(sample);
            lslOutlet.push_chunk(sample,tstamp);
        }

        public void push(string[] sample,double tstamp)
        {
            lslOutlet.push_sample(sample);
        }

    }
}
