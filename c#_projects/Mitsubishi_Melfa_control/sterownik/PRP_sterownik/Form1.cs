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

    public partial class Form1 : Form
    {
        Data available_pos;
        static readonly object thislock = new object();
        static public SerialPort myCOM;
        Thread RS232data_Thread;
        Thread pos_read;
        static public bool read_state;
        static bool _quit = false;
        public Form1()
        {
            InitializeComponent();
            ////////////////////////INIT CONFIG/////////////////////////
            foreach (string s in SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(s);
               
            }

            string[] baudRates ={ "4800","9600","19200","38400","57600","115200" };
            foreach (string s in baudRates)
            {
                comboBox2.Items.Add(s);
            }

            foreach (string s in Enum.GetNames(typeof(Parity)))
            {
                comboBox3.Items.Add(s);
            }

            comboBox4.Items.Add("4");
            comboBox4.Items.Add("5");
            comboBox4.Items.Add("6");
            comboBox4.Items.Add("7");
            comboBox4.Items.Add("8");

            foreach (string s in Enum.GetNames(typeof(StopBits)))
            {
                comboBox5.Items.Add(s);
            }

            foreach (string s in Enum.GetNames(typeof(Handshake)))
            {
                comboBox6.Items.Add(s);
            }

            textBox3.ReadOnly = true;
            tabPage3.Hide();
            tabControl1.TabPages.Remove(tabPage3);
            ////////////////////////////////// PAGE 3 - CONTROL UI //////////////////////////
            label11.Visible = false;
            label12.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            label15.Visible = false;
            label16.Visible = false;

            groupBox8.Visible = false;
            groupBox9.Visible = false;

            button8.Visible = false;
            button9.Visible = false;
            button10.Visible = false;
            button11.Visible = false;
            button12.Visible = false;
            button13.Visible = false;
            button7.Visible = false;

        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = 1;
            comboBox3.SelectedIndex = 2;
            comboBox4.SelectedIndex = 4;
            comboBox5.SelectedIndex = 2;
            comboBox6.SelectedIndex = 0;
            textBox1.Text = 500.ToString();
            textBox2.Text = 500.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;
            comboBox6.SelectedIndex = -1;
            textBox1.Text = String.Empty;
            textBox2.Text = String.Empty;
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void textBox3_Enter(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox4.Text += DateTime.Now.ToString("h:mm:ss tt") + ">>  " + textBox3.Text;
            textBox4.AppendText(Environment.NewLine);
            if (textBox3.Text != "")
            {
                myCOM.Write(textBox3.Text + '\r');
                textBox3.Text = "";
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button4_Click(this,new EventArgs());
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (myCOM != null)
                MessageBox.Show("U are already connected", "Error");
            else
            {
                if (comboBox2.SelectedIndex != -1 && comboBox3.SelectedIndex != -1
                    && comboBox4.SelectedIndex != -1 && comboBox5.SelectedIndex != -1
                    && comboBox6.SelectedIndex != -1 && textBox1.Text != String.Empty
                    && textBox2.Text != String.Empty && comboBox1.SelectedIndex != -1)
                {
                    myCOM = new SerialPort(comboBox1.SelectedItem.ToString());
                    myCOM.BaudRate = int.Parse(comboBox2.SelectedItem.ToString());
                    myCOM.Parity = (Parity)Enum.Parse(typeof(Parity), comboBox3.SelectedItem.ToString(), true);
                    myCOM.DataBits = int.Parse(comboBox4.SelectedItem.ToString().ToUpperInvariant());
                    myCOM.StopBits = (StopBits)Enum.Parse(typeof(StopBits), comboBox5.Text, true);
                    myCOM.Handshake = (Handshake)Enum.Parse(typeof(Handshake), comboBox6.Text, true);
                    myCOM.ReadTimeout = int.Parse(textBox1.Text);
                    myCOM.WriteTimeout = int.Parse(textBox2.Text);


                    textBox3.ReadOnly = false;

                    currentCOM.Text = comboBox1.SelectedItem.ToString();
                    currentBaudRate.Text = comboBox2.SelectedItem.ToString();
                    currentDataBits.Text = comboBox4.SelectedItem.ToString().ToUpperInvariant();
                    currentParity.Text = comboBox3.SelectedItem.ToString();
                    currentHandshake.Text = comboBox6.Text;
                    currentStopBits.Text = comboBox5.Text;

                    myCOM.Open();
                    read_state = true;
                    _quit = false;

                    RS232data_Thread = new Thread(delegate ()
                    {

                        while (!_quit)
                        {
                                 Invoke((MethodInvoker)delegate
                                {
                                    if (read_state && myCOM.BytesToRead > 0)
                                    {
                                        try
                                        {
                                            textBox5.Text += DateTime.Now.ToString("h:mm:ss tt") + "  " + myCOM.ReadTo("\n").ToString();
                                            textBox5.AppendText(Environment.NewLine);
                                        }
                                        catch (TimeoutException) { }
                                    }

                                });                 
                        }
                        
                     });
                    RS232data_Thread.Start();

                    tabControl1.TabPages.Insert(2, tabPage3);
                    this.tabPage3.Show();
                    textBox7.Text = "300";
                }

            }
        }


        public void button5_Click(object sender, EventArgs e)
        {
            if (myCOM != null)
            {
                read_state = false;
                _quit = true;
                RS232data_Thread.Abort();

                myCOM.Close();
                myCOM = null;

                currentCOM.Text = "None";
                currentBaudRate.Text = "None";
                currentDataBits.Text = "None";
                currentParity.Text = "None";
                currentHandshake.Text = "None";
                currentStopBits.Text = "None";

                textBox3.ReadOnly = true;
                foreach (string s in SerialPort.GetPortNames())
                {
                    comboBox1.Items.Add(s);
                }
                tabPage3.Hide();
                tabControl1.TabPages.Remove(tabPage3);
                ////////////////////////////////// PAGE 3 - CONTROL UI //////////////////////////
                label11.Visible = false;
                label12.Visible = false;
                label13.Visible = false;
                label14.Visible = false;
                label15.Visible = false;
                label16.Visible = false;

                groupBox8.Visible = false;
                groupBox9.Visible = false;

                button8.Visible = false;
                button9.Visible = false;
                button10.Visible = false;
                button11.Visible = false;
                button12.Visible = false;
                button13.Visible = false;
                button7.Visible = false;
            }
            else
                MessageBox.Show("U are not connected","Error");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            read_state = false;

            myCOM.Write("WH" + "\r");
            Thread.Sleep(50);
            if (myCOM.BytesToRead > 0)
                    {
                        try
                        {
                            textBox6.Text = myCOM.ReadTo("\n").ToString();
                        }
                        catch (TimeoutException) { }
                    }
            read_state = true;


        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                read_state = false;
                pos_read = new Thread(delegate ()
                  {
                      while (true)
                      {
                          if (textBox7.Text != "")
                              Thread.Sleep(Math.Abs(Convert.ToInt32(textBox7.Text)));
                          else
                          {
                              MessageBox.Show("undefined value", "Error");
                              break;
                          }
                          try
                          {

                              myCOM.Write("WH" + "\r");
                              if (myCOM.BytesToRead > 10)
                              {
                                  Invoke((MethodInvoker)delegate
                                  {
                                      textBox6.Text = myCOM.ReadTo("\n").ToString();
                                  });
                              }
                          }
                          catch (TimeoutException) { }
                      }
                  });
                pos_read.Start();
            }
            else
            {
                pos_read.Abort();
                read_state = true;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //button5.PerformClick();
            if (myCOM != null)
            {
                read_state = false;
                _quit = true;
                RS232data_Thread.Abort();

                myCOM.Close();
                myCOM = null;

                currentCOM.Text = "None";
                currentBaudRate.Text = "None";
                currentDataBits.Text = "None";
                currentParity.Text = "None";
                currentHandshake.Text = "None";
                currentStopBits.Text = "None";

                textBox3.ReadOnly = true;
                foreach (string s in SerialPort.GetPortNames())
                {
                    comboBox1.Items.Add(s);
                }
                tabPage3.Hide();
                tabControl1.TabPages.Remove(tabPage3);
            }
        }
        /////////////////////////////////////// CONTROL UI ////////////////////////////////////////////////
        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox7.SelectedIndex == 0 || comboBox7.SelectedIndex == 3)
            {
                label11.Visible = true;
                label12.Visible = true;
                label13.Visible = true;
                label14.Visible = true;
                label15.Visible = true;
                label16.Visible = true;

                groupBox8.Visible = true;
                groupBox9.Visible = true;

                label11.Text = "J1";
                label12.Text = "J2";
                label13.Text = "J3";
                label14.Text = "J4";
                label15.Text = "J5";
                label16.Text = "J6";

                numericUpDown1.Visible = true;
                numericUpDown2.Visible = true;
                numericUpDown3.Visible = true;
                numericUpDown4.Visible = true;
                numericUpDown5.Visible = true;
                numericUpDown6.Visible = true;

                numericUpDown1.Minimum = -360;
                numericUpDown1.Maximum = 360;
                numericUpDown2.Minimum = -360;
                numericUpDown2.Maximum = 360;
                numericUpDown3.Minimum = -360;
                numericUpDown3.Maximum = 360;
                numericUpDown4.Minimum = -360;
                numericUpDown4.Maximum = 360;
                numericUpDown5.Minimum = -360;
                numericUpDown5.Maximum = 360;
                numericUpDown6.Minimum = -360;
                numericUpDown6.Maximum = 360;

                maskedTextBox1.Visible = true;
                maskedTextBox2.Visible = true;
                maskedTextBox3.Visible = true;
                maskedTextBox4.Visible = true;
                maskedTextBox5.Visible = true;
                maskedTextBox6.Visible = true;

                button8.Visible = true;
                button9.Visible = true;
                button10.Visible = true;
                button11.Visible = true;
                button12.Visible = true;
                button13.Visible = true;
                button7.Visible = true;
            }
            else if(comboBox7.SelectedIndex == 1 || comboBox7.SelectedIndex == 2) 
            {
                label11.Visible = true;
                label12.Visible = true;
                label13.Visible = true;
                label14.Visible = false;
                label15.Visible = false;
                label16.Visible = false;

                groupBox8.Visible = true;
                groupBox9.Visible = true;

                label11.Text = "X";
                label12.Text = "Y";
                label13.Text = "Z";
                label14.Text = "J4";
                label15.Text = "J5";
                label16.Text = "J6";

                numericUpDown1.Visible = true;
                numericUpDown2.Visible = true;
                numericUpDown3.Visible = true;
                numericUpDown4.Visible = false;
                numericUpDown5.Visible = false;
                numericUpDown6.Visible = false;

                numericUpDown1.Minimum = -1500;
                numericUpDown1.Maximum = 1500;
                numericUpDown2.Minimum = -1500;
                numericUpDown2.Maximum = 1500;
                numericUpDown3.Minimum = -1500;
                numericUpDown3.Maximum = 1500;
                numericUpDown4.Minimum = -1500;
                numericUpDown4.Maximum = 1500;
                numericUpDown5.Minimum = -1500;
                numericUpDown5.Maximum = 1500;
                numericUpDown6.Minimum = -1500;
                numericUpDown6.Maximum = 1500;

                maskedTextBox1.Visible = true;
                maskedTextBox2.Visible = true;
                maskedTextBox3.Visible = true;
                maskedTextBox4.Visible = false;
                maskedTextBox5.Visible = false;
                maskedTextBox6.Visible = false;

                button8.Visible = true;
                button9.Visible = true;
                button10.Visible = true;
                button11.Visible = false;
                button12.Visible = false;
                button13.Visible = false;
                button7.Visible = true;
            }
            else if (comboBox7.SelectedIndex == 4)
            {
                label11.Visible = true;
                label12.Visible = true;
                label13.Visible = true;
                label14.Visible = true;
                label15.Visible = true;
                label16.Visible = true;

                groupBox8.Visible = true;
                groupBox9.Visible = true;

                label11.Text = "X";
                label12.Text = "Y";
                label13.Text = "Z";
                label14.Text = "A";
                label15.Text = "B";
                label16.Text = "F";

                numericUpDown1.Visible = true;
                numericUpDown2.Visible = true;
                numericUpDown3.Visible = true;
                numericUpDown4.Visible = true;
                numericUpDown5.Visible = true;
                numericUpDown6.Visible = true;

                numericUpDown1.Minimum = -1500;
                numericUpDown1.Maximum = 1500;
                numericUpDown2.Minimum = -1500;
                numericUpDown2.Maximum = 1500;
                numericUpDown3.Minimum = -1500;
                numericUpDown3.Maximum = 1500;
                numericUpDown4.Minimum = -360;
                numericUpDown4.Maximum = 360;
                numericUpDown5.Minimum = -360;
                numericUpDown5.Maximum = 360;
                numericUpDown6.Minimum = 00;
                numericUpDown6.Maximum = 22;

                maskedTextBox1.Visible = true;
                maskedTextBox2.Visible = true;
                maskedTextBox3.Visible = true;
                maskedTextBox4.Visible = true;
                maskedTextBox5.Visible = true;
                maskedTextBox6.Visible = false;

                button8.Visible = false;
                button9.Visible = false;
                button10.Visible = false;
                button11.Visible = false;
                button12.Visible = false;
                button13.Visible = false;
                button7.Visible = true;
            }



        }
        /////////////////////////////////////// CONTROL PAGE ////////////////////////////////////////////////////
        private void button15_Click(object sender, EventArgs e)
        {
            if (comboBox7.SelectedIndex == 0)
                MessageBox.Show("Function:  \n\nRotates the specified joint by the specified angle [in degrees] from the current position. (Joint interpolation) \n\n\nInput Format: \n\n DJ <joint number>, <turning angle>", "HELP");
            else if(comboBox7.SelectedIndex == 1)
                MessageBox.Show("Function:\n\nMoves the end of the hand to a position away from the current position by the distance [in milimeters] specified in X, Y and Z directions. (Linear interpolation)\n\n\nInput Format: \n\nDS [< travel distance in X >],\n   [< travel distance in Y >],\n   [< travel distance in Z >]", "HELP");
            else if (comboBox7.SelectedIndex == 2)
                MessageBox.Show("Function:\n\nMoves the end of the hand to a position away from the current position by the distance [in milimeters] specified in X, Y and Z directions. (Joiny interpolation)\n\n\nInput Format: \n\nDW [< travel distance in X >],\n   [< travel distance in Y >],\n   [< travel distance in Z >]", "HELP");
            else if (comboBox7.SelectedIndex == 3)
                MessageBox.Show("Function:  \n\nTurns each joint the specified angle [in degrees] from the current position. (Joint interpolation) \n\n\nInput Format: \n\nMJ [<waist joint angle>],\n [<shoulder joint angle>],\n[< elbow joint angle >],\n [< pitch joint angle >],\n [< roll joint angle >]", "HELP");
            else if (comboBox7.SelectedIndex == 4)
                MessageBox.Show("Function:  \n\nMoves the tip of hand to a position whose coordinates (position[mm] and angle[deg]) have been specified. (Joint interpolation) \n\n\nInput Format: \n\nMP [< X coordinate value>], [< Y coordinate value>], [<Z coordinate value >], [<A turning angle>], [<B turning angle>] [,[<R/L>] [,[<A/B>]]]", "HELP");
        }

        public void SendCommand(string val)
        {
            myCOM.Write(val + "\r");
            textBox14.Text += DateTime.Now.ToString("h:mm:ss tt") + "  " + val;
            textBox14.AppendText(Environment.NewLine);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            SendCommand("SP " + numericUpDown7.Value);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (comboBox7.SelectedIndex == 0)
                SendCommand("DJ 1," + numericUpDown1.Value.ToString().Replace(",","."));
            else if(comboBox7.SelectedIndex == 1)
                SendCommand("DS " + numericUpDown1.Value.ToString().Replace(",", ".")+",0,0");
            else if (comboBox7.SelectedIndex == 2)
                SendCommand("DW " + numericUpDown1.Value.ToString().Replace(",", ".") + ",0,0");
            else if (comboBox7.SelectedIndex == 3)
                SendCommand("MJ " + numericUpDown1.Value.ToString().Replace(",", "."));
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (comboBox7.SelectedIndex == 0)
                SendCommand("DJ 2," + numericUpDown2.Value.ToString().Replace(",", "."));
            else if (comboBox7.SelectedIndex == 1)
                SendCommand("DS 0," + numericUpDown2.Value.ToString().Replace(",", ".") + ",0");
            else if (comboBox7.SelectedIndex == 2)
                SendCommand("DW 0," + numericUpDown2.Value.ToString().Replace(",", ".") + ",0");
            else if (comboBox7.SelectedIndex == 3)
                SendCommand("MJ 0," + numericUpDown2.Value.ToString().Replace(",", "."));
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (comboBox7.SelectedIndex == 0)
                SendCommand("DJ 3," + numericUpDown3.Value.ToString().Replace(",", "."));
            else if (comboBox7.SelectedIndex == 1)
                SendCommand("DS 0,0," + numericUpDown3.Value.ToString().Replace(",", "."));
            else if (comboBox7.SelectedIndex == 2)
                SendCommand("DW 0,0," + numericUpDown3.Value.ToString().Replace(",", "."));
            else if (comboBox7.SelectedIndex == 3)
                SendCommand("MJ 0,0," + numericUpDown3.Value.ToString().Replace(",", "."));
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (comboBox7.SelectedIndex == 0)
                SendCommand("DJ 4," + numericUpDown4.Value.ToString().Replace(",", "."));
            else if (comboBox7.SelectedIndex == 3)
                SendCommand("MJ 0,0,0," + numericUpDown4.Value.ToString().Replace(",", "."));
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (comboBox7.SelectedIndex == 0)
                SendCommand("DJ 5," + numericUpDown5.Value.ToString().Replace(",", "."));
            else if (comboBox7.SelectedIndex == 3)
                SendCommand("MJ 0,0,0,0," + numericUpDown5.Value.ToString().Replace(",", "."));
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (comboBox7.SelectedIndex == 0)
                SendCommand("DJ 6," + numericUpDown6.Value.ToString().Replace(",", "."));
            else if (comboBox7.SelectedIndex == 3)
                SendCommand("MJ 0,0,0,0,0," + numericUpDown6.Value.ToString().Replace(",", "."));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SendCommand("SP 5");

            if (comboBox7.SelectedIndex == 4)
                if (numericUpDown6.Value == 0)
                SendCommand("MP " + numericUpDown1.Value.ToString().Replace(",", ".") +"," + numericUpDown2.Value.ToString().Replace(",", ".")+","+ numericUpDown3.Value.ToString().Replace(",", ".")+","+ numericUpDown4.Value.ToString().Replace(",", ".")+"," + numericUpDown5.Value.ToString().Replace(",", "."));
                else if (numericUpDown6.Value == 01)
                    SendCommand("MP " + numericUpDown1.Value.ToString().Replace(",", ".") + "," + numericUpDown2.Value.ToString().Replace(",", ".") + "," + numericUpDown3.Value.ToString().Replace(",", ".") + "," + numericUpDown4.Value.ToString().Replace(",", ".") + "," + numericUpDown5.Value.ToString().Replace(",", ".")+",0,R");
                else if (numericUpDown6.Value == 02)
                    SendCommand("MP " + numericUpDown1.Value.ToString().Replace(",", ".") + "," + numericUpDown2.Value.ToString().Replace(",", ".") + "," + numericUpDown3.Value.ToString().Replace(",", ".") + "," + numericUpDown4.Value.ToString().Replace(",", ".") + "," + numericUpDown5.Value.ToString().Replace(",", ".") + ",0,L");
                else if (numericUpDown6.Value == 10)
                    SendCommand("MP " + numericUpDown1.Value.ToString().Replace(",", ".") + "," + numericUpDown2.Value.ToString().Replace(",", ".") + "," + numericUpDown3.Value.ToString().Replace(",", ".") + "," + numericUpDown4.Value.ToString().Replace(",", ".") + "," + numericUpDown5.Value.ToString().Replace(",", ".") + ",A,0");
                else if (numericUpDown6.Value == 11)
                    SendCommand("MP " + numericUpDown1.Value.ToString().Replace(",", ".") + "," + numericUpDown2.Value.ToString().Replace(",", ".") + "," + numericUpDown3.Value.ToString().Replace(",", ".") + "," + numericUpDown4.Value.ToString().Replace(",", ".") + "," + numericUpDown5.Value.ToString().Replace(",", ".") + ",A,R");
                else if (numericUpDown6.Value == 12)
                    SendCommand("MP " + numericUpDown1.Value.ToString().Replace(",", ".") + "," + numericUpDown2.Value.ToString().Replace(",", ".") + "," + numericUpDown3.Value.ToString().Replace(",", ".") + "," + numericUpDown4.Value.ToString().Replace(",", ".") + "," + numericUpDown5.Value.ToString().Replace(",", ".") + ",A,L");
                else if (numericUpDown6.Value == 20)
                    SendCommand("MP " + numericUpDown1.Value.ToString().Replace(",", ".") + "," + numericUpDown2.Value.ToString().Replace(",", ".") + "," + numericUpDown3.Value.ToString().Replace(",", ".") + "," + numericUpDown4.Value.ToString().Replace(",", ".") + "," + numericUpDown5.Value.ToString().Replace(",", ".") + ",B,0");
                else if (numericUpDown6.Value == 21)
                    SendCommand("MP " + numericUpDown1.Value.ToString().Replace(",", ".") + "," + numericUpDown2.Value.ToString().Replace(",", ".") + "," + numericUpDown3.Value.ToString().Replace(",", ".") + "," + numericUpDown4.Value.ToString().Replace(",", ".") + "," + numericUpDown5.Value.ToString().Replace(",", ".") + ",B,R");
                else if (numericUpDown6.Value == 22)
                    SendCommand("MP " + numericUpDown1.Value.ToString().Replace(",", ".") + "," + numericUpDown2.Value.ToString().Replace(",", ".") + "," + numericUpDown3.Value.ToString().Replace(",", ".") + "," + numericUpDown4.Value.ToString().Replace(",", ".") + "," + numericUpDown5.Value.ToString().Replace(",", ".") + ",B,L");

            button8.PerformClick();
            button9.PerformClick();
            button10.PerformClick();
            button11.PerformClick();
            button12.PerformClick();
            button13.PerformClick();
        }



        ///////////////////////////////////// MULTIPLIERS ////////////////////////////////////////////////////
        private void maskedTextBox6_TextChanged(object sender, EventArgs e)
        {
            if (sender == maskedTextBox1)
                numericUpDown1.Increment = Convert.ToDecimal(maskedTextBox1.Text);
            else if (sender == maskedTextBox2)
                numericUpDown2.Increment = Convert.ToDecimal(maskedTextBox2.Text);
            else if (sender == maskedTextBox3)
                numericUpDown3.Increment = Convert.ToDecimal(maskedTextBox3.Text);
            else if (sender == maskedTextBox4)
                numericUpDown4.Increment = Convert.ToDecimal(maskedTextBox4.Text);
            else if (sender == maskedTextBox5)
                numericUpDown2.Increment = Convert.ToDecimal(maskedTextBox5.Text);
            else if (sender == maskedTextBox6)
                numericUpDown2.Increment = Convert.ToDecimal(maskedTextBox6.Text);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            SendCommand("GC");
        }

        private void button17_Click(object sender, EventArgs e)
        {
            SendCommand("GO");
        }

        private void button18_Click(object sender, EventArgs e)
        {
            SendCommand("HE " + numericUpDown8.Value);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            Data available_pos = new Data(ref myCOM);
            available_pos.ShowDialog(); // Shows Form2
        }
    }
}
