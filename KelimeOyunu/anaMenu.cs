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
    public partial class anaMenu : Form
    {
        public anaMenu()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //jsveri.JsonOkuma();
            //jsveri.JsonYazma();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (oyuncuText.Text != "")
            {
                Oyun newgame = new Oyun();
                newgame.oyuncuAdi = oyuncuText.Text;
                newgame.Show();
            }
            else
            {
                MessageBox.Show("Lütfen Oyuncu Adı giriniz");
            }
            
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            KelimeEkle kelimeEkle = new KelimeEkle();
            kelimeEkle.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            skorTablosu newtablo = new skorTablosu();
            newtablo.Show();
        }
    }
}
