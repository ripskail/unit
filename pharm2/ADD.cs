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
    public partial class ADD : Form
    {
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();
        int flag;
        string PATH;
        public ADD(int f)
        {
            InitializeComponent();
            flag = f;
            PATH = Settings1.Default.PATH;
        }
        string st;
        private void Form5_Load(object sender, EventArgs e)
        {
            fill();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString() != "")
                {
                    textBox2.Text = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
                    textBox1.Text = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
                }

            }
        }

        public void fill()
        {
            if (flag == 1)
            {
                st = "SELECT * FROM \"Version\" ORDER BY name;";
            }
            if (flag == 2)
            {
                st = "SELECT * FROM \"Bit\" ORDER BY name;";

            }
            if (flag == 3)
            {
                st = "SELECT * FROM \"Firebird\" ORDER BY name;";
            }
            if (flag == 4)
            {
                st = "SELECT * FROM \"Obl\" ORDER BY name;";
            }
            if (flag == 5)
            {
                st = "SELECT * FROM \"OS\" ORDER BY name;";
            }

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
        private void INSERT(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                st = "INSERT INTO public.\"Version\"(name)VALUES('" + textBox1.Text+"');";
            }
            if (flag == 2)
            {
                st = "INSERT INTO public.\"Bit\"(name)VALUES('" + textBox1.Text + "');";

            }
            if (flag == 3)
            {
                st = "INSERT INTO public.\"Firebird\"(name)VALUES('" + textBox1.Text + "');";
            }
            if (flag == 4)
            {
                st = "INSERT INTO public.\"Obl\"(name)VALUES('" + textBox1.Text + "');";

            }
            if (flag == 5)
            {
                st = "INSERT INTO public.\"OS\"(name)VALUES('" + textBox1.Text + "');";
            }
           

                 NpgsqlConnection con = new NpgsqlConnection(PATH);
            con.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(st, con);
            ds.Reset();
            da.Fill(ds);
            con.Close();
            fill();
        }

        private void DELETE(object sender, EventArgs e)
        {
            
                if (flag == 1)
            {
                st = "DELETE FROM public.\"Version\"WHERE id =" + textBox2.Text + ";";
            }
            if (flag == 2)
            {
                st = "DELETE FROM public.\"Bit\"WHERE id =" + textBox2.Text + ";";

            }
            if (flag == 3)
            {
                st = "DELETE FROM public.\"Firebird\"WHERE id =" + textBox2.Text + ";";
            }
            if (flag == 4)
            {
                st = "DELETE FROM public.\"Obl\"WHERE id =" + textBox2.Text + ";";
            }
            if (flag == 5)
            {
                st = "DELETE FROM public.\"OS\"WHERE id =" + textBox2.Text + ";";
            }
            NpgsqlConnection con = new NpgsqlConnection(PATH);
            con.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(st, con);
            ds.Reset();
            da.Fill(ds);
            con.Close();
            fill();
        }

        private void UPDATE(object sender, EventArgs e)
        {
           
            if (flag == 1)
            {
                st = "UPDATE public.\"Version\" SET name ='" + textBox1.Text + "' WHERE id =" + textBox2.Text + ";";
            }
            if (flag == 2)
            {
                st = "UPDATE public.\"Bit\" SET name ='" + textBox1.Text + "' WHERE id =" + textBox2.Text + ";";
     
            }
            if (flag == 3)
            {
                st = "UPDATE public.\"Firebird\" SET name ='" + textBox1.Text + "' WHERE id =" + textBox2.Text + ";"; 
            }
            if (flag == 4)
            {
                st = "UPDATE public.\"Obl\" SET name ='" + textBox1.Text + "' WHERE id =" + textBox2.Text + ";";
            }
            if (flag == 5)
            {
                st = "UPDATE public.\"OS\" SET name ='" + textBox1.Text + "' WHERE id =" + textBox2.Text + ";";
            }
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
