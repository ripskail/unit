using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using Npgsql;

namespace pharm2
{
    public partial class Form2 : Form
    {
        DataTable f;
        Form1 m;
        Form2 qw;
        string PATH,STR;
        public Form2(Form1 form)
        {
            InitializeComponent();
            m = form;
            PATH = Settings1.Default.PATH;
          

        }

        void menuItem_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            string r = menuItem.Selected.ToString();
            if (menuItem.CheckState == CheckState.Checked)
                MessageBox.Show("Отмечен");
            else if (menuItem.CheckState == CheckState.Unchecked)
                MessageBox.Show("Отметка снята");
        }
        private BindingSource bindingSource1 = new BindingSource();
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();


        public void stroka()
        {
            STR = Settings1.Default.Findstring1.Replace(@"\", @"");
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            STR = Settings1.Default.Findstring1.Replace(@"\", @"");
            if (true == Settings1.Default.ChekedAktiv)
            {
                checkBox1.Checked = true;
            }
            else { }
            if (true == Settings1.Default.ChekedTum)
            {
                checkBox3.Checked = true;
            }
            else { }
            if (true == Settings1.Default.ChekedTob)
            {
                checkBox2.Checked = true;
            }
            else { }
            if (true == Settings1.Default.ChekedSois)
            {
                checkBox4.Checked = true;
            }
            else { }
            if (true == Settings1.Default.ChekedPril)
            {
                checkBox6.Checked = true;
            }
            else { }
            if (true == Settings1.Default.ChekedIshm)
            {
                checkBox5.Checked = true;
            }
            else { }
            olast();
            // fill();
        }

        public void fill()
        {
            NpgsqlConnection con = new NpgsqlConnection(PATH);
            con.Open();
            string sql = "SELECT "+ STR + "  FROM public.\"Apteka\" ap left join public.\"Obl\" o on ap.obl_id = o.id left join public.\"Version\" v on ap.version = v.id left join public.\"OS\" s on ap.os = s.id left join public.\"Firebird\" f on ap.firebird = f.id left join public.\"Bit\" b on ap.bit = b.id;";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
            ds.Reset();
            da.Fill(ds);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
            con.Close();

            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.Sort(dataGridView1.Columns["id"], ListSortDirection.Ascending);
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
           
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString() != "")
                {
                    m.SW(dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString());
                }
                else
                {
                    m.SW(dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString());
                }
               
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
               CurrencyManager cManager =
                dataGridView1.BindingContext[dataGridView1.DataSource, dataGridView1.DataMember]
                as CurrencyManager;

            cManager.SuspendBinding();
            dataGridView1.CurrentCell = null;
            for (int i = 0; i < dataGridView1.RowCount; i++)//тут выделяем строки с символоми из строки поиска
            {
                dataGridView1.Rows[i].Selected = false;
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                    {
                        if (textBox1.Text != " " || textBox1.Text != String.Empty)
                        {
                            if (dataGridView1.Rows[i].Cells[j].Value.ToString().ToLower().Contains(textBox1.Text.ToLower()))
                            {
                                if (!dataGridView1.Rows[i].Visible)
                                {
                                    dataGridView1.Rows[i].Visible = true;
                                }
                                dataGridView1.Rows[i].Selected = true;
                                break;
                            }
                        }
                    }
            }

            for (int i = 0; i < dataGridView1.RowCount; i++)// а тут, делаем невидимыми все остальные 
            {
                if (dataGridView1.Rows[i].Selected != true)
                {
                    dataGridView1.CurrentCell = null;
                    if (dataGridView1.Rows[i].Cells[0].Value != null)
                    {

                        dataGridView1.Rows[i].Visible = false;
                    }

                }


            }
            cManager.ResumeBinding();
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void прилегающиеРайоныToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            Form7 newForm = new Form7(m,this);
            newForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string st = "DELETE FROM public.\"Apteka\"WHERE id =" + dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString() + ";";

            NpgsqlConnection con = new NpgsqlConnection(PATH);
            con.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(st, con);
            ds.Reset();
            da.Fill(ds);
            con.Close();
            fill();
        }

        public void update()
        {
            string aa = "";
            if (checkBox1.Checked == true)
            { aa = "where ap.activ = true"; }
            else { aa = ""; }

            NpgsqlConnection con = new NpgsqlConnection(PATH);
            con.Open();
            string sql = "SELECT  " + STR + "  FROM public.\"Apteka\" ap left join public.\"Obl\" o on ap.obl_id = o.id left join public.\"Version\" v on ap.version = v.id left join public.\"OS\" s on ap.os = s.id left join public.\"Firebird\" f on ap.firebird = f.id left join public.\"Bit\" b on ap.bit = b.id " + aa + ";";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
            ds.Reset();
            da.Fill(ds);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
            con.Close();

            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.Sort(dataGridView1.Columns["id"], ListSortDirection.Ascending);
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            olast();


        }

        public void olast()
        {
            string where = "";
            string d = "";
            string q = "";
            string aa = "";
            if (checkBox3.Checked == true | checkBox2.Checked == true | checkBox4.Checked == true | checkBox5.Checked == true | checkBox6.Checked == true | checkBox1.Checked == true)
            {
                where = "where ";
            }
            else where = "";
            if (checkBox3.Checked == true | checkBox2.Checked == true | checkBox4.Checked == true | checkBox5.Checked == true | checkBox6.Checked == true)
            {
                q = "(";
                d = ")";
            }
            else
            {
                q = "";
                d = "";
            }
            if (checkBox1.Checked == true)
            {
                if (checkBox3.Checked == true | checkBox2.Checked == true | checkBox4.Checked == true | checkBox5.Checked == true | checkBox6.Checked == true)
                {
                    aa = " and ap.activ = true";
                    q = "(";
                    d = ") ";
                }
                else aa = "ap.activ = true";
            }
            else {
                aa = ""; }

            int w = 0;
            string txt = "";
            if (checkBox3.Checked == true)
            {
                txt = "o.name ='ТЮМЕНЬ'";
                w++;
            }
            if (checkBox2.Checked == true)
            {
                if (checkBox3.Checked == true)
                {
                    txt += " or o.name ='ТОБОЛЬСК'";
                }
                else { txt = "o.name ='ТОБОЛЬСК'"; }
            }
            if (checkBox4.Checked == true)
            {
                if (checkBox3.Checked == true| checkBox2.Checked == true)
                {
                    txt += " or o.name ='СОИСПОЛНИТЕЛИ'";
                }
                else { txt = "o.name ='СОИСПОЛНИТЕЛИ'"; }
            }
            if (checkBox5.Checked == true)
            {
                if (checkBox3.Checked == true | checkBox2.Checked == true| checkBox4.Checked == true)
                {
                    txt += " or o.name ='ИШИМ'";
                }
                else { txt = "o.name ='ИШИМ'"; }
            }
            if (checkBox6.Checked == true)
            {
                if (checkBox3.Checked == true | checkBox2.Checked == true | checkBox4.Checked == true|checkBox5.Checked == true)
                {
                    txt += " or o.name ='ПРИЛЕГАЮЩИЕ РАЙОНЫ'";
                }
                else { txt = "o.name ='ПРИЛЕГАЮЩИЕ РАЙОНЫ'"; }
            }

 
            
            NpgsqlConnection con = new NpgsqlConnection(PATH);
            con.Open();
            string sql = "SELECT "+ STR + " FROM public.\"Apteka\" ap left join public.\"Obl\" o on ap.obl_id = o.id left join public.\"Version\" v on ap.version = v.id left join public.\"OS\" s on ap.os = s.id left join public.\"Firebird\" f on ap.firebird = f.id left join public.\"Bit\" b on ap.bit = b.id " + where+q + txt+d + aa +";";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
            ds.Reset();
            da.Fill(ds);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
            con.Close();

            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            if (dataGridView1.Columns["id"] != null)
            {
                dataGridView1.Sort(dataGridView1.Columns["id"], ListSortDirection.Ascending);
            }

        }
        public void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            olast();
        }

        private void фильтрToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            olast();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            olast();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            olast();
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            olast();
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            olast();
        }
         public void clicc()
        {
           // clic.Click += 
        }
        public void настройкиТаблицыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form fc = Application.OpenForms["Form8"];
            if (fc != null)
            {
                fc.Focus();
                fc.WindowState = FormWindowState.Maximized;

            }
            else
            {
                Form8 newForm = new Form8(this);
                newForm.Show();

            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                Settings1.Default.ChekedAktiv = true;
            }
            else { Settings1.Default.ChekedAktiv = false; }
            if ( checkBox3.Checked == true)
            {
                Settings1.Default.ChekedTum = true;
            }
            else { Settings1.Default.ChekedTum = false; }
            if (checkBox2.Checked == true)
            {
                Settings1.Default.ChekedTob = true;
            }
            else { Settings1.Default.ChekedTob = false; }
            if (checkBox4.Checked == true)
            {
                Settings1.Default.ChekedSois = true;
            }
            else { Settings1.Default.ChekedSois = false; }
            if (checkBox6.Checked == true)
            {
                Settings1.Default.ChekedPril = true;
            }
            else { Settings1.Default.ChekedPril = false; }
            if (checkBox5.Checked == true )
            {
                Settings1.Default.ChekedIshm = true;
            }
            else { Settings1.Default.ChekedIshm = false; }
            Settings1.Default.Save();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings1.Default.Save();
        }
    }
}
