// Soru setlerini okur ve doğrular.
// Hem yeni biçimi ({ ad, sorular: [...] }) hem de v1 masaüstü sürümünün düz dizi biçimini
// ([{ soru, cevap, harfSayisi }]) kabul eder; eski sorucevap.json dosyası doğrudan yüklenebilir.

import { AYARLAR, normalize } from './oyun.js';

const GECERLI_CEVAP = /^[A-ZÇĞİÖŞÜ]+$/u;

export class VeriHatasi extends Error {
  constructor(mesaj) {
    super(mesaj);
    this.name = 'VeriHatasi';
  }
}

/**
 * @returns {{ ad: string, sorular: {soru: string, cevap: string}[], atlanan: string[] }}
 */
export function soruSetiniDogrula(json, varsayilanAd = 'Özel set') {
  const ham = Array.isArray(json) ? json : json?.sorular;
  if (!Array.isArray(ham)) {
    throw new VeriHatasi('Dosyada "sorular" dizisi bulunamadı.');
  }

  const [enKisa, enUzun] = [Math.min(...AYARLAR.uzunluklar), Math.max(...AYARLAR.uzunluklar)];
  const sorular = [];
  const atlanan = [];
  const gorulen = new Set();

  ham.forEach((oge, i) => {
    const soru = String(oge?.soru ?? '').replace(/\s+/g, ' ').trim();
    const cevap = normalize(oge?.cevap);
    const uzunluk = [...cevap].length;

    if (!soru || !cevap) atlanan.push(`${i + 1}. öge: soru veya cevap boş`);
    else if (!GECERLI_CEVAP.test(cevap)) atlanan.push(`"${oge.cevap}": cevap sadece harflerden oluşmalı`);
    else if (uzunluk < enKisa || uzunluk > enUzun) atlanan.push(`"${oge.cevap}": ${enKisa}-${enUzun} harf olmalı`);
    else if (gorulen.has(cevap)) atlanan.push(`"${oge.cevap}": tekrar eden cevap`);
    else {
      gorulen.add(cevap);
      sorular.push({ soru, cevap });
    }
  });

  return {
    ad: String(json?.ad ?? varsayilanAd).trim() || varsayilanAd,
    sorular,
    atlanan,
  };
}

export async function soruSetiniGetir(url) {
  let yanit;
  try {
    yanit = await fetch(url, { cache: 'no-cache' });
  } catch {
    throw new VeriHatasi(
      'Soru dosyası okunamadı. Oyunu dosyaya çift tıklayarak değil, bir web sunucusu üzerinden açın (README\'ye bakın).'
    );
  }
  if (!yanit.ok) throw new VeriHatasi(`Soru dosyası yüklenemedi (HTTP ${yanit.status}).`);
  return soruSetiniDogrula(await yanit.json(), 'Genel Kültür');
}

/**
 * Dosya içeriğini metne çevirir. UTF-8 değilse Türkçe Windows/ISO-8859-9 kodlamasıyla dener;
 * v1 masaüstü sürümü JSON dosyalarını bu kodlamayla kaydediyordu.
 */
export function metniCoz(arabellek) {
  try {
    return new TextDecoder('utf-8', { fatal: true }).decode(arabellek).replace(/^﻿/, '');
  } catch {
    return new TextDecoder('windows-1254').decode(arabellek);
  }
}

export async function dosyadanSoruSeti(dosya) {
  let json;
  try {
    json = JSON.parse(metniCoz(await dosya.arrayBuffer()));
  } catch {
    throw new VeriHatasi('Dosya geçerli bir JSON değil.');
  }
  return soruSetiniDogrula(json, dosya.name.replace(/\.json$/i, ''));
}
