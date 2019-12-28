using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Singleton.BL;
using Singleton.Entities;
using Singleton.Entities.ValueObject;
using Singleton.WebApp.Models;

namespace Singleton.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private MusteriManager musteriManager = new MusteriManager();
        // GET: Home
        public ActionResult Index()
        {
            BL.Test test = new BL.Test();
            //test.InsertTest();
            //test.UpdateTest();
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                MusteriManager mm = new MusteriManager();
                BusinessLayerResult<Musteri> res =  mm.RegisterMusteri(model);

                if(res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x));
                    return View(model);
                }
                
                return RedirectToAction("RegisterOk");

            }
                
                return View(model); 
        }

        public ActionResult RegisterOk()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                MusteriManager mm = new MusteriManager();
                BusinessLayerResult<Musteri> res = mm.LoginMusteri(model);

                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x));
                    return View(model);
                }

                Session["login"] = res.Result;
                return RedirectToAction("Index");
            }
            

            return View();
        }
        
        public ActionResult EditProfile()
        {
            BusinessLayerResult<Musteri> res =
                musteriManager.GetUserById(CurrentSession.User.MusteriID);

            if (res.Errors.Count > 0)
            {
                //ErrorViewModel errorNotifyObj = new ErrorViewModel()
                //{
                //    Title = "Hata Oluştu",
                //    Items = res.Errors
                //};

                //return View("Error", errorNotifyObj);
            }

            return View(res.Result);
        }

        [HttpPost]
        public ActionResult EditProfile(Musteri model)
        {
            
            if (ModelState.IsValid)
            {
                Musteri mus = musteriManager.Find(x => x.MusteriID == model.MusteriID);
                mus.Name = model.Name;
                mus.Surname = model.Surname;
                mus.Adres = model.Adres;
                mus.Email = model.Email;
                mus.Password = model.Password;
                mus.TelNo = model.TelNo;


                musteriManager.Update(mus);
                return RedirectToAction("ShowProfile");
            }

            return View(model);
        }

        public ActionResult ShowProfile()
        {
            BusinessLayerResult<Musteri> res =
                   musteriManager.GetUserById(CurrentSession.User.MusteriID);

            if (res.Errors.Count > 0)
            {
                //ErrorViewModel errorNotifyObj = new ErrorViewModel()
                //{
                //    Title = "Hata Oluştu",
                //    Items = res.Errors
                //};

                //return View("Error", errorNotifyObj);
            }

            return View(res.Result);
        }

        public ActionResult Logout()
        {
            Session.Clear();

            return RedirectToAction("Index");
        }


    }
}