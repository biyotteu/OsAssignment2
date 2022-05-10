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
        public List<Process> processes = new List<Process>();
        public int processNumber = 0, quantum = 1;
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

        private void button1_Click(object sender, EventArgs e)
        {
            ProcessList newProcessList = new ProcessList(this);
            newProcessList.ShowDialog();
            render();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    FCFS();
                    break;
                case 1:
                    SJF();
                    break;
                case 2:
                    priorityScheduling();
                    break;
                case 3:
                    RR();
                    break;
                default:
                    break;
            }
        }

        private void render()
        {
            listView1.Items.Clear();
            foreach (var item in this.processes)
            {
                ListViewItem lvi = new ListViewItem();

                lvi.UseItemStyleForSubItems = false;
                lvi.SubItems.Clear();
                lvi.SubItems[0].Text = item.num.ToString();
                //lvi.SubItems.Add(item.num.ToString());
                lvi.SubItems.Add(item.burstTime.ToString());
                lvi.SubItems.Add(item.arrivalTime.ToString());
                lvi.SubItems.Add(item.priority.ToString());
                lvi.SubItems.Add("■");
                lvi.SubItems[4].ForeColor = item.color;
                listView1.Items.Add(lvi);
            }
        }

        private void FCFS()
        {
            List<Process> tmp = processes.ToList();
            tmp.Sort((Process a, Process b) => a.arrivalTime > b.arrivalTime ? 1:-1);
            int last = 0;
            foreach (var item in tmp)
            {
                if(last <= item.arrivalTime)
                {
                    last = item.arrivalTime + item.burstTime;
                }
                else
                {
                    
                }
            }

        }
        private void SJF()
        {

        }

        private void priorityScheduling()
        {

        }

        private void RR()
        {

        }
    }
}
