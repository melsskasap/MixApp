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
    public partial class frmUrunKayıt : Form
    {
        MixDoContext db = new MixDoContext();
        public string[] MyArray { get; set; }

        public frmUrunKayıt()
        {
            InitializeComponent();
        }

        private void frmUrunKayıt_Load(object sender, EventArgs e)
        {
            Combo();
        }

        private void Combo()
        {
            //txtCari.DataSource = db.Carilers.ToList();
            //txtCari.ValueMember = "Id";
            //txtCari.DisplayMember = "Cari Adi";
            //txtCari.SelectedIndex = -1;

            AutoCompleteStringCollection veri = new AutoCompleteStringCollection();
            var lst = db.Carilers.Select(x=>x.CariAdi).Distinct();
            foreach (string cr in lst)
            {
                veri.Add(cr);
                txtCari.Items.Add(cr);
            }
            txtCari.AutoCompleteCustomSource = veri;

            txtUrunGrup.DataSource = db.Carilers.ToList();
            txtUrunGrup.ValueMember = "Id";
            txtUrunGrup.DisplayMember = "GrupAdi";
            txtUrunGrup.SelectedIndex = -1;

            var dgv = txtUrunGrup.Items.Count;
            MyArray = new string[dgv];
            for (int p = 0; p < dgv; p++)
            {
                MyArray[p] = txtUrunGrup.Items[p].ToString();
            }
        }

        private void Liste_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)

        {
            var cb = e.Control as ComboBox;
            if (Liste.CurrentCell.ColumnIndex == 0 && cb!=null)
            {
                cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cb.AutoCompleteSource = AutoCompleteSource.CustomSource;
                cb.AutoCompleteCustomSource.AddRange(MyArray);
            }
            else if (Liste.CurrentCell.ColumnIndex !=2 && cb!=null)
            {
                cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            }
        }
    }
}
