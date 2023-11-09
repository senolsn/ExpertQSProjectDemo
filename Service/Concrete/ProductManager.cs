using Entity.Concrete;
using Repository.Abstract;
using Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductRepository _repository;

        public ProductManager(IProductRepository repository)
        {
            _repository = repository;
        }

        public void Add(Product product)
        {
            _repository.Add(product);   
        }

        public List<Product> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
