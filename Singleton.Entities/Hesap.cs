using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton.Entities
{
    [Table("Hesaps")]
    public class Hesap
    {
        //private long hesapno;
        //MÜSTERİ VE BANKA İLİŞKİ ++

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HesapID { get; set; }

        [DisplayName("Bakiye")]
        public decimal Bakiye { get; set; }

        [DisplayName("Müsteri Numarası")]
        public long MusteriNo { get; set; }

        [DisplayName("Ek No")]
        public int EkNo { get; set; }

        [DisplayName("HesapNo")]
        public long HesapNo { get; set; }

        [DisplayName("IBAN")]
        public long IBAN { get; set; }

        [DisplayName("Durum")]
        public bool Durum { get; set; }

        public int MusteriID { get; set; }

        public int BankaID { get; set; }

        //public virtual List<Transfer> Transfers { get; set; }

        public virtual Musteri Musteri { get; set; }

        public virtual Banka Banka { get; set; }
        

        //public Hesap()
        //{
        //    Transfers = new List<Transfer>();
        //}
    }
}
