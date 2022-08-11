using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Renci.SshNet;
using Renci.SshNet.Common;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using Npgsql;


namespace pharm2
{
    public partial class Form1 : Form
    {
        System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
        int i = 0;
        int j = 0;
        DataTable dt,all;
        string ip,PATH,STR;
        private DataSet ds = new DataSet();
        string mac;
        public Form1()
        {

            InitializeComponent();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 10000;
            if (Settings1.Default.VNC != null && Settings1.Default.PYTTU != null && Settings1.Default.IB != null && Settings1.Default.WINS != null && Settings1.Default.PATH != null)
            {
                VNC1 = Settings1.Default.VNC;
                PUTTY = Settings1.Default.PYTTU;
                IB = Settings1.Default.IB;
                WINS = Settings1.Default.WINS;
                PATH = Settings1.Default.PATH;
                
            }
            else
            {
                Form4 newForm = new Form4(this);
                newForm.Show();
            }
            call();
            connect();
            ipkass();
            oblast();
            verpo();
            win();
            fireb();
            bit();
            ipkass();

            comboBox1.SelectedItem = 267;
            comboBox1.Focus();
        }
   
            public void cl()
        {
            NpgsqlConnection con = new NpgsqlConnection(PATH);
            con.Open();
            string sql = "SELECT * FROM \"Apteka\";";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
            ds.Reset();
            da.Fill(ds);
            dt = ds.Tables[0];
            con.Close();
            comboBox1.Update();
            comboBox1.Refresh();
        }
        public void call()
        {
            NpgsqlConnection con = new NpgsqlConnection(PATH);
            con.Open();
            string sql = "SELECT * FROM \"Apteka\";";
            DataSet erww = new DataSet();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
            erww.Reset();
            da.Fill(erww);
            all = erww.Tables[0];
            con.Close();
        }
            public void connect()
        {
            NpgsqlConnection con = new NpgsqlConnection(PATH);
            con.Open();
            string sql = "SELECT id as kod,kod_parus as par FROM \"Apteka\" ORDER BY par;";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
            ds.Reset();
            da.Fill(ds);
            dt = ds.Tables[0];
            con.Close();
            /*
            string[] qwerty = new string[dt.Rows.Count];
            string[] qwe = new string[dt.Rows.Count];
            foreach (DataRow row in dt.Rows)
            {
                qwerty[j] = row["kod_parus"].ToString();
                qwe[j] = row["id"].ToString();
                j++;
            }
            j = 0;
          Array.Sort(qwerty);
            comboBox1.DataSource = qwerty;
            comboBox1.DisplayMember = qwerty;
            */
            comboBox1.DataSource = ds.Tables[0];
            comboBox1.DisplayMember = "par";
            comboBox1.ValueMember = "kod";
           // comboBox1.Sorted.ToString();
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


        public void ipkass()
        {
            DataSet dss = new DataSet();
        NpgsqlConnection con = new NpgsqlConnection(PATH);
            con.Open();
            string sql = "SELECT r.id, r.ip, w.kod_parus FROM public.\"IPkass\" r left join \"Apteka\" w ON r.apteka_id = w.id;";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
            dss.Reset();
            da.Fill(dss);
            DataTable dtt;
            dtt = dss.Tables[0];
            con.Close();
            listBox1.Items.Clear();
            try
            {
                foreach (DataRow row in dtt.Rows)
                {
                    if (comboBox1.SelectedValue.ToString() != "")
                    {
                        if (row["kod_parus"].ToString() == comboBox1.Text.ToString())
                        {
                            string str = row["ip"].ToString();
                            listBox1.Items.Add(str);
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                    }
                }
            }
            catch { }
           
            comboBox7.DataSource = dss.Tables[0];
            comboBox7.DisplayMember = "ip";
            comboBox7.ValueMember = "id";
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            button3.Enabled = true;
            i = 0;
            timer1.Stop();
        }
        PingReply rep;
        public bool chekip(string ip)
        {
            if (string.IsNullOrEmpty(ip))
            {
                MessageBox.Show("Введите IP");
                return false;
            }
            else
            {
                string pattern = @"\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b";
                if (Regex.IsMatch(ip, pattern))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Не верный IP!!!");
                    textBox14.Clear();
                    return false;
                }
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(textBox14.Text.Trim()))
            {
                mac = ip;
            }
            else {
                if (chekip(textBox14.Text))
                {
                    mac = textBox14.Text.Trim();

                }
                else
                {
                    mac = ip;

                }
            }

                Ping pingreq = new Ping();

                try
                {
                    textBox1.Clear();

                Ping ping =
            new Ping();
                PingReply pingReply = ping.Send(mac);
                textBox1.Text +=" "+ pingReply.RoundtripTime; //время ответа
                textBox1.Text += " " + pingReply.Status;        //статус
                textBox1.Text += " " + pingReply.Address;       //IP

                        rep = pingreq.Send(mac);
                        textBox1.Text += "  Reply From {" + rep.Address.ToString() + "} : time={" + rep.RoundtripTime + "} TTL={" + rep.Options.Ttl + "}" + "\r\n";
                    
                }
                catch (Exception) // тут перехватывайте нужный тип исключения
                {
                    MessageBox.Show("Нет связи c "+ mac);
                }
                finally
                {

                }
            

        }
      
        private void DoCommand()
        {
            if (string.IsNullOrEmpty(textBox14.Text.Trim()))
            {
                mac = ip;
            }
            else
            {
                if (chekip(textBox14.Text.Trim()))
                {
                    mac = textBox14.Text.Trim();

                }
                else
                {
                    mac = ip;

                }
            }
            string u = "devel";
            string p = "devel";
            if (string.IsNullOrEmpty(mac)) { MessageBox.Show("Выберите IP"); }
            else { 
                PasswordConnectionInfo connectionInfo = new PasswordConnectionInfo(mac, u, p);
            connectionInfo.Timeout = TimeSpan.FromSeconds(30);

                using (var client = new SshClient(connectionInfo))
                {

                    try
                    {
                        client.Connect();
                        if (client.IsConnected)
                        {

                            var command = client.RunCommand("x11vnc");

                            var result = command.Result;
                            result = result.Substring(0, result.Length - 1);

                            textBox1.Text = "SSH connection active " + result;

                        }
                        else
                        {
                            textBox1.Text = "SSH connection NOTactive";
                        }
                    }
                    catch (Exception) // тут перехватывайте нужный тип исключения
                    {
                        MessageBox.Show("Связь разорвана!!!");
                        client.Disconnect();
                        // textBox1.Text = " Связь SSH разорвана";
                    }
                    finally
                    {
                        client.Disconnect();
                    }
                }
            }
        }
        private async void button2_ClickAsync(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox14.Text.Trim()))
            {
                if (string.IsNullOrEmpty(mac)) { MessageBox.Show("Выберите IP"); }
                else
                {
                    // MessageBox.Show("Пустое поле!");
                    mac = ip;
                    // exit;
                    button3.Enabled = false;
                    timer1.Start();
                    MessageBox.Show("Подождите 10 сек... после этого нажмите  кнопку VNC");
                    button2.Enabled = false;
                    textBox1.Text = "Подключение по PUTTY...";
                    try
                    {
                        await Task.Run(new Action(DoCommand));
                    }
                    catch (Exception) // тут перехватывайте нужный тип исключения
                    {
                        MessageBox.Show("Не удалось выполнить команду");
                    }
                    finally
                    {
                        textBox1.Text = " Связь SSH разорвана";
                        button2.Enabled = true;
                    }
                }
            }
            else
            {
                if (chekip(textBox14.Text.Trim()))
                {
                    button3.Enabled = false;
                    timer1.Start();
                    MessageBox.Show("Подождите 10 сек... после этого нажмите  кнопку VNC");
                    button2.Enabled = false;
                    textBox1.Text = "Подключение по PUTTY...";
                    try
                    {
                        await Task.Run(new Action(DoCommand));
                    }
                    catch (Exception) // тут перехватывайте нужный тип исключения
                    {
                        MessageBox.Show("Не удалось выполнить команду");
                    }
                    finally
                    {
                        textBox1.Text = " Связь SSH разорвана";
                        button2.Enabled = true;
                    }
                }
            }
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox14.Text.Trim()))
            {
                mac = ip;
            }
            else
            {
                if (chekip(textBox14.Text.Trim()))
                {
                    mac = textBox14.Text.Trim();

                }
                else
                {
                    mac = ip;

                }
            }
           // string hostname = ip; /*ip к чему будешь коннектится*/
            ProcessStartInfo VNC = new ProcessStartInfo();
            VNC.FileName = (@""+ VNC1);/*путь к vncviewer*/
            VNC.Arguments = mac;
            Process.Start(VNC);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox14.Text.Trim()))
            {
                mac = ip;
            }
            else
            {
                if (chekip(textBox14.Text.Trim()))
                {
                    mac = textBox14.Text.Trim();

                }
                else
                {
                    mac = ip;

                }
            }
            //string hostname = ip; /*ip к чему будешь коннектится*/
            ProcessStartInfo IB = new ProcessStartInfo();
            IB.FileName = (@""+ IB);/*путь к vncviewer*/
            if (Process.GetProcessesByName("IBExpert").Any())
            {
                System.Diagnostics.Process[] p = System.Diagnostics.Process.GetProcessesByName("IBExpert");
                if (p.Length > 0)
                {
                    ShowWindow(p[0].MainWindowHandle, 10);
                    ShowWindow(p[0].MainWindowHandle, 5);
                    SetForegroundWindow(p[0].MainWindowHandle);
                }
            }
            else
            {
                Process.Start(IB);
            }
        }
        [DllImport("user32.dll")]
        private static extern int SetForegroundWindow(IntPtr hWnd);

        private const int SW_SHOWNORMAL = 10;
        private const int SW_SHOWMAXIMIZED = 3;
        private const int SW_RESTORE = 5;

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        public void SW(string q)
        {
            comboBox1.SelectedValue = q;
            this.WindowState = FormWindowState.Normal;
            this.Focus();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form fc = Application.OpenForms["Form2"];
            if (fc != null)
            {
                fc.Focus();
                fc.WindowState = FormWindowState.Maximized;

            }
            else
            {
                Form2 newForm = new Form2(this);
                newForm.Show();
               
            }
           
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            //cl();
            ipkass();
 
            int w = 0;
            //listView1.Items.Clear();
           
                textBox3.Text = comboBox1.SelectedText.ToString();
                foreach (DataRow row in all.Rows)
                {
                    if (row["id"].ToString() == comboBox1.SelectedValue.ToString() )
                    {
                        textBox6.Text = row["id"].ToString();
                        //  textBox2.Text = row["obl_id"].ToString();
                        // comboBox2.Items.IndexOf(row["obl_id"].ToString());
                        comboBox2.SelectedValue = Convert.ToInt32(row["obl_id"].ToString());
                        textBox4.Text = row["kod_parus"].ToString();
                        textBox5.Text = row["kol_rs"].ToString();
                        textBox3.Text = row[2].ToString();
                        // textBox6.Text = row[8].ToString();
                        comboBox3.SelectedValue = Convert.ToInt32(row["version"].ToString());
                        textBox7.Text = row["ip_server"].ToString();
                        listBox1.Items.Add(row["ip_server"].ToString());
                        textBox2.Text = row["adress"].ToString();
                        //   textBox8.Text = row[13].ToString();
                        comboBox4.SelectedValue = Convert.ToInt32(row["os"].ToString());
                        textBox9.Text = row["alias"].ToString();
                        textBox10.Text = row["scan_recept"].ToString();
                        string st = row[17].ToString();
                        string[] myst = st.Split(',');

                        foreach (var s in myst)
                            textBox16.Text = s;
                        w++;

                        // textBox12.Text = row["firebird"].ToString();
                        comboBox5.SelectedValue = Convert.ToInt32(row["firebird"].ToString());
                        // textBox13.Text = row["bit"].ToString();
                        comboBox6.SelectedValue = Convert.ToInt32(row["bit"].ToString());
                        dateTimePicker1.Text = row["prov_backup"].ToString();
                        textBox15.Text = row["comment"].ToString();
                    textBox11.Text = row["internet"].ToString();
                    textBox12.Text = row["oxrana"].ToString();
                    textBox8.Text = row["RV_ip"].ToString();

                        checkBox1.Checked = proverka(row["Chek_server"].ToString());
                        checkBox2.Checked = proverka(row["elec_recept"].ToString());
                        checkBox4.Checked = proverka(row["lekarstvo"].ToString());
                        checkBox5.Checked = proverka(row["Docker"].ToString());
                        checkBox6.Checked = proverka(row["zayvka"].ToString());
                        checkBox3.Checked = proverka(row["acha"].ToString());
                        checkBox7.Checked = proverka(row["backup"].ToString());
                        checkBox8.Checked = proverka(row["nbackup"].ToString());
                        checkBox9.Checked = proverka(row["pkillmono"].ToString());
                        checkBox10.Checked = proverka(row["activ"].ToString());
                }
                }
                w = 0;
          

        }
        public bool proverka(string row)
        {
            if (row == "True")
            {
                return  true;
            }
            return false;
        }
        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                ip = listBox1.SelectedItem.ToString();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox14.Clear();
        }

        private void button10_Click(object sender, EventArgs e)
        {
         
        }

        private void button12_Click(object sender, EventArgs e)
        {
           
        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            button14.Visible = true;
            comboBox1.Enabled = false;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0) return;
            var tmpValue = listBox1.Items[listBox1.SelectedIndex].ToString();
            textBox14.Text = tmpValue;
            mac = tmpValue;
        }
      
            DataSet er = new DataSet();
        DataTable rt = new DataTable();
        string gg;
        private void but()
        {
            NpgsqlConnection con = new NpgsqlConnection(PATH);
            con.Open();
            string r = "SELECT id from public.\"IPkass\" WHERE ip ='" + comboBox7.Text + "';";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(r, con);
            er.Reset();
            da.Fill(er);
            rt = er.Tables[0];
            con.Close();
            foreach (DataRow row in rt.Rows)
            {
                gg = row["id"].ToString();
            }

            }
        private void button14_Click(object sender, EventArgs e)
        {
            but();
            NpgsqlConnection con = new NpgsqlConnection(PATH);
            con.Open();
            DataSet ers = new DataSet();
            string r = "UPDATE public.\"IPkass\" SET apteka_id = '" + textBox6.Text + "' WHERE id ='" + gg + "';";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(r, con);
            ers.Reset();
            da.Fill(ers);
            con.Close();
            listBox1.Items.Add(comboBox7.Text);
            connect();
            comboBox1.Refresh();
            oblast();
            verpo();
            win();
            fireb();
            bit();
            this.Refresh();
            //Form3 newForm = new Form3(dt, comboBox1.SelectedValue.ToString(),1);
            // newForm.Show();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {
            Form3 newForm = new Form3(all, comboBox1.SelectedValue.ToString(), 2);
            newForm.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
           // cl();
            connect();
          comboBox1.Refresh();
            oblast();
            verpo();
            win();
            fireb();
            bit();
          //  cl();
            this.Refresh();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ProcessStartInfo put = new ProcessStartInfo();
            put.FileName = (@""+PUTTY);/*путь к PUTTY*/
            put.Arguments = mac;
            Process.Start(put);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 newForm = new Form4(this);
            newForm.Show();
        }
        string VNC1, IB, PUTTY,WINS;

        private void областьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 newForm = new Form5(4);
            newForm.Show();
        }

        private void verПриложенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 newForm = new Form5(1);
            newForm.Show();
        }

        private void разрядСистемыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 newForm = new Form5(2);
            newForm.Show();
        }

        private void верFirebirdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 newForm = new Form5(3);
            newForm.Show();
        }

        private void oSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 newForm = new Form5(5);
            newForm.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedItem != null){
                NpgsqlConnection con = new NpgsqlConnection(PATH);
                con.Open();
                DataSet ers = new DataSet();
                string r = "UPDATE public.\"IPkass\" SET apteka_id = null WHERE ip ='" + listBox1.SelectedItem + "';";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(r, con);

                ers.Reset();
                da.Fill(ers);
                con.Close();
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                connect();
                comboBox1.Refresh();
                oblast();
                verpo();
               win();
                fireb();
                bit();
                this.Refresh();
            }
        }
        int index;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //comboBox1.DroppedDown = true;
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            index = comboBox1.FindString(comboBox1.Text);
          
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            comboBox1.DroppedDown = true;
        }

        private void iPКассToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 newForm = new Form6();
            newForm.Show();
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox7_Click(object sender, EventArgs e)
        {
            comboBox7.DroppedDown = true;
        }

        private void comboBox7_TextChanged(object sender, EventArgs e)
        {
            index = comboBox7.FindString(comboBox7.Text);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void аптекиToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            ProcessStartInfo wins = new ProcessStartInfo();
            wins.FileName = (@"" + WINS);/*путь к vncviewer*/
            wins.Arguments = mac;
            Process.Start(wins);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            NpgsqlConnection con = new NpgsqlConnection(PATH);
            con.Open();
            DataSet ers = new DataSet();
            if(dateTimePicker1.Text == "") { dateTimePicker1.Text = "07.04.2020"; }
                string sql = "UPDATE public.\"Apteka\" SET obl_id ="+comboBox2.SelectedValue+", podraz ='"+textBox3.Text+"', kod_parus ='"+ textBox4.Text + "', adress ='" + textBox2.Text + "', kol_rs ='" + textBox5.Text + "', activ ='" + checkBox10.Checked + "', \"Chek_server\"='" + checkBox1.Checked + "', \"version\" =" + comboBox3.SelectedValue + ", ip_server ='" + textBox7.Text + "', elec_recept ='" + checkBox2.Checked + "', acha ='" + checkBox3.Checked + "', lekarstvo ='" + checkBox4.Checked + "', os ='" +comboBox4.SelectedValue+ "', docker ='" + checkBox5.Checked + "', zayvka ='" + checkBox6.Checked + "', alias ='" + textBox9.Text + "', phone ='" + textBox16.Text + "', comment ='" + textBox15.Text + "', scan_recept ='" + textBox10.Text + "', firebird ='" + comboBox5.SelectedValue + "', \"bit\"='" + comboBox6.SelectedValue + "', checkbase ='" + checkBox3.Checked + "', backup ='" + checkBox7.Checked + "', pkillmono ='" + checkBox9.Checked + "', nbackup ='" + checkBox8.Checked + "', prov_backup ='" + dateTimePicker1.Value + "', \"RV_ip\" ='" + textBox8.Text + "', internet ='" + textBox11.Text + "', oxrana ='" + textBox12.Text + "' WHERE id ='" + comboBox1.SelectedValue.ToString()+"';";
        NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
            ers.Reset();
            da.Fill(ers);
            con.Close();
            call();
           // connect();
            comboBox1.Refresh();
            //oblast();
            //verpo();
            //win();
            //fireb();
           // bit();
            this.Refresh();
        }

        public void path(string v, string i, string p)
        {
            VNC1 = v;
            IB = i;
            PUTTY = p;
        }
    }
}
