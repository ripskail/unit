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
    public partial class SETTABL : Form
    {
        ALLAPTEKA ff;
        public SETTABL(ALLAPTEKA f)
        {
            InitializeComponent();
            ff = f;
           
        }
        string txt = "";
        List<string> term = new List<string>();

        private void checkBox26_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox27_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void checkBox28_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void checkBox25_CheckedChanged(object sender, EventArgs e)
        {

            
        }

        private void checkBox24_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox23_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void checkBox22_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox21_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox20_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox19_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox18_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox17_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox16_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox15_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void checkBox14_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        public void olast()
        {
            int w = 0;

            if (checkBox1.Checked == true)
            {
                term.Add("ap.id");
            }
            else { term.Remove("ap.id"); }
            if (checkBox3.Checked == true)
            {
                term.Add("podraz as Подразделение");
            }
            else { term.Remove("podraz as Подразделение"); }
            if (checkBox4.Checked == true)
            {
                term.Add("kod_parus as \"Код паруса\"");
            }
            else { term.Remove("kod_parus as \"Код паруса\""); }
            if (checkBox2.Checked == true)
            {
                term.Add("o.name as Область");
            }
            else { term.Remove("o.name as Область"); }
           
            if (checkBox5.Checked == true)
            {
                term.Add("adress as Адрес");
            }
            else { term.Remove("adress as Адрес"); }
            if (checkBox6.Checked == true)
            {
                term.Add("kol_rs as \"Раб.ст.\"");
            }
            else { term.Remove("kol_rs as \"Раб.ст.\""); }
            if (checkBox7.Checked == true)
            {
                term.Add("activ as Активность");
            }
            else { term.Remove("activ as Активность"); }
           
            if (checkBox8.Checked == true)
            {
                term.Add("\"Chek_server\" as \"chek server\"");
            }
            else { term.Remove("\"Chek_server\" as \"chek server\""); }
            if (checkBox9.Checked == true)
            {
                term.Add("v.name as Версия");
            }
            else { term.Remove("v.name as Версия"); }
            if (checkBox10.Checked == true)
            {
                term.Add("ip_server as IPserver");
            }
            else { term.Remove("ip_server as IPserver"); }
            if (checkBox11.Checked == true)
            {
                term.Add("elec_recept as \"элек.рецепт\"");
            }
            else { term.Remove("elec_recept as \"элек.рецепт\""); }
            if (checkBox12.Checked == true)
            {
                term.Add("acha");
            }
            else { term.Remove("acha"); }
            if (checkBox14.Checked == true)
            {
                term.Add("s.name as OS");
            }
            else { term.Remove("s.name as OS"); }
            if (checkBox13.Checked == true)
            {
                term.Add("lekarstvo as Лекарство");
            }
            else { term.Remove("lekarstvo as Лекарство"); }
            if (checkBox26.Checked == true)
            {
                term.Add("nbackup");
            }
            else { term.Remove("nbackup"); }
            if (checkBox27.Checked == true)
            {
                term.Add("prov_backup as \"Проверка Backup\"");
            }
            else
            {
                term.Remove("prov_backup as \"Проверка Backup\"");
            }
            if (checkBox28.Checked == true)
            {
                term.Add("\"RV_ip\"");
            }
            else
            {
                term.Remove("\"RV_ip\"");
            }
            if (checkBox25.Checked == true)
            {
                term.Add("pkillmono");
            }
            else { term.Remove("pkillmono"); }
            if (checkBox24.Checked == true)
            {
                term.Add("backup");
            }
            else { term.Remove("backup"); }
            if (checkBox23.Checked == true)
            {
                term.Add("checkbase");
            }
            else { term.Remove("checkbase"); }
            if (checkBox22.Checked == true)
            {
                term.Add("b.name as bit");
            }
            else { term.Remove("b.name as bit"); }
            if (checkBox21.Checked == true)
            {
                term.Add("f.name as Firebird");
            }
            else { term.Remove("f.name as Firebird"); }
            if (checkBox20.Checked == true)
            {
                term.Add("scan_recept as СКАНрецепт");
            }
            else { term.Remove("scan_recept as СКАНрецепт"); }
            if (checkBox19.Checked == true)
            {
                term.Add("comment as Комментарий");
            }
            else { term.Remove("comment as Комментарий"); }
            if (checkBox18.Checked == true)
            {
                term.Add("phone as Телефон");
            }
            else { term.Remove("phone as Телефон"); }
            if (checkBox17.Checked == true)
            {
                term.Add("alias as АлиасБД");
            }
            else { term.Remove("alias as АлиасБД"); }
            if (checkBox16.Checked == true)
            {
                term.Add("zayvka as Заявка");
            }
            else { term.Remove("zayvka as Заявка"); }
            if (checkBox15.Checked == true)
            {
                term.Add("docker");
            }
            else { term.Remove("docker"); }
            if (checkBox29.Checked == true)
            {
                term.Add("internet");
            }
            else { term.Remove("internet"); }
            if (checkBox30.Checked == true)
            {
                term.Add("oxrana");
            }
            else { term.Remove("oxrana"); }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {

            olast();


                foreach (string element in term)
            {
                txt += element;
                if (term[term.Count - 1] != element)
                {
                    txt += " ,";
                }
            }
            Settings1.Default.Findstring1 = txt;
            Settings1.Default.Save();

            ff.stroka();
            ff.обновитьToolStripMenuItem_Click(null, null);
          // ff.clic
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            checkBox3.Checked = true;
            checkBox4.Checked = true;
            checkBox5.Checked = true;
            checkBox6.Checked = true;
            checkBox7.Checked = true;
            checkBox8.Checked = true;
            checkBox9.Checked = true;
            checkBox10.Checked = true;
            checkBox11.Checked = true;
            checkBox12.Checked = true;
            checkBox13.Checked = true;
            checkBox14.Checked = true;
            checkBox15.Checked = true;
            checkBox16.Checked = true;
            checkBox17.Checked = true;
            checkBox18.Checked = true;
            checkBox19.Checked = true;
            checkBox20.Checked = true;
            checkBox21.Checked = true;
            checkBox22.Checked = true;
            checkBox23.Checked = true;
            checkBox24.Checked = true;
            checkBox25.Checked = true;
            checkBox26.Checked = true;
            checkBox27.Checked = true;
            checkBox28.Checked = true;
            checkBox29.Checked = true;
            checkBox30.Checked = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
