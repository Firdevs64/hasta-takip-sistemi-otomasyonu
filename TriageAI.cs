using System;
using System.Collections.Generic;
using System.Linq;

namespace HastaTakipSistemi
{
    public class TriageResult
    {
        public string Clinic { get; set; }      // Önerilen bölüm adı (bolumAd ile eşleşecek)
        public string Priority { get; set; }    // Düşük/Orta/Yüksek
        public double Confidence { get; set; }  // 0-100
        public string Explanation { get; set; } // Neden
    }

    public static class TriageAI
    {
        public static List<TriageResult> PredictTop3(string complaint)
        {
            string t = (complaint ?? "").ToLowerInvariant();

            var score = new Dictionary<string, int>
            {
                ["KBB"] = 0,
                ["Dahiliye"] = 0,
                ["Kardiyoloji"] = 0,
                ["Nöroloji"] = 0,
                ["Üroloji"] = 0,
                ["Cildiye"] = 0,
                ["Ortopedi"] = 0,
                ["Diş Hekimliği"] = 0,
                ["Genel Cerrahi"] = 0,
                ["Göz Hastalıkları"] = 0,
                ["Acil"] = 0
            };

            var why = score.Keys.ToDictionary(k => k, k => new List<string>());

            // Kırmızı bayraklar -> Acil
            if (ContainsAny(t, "nefes darlığı", "göğüs ağrısı", "bayıl", "felç", "şiddetli", "kanama"))
            {
                score["Acil"] += 90;
                why["Acil"].Add("Kırmızı bayrak belirtiler");
            }

            // KBB
            if (ContainsAny(t, "boğaz", "bademcik", "kulak", "sinüz", "burun", "geniz", "ses kısıklığı"))
            {
                score["KBB"] += 60;
                why["KBB"].Add("KBB anahtar kelimeleri");
            }

            // Dahiliye
            if (ContainsAny(t, "ateş", "halsizlik", "baş dön", "mide", "bulantı", "ishal", "karın ağrısı", "grip", "üşüt"))
            {
                score["Dahiliye"] += 55;
                why["Dahiliye"].Add("Genel belirtiler");
            }

            // Kardiyoloji
            if (ContainsAny(t, "çarpıntı", "kalp", "tansiyon", "ritim", "göğüs"))
            {
                score["Kardiyoloji"] += 60;
                why["Kardiyoloji"].Add("Kalp-damar anahtar kelimeleri");
            }

            // Nöroloji
            if (ContainsAny(t, "baş ağrısı", "migren", "uyuş", "denge", "titreme", "nöbet", "konuşma bozuk"))
            {
                score["Nöroloji"] += 60;
                why["Nöroloji"].Add("Nörolojik anahtar kelimeler");
            }

            // Üroloji
            if (ContainsAny(t, "idrar", "yanma", "sık idrar", "böbrek taşı", "kasık ağrısı"))
            {
                score["Üroloji"] += 60;
                why["Üroloji"].Add("Ürolojik anahtar kelimeler");
            }

            // Cildiye
            if (ContainsAny(t, "kaşıntı", "döküntü", "alerji", "egzama", "sivilce", "deri"))
            {
                score["Cildiye"] += 60;
                why["Cildiye"].Add("Cilt anahtar kelimeleri");
            }

            // Ortopedi
            if (ContainsAny(t, "bilek", "diz", "omuz", "bel", "kırık", "burkul", "kas ağrısı", "eklem"))
            {
                score["Ortopedi"] += 60;
                why["Ortopedi"].Add("Kas-iskelet anahtar kelimeleri");
            }

            // Diş
            if (ContainsAny(t, "diş", "diş ağrısı", "çene", "dolgu"))
            {
                score["Diş Hekimliği"] += 70;
                why["Diş Hekimliği"].Add("Diş ile ilgili anahtar kelimeler");
            }

            // Genel Cerrahi
            if (ContainsAny(t, "apandisit", "ameliyat", "fıtık", "şişlik", "yara"))
            {
                score["Genel Cerrahi"] += 65;
                why["Genel Cerrahi"].Add("Cerrahi anahtar kelimeler");
            }

            // Göz
            if (ContainsAny(t, "göz", "bulanık", "görme", "çapak", "batma"))
            {
                score["Göz Hastalıkları"] += 65;
                why["Göz Hastalıkları"].Add("Göz ile ilgili anahtar kelimeler");
            }

            // normalize + top3
            var raw = score.Select(kv => (clinic: kv.Key, s: Math.Max(0, kv.Value))).ToList();
            int sum = raw.Sum(x => x.s);
            if (sum == 0) sum = 1;

            return raw
                .Select(x => new TriageResult
                {
                    Clinic = x.clinic,
                    Confidence = Math.Round(x.s * 100.0 / sum, 1),
                    Priority = PriorityFromScore(x.s),
                    Explanation = why[x.clinic].Count == 0 ? "Belirgin anahtar kelime bulunamadı." : string.Join(", ", why[x.clinic])
                })
                .OrderByDescending(x => x.Confidence)
                .Take(3)
                .ToList();
        }

        static bool ContainsAny(string text, params string[] keywords)
            => keywords.Any(k => text.Contains(k));

        static string PriorityFromScore(int s)
        {
            if (s >= 70) return "Yüksek";
            if (s >= 40) return "Orta";
            return "Düşük";
        }
    }
}
