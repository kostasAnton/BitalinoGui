using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BitalinoGui.Model;
using Intel.RealSense;

namespace BitalinoGui.Controller
{
    public class CameraController
    {
        SensorForm sensorForm;
        IntelRealSence_Cam camera;
        bool conected_camera = true;
        ArrayList recordedBitamps = new ArrayList();
        public CameraController(SensorForm sensorForm)
        {
            this.sensorForm = sensorForm;
            this.camera = new IntelRealSence_Cam(this);
        }

        public void loopCamera_Frames()
        {
            camera.startRecordingProcess();
        }
       

        public void cancelCameraRecording()
        {
            camera.getCancelToken().Cancel();
        }
        
        public void uploadBitmap(Bitmap bitmap,bool rgb)
        {
            if (rgb)
            {
                sensorForm.Invoke
                (
                    (MethodInvoker)delegate
                    {
                        sensorForm.setPictureBoxRgb(bitmap);
                    }
                );
            }
            else
            {
                sensorForm.Invoke
                (
                    (MethodInvoker)delegate
                    {
                        sensorForm.setPictureBoxDepth(bitmap);
                    }
                );
            }
        }

        public void setConnected_Camera_Flag(bool connectedState)
        {
            this.conected_camera = connectedState;
        }

        public bool getConnectedState()
        {
            return this.conected_camera;
        }

        public IntelRealSence_Cam getCamera()
        {
            return this.camera;
        }

      
        public ArrayList getRecorderBitamaps()
        {
            return this.recordedBitamps;
        }

        public void setrecordedBitamps(ArrayList recordedBitamps)
        {
            this.recordedBitamps = recordedBitamps;
        }

    }
}
