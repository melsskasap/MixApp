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
    public partial class frmBeden : Form
    {
        MixDoContext db = new MixDoContext();
        int secimId = -1;
        bool edit = false;
        public frmBeden()
        {
            InitializeComponent();
        }

        private void frmBeden_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void Listele()
        {
            Liste.Rows.Clear();
            int i = 0;
            var lst = db.Bedenlers.ToList();
            foreach (var k in lst)
            {
                Liste.Rows.Add();
                Liste.Rows[i].Cells[0].Value = k.Id;
                Liste.Rows[i].Cells[1].Value = k.Beden;
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
            Bedenler bdn = db.Bedenlers.First(x => x.Id == secimId);
            bdn.Beden = txtBeden.Text;
            db.SaveChanges();
            MessageBox.Show("Beden başarıyla güncellenmiştir");
            Listele();
            Temizle();
        }

        private void Ekle()
        {
            var cont = (from k in db.Bedenlers
                        where (k.Beden.ToLower() == txtBeden.Text.ToLower())
                        select k).ToList();

            if (cont.Count == 0)
            {
                if (txtBeden.Text != "")
                {
                    Bedenler bdn = new Bedenler();
                    bdn.Beden = txtBeden.Text;
                    db.Bedenlers.Add(bdn);
                    db.SaveChanges();
                    MessageBox.Show("Beden başarıyla eklenmiştir.");
                    Listele();
                }
                else
                    MessageBox.Show("Beden boş geçilemez.");
            }
            else
                MessageBox.Show("Beden daha önce girilmiş.");

            Temizle();
        }

        private void Temizle()
        {
            txtBeden.Text = "";
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
            txtBeden.Text = Liste.CurrentRow.Cells[1].Value.ToString();
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
            db.Bedenlers.Remove(db.Bedenlers.First(x => x.Id == secimId));
            db.SaveChanges();
            MessageBox.Show("Beden başarıyla silinmiştir.");
            Listele();
            Temizle();
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
