using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pharm2
{
    public partial class Form4 : Form
    {
        Form1 m;
        public Form4(Form1 form)
        {
            InitializeComponent();
            m = form;
            textBox1.Text = Settings1.Default.VNC;
            textBox2.Text = Settings1.Default.PYTTU;
            textBox3.Text = Settings1.Default.IB;
            textBox4.Text = Settings1.Default.WINS;
            textBox5.Text = Settings1.Default.PATH;
        }
        string fileContent = string.Empty;
        string filePath = string.Empty;
        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = ofd.FileName;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = ofd.FileName;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = ofd.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != null)
            {
                ProcessStartInfo put = new ProcessStartInfo();
                put.FileName = (@"" + textBox2.Text);/*путь к putty*/
                Process.Start(put);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != null)
            {
                ProcessStartInfo put = new ProcessStartInfo();
                put.FileName = (@"" + textBox3.Text);/*путь к ib*/
                Process.Start(put);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null)
            {
                ProcessStartInfo put = new ProcessStartInfo();
                put.FileName = (@"" + textBox1.Text);/*путь к VNC*/
                Process.Start(put);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        { 
            if (textBox1.Text != null && textBox2.Text != null && textBox3.Text != null)
            {
                Settings1.Default.VNC = textBox1.Text;
                Settings1.Default.PYTTU = textBox2.Text;
                Settings1.Default.IB = textBox3.Text;
                Settings1.Default.WINS = textBox4.Text;
                Settings1.Default.PATH = textBox5.Text;
                Settings1.Default.Save();
                m.path(textBox1.Text, textBox3.Text, textBox2.Text);
                this.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox4.Text = ofd.FileName;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null)
            {
                ProcessStartInfo put = new ProcessStartInfo();
                put.FileName = (@"" + textBox4.Text);/*путь к WINS*/
                Process.Start(put);
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
    }
}
