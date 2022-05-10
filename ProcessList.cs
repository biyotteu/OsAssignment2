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
    public partial class ProcessList : Form
    {
        Form1 parent;
        public ProcessList(Form1 f)
        {
            InitializeComponent();
            parent = f;
            
        }

        private void ProcessList_Load(object sender, EventArgs e)
        {

            textBox1.Text = parent.quantum.ToString();
            listView1.FullRowSelect = true;
            for (int i = 0; i < listView1.Columns.Count; i++)
            {
                listView1.Columns[i].Width = (listView1.Width - 3) / listView1.Columns.Count;
                listView1.Columns[i].TextAlign = HorizontalAlignment.Center;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.parent.quantum = Int32.Parse(textBox1.Text);
            this.Close();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            Setting setting = new Setting(null);
            var res = setting.ShowDialog();
            if(res == DialogResult.OK)
            {
                setting.process.num = ++this.parent.processNumber;
                this.parent.processes.Add(setting.process);
                render();
            }
        }
        private void render()
        {
            listView1.Items.Clear();
            foreach (var item in this.parent.processes)
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

        private void EditButton_Click(object sender, EventArgs e)
        {
            Setting setting = new Setting(this.parent.processes[listView1.SelectedItems[0].Index]);
            var res = setting.ShowDialog();
            if (res == DialogResult.OK)
            {
                this.parent.processes[listView1.SelectedItems[0].Index] = setting.process;
                render();
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            this.parent.processes.RemoveAt(listView1.SelectedItems[0].Index);
            render();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!(char.IsDigit(e.KeyChar)|| e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }
    }
}
