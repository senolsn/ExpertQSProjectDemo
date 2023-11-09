using Microsoft.EntityFrameworkCore;
using Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Utilities.Entities.Abstract;
using Utilities.Entities.Concrete;

namespace Repository.Concrete
{
    public class UserRepositoryDal : IUserRepository
    {
        public void Add(User user)
        {
            using (var context = new RepositoryContext())
            {
                var addedUser = context.Entry(user);
                addedUser.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public User Get(Expression<Func<User, bool>> filter)
        {
            using (var context = new RepositoryContext())
            {
                return context.Set<User>().SingleOrDefault(filter);
            }
        }
    }
}
