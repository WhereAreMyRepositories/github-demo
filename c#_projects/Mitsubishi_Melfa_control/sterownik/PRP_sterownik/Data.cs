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
      
        public Data(ref SerialPort myCOM)
        {
            
            InitializeComponent();
            myC = myCOM;
          
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
            

            for (int i = Convert.ToInt16(textBox4.Text); i <= Convert.ToInt16(textBox5.Text); i++)
            {
                try
                {
                    DataRow nextpos = dataSet1.Tables["Table"].NewRow();
                    myC.Write("PR " + i);                    

                    nextpos["Id"] = i;
                    Thread.Sleep(10);
                    while(myC.BytesToRead > 10)
                    {
                        try
                        {
                            nextpos["data"] = myC.ReadTo("\n").ToString();
                        }
                        catch (TimeoutException) { }
                    }


                    dataSet1.Tables["Table"].Rows.Add(nextpos);
                }
                catch ( Exception e) { MessageBox.Show(e.ToString()); break;  }
            }
       
        }

        private void tableDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
