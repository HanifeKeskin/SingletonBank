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
    [Table("Musteris")]
    public class Musteri
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MusteriID { get; set; }

        [DisplayName("Ad"), Required(ErrorMessage = "{0} alanı gereklidir."),
            StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır."), RegularExpression("^[a-zA-Z]*$", ErrorMessage = "{0} alanı sadece harf olmalıdır.")]
        public string Name { get; set; }

        [DisplayName("Soyad"), Required(ErrorMessage = "{0} alanı gereklidir."),
            StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Surname { get; set; }

        [DisplayName("TCKN"),
            Required(ErrorMessage = "{0} alanı gereklidir.")]
        public long TCKN { get; set; }

        [DisplayName("Müşteri Numarası"), Required(ErrorMessage = "{0} alanı gereklidir.")]
        public long MusteriNo { get; set; }

        [DisplayName("E-Posta"),
            Required(ErrorMessage = "{0} alanı gereklidir."),
            StringLength(70, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Email { get; set; }

        [DisplayName("Adres"),Required(ErrorMessage = "{0} alanı gereklidir."),
            StringLength(70, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Adres { get; set; }

        [DisplayName("TelefonNo"),Required(ErrorMessage = "{0} alanı gereklidir.")
            ]
        public string TelNo { get; set; }

        [DisplayName("Şifre"),
            Required(ErrorMessage = "{0} alanı gereklidir."),
            StringLength(6, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Password { get; set; }

        public virtual List<Hesap> Hesaps { get; set; }
        
        //public virtual Kredi Kredi { get; set; }

        public Musteri()
        {
            Hesaps = new List<Hesap>();
        }
    }
}
