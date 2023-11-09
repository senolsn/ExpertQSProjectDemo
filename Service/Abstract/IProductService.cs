using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstract
{
    public interface IProductService
    {
        void Add(Product product);

        List<Product> GetAll();
    }
}
