using System;
using System.Linq;
using System.Windows.Forms;

namespace ImageProcessing1
{
    public partial class FiltersDialog : Form
    {
        readonly MainForm _mainForm;
        public FiltersDialog(MainForm mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;
        }

        private void blurButton_Click(object sender, EventArgs e)
        {
            factorTextBox.Text = @"1.0";
            biasTextBox.Text = @"0.0";
            transformMatrixTextBox.Text = @"
   0.0, 0.2,  0.0,
   0.2, 0.2,  0.2,
   0.0, 0.2,  0.0
";
            Apply();
        }

        private void motionBlurButton_Click(object sender, EventArgs e)
        {
            factorTextBox.Text = @"0.1";
            biasTextBox.Text = @"0.0";
            transformMatrixTextBox.Text = @"
  1, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 1, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 1, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 1, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 1, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 1, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 1, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 1, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 1,
";
            Apply();
        }

        private void findEdgesButton_Click(object sender, EventArgs e)
        {
            factorTextBox.Text = @"1.0";
            biasTextBox.Text = @"0.0";
            transformMatrixTextBox.Text = @"
   -1,  0,  0,  0,  0,
   0, -2,  0,  0,  0,
   0,  0,  6,  0,  0,
   0,  0,  0, -2,  0,
   0,  0,  0,  0, -1,
";
            Apply();
        }

        private void sharpenButton_Click(object sender, EventArgs e)
        {
            factorTextBox.Text = @"1.0";
            biasTextBox.Text = @"0.0";
            transformMatrixTextBox.Text = @"
   1,  1,  1,
   1, -7,  1,
   1,  1,  1
";
            Apply();
        }

        private void embossButton_Click(object sender, EventArgs e)
        {
            factorTextBox.Text = @"1.0";
            biasTextBox.Text = @"128.0";
            transformMatrixTextBox.Text = @"
  -1, -1,  0,
  -1,  0,  1,
   0,  1,  1
";
            Apply();
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            Apply();
        }

        private void Apply()
        {
            float[,] filter;
            double factor;
            double bias;
            try
            {
                var rawMatrix = transformMatrixTextBox.Text.Split('\n', ' ', ',', '\r')
                    .Where(x => !String.IsNullOrEmpty(x)).Select(float.Parse).ToList();
                var filterSize = (int)Math.Sqrt(rawMatrix.Count);
                if (filterSize * filterSize != rawMatrix.Count) return; // return if is not square
                filter = new float[filterSize, filterSize];
                for (int i = 0; i < filterSize; ++i)
                    for (int j = 0; j < filterSize; ++j)
                        filter[i, j] = rawMatrix[i + filterSize * j];

                factor = float.Parse(factorTextBox.Text);
                bias = float.Parse(biasTextBox.Text);
            }
            catch { return; } // just nvm

            _mainForm?.ApplyFilter(filter, factor, bias);
        }

        private void FiltersDialog_Load(object sender, EventArgs e)
        {

        }

        private void laplacianButton_Click(object sender, EventArgs e)
        {
            factorTextBox.Text = @"3.0";
            biasTextBox.Text = @"40.0";
            transformMatrixTextBox.Text = @"
0, 1, 0
1, -4, 1
0, 1, 0
";
            Apply();
        }
    }
}
