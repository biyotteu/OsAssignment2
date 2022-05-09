using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OsAssignment2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for(int i = 0; i < listView1.Columns.Count; i++)
            {
                listView1.Columns[i].Width = (listView1.Width-3) / listView1.Columns.Count;
                listView1.Columns[i].TextAlign = HorizontalAlignment.Center;
            }
            for (int i = 0; i < listView2.Columns.Count; i++)
            {
                listView2.Columns[i].Width = (listView2.Width - 3) / listView2.Columns.Count;
                listView2.Columns[i].TextAlign = HorizontalAlignment.Center;
            }
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
            comboBox1.SelectedIndex = 0;
        }
    }
}
