using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Utilities.Entities.Abstract;

namespace Entity.Concrete
{
    public class Change : IEntity
    {
        public int Id { get; set; }
        public int CodeId { get; set; }
        public decimal Buy { get; set; }
        public decimal Sell { get; set; }
    }
}
