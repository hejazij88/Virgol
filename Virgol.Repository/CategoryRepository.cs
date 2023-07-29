using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Virgol_Model;
using Virgol_Model.Context;

namespace Virgol.Repository
{
    public class CategoryRepository : GenericRepository<Ctegory>, ICategoryRepository
    {
        public CategoryRepository(VirgolContext context) : base(context)
        {
        }
    }
}
