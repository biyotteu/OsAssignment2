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
    public partial class Setting : Form
    {
        public Process process = new Process();
        public Setting()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                colorPreview.BackColor = colorDialog1.Color;
                process.color = colorDialog1.Color;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            process.arrivalTime = Decimal.ToInt32(ArrivalTime.Value);
            process.burstTime = Decimal.ToInt32(BurstTime.Value);
            process.priority = Decimal.ToInt32(Priority.Value);
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
