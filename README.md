# YazGel Kelime Oyunu

[![Test ve yayın](https://github.com/barisgokdemir/YazGelKelimeOyunu/actions/workflows/yayin.yml/badge.svg)](https://github.com/barisgokdemir/YazGelKelimeOyunu/actions/workflows/yayin.yml)

Tarayıcıda oynanan Türkçe kelime yarışması. 14 soru, 4 dakika, 3 rakip.

**[Oyna: barisgokdemir.github.io/YazGelKelimeOyunu](https://barisgokdemir.github.io/YazGelKelimeOyunu/)**

<!-- Ekran görüntüsü: docs/ekran.png ekleyip aşağıdaki satırın yorumunu kaldırın -->
<!-- ![Oyun ekranı](docs/ekran.png) -->

## Nasıl oynanır

- 4 harfliden 10 harfliye kadar her uzunluktan 2, toplam 14 soru gelir. Hepsi için 4 dakikan var.
- Her harf 100 puan değerinde. **Harf al** dediğinde rastgele bir harf açılır ve sorunun değeri 100 puan düşer.
- Cevabı bildiğinde **Cevapla**'ya bas. Ana süre durur, cevap için 20 saniyen olur.
- Doğru cevap sorunun kalan değerini kazandırır. Yanlış cevap ya da cevap süresinin dolması aynı değeri puanından düşer.
- Emin değilsen **Pas** geç: puan kazanmaz, kaybetmezsin.
- Oyun sonunda puanın 3 rakibin puanıyla karşılaştırılır. Rakipler aynı soruları farklı beceri düzeylerinde "oynar"; skorları her oyunda o oyunun sorularından hesaplanır.

Klavye kısayolları: `H` harf al, `Enter` cevapla, `P` pas.

## Özellikler

- Saf HTML, CSS ve JavaScript; framework veya derleme adımı yok
- Sorular JSON dosyasından okunur (`data/sorular.json`, her uzunluktan 8 soru)
- Bir önceki oyunda çıkan sorular mümkünse tekrar gelmez
- Türkçe harf karşılaştırması (`i/İ`, `ı/I`) doğru yapılır
- Skor tablosu tarayıcıda saklanır (`localStorage`, ilk 10)
- Kendi soru setini JSON olarak yükleyip oynayabilirsin
- Mobil uyumlu, klavyeyle oynanabilir, ses efektleri WebAudio ile üretilir (ses dosyası yok)

## Kendi soru setin

Giriş ekranındaki **Kendi sorularınla oyna** bölümünden bir JSON dosyası yükle:

```json
{
  "ad": "Benim setim",
  "sorular": [
    { "soru": "Irmaktan küçük akarsu", "cevap": "Dere" },
    { "soru": "Uzun ömrü ve geniş gölgesiyle bilinen anıt ağaç", "cevap": "Çınar" }
  ]
}
```

- Cevaplar 4-10 harf olmalı ve sadece harf içermeli. Birleşik cevaplar boşluksuz yazılır, ör. `Hedefkitle`.
- Her uzunluktan (4, 5, 6, 7, 8, 9, 10) en az 2 soru gerekir.
- İlk sürümün `sorucevap.json` biçimi (düz dizi, `harfSayisi` alanı, ISO-8859-9 kodlama) de olduğu gibi çalışır.

Varsayılan sete soru eklemek istersen `data/sorular.json` dosyasına ekleyip pull request açabilirsin. Testler her uzunlukta 8 soru olduğunu kontrol eder.

## Yerelde çalıştırma

Sorular `fetch` ile okunduğu için `index.html`'e çift tıklamak yerine basit bir web sunucusu gerekir:

```bash
npx serve .            # veya: python -m http.server
```

Ardından terminalde yazan adresi aç (`npx serve` için genelde `http://localhost:3000`, Python için `http://localhost:8000`). VS Code kullanıyorsan Live Server eklentisi de olur.

Testler (Node.js 20+):

```bash
npm test
```

## Proje yapısı

```
├── index.html
├── css/style.css
├── data/sorular.json      # Varsayılan soru seti
├── js/
│   ├── oyun.js            # Oyun motoru: kurallar, puanlama, süre (DOM'dan bağımsız)
│   ├── veri.js            # Soru seti okuma ve doğrulama
│   ├── depo.js            # localStorage (skorlar, ayarlar)
│   ├── ses.js             # WebAudio ses efektleri
│   └── main.js            # Arayüz
└── tests/oyun.test.js     # node:test ile birim testleri
```

## Geçmiş

Bu oyunun ilk sürümü 2021'de **C# WinForms** ile masaüstü uygulaması olarak yazıldı. Kod o günkü haliyle
[`v1.0-masaustu`](https://github.com/barisgokdemir/YazGelKelimeOyunu/releases/tag/v1.0-masaustu) etiketinde duruyor,
derlenmiş `.exe` de aynı sayfada.

Web sürümünde (v2.0) değişenler:

| İlk sürüm (2021) | Web sürümü |
|---|---|
| Soru ve skor dosyaları geliştiricinin bilgisayarındaki sabit yoldan okunuyordu | Dosyalar göreli yoldan okunuyor, her yerde çalışıyor |
| JSON ISO-8859-9 kodlamalıydı, skor tablosunda Türkçe harfler bozuluyordu | UTF-8; eski dosyalar da otomatik tanınıyor |
| 30 soru | 56 soru, her uzunluktan 8 |
| Yanlış cevabın ve cevap süresinin dolmasının cezası yoktu | Yanlış cevap ve süre bitimi sorunun değeri kadar puan götürüyor |
| Harf alma bazı durumlarda döngüye girebiliyordu | Açılmamış harflerden seçiliyor, tüm harfler açılınca soru kapanıyor |
| Rakip skorları sabit aralıktan rastgele geliyordu | Rakipler aynı soruları kendi beceri düzeyleriyle oynuyor |
| Kelime ekleme formu | Kendi soru setini yükleme |

YazGel adı, hem "yazılım geliştirme"nin kısaltması hem de "yaz, gel" anlamında.

## Lisans

[MIT](LICENSE)
