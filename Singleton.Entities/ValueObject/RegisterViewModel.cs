using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton.Entities.ValueObject
{
    public class RegisterViewModel
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
            MinLength(10, ErrorMessage = "{0} min {1} karakter olmalı."),
            MaxLength(10, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string TelNo { get; set; }

        [DisplayName("TCKN"), 
            Required(ErrorMessage = "{0} alanı boş geçilemez."),
            MinLength(11, ErrorMessage = "{0} min {1} karakter olmalı."),
            MaxLength(11, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır."),
            ]
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

        //[DisplayName("Şifre Tekrar"),
        //    Required(ErrorMessage = "{0} alanı boş geçilemez."),
        //    DataType(DataType.Password),
        //    StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı."),
        //    Compare("Password", ErrorMessage = "{0} ile {1} uyuşmuyor.")]
        //public string RePassword { get; set; }
    }
}
