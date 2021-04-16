using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace KelimeOyunu
{
    class json
    {
        public List<kelime> JsonOkuma(string dosyayolu)
        {
            string JsonOkunanData = File.ReadAllText(dosyayolu, Encoding.GetEncoding("iso-8859-9"));
            List<kelime> okunanJson = JsonConvert.DeserializeObject<List<kelime>>(JsonOkunanData);
            List<kelime> istenenKelime = new List<kelime>();

            //MessageBox.Show(okunanJson[1].soru);
        
            foreach (var item in okunanJson)
            {
                
                    istenenKelime.Add(item);
                

                //MessageBox.Show(item.id);
            }
            return istenenKelime;
            
        }
        public void JsonYazma(string ekleSoru, string ekleCevap, string ekleHSayi,string dosyayolu)
        {
            kelime yenikelime = new kelime();
            yenikelime.soru = ekleSoru;
            yenikelime.cevap = ekleCevap;
            yenikelime.harfSayisi = ekleHSayi;
            
            //json dosyasından tüm veriyi çek
            string JsonOkunanData = File.ReadAllText(dosyayolu, Encoding.GetEncoding("iso-8859-9"));
            //veriyi listeye döüştür
            List<kelime> okunanJson = Newtonsoft.Json.JsonConvert.DeserializeObject<List<kelime>>(JsonOkunanData);
            //listeye yeni veriyi ekle 
            okunanJson.Add(yenikelime);
            //listeyi jsona dönüştür
            string jsonkelime = Newtonsoft.Json.JsonConvert.SerializeObject(okunanJson,Formatting.Indented);
            //jsona yaz 
           File.WriteAllText(@"C:\Users\baris\source\repos\KelimeOyunu\sorucevap.json", jsonkelime, Encoding.GetEncoding("iso-8859-9"));
            MessageBox.Show("yazdi");
        }
        public void JsonYazmaSkor(string oyuncuAdi , string kalanSure, string oyunanmaZamani, string puan, string basariDurumu, string dosyayolu)
        {
            Skor newSkor = new Skor();
            newSkor.oyuncuAdi = oyuncuAdi;
            newSkor.kalanSure = kalanSure;
            newSkor.oyunanmaZamani = oyunanmaZamani;
            newSkor.puan = puan;
            newSkor.basariDurumu = basariDurumu;
            //json dosyasından tüm veriyi çek
            string JsonOkunanData = File.ReadAllText(dosyayolu, Encoding.GetEncoding("iso-8859-9"));
            //veriyi listeye döüştür
            List<Skor> okunanJson = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Skor>>(JsonOkunanData);
            //listeye yeni veriyi ekle 
            okunanJson.Add(newSkor);
            //listeyi jsona dönüştür
            string jsonkelime = Newtonsoft.Json.JsonConvert.SerializeObject(okunanJson, Formatting.Indented);
            //jsona yaz 
            File.WriteAllText(@"C:\Users\baris\source\repos\KelimeOyunu\Skor.json", jsonkelime, Encoding.GetEncoding("iso-8859-9"));
            MessageBox.Show("yazdi");
        }
        public List<Skor> JsonOkumaSkor(string dosyayolu)
        {
            string JsonOkunanData = File.ReadAllText(dosyayolu, Encoding.GetEncoding("iso-8859-9"));
            List<Skor> okunanJson = JsonConvert.DeserializeObject<List<Skor>>(JsonOkunanData);
            List<Skor> istenenKelime = new List<Skor>();

            //MessageBox.Show(okunanJson[1].soru);

            foreach (var item in okunanJson)
            {

                istenenKelime.Add(item);


                //MessageBox.Show(item.id);
            }
            return istenenKelime;

        }

    }
}
