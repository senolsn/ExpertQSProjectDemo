using Entity.DTOs;
using Microsoft.AspNetCore.Mvc;
using Service.Abstract;

namespace ExpertQSProject.Controllers
{
    public class AuthController : Controller
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (userToLogin == null)
            {
                ModelState.AddModelError("Hatalı Giriş","Hata");
                return View();
            }

            // Giriş başarılı, başka bir sayfaya yönlendirme yapabilirsiniz.
                HttpContext.Session.SetString("UserSession", userForLoginDto.Email);
                ViewData["UserLoggedIn"] = true; 
                return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserRegisterDto userForRegisterDto)
        {
            var userExists = _authService.UserExists(userForRegisterDto.Email); //User Mevcut mu ?
            if (!userExists)
            {
                ModelState.AddModelError("Hata", "Bu mail zaten kullanılıyor!");
                return View();
            }

            var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);
            if (registerResult != null)
            {
                // Kayıt başarılı, başka bir sayfaya yönlendirme yapabilirsiniz.
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("Hata", "Kayıt başarısız!");
            return View();
        }
    }
}
