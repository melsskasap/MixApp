using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixApp.Model
{
    public class Urunler : BaseEntity
    {

        public int CariId { get; set; }
        [ForeignKey("CariId")]
        public virtual Cariler Cariler { get; set; }

        public int UrunGrupId { get; set; }
        [ForeignKey("UrunGrupId")]
        public virtual UrunGruplari UrunGruplari { get; set; }

        public int RenkId { get; set; }
        [ForeignKey("RenkId")]
        public virtual Renkler Renkler { get; set; }

        public int BedenId { get; set; }
        [ForeignKey("BedenId")]
        public virtual Bedenler Bedenler { get; set; }

        public int SezonId { get; set; }
        [ForeignKey("SezonId")]
        public virtual Sezonlar Sezonlar { get; set; }

        public int MarkaId { get; set; }
        [ForeignKey("MarkaId")]
        public virtual Markalar Markalar { get; set; }

        public string Aciklama { get; set; }

        public string Barkod { get; set; }

    }
}
