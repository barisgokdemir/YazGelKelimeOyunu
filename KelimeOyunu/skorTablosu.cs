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
    public partial class skorTablosu : Form
    {
        public skorTablosu()
        {
            InitializeComponent();
        }

        private void skorTablosu_Load(object sender, EventArgs e)
        {
            json jsonveri = new json();

            List<Skor> skorlist = new List<Skor>();
            skorlist = jsonveri.JsonOkumaSkor(@"C:\Users\baris\source\repos\KelimeOyunu\Skor.json");
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;

            listView1.Columns.Add("ADI", 100);
            listView1.Columns.Add("DURUM", 100);
            listView1.Columns.Add("PUAN", 70);
            listView1.Columns.Add("KALAN SÜRE", 140);
            listView1.Columns.Add("OYNANMA ZAMANI", 160);

            foreach (var item in skorlist)
            {

             string[] row = { item.oyuncuAdi, item.basariDurumu+".bitirdiniz",item.puan,item.kalanSure,item.oyunanmaZamani };
             var satir = new ListViewItem(row);
             listView1.Items.Add(satir);

                
            }          


             

        }
    }
}
