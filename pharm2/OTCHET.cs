using FirebirdSql.Data.FirebirdClient;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pharm2
{
    public partial class OTCHET : Form
    {
        DataTable dt;
        string PATH;
        private DataSet ds = new DataSet();
        public OTCHET()
        {
            InitializeComponent();
            PATH = Settings1.Default.PATH;
            NpgsqlConnection con = new NpgsqlConnection(PATH);
            con.Open();
            string sql = "SELECT ip_server as ip,kod_parus as par,alias as al FROM \"Apteka\" ORDER BY par;";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
            ds.Reset();
            da.Fill(ds);
            dt = ds.Tables[0];
            con.Close();
            /*
            string[] qwerty = new string[dt.Rows.Count];
            string[] qwe = new string[dt.Rows.Count];
            */
            foreach (DataRow row in dt.Rows)
            {

                string[] rt = { "",row["ip"].ToString(), row["par"].ToString(), row["al"].ToString() };
                var listViewItem = new ListViewItem(rt);
                listView2.Items.Add(listViewItem);
            }
        }
        async Task<String> REP(string ip)
        {
            try
            {
                using (var connection = new FbConnection("database=" + ip + ";user=" + textBox1.Text + ";password=" + textBox2.Text))
                {
                    connection.Open();
                    using (var command = new FbCommand())
                    {
                        command.Connection = connection;//SUBTIPO = @SUBTIPO WHERE ID = @ID
                        command.CommandText = "UPDATE USERREPORTS SET TEMPLATE = @name  WHERE (CODE = '" + textBox4.Text + "')";
                        command.Parameters.Add("@name", FbDbType.Binary).Value = System.IO.File.ReadAllBytes(textBox3.Text);
                        command.ExecuteNonQuery();
                    }
                }
                richTextBox1.Text += "Успешно!" + ip + Environment.NewLine;
            }
            catch { richTextBox1.Text +="Ошибка!"+ ip + Environment.NewLine; }

            return "1";
        }
        public async Task allrep(List<string> list)
        {
            try
            { 
                for (int i = 0; i < list.Count; i++)
                {
                    string t = await REP(list[i]);
                }
            }
            catch { }

        }
        private async void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "") {
                List<string> list = new List<string>(); //делаем пустой строковой лист
                for (int i = 0; i < listView2.Items.Count; i++)
                {
                    if (listView2.Items[i].Checked)
                    {
                        list.Add(this.listView2.Items[i].SubItems[1].Text.ToString() + ':' + listView2.Items[i].SubItems[3].Text.ToString());
                    }
                }
                //REP("1");
                await allrep(list);
            }
            else { label5.Text = "Заполните все поля!"; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem listItem in listView2.Items)
            {
                listItem.Checked = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem listItem in listView2.Items)
            {
                listItem.Checked = false;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }
            textBox3.Text = filePath;
        }
    }
}
