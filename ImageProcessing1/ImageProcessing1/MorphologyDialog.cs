using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImageProcessing1
{
    public partial class MorphologyDialog : Form
    {
        readonly MainForm _mainForm;
        public MorphologyDialog(MainForm mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;
        }

        private bool[,] ParseStructeringMatrix()
        {
            var rawMatrix = transformMatrixTextBox.Text.Split('\n', ' ', ',', '\r')
                .Where(x => !String.IsNullOrEmpty(x)).Select(int.Parse).ToList();
            var filterSize = (int)Math.Sqrt(rawMatrix.Count);
            var filter = new bool[filterSize, filterSize];
            for (int i = 0; i < filterSize; ++i)
                for (int j = 0; j < filterSize; ++j)
                    filter[i, j] = rawMatrix[i + filterSize * j] == 1;

            return filter;
        }

        private void dilationButton_Click(object sender, EventArgs e) =>
            _mainForm?.ApplyDilation(ParseStructeringMatrix());

        private void erosionButton_Click(object sender, EventArgs e) =>
            _mainForm?.ApplyErosion(ParseStructeringMatrix());

        private void openingButton_Click(object sender, EventArgs e) =>
            _mainForm?.ApplyOpening(ParseStructeringMatrix());

        private void closingButton_Click(object sender, EventArgs e) =>
            _mainForm?.ApplyClosing(ParseStructeringMatrix());

        private void discSizeTextBox_TextChanged(object sender, EventArgs e)
        {
            int size;
            if (!int.TryParse(discSizeTextBox.Text, out size))
                return;

            var discString = new StringBuilder();
            var middle = (float)(size + 1) / 2;

            for (int i = 1; i <= size; ++i, discString.AppendLine())
                for (int j = 1; j <= size; ++j)
                    discString.Append(Math.Abs(middle - i) + Math.Abs(middle - j) < middle ? "1, " : "0, ");

            transformMatrixTextBox.Text = discString.ToString();
        }
    }
}
