using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Virgol_Model;
using Virgol_Model.Context;

namespace Virgol.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(VirgolContext context) : base(context)
        {
        }
    }
}
