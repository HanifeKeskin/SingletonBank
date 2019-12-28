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
    [Table("Transfers")]
    public class Transfer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransferID { get; set; }

        [DisplayName("Zaman")]
        public DateTime Zaman { get; set; }

        [DisplayName("Tutar")]
        public string Tutar { get; set; }

        [DisplayName("Transfer Türü")]
        public bool TransferTuru { get; set; }

        [DisplayName("Gönderen Hesap")]
        public long GonderenHesapNo { get; set; }

        [DisplayName("Alıcı Hesap")]
        public string AliciHesapNo { get; set; }

        //public int HesapID { get; set; }

        //public virtual Hesap Hesap { get; set; }

    }
}
