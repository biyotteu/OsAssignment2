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
        public List<Result> results = new List<Result>();
        public Form1()
        {
            InitializeComponent();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            processes.Add(new Process());
            processes.Add(new Process());
            processes[0].arrivalTime = 1;
            processes[0].burstTime = 5;
            processes[0].num = 1;
            processes[0].color = Color.Red;
            processes[1].arrivalTime = 3;
            processes[1].burstTime = 3;
            processes[1].num = 2;
            processes[1].color = Color.Blue;
            processNumber = 2;

            for (int i = 0; i < listView1.Columns.Count; i++)
            {
                listView1.Columns[i].Width = (listView1.Width-3) / listView1.Columns.Count;
                listView1.Columns[i].TextAlign = HorizontalAlignment.Center;
            }
            for (int i = 0; i < listView2.Columns.Count; i++)
            {
                listView2.Columns[i].Width = (listView2.Width - 3) / listView2.Columns.Count;
                listView2.Columns[i].TextAlign = HorizontalAlignment.Center;
            }
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.RangeBar;
            comboBox1.SelectedIndex = 0;
            chart1.ChartAreas[0].AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            render();
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
            results.Clear();
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
            render2();
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
        private void render2()
        {
            listView2.Items.Clear();
            foreach (var item in this.results)
            {
                ListViewItem lvi = new ListViewItem();

                //lvi.UseItemStyleForSubItems = false;
                lvi.SubItems.Add(item.num.ToString());
                //lvi.SubItems.Add(item.num.ToString());
                lvi.SubItems.Add(item.waitTime.ToString());
                lvi.SubItems.Add(item.completeTime.ToString());
                listView2.Items.Add(lvi);
            }
        }
        private void FCFS()
        {
            List<Process> tmp = processes.ToList();
            tmp.Sort((Process a, Process b) => a.arrivalTime > b.arrivalTime ? 1:-1);
            int last = 0;
            foreach (var process in tmp)
            {
                
                System.Windows.Forms.DataVisualization.Charting.DataPoint point = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
                point.Color = process.color;
                if (last <= process.arrivalTime)
                {
                    last = process.arrivalTime + process.burstTime;
                    point.SetValueXY(process.num, process.arrivalTime, process.arrivalTime + process.burstTime);
                }
                else
                {
                    point.SetValueXY(process.num, last, last + process.burstTime);
                    last += process.burstTime;
                }
                chart1.Series[0].Points.Add(point);
                
            }
            /*foreach (var item in tmp)
            {
                
            }*/

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
