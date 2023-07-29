using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Virgol_Model;
using Virgol_Model.Context;

namespace Virgol.Service
{
    public class RoleServices : GenericService<Role>, IRoleService
    {
        public RoleServices(VirgolContext context) : base(context)
        {
        }
    }
}
