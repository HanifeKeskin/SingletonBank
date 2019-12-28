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
    [Table("HgsSatis")]
    public class HgsSatis
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HgsSatisID { get; set; }

        [DisplayName("Hgs Numarası")]
        public long HgsNo { get; set; }

        [DisplayName("Hgs Satış Tutarı")]
        public decimal Tutar { get; set; }

        [DisplayName("Hesap Numarası")]
        public long HesapNo { get; set; }

        [DisplayName("Zaman")]
        public DateTime Zaman { get; set; }
    }
}
