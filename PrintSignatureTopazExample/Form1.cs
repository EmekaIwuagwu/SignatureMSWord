using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;


namespace PrintSignatureTopazExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string constring = @"Data Source=DESKTOP-JO50TII\SQLEXPRESS;Initial Catalog=SignatureBox;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(constring))
            {
                con.Open();
                string query = "select * from MySignatureTable where SignatureHolder = @SignatureHolder";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@SignatureHolder", textBox1.Text);
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            string fileName = @"C:\Users\emi\Desktop\test.jpg";
                            byte[] imageBytes = Convert.FromBase64String(rd["SignatureBase64"].ToString());
                            MemoryStream ms = new MemoryStream(imageBytes, 0,imageBytes.Length);
                            ms.Write(imageBytes, 0, imageBytes.Length);
                            Image image = Image.FromStream(ms, true,true);
                            image.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);

                            Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
                            Microsoft.Office.Interop.Word.Document doc = null;

                            try
                            {
                                doc = app.Documents.Open(@"C:\Users\emi\Desktop\sign1.docx", Type.Missing);
                                var Signature2Ctrl = doc.SelectContentControlsByTag("Signature2");
                                var testingCtrl = Signature2Ctrl[1];
                                testingCtrl.Range.InlineShapes.AddPicture(fileName, Type.Missing, Type.Missing, Type.Missing);
                                doc.Save();
                                MessageBox.Show("Complete!");
                            }
                            catch(Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                        }
                    }
                }
            }
        }
    }
}
