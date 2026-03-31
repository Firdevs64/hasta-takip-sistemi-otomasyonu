# 🏥 Hasta Takip Sistemi (HTS) – Klinik Karar Destekli

Bu proje, C# WinForms ve DevExpress bileşenleri kullanılarak geliştirilmiş,
veritabanı destekli bir **Hasta Takip Sistemi**dir.

Sistem; hasta kayıt, güncelleme, silme ve listeleme işlemlerinin yanında  
**şikayet ve hayati bulgulara (ateş, nabız)** dayalı **AI destekli klinik karar önerisi**
sunmaktadır.

> ⚠️ Sistem **karar destek amaçlıdır**. Nihai tıbbi karar sağlık personeline aittir.

## 🚀 Özellikler

- Hasta kayıt / güncelleme / silme
- SQL Server + Stored Procedure kullanımı
- DevExpress GridControl ile gelişmiş veri listeleme
- LookupEdit ile dinamik **Durum** ve **Bölüm** seçimi
- İstatistik ekranı (hasta sayıları, yaş ortalaması vb.)
- **AI Destekli Klinik Karar Modülü**
  - Şikayete göre bölüm önerisi
  - Acil / Muayene durumu belirleme
  - Ateş ve nabız gibi hayati bulguları değerlendirme
  - Güven yüzdesi ve açıklama üretme

## 🤖 Klinik Karar Destek (AI) Mantığı

AI modülü şu verileri kullanır:

- **Hasta Şikayeti (metin)**
- **Ateş (°C)**
- **Nabız (bpm)**

### Çalışma Prensibi
- Şikayet anahtar kelimeleri analiz edilir
- Hayati bulgular eşik değerlere göre değerlendirilir
- Risk varsa güven yüzdesi artırılır
- Sonuç olarak:
  - Önerilen bölüm
  - Önerilen durum (Acil / Muayene)
  - Güven oranı
  - Açıklayıcı gerekçe
  kullanıcıya gösterilir

## 🧱 Kullanılan Teknolojiler

- **C# (.NET Framework)**
- **WinForms**
- **DevExpress**
- **SQL Server**
- **Stored Procedures**
- Kural tabanlı AI (ML.NET kullanılmadan)

## 🖥️ Ekranlar

- Giriş / Kayıt ekranı
- Ana hasta yönetim ekranı
- AI Klinik Karar Destek paneli
- İstatistik ekranı

## 📌 Proje Amacı

Bu proje, hastane otomasyonlarında:
- veri yönetimi
- kullanıcı arayüzü tasarımı
- karar destek sistemleri
konularını **tek bir uygulamada** birleştirmeyi amaçlamaktadır.

Ayrıca projede **AI kavramının** yalnızca görüntü işleme veya tahlil verisiyle
sınırlı olmadığı, **metin ve kural tabanlı yaklaşımlarla** da uygulanabileceği
gösterilmiştir.


