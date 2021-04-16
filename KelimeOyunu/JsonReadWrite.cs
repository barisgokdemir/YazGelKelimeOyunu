using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jsondeneme
{
    class JsonReadWrite
    {
       /* 
        // var olan Json doyasından okuma
       public void JsonOkuma()
        {
            string JsonOkunanData = File.ReadAllText(@"D:\JsonIslemlerim\Kullaniciler.json");
            //List<Kullanici> okunanJson = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Kullanici>>(JsonOkunanData);

            foreach (var item in okunanJson)
            {
                Console.WriteLine("ID : " + item.ID);
                
                Console.WriteLine();
            }


        }
        // json dosyasına veri girişi 
        public static void JsonYazma()
        {
            Console.WriteLine(@"Bilgileriniz Json Formatında D:\JsonIslemlerim\Kullaniciler.json olarak kayıt edilecektir.");

            if (Directory.Exists(@"D:\JsonIslemlerim\"))
            {
                // ilgili klasor var ise herhangi bir işlem yapmıyoruz. 
            }
            else
            {
                Directory.CreateDirectory(@"D:\JsonIslemlerim\");
            }

           

            string JsonKullaniciler = Newtonsoft.Json.JsonConvert.SerializeObject(k1);
            File.WriteAllText(@"D:\JsonIslemlerim\deneme1.json", JsonKullaniciler);



            Console.WriteLine("Json Export işlemi tamamlandı");

        }
        // json dosyasından agac içerisine yazma fonksiyonu
        public void JsonToTree()
        {

            string JsonOkunanData = File.ReadAllText(@"D:\JsonIslemlerim\Kullaniciler.json");
            List<Kullanici> okunanJson = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Kullanici>>(JsonOkunanData);
            Kisi yeniKisi = new Kisi();
            foreach (var item in okunanJson)
            {
                yeniKisi.Ad = item.Ad;
                yeniKisi.Soyad = item.Soyad;
                yeniKisi.Eposta = item.Eposta;
                //Program.ikili.Ekle(yeniKisi);

            }



        }
        // agaç içindeki verileri Json dosyasına yazma metodu
        public static void TreeToJson()
            
            Console.WriteLine(@"Bilgileriniz Json Formatında D:\JsonIslemlerim\Kullaniciler.json olarak kayıt edilecektir.");

            if (Directory.Exists(@"D:\JsonIslemlerim\"))
            {
                // ilgili klasor var ise herhangi bir işlem yapmıyoruz. 
            }
            else
            {
                Directory.CreateDirectory(@"D:\JsonIslemlerim\");
            }

            string JsonKullaniciler = Newtonsoft.Json.JsonConvert.SerializeObject(Kullaniciler);
            File.WriteAllText(@"D:\JsonIslemlerim\Kullaniciler.json", JsonKullaniciler);

            Console.WriteLine("Json Export işlemi tamamlandı");

            //Agac içerisindeki dugum dönür çalıştıgında her dügüm de TreeToJson metodu çalışacak böylece tüm veriler agaçtan json dosyasına aktarılacak 
            /* 
              //dosyaya yazmak için kullanılacak agaç yapısı içinde olacak metot
            public int DugumDondur(İkiliAramaAgacDugumu dugum)
            {
                int count = 0;
                Kisi kisiyaz = new Kisi();
          

                if (dugum != null)
                {  
                    kisiyaz = dugum.veri;
                   Program.TreeToJson(kisiyaz);
                    count = 1;
                    count += DugumDondur(dugum.sol);
                    count += DugumDondur(dugum.sag);
                }
                return count;
            }                       
             
             
        }
       
*/

        }
    }
