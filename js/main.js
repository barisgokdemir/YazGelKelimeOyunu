// YazGel Kelime Oyunu - arayüz
import { AYARLAR, HavuzHatasi, Oyun, rakipUret, siraBul, soruSec, trBuyuk } from './oyun.js';
import { soruSetiniGetir, dosyadanSoruSeti, VeriHatasi } from './veri.js';
import { depo } from './depo.js';
import { ses } from './ses.js';

const $ = (secici) => document.querySelector(secici);
const $$ = (secici) => [...document.querySelectorAll(secici)];

const el = {
  ekranlar: {
    giris: $('#ekran-giris'),
    oyun: $('#ekran-oyun'),
    sonuc: $('#ekran-sonuc'),
    skorlar: $('#ekran-skorlar'),
  },
  girisFormu: $('#giris-formu'),
  oyuncuAdi: $('#oyuncu-adi'),
  oynaDugmesi: $('#oyna-dugmesi'),
  girisMesaji: $('#giris-mesaji'),
  setDosyasi: $('#set-dosyasi'),
  setDurumu: $('#set-durumu'),
  varsayilanSet: $('#varsayilan-set'),

  soruNo: $('#soru-no'),
  sure: $('#sure'),
  sureKutusu: $('#sure-kutusu'),
  puan: $('#puan'),
  sesDugmesi: $('#ses-dugmesi'),
  rakipler: $('#rakipler'),
  harfSayisi: $('#harf-sayisi'),
  soruDegeri: $('#soru-degeri'),
  soruMetni: $('#soru-metni'),
  kutular: $('#kutular'),
  geriBildirim: $('#geri-bildirim'),
  kontroller: $('#kontroller'),
  harfAl: $('#harf-al'),
  cevapla: $('#cevapla'),
  pas: $('#pas'),
  cevapFormu: $('#cevap-formu'),
  cevap: $('#cevap'),
  cevapCubugu: $('#cevap-cubugu'),
  cevapKalan: $('#cevap-kalan'),
  ceza: $('#ceza'),

  sonucBaslik: $('#sonuc-baslik'),
  sonucOzet: $('#sonuc-ozet'),
  siralama: $('#siralama'),
  sonucNot: $('#sonuc-not'),
  soruListesi: $('#soru-listesi'),

  skorSatirlari: $('#skor-satirlari'),
  skorBos: $('#skor-bos'),
  kurallar: $('#kurallar'),
};

const SONUC_BEKLEME_MS = 1800;
const SIRA_METNI = ['', 'Birinci oldun!', 'İkinci oldun', 'Üçüncü oldun', 'Dördüncü oldun'];

const durum = {
  varsayilanSet: null,
  aktifSet: null,
  oyun: null,
  rakipler: [],
  oyuncuAdi: '',
  zamanlayici: null,
  sonZaman: 0,
  beklemede: false,
  sonUyariSaniyesi: null,
  yeniSkorId: null,
};

// ---------- Yardımcılar ----------

function ekranGoster(ad) {
  for (const [isim, ekran] of Object.entries(el.ekranlar)) ekran.hidden = isim !== ad;
  if (ad !== 'oyun') zamanlayiciDurdur();
  if (ad === 'skorlar') skorlariGoster();
  window.scrollTo({ top: 0 });
  // Oyun ekranında odak bir düğmede kalırsa Enter o düğmeye basar; bu yüzden soru metnine odaklanılır
  const odaklar = { giris: '#oyuncu-adi', oyun: '#soru-metni', sonuc: '#sonuc-baslik', skorlar: '#skor-baslik' };
  $(odaklar[ad])?.focus({ preventScroll: true });
}

function sureMetni(saniye) {
  const s = Math.ceil(saniye);
  return `${Math.floor(s / 60)}:${String(s % 60).padStart(2, '0')}`;
}

function puanMetni(puan) {
  return puan.toLocaleString('tr-TR');
}

function isaretliPuan(puan) {
  if (puan > 0) return `+${puanMetni(puan)}`;
  if (puan < 0) return `−${puanMetni(-puan)}`;
  return '0';
}

function geriBildirim(metin, tur = '') {
  el.geriBildirim.textContent = metin;
  el.geriBildirim.className = `geri-bildirim ${tur}`;
}

// ---------- Soru seti ----------

function setKullanilabilirMi(set) {
  soruSec(set.sorular); // yetersizse HavuzHatasi fırlatır
}

async function varsayilanSetiYukle() {
  try {
    durum.varsayilanSet = await soruSetiniGetir('data/sorular.json');
    setKullanilabilirMi(durum.varsayilanSet);
    durum.aktifSet = durum.varsayilanSet;
    el.girisMesaji.textContent = '';
  } catch (hata) {
    el.girisMesaji.textContent = hata.message;
  }
  oynaDugmesiniGuncelle();
}

function oynaDugmesiniGuncelle() {
  el.oynaDugmesi.disabled = !durum.aktifSet;
}

async function ozelSetYukle(dosya) {
  el.setDurumu.classList.remove('hata');
  try {
    const set = await dosyadanSoruSeti(dosya);
    setKullanilabilirMi(set);
    durum.aktifSet = set;
    const atlanan = set.atlanan.length ? ` ${set.atlanan.length} soru biçim hatası nedeniyle atlandı.` : '';
    el.setDurumu.textContent = `"${set.ad}" yüklendi: ${set.sorular.length} soru.${atlanan}`;
    el.varsayilanSet.hidden = false;
  } catch (hata) {
    if (!(hata instanceof VeriHatasi || hata instanceof HavuzHatasi)) throw hata;
    el.setDurumu.textContent = hata.message;
    el.setDurumu.classList.add('hata');
    el.setDosyasi.value = '';
  }
  oynaDugmesiniGuncelle();
}

function varsayilanaDon() {
  durum.aktifSet = durum.varsayilanSet;
  el.setDosyasi.value = '';
  el.setDurumu.textContent = '';
  el.setDurumu.classList.remove('hata');
  el.varsayilanSet.hidden = true;
  oynaDugmesiniGuncelle();
}

// ---------- Oyun akışı ----------

function oyunuBaslat() {
  const sorular = soruSec(durum.aktifSet.sorular, { haric: depo.sonCevaplar() });
  durum.oyun = new Oyun(sorular);
  durum.rakipler = rakipUret(sorular);
  durum.beklemede = false;
  durum.sonUyariSaniyesi = null;

  ekranGoster('oyun');
  soruyuGoster();
  zamanlayiciBaslat();
}

function zamanlayiciBaslat() {
  zamanlayiciDurdur();
  durum.sonZaman = performance.now();
  durum.zamanlayici = setInterval(tick, 200);
}

function zamanlayiciDurdur() {
  clearInterval(durum.zamanlayici);
  durum.zamanlayici = null;
}

function tick() {
  const simdi = performance.now();
  const gecen = (simdi - durum.sonZaman) / 1000;
  durum.sonZaman = simdi;
  if (durum.beklemede || !durum.oyun) return;

  const olay = durum.oyun.tick(gecen);
  sureyiGuncelle();
  if (olay) olayIsle(olay);
}

function sureyiGuncelle() {
  const oyun = durum.oyun;
  el.sure.textContent = sureMetni(oyun.kalanSure);

  const azaldi = oyun.kalanSure <= 30 && oyun.durum === 'soru';
  el.sureKutusu.classList.toggle('azaldi', azaldi);
  el.sureKutusu.classList.toggle('durdu', oyun.durum === 'cevap');

  if (oyun.durum === 'cevap') {
    const oran = oyun.cevapKalan / AYARLAR.cevapSuresi;
    el.cevapCubugu.style.transform = `scaleX(${oran})`;
    el.cevapCubugu.classList.toggle('azaldi', oyun.cevapKalan <= 5);
    el.cevapKalan.textContent = Math.ceil(oyun.cevapKalan);
  }

  // Son 10 saniyede (ana süre ya da cevap süresi) saniyede bir tık sesi
  const kalan = oyun.durum === 'cevap' ? oyun.cevapKalan : oyun.kalanSure;
  const tamSaniye = Math.ceil(kalan);
  if (kalan <= 10 && tamSaniye !== durum.sonUyariSaniyesi) {
    durum.sonUyariSaniyesi = tamSaniye;
    ses.tik();
  }
}

function soruyuGoster() {
  const oyun = durum.oyun;
  const s = oyun.aktif;
  el.soruNo.textContent = `${oyun.index + 1}/${oyun.sorular.length}`;
  el.harfSayisi.textContent = `${s.harfler.length} harf`;
  el.soruMetni.textContent = s.soru;
  el.puan.textContent = puanMetni(oyun.puan);
  el.kutular.replaceChildren(
    ...s.harfler.map((_, i) => {
      const kutu = document.createElement('span');
      kutu.className = 'petek';
      kutu.dataset.index = i;
      kutu.setAttribute('aria-label', 'kapalı harf');
      return kutu;
    })
  );
  el.kutular.setAttribute('aria-label', `${s.harfler.length} harfli cevap`);
  degeriGuncelle();
  rakipleriGoster();
  sureyiGuncelle();
  kontrolModu('soru');
  geriBildirim('');
  if (el.ekranlar.oyun.contains(document.activeElement) || document.activeElement === document.body) {
    el.soruMetni.focus({ preventScroll: true });
  }
}

function degeriGuncelle() {
  const deger = durum.oyun.soruDegeri;
  el.soruDegeri.textContent = `${puanMetni(deger)} puan`;
  el.ceza.textContent = puanMetni(deger);
}

function rakipleriGoster() {
  const satirlar = durum.rakipler.map((r) => {
    const li = document.createElement('li');
    li.innerHTML = '<span></span><strong></strong>';
    li.firstChild.textContent = r.ad;
    li.lastChild.textContent = puanMetni(r.puan);
    return li;
  });
  const sen = document.createElement('li');
  sen.className = 'oyuncu';
  sen.innerHTML = '<span></span><strong></strong>';
  sen.firstChild.textContent = durum.oyuncuAdi;
  sen.lastChild.textContent = puanMetni(durum.oyun.puan);
  el.rakipler.replaceChildren(...satirlar, sen);
}

function kontrolModu(mod) {
  const cevapModu = mod === 'cevap';
  el.kontroller.hidden = cevapModu;
  el.cevapFormu.hidden = !cevapModu;
  for (const d of [el.harfAl, el.cevapla, el.pas]) d.disabled = mod !== 'soru';

  if (cevapModu) {
    el.cevap.value = '';
    el.cevap.maxLength = durum.oyun.aktif.harfler.length + 4;
    el.cevapCubugu.style.transform = 'scaleX(1)';
    el.cevapCubugu.classList.remove('azaldi');
    el.cevapKalan.textContent = AYARLAR.cevapSuresi;
    el.cevap.focus();
  }
}

function harfAl() {
  const olay = durum.oyun.harfAl();
  if (!olay) return;
  ses.harf();
  const kutu = el.kutular.children[olay.index];
  kutu.textContent = olay.harf;
  kutu.classList.add('acik');
  kutu.setAttribute('aria-label', olay.harf);
  degeriGuncelle();
  if (olay.kapandi) olayIsle(olay.kapandi);
}

function cevaplamayaBasla() {
  if (!durum.oyun.cevaplamayaBasla()) return;
  durum.sonUyariSaniyesi = null;
  kontrolModu('cevap');
  sureyiGuncelle();
}

function cevapGonder(e) {
  e.preventDefault();
  const olay = durum.oyun.cevapVer(el.cevap.value);
  if (olay) olayIsle(olay);
  else el.cevap.focus();
}

function pasGec() {
  const olay = durum.oyun.pas();
  if (olay) olayIsle(olay);
}

function olayIsle(olay) {
  if (olay.tip === 'soruKapandi') {
    soruSonucunuGoster(olay);
  } else if (olay.tip === 'bitti') {
    oyunuBitir(olay);
  }
}

function soruSonucunuGoster(olay) {
  durum.beklemede = true;
  kontrolModu('bekle');
  el.cevapFormu.hidden = true;
  el.kontroller.hidden = false;

  const tur = { dogru: 'dogru', yanlis: 'yanlis', sureDoldu: 'yanlis' }[olay.sonuc] ?? 'notr';
  [...olay.cevap].forEach((harf, i) => {
    const kutu = el.kutular.children[i];
    const zatenAcik = kutu.classList.contains('acik');
    kutu.textContent = harf;
    kutu.setAttribute('aria-label', harf);
    kutu.className = `petek ${tur}`;
    if (!zatenAcik) {
      kutu.classList.add('sirayla');
      kutu.style.animationDelay = `${i * 60}ms`;
    }
  });

  const mesajlar = {
    dogru: [`Doğru! ${isaretliPuan(olay.puanDegisimi)}`, 'dogru'],
    yanlis: [`Yanlış. ${isaretliPuan(olay.puanDegisimi)}`, 'yanlis'],
    sureDoldu: [`Süre doldu. ${isaretliPuan(olay.puanDegisimi)}`, 'yanlis'],
    pas: ['Pas geçtin.', ''],
    acildi: ['Tüm harfler açıldı, puan yok.', ''],
  };
  const [metin, sinif] = mesajlar[olay.sonuc];
  geriBildirim(metin, sinif);
  ({ dogru: ses.dogru, yanlis: ses.yanlis, sureDoldu: ses.yanlis }[olay.sonuc] ?? ses.pas)();

  el.puan.textContent = puanMetni(durum.oyun.puan);
  rakipleriGoster();

  setTimeout(() => {
    if (!durum.oyun) return;
    if (olay.bitis) {
      oyunuBitir(olay.bitis);
      return;
    }
    durum.beklemede = false;
    durum.sonZaman = performance.now();
    durum.sonUyariSaniyesi = null;
    soruyuGoster();
  }, SONUC_BEKLEME_MS);
}

function oyunuBitir(olay) {
  zamanlayiciDurdur();
  ses.bitis();
  const oyun = durum.oyun;
  const sira = siraBul(oyun.puan, durum.rakipler);

  el.sonucBaslik.textContent = olay.neden === 'sure' && sira !== 1 ? `Süre bitti. ${SIRA_METNI[sira]}` : SIRA_METNI[sira];
  el.sonucBaslik.classList.toggle('birinci', sira === 1);
  const cevaplanan = oyun.sorular.filter((s) => s.sonuc).length;
  const dogru = oyun.sorular.filter((s) => s.sonuc === 'dogru').length;
  el.sonucOzet.textContent = `${puanMetni(oyun.puan)} puan. ${oyun.sorular.length} sorudan ${dogru} doğru${cevaplanan < oyun.sorular.length ? `, ${oyun.sorular.length - cevaplanan} soruya sıra gelmedi` : ''}.`;

  const tablo = [...durum.rakipler.map((r) => ({ ...r, oyuncu: false })), { ad: durum.oyuncuAdi, puan: oyun.puan, oyuncu: true }]
    .sort((a, b) => b.puan - a.puan || Number(b.oyuncu) - Number(a.oyuncu));
  el.siralama.replaceChildren(
    ...tablo.map((r) => {
      const li = document.createElement('li');
      if (r.oyuncu) li.className = 'oyuncu';
      li.innerHTML = '<span></span><span></span>';
      li.firstChild.textContent = r.ad;
      li.lastChild.textContent = puanMetni(r.puan);
      return li;
    })
  );

  const tabloSirasi = depo.skorEkle({
    ad: durum.oyuncuAdi,
    puan: oyun.puan,
    sira,
    kalanSure: Math.ceil(oyun.kalanSure),
    tarih: new Date().toISOString(),
    set: durum.aktifSet.ad,
  });
  durum.yeniSkorId = tabloSirasi >= 0 ? depo.skorlar()[tabloSirasi].id : null;
  el.sonucNot.textContent = tabloSirasi >= 0 ? `Skor tablosunda ${tabloSirasi + 1}. sıradasın.` : '';

  el.soruListesi.replaceChildren(
    ...oyun.sorular.map((s) => {
      const li = document.createElement('li');
      const puan = document.createElement('span');
      puan.className = s.puanDegisimi > 0 ? 'arti' : s.puanDegisimi < 0 ? 'eksi' : '';
      puan.textContent = s.sonuc ? ` (${isaretliPuan(s.puanDegisimi)})` : ' (sıra gelmedi)';
      const cevap = document.createElement('b');
      cevap.textContent = s.cevap;
      li.append(`${s.soru}: `, cevap, puan);
      return li;
    })
  );

  depo.sonCevaplarKaydet(oyun.sorular.map((s) => s.cevap));
  durum.oyun = null;
  ekranGoster('sonuc');
}

function oyundanCik() {
  durum.oyun = null;
  zamanlayiciDurdur();
}

// ---------- Skor tablosu ----------

function skorlariGoster() {
  const skorlar = depo.skorlar();
  el.skorBos.hidden = skorlar.length > 0;
  el.skorSatirlari.closest('table').hidden = skorlar.length === 0;
  const tarihBicimi = new Intl.DateTimeFormat('tr-TR', { dateStyle: 'short', timeStyle: 'short' });
  el.skorSatirlari.replaceChildren(
    ...skorlar.map((s, i) => {
      const tr = document.createElement('tr');
      if (s.id === durum.yeniSkorId) tr.className = 'yeni';
      for (const deger of [i + 1, s.ad, puanMetni(s.puan), `${s.sira}.`, sureMetni(s.kalanSure), tarihBicimi.format(new Date(s.tarih))]) {
        const td = document.createElement('td');
        td.textContent = deger;
        tr.append(td);
      }
      return tr;
    })
  );
}

// ---------- Olay bağlantıları ----------

el.oyuncuAdi.value = depo.oyuncuAdi();

el.girisFormu.addEventListener('submit', (e) => {
  e.preventDefault();
  const ad = el.oyuncuAdi.value.replace(/\s+/g, ' ').trim();
  if (!ad) {
    el.girisMesaji.textContent = 'Oynamak için adını yaz.';
    el.oyuncuAdi.focus();
    return;
  }
  if (!durum.aktifSet) return;
  el.girisMesaji.textContent = '';
  durum.oyuncuAdi = trBuyuk(ad);
  depo.oyuncuAdiKaydet(ad);
  oyunuBaslat();
});

el.setDosyasi.addEventListener('change', () => {
  const dosya = el.setDosyasi.files[0];
  if (dosya) ozelSetYukle(dosya);
});
el.varsayilanSet.addEventListener('click', varsayilanaDon);

el.harfAl.addEventListener('click', harfAl);
el.cevapla.addEventListener('click', cevaplamayaBasla);
el.pas.addEventListener('click', pasGec);
el.cevapFormu.addEventListener('submit', cevapGonder);

$('#tekrar-oyna').addEventListener('click', oyunuBaslat);
$('#skorlari-temizle').addEventListener('click', () => {
  if (confirm('Bu tarayıcıdaki tüm skorlar silinsin mi?')) {
    depo.skorlariTemizle();
    skorlariGoster();
  }
});

$$('[data-ekran-git]').forEach((d) =>
  d.addEventListener('click', () => {
    oyundanCik();
    ekranGoster(d.dataset.ekranGit);
  })
);
$$('[data-ac="kurallar"]').forEach((d) => d.addEventListener('click', () => el.kurallar.showModal()));

ses.ayarla(depo.sesAcik());
el.sesDugmesi.setAttribute('aria-pressed', String(ses.acik));
el.sesDugmesi.addEventListener('click', () => {
  ses.ayarla(!ses.acik);
  depo.sesKaydet(ses.acik);
  el.sesDugmesi.setAttribute('aria-pressed', String(ses.acik));
  el.sesDugmesi.title = ses.acik ? 'Sesi kapat' : 'Sesi aç';
});

document.addEventListener('keydown', (e) => {
  const oyun = durum.oyun;
  if (!oyun || el.ekranlar.oyun.hidden || durum.beklemede || el.kurallar.open) return;
  if (e.ctrlKey || e.metaKey || e.altKey) return;
  if (oyun.durum !== 'soru' || e.target.matches('input')) return;

  const tus = e.key.toLocaleLowerCase('tr-TR');
  if (tus === 'h') {
    e.preventDefault();
    harfAl();
  } else if (tus === 'p') {
    e.preventDefault();
    pasGec();
  } else if (e.key === 'Enter' || e.key === ' ') {
    if (e.target.matches('button')) return; // odaktaki düğme kendi işini yapsın
    e.preventDefault();
    cevaplamayaBasla();
  }
});

// Not: sekme arka plandayken süre bilerek durdurulmaz; aksi halde cevabı başka sekmede aramak serbest olurdu.
// tick() gerçek geçen süreyi ölçtüğü için tarayıcının arka plan kısıtlaması süreyi bozmaz.

varsayilanSetiYukle();
