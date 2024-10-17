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
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
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
            string sql = "SELECT ap.ip_server as ip,ap.kod_parus as par,ap.alias as al,pp.word as pas FROM \"Apteka\" ap,\"pass\" pp WHERE pp.id = ap.pass_id ORDER BY par;";
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

                string[] rt = { "", row["ip"].ToString(), row["par"].ToString(), row["al"].ToString(), row["pas"].ToString() };
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

        public void FDB( string dms,string user,string pass,string pyt)
        {
            string[] words = dms.Split(new char[] { ':' });
            int check = pingip(words[0]);
            //this.richTextBox1.Invoke((MethodInvoker)delegate {
                // Running on the UI thread
            //    this.richTextBox1.AppendText(dms +" - "+ check.ToString() + Environment.NewLine);
            //});
            
            if (check == 1)
            {
                string[] kodpass = dms.Split(new char[] { '{' });
                string[] kod = kodpass[1].Split(new char[] { '}' });
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                //startInfo.CreateNoWindow = true;
                string cd = "\"C:\\Program Files (x86)\\Firebird\\Firebird_2_5\\bin\\isql.exe\"";
                startInfo.Arguments = "/C "+cd+" -user "+ user +" -password "+ pass +" "+ kodpass[0] + " -ch WIN1251 -m -i "+ pyt;
                process.StartInfo = startInfo;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.Start();
                //* Read the output (or the error)
                string output = process.StandardOutput.ReadToEnd();
                //Console.WriteLine(output);
                string err = process.StandardError.ReadToEnd();
                //Console.WriteLine(err);
                process.WaitForExit();



                if(output == "")
                {
                    
                        this.treeView1.Invoke((MethodInvoker)delegate
                        {
                            // Running on the UI thread
                            this.treeView1.Nodes[0].Nodes.Add(new TreeNode(dms));
                        });
                        this.label5.Invoke((MethodInvoker)delegate
                        {
                            yes += 1;
                            this.label5.Text = yes.ToString();
                        });

                        this.listView2.Invoke((MethodInvoker)delegate
                        {
                            ListViewItem item1 = listView2.FindItemWithText(kod[0], true, 0);
                            if (item1 != null)
                            {
                                this.listView2.Items[item1.Index].BackColor = Color.Green;
                            }
                        });

                }
                else
                {
                    
                        this.richTextBox1.Invoke((MethodInvoker)delegate {
                            // Running on the UI thread
                            this.richTextBox1.AppendText(dms + " - " + output + Environment.NewLine);
                        });
                        this.treeView1.Invoke((MethodInvoker)delegate {
                            // Running on the UI thread
                            this.treeView1.Nodes[1].Nodes.Add(new TreeNode(dms));
                        });
                        this.label7.Invoke((MethodInvoker)delegate {
                            no += 1;
                            this.label7.Text = no.ToString();
                        });
                        this.listView2.Invoke((MethodInvoker)delegate
                        {
                            ListViewItem item1 = listView2.FindItemWithText(kod[0], true, 0);
                            if (item1 != null)
                            {
                                this.listView2.Items[item1.Index].BackColor = Color.Red;
                            }
                        });
                    
                }  
                
            }
            else
            {
                this.treeView1.Invoke((MethodInvoker)delegate {
                    // Running on the UI thread
                    this.treeView1.Nodes[1].Nodes.Add(new TreeNode(dms));
                });
                this.label7.Invoke((MethodInvoker)delegate {
                    no += 1;
                    this.label7.Text = no.ToString();
                });
            }

        }
 
        public bool chekip(string ip)
        {
            if (string.IsNullOrEmpty(ip))
            {
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
                    this.richTextBox1.Invoke((MethodInvoker)delegate {
                        // Running on the UI thread
                        this.richTextBox1.AppendText("Не верный IP!!! - " + ip + Environment.NewLine);
                    });
                    
                    return false;
                }
            }

        }
        PingReply rep;
        int pingip(String pp)
        {
            int st = 0;
            String mac = "";
            if (string.IsNullOrEmpty(pp))
            {
                richTextBox1.AppendText("Нет IP!!! - " + pp + Environment.NewLine);
            }
            else
            {
                if (chekip(pp))
                {
                    mac = pp;

                }
                else { richTextBox1.AppendText("Ошибка IP!!! - " + pp + Environment.NewLine); }
            }

            Ping pingreq = new Ping();

            try
            {
               // richTextBox1.Clear();

                Ping ping =
            new Ping();
                PingReply pingReply = ping.Send(mac);
               // richTextBox1.Text += " " + pingReply.RoundtripTime; //время ответа
               // richTextBox1.Text += " " + pingReply.Status;        //статус
               // richTextBox1.Text += " " + pingReply.Address;       //IP

                rep = pingreq.Send(mac);
                // richTextBox1.Text += "  Reply From {" + rep.Address.ToString() + "} : time={" + rep.RoundtripTime + "} TTL={" + rep.Options.Ttl + "}" + "\r\n";
                this.richTextBox1.Invoke((MethodInvoker)delegate {
                    // Running on the UI thread
                    this.richTextBox1.AppendText("ping - " + mac + Environment.NewLine);
                    st =1;
                });
                

            }
            catch (Exception) // тут перехватывайте нужный тип исключения
            {
                st = 0;
            }
            finally
            {
                
            }
            return st;
        }
        int yes = 0;
        int no = 0;
        private void button4_Click(object sender, EventArgs e)
        {
           
            treeView1.Nodes.Clear();
            richTextBox1.Clear();
            list.Clear();
            for (int i = 0; i < listView2.Items.Count; i++)
            {

                if (listView2.Items[i].Checked)
                {

                    list.Add(this.listView2.Items[i].SubItems[1].Text.ToString() + ':' + listView2.Items[i].SubItems[3].Text.ToString() + '{' + listView2.Items[i].SubItems[2].Text.ToString()+'}'+ listView2.Items[i].SubItems[4].Text.ToString());
                }



            }
            TreeNode us = new TreeNode("Успешно");
            TreeNode oo = new TreeNode("Ошибка");
            treeView1.Nodes.Add(us);
            treeView1.Nodes.Add(oo);


            int ind = 1;
            foreach (var item in list)
            {
                //treeView1.Nodes[0].Nodes.Add(new TreeNode(item));
                string[] pp = item.Split(new char[] { '}' });
                List<Thread> ThrArr = new List<Thread>();
                Thread devthread = new Thread(() => FDB(item,"SYSDBA", pp[1], textBox3.Text));
                devthread.Name = "thr" + ind.ToString();
                ThrArr.Add(devthread);
                devthread.Start();
                richTextBox1.AppendText("Поток зап - " + devthread.Name + Environment.NewLine);
                
                ind++;
            }
            /*
            for (int i = 0; i < list.Count; i++)
            {
                if (await Task.Run(() => ExecuteBatchScript("database=" + list[i] + ";user=" + textBox1.Text + ";password=" + textBox2.Text, textBox3.Text, listOtvet)))
                {
                    listOtvet.Add("Успешно!" + list[i]);
                }
                else { listOtvet.Add("Ошибка!" + list[i]); }

            }

            richTextBox1.Clear();
            for (int i = 0; i < listOtvet.Count; i++)
            {
                richTextBox1.Text += listOtvet[i] + Environment.NewLine;
            }*/

        }

     

        private void SCRIPT_Load(object sender, EventArgs e)
        {

        }
    }
}
