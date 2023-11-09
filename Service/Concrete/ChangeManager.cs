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
    public class ChangeManager : IChangeService
    {
        private readonly IChangeRepository _changeRepository;

        public ChangeManager(IChangeRepository changeRepository)
        {
            _changeRepository = changeRepository;
        }

        public List<ChangeCode> GetAllChangeCode()
        {
            return _changeRepository.GetAllChangeCode();
        }

        public void Save(Change change)
        {
            _changeRepository.Save(change);
        }

        public ChangeCode GetChangeCodeById(int id)
        {
            return _changeRepository.Get(c => c.Id == id);
        }
    }
}
