using Singleton.BL;
using Singleton.Entities;
using Singleton.RestApi.Models;
using Singleton.RestApi.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;

namespace Singleton.RestApi.Controllers
{
    public class MusteriController : ApiController
    {
        MusteriManager musteriManager = new MusteriManager();

        [HttpPost]
        public IHttpActionResult Loginpost(long tc, string pass)
        {
            
                bool dogruMu = musteriManager.Login(tc, pass);
                SifreKontrol kontrol = new SifreKontrol { isValid = dogruMu };
                List<SifreKontrol> sifreKontrol = new List<SifreKontrol>();
                sifreKontrol.Add(kontrol);

                var content = new ResponseContent<SifreKontrol>(sifreKontrol);
                // Return content as a json and proper http response
                return new StandartResult<SifreKontrol>(content, Request);

            
        }

        [HttpPost]
        public IHttpActionResult SignUpPost([FromBody]Musteri musteri)
        {
            var content = new ResponseContent<Musteri>(null);
            if (musteri != null)
            {
                
                    if(musteriManager.Insert(musteri) == 1)
                    {
                        content.Result = "1";
                    }
                    else
                    {
                        content.Result = "0";
                    }
                    //content.Result = musteriManager.Insert(musteri) ? "1" : "0";

                    return new StandartResult<Musteri>(content, Request);
                
            }

            content.Result = "0";

            return new StandartResult<Musteri>(content, Request);
        }

        [HttpPost]
        [Route("api/Musteri/Update")]
        public IHttpActionResult UpdateKisiPost(Musteri musteri)
        {
            var content = new ResponseContent<Musteri>(null);

            if (musteriManager.Update(musteri)==1)
            {
                Musteri mus = musteriManager.Find(x => x.MusteriID == CurrentSession.User.MusteriID);
                mus.Name = musteri.Name;
                mus.Surname = musteri.Surname;
                mus.Adres = musteri.Adres;
                mus.Email = musteri.Email;
                mus.Password = musteri.Password;
                mus.TelNo = musteri.TelNo;
                musteriManager.Update(mus);

                content.Result = "1";
            }
            else
            {
                content.Result = "0";
            }

            return new StandartResult<Musteri>(content, Request);
        }
    }
}
