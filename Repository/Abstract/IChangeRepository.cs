using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Abstract
{
    public interface IChangeRepository
    {
        void Save(Change change);

        List<ChangeCode> GetAllChangeCode(Expression<Func<ChangeCode, bool>> filter = null);
        ChangeCode Get(Expression<Func<ChangeCode, bool>> filter);

    }
}
