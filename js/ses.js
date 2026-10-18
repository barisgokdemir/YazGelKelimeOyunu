// Ses dosyası kullanmadan WebAudio ile kısa efektler.
// Tarayıcılar sesi ilk kullanıcı etkileşiminden sonra açtığı için bağlam ilk çağrıda oluşturulur.

let baglam = null;
let acik = true;

function bagla() {
  if (!baglam) {
    const AudioCtx = window.AudioContext || window.webkitAudioContext;
    if (!AudioCtx) return null;
    baglam = new AudioCtx();
  }
  if (baglam.state === 'suspended') baglam.resume();
  return baglam;
}

function ton(frekans, { sure = 0.12, gecikme = 0, tip = 'sine', ses = 0.15 } = {}) {
  if (!acik) return;
  const ctx = bagla();
  if (!ctx) return;

  const baslangic = ctx.currentTime + gecikme;
  const osc = ctx.createOscillator();
  const kazanc = ctx.createGain();
  osc.type = tip;
  osc.frequency.setValueAtTime(frekans, baslangic);
  kazanc.gain.setValueAtTime(ses, baslangic);
  kazanc.gain.exponentialRampToValueAtTime(0.0001, baslangic + sure);
  osc.connect(kazanc).connect(ctx.destination);
  osc.start(baslangic);
  osc.stop(baslangic + sure + 0.02);
}

export const ses = {
  ayarla(deger) {
    acik = deger;
  },
  get acik() {
    return acik;
  },
  harf: () => ton(660, { sure: 0.08, tip: 'triangle' }),
  tik: () => ton(1200, { sure: 0.04, ses: 0.06, tip: 'square' }),
  dogru: () => [523, 659, 784].forEach((f, i) => ton(f, { gecikme: i * 0.09, sure: 0.18, tip: 'triangle' })),
  yanlis: () => [220, 165].forEach((f, i) => ton(f, { gecikme: i * 0.14, sure: 0.22, tip: 'sawtooth', ses: 0.08 })),
  pas: () => ton(330, { sure: 0.12, tip: 'triangle' }),
  bitis: () => [784, 659, 523, 784].forEach((f, i) => ton(f, { gecikme: i * 0.12, sure: 0.2, tip: 'triangle' })),
};
