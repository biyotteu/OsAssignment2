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
        Form1 form;
        public ProcessList()
        {
            InitializeComponent();
        }

        private void ProcessList_Load(object sender, EventArgs e)
        {
            form = sender as Form1;
            for (int i = 0; i < listView1.Columns.Count; i++)
            {
                listView1.Columns[i].Width = (listView1.Width - 3) / listView1.Columns.Count;
                listView1.Columns[i].TextAlign = HorizontalAlignment.Center;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            Setting setting = new Setting();
            var res = setting.ShowDialog();
            if(res == DialogResult.OK)
            {
                form.processes.Add(setting.process);
            }
        }
    }
}
