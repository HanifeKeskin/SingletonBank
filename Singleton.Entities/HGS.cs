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
    [Table("HGS")]
    public class HGS
    {

        //Hesap ilişki ++

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HgsID { get; set; }

        [DisplayName("Hgs Numarası")]
        public long HgsNo { get; set; }

        [DisplayName("Müsteri Numarası")]
        public long MusteriNo { get; set; }

        [DisplayName("Hgs Bakiyesi")]
        public decimal HgsBakiyesi { get; set; }

        [DisplayName("Plaka")]
        public string Plaka { get; set; }

    }
}
