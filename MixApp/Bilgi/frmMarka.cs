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
    public partial class frmMarka : Form
    {
        MixDoContext db = new MixDoContext();
        int secimId = -1;
        bool edit = false;

        public frmMarka()
        {
            InitializeComponent();
        }


        private void Listele()
        {
            Liste.Rows.Clear();
            int i = 0;
            var lst = db.Markalars.ToList();
            foreach (var k in lst)
            {
                Liste.Rows.Add();
                Liste.Rows[i].Cells[0].Value = k.Id;
                Liste.Rows[i].Cells[1].Value = k.Marka;
                i++;
            }
            Liste.AllowUserToAddRows = false;
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (edit && secimId > 0 && MessageBox.Show("Seçili olan kayıt güncellenecektir.\nGüncelleme işlemini onaylıyor musunuz?", "Güncelleme İşlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Guncelle();
            else if (edit == false)
                Ekle();
        }

        private void Guncelle()
        {
            Markalar mrk = db.Markalars.First(x => x.Id == secimId);
            mrk.Marka = txtMarka.Text;
            db.SaveChanges();
            MessageBox.Show("Marka başarıyla güncellenmiştir");
            Listele();
            Temizle();
        }

        private void Ekle()
        {
            var cont = (from k in db.Markalars
                        where (k.Marka.ToLower() == txtMarka.Text.ToLower())
                        select k).ToList();

            if (cont.Count == 0)
            {
                if (txtMarka.Text != "")
                {
                    Markalar mrk = new Markalar();
                    mrk.Marka = txtMarka.Text;
                    db.Markalars.Add(mrk);
                    db.SaveChanges();
                    MessageBox.Show("Marka başarıyla eklenmiştir.");
                    Listele();
                }
                else
                    MessageBox.Show("Marka boş geçilemez.");
            }
            else
                MessageBox.Show("Marka daha önce girilmiş.");

            Temizle();
        }

        private void Temizle()
        {
            txtMarka.Text = "";
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
            txtMarka.Text = Liste.CurrentRow.Cells[1].Value.ToString();
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
            db.Markalars.Remove(db.Markalars.First(x => x.Id == secimId));
            db.SaveChanges();
            MessageBox.Show("Marka başarıyla silinmiştir.");
            Listele();
            Temizle();
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

