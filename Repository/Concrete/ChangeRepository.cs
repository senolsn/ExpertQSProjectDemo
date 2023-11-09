using Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Utilities.Entities.Abstract;

namespace Repository.Concrete
{
    public class ChangeRepository : IChangeRepository
    {
        public List<ChangeCode> GetAllChangeCode(Expression<Func<ChangeCode, bool>> filter = null)
        {
            using (var context = new RepositoryContext())
            {
                return
                       filter == null
                       ? context.Set<ChangeCode>().ToList()
                       : context.Set<ChangeCode>().Where(filter).ToList();
            }
        }

        public ChangeCode Get(Expression<Func<ChangeCode, bool>> filter)
        {
            using (var context = new RepositoryContext())
            {
                return context.Set<ChangeCode>().SingleOrDefault(filter);
            }
        }

        public void Save(Change change)
        {
            using (var context = new RepositoryContext())
            {
                var addedChange = context.Entry(change);
                addedChange.State = EntityState.Added;
                context.SaveChanges();
            }
        }
    }
}
