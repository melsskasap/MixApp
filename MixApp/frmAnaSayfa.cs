using MixApp.Bilgi;
using MixApp.Fonksiyonlar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MixApp
{
    public partial class frmAnaSayfa : Form
    {
        Formlar F = new Formlar();
        public frmAnaSayfa()
        {
            InitializeComponent();
        }

        private void frmAnaSayfa_Load(object sender, EventArgs e)
        {
            pnl1.Visible = true;
            pnl2.Visible = false;
            pnl3.Visible = false;
            groupBox1.Text = "Bir İşlemleri";
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            pnl1.Visible = true;
            pnl2.Visible = false;
            pnl3.Visible = false;
            groupBox1.Text = "Bir İşlemleri";
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            pnl1.Visible = false;
            pnl2.Visible = true;
            pnl3.Visible = false;
            groupBox1.Text = "İki İşlemleri";
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            pnl1.Visible = false;
            pnl2.Visible = false;
            pnl3.Visible = true;
            groupBox1.Text = "Üç İşlemleri";
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            Application.Exit(); // garbageCollector mesaj gönderiyor, işim bitti programı kapatabilirsin.
        }

        private void btnCariGiris_Click(object sender, EventArgs e)
        {
            F.CariGirisIslemi(); // Form sayısı arttığında, buradaki işlemleri tekrar tekrar yazmak yerine; Formlar.cs içerisinde bir method tanımlayarak onu çağırıyoruz.
        }

        private void btnUrunGrupGiris_Click(object sender, EventArgs e)
        {
            F.UrunGrupGiris();
        }

        private void btnRenkGiris_Click(object sender, EventArgs e)
        {
            F.RenkGiris();
        }

        private void btnBedenGiris_Click(object sender, EventArgs e)
        {
            F.BedenGiris();
        }

        private void btnSezonGiris_Click(object sender, EventArgs e)
        {
            F.SezonGiris();
        }

       

        private void btnMarka_Click(object sender, EventArgs e)
        {
            F.MarkaGiris();
        }

        private void btnUrunKayit_Click(object sender, EventArgs e)
        {
            F.UrunKayit();
        }
    }
}
