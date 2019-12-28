using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton.Entities.ValueObject
{
    public class HgsViewModel
    {
        [DisplayName("Hgs Bakiye"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public string HgsBakiye { get; set; }

    }
}
