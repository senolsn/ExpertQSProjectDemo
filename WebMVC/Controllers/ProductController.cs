using Entity.Concrete;
using ExpertQSProject.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Abstract;

namespace ExpertQSProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

 
        public IActionResult Create()
        {

            if (HttpContext.Session.GetString("UserSession") == null)
            {
                // Kullanıcı oturum açmamış, başka bir sayfaya yönlendir veya hata mesajı göster
                return RedirectToAction("Login", "Auth"); // Giriş sayfasına yönlendirildi.
            }

            return View();
        }

        //Product ekleme sayfası
        
        [HttpPost]
        public IActionResult Create(ProductViewModel productViewModel)
        {
     
            if (productViewModel == null)
            {
                return BadRequest("Ürün bilgileri eksik veya hatalı.");
            }

            
            var product = new Product
            {
                Name = productViewModel.Name,
                Description = productViewModel.Description,
                Price = productViewModel.Price
            };

            _productService.Add(product);

            return RedirectToAction("Index","Home"); 
        }
    }
}
