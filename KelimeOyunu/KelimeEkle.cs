using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KelimeOyunu
{
    public partial class KelimeEkle : Form
    {
        public KelimeEkle()
        {
            InitializeComponent();
        }
        json newJson = new json();
        private void btnEkle_Click(object sender, EventArgs e)
        {
            string soru, cevap,harfsayisi;
            // json olası hataların önune geçmek ve dosya yapımızı korumak için kontroller ek” 
            string v = soruText.Text.Replace('"', ' ').Replace(':', ' ').Replace('”',' ').Replace('“', ' ');
            soru = v;
            cevap = cevapText.Text;
            harfsayisi = cevapText.Text.Length.ToString();
            newJson.JsonYazma(soru,cevap,harfsayisi,@"C:\Users\baris\source\repos\KelimeOyunu\sorucevap.json");
            cevapText.Text = "";
            soruText.Text = "";

        }
    }
}
