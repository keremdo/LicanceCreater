using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace LicanceCreater
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void btnLicanceCreate_Click(object sender, EventArgs e)
        {
            try
            {
                // Xmals adlı klasörün yolu
                string xmalsFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Xmals");

                // Eğer Xmals klasörü yoksa oluştur
                if (!Directory.Exists(xmalsFolderPath))
                {
                    Directory.CreateDirectory(xmalsFolderPath);
                }

                // Oluşturulacak XML dosyasının adı ve yolu (Xmals klasöründe)
                string xmlFileName = txtCustomerName.Text + " " + "Lisans.xml";
                string xmlFilePath = Path.Combine(xmalsFolderPath, xmlFileName);

                // XML dosyasını oluştur
                XmlTextWriter xmlWriter = new XmlTextWriter(xmlFilePath, null);
                xmlWriter.Formatting = Formatting.Indented;

                // XML dosyasının kök elementini oluştur
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("Lisans");

                string genericKey = GenerateLicenseKey(32);

                // Müşteri bilgileri için alt elementler oluştur
                xmlWriter.WriteStartElement("Musteri");
                xmlWriter.WriteElementString("AdSoyad", txtCustomerName.Text);
                xmlWriter.WriteElementString("UrunKodu", txtProductNum.Text);
                xmlWriter.WriteElementString("TelefonNo", txtCustomerPhone.Text);
                xmlWriter.WriteElementString("LisansAnahtari", genericKey);
                xmlWriter.WriteElementString("LisansBaşlamaTarihi", DateTime.Now.ToString());
                xmlWriter.WriteElementString("PazarYeri", txtPazarYeri.Text);
                xmlWriter.WriteEndElement(); // Musteri elementi

                // Kök elementi kapat
                xmlWriter.WriteEndElement(); // Lisans elementi
                xmlWriter.WriteEndDocument();

                // XML dosyasını kapat
                xmlWriter.Close();

                MessageBox.Show("XML dosyası başarıyla oluşturuldu.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("XML dosyası oluşturulurken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GenerateLicenseKey(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-";

            // Rastgele anahtar oluştur
            Random random = new Random();
            char[] key = new char[length];
            for (int i = 0; i < length; i++)
            {
                key[i] = chars[random.Next(chars.Length)];
            }

            return new string(key);
        }
    }
}
