using Singleton.BL;
using Singleton.Entities;
using Singleton.Entities.ValueObject;
using Singleton.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Singleton.WebApp.Controllers
{
    public class HgsController : Controller
    {
        Random rand = new Random();
        HgsManager hgsManager = new HgsManager();
        HesapManager hesapManager = new HesapManager();
        HgsSatisManager hgsSatisManager = new HgsSatisManager();


        // GET: Hgs
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HgsBasvuru()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HgsBasvuru(HGS hgs)
        {
            BusinessLayerResult<HGS> layerResult = new BusinessLayerResult<HGS>();
            HGS plk = hgsManager.Find(x => x.Plaka == hgs.Plaka);
            if (ModelState.IsValid)
            {
                if(hgs.Plaka == null || hgs.Plaka == "0")
                {
                    layerResult.Errors.Add("Lütfen plakayı 7-8 haneli bir değer olarak giriniz!");
                }
                else if(plk != null)
                {
                    layerResult.Errors.Add("Lütfen farklı bir plaka giriniz");
                }
                else 
                {
                    //servis if true
                    hgs.MusteriNo = CurrentSession.User.MusteriNo;
                    hgs.HgsBakiyesi = 0;
                    hgs.HgsNo = (rand.Next(100000000, 999999999) + rand.Next(0, 9));
                    //ServiceReference1.hgsEkleRequest servis = new ServiceReference1.hgsEkleRequest();
                    ServiceReference1.HgsWebServiceSoapClient servis = new ServiceReference1.HgsWebServiceSoapClient();
                    bool sonuc = servis.hgsEkle(hgs.HgsNo, CurrentSession.User.TCKN, CurrentSession.User.Name, CurrentSession.User.Surname, hgs.Plaka);

                    if (sonuc == true)
                    {
                        hgsManager.Insert(hgs);
                        //işlem gerçekleşti ekranı 
                        return RedirectToAction("HgsBilgi", "HGS", new { id = hgs.HgsID });
                    }
                    else
                    {
                        //isleminzi gerçekleşmedi ekranı
                        layerResult.Errors.Add("İşleminiz gerçekleştirilemedi, Lütfen tekrar deneyiniz");
                    }

                    if (layerResult.Errors.Count > 0)
                    {
                        layerResult.Errors.ForEach(x => ModelState.AddModelError("", x));
                        return View(hgs);
                    }
                }
                
            }

            return RedirectToAction("Index");
        }

        public ActionResult HgsIslemleri()
        {
            List<HGS> hgsler = new List<HGS>();

            foreach (HGS hes in hgsManager.List())
            {
                if (hes.MusteriNo == CurrentSession.User.MusteriNo)
                {
                    hgsler.Add(hes);
                }
            }
            //ViewBag.hgsler = new SelectList(hgsler, "HgsNo", "HgsNo");
            TempData["hgsler"] = new SelectList(hgsler, "HgsNo", "HgsNo");

            return View();
        }
        
        [HttpPost]
        public ActionResult HgsIslemleri(HGS hgs)
        {
            if (ModelState.IsValid)
            {
                HGS hes = hgsManager.Find(x => x.HgsNo == hgs.HgsNo);
                //int id = hes.HgsID;
                return RedirectToAction("HgsBilgi","HGS", new { id = hes.HgsID });
            }
            return View(hgs);
        }
        
        public ActionResult HgsBilgi(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HGS hgs = hgsManager.Find(x => x.HgsID == id.Value);

            if (hgs == null)
            {
                return HttpNotFound();
            }

            return View(hgs);
        }

        public ActionResult HgsSorgu(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //servis
            HGS hgs = hgsManager.Find(x => x.HgsID == id.Value);
            if (hgs == null)
            {
                return HttpNotFound();
            }

            ServiceReference1.HgsWebServiceSoapClient servis = new ServiceReference1.HgsWebServiceSoapClient();
            string bakiye = servis.hgsSorgula(hgs.HgsID);
            bakiye = bakiye.Replace(".", ",");

            if (bakiye.ToString() != null)
            {
                hgs.HgsBakiyesi = decimal.Parse(bakiye);
                hgsManager.Update(hgs);
            }

            return View(hgs);
        }

        public ActionResult HgsSatis(int? id)
        {
            HesapManager hesapManager = new HesapManager();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HGS hgs = hgsManager.Find(x => x.HgsID == id.Value);
            if (hgs == null)
            {
                return HttpNotFound();
            }

            HgsSatis hgsSatis = new HgsSatis();
            hgsSatis.HgsNo = hgs.HgsNo;

            List<Hesap> hesaplar = new List<Hesap>();
            foreach (Hesap hes in hesapManager.List())
            {
            if (hes.MusteriNo == CurrentSession.User.MusteriNo && hes.Bakiye != 0)
            {
               hesaplar.Add(hes);
            }
            }
            //ViewBag.hgsler = new SelectList(hgsler, "HgsNo", "HgsNo");
            TempData["hesaplar"] = new SelectList(hesaplar, "HesapNo", "HesapNo");

            return View(hgsSatis);
        }

        [HttpPost]
        public ActionResult HgsSatis(HgsSatis hgsSatis)
        {
            BusinessLayerResult<HgsSatis> layerResult = new BusinessLayerResult<HgsSatis>();
            HgsSatisManager hgsSatisManager = new HgsSatisManager();

            if (ModelState.IsValid)
            {
                HGS hgs = hgsManager.Find(x => x.HgsNo == hgsSatis.HgsNo);
                Hesap kullanilan = hesapManager.Find(x => x.HesapNo == hgsSatis.HesapNo);

                if (kullanilan == null)
                {
                    layerResult.Errors.Add("Girilen alıcı hesap numarası yanlış veya eksik");
                }
                else if (hgsSatis.Tutar.ToString() == null || hgsSatis.Tutar == 0)
                {
                    layerResult.Errors.Add("Lütfen 0'dan başka bir tutar giriniz");
                }
                else if (kullanilan.Bakiye < hgsSatis.Tutar)
                {
                    layerResult.Errors.Add("Yetersiz bakiye");
                }
                else 
                {
                    ServiceReference1.HgsWebServiceSoapClient servis = new ServiceReference1.HgsWebServiceSoapClient();
                    decimal bakiye = Convert.ToDecimal(servis.hgsSatis(hgs.HgsBakiyesi, hgs.HgsID, hgsSatis.Tutar));

                    if (bakiye.ToString() != null)
                    {
                        hgsSatis.Zaman = DateTime.Now;

                        kullanilan.Bakiye = (kullanilan.Bakiye - hgsSatis.Tutar);
                        hesapManager.Update(kullanilan);


                        hgs.HgsBakiyesi = hgs.HgsBakiyesi + hgsSatis.Tutar;
                        hgsManager.Update(hgs);

                        hgsSatisManager.Insert(hgsSatis);
                    }

                }

                if (layerResult.Errors.Count > 0)
                {
                    layerResult.Errors.ForEach(x => ModelState.AddModelError("", x)); List<Hesap> hesaplar = new List<Hesap>();
                    foreach (Hesap hes in hesapManager.List())
                    {
                        if (hes.MusteriNo == CurrentSession.User.MusteriNo && hes.Bakiye != 0)
                        {
                            hesaplar.Add(hes);
                        }
                    }
                    //ViewBag.hgsler = new SelectList(hgsler, "HgsNo", "HgsNo");
                    TempData["hesaplar"] = new SelectList(hesaplar, "HesapNo", "HesapNo");
                    return View(hgsSatis);
                }
                return RedirectToAction("HgsBilgi", "HGS", new { id = hgs.HgsID });
            }
            return View(hgsSatis);
        }
        
    }
}