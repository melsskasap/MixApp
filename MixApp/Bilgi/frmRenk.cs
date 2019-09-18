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
    public partial class frmRenk : Form
    {
        MixDoContext db = new MixDoContext();
        int secimId = -1;
        bool edit = false;
        public frmRenk()
        {
            InitializeComponent();
        }

        private void frmRenk_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void Listele()
        {
            Liste.Rows.Clear();
            int i = 0;
            var lst = db.Renklers.ToList();
            foreach (var k in lst)
            {
                Liste.Rows.Add();
                Liste.Rows[i].Cells[0].Value = k.Id;
                Liste.Rows[i].Cells[1].Value = k.Renk;
                i++;
            }
            Liste.AllowUserToAddRows = false;
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (edit && secimId > 0 && MessageBox.Show("Seçili olan kayıt güncellenecektir.\nGüncelleme işlemini onaylıyor musunuz?", "Güncelleme İşlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Guncelle();
            else if(edit == false)
                YeniKayit();

            Temizle();
        }

        private void Guncelle()
        {
            Renkler rnk = db.Renklers.First(x => x.Id == secimId);
            rnk.Renk = txtRenk.Text;
            db.SaveChanges();
            MessageBox.Show("Renk başarıyla güncellendi");
            Listele();
            Temizle();
        }

        private void YeniKayit()
        {
            var cont = (from k in db.Renklers
                        where k.Renk.ToLower() == txtRenk.Text.ToLower()
                        select k).ToList();
            if (cont.Count == 0)
            {
                if (txtRenk.Text != "")
                {
                    Renkler rnk = new Renkler();
                    rnk.Renk = txtRenk.Text;
                    db.Renklers.Add(rnk);
                    db.SaveChanges();
                    MessageBox.Show("Renk başarıyla eklenmiştir.");
                    Listele();
                    Temizle();
                }
               else
                    MessageBox.Show("Renk boş geçilemez.");
            }
            else
                MessageBox.Show("Renk daha önce girilmiş.");
        }

        private void Temizle()
        {
            txtRenk.Text = "";
            secimId = -1;
            edit = false;
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
            txtRenk.Text = Liste.CurrentRow.Cells[1].Value.ToString();
            secimId = int.Parse(Liste.CurrentRow.Cells[0].Value.ToString());
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (edit && secimId > 0 && MessageBox.Show("Seçili olan kayıt silinecektir.\nSilme işlemini onaylıyor musunuz?", "Silme İşlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Sil();

            Temizle();
        }

        private void Sil()
        {
            db.Renklers.Remove(db.Renklers.First(x => x.Id == secimId));
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
