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
    public partial class Form7 : Form
    {
        Form1 m;
        Form2 d;
        string PATH;
        public Form7(Form1 f, Form2 fo)
        {
            InitializeComponent();
            PATH = Settings1.Default.PATH;
            oblast();
            verpo();
            win();
            fireb();
            bit();
            m = f;
            d = fo;
        }
        public void fireb()
        {
            NpgsqlConnection con = new NpgsqlConnection(PATH);
            con.Open();
            DataSet ers = new DataSet();
            string sql = "SELECT id as kod,name as nm FROM \"Firebird\";";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
            ers.Reset();
            da.Fill(ers);
            comboBox5.DataSource = ers.Tables[0];
            comboBox5.DisplayMember = "nm";
            comboBox5.ValueMember = "kod";
            con.Close();
        }
        public void bit()
        {
            NpgsqlConnection con = new NpgsqlConnection(PATH);
            con.Open();
            DataSet ers = new DataSet();
            string sql = "SELECT id as kod,name as nm FROM \"Bit\";";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
            ers.Reset();
            da.Fill(ers);
            comboBox6.DataSource = ers.Tables[0];
            comboBox6.DisplayMember = "nm";
            comboBox6.ValueMember = "kod";
            con.Close();
        }
        public void win()
        {
            NpgsqlConnection con = new NpgsqlConnection(PATH);
            con.Open();
            DataSet ers = new DataSet();
            string sql = "SELECT id as kod,name as nm FROM \"OS\";";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
            ers.Reset();
            da.Fill(ers);
            comboBox4.DataSource = ers.Tables[0];
            comboBox4.DisplayMember = "nm";
            comboBox4.ValueMember = "kod";
            con.Close();
        }
        public void verpo()
        {
            NpgsqlConnection con = new NpgsqlConnection(PATH);
            con.Open();
            DataSet ers = new DataSet();
            string sql = "SELECT id as kod,name as nm FROM \"Version\";";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
            ers.Reset();
            da.Fill(ers);
            comboBox3.DataSource = ers.Tables[0];
            comboBox3.DisplayMember = "nm";
            comboBox3.ValueMember = "kod";
            con.Close();
        }
        public void oblast()
        {
            NpgsqlConnection con = new NpgsqlConnection(PATH);
            con.Open();
            DataSet ers = new DataSet();
            string sql = "SELECT id as kod,name as nm FROM \"Obl\";";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
            ers.Reset();
            da.Fill(ers);
            comboBox2.DataSource = ers.Tables[0];
            comboBox2.DisplayMember = "nm";
            comboBox2.ValueMember = "kod";
            con.Close();
        }
        /*private void button14_Click(object sender, EventArgs e)
        {
            NpgsqlConnection con = new NpgsqlConnection("Server=192.168.146.35;Port=5432; User Id=okit;Password=okit; Database=Pharm;");
            con.Open();
            DataSet ers = new DataSet();
            string r = "UPDATE public.\"IPkass\" SET apteka_id = '" + textBox6.Text + "' WHERE id ='" + comboBox7.SelectedValue + "';";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(r, con);
            ers.Reset();
            da.Fill(ers);
            con.Close();
            listBox1.Items.Add(comboBox7.Text);
            oblast();
            verpo();
            win();
            fireb();
            bit();
            this.Refresh();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                NpgsqlConnection con = new NpgsqlConnection("Server=192.168.146.35;Port=5432; User Id=okit;Password=okit; Database=Pharm;");
                con.Open();
                DataSet ers = new DataSet();
                string r = "UPDATE public.\"IPkass\" SET apteka_id = null WHERE ip ='" + listBox1.SelectedItem + "';";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(r, con);

                ers.Reset();
                da.Fill(ers);
                con.Close();
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                oblast();
                verpo();
                win();
                fireb();
                bit();
                this.Refresh();
            }
        }*/

   

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(PATH);
                con.Open();
                DataSet ers = new DataSet();
                if (dateTimePicker1.Text == "") { dateTimePicker1.Text = "07.04.2020"; }
                string sql = "INSERT INTO public.\"Apteka\"(obl_id, podraz, kod_parus, adress, kol_rs, activ, \"Chek_server\", version, ip_server, elec_recept, acha, lekarstvo, os, docker, zayvka, alias, phone, comment, scan_recept, firebird, \"bit\", checkbase, backup, pkillmono, nbackup, prov_backup) VALUES(" + comboBox2.SelectedValue + ", '" + textBox3.Text + "', '" + textBox4.Text + "', '" + textBox2.Text + "', '" + textBox5.Text + "', '" + checkBox10.Checked + "', '" + checkBox1.Checked + "', " + comboBox3.SelectedValue + ", '" + textBox7.Text + "', '" + checkBox2.Checked + "','" + checkBox3.Checked + "', '" + checkBox4.Checked + "', '" + comboBox4.SelectedValue + "', '" + checkBox5.Checked + "', '" + checkBox6.Checked + "', '" + textBox9.Text + "', '" + textBox16.Text + "', '" + textBox15.Text + "', '" + textBox10.Text + "', '" + comboBox5.SelectedValue + "','" + comboBox6.SelectedValue + "','" + checkBox3.Checked + "', '" + checkBox7.Checked + "','" + checkBox9.Checked + "','" + checkBox8.Checked + "', '" + dateTimePicker1.Value + "');";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                ers.Reset();
                da.Fill(ers);
                con.Close();
                oblast();
                verpo();
                win();
                fireb();
                bit();
                m.connect();
                d.fill();
                this.Refresh();
                this.Close();
            }
            catch { }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
