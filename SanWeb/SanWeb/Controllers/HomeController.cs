using SanWeb.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace SanWeb.Controllers
{
    public class HomeController : Controller
    {
        SANEntities db = new SANEntities();
        public ActionResult Index()
        {
            return View(db.MoneyCurrencies.ToList());
        }

        [HttpPost]
        public JsonResult SaveAndUpdateCurrency(int itemID, string idName, string idCode, string idBuying, string idSelling, string idReception, string idSales)
        {
            var result = new jsonMessage();
            try
            {
                MoneyCurrency currency = new MoneyCurrency();
                currency.Id = itemID;
                currency.Name = idName;
                currency.Code = idCode;
                currency.Exchange_Buying = idBuying;
                currency.Exchange_Selling = idSelling;
                currency.Effective_Reception = idReception;
                currency.Effective_Sales = idSales;
                currency.Date = DateTime.Now;
                db.Entry(currency).State = EntityState.Modified;
                result.Message = "Başarıyla Güncelleştirildi...";
                result.Status = true;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = "İşlem Başarısız Tekrar Deneyiniz";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}