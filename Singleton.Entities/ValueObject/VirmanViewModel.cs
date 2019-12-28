using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton.Entities.ValueObject
{
    public class VirmanViewModel
    {
        [DisplayName("Gönderen Hesap"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public long GonderenHesapNo { get; set; }

        [DisplayName("Alıcı Hesap"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public long AlıcıHesapNo { get; set; }

        [DisplayName("Tutar"), Required(ErrorMessage = "{0} alanı boş geçilemez."), MaxLength(10000)]
        public string Tutar { get; set; }
    }
}
