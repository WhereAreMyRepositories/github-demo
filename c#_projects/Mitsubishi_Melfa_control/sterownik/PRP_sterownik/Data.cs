using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRP_sterownik
{
    public partial class Data : Form
    {
        SerialPort myC;
        ManualResetEvent mrse;
  
        public Data(ref SerialPort myCOM,ref ManualResetEvent mrse)
        {
            
            InitializeComponent();
            myC = myCOM;
            this.mrse = mrse;
        }

        private void tableBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.tableBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dataSet1);

        }

        private void Data_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.Table' table. You can move, or remove it, as needed.
            this.tableTableAdapter.Fill(this.dataSet1.Table);

        }

        private void button1_Click(object sender, EventArgs ex)
        {
            string data = "";
            dataSet1.Tables["Table"].Clear();
            mrse.Reset();

           
                for (int i = Convert.ToInt16(textBox4.Text); i <= Convert.ToInt16(textBox5.Text); i++)
                {

                    try
                    {
                    
                        
                        myC.Write("PR " + i);

                        
                        Thread.Sleep(10);
                        while (myC.BytesToRead > 0)
                        {
                            try
                            {
                                data = myC.ReadTo("\r").ToString();
                            }
                            catch (TimeoutException) { }
                        }
                        

                    if (!checkBox1.Checked)
                    {
                        if (!data.StartsWith("0,0,0,0,0,0"))
                        {
                            DataRow nextpos = dataSet1.Tables["Table"].NewRow();
                            nextpos["Id"] = i;
                            nextpos["data"] = data;
                            dataSet1.Tables["Table"].Rows.Add(nextpos);
                        }
                    }
                    else
                    {
                        if (data.StartsWith("0,0,0,0,0,0"))
                        {
                            data = "undefined";
                            DataRow nextpos = dataSet1.Tables["Table"].NewRow();
                            nextpos["Id"] = i;
                            nextpos["data"] = data;
                            dataSet1.Tables["Table"].Rows.Add(nextpos);
                        }
                        else
                        {
                            DataRow nextpos = dataSet1.Tables["Table"].NewRow();
                            nextpos["Id"] = i;
                            nextpos["data"] = data;
                            dataSet1.Tables["Table"].Rows.Add(nextpos);
                        }
                    }

                        
                    }
                    catch (Exception e) { MessageBox.Show(e.ToString()); break; }
                }
            mrse.Set();


        }

        private void tableDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
