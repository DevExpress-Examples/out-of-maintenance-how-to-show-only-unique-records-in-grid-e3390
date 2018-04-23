using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FilteringRepeatRows
{
    public partial class Form1 : Form
    {
        List<string>[] filterFields = new List<string>[2];
        FilterHelper helper;

        public Form1()
        {
            InitializeComponent();
            List<string> fields = new List<string>();
            fields.Add("Name");
            fields.Add("Company");
            fields.Add("Address");
            filterFields[0] = null;
            filterFields[1] = fields;
            helper = new FilterHelper(gridView1);
            radioGroup1.SelectedIndex = 0;
            gridControl1.DataSource = CreateTable(20);
        }

        DataTable CreateTable(int rowCount)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Name");
            table.Columns.Add("Company");
            table.Columns.Add("Age", typeof(int));
            table.Columns.Add("Address");
            int repeatCount = 3;
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < repeatCount; j++)
                {
                    table.Rows.Add(new object[] { String.Format("Name{0}", i), String.Format("Company{0}", i), 25 + i * repeatCount + j, String.Format("Address{0}", i) });
                }
            }
            return table;
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            helper.Fields = filterFields[radioGroup1.SelectedIndex];
        }
    }
}
