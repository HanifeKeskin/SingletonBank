using Singleton.DAL.EntityFramework;
using Singleton.Entities;
using Singleton.BL.Abstract;
using Singleton.Entities.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace Singleton.BL
{
    public class MusteriManager : ManagerBase<Musteri>
    {
        Repository<Musteri> repo_musteri = new Repository<Musteri>();
       
        Random rand = new Random();

        public BusinessLayerResult<Musteri> RegisterMusteri(RegisterViewModel data)
        {
            long tc = Convert.ToInt64(data.TCKN);
            Musteri musteri = Find(x => x.TCKN == tc || x.Email == data.EMail);

            BusinessLayerResult<Musteri> layerResult = new BusinessLayerResult<Musteri>();

            if (musteri != null)
            {
                if(musteri.TCKN == tc)
                {
                    layerResult.Errors.Add("TC Kimlik No kayıtlı");
                }
                if(musteri.Email == data.EMail)
                {
                    layerResult.Errors.Add("Email kayıtlı");
                }
                //if(data.TCKN.Length > 11)
                //{
                //    layerResult.Errors.Add("TC Kimlik numaranız 11 karakter olmalıdır");
                //}
            }
            else
            {
                int dbResult = Insert(new Musteri()
                {
                    TCKN = tc,
                    Name = data.Name,
                    Surname = data.Surname,
                    MusteriNo = rand.Next(10000000, 999999999),
                    Email = data.EMail,
                    Adres = data.Adres,
                    TelNo = data.TelNo,
                    Password = data.Password,
                });

                if(dbResult > 0)
                {
                    layerResult.Result = Find(x => x.TCKN == tc || x.Email == data.EMail);
                }
            }
            

            return layerResult;

        }

        public BusinessLayerResult<Musteri> LoginMusteri(LoginViewModel data)
        {
            long tc = Convert.ToInt64(data.TCKN);
            

            BusinessLayerResult<Musteri> res = new BusinessLayerResult<Musteri>();
            res.Result =  Find(x => x.TCKN == tc && x.Password == data.Password);
            
            if (res.Result != null)
            {
                
            }
            else
            {
                res.Errors.Add("TC Kimlik ya da şifre uyuşmuyor.");
            }


            return res;

        }

        //YARIM
        public BusinessLayerResult<Musteri> UpdateMusteri(Musteri data)
        {
            long tc = Convert.ToInt64(data.TCKN);
            //Musteri musteri = repo_musteri.Find(x => x.TCKN == tc);

            //BusinessLayerResult<Musteri> layerResult = new BusinessLayerResult<Musteri>();
            
            Musteri db_user = Find(x => x.TCKN != tc && (x.Name == data.Name || x.Email == data.Email));
            BusinessLayerResult<Musteri> res = new BusinessLayerResult<Musteri>();

            if (db_user != null && db_user.TCKN != tc)
            {
                if (db_user.TelNo == data.TelNo)
                {
                    //res.AddError(ErrorMessageCode.UsernameAlreadyExist, "Telefon numarası kayıtlı.");
                }

                if (db_user.Email == data.Email)
                {
                    //res.AddError(ErrorMessageCode.EmailAlreadyExists, "E-posta adresi kayıtlı.");
                }

                return res;
            }

            res.Result = Find(x => x.TCKN == tc);
            res.Result.Email = data.Email;
            res.Result.Name = data.Name;
            res.Result.Surname = data.Surname;
            res.Result.Password = data.Password;
            res.Result.TelNo = data.TelNo;
            res.Result.Adres = data.Adres;

            if (base.Update(res.Result) == 0)
            {
                //res.AddError(ErrorMessageCode.ProfileCouldNotUpdated, "Profil güncellenemedi.");
            }

            return res;


            //return layerResult;
        }

        public BusinessLayerResult<Musteri> GetUserById(int id)
        {
            BusinessLayerResult<Musteri> res = new BusinessLayerResult<Musteri>();
            res.Result = Find(x => x.MusteriID == id);

            if (res.Result == null)
            {
                //res.AddError(ErrorMessageCode.UserNotFound, "Kullanıcı bulunamadı.");
            }

            return res;
        }

        public bool Login(long tc, string pass)
        {
            try
            {
                using (var context = new DatabaseContext())
                {
                    bool isUserValid;
                    var result = context.Musteris.FirstOrDefault(x => x.TCKN == tc && x.Password == pass);
                    if (result == null)
                    {
                        isUserValid = false;
                    }
                    else
                    {
                        isUserValid = true;
                    }
                    if (isUserValid)
                    {
                        return true;
                    }
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:KullaniciBusiness::LoginKullanici::Error occured.", ex);
            }
        }

        private static DatabaseContext _context;

        public static DatabaseContext Context
        {
            get
            {//context'in her koşulda dolu gelmesi sağlandı
                if (_context == null)
                    _context = new DatabaseContext();
                return _context;
            }
            set { _context = value; }
        }
        
         
         
         public Musteri SignUp(Musteri musteri)
        {
           
                try
                {
                    Context.Set<Musteri>().Add(musteri);
                    Context.SaveChanges();
                    return musteri;
                }
                catch (Exception ex)
                {
                    throw new Exception("Business:AraclarBusiness::AracEkle::Error occured.", ex);
                }
        }
        public Musteri UpdateKisi(Musteri musteri)
        {
            try
            {
                if (repo_musteri.Update(musteri)==1)
                    return musteri;
                
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("MusteriBusiness:MusteriRepository:Güncelleme Hatası", ex);
            }
        }
        
        //Profil görüntüle
        
    }
}
