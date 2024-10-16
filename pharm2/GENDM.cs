using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using Word = Microsoft.Office.Interop.Word;

namespace pharm2
{
    public partial class GENDM : Form
    {
        public GENDM()
        {
            InitializeComponent();
        }
        public static string SplitToLines(string str)
        {
            string pattern = "[^%&',;=?$\x22]+";
            return Regex.Replace(str, pattern, "$0\r\n");
        }
        string[] q;
        private void GEN(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {

                String stroka = textBox17.Text;
                listBox1.Items.Clear();
                flowLayoutPanel1.Controls.Clear();
                if (checkBox12.Checked)
                {

                }
                else { stroka = SplitToLines(textBox17.Text); }

                stroka = stroka.Replace("=", "");
                String[] s = stroka.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < s.Length; i++)
                {
                    int maxLength = 2;
                    string result = s[i].Substring(0, Math.Min(s[i].Length, maxLength));
                    if (result != "01")
                    {
                        String modified = s[i].Insert(0, "01");
                        String modifiednew = modified.Insert(16, "21");
                        s[i] = modifiednew;
                        listBox1.Items.Add(modifiednew);
                    }
                    else
                    {
                        listBox1.Items.Add(s[i] + "=");
                    }

                }



                List<PictureBox> picturebox = new List<PictureBox>();
                QRCodeWriter qrEncode = new QRCodeWriter(); //создание QR кода
                BarcodeWriter data = new BarcodeWriter();
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                q = new string[500];
                var y = 15;
                //label2.Text = listBox1.Items.Count.ToString();
                for (int i = 0; i < listBox1.Items.Count; i++)
                {

                    //string strRUS = textBox17.Text;  //строка на русском языке

                    Dictionary<EncodeHintType, object> hints = new Dictionary<EncodeHintType, object>();    //для колекции поведений
                    hints.Add(EncodeHintType.CHARACTER_SET, "utf-8");   //добавление в коллекцию кодировки utf-8

                    var bw = new BarcodeWriter
                    {
                        Format = BarcodeFormat.DATA_MATRIX,
                        Options = new EncodingOptions { Width = 120, Height = 120 }
                    };
                    var img = bw.Write(listBox1.Items[i].ToString());

                    var pb = new PictureBox();
                    pb.Location = new Point(picturebox.Count * 60 + 50, y);
                    pb.Size = new Size(50, 50);
                    try
                    {
                        pb.Image = img;
                    }
                    catch (OutOfMemoryException) { continue; }
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    pb.Name = "pic" + i;
                    flowLayoutPanel1.Controls.Add(pb);
                    picturebox.Add(pb);
                    string put = @"C:\Users\Public\Documents\" + i + ".jpg";
                    pb.Image.Save(put, ImageFormat.Jpeg);
                    q[i] = put;

                    //pictureBox1.Image = img;
                }
            }
            else
            {
                String stroka = textBox17.Text;
                listBox1.Items.Clear();
                flowLayoutPanel1.Controls.Clear();
                if (checkBox12.Checked)
                {

                }
                else { stroka = SplitToLines(textBox17.Text); }

                stroka = stroka.Replace("=", "");
                String[] s = stroka.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < s.Length; i++)
                {
                    int maxLength = 2;
                    string result = s[i].Substring(0, Math.Min(s[i].Length, maxLength));
                    if (result != "01")
                    {
                        //String modified = s[i].Insert(0, "01");
                       // String modifiednew = modified.Insert(16, "21");
                       // s[i] = modifiednew;
                        listBox1.Items.Add(s[i]);
                    }
                    else
                    {
                        listBox1.Items.Add(s[i] + "=");
                    }

                }



                List<PictureBox> picturebox = new List<PictureBox>();
                QRCodeWriter qrEncode = new QRCodeWriter(); //создание QR кода
                BarcodeWriter data = new BarcodeWriter();
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                q = new string[500];
                var y = 15;
                //label2.Text = listBox1.Items.Count.ToString();
                for (int i = 0; i < listBox1.Items.Count; i++)
                {

                    //string strRUS = textBox17.Text;  //строка на русском языке

                    Dictionary<EncodeHintType, object> hints = new Dictionary<EncodeHintType, object>();    //для колекции поведений
                    hints.Add(EncodeHintType.CHARACTER_SET, "utf-8");   //добавление в коллекцию кодировки utf-8


                    BitMatrix qrMatrix = qrEncode.encode(   //создание матрицы QR
                listBox1.Items[i].ToString(),                 //кодируемая строка
                BarcodeFormat.QR_CODE,  //формат кода, т.к. используется QRCodeWriter применяется QR_CODE
                200,                    //ширина
                200,                    //высота
                hints);                 //применение колекции поведений

                    BarcodeWriter qrWrite = new BarcodeWriter();    //класс для кодирования QR в растровом файле
                    Bitmap qrImage = qrWrite.Write(qrMatrix);   //создание изображения

                   // var bw = new BarcodeWriter
                   // {
                        
                   //     Format = BarcodeFormat.QR_CODE,
                   //     Options = new EncodingOptions {  Width = 120, Height = 120 }
                   // };
                   // var img = bw.Write(listBox1.Items[i].ToString());

                    var pb = new PictureBox();
                    pb.Location = new Point(picturebox.Count * 60 + 50, y);
                    pb.Size = new Size(20, 20);
                    try
                    {
                        pb.Image = qrImage;
                    }
                    catch (OutOfMemoryException) { continue; }
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    pb.Name = "pic" + i;
                    flowLayoutPanel1.Controls.Add(pb);
                    picturebox.Add(pb);
                    string put = @"C:\Users\Public\Documents\" + i + ".jpg";
                    pb.Image.Save(put, ImageFormat.Jpeg);
                    q[i] = put;

                    //pictureBox1.Image = img;
                }
            }
        }

        private void WORD(object sender, EventArgs e)
        {
            // Получить объект приложения Word.
            Word._Application word_app = new Word.Application();

            // Сделать Word видимым (необязательно).
            word_app.Visible = false;

            // Создаем документ Word.
            object missing = Type.Missing;
            Word._Document word_doc = word_app.Documents.Add(
                ref missing, ref missing, ref missing, ref missing);
            for (int i = 0; i < q.Length; i++)
            {
                Object oMissed = word_doc.Paragraphs[1].Range;
                Object oLinkToFile = false;
                Object oSaveWithDocument = true;
                if (q[i] != null)
                {
                    word_doc.InlineShapes.AddPicture(q[i], ref oLinkToFile, ref oSaveWithDocument, ref oMissed);
                    //  object fileName = saveFileDialog1.ToString();// @"C:\Test\NewDocument.docx";
                    File.Delete(q[i]);
                }
            }
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "|*.docx";
            saveFileDialog1.Title = "Save the Word Document";
            if (DialogResult.OK == saveFileDialog1.ShowDialog())
            {
                string docName = saveFileDialog1.FileName;
                if (docName.Length > 0)
                {
                    object oDocName = (object)docName;
                    word_doc.SaveAs(ref oDocName, ref missing, ref missing, ref missing, ref missing, ref missing,
                                 ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                                 ref missing, ref missing, ref missing, ref missing);
                }
            }
            word_doc.Close();
            word_app.Quit();
        }
    }
}
