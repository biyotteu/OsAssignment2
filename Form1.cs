using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

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
            processes.Add(new Process());
            processes.Add(new Process());
            processes.Add(new Process());
            processes[0].arrivalTime = 0;
            processes[0].burstTime = 2;
            processes[0].num = 1;
            processes[0].priority = 2;
            processes[0].color = Color.Red;
            processes[1].arrivalTime = 1;
            processes[1].burstTime = 1;
            processes[1].num = 2;
            processes[1].priority = 1;
            processes[1].color = Color.Blue;
            processes[2].arrivalTime = 4;
            processes[2].burstTime = 8;
            processes[2].num = 3;
            processes[2].priority = 4;
            processes[2].color = Color.Green;
            processes[3].arrivalTime = 3;
            processes[3].burstTime = 4;
            processes[3].num = 4;
            processes[3].priority = 2;
            processes[3].color = Color.Yellow;
            processes[4].arrivalTime = 2;
            processes[4].burstTime = 5;
            processes[4].num = 5;
            processes[4].priority = 3;
            processes[4].color = Color.Pink;
            processNumber = 5;

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
            chart1.Legends[0].CustomItems.Clear();
            chart1.Series[0].Points.Clear();
            results.Clear();
            foreach (var process in processes)
            {
                chart1.Legends[0].CustomItems.Add(process.color, "P" + process.num.ToString());
            }
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
            double wait = 0, turn = 0;
            results.Sort((Result a, Result b) => a.num > b.num ? 1 : -1);
            listView2.Items.Clear();
            foreach (var item in this.results)
            {
                wait += item.waitTime;
                turn += item.completeTime;

                ListViewItem lvi = new ListViewItem();
                lvi.SubItems[0].Text = item.num.ToString();
                lvi.SubItems.Add(item.waitTime.ToString());
                lvi.SubItems.Add(item.completeTime.ToString());
                listView2.Items.Add(lvi);
            }
            AWT.Text = "Average Wait Time : " + String.Format("{0:0.##}", (wait / results.Count));
            ATT.Text = "Average Turnaround Time : " + String.Format("{0:0.##}", (turn / results.Count));
        }

        private void addPoint(Process process,int start,int end)
        {
            System.Windows.Forms.DataVisualization.Charting.DataPoint point = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
            point.Color = process.color;
            point.SetValueXY(process.num, start, end);
            /*point.LabelForeColor = Color.White;
            point.Label = "P" + process.num.ToString();*/
            chart1.Series[0].Points.Add(point);
            if (process.num == 5) Debug.WriteLine(start.ToString() + "###");
        }
        private void FCFS()
        {
            List<Process> tmp = processes.ToList();
            tmp.Sort((Process a, Process b) => a.arrivalTime == b.arrivalTime ? (a.priority > b.priority ? 1 : -1) : (a.arrivalTime > b.arrivalTime ? 1 : -1));
            int last = 0;
            foreach (var process in tmp)
            {
                int start, end;
                if(last <= process.arrivalTime)
                {
                    start = process.arrivalTime;
                }
                else
                {
                    start = last;
                }
                end = start + process.burstTime;
                last = end;
                addPoint(process, start, end);
                Result res = new Result(process.num, start - process.arrivalTime, end);
                results.Add(res);
            }
        }
        private void SJF()
        {
            Dictionary<int, int> idx = new Dictionary<int, int>();
            MinHeap<Process> waitQ = new MinHeap<Process>(new arrivalTimeCompare());
            MinHeap<Process> readyQ = new MinHeap<Process>(new burstTimeCompare());
            int last = 0;
            foreach (Process a in processes)
            {
                results.Add(new Result(a.num, -1, -1));
                idx.Add(a.num, last++);
                waitQ.Add(new Process(a));
            }
            last = 0;
            while (waitQ.Count > 0)
            {
                while (waitQ.Count > 0 && waitQ.GetMin().arrivalTime <= last)
                {
                    Debug.WriteLine(waitQ.GetMin().num+" "+last.ToString()+"##");
                    readyQ.Add(waitQ.ExtractDominating());
                }
                if (readyQ.Count == 0)
                {
                    last = waitQ.GetMin().arrivalTime;
                    continue;
                }
                while (readyQ.Count > 0)
                {
                    Process a = readyQ.ExtractDominating();
                    if (waitQ.Count > 0 && waitQ.GetMin().arrivalTime < last + a.burstTime)
                    {
                        addPoint(a, last, waitQ.GetMin().arrivalTime);
                        a.burstTime -= (waitQ.GetMin().arrivalTime - last);
                        last = waitQ.GetMin().arrivalTime;
                        readyQ.Add(a);
                        readyQ.Add(waitQ.ExtractDominating());
                        continue;
                    }
                    results[idx[a.num]].completeTime = last + a.burstTime;

                    addPoint(a, last, last + a.burstTime);

                    last += a.burstTime;
                }
            }
            foreach (Process a in processes)
            {
                results[idx[a.num]].waitTime = results[idx[a.num]].completeTime - a.arrivalTime - a.burstTime;
            }
        }

        private void priorityScheduling()
        {
            Dictionary<int, int> idx = new Dictionary<int, int>();
            MinHeap<Process> waitQ = new MinHeap<Process>(new arrivalTimeCompare());
            MinHeap<Process> readyQ = new MinHeap<Process>(new priorityCompare());
            int last = 0;
            foreach (Process a in processes)
            {
                results.Add(new Result(a.num, -1, -1));
                idx.Add(a.num, last++);
                waitQ.Add(new Process(a));
            }
            last = 0;
            while (waitQ.Count > 0)
            {
                while (waitQ.Count > 0 && waitQ.GetMin().arrivalTime <= last)
                {
                    readyQ.Add(waitQ.ExtractDominating());
                }
                if (readyQ.Count == 0)
                {
                    last = waitQ.GetMin().arrivalTime;
                    continue;
                }
                while (readyQ.Count > 0)
                {
                    Process a = readyQ.ExtractDominating();
                    if (waitQ.Count>0 && waitQ.GetMin().arrivalTime < last + a.burstTime)
                    {
                        addPoint(a, last, waitQ.GetMin().arrivalTime);
                        a.burstTime -= (waitQ.GetMin().arrivalTime - last);
                        last = waitQ.GetMin().arrivalTime;
                        readyQ.Add(a);
                        readyQ.Add(waitQ.ExtractDominating());
                        continue;
                    }
                    results[idx[a.num]].completeTime = last + a.burstTime;

                    addPoint(a, last, last + a.burstTime);

                    last += a.burstTime;
                }
            }
            foreach (Process a in processes)
            {
                results[idx[a.num]].waitTime = results[idx[a.num]].completeTime - a.arrivalTime - a.burstTime;
            }
        }
        
        private void RR()
        {
            Dictionary<int, int> idx = new Dictionary<int, int>();
            MinHeap<Process> waitQ = new MinHeap<Process>(new arrivalTimeCompare());
            MinHeap<Process> readyQ = new MinHeap<Process>(new priorityCompare());
            int last = 0;
            foreach (Process a in processes)
            {
                results.Add(new Result(a.num,-1,-1));
                idx.Add(a.num,last++);
                waitQ.Add(new Process(a));
            }
            last = 0;
            while (waitQ.Count > 0)
            {
                List<Process> tmp = new List<Process>();
                while(waitQ.Count > 0 && waitQ.GetMin().arrivalTime <= last)
                {
                    readyQ.Add(waitQ.ExtractDominating());
                }
                if (readyQ.Count == 0)
                {
                    last = waitQ.GetMin().arrivalTime;
                    continue;
                }
                while (readyQ.Count > 0)
                {
                    Process a = readyQ.ExtractDominating();
                    int ptime = quantum < a.burstTime ? quantum : a.burstTime;

                    results[idx[a.num]].completeTime = last + ptime;
                    
                    addPoint(a, last, last + ptime);

                    last += ptime;
                    a.burstTime -= ptime;
                    if (a.burstTime > 0) tmp.Add(a);
                    
                    while (waitQ.Count > 0 && waitQ.GetMin().arrivalTime <= last)
                    {
                        readyQ.Add(waitQ.ExtractDominating());
                    }
                }
                foreach (var item in tmp)
                {
                    waitQ.Add(item);
                }
            }
            foreach (Process a in processes)
            {
                results[idx[a.num]].waitTime = results[idx[a.num]].completeTime - a.arrivalTime - a.burstTime;
            }
        }
        public class priorityCompare : Comparer<Process>
        {
            public override int Compare(Process x, Process y)
            {
                return x.priority == y.priority ? 0 : (x.priority < y.priority ? -1 : 1);
            }
        }
        public class burstTimeCompare : Comparer<Process>
        {
            public override int Compare(Process x, Process y)
            {
                return x.burstTime == y.burstTime ? 0 : (x.burstTime < y.burstTime ? -1 : 1);
            }
        }
        public class arrivalTimeCompare : Comparer<Process>
        {
            public override int Compare(Process x, Process y)
            {
                return x.arrivalTime == y.arrivalTime ? 0 : (x.arrivalTime < y.arrivalTime ? -1 : 1);
            }
        }
    }
}
