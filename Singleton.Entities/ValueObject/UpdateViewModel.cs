using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton.Entities.ValueObject
{
    public class UpdateViewModel
    {
        [DisplayName("Ad"),
            Required(ErrorMessage = "{0} alanı boş geçilemez."),
            StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string Name { get; set; }

        [DisplayName("Soyad"),
            Required(ErrorMessage = "{0} alanı boş geçilemez."),
            StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string Surname { get; set; }

        [DisplayName("Adres"),
            Required(ErrorMessage = "{0} alanı boş geçilemez."),
            StringLength(70, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string Adres { get; set; }

        [DisplayName("Telefon No"),
            Required(ErrorMessage = "{0} alanı boş geçilemez."),
            StringLength(11, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string TelNo { get; set; }

        [DisplayName("TCKN"),
            Required(ErrorMessage = "{0} alanı gereklidir."),
            StringLength(11, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string TCKN { get; set; }

        [DisplayName("E-posta"),
            Required(ErrorMessage = "{0} alanı boş geçilemez."),
            StringLength(70, ErrorMessage = "{0} max. {1} karakter olmalı."),
            EmailAddress(ErrorMessage = "{0} alanı için geçerli bir e-posta adresi giriniz.")]
        public string EMail { get; set; }

        [DisplayName("Şifre"),
            Required(ErrorMessage = "{0} alanı boş geçilemez."),
            DataType(DataType.Password),
            StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string Password { get; set; }
    }
}
