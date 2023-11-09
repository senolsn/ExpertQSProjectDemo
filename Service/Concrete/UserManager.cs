using Repository.Abstract;
using Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Entities.Concrete;

namespace Service.Concrete
{
    public class UserManager : IUserService
    {

        IUserRepository _userDal;

        public UserManager(IUserRepository userDal)
        {
            _userDal = userDal;
        }

        public void Add(User user)
        {
            _userDal.Add(user);
        }

        public User GetByMail(string email)
        {
            return _userDal.Get(u => u.Email == email);
        }
    }
}
