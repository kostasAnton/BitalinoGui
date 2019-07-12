using Intel.RealSense;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BitalinoGui.Controller;
using LSL;
using Accord.Video.FFMPEG;

namespace BitalinoGui.Model
{
    public class IntelRealSence_Cam
    {
        //unique process id for lab recorder
        private string guid="98CC4F8E-5C2D-42E2-8F1B-8505643EAD3D";
    
        // Intel RealSense Variables
        private Pipeline pipeline;
        private Colorizer colorizer;
        private CancellationTokenSource tokenSource = new CancellationTokenSource();

        private CustomProcessingBlock processBlock;
        private CameraController camera_controller;
        string save_path="";
        LabRecorderWrapper camera_labWrapper;


        private VideoFileWriter vidWriter_Depth;
        private VideoFileWriter vidWriter_Color;

        public IntelRealSence_Cam(CameraController cameraController)
        {
            this.camera_controller = cameraController;
            this.camera_labWrapper = new LabRecorderWrapper(2,"IntelRealSense Camera","Bitmaps",guid);
            camera_labWrapper.LinkLabStreamingLayer();
        }

        public void startRecordingProcess()
        {
            try
            {
                pipeline = new Pipeline();
                colorizer = new Colorizer();

                var cfg = new Config();
                cfg.EnableStream(Stream.Depth, 640, 480, Format.Z16, 60);
                cfg.EnableStream(Stream.Color, 640, 480, Format.Bgr8, 60);
                applyRecordingConfig();
                pipeline.Start(cfg);
                
                processBlock = new CustomProcessingBlock((f, src) =>
                {
                    using (var releaser = new FramesReleaser())
                    {
                        var frames = FrameSet.FromFrame(f, releaser);

                        VideoFrame depth = FramesReleaser.ScopedReturn(releaser, frames.DepthFrame);
                        VideoFrame color = FramesReleaser.ScopedReturn(releaser, frames.ColorFrame);

                        var res = src.AllocateCompositeFrame(releaser, depth, color);

                        src.FramesReady(res);
                    }
                });

                processBlock.Start(f =>
                {
                    using (var releaser = new FramesReleaser())
                    {
                        var frames = FrameSet.FromFrame(f, releaser);

                        var depth_frame = FramesReleaser.ScopedReturn(releaser, frames.DepthFrame);
                        var color_frame = FramesReleaser.ScopedReturn(releaser, frames.ColorFrame);
                        var colorized_depth = colorizer.Colorize(depth_frame);
                        
                        Bitmap bmpColor = new Bitmap(color_frame.Width, color_frame.Height, color_frame.Stride, System.Drawing.Imaging.PixelFormat.Format24bppRgb, color_frame.Data);
                        vidWriter_Color.WriteVideoFrame(bmpColor);

                        Bitmap bmpDepth = new Bitmap(colorized_depth.Width, colorized_depth.Height, colorized_depth.Stride, System.Drawing.Imaging.PixelFormat.Format24bppRgb, colorized_depth.Data);
                        vidWriter_Depth.WriteVideoFrame(bmpDepth);
                        
                        Console.WriteLine(Convert.ToString("Intel frame numeric:"+color_frame.Number));
                        // camera_controller.getRecorderBitamaps().Add(bmpDepth);
                        camera_controller.uploadBitmap(bmpColor,true);
                        camera_controller.uploadBitmap(bmpDepth, false);
                        String[] sample = new String[2];
                        //sample[0] = "" + Convert.ToString(colorized_depth.Number) + "_" + Convert.ToString(colorized_depth.Timestamp);
                        //sample[1] = "" + Convert.ToString(color_frame.Number) + "_" + Convert.ToString(color_frame.Timestamp);

                        sample[0] = Convert.ToString(colorized_depth.Number);
                        sample[1] = Convert.ToString(color_frame.Number);

                        camera_labWrapper.push(sample);
                    }
                });

                
                var token = tokenSource.Token;
                
                var t = Task.Factory.StartNew(() =>
                {
                    // Main Loop -- 
                    while (!token.IsCancellationRequested)
                    {
                        using (var frames = pipeline.WaitForFrames())
                        {
                            processBlock.ProcessFrames(frames);
                        }
                    }

                }, token);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                camera_controller.setConnected_Camera_Flag(false);
                
            }
            
        }

       
        public CancellationTokenSource getCancelToken()
        {
            return this.tokenSource;
        }

        public void setSavePath(String save_path)
        {
            this.save_path = save_path;
        }

        public string getSavePath()
        {
            return this.save_path;
        }


        private void applyRecordingConfig()
        {
                vidWriter_Depth = new VideoFileWriter();
                vidWriter_Depth.Width = 640;
                vidWriter_Depth.Height = 480;
                vidWriter_Depth.VideoCodec = VideoCodec.H264;
                //vidWriter_Depth.VideoOptions["crf"] = "17";
                //vidWriter_Depth.VideoOptions["preset"] = "ultrafast";
                vidWriter_Depth.Open(save_path+"_Depth.avi");

                vidWriter_Color = new VideoFileWriter();
                vidWriter_Color.Width = 640;
                vidWriter_Color.Height = 480;
                vidWriter_Color.VideoCodec = VideoCodec.H264;
                //vidWriter_Color.VideoOptions["crf"] = "17";
                //vidWriter_Color.VideoOptions["preset"] = "ultrafast";
                vidWriter_Color.Open(save_path+"_Color.avi");
            
        }
    }
}
