using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pharm2
{
    public partial class Form6 : Form
    {
        string PATH;
        public Form6()
        {
            InitializeComponent();
            PATH = Settings1.Default.PATH;
        }
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();
        private DataTable dtt = new DataTable();
        public void fill()
        {
            string st = "SELECT f.id, f.ip, w.kod_parus FROM public.\"IPkass\" f left join public.\"Apteka\" w on f.apteka_id = w.id order by f.id;;";

            NpgsqlConnection con = new NpgsqlConnection(PATH);
            con.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(st, con);
            ds.Reset();
            da.Fill(ds);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
            con.Close();

            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }
        private void Form6_Load(object sender, EventArgs e)
        {


            fill();
            NpgsqlConnection con = new NpgsqlConnection(PATH);
            con.Open();
            DataSet ers = new DataSet();
            string sql = "SELECT * FROM \"Apteka\";";
            NpgsqlDataAdapter daq = new NpgsqlDataAdapter(sql, con);
            ers.Reset();
            daq.Fill(ers);
            dtt = ers.Tables[0];
            string[] qwerty = new string[dtt.Rows.Count];
            int j = 0;
            foreach (DataRow row in dtt.Rows)
            {
                qwerty[j] = row["kod_parus"].ToString();
                if (qwerty[j] == "") { qwerty[j] = row["podraz"].ToString(); }
                j++;
            }
            j = 0;
            Array.Sort(qwerty);
            comboBox1.DataSource = qwerty;

            con.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString() != "")
                {
                    textBox1.Text = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
                    textBox2.Text = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
                    comboBox1.SelectedIndex = comboBox1.FindStringExact(dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString());
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        { 
            
            string st = "INSERT INTO public.\"IPkass\"(ip, apteka_id) SELECT '" + textBox2.Text + "',id FROM public.\"Apteka\" WHERE  kod_parus ='" + comboBox1.SelectedValue + "';"; 

        NpgsqlConnection con = new NpgsqlConnection(PATH);
        con.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(st, con);
        ds.Reset();
            da.Fill(ds);
            con.Close();
            fill();
        }

        private void button3_Click(object sender, EventArgs e)
        {
          string  st = "DELETE FROM public.\"IPkass\"WHERE id =" + textBox1.Text + ";";
        
        NpgsqlConnection con = new NpgsqlConnection(PATH);
        con.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(st, con);
        ds.Reset();
            da.Fill(ds);
            con.Close();
            fill();
    }

        private void button2_Click(object sender, EventArgs e)
        {
            string st = "UPDATE public.\"IPkass\" SET ip = '"+textBox2.Text+"',apteka_id =(select id from public.\"Apteka\" WHERE kod_parus = '" + comboBox1.SelectedValue + "') WHERE id =" + textBox1.Text + ";";
        
        NpgsqlConnection con = new NpgsqlConnection(PATH);
        con.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(st, con);
        ds.Reset();
            da.Fill(ds);
            con.Close();
            fill();
    }
    }
}
