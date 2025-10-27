using System.Data.SqlClient;
namespace HastaTakipSistemi
{
    internal class frmSqlBaglanti
    {
        string adres = @"Data Source=FIRDEVS;Initial Catalog=db_HastaneYonetim;Integrated Security=True;Encrypt=False;";

        public SqlConnection baglan()
        {
            SqlConnection baglanti = new SqlConnection(adres);
            baglanti.Open();
            return baglanti;
        }
    }
}
