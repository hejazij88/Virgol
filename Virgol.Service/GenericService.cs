using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Virgol.Repository;
using Virgol_Model;
using Virgol_Model.Context;

namespace Virgol.Service
{
    public class GenericService<T> : GenericRepository<T> where T : BaseEntity
    {
        public GenericService(VirgolContext context) : base(context)
        {
        }
    }
}
