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
    public partial class Oyun : Form
    {
        json jsveri = new json();
        static List<kelime> gelenkelime = new List<kelime>();
        static kelime gecerliKelime = new kelime();
        static int level = 0;
        static int aktivSoru = 0;
        public string oyuncuAdi="";
        public bool sonsoru = false;
        public int Puan=0;
        public int gecerliPuan = 400;
        public int basariDurumu;
        //başlangıçta 4 harfli çektigi için +400 puan geliyor 0 lamak için -400 de başlatıyoruz
        public int harfalPuan = 0;
        
        public int[] rakipSkor = new int[3];
        public Oyun()
        {
            InitializeComponent();
        }

        string[] oyuncu1 = { "barış", "orhan", "yigit", "aygün", "ramazan", "betül", "buşra", "ayşe", "Ahu" };
        string[] oyuncu2 = { "ali", "furkan", "hakan", "muhammed", "abdullah", "Aleyna", "Aslı", "Ezgi", "Aybike" };
        string[] oyuncu3 = { "atilla", "göktuğ", "alptuğ", "efe", "berk", "Beyza", "Cemre", "Defne", "Elif" };


        private void Oyun_Load(object sender, EventArgs e)
        { //rast gele rakip isimleri
            lbOyuncu1.Text = oyuncu1[rand.Next(0, 9)].ToUpper();
            lbOyuncu2.Text = oyuncu2[rand.Next(0, 9)].ToUpper();
            lbOyuncu3.Text = oyuncu3[rand.Next(0, 9)].ToUpper();
            gelenkelime = jsveri.JsonOkuma(@"C:\Users\baris\source\repos\KelimeOyunu\sorucevap.json");
            btnNext.PerformClick();
            //ilk soruda 4 harfli ilk geldigi için +400 oluyor onu başlangıçta tekrar sıfırlıyoruz 
            lbOyuncu.Text = oyuncuAdi.ToUpper();
            int tempSkor = 0;
            char temp='-';
            for (int i = 0; i < gelenHarfler.Length; i++)
            {
                gelenHarfler[i] = temp;
            }
            // rakip oyuncu puanları oluşturma rastgele
           for(int i = 0; i < 3; i++)
            {
            tempSkor= rand.Next(5000, 9800);
                if (tempSkor % 100 == 0)
                {
                    rakipSkor[i] = tempSkor;
                }
                else
                {
                    tempSkor = tempSkor - (tempSkor % 100);
                    rakipSkor[i] = tempSkor;
                }

            }
            lbSkor1.Text = rakipSkor[0].ToString();
            lbSkor2.Text = rakipSkor[1].ToString();
            lbSkor3.Text = rakipSkor[2].ToString();
            // rastgele rakipler 


        }
        // geri sayim 
        public int saniye = 1;
        public int dk = 4;
        public int oyunsuresi = 241;
        private void kalanSure_Tick(object sender, EventArgs e)
        {

            if (oyunsuresi % 60 == 0)
            {
                saniye = 60;
                dk--;

            }
            saniye--;
            oyunsuresi--;
            lbSure.Text = dk.ToString() + ": " + saniye.ToString();
            if (oyunsuresi == 0)
            {
                kalanSure.Enabled = false;
                gameOver();

            }

        }
     
        
        Random rand = new Random();
        // gelen kelimelerin harfleri
        char[] gelenHarfler = new char[4];
        private void btnHarf_Click(object sender, EventArgs e) {
            harfalPuan = harfalPuan - 100;
            // kelimenin içinden random harf getir
            int random=rand.Next(0, gecerliKelime.cevap.Length);
            //gelen harf önceden gelmedi ise diziye ata
           
            //gelen harf önceden geldi ise gelmeyene kadar random dene 
            while (gelenHarfler[random] == gecerliKelime.cevap[random])
            {
                int random2 = rand.Next(0, gecerliKelime.cevap.Length);
               if( gelenHarfler[random2] != gecerliKelime.cevap[random2])
               {
                    gelenHarfler[random2] = gecerliKelime.cevap[random2];
                    break;
               }
               //tüm harfler geldi ise harf bitti 
               if (cevapText.Text == gecerliKelime.cevap)
               {
                    MessageBox.Show("bitti");
                    break;
               }                       
                


            } if(gelenHarfler[random] != gecerliKelime.cevap[random])
            {
                gelenHarfler[random] = gecerliKelime.cevap[random];
            }
            string temp = "";
            for(int i = 0; i < gelenHarfler.Length; i++)
            {
                temp = temp + gelenHarfler[i];
            }
            cevapText.Text = temp;
            // while sonu 

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            kelime nextkelime = new kelime();
            List<kelime> nextList = new List<kelime>();
            //onceki gelen 4 harfli dışında kalan 4 harflilerden liste yaipip içinden birini aliyoruz 
            foreach(var item in gelenkelime){
                // levele göre geçerli soru getiriyor 
                if(item.cevap.Length==(4+level) && item.cevap != gecerliKelime.cevap)
                {
                    nextList.Add(item);
                }
               

            }
        
            gecerliKelime = nextList[rand.Next(0, nextList.Count)];

            label7.Text = gecerliKelime.soru;
            
            //sonraki sorunun gelen harfleri sayisi tanimi 
            cevapText.Text = "---";
            // gelen harfleri-- ile degiştiriyoruz
            gelenHarfler = new char[gecerliKelime.cevap.Length];
            char temp = '-';
            for (int i = 0; i < gelenHarfler.Length; i++)
            {
                gelenHarfler[i] = temp;
            }
            //gelen harfler bitimi 

            aktivSoru++;
            if (aktivSoru % 2 == 0)
            {
                level++;
                gecerliPuan =(gecerliKelime.cevap.Length * 100);
            }

          
            // sonraki soruda cevap tahmin et diyene kadar seçime kapalı oluyor
            cevapBox.Enabled = false;
            cevapKontrol.Visible = false;
            //aktiv soruyu artır
            
            label2.Text = aktivSoru.ToString() + ".Soru:";
            //her iki soru da bir level artsın



            //sonraki soru için sıfırlıyoruz 
            harfalPuan = 0;
            //son soruya gelince butonu kapat
          if (aktivSoru == 14)
          {
              
                btnNext.Enabled = false;
          }

        }

        private void cevapKontrol_Click(object sender, EventArgs e)
        {
            if (cevapBox.Text.ToLower() == gecerliKelime.cevap.ToLower())
            {
                cevapText.Text = gecerliKelime.cevap;
                
                tahminSure.Enabled = false;
                kalanSure.Enabled = true;
                MessageBox.Show("Dogru bildiniz");
                // dogru ise sonraki soru için tahmin saniyeyi 0 layıp tekrar baştan başlıaycak
                //her soru için tahmin süresi 20 saniye olacak
                tahminsaniye = 0;
                tahminTime.Text = tahminsaniye.ToString();

               //
                       

                btnNext.PerformClick();  
                Puan = Puan + gecerliPuan + harfalPuan;
                lbPuan.Text = Puan.ToString();  
                if (aktivSoru==15)
                {
                    gameOver();
                    
                }
                if (aktivSoru == 14)
                {
                    aktivSoru++;
                }
                

            }
            cevapBox.Text = "";


        }

        private void btnTahmin_Click(object sender, EventArgs e)
        {
            // tahmin süresini başlat 
            cevapBox.Enabled = true;
            tahminSure.Enabled = true;
            kalanSure.Enabled = false;
            cevapKontrol.Visible = true;


        }
        // tahmin sure timer 
       public int tahminsaniye = 0;
        private void tahminSure_Tick(object sender, EventArgs e)
        {
            tahminTime.Text = tahminsaniye.ToString();
            tahminsaniye++;
            if (tahminsaniye == 20)
            {
                tahminSure.Enabled = false;
                kalanSure.Enabled = true;
                tahminsaniye = 0;

            }
        }

        private void gameOver()
        {
            
            //puan rakiSkor 
            Array.Sort(rakipSkor);
            int temp = -1;
            for (int i=2;i>0; i--)
            {
                if (rakipSkor[i] <= Puan)
                {
                    temp = i;
                    break;
                }
            }
            if (temp == 2)
            {
                MessageBox.Show("tebrikler Oyunu kazandiniz");
                basariDurumu = 1;

            }
            if (temp == 1)
            {
                MessageBox.Show("2. bitirdiniz");
                basariDurumu = 2;

            }
            if (temp == 0)
            {
                MessageBox.Show("3. bitirdiniz");
                basariDurumu = 3;

            }
            if (temp == -1)
            {
                MessageBox.Show("4. bitirdiniz");
                basariDurumu = 4;

            }

            string oyunanmaZamani = DateTime.Now.ToString();

            kalanSure.Enabled = false;
           
            // Skor yazma
            jsveri.JsonYazmaSkor(oyuncuAdi,lbSure.Text,oyunanmaZamani,Puan.ToString(),basariDurumu.ToString(), @"C:\Users\baris\source\repos\KelimeOyunu\Skor.json");




        }

        private void cevapBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cevapKontrol.PerformClick();
            }
        }

        private void mainBtn_Click(object sender, EventArgs e)
        {
            
            Close();
        }

        private void Oyun_FormClosed(object sender, FormClosedEventArgs e)
        {
            level = 0;
            aktivSoru = 0;
        }
    }
}

 

