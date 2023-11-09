using Entity.Concrete;
using ExpertQSProject.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Abstract;
using System.Diagnostics;
using System.Xml;

namespace ExpertQSProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly IChangeService _changeService;

        public HomeController(ILogger<HomeController> logger, IProductService productService,IChangeService changeService)
        {
            _logger = logger;
            _productService = productService;
            _changeService = changeService;
        }

        public IActionResult Index()
        {
            // Kullanıcı oturum açmamışsa login sayfasında kalıyor eğer başarılı oturum açılmışsa home'a yönlendirilir.
            if (HttpContext.Session.GetString("UserSession") == null)
            {
                // Kullanıcı oturum açmamış, başka bir sayfaya yönlendir veya hata mesajı göster
                return RedirectToAction("Login", "Auth"); // Giriş sayfasına yönlendirildi.
            }

            var products = _productService.GetAll();
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

    
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Change()
        {

            if (HttpContext.Session.GetString("UserSession") == null)
            {
                // Kullanıcı oturum açmamış, başka bir sayfaya yönlendir veya hata mesajı göster
                return RedirectToAction("Login", "Auth"); // Giriş sayfasına yönlendirildi.
            }

            string link = "https://www.tcmb.gov.tr/kurlar/today.xml";
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(link);

            // Dolar'ın Alış Fiyatı ve Satış Fiyatı değerleri
            string USDBuying = xmlDoc.SelectSingleNode("/Tarih_Date/Currency[@Kod='USD']/BanknoteBuying").InnerXml;
            string USDSelling = xmlDoc.SelectSingleNode("/Tarih_Date/Currency[@Kod='USD']/BanknoteSelling").InnerXml;

            // Sterlin'in Alış Fiyatı ve Satış Fiyatı değerleri
            string GBPBuying = xmlDoc.SelectSingleNode("/Tarih_Date/Currency[@Kod='GBP']/BanknoteBuying").InnerXml;
            string GBPSelling = xmlDoc.SelectSingleNode("/Tarih_Date/Currency[@Kod='GBP']/BanknoteSelling").InnerXml;

            // Euro'nun Alış Fiyatı ve Satış Fiyatı değerleri
            string EURBuying = xmlDoc.SelectSingleNode("/Tarih_Date/Currency[@Kod='EUR']/BanknoteBuying").InnerXml;
            string EURSelling = xmlDoc.SelectSingleNode("/Tarih_Date/Currency[@Kod='EUR']/BanknoteSelling").InnerXml;

            var changeCodes = _changeService.GetAllChangeCode();

            ViewBag.ChangeCodes = changeCodes;

            ViewBag.USDBuying = USDBuying;
            ViewBag.USDSelling = USDSelling;
            ViewBag.GBPBuying = GBPBuying;
            ViewBag.GBPSelling = GBPSelling;
            ViewBag.EURBuying = EURBuying;
            ViewBag.EURSelling = EURSelling;

            return View();
        }

        [HttpPost]
        public IActionResult SaveChange(int currencySelect)
        {
            if (HttpContext.Session.GetString("UserSession") == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            //Select inputundan seçilen ChangeCode'ı(EUR,USD,GBP) alıyorum.
            var selectedChangeCode = _changeService.GetChangeCodeById(currencySelect);

            if (selectedChangeCode == null)
            {
                // Seçilen ChangeCode mevcut değilse, hata mesajı göster veya başka bir işlem yapın
                TempData["ErrorMessage"] = "Geçersiz döviz kodu seçildi.";
                return RedirectToAction("Change");
            }

            string link = "https://www.tcmb.gov.tr/kurlar/today.xml";
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(link);

            decimal buyingRate;
            decimal sellingRate;

            // Seçilen ChangeCode'a göre alış ve satış fiyatlarını çekin
            switch (selectedChangeCode.Code)
            {
                case "USD":
                case "EUR":
                case "GBP":
                    string currencyCode = selectedChangeCode.Code;
                    string buyingRateStr = xmlDoc.SelectSingleNode($"/Tarih_Date/Currency[@Kod='{currencyCode}']/BanknoteBuying").InnerXml;
                    string sellingRateStr = xmlDoc.SelectSingleNode($"/Tarih_Date/Currency[@Kod='{currencyCode}']/BanknoteSelling").InnerXml;

                    // Eğer nokta ile ayrılmışsa, noktayı virgüle dönüştür
                    if (buyingRateStr.Contains("."))
                    {
                        buyingRateStr = buyingRateStr.Replace(".", ",");
                    }
                    if (sellingRateStr.Contains("."))
                    {
                        sellingRateStr = sellingRateStr.Replace(".", ",");
                    }

                    buyingRate = decimal.Parse(buyingRateStr);
                    sellingRate = decimal.Parse(sellingRateStr);
                    break;

                default:
                    TempData["ErrorMessage"] = "Geçersiz döviz kodu seçildi.";
                    return RedirectToAction("Change");
            }

            // Change nesnesini oluşturup ve dbye kaydediyorum.
            var change = new Change
            {
                CodeId = currencySelect,
                Buy = buyingRate,
                Sell = sellingRate
            };

            _changeService.Save(change);

            TempData["SuccessMessage"] = "Değişiklikler kaydedildi.";

            return RedirectToAction("Change");
        }

        public IActionResult Cart()
        {
            if (HttpContext.Session.GetString("UserSession") == null)
            {
                // Kullanıcı oturum açmamış, başka bir sayfaya yönlendir
                return RedirectToAction("Login", "Auth"); // Giriş sayfasına yönlendirildi.
            }

            return View();
        }


    }
}