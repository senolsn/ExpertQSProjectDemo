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
    public class ProductRepositoryDal : IProductRepository
    {
        public void Add(Product product)
        {
            using (var context = new RepositoryContext())
            {
                var addedProduct = context.Entry(product);
                addedProduct.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            using (var context = new RepositoryContext())
            {
                return
                       filter == null
                       ? context.Set<Product>().ToList()
                       : context.Set<Product>().Where(filter).ToList();
            }
        }
    }
}
