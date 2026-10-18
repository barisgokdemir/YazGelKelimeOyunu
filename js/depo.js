// localStorage sarmalayıcısı. Gizli sekme veya engellenmiş depolamada sessizce varsayılana döner.

const ONEK = 'yazgel.';
const SKOR_ANAHTARI = 'skorlar.v2';
const SKOR_LIMITI = 10;

function oku(anahtar, varsayilan) {
  try {
    const deger = localStorage.getItem(ONEK + anahtar);
    return deger === null ? varsayilan : JSON.parse(deger);
  } catch {
    return varsayilan;
  }
}

function yaz(anahtar, deger) {
  try {
    localStorage.setItem(ONEK + anahtar, JSON.stringify(deger));
  } catch {
    // depolama kullanılamıyor; oyun etkilenmez
  }
}

export const depo = {
  oyuncuAdi: () => oku('oyuncuAdi', ''),
  oyuncuAdiKaydet: (ad) => yaz('oyuncuAdi', ad),

  sesAcik: () => oku('ses', true),
  sesKaydet: (acik) => yaz('ses', acik),

  sonCevaplar: () => new Set(oku('sonCevaplar', [])),
  sonCevaplarKaydet: (cevaplar) => yaz('sonCevaplar', [...cevaplar]),

  skorlar: () => oku(SKOR_ANAHTARI, []),

  /** Skoru ekler, ilk 10'u saklar ve eklenen skorun listedeki sırasını döner (-1: listeye giremedi). */
  skorEkle(skor) {
    const kayit = { ...skor, id: `${Date.now()}-${Math.random().toString(36).slice(2, 7)}` };
    const liste = [...this.skorlar(), kayit]
      .sort((a, b) => b.puan - a.puan || b.kalanSure - a.kalanSure || a.tarih.localeCompare(b.tarih))
      .slice(0, SKOR_LIMITI);
    yaz(SKOR_ANAHTARI, liste);
    return liste.findIndex((s) => s.id === kayit.id);
  },

  skorlariTemizle: () => yaz(SKOR_ANAHTARI, []),
};
