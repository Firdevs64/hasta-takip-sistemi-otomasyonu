using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HastaTakipSistemi
{
    public partial class frmAnaSayfa : Form
    {
        public frmAnaSayfa()
        {
            InitializeComponent();
        }

        frmSqlBaglanti bgl = new frmSqlBaglanti();
        private void frmAnaSayfa_Load(object sender, EventArgs e)
        {
            Listele();
            durumDoldur();
            bolumDoldur();

            gridView1.FocusedRowHandle = DevExpress.XtraGrid.GridControl.InvalidRowHandle; // hiçbir satır seçili olmasın
            AlanlariTemizle(); // kutular boş başlasın
        }
        private void Listele()
        {
            SqlCommand liste = new SqlCommand("listele", bgl.baglan());
            SqlDataAdapter da = new SqlDataAdapter(liste);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void durumDoldur()
        {
            SqlCommand durum = new SqlCommand("durumDoldur", bgl.baglan());
            SqlDataAdapter da = new SqlDataAdapter(durum);
            DataTable dt = new DataTable();
            da.Fill(dt);

            txtDurum.Properties.DataSource = dt;           // Veriyi bağladık
            txtDurum.Properties.DisplayMember = "durumAd"; // Kullanıcıya gözüken değer
            txtDurum.Properties.ValueMember = "durumID";   // Arka planda saklanan ID

            // 1. Başlangıçta "[EditValue is null]" yerine görünecek metni ayarla
            txtDurum.Properties.NullText = "Lütfen durum seçiniz...";

            // 2. Açılır listedeki "durumAd" gibi sütun başlıklarını gizle
            txtDurum.Properties.ShowHeader = false;

            // 3. (İsteğe bağlı) Alttaki arama kutusunu ve boyutlandırma alanını gizler, daha sade durur.
            txtDurum.Properties.ShowFooter = false;

            // Açılır pencerenin yüksekliğini içindeki satır sayısına göre ayarla
            txtDurum.Properties.DropDownRows = dt.Rows.Count;

            txtDurum.Properties.PopulateColumns();         // Kolonları otomatik doldur
            txtDurum.Properties.Columns["durumID"].Visible = false; // ID kolonunu gizle
        }

        private void bolumDoldur()
        {
            SqlCommand bolum = new SqlCommand("bolumDoldur", bgl.baglan());
            SqlDataAdapter da = new SqlDataAdapter(bolum);
            DataTable dt = new DataTable();
            da.Fill(dt);

            txtBolum.Properties.DataSource = dt;           // Veriyi bağladık
            txtBolum.Properties.DisplayMember = "bolumAd"; // Kullanıcıya gözüken değer
            txtBolum.Properties.ValueMember = "bolumID";   // Arka planda saklanan ID

            // 1. Başlangıçta "[EditValue is null]" yerine görünecek metni ayarla
            txtBolum.Properties.NullText = "Lütfen bölüm seçiniz...";

            // 2. Açılır listedeki "durumAd" gibi sütun başlıklarını gizle
            txtBolum.Properties.ShowHeader = false;

            // 3. (İsteğe bağlı) Alttaki arama kutusunu ve boyutlandırma alanını gizler, daha sade durur.
            txtBolum.Properties.ShowFooter = false;

            // Açılır pencerenin yüksekliğini içindeki satır sayısına göre ayarla
            txtBolum.Properties.DropDownRows = dt.Rows.Count;

            txtBolum.Properties.PopulateColumns();         // Kolonları otomatik doldur
            txtBolum.Properties.Columns["bolumID"].Visible = false; // ID kolonunu gizle
        }


        private void btnListele_Click(object sender, EventArgs e)
        {
            Listele();
            int secilenDurumID = Convert.ToInt32(txtDurum.EditValue);
            int secilenBolumID = Convert.ToInt32(txtBolum.EditValue);
        }


        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView1.FocusedRowHandle >= 0)
            {
                txtID.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ID").ToString();
                txtAd.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "hAd").ToString();
                txtSoyad.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "hSoyad").ToString();
                txtTc.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "hTc").ToString();
                txtTelefon.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "hTel").ToString();
                txtYas.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "hYas").ToString();
                txtCinsiyet.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "hCinsiyet").ToString();
                txtSikayet.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "hSikayet").ToString();
                txtTarih.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "kTarih").ToString();
                txtDurum.EditValue = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "hDurum"));
                txtBolum.EditValue = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "hBolum"));
                lblEx.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "hExMi").ToString();
            }
        }

        private void rbEvet_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEvet.Checked == true)
            {
                lblEx.Text = "True";
            }
            else
            {
                lblEx.Text = "False";
            }
        }

        private void lblEx_TextChanged(object sender, EventArgs e)
        {
            if(lblEx.Text == "True")
            {
                rbEvet.Checked = true;
            }
            else
            {
                rbHayir.Checked = true;
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (txtAd.Text != "" && txtBolum.Text != "" && txtCinsiyet.Text != "" && txtDurum.Text != "" && txtSikayet.Text != "" && txtSoyad.Text != "" && txtTc.Text != "" && txtTelefon.Text != "" && txtYas.Text != "")
            {
                kaydet();
            }
            else
            {
                MessageBox.Show("Lütfen İlgili Tüm Alanları Doldurunuz!","Kayıt Başarısız", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void kaydet()
        {
            SqlCommand kaydet = new SqlCommand("kaydet",bgl.baglan());
            kaydet.CommandType = CommandType.StoredProcedure;
            kaydet.Parameters.AddWithValue("ad",txtAd.Text.ToString());
            kaydet.Parameters.AddWithValue("soyad",txtSoyad.Text.ToString());
            kaydet.Parameters.AddWithValue("tc",txtTc.Text.ToString());
            kaydet.Parameters.AddWithValue("tel",txtTelefon.Text.ToString());
            kaydet.Parameters.AddWithValue("yas",int.Parse(txtYas.Text.ToString()));
            kaydet.Parameters.AddWithValue("cins",txtCinsiyet.Text.ToString());
            kaydet.Parameters.AddWithValue("sikayet",txtSikayet.Text.ToString());
            kaydet.Parameters.AddWithValue("tarih",DateTime.Now);
            kaydet.Parameters.AddWithValue("durum",txtDurum.EditValue);
            kaydet.Parameters.AddWithValue("bolum",txtBolum.EditValue);

            if (lblEx.Text == "True")
            {
                kaydet.Parameters.AddWithValue("ex",1);
            }
            else
            {
                kaydet.Parameters.AddWithValue("ex", 0);
            }
            kaydet.ExecuteNonQuery();
            MessageBox.Show("Kayıt Başarıyla Eklendi", "Kayıt Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
        }
        private void AlanlariTemizle()
        {
            txtID.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            txtTc.Text = "";
            txtTelefon.Text = "";
            txtYas.Text = "";
            txtCinsiyet.Text = "";
            txtSikayet.Text = "";
            txtDurum.EditValue = null;
            txtBolum.EditValue = null;
            lblEx.Text = "False";
            rbEvet.Checked = false;
            rbHayir.Checked = true;
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            sil();
        }
        private void sil()
        {
            DialogResult dr = MessageBox.Show($"{txtID.Text} numaralı kayıt silinecek. Onaylıyor musunuz?","Onay",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                SqlCommand sil = new SqlCommand("sil", bgl.baglan());
                sil.CommandType = CommandType.StoredProcedure;
                sil.Parameters.AddWithValue("id", int.Parse(txtID.Text.ToString()));
                sil.ExecuteNonQuery();
                MessageBox.Show("Kayıt Başarıyla Silindi", "Kayıt Silme Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Listele();
            }
            else
            {

            }

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show($"{txtID.Text} numaralı kayıt güncellenecek. Onaylıyor musunuz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                guncelle();
            }
        }
        private void guncelle()
        {
            SqlCommand guncelle = new SqlCommand("guncelle", bgl.baglan());
            guncelle.CommandType = CommandType.StoredProcedure;
            guncelle.Parameters.AddWithValue("id", int.Parse(txtID.Text));
            guncelle.Parameters.AddWithValue("ad", txtAd.Text.ToString());
            guncelle.Parameters.AddWithValue("soyad", txtSoyad.Text.ToString());
            guncelle.Parameters.AddWithValue("tc", txtTc.Text.ToString());
            guncelle.Parameters.AddWithValue("tel", txtTelefon.Text.ToString());
            guncelle.Parameters.AddWithValue("yas", int.Parse(txtYas.Text.ToString()));
            guncelle.Parameters.AddWithValue("cins", txtCinsiyet.Text.ToString());
            guncelle.Parameters.AddWithValue("sikayet", txtSikayet.Text.ToString());
            guncelle.Parameters.AddWithValue("tarih", DateTime.Now);
            guncelle.Parameters.AddWithValue("durum", txtDurum.EditValue);
            guncelle.Parameters.AddWithValue("bolum", txtBolum.EditValue);

            if (lblEx.Text == "True")
            {
                guncelle.Parameters.AddWithValue("ex", 1);
            }
            else
            {
                guncelle.Parameters.AddWithValue("ex", 0);
            }
            guncelle.ExecuteNonQuery();
            MessageBox.Show("Güncelleme İşlemi Başarılı", "Güncelleme Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
        }

        private void btnFormuTemizle_Click(object sender, EventArgs e)
        {
            AlanlariTemizle();
        }

        private void btnİstatistic_Click(object sender, EventArgs e)
        {
            frmIstatistic fr = new frmIstatistic();
            fr.Show();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblEx_Click(object sender, EventArgs e)
        {

        }

        private void btnAIOneri_Click(object sender, EventArgs e)
        {
            string sikayet = txtSikayet.Text?.Trim();
            // Hayati bulguları oku (boş olabilir)
            double? ates = ParseNullableDouble(txtAtes.Text);
            int? nabiz = ParseNullableInt(txtNabiz.Text);

            // Bulgulardan "kırmızı bayrak" çıkar
            var vital = VitalFlags(ates, nabiz);


            if (string.IsNullOrWhiteSpace(sikayet))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "AI önerisi için önce 'Hasta Şikayet' alanını doldur.",
                    "Bilgi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }

            var top3 = TriageAI.PredictTop3(sikayet);
            var best = top3.First();

            // Hayati bulgulara göre güven/öncelik ayarı (basit ama etkili)
            int extraConfidence = 0;
            string priorityOverride = null;

            if (vital.IsEmergency)
            {
                extraConfidence += 20;          // acil bulgu varsa güveni artır
                priorityOverride = "Yüksek";    // önceliği yükselt
            }
            else if (vital.IsWarning)
            {
                extraConfidence += 10;
            }

            // best değerini güncelle (priority override + confidence clamp)
            string finalPriority = priorityOverride ?? best.Priority;
            int baseConfidence = Convert.ToInt32(Math.Round(best.Confidence)); // double gelirse düzgün yuvarlar
            int finalConfidence = Math.Min(95, baseConfidence + extraConfidence);
            string finalExplain =
                best.Explanation +
                VitalExplainText(ates, nabiz, vital);


            // Durum önerisi (Acil/Muayene)
            string önerilenDurum = DurumFromPriority(finalPriority);

            // Bölüm önerisi: AI “Acil” çıkarırsa bölüm yerine Dahiliye öneriyoruz (istersen bunu kaldırabilirsin)
            string önerilenBolum = (best.Clinic == "Acil") ? "Dahiliye" : best.Clinic;

            bool bolumOk = SetLookUpByDisplayText(txtBolum, "bolumAd", "bolumID", önerilenBolum);
            bool durumOk = SetLookUpByDisplayText(txtDurum, "durumAd", "durumID", önerilenDurum);

            // Sonucu göster (hoca burada etkilenir)
            string msg =
                "AI Önerisi (Şikayet + Hayati Bulgular)\n\n" +
                $"- Önerilen Bölüm: {önerilenBolum}\n" +
                $"- Önerilen Durum: {önerilenDurum}\n" +
                $"- Güven: %{finalConfidence}\n" +
                $"- Neden: {finalExplain}\n\n" +
                "Alternatifler:\n" +
                $"{top3[1].Clinic} (%{top3[1].Confidence})\n" +
                $"{top3[2].Clinic} (%{top3[2].Confidence})\n\n" +
                "Not: Bu sonuçlar karar destek amaçlıdır. Nihai karar sağlık personeline aittir.";


            DevExpress.XtraEditors.XtraMessageBox.Show(
                msg,
                "Klinik Karar Destek (AI)",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            if (!bolumOk)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    $"Dikkat: '{önerilenBolum}' bölümü sistemde bulunamadı. Bölüm listesini kontrol et.",
                    "Uyarı",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }

            if (!durumOk)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    $"Dikkat: '{önerilenDurum}' durumu sistemde bulunamadı. Durum listesini kontrol et.",
                    "Uyarı",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }
        private bool SetLookUpByDisplayText(
            DevExpress.XtraEditors.LookUpEdit edit,
            string displayColumn,
            string valueColumn,
            string displayText)
        {
            if (edit.Properties.DataSource is DataTable dt)
            {
                var row = dt.AsEnumerable()
                    .FirstOrDefault(r =>
                        string.Equals(
                            r.Field<string>(displayColumn),
                            displayText,
                            StringComparison.OrdinalIgnoreCase));

                if (row != null)
                {
                    edit.EditValue = row[valueColumn];
                    return true;
                }
            }
            return false;
        }

        private string DurumFromPriority(string priority)
        {
            if (priority == "Yüksek") return "Acil";
            if (priority == "Orta") return "Muayene";
            if (priority == "Düşük") return "Muayene";
            return "Muayene";
        }

        private double? ParseNullableDouble(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return null;

            // TR ondalık için 36,5 yazanlar olur diye:
            s = s.Trim().Replace(',', '.');

            if (double.TryParse(s, System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out double v))
                return v;

            return null;
        }

        private int? ParseNullableInt(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return null;
            if (int.TryParse(s.Trim(), out int v)) return v;
            return null;
        }

        private (bool IsEmergency, bool IsWarning) VitalFlags(double? ates, int? nabiz)
        {
            bool emergency = false;
            bool warning = false;

            // Ateş eşikleri (basit klinik mantık)
            if (ates.HasValue)
            {
                if (ates.Value >= 39.0 || ates.Value <= 35.0) emergency = true;
                else if (ates.Value >= 38.0) warning = true;
            }

            // Nabız eşikleri
            if (nabiz.HasValue)
            {
                if (nabiz.Value >= 130 || nabiz.Value <= 45) emergency = true;
                else if (nabiz.Value >= 110) warning = true;
            }

            return (emergency, warning);
        }

        private string VitalExplainText(double? ates, int? nabiz, (bool IsEmergency, bool IsWarning) vital)
        {
            // Kullanıcı girmediyse sessiz geç
            if (!ates.HasValue && !nabiz.HasValue) return "";

            string a = ates.HasValue ? $"{ates:0.0}°C" : "—";
            string n = nabiz.HasValue ? $"{nabiz} bpm" : "—";

            if (vital.IsEmergency)
                return $" | Hayati bulgular: Ateş={a}, Nabız={n} (kırmızı bayrak)";
            if (vital.IsWarning)
                return $" | Hayati bulgular: Ateş={a}, Nabız={n} (dikkat)";

            return $" | Hayati bulgular: Ateş={a}, Nabız={n} (normal aralık)";
        }
    }
}
