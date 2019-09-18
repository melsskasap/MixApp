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
    public partial class frmSezon : Form
    {
        MixDoContext db = new MixDoContext();
        int secimId = -1;
        bool edit = false;
        public frmSezon()
        {
            InitializeComponent();
        }

        private void frmSezon_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void Listele()
        {
            Liste.Rows.Clear();
            int i = 0;
            var szn = db.Sezonlars.ToList();
            foreach (var k in szn)
            {
                Liste.Rows.Add();
                Liste.Rows[i].Cells[0].Value = k.Id;
                Liste.Rows[i].Cells[1].Value = k.Sezon;
                i++;
            }
            Liste.AllowUserToAddRows = false;
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            Ekle();
        }

        private void Ekle()
        {
            if (txtSezon.Text != "")
            {
                Sezonlar szn = new Sezonlar();
                szn.Sezon = txtSezon.Text;
                db.Sezonlars.Add(szn);
                db.SaveChanges();
                MessageBox.Show("Sezon başarıyla eklenmiştir.");
                Listele();
                Temizle();
            }
        }

        private void Temizle()
        {
            txtSezon.Text = "";
            secimId = -1;
            edit = false;
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            Close();
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
            txtSezon.Text = Liste.CurrentRow.Cells[1].Value.ToString();
            secimId = int.Parse(Liste.CurrentRow.Cells[0].Value.ToString());
        }
    }
}
