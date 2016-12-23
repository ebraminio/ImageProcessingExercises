using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ImageProcessing1
{
    public partial class MainForm : Form
    {
        HistogramDialog _histogramDialog;
        Action<string> _showExtraInfoCallback;
        String fileName;
        public MainForm()
        {
            InitializeComponent();

            _histogramDialog = new HistogramDialog();

            var images = Directory.GetFiles(".", "*.png");
            if (images.Length != 0)
                OpenImage(fileName = images.First());

            _showExtraInfoCallback = str => Text = str;
        }
        
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                OpenImage(fileName = openFileDialog.FileName);
        }
        
        byte[,] _inputImage;
        private void OpenImage(string fileName)
        {
            _inputImage = new DirectBitmap(Image.FromFile(fileName)).AsArray();
            inputPictureBox.Image = new DirectBitmap(_inputImage).Bitmap;
            SetImage(_inputImage);
        }

        private void threshouldToolStripMenuItem_Click(object sender, EventArgs e) =>
            new TrackDialog(this, value => ApplyThreshould(value)).Show();

        private void logarithmicToolStripMenuItem_Click(object sender, EventArgs e) =>
            new TrackDialog(this, value => ApplyLogarithmic(value)).Show();

        private void filtersToolStripMenuItem_Click(object sender, EventArgs e) =>
            new FiltersDialog(this).Show();

        private void morphologyToolStripMenuItem_Click(object sender, EventArgs e) =>
            new MorphologyDialog(this).Show();

        private void closeToolStripMenuItem_Click(object sender, EventArgs e) =>
            Application.Exit();

        int lastThreshould = 127;

        public void ApplyThreshould(int value) =>
            outputPictureBox.Image = new DirectBitmap(_inputImage.Threshould(lastThreshould = value)).Bitmap;

        public void ApplyLogarithmic(int value) =>
            outputPictureBox.Image = new DirectBitmap(_inputImage.Logarithmic(value)).Bitmap;

        public void ApplyFilter(float[,] filter, double factor, double bias) =>
            SetImage(_inputImage.Convolution(filter, factor, bias));

        public void ApplyDilation(bool[,] filter) =>
            SetImage(_inputImage.Threshould(lastThreshould).Dilation(filter));

        public void ApplyErosion(bool[,] filter) =>
            SetImage(_inputImage.Threshould(lastThreshould).Erosion(filter));

        public void ApplyOpening(bool[,] filter) =>
            SetImage(_inputImage.Threshould(lastThreshould).Erosion(filter).Dilation(filter));

        public void ApplyClosing(bool[,] filter) =>
            SetImage(_inputImage.Threshould(lastThreshould).Dilation(filter).Erosion(filter));

        private void equalizationToolStripMenuItem_Click(object sender, EventArgs e) =>
            SetImage(_inputImage.HistogramEqualization());

        private void colorConnectedComponentsToolStripMenuItem_Click(object sender, EventArgs e) =>
            SetImage(_inputImage.Threshould(lastThreshould).ColorConnectedComponents(_showExtraInfoCallback));

        private void glcmToolStripMenuItem_Click(object sender, EventArgs e) =>
            SetImage(_inputImage.GLCM(_showExtraInfoCallback));

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e) =>
            _histogramDialog.Show();

        public void SetImage(int[,] image) =>
            outputPictureBox.Image = new DirectBitmap(image).Bitmap;

        public void SetImage(byte[,] image)
        {
            outputPictureBox.Image = new DirectBitmap(image).Bitmap;
            _histogramDialog.Display(image.Histogram());
        }
    }
    
}
