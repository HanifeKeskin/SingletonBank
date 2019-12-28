using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Singleton.Entities;

namespace Singleton.DAL.EntityFramework
{
    public class MyInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        Random rand = new Random();

        protected override void Seed(DatabaseContext context)
        {
            Musteri musteri1 = new Musteri()
            {
                Name = "Beyza",
                Surname = "Ozkara",
                TCKN = 12345678912,
                Password = "123456",
                Email = "beyza@gmail.com",
                Adres = "Manisa turgutlu celal bayar",
                TelNo = "5123456789",
                MusteriNo = rand.Next(100000000, 999999999)
                //MusteriID = 111222333
            };

            Musteri musteri2 = new Musteri()
            {
                Name = "Hanife",
                Surname = "Keskin",
                TCKN = 21345987612,
                Password = "123456",
                Email = "hanife@gmail.com",
                Adres = "Manisa turgutlu celal bayar",
                TelNo = "5975462938",
                MusteriNo = rand.Next(100000000, 999999999)
                //MusteriID = 111222334
                // $"musteriID{i}" formatı kullanılabilir
            };

            context.Musteris.Add(musteri1);
            context.Musteris.Add(musteri2);
            context.SaveChanges();

            Banka banka1 = new Banka()
            {
                BankaAdi = "SingletonBank"
            };

            context.Bankas.Add(banka1);
            context.SaveChanges();


            //Hesap musteri1Hesap = new Hesap()
            //{
            //    MusteriID = musteri1.MusteriID,
            //    Bakiye = 1000,
            //    EkNo = 1000,
            //    HesapNo = 1112223331122,
            //    IBAN = 123456789,
            //    Durum = true,
            //    BankaID = banka1.BankaID,
            //    MusteriNo = musteri1.MusteriNo
            //};

            //context.Hesaps.Add(musteri1Hesap);
            //context.SaveChanges();
        }
    }
}
