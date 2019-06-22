using BitalinoGui.Controller;
using System;
using System.Threading;
using System.Windows.Forms;

namespace BitalinoGui
{
    public partial class splashForm : Form
    {
        private DetectiveController controller;
        public splashForm(DetectiveController controller)
        {
            this.controller = controller;
            InitializeComponent();
        }
        public splashForm()
        {
            InitializeComponent();
        }

        public void addError(String error)
        {
            errorListBox.Items.Add(error);
        }

        public void setController(DetectiveController controller)
        {
            this.controller = controller;
        }

        public DetectiveController getController()
        {
            return this.controller;
        }

     
    }
}

