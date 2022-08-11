using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace pharm2
{
    public partial class Form3 : Form
    {
        DataTable r;
        string code;
        int flag;
        int i;
        public Form3(DataTable w,string m,int f)
        {
            InitializeComponent();
            r = w;
            code = m;
            flag = f;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0) return;
            var tmpValue = listBox1.Items[listBox1.SelectedIndex].ToString();
            textBox1.Text = tmpValue;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(textBox1.Text);
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            i = 0;
            foreach (DataRow row in r.Rows) 
            {
                i++;
                if (row["kod_parus"].ToString() == code)
                {
                   
                    if (flag == 1)
                    {
                        listBox1.Items.Add(row["Выделенная связь"].ToString());
                        //row["Значение"] = 10;
                        string str = row[29].ToString();
                        string[] mystring = str.Split(',');
                        foreach (var s in mystring)
                            listBox1.Items.Add(s);
                    }
                    if (flag == 2)
                    {
                        //textBox5.Text = row["Рабочих мест"].ToString();
                        string st = row[18].ToString();
                        string[] myst = st.Split(',');
                        foreach (var s in myst)
                            listBox1.Items.Add(s);
                    }
                    break;

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items[listBox1.SelectedIndex] = textBox1.Text;

        }
        public int sqwerty()
        {
            string[] items = listBox1.Items
            .OfType<object>()
            .Select(item => item.ToString())
            .ToArray();
            string result = string.Join(",", items);
            i = 0;
            foreach (DataRow row in r.Rows)
            {
                if (row["Код Паруса"].ToString() == code)
                {
                    i++;
                }
                return i;
            }
            return i;
        }
        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            int r;
            r = i+1;
            Excel.Application app = new Excel.Application();
            string fileName = "Z:\\prog\\NoDel.xlsx"; //имя Excel файла
            app.Workbooks.Open(fileName);
            Excel.Workbook book = app.ActiveWorkbook;
                Excel.Worksheet sheet = (Excel.Worksheet)book.Sheets[1];
            string[] items = listBox1.Items
            .OfType<object>()
            .Select(item => item.ToString())
            .ToArray();
            string result = string.Join(",", items);
            sheet.Cells[r, "AD"] = result;
            book.Save();
            app.Quit();
            //сохраняем файл
            // Form1 ne = new Form1(dt);
            // ne.Show();
            // dt.Rows[1][3] = "Value";
            /*
            string[] items = listBox1.Items
            .OfType<object>()
            .Select(item => item.ToString())
            .ToArray();
            string result = string.Join(",", items);
            string fileName = "Z:\\prog\\NoDel.xlsx"; //имя Excel файла  
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWb = xlApp.Workbooks.Open(fileName); //открываем Excel файл
            Excel.Worksheet xlSht = xlWb.Sheets[1]; //первый лист в файле
            string iLastRow = xlSht.Cells[i, "AD"].Value = result;  //последняя заполненная строка в столбце А
          //  for (int i = 1; i < 51; i++)
          //  {
          //      iLastRow++;
          //      xlSht.Cells[iLastRow, "A"].Value = i.ToString();
         //   }
            //xlApp.Visible = true;
            xlWb.Close(true); //закрыть и сохранить книгу
            xlApp.Quit();
            MessageBox.Show("Файл успешно сохранён!");
            */
        }
    }
}
