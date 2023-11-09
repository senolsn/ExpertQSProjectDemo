using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstract
{
    public interface IChangeService
    {
        void Save(Change change);
        List<ChangeCode> GetAllChangeCode();
        ChangeCode GetChangeCodeById(int id);
    }
}
