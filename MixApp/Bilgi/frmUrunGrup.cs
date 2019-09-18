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
    public partial class frmUrunGrup : Form
    {
        MixDoContext db = new MixDoContext();
        int secimId = -1;
        bool edit = false;
        public frmUrunGrup()
        {
            InitializeComponent();
        }

        private void frmUrunGrup_Load(object sender, EventArgs e)
        {
            Listele();
        }

        void Temizle()
        {
            txtGrupAdi.Text = "";
            edit = false;
            secimId = -1;
        }

        private void Listele()
        {
            Liste.Rows.Clear();
            int i = 0;
            var lst = db.UrunGruplaris.ToList().OrderBy(x => x.GrupAdi);
            foreach (var k in lst)
            {
                Liste.Rows.Add();
                Liste.Rows[i].Cells[0].Value = k.Id;
                Liste.Rows[i].Cells[1].Value = k.GrupAdi;
                i++;
            }
            Liste.AllowUserToAddRows = false;
        }
        void YeniKayit()
        {
            var cont = (from s in db.UrunGruplaris
                        where s.GrupAdi.ToLower() == txtGrupAdi.Text.ToLower()
                        select s).ToList();

            if (cont.Count == 0)
            {
                if (txtGrupAdi.Text != "")
                {
                    UrunGruplari urn = new UrunGruplari();
                    urn.GrupAdi = txtGrupAdi.Text;
                    db.UrunGruplaris.Add(urn);
                    db.SaveChanges();
                    MessageBox.Show("Kayıt işlemi başarılı.");
                    Listele();
                    Temizle();
                }
                else
                    MessageBox.Show("Grup adı boş olamaz!!!");
            }
            else
                MessageBox.Show("Grup adı daha önce girilmiş.");
        }

        void Guncelle()
        {
            UrunGruplari urn = db.UrunGruplaris.First(x => x.Id == secimId);
            urn.GrupAdi = txtGrupAdi.Text;
            db.SaveChanges();
            MessageBox.Show("Güncelle işlemi başarılı.");
            Listele();
            Temizle();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (edit && secimId > 0 && MessageBox.Show("Seçili olan kayıt güncellenecektir.\nGüncelleme işlemini onaylıyor musunuz?", "Güncelleme İşlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Guncelle();
            else if (edit == false)
                YeniKayit();

            Temizle();
        }

        private void Liste_DoubleClick(object sender, EventArgs e)
        {
            Sec();
            if (secimId > 0)
                edit = true;
            else
                edit = false;
        }

        private void Sec()
        {
            txtGrupAdi.Text = Liste.CurrentRow.Cells[1].Value.ToString();
            secimId = int.Parse(Liste.CurrentRow.Cells[0].Value.ToString());
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (edit && secimId > 0 && MessageBox.Show("Seçili olan kayıt silinecektir.\nSilme işlemini onaylıyor musunuz?", "Silme İşlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Sil();
            else if (edit == false)
                YeniKayit();

            Temizle();
        }

        private void Sil()
        {
            db.UrunGruplaris.Remove(db.UrunGruplaris.First(x => x.Id == secimId));
            db.SaveChanges();
            MessageBox.Show("Silme işlemi başarılı.");
            Listele();
            Temizle();
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
