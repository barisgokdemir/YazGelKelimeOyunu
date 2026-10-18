import { test } from 'node:test';
import assert from 'node:assert/strict';
import { readFile } from 'node:fs/promises';

import {
  AYARLAR, Oyun, HavuzHatasi, normalize, rakipUret, siraBul, soruSec, tohumluRastgele,
} from '../js/oyun.js';
import { metniCoz, soruSetiniDogrula } from '../js/veri.js';

const veri = JSON.parse(await readFile(new URL('../data/sorular.json', import.meta.url), 'utf8'));

function yeniOyun(tohum = 1) {
  const rastgele = tohumluRastgele(tohum);
  return new Oyun(soruSec(veri.sorular, { rastgele }), { rastgele });
}

test('Türkçe normalizasyon i/İ ve ı/I harflerini doğru eşler', () => {
  assert.equal(normalize('ılgın'), 'ILGIN');
  assert.equal(normalize('işhanı'), 'İŞHANI');
  assert.equal(normalize(' yaşlı başlı '), 'YAŞLIBAŞLI');
  assert.equal(normalize('kâğıt'), 'KAĞIT');
});

test('soru seti geçerli: her uzunlukta 8 soru, atlanan yok', () => {
  const sonuc = soruSetiniDogrula(veri);
  assert.equal(sonuc.atlanan.length, 0, sonuc.atlanan.join('\n'));
  for (const u of AYARLAR.uzunluklar) {
    assert.equal(sonuc.sorular.filter((s) => [...s.cevap].length === u).length, 8, `${u} harfliler`);
  }
});

test('v1 masaüstü biçimi (düz dizi + harfSayisi) kabul edilir', () => {
  const v1 = [{ soru: 'Ait olunan yer', cevap: 'Sıla', harfSayisi: '4' }, { soru: 'kısa', cevap: 'Su', harfSayisi: '2' }];
  const sonuc = soruSetiniDogrula(v1);
  assert.deepEqual(sonuc.sorular, [{ soru: 'Ait olunan yer', cevap: 'SILA' }]);
  assert.equal(sonuc.atlanan.length, 1);
});

test('v1 dosyası ISO-8859-9 kodlamalı olsa da Türkçe harfler bozulmaz', () => {
  // "Sıla" ve "ağaç" ISO-8859-9 baytlarıyla
  const iso = Uint8Array.from([0x53, 0xfd, 0x6c, 0x61, 0x20, 0x61, 0xf0, 0x61, 0xe7]);
  assert.equal(metniCoz(iso), 'Sıla ağaç');
  assert.equal(metniCoz(new TextEncoder().encode('﻿İşhanı')), 'İşhanı');
});

test('14 soru seçilir, her uzunluktan 2 tane, kısadan uzuna', () => {
  const secilen = soruSec(veri.sorular, { rastgele: tohumluRastgele(7) });
  assert.equal(secilen.length, 14);
  assert.deepEqual(secilen.map((s) => s.harfler.length), [4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10]);
  assert.equal(new Set(secilen.map((s) => s.cevap)).size, 14);
});

test('önceki oyunun cevapları mümkünse tekrar seçilmez', () => {
  const ilk = soruSec(veri.sorular, { rastgele: tohumluRastgele(3) });
  const haric = new Set(ilk.map((s) => s.cevap));
  const ikinci = soruSec(veri.sorular, { rastgele: tohumluRastgele(4), haric });
  assert.equal(ikinci.filter((s) => haric.has(s.cevap)).length, 0);
});

test('yetersiz havuz eksik uzunlukları bildirir', () => {
  assert.throws(() => soruSec([{ soru: 'a', cevap: 'Soba' }]), (e) => e instanceof HavuzHatasi && e.eksikUzunluklar.includes(4));
});

test('doğru cevap kalan harf değeri kadar puan kazandırır', () => {
  const oyun = yeniOyun();
  const s = oyun.aktif;
  oyun.harfAl();
  assert.equal(oyun.soruDegeri, (s.harfler.length - 1) * 100);
  oyun.cevaplamayaBasla();
  const olay = oyun.cevapVer(s.cevap.toLocaleLowerCase('tr-TR'));
  assert.equal(olay.sonuc, 'dogru');
  assert.equal(oyun.puan, 300);
  assert.equal(oyun.index, 1);
});

test('yanlış cevap aynı değer kadar puan düşürür', () => {
  const oyun = yeniOyun();
  oyun.cevaplamayaBasla();
  const olay = oyun.cevapVer('yanlış');
  assert.equal(olay.sonuc, 'yanlis');
  assert.equal(oyun.puan, -400);
});

test('cevap süresi dolunca ceza uygulanır ve sonraki soruya geçilir', () => {
  const oyun = yeniOyun();
  oyun.cevaplamayaBasla();
  assert.equal(oyun.tick(19), null);
  const olay = oyun.tick(1);
  assert.equal(olay.sonuc, 'sureDoldu');
  assert.equal(oyun.puan, -400);
  assert.equal(oyun.durum, 'soru');
});

test('cevap modunda ana süre durur', () => {
  const oyun = yeniOyun();
  oyun.tick(10);
  oyun.cevaplamayaBasla();
  oyun.tick(5);
  assert.equal(oyun.kalanSure, 230);
  assert.equal(oyun.cevapKalan, 15);
});

test('tüm harfler alınırsa soru 0 puanla kapanır; harf al döngüye girmez', () => {
  const oyun = yeniOyun();
  const uzunluk = oyun.aktif.harfler.length;
  const acilan = new Set();
  let son;
  for (let i = 0; i < uzunluk; i++) {
    son = oyun.harfAl();
    acilan.add(son.index);
  }
  assert.equal(acilan.size, uzunluk);
  assert.equal(son.kapandi.sonuc, 'acildi');
  assert.equal(oyun.puan, 0);
  assert.equal(oyun.index, 1);
});

test('pas puanı değiştirmez', () => {
  const oyun = yeniOyun();
  assert.equal(oyun.pas().sonuc, 'pas');
  assert.equal(oyun.puan, 0);
});

test('ana süre bitince oyun biter ve işlem kabul edilmez', () => {
  const oyun = yeniOyun();
  const olay = oyun.tick(AYARLAR.toplamSure);
  assert.equal(olay.tip, 'bitti');
  assert.equal(olay.neden, 'sure');
  assert.equal(oyun.harfAl(), null);
  assert.equal(oyun.cevaplamayaBasla(), null);
});

test('14 soruyu harf almadan bilen oyuncu en yüksek puanı (9800) alır', () => {
  const oyun = yeniOyun(42);
  let olay;
  for (let i = 0; i < 14; i++) {
    oyun.cevaplamayaBasla();
    olay = oyun.cevapVer(oyun.aktif.cevap);
  }
  assert.equal(olay.bitis.neden, 'tamam');
  assert.equal(oyun.puan, 9800);
  assert.equal(oyun.durum, 'bitti');
});

test('rakip skorları 100\'ün katı, 0 ile 9800 arasında; isimler farklı', () => {
  for (let tohum = 1; tohum <= 50; tohum++) {
    const rastgele = tohumluRastgele(tohum);
    const rakipler = rakipUret(soruSec(veri.sorular, { rastgele }), { rastgele });
    assert.equal(new Set(rakipler.map((r) => r.ad)).size, 3);
    for (const r of rakipler) {
      assert.ok(r.puan >= 0 && r.puan <= 9800 && r.puan % 100 === 0, `${r.ad}: ${r.puan}`);
    }
  }
});

test('sıralama: eşitlikte oyuncu önde', () => {
  const rakipler = [{ puan: 5000 }, { puan: 3000 }, { puan: 7000 }];
  assert.equal(siraBul(8000, rakipler), 1);
  assert.equal(siraBul(5000, rakipler), 2);
  assert.equal(siraBul(1000, rakipler), 4);
});
