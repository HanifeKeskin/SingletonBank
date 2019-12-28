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
    [Table("Kredis")]
    public class Kredi
    {
        //Müsteri ilişki ++

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int KrediID { get; set; }

        [DisplayName("Tutar")]
        public decimal Tutar { get; set; }

        public int MusteriID { get; set; }

        public virtual Musteri Musteri { get; set; }
    }
}
