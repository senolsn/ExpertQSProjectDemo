using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Utilities.Entities.Abstract;

namespace Entity.Concrete
{
    public class ChangeCode : IEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
    }
}
