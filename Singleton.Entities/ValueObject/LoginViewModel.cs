using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton.Entities.ValueObject
{
    public class LoginViewModel
    {
        [DisplayName("TCKN"), Required(ErrorMessage = "{0} alanı boş geçilemez."),
            MinLength(11, ErrorMessage = "{0} min {1} karakter olmalı."),
            MaxLength(11, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string TCKN { get; set; }

        [DisplayName("Şifre"), StringLength(6), Required(ErrorMessage = "{0} alanı boş geçilemez."), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
