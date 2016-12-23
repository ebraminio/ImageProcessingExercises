using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ImageProcessing1
{
    public partial class HistogramDialog : Form
    {
        public HistogramDialog()
        {
            InitializeComponent();
        }

        public void Display(Dictionary<int, int> histogram)
        {
            var series = new Series("Histogram");
            series.ChartType = SeriesChartType.Area;
            series.Points.DataBindXY(histogram.Keys, histogram.Values);
            try
            {
                chart1.Series.Clear();
                chart1.Series.Add(series);
                chart1.Show();
            }
            catch
            {
            }
        }
    }
}
