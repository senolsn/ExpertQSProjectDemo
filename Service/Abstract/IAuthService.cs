using Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Entities.Concrete;

namespace Service.Abstract
{
    public interface IAuthService
    {
        User Register(UserRegisterDto userRegisterDto, string password);
        User Login(UserLoginDto userLoginDto);
        bool UserExists(string email);
    }
}
