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
    public class Banka
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BankaID { get; set; }

        [DisplayName("Banka Adı"),
            StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string BankaAdi { get; set; }

        public virtual List<Hesap> hesaps { get; set; }
    }
}
