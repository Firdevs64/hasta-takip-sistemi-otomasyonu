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
            txtBolum.Properties.NullText = "Lütfen durum seçiniz...";

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
                txtDurum.EditValue = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "hDurum").ToString();
                txtBolum.EditValue = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "hBolum").ToString();
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
    }
}
