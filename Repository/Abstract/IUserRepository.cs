using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Utilities.Entities.Concrete;

namespace Repository.Abstract
{
    public interface IUserRepository
    {
        void Add(User user);

        User Get(Expression<Func<User, bool>> filter);
    }
}
