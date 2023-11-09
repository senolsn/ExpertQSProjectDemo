using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Utilities.Entities.Abstract;

namespace Repository.Abstract
{
    public interface IProductRepository
    {
        void Add(Product product);

        List<Product> GetAll(Expression<Func<Product, bool>> filter = null);

    }
}
