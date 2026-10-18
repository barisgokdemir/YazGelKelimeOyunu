// YazGel Kelime Oyunu - oyun motoru
// DOM'a dokunmaz; arayüz bu sınıfın metodlarını çağırır ve dönen olaylara göre ekranı günceller.
// Süre, arayüzün çağırdığı tick(saniye) ile ilerler; böylece motor testlerde zamandan bağımsız çalışır.

export const AYARLAR = Object.freeze({
  toplamSure: 240,        // saniye (4 dakika)
  cevapSuresi: 20,        // "Cevapla"ya basıldıktan sonra tanınan süre
  harfPuani: 100,
  uzunluklar: [4, 5, 6, 7, 8, 9, 10],
  uzunlukBasinaSoru: 2,   // toplam 14 soru
});

export const RAKIP_ISIMLERI = Object.freeze([
  'Deniz', 'Ece', 'Kaan', 'Selin', 'Mert', 'Zeynep', 'Emre', 'İpek',
  'Ozan', 'Derya', 'Can', 'Melis', 'Tuna', 'Elif', 'Arda', 'Nehir',
]);

/** Türkçe kurallarına göre büyük harfe çevirir (i → İ, ı → I). */
export function trBuyuk(metin) {
  return String(metin ?? '').toLocaleUpperCase('tr-TR');
}

/** Cevap karşılaştırması için: büyük harf, boşluksuz, şapkasız. */
export function normalize(metin) {
  return trBuyuk(metin)
    .replace(/\s+/g, '')
    .replace(/Â/g, 'A')
    .replace(/Î/g, 'İ')
    .replace(/Û/g, 'U');
}

/** Tekrarlanabilir rastgelelik için küçük bir PRNG (testlerde kullanılır). */
export function tohumluRastgele(tohum) {
  let a = tohum >>> 0;
  return function mulberry32() {
    a = (a + 0x6d2b79f5) >>> 0;
    let t = a;
    t = Math.imul(t ^ (t >>> 15), t | 1);
    t ^= t + Math.imul(t ^ (t >>> 7), t | 61);
    return ((t ^ (t >>> 14)) >>> 0) / 4294967296;
  };
}

function karistir(dizi, rastgele) {
  const kopya = [...dizi];
  for (let i = kopya.length - 1; i > 0; i--) {
    const j = Math.floor(rastgele() * (i + 1));
    [kopya[i], kopya[j]] = [kopya[j], kopya[i]];
  }
  return kopya;
}

export class HavuzHatasi extends Error {
  constructor(eksikUzunluklar) {
    super(`Soru havuzu yetersiz. Şu uzunluklarda en az ${AYARLAR.uzunlukBasinaSoru} soru gerekli: ${eksikUzunluklar.join(', ')}`);
    this.name = 'HavuzHatasi';
    this.eksikUzunluklar = eksikUzunluklar;
  }
}

/**
 * Havuzdan her uzunluk için soru seçer ve kısadan uzuna sıralar.
 * `haric` içindeki cevaplar (ör. bir önceki oyunda çıkanlar) mümkünse tercih edilmez.
 */
export function soruSec(havuz, { rastgele = Math.random, haric = new Set(), ayarlar = AYARLAR } = {}) {
  const gruplar = new Map(ayarlar.uzunluklar.map((u) => [u, []]));
  for (const s of havuz) {
    const harfler = [...normalize(s.cevap)];
    gruplar.get(harfler.length)?.push({ soru: s.soru, cevap: harfler.join(''), harfler });
  }

  const eksik = [...gruplar].filter(([, liste]) => liste.length < ayarlar.uzunlukBasinaSoru).map(([u]) => u);
  if (eksik.length > 0) throw new HavuzHatasi(eksik);

  const secilen = [];
  for (const uzunluk of ayarlar.uzunluklar) {
    const liste = karistir(gruplar.get(uzunluk), rastgele);
    const yeni = liste.filter((s) => !haric.has(s.cevap));
    const eski = liste.filter((s) => haric.has(s.cevap));
    secilen.push(...[...yeni, ...eski].slice(0, ayarlar.uzunlukBasinaSoru));
  }
  return secilen;
}

/**
 * Rakip skorlarını, rakiplerin aynı soruları kendi becerileriyle oynadığını varsayarak üretir.
 * Böylece skorlar sabit bir aralıktan değil, o oyunun sorularından gelir.
 */
export function rakipUret(sorular, { rastgele = Math.random, beceriler = [0.55, 0.68, 0.8], cezaOlasiligi = 0.2 } = {}) {
  const isimler = karistir(RAKIP_ISIMLERI, rastgele).slice(0, beceriler.length);
  return beceriler.map((beceri, i) => {
    let puan = 0;
    for (const s of sorular) {
      const uzunluk = s.harfler.length;
      const alinanHarf = Math.floor(rastgele() * (uzunluk * (1.1 - beceri)));
      const deger = Math.max(0, uzunluk - alinanHarf) * AYARLAR.harfPuani;
      if (rastgele() < beceri) puan += deger;
      else if (rastgele() < cezaOlasiligi) puan -= deger;
    }
    return { ad: isimler[i], puan: Math.max(0, puan) };
  });
}

/** 1'den başlayan sıra. Eşitlikte oyuncu önde sayılır. */
export function siraBul(oyuncuPuani, rakipler) {
  return 1 + rakipler.filter((r) => r.puan > oyuncuPuani).length;
}

export class Oyun {
  /**
   * @param {{soru: string, cevap: string, harfler: string[]}[]} sorular soruSec() çıktısı
   */
  constructor(sorular, { rastgele = Math.random, ayarlar = AYARLAR } = {}) {
    this.ayarlar = ayarlar;
    this.rastgele = rastgele;
    this.sorular = sorular.map((s) => ({ ...s, acik: new Set(), sonuc: null, puanDegisimi: 0, verilenCevap: '' }));
    this.index = 0;
    this.puan = 0;
    this.kalanSure = ayarlar.toplamSure;
    this.cevapKalan = 0;
    this.durum = 'soru'; // 'soru' | 'cevap' | 'bitti'
    this.bitisNedeni = null; // 'tamam' | 'sure'
  }

  get aktif() {
    return this.sorular[this.index] ?? null;
  }

  get soruDegeri() {
    const s = this.aktif;
    return s ? (s.harfler.length - s.acik.size) * this.ayarlar.harfPuani : 0;
  }

  /** Açılmamış harflerden rastgele birini açar. Tüm harfler açılırsa soru 0 puanla kapanır. */
  harfAl() {
    if (this.durum !== 'soru') return null;
    const s = this.aktif;
    const kapali = s.harfler.map((_, i) => i).filter((i) => !s.acik.has(i));
    const index = kapali[Math.floor(this.rastgele() * kapali.length)];
    s.acik.add(index);

    const olay = { tip: 'harf', index, harf: s.harfler[index], deger: this.soruDegeri };
    if (s.acik.size === s.harfler.length) {
      return { ...olay, kapandi: this._soruyuKapat('acildi', 0) };
    }
    return olay;
  }

  cevaplamayaBasla() {
    if (this.durum !== 'soru') return null;
    this.durum = 'cevap';
    this.cevapKalan = this.ayarlar.cevapSuresi;
    return { tip: 'cevapBasladi', sure: this.cevapKalan };
  }

  /** Doğru cevap soru değeri kadar puan kazandırır, yanlış cevap aynı değer kadar düşürür. */
  cevapVer(metin) {
    if (this.durum !== 'cevap') return null;
    const tahmin = normalize(metin);
    if (!tahmin) return null;

    const s = this.aktif;
    s.verilenCevap = tahmin;
    const deger = this.soruDegeri;
    return tahmin === s.cevap ? this._soruyuKapat('dogru', deger) : this._soruyuKapat('yanlis', -deger);
  }

  /** Soruyu puan almadan ve kaybetmeden geçer. */
  pas() {
    if (this.durum !== 'soru') return null;
    return this._soruyuKapat('pas', 0);
  }

  /** Süreyi ilerletir. Cevap modunda ana süre durur, sadece cevap süresi akar. */
  tick(saniye) {
    if (this.durum === 'soru') {
      this.kalanSure = Math.max(0, this.kalanSure - saniye);
      if (this.kalanSure === 0) {
        return this._bitir('sure');
      }
    } else if (this.durum === 'cevap') {
      this.cevapKalan = Math.max(0, this.cevapKalan - saniye);
      if (this.cevapKalan === 0) {
        return this._soruyuKapat('sureDoldu', -this.soruDegeri);
      }
    }
    return null;
  }

  _soruyuKapat(sonuc, puanDegisimi) {
    const s = this.aktif;
    s.sonuc = sonuc;
    s.puanDegisimi = puanDegisimi;
    this.puan += puanDegisimi;

    const olay = { tip: 'soruKapandi', sonuc, puanDegisimi, cevap: s.cevap, soruIndex: this.index };

    if (this.index >= this.sorular.length - 1) {
      return { ...olay, bitis: this._bitir('tamam') };
    }
    this.index += 1;
    this.durum = 'soru';
    this.cevapKalan = 0;
    return olay;
  }

  _bitir(neden) {
    this.durum = 'bitti';
    this.bitisNedeni = neden;
    this.cevapKalan = 0;
    return { tip: 'bitti', neden, puan: this.puan, kalanSure: this.kalanSure };
  }
}
