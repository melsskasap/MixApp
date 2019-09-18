using MixApp.Bilgi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MixApp.Fonksiyonlar
{
    public class Formlar
    {
        #region Cari İşlemleri
        public void CariGirisIslemi()
        {
            frmCariGiris frm = new frmCariGiris();
            //frm.MdiParent = ActiveForm;
            frm.MdiParent = Application.OpenForms["frmAnaSayfa"] as frmAnaSayfa;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }
        #endregion

        #region Ürün Grup Girişi
        public void UrunGrupGiris()
        {
            frmUrunGrup frm = new frmUrunGrup();
            frm.ShowDialog();
        }
        #endregion

        #region Renk Girişi
        public void RenkGiris()
        {
            frmRenk frm = new frmRenk();
            frm.ShowDialog();
        }
        #endregion

        #region Beden Girişi
        public void BedenGiris()
        {
            frmBeden frm = new frmBeden();
            frm.ShowDialog();
        }
        #endregion

        #region Sezon Girişi
        public void SezonGiris()
        {
            frmSezon frm = new frmSezon();
            frm.ShowDialog();
        }
        #endregion

        #region Marka Girişi
        public void MarkaGiris()
        {
            frmMarka frm = new frmMarka();
            frm.ShowDialog();
        }
        #endregion

        #region Ürün Kayıt
        public void UrunKayit()
        {
            frmUrunKayıt frm = new frmUrunKayıt();
            //frm.MdiParent = ActiveForm;
            frm.MdiParent = Application.OpenForms["frmAnaSayfa"] as frmAnaSayfa;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }
        #endregion
    }
}
