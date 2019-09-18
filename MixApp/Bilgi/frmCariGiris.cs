using MixApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MixApp.Bilgi
{
    public partial class frmCariGiris : Form
    {
        MixDoContext db = new MixDoContext(); // db bağlantısı
        int secimId = -1;
        bool edit = false;
        public frmCariGiris()
        {
            InitializeComponent();
        }

        private void frmCariGiris_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void Listele()
        {
            Liste.Rows.Clear();
            int i = 0;
            //var lst = db.Carilers.ToList();
            //var lst = from s in db.Carilers
            //select s; // LINQ sorgusu

            var lst = (from s in db.Carilers
                       where s.CariAdi.Contains(txtBul.Text) || s.Adres.Contains(txtBul.Text)
                       select new
                       {
                           cari = s.CariAdi,
                           tel = s.Tel,
                           id = s.Id,
                           mail = s.Mail,
                           adr = s.Adres
                       }).Distinct().OrderBy(x => x.cari);

            foreach (var k in lst)
            {
                Liste.Rows.Add();
                // sütunlara değerleri atıyoruz.
                Liste.Rows[i].Cells[0].Value = k.id;
                Liste.Rows[i].Cells[1].Value = k.cari;
                Liste.Rows[i].Cells[2].Value = k.tel;
                Liste.Rows[i].Cells[3].Value = k.mail;
                Liste.Rows[i].Cells[4].Value = k.adr;
                i++;
            }
            Liste.AllowUserToAddRows = false;
        }

        void YeniKaydet()
        {
            try
            {
                Cariler cr = new Cariler();
                cr.Adres = txtAdres.Text;
                cr.CariAdi = txtCariAdi.Text;
                cr.Mail = txtMail.Text;
                cr.Tel = txtTel.Text;
                cr.VergiD = txtVD.Text;
                cr.VnoTc = txtVN.Text;
                cr.Web = txtWeb.Text;
                cr.SaveDate = DateTime.Now;
                cr.SaveUser = -1;
                cr.UpdateDate = DateTime.Now;
                cr.UpdateUser = -1;
                db.Carilers.Add(cr);
                db.SaveChanges();
                MessageBox.Show("Kayıt başarıyla oluşturuldu.");
                Listele();
                Temizle();
            }
            catch (Exception e)
            {
                MessageBox.Show("Hata Bilgisi : " + e);
            }
        }

        void Guncelle()
        {
            Cariler cr = db.Carilers.Find(secimId);
            cr.Adres = txtAdres.Text;
            cr.CariAdi = txtCariAdi.Text;
            cr.Mail = txtMail.Text;
            cr.Tel = txtTel.Text;
            cr.VergiD = txtVD.Text;
            cr.VnoTc = txtVN.Text;
            cr.Web = txtWeb.Text;
            cr.UpdateDate = DateTime.Now;
            cr.UpdateUser = -1;
            
            db.SaveChanges();
            MessageBox.Show("Güncelleme başarıyla yapıldı.");
            Listele();
            Temizle();
        }

        private void Temizle()
        {
            foreach (Control ct in splitContainer2.Panel1.Controls)
            {
                if (ct is TextBox || ct is ComboBox)
                {
                    ct.Text = "";
                }
            }
            edit = false;
            secimId = -1;
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (secimId > 0 && edit == true && MessageBox.Show("Seçili olan kayıt güncellenecektir.\nGüncelleme işlemini onaylıyor musunuz?", "Güncelleme İşlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Guncelle();
            }
            else if (edit == false)
            {
                YeniKaydet();
            }
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCollapse_Click(object sender, EventArgs e)
        {
            if (splitContainer1.Panel2Collapsed == true)
            {
                splitContainer1.Panel2Collapsed = false;
                btnCollapse.Text = "GİZLE";
            }
            else if (splitContainer1.Panel2Collapsed == false)
            {
                splitContainer1.Panel2Collapsed = true;
                btnCollapse.Text = "GÖSTER";
            }
        }

        private void btnCollapseH_Click(object sender, EventArgs e)
        {
            if (splitContainer2.Panel2Collapsed == true)
            {
                splitContainer2.Panel2Collapsed = false;
                btnCollapseH.Text = "GİZLE";
            }
            else if (splitContainer2.Panel2Collapsed == false)
            {
                splitContainer2.Panel2Collapsed = true;
                btnCollapseH.Text = "GÖSTER";
            }
        }

        private void Liste_DoubleClick(object sender, EventArgs e)
        {
            Sec();
            if (secimId > 0)
            {
                Ac(secimId);
            }
        }

        private void Ac(int secimId)
        {
            edit = true;
            //Cariler cari = db.Carilers.First(x => x.Id == secimId);
            Cariler cari = db.Carilers.Find(secimId);
            txtAdres.Text = cari.Adres;
            txtCariAdi.Text = cari.CariAdi;
            txtMail.Text = cari.Mail;
            txtTel.Text = cari.Tel;
            txtVD.Text = cari.VergiD;
            txtVN.Text = cari.VnoTc;
            txtWeb.Text = cari.Web;
        }

        private void Sec()
        {
            try
            {
                //secimId = Convert.ToInt32(Liste.CurrentRow.Cells[0].Value); // Cells["Id"] -> index yerine hücre adı da yazılabilir. // Int' çevirmek için (int) şeklinde de yazılabilir. ama ram'i çok zorlar.
                secimId = int.Parse(Liste.CurrentRow.Cells[0].Value.ToString());
            }
            catch (Exception)
            {
                secimId = -1;
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
            Listele();
        }

        private void btnBul_Click(object sender, EventArgs e)
        {
            Listele();
        }
    }
}
