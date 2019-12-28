using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Singleton.BL;
using Singleton.Entities;
using Singleton.Entities.ValueObject;
using Singleton.WebApp.Models;

namespace Singleton.WebApp.Controllers
{
    public class HesapController : Controller
    {
        Random ran = new Random();
        HesapManager hesapManager = new HesapManager();

        TransferManager transferManager = new TransferManager();

        // GET: Hesap
        public ActionResult Index()
        {
            return View(hesapManager.List());
        }

        // GET: Hesap/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Hesap hesap = hesapManager.Find(x => x.HesapID == id.Value);

            if (hesap == null)
            {
                return HttpNotFound();
            }

            return View(hesap);
        }

        // GET: Hesap/Create
        public ActionResult Create()
        {
            MusteriManager musteriManager = new MusteriManager();
            BusinessLayerResult<Musteri> res = musteriManager.GetUserById(CurrentSession.User.MusteriID);

            return View(res.Result);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Hesap hesap)
        {
            if(ModelState.IsValid)
            {
                hesap.MusteriID = CurrentSession.User.MusteriID;
                hesap.MusteriNo = CurrentSession.User.MusteriNo;

                int eknumara = 1000;
                foreach (Hesap hesa in hesapManager.List())
                {
                    if(hesa.MusteriNo == hesap.MusteriNo)
                    {
                    Hesap musteri = hesapManager.Find(x => x.MusteriNo == hesap.MusteriNo);
                    if(musteri == null)
                    {
                        hesap.EkNo = eknumara;
                    }
                    else
                    {
                        eknumara = eknumara + 1;
                        hesap.EkNo = eknumara;
                    }
                        //Hesap hes = hesapManager.Find(x => x.EkNo == hesa.EkNo);
                        //if(hes != null)
                        //{
                        //    eknumara = eknumara + 1;
                        //}
                    }
                }

                hesap.EkNo = eknumara;
                eknumara = 1000;

                string hno = (hesap.MusteriNo.ToString() + hesap.EkNo.ToString());
                hesap.HesapNo = Convert.ToInt64(hno);
                hesap.BankaID = 1;
                hesap.Bakiye = 0;
                hesap.Durum = true;
                hesap.IBAN = ran.Next(111111111, 999999999);
                hesapManager.Insert(hesap);
            }
            else
            {

            }
            return RedirectToAction("Index");
        }

        // GET: Hesap/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Hesap hesap = hesapManager.Find(x => x.HesapID == id.Value);

            if (hesap == null)
            {
                return HttpNotFound();
            }
            
            return View(hesap);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Hesap hesap)
        {
            BusinessLayerResult<Hesap> layerResult = new BusinessLayerResult<Hesap>();

            if (ModelState.IsValid)
            {
                Hesap hes = hesapManager.Find(x => x.HesapID == hesap.HesapID);
                if (hes.Bakiye != 0)
                {
                    layerResult.Errors.Add("Lütfen hesabınızdaki parayı çekip tekrar deneyin");
                }
                else
                {
                    hes.Durum = false;

                    hesapManager.Update(hes);
                    return RedirectToAction("Index");
                }

                if (layerResult.Errors.Count > 0)
                {
                    layerResult.Errors.ForEach(x => ModelState.AddModelError("", x));
                    
                    return View(hesap);
                }
            }

            return View(hesap);
        }

        // GET: Hesap/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hesap hesap = hesapManager.Find(x=> x.HesapID == id.Value);

            if (hesap == null)
            {
                return HttpNotFound();
            }
            return View(hesap);
        }

        // POST: Hesap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hesap hesap = hesapManager.Find(x=> x.HesapID == id);

            //hesapManager.Delete(hesap);
            return RedirectToAction("Index");
        }

        //public ActionResult Virman()
        //{
        ////    MusteriManager musteriManager = new MusteriManager();
        ////    BusinessLayerResult<Musteri> res = musteriManager.GetUserById(CurrentSession.User.MusteriID);

        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Virman(VirmanViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Hesap gonderen = hesapManager.Find(x => x.HesapNo == model.GonderenHesapNo);
        //        Hesap alici = hesapManager.Find(x => x.HesapNo == model.AlıcıHesapNo);

        //        decimal para = Convert.ToInt64(model.Tutar);
                
        //        if(gonderen.Durum == true && alici.Durum == true && gonderen.Bakiye >= para)
        //        {
        //            gonderen.Bakiye = (gonderen.Bakiye - para);
        //            alici.Bakiye = (alici.Bakiye + para);
        //            hesapManager.Update(gonderen);
        //            hesapManager.Update(alici);
        //        }
        //        else
        //        {
        //            return View(model);
        //        }

        //        return RedirectToAction("Index");
        //    }
        //    return View(model);
        //}

        //public ActionResult Havale()
        //{
        //    //    MusteriManager musteriManager = new MusteriManager();
        //    //    BusinessLayerResult<Musteri> res = musteriManager.GetUserById(CurrentSession.User.MusteriID);

        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Havale(HavaleViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Hesap gonderen = hesapManager.Find(x => x.HesapNo == model.GonderenHesapNo);
        //        Hesap alici = hesapManager.Find(x => x.HesapNo == model.AlıcıHesapNo);

        //        decimal para = Convert.ToInt64(model.Tutar);

        //        if (gonderen.MusteriNo == alici.MusteriNo)
        //        {
        //            //Virman sayfasına yönlendiriliyorsunuz sayfası
        //            return RedirectToAction("Virman");
        //        }
        //        if (gonderen.Durum == true && alici.Durum == true && gonderen.Bakiye >= para)
        //        {
        //            gonderen.Bakiye = (gonderen.Bakiye - para);
        //            alici.Bakiye = (alici.Bakiye + para);
        //            hesapManager.Update(gonderen);
        //            hesapManager.Update(alici);

        //            //Araya havale başaralı sayfası koyulabilir
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            return View(model);
        //        }

        //    }
        //    return View(model);
        //}

        //public ActionResult Transfer(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    Hesap hesap = hesapManager.Find(x => x.HesapID == id.Value);

        //    Transfer tran = new Transfer();
        //    tran.GonderenHesapNo = hesap.HesapNo;

        //    if (hesap == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(tran);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Transfer(Transfer tran)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Hesap gonderen = hesapManager.Find(x => x.HesapNo == tran.GonderenHesapNo);

        //        Hesap alici = hesapManager.Find(x => x.HesapNo == tran.AliciHesapNo);
                
        //        tran.Zaman = DateTime.Now;
        //        //tran.GonderenHesapNo = gonderen.HesapNo;
        //        tran.AliciHesapNo = alici.HesapNo;

        //        if(gonderen.MusteriNo == alici.MusteriNo)
        //        {
        //            tran.TransferTuru = true; //Virman
        //        }
        //        else
        //        {
        //            tran.TransferTuru = false; //Havale
        //        }

        //        decimal para = Convert.ToInt64(tran.Tutar);

        //        if (alici.Durum == true && gonderen.Bakiye >= para)
        //        {
        //            gonderen.Bakiye = (gonderen.Bakiye - para);
        //            alici.Bakiye = (alici.Bakiye + para);
        //            hesapManager.Update(gonderen);
        //            hesapManager.Update(alici);
        //            transferManager.Insert(tran);
        //        }
        //        else
        //        {
        //            return View(tran);
        //        }

        //        return RedirectToAction("Index");

        //        //Hesap hes = hesapManager.Find(x => x.HesapID == hesap.HesapID);


        //        //hesapManager.Update(hes);
        //        //return RedirectToAction("Index");
        //    }

        //    return View(tran);
        //}

        public ActionResult Cek(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Hesap hesap = hesapManager.Find(x => x.HesapID == id.Value);

            ParaViewModel model = new ParaViewModel();
            model.ParaHesapNo = hesap.HesapNo;
            model.Bakiye = hesap.Bakiye;

            if (hesap == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Cek(ParaViewModel model)
        {

            BusinessLayerResult<ParaViewModel> layerResult = new BusinessLayerResult<ParaViewModel>();

            if (ModelState.IsValid)
            {
                if (Decimal.Parse(model.Tutar) == 0)
                {
                    layerResult.Errors.Add("0'dan başka bir tutar giriniz");
                }
                else
                {
                    long hesap = model.ParaHesapNo;
                    Hesap hes = hesapManager.Find(x => x.HesapNo == hesap);

                    decimal para = Decimal.Parse(model.Tutar);

                    if (hes.Bakiye >= para)
                    {
                        hes.Bakiye = (hes.Bakiye - para);

                        hesapManager.Update(hes);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        layerResult.Errors.Add("Yetersiz Bakiye");
                        //yönlendirme sayfası -- yetersiz bakiye durumu
                    }
                }
                if (layerResult.Errors.Count > 0)
                {
                    layerResult.Errors.ForEach(x => ModelState.AddModelError("", x));
                    return View(model);
                }

            }

            return View(model);
        }


        public ActionResult Yatir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Hesap hesap = hesapManager.Find(x => x.HesapID == id.Value);

            ParaViewModel model = new ParaViewModel();
            model.ParaHesapNo = hesap.HesapNo;
            model.Bakiye = hesap.Bakiye;

            if (hesap == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Yatir(ParaViewModel model)
        {
            BusinessLayerResult<ParaViewModel> layerResult = new BusinessLayerResult<ParaViewModel>();
            if (ModelState.IsValid)
            {
                if(Decimal.Parse(model.Tutar) == 0)
                {
                    layerResult.Errors.Add("0'dan başka bir tutar giriniz");
                }
                else
                {
                    long hesap = model.ParaHesapNo;
                    Hesap hes = hesapManager.Find(x => x.HesapNo == hesap);

                    decimal para = Decimal.Parse(model.Tutar);

                    hes.Bakiye = (hes.Bakiye + para);

                    hesapManager.Update(hes);
                    return RedirectToAction("Index");
                }

                if (layerResult.Errors.Count > 0)
                {
                    layerResult.Errors.ForEach(x => ModelState.AddModelError("", x));
                    return View(model);
                }
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Virman(int? id)
        {
            //@Html.DropDownListFor(model => model.HesapNo, new SelectList(ViewBag.HesapList, "HesapNo", "Bakiye" ))
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Hesap hesap = hesapManager.Find(x => x.HesapID == id.Value);

            if (hesap == null)
            {
                return HttpNotFound();
            }

            Transfer tran = new Transfer();
            tran.GonderenHesapNo = hesap.HesapNo;

            List<Hesap> hesaplar = new List<Hesap>();

            foreach (Hesap hes in hesapManager.List())
            {
                if (hes.MusteriNo == CurrentSession.User.MusteriNo && hes.Durum == true && hes.HesapNo != hesap.HesapNo)
                {
                    hesaplar.Add(hes);
                }
            }
            TempData["hesaplar"] = new SelectList(hesaplar, "HesapNo", "HesapNo");

            return View(tran);
        }

        [HttpPost]
        public ActionResult Virman(Transfer tran)
        {
            BusinessLayerResult<Transfer> layerResult = new BusinessLayerResult<Transfer>();

            if (ModelState.IsValid)
            {
                Hesap kontrol = hesapManager.Find(x => x.HesapNo == tran.GonderenHesapNo);
                Hesap gonderen = hesapManager.Find(x => x.HesapNo == tran.GonderenHesapNo);
                long aliciHesapNo = Convert.ToInt64(tran.AliciHesapNo);

                Hesap alici = hesapManager.Find(x => x.HesapNo == aliciHesapNo);

                if(aliciHesapNo == 0)
                {
                    layerResult.Errors.Add("Girilen alıcı hesap numarası yanlış veya eksik");
                }
                if(alici == null)
                {
                    layerResult.Errors.Add("Yanlış hesap numarası girdiniz!");
                }
                else
                {
                    if(Decimal.Parse(tran.Tutar) == 0)
                    {
                        layerResult.Errors.Add("Lütfen 0'dan başka bir tutar giriniz");
                    }
                    else
                    {

                        tran.Zaman = DateTime.Now;
                        //tran.GonderenHesapNo = gonderen.HesapNo;
                        tran.AliciHesapNo = alici.HesapNo.ToString();

                        if (gonderen.MusteriNo == alici.MusteriNo)
                        {
                            tran.TransferTuru = true; //Virman
                        }
                        else
                        {
                            tran.TransferTuru = false; //Havale
                        }

                        decimal para = Decimal.Parse(tran.Tutar);

                        if (alici.Durum == true && gonderen.Bakiye >= para)
                        {
                            gonderen.Bakiye = (gonderen.Bakiye - para);
                            alici.Bakiye = (alici.Bakiye + para);
                            hesapManager.Update(gonderen);
                            hesapManager.Update(alici);
                            transferManager.Insert(tran);
                        }
                        else if (gonderen.Bakiye < para)
                        {
                            layerResult.Errors.Add("Girilen tutar, hesap bakiyesinden yüksek. Lütfen uygun bir tutar giriniz!");
                        }
                    }

                }

                if (layerResult.Errors.Count > 0)
                {
                    layerResult.Errors.ForEach(x => ModelState.AddModelError("", x));

                    List<Hesap> hesaplar = new List<Hesap>();

                    foreach (Hesap hes in hesapManager.List())
                    {
                        if (hes.MusteriNo == CurrentSession.User.MusteriNo && hes.Durum == true && hes.HesapNo != tran.GonderenHesapNo)
                        {
                            hesaplar.Add(hes);
                        }
                    }
                    TempData["hesaplar"] = new SelectList(hesaplar, "HesapNo", "HesapNo");
                    return View(tran);
                }
                return RedirectToAction("Index");
                
            }

            return View(tran);
        }

        public ActionResult Havale(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Hesap hesap = hesapManager.Find(x => x.HesapID == id.Value);

            if (hesap == null)
            {
                return HttpNotFound();
            }

            Transfer tran = new Transfer();
            tran.GonderenHesapNo = hesap.HesapNo;
            

            return View(tran);
        }
        
        [HttpPost]
        public ActionResult Havale(Transfer tran)
        {
            BusinessLayerResult<Transfer> layerResult = new BusinessLayerResult<Transfer>();

            if (ModelState.IsValid)
            {
                Hesap gonderen = hesapManager.Find(x => x.HesapNo == tran.GonderenHesapNo);
                long aliciHesapNo = Convert.ToInt64(tran.AliciHesapNo);

                Hesap alici = hesapManager.Find(x => x.HesapNo == aliciHesapNo);

                if (alici == null)
                {
                    layerResult.Errors.Add("Girilen alıcı hesap numarası yanlış veya eksik");
                }
                else if(aliciHesapNo == tran.GonderenHesapNo)
                {
                    layerResult.Errors.Add("Gönderen ve alıcı hesap numaraları aynı olamaz. Lütfen bilgileri tekrar giriniz!");
                }
                else if(aliciHesapNo == gonderen.HesapNo)
                {
                    layerResult.Errors.Add("Alıcı ve gönderen hesap numarası aynı olamaz");
                }
                else
                {
                    if (Decimal.Parse(tran.Tutar) == 0)
                    {
                        layerResult.Errors.Add("Lütfen 0'dan başka bir tutar giriniz");
                    }
                    else
                    {

                        tran.Zaman = DateTime.Now;
                        tran.AliciHesapNo = alici.HesapNo.ToString();

                        if (gonderen.MusteriNo == alici.MusteriNo)
                        {
                            tran.TransferTuru = true; //Virman
                        }
                        else
                        {
                            tran.TransferTuru = false; //Havale
                        }

                        decimal para = Decimal.Parse(tran.Tutar);

                        if (alici.Durum == true && gonderen.Bakiye >= para)
                        {
                            gonderen.Bakiye = (gonderen.Bakiye - para);
                            alici.Bakiye = (alici.Bakiye + para);
                            hesapManager.Update(gonderen);
                            hesapManager.Update(alici);
                            transferManager.Insert(tran);
                        }
                        else if (gonderen.Bakiye < para)
                        {
                            layerResult.Errors.Add("Girilen tutar, hesap bakiyesinden yüksek. Lütfen uygun bir tutar giriniz!");
                        }
                        else if (alici.Durum != true)
                        {
                            layerResult.Errors.Add("Böyle bir hesap bulunmamaktadır!");
                        }
                    }
                }
                
                if (layerResult.Errors.Count > 0)
                {
                    layerResult.Errors.ForEach(x => ModelState.AddModelError("", x));
                    return View(tran);
                }
                return RedirectToAction("Index");
            }
            return View(tran);
        }
    }
}
