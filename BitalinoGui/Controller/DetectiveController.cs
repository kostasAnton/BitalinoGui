using System;
using System.Threading;
using System.Windows.Forms;
using BitalinoGui.View;
namespace BitalinoGui.Controller
{
    public class DetectiveController
    {   /*
            This class is the controller for detective monk class.It is used for inreractions
            between the detective and the graphical user interface that is presented to the user.
         */
        private SensorForm form;
        private splashForm _form;
        private DetectiveMonk monk;

        public DetectiveController(SensorForm form,splashForm _form)
        {
            this.form = form;
            this._form = _form;
            monk = new DetectiveMonk(this);
        }
        //this section just fills the corresponding components synchronously
        public void fillSyncMacListBox(String item)
        { 
            form.fillMacListbox(item);
        }

        public void clearMac_Joystick_ListBoxes()
        {
            form.clearMac_Joystick_ListBox();
        }

        public void fillSyncJoystickListBox(String item)
        {
            form.fillJoystickListbox(item);
        }
        //this section corresponds to methods for threads which need to update the GUI 
        public void updateMacListBox(String item)
        {
            this.form.Invoke(
            (MethodInvoker)delegate
            {
                form.fillMacListbox(item);
            }
            );
        }

        public void updateCursor_Form()
        {
            this.form.Invoke(
            (MethodInvoker)delegate
            {
                form.Cursor = Cursors.Default;
            }
            );
        }

        public void updatJoystickListBox(String item)
        {
            this.form.Invoke(
            (MethodInvoker)delegate
            {
                form.fillJoystickListbox(item);
            }
            );
        }
        public void update_Empty_Mac_Joystick()
        {
            this.form.Invoke(
            (MethodInvoker)delegate
            {

                form.clearMac_Joystick_ListBox();
            }
            );
        }

        //Detection methods of controller
        public void refreshDevicesList()
        {
            monk.detectionThread();
        }

        public void initialDetection()
        {
            ThreadStart splashIt = new ThreadStart(splash_Brother_Start);
            Thread splashThread = new Thread(splashIt);
            splashThread.Start();
            monk._detectDevices();
            Application.Exit();
            //splashThread.Abort();
        }

        //this section relates to methods for displaying and updating the splash form
        public void reportToSplash(String error)
        {
            _form.Invoke
            (
                (MethodInvoker)delegate
                {
                    _form.addError(error);
                }
            );
        }

        private void splash_Brother_Start()
        {
            Application.Run(_form);
        }

        public DetectiveMonk getDetectiveMonk()
        {
            return this.monk;
        }

    }
}
