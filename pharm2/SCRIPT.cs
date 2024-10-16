using FirebirdSql.Data.FirebirdClient;
using FirebirdSql.Data.Isql;
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
    public partial class SCRIPT : Form
    {
        DataTable dt;
        string PATH;
        private DataSet ds = new DataSet();
        public SCRIPT()
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

                string[] rt = { "", row["ip"].ToString(), row["par"].ToString(), row["al"].ToString() };
                var listViewItem = new ListViewItem(rt);
                listView2.Items.Add(listViewItem);
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
            textBox4.Text = System.IO.File.ReadAllText(textBox3.Text);
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
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            
           

        }
        List<string> listOtvet = new List<string>(); //делаем пустой строковой лист
        List<string> list = new List<string>(); //делаем пустой строковой лист

  

        private static void Script_UnknownStatement(object sender, UnknownStatementEventArgs e)
        {
            e.NewStatementType = SqlStatementType.Update;
            e.Handled = true;
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            list.Clear();
            for (int i = 0; i < listView2.Items.Count; i++)
            {
                if (listView2.Items[i].Checked)
                {
                    list.Add(this.listView2.Items[i].SubItems[1].Text.ToString() + ':' + listView2.Items[i].SubItems[3].Text.ToString());
                }
            }
           for (int i = 0; i < list.Count; i++)
            {
                //await Task.Run(() => ExecuteBatchScript("database=" + list[i] + ";user=" + textBox1.Text + ";password=" + textBox2.Text, textBox3.Text, listOtvet));
                if (await Task.Run(() => ExecuteBatchScript("database=" + list[i] + ";user=" + textBox1.Text + ";password=" + textBox2.Text, textBox3.Text, listOtvet)))
                {
                    listOtvet.Add("Успешно!" + list[i]); // richTextBox1.Text += "Успешно!" + list[i] + Environment.NewLine;
                }
                else { listOtvet.Add("Ошибка!" + list[i]);/* richTextBox1.Text += "Ошибка!" + list[i] + Environment.NewLine;*/  }

                //ExecuteBatchScript("database=" + list[i] + ";user=" + textBox1.Text + ";password=" + textBox2.Text, textBox3.Text);
            }
            //backgroundWorker1.RunWorkerAsync(2000);
            richTextBox1.Clear();
            for (int i = 0; i < listOtvet.Count; i++)
            {
                richTextBox1.Text += listOtvet[i] + Environment.NewLine;
            }
        }
        public static  bool  ExecuteBatchScript(
            string connectionString,
            string pathToScriptFile, List<string> listOtvet)
          
        {
           // FbConnection fbConnection = null;
            try
            {
                var r = new StreamReader(pathToScriptFile, Encoding.GetEncoding("windows-1251"));
                
                    FbScript script = new FbScript(r.ReadToEnd());
                    script.Parse();
                
                //StreamReader sr = File.OpenText(pathToScriptFile, Encoding.UTF8);
                //FbScript script = new FbScript(sr.ReadToEnd());
                //script.Parse();
                using (FbConnection fbConnection = new FbConnection(connectionString))
                {
                    var execution = new FbBatchExecution(fbConnection);
                    execution.AppendSqlStatements(script);
                    execution.Execute(true);
                
                    //fbConnection.Close();
                   // fbConnection.Dispose();
                }
                //listOtvet.Add("Успешно!" + connectionString);
            }
            catch(Exception ex) {
                //listOtvet.Add("Ошибка!" + connectionString);
                //fbConnection.Close();
                //fbConnection.Dispose();
                listOtvet.Add("Ошибка!" + ex);
                return false;
            }
            return true;
        }


        private static void CopyParameters(FbCommand source, FbCommand inner)
        {
            foreach (FbParameter parameter in source.Parameters)
            {
                inner.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
            }
        }
        private void backgroundWorker1_DoWork_1(object sender, DoWorkEventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" )//&& textBox3.Text != "")
            {
                
                for (int i = 0; i < list.Count; i++)
                {

                    try
                    {
                        //if(ExecuteBatchScript("database=" + list[i] + ";user=" + textBox1.Text + ";password=" + textBox2.Text, textBox3.Text))
                        //{
                        //    listOtvet.Add("Успешно!" + list[i]); // richTextBox1.Text += "Успешно!" + list[i] + Environment.NewLine;
                        //}
                        //else { listOtvet.Add("Ошибка!" + list[i]);/* richTextBox1.Text += "Ошибка!" + list[i] + Environment.NewLine;*/
                       // }
                    }
                    catch { listOtvet.Add("Ошибка!" + list[i]);/* richTextBox1.Text += "Ошибка!" + list[i] + Environment.NewLine;*/  }

                    //string t = await REP(list[i]);
                }

            }

            Thread.Sleep(2000);

        }

        private void backgroundWorker1_RunWorkerCompleted_1(object sender, RunWorkerCompletedEventArgs e)
        {
            string b = (string)e.Result;
            //MessageBox.Show(b);
        }
    }
}
