using Singleton.BL.Abstract;
using Singleton.DAL.EntityFramework;
using Singleton.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton.BL
{
    public class HesapManager : ManagerBase<Hesap>
    {
        //public Repository<Hesap> repo_hesap  =  new Repository<Hesap>();
        Random rand = new Random();

        public BusinessLayerResult<Hesap> RegisterHesap(Hesap hesap)
        {
            
            BusinessLayerResult<Hesap> layerResult = new BusinessLayerResult<Hesap>();

            if (hesap.MusteriID != 0 )
            {
                
                int dbResult = Insert(new Hesap()
                {
                    Bakiye = 0,
                    MusteriNo = hesap.MusteriNo,
                    //EkNo = rand.Next(1000, 1111),
                    BankaID = 1,
                    MusteriID = hesap.MusteriID,
                    Durum = true,
                    IBAN = rand.Next(111111111, 999999999)
                });

                if (dbResult > 0)
                {
                    //tekrar bak
                    layerResult.Result = Find(x => x.HesapNo == hesap.HesapNo);
                }
            }
            else
            {
            }
            
            return layerResult;

        }
        public BusinessLayerResult<Hesap> DeleteHesap(Hesap data)
        {
            long hn = Convert.ToInt64(data.HesapNo);
            //Musteri musteri = repo_musteri.Find(x => x.TCKN == tc);

            //BusinessLayerResult<Musteri> layerResult = new BusinessLayerResult<Musteri>();
            Hesap db_hesap = Find(x => x.HesapNo == hn);
            BusinessLayerResult<Hesap> res = new BusinessLayerResult<Hesap>();

            if (db_hesap != null && db_hesap.HesapNo == hn)
            {
                return res;
            }

            res.Result = Find(x => x.HesapNo == hn);
            res.Result.Durum = false;

            if (base.Update(res.Result) == 0)
            {
                //res.AddError(ErrorMessageCode.ProfileCouldNotUpdated, "Profil güncellenemedi.");
            }

            return res;

            //return layerResult;
        }


    }
}
