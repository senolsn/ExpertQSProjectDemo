using Entity.DTOs;
using Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Entities.Concrete;
using Utilities.Security.Hashing;

namespace Service.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;

        public AuthManager(IUserService userService)
        {
            _userService = userService;
        }

        /*UserLoginDto olarak gelen cevabı alıyorum öncelikle girilen mail'e ait kullanıcı var mı onu kontrol ediyorum. (deneme123@deneme.com) 
        Eğer girilen maile ait kullanıcı var ise hashlemek için yazdığım tool'u kullanıyorum dtodan gelen parolasını (123) hashleyerek veritabanındaki 
        tutulan hashli parolasıyla eşit mi kontrol ediyorum eğer doğru ise true dönüyor. en sonunda ise user'i geri döndürüyorum.
            */
        public User Login(UserLoginDto userLoginDto)
        {
            var userToCheck = _userService.GetByMail(userLoginDto.Email);
            if (userToCheck == null)
            {
                throw new Exception("User Not Found!");
            }

            if (!HashingHelper.VerifyPasswordHash(userLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                throw new Exception("Password Invalid!");
            }

            return userToCheck;

        }
        /*
         Girilen Dto'nun bilgilerini alıyorum. Parolayı hash ve salting yapıyorum sonra User nesnesi oluşturup db'ye öyle kaydediyorum.
         */
        public User Register(UserRegisterDto userRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;

            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var user = new User
            {
                Email = userRegisterDto.Email,
                FirstName = userRegisterDto.FirstName,
                LastName = userRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };
            _userService.Add(user);
            return user;
        }

        public bool UserExists(string email)
        {
            if (_userService.GetByMail(email) != null)
            {
                return false;
            }
            return true;
        }
    }
}
