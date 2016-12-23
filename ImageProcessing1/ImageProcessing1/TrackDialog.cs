using System;
using System.Windows.Forms;

namespace ImageProcessing1
{
    public partial class TrackDialog : Form
    {
        readonly MainForm _mainForm;
        readonly Action<int> _action;
        public TrackDialog(MainForm mainForm, Action<int> action)
        {
            InitializeComponent();
            _mainForm = mainForm;
            _action = action;
        }

        private void threshouldTrackBar_Scroll(object sender, EventArgs e) =>
            _action(threshouldTrackBar.Value);
    }
}
