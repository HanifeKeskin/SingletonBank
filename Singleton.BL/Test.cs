using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Singleton.DAL.EntityFramework;
using Singleton.Entities;

namespace Singleton.BL
{
    public class Test
    {
        private Repository<Musteri> repo_musteri = new Repository<Musteri>();
        private Repository<Hesap> repo_hesap = new Repository<Hesap>();

        public Test()
        {
            Repository<Musteri> repo = new Repository<Musteri>();
            repo.List();
        }

        public void InsertTest()
        {
            
            int result = repo_musteri.Insert(new Musteri()
            {
                Name = "Rama",
                Surname = "Alşaban",
                Adres = "Manisa Turgutlu Yurt",
                TelNo = "5397651274",
                Email = "rama@gmail.com",
                MusteriNo = 123456798,
                TCKN = 12349742679,
                Password = "123456",
            });
        }

        public void UpdateTest()
        {
            Musteri musteri = repo_musteri.Find(x => x.TCKN == 12345678912);
             if(musteri != null)
            {
                musteri.Adres = "Bilecik Merkez Hürriyet mah";

                int result = repo_musteri.Update(musteri);
            }
        }



    }
}
