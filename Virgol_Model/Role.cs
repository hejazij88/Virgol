using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Virgol_Model
{
    public class Role:BaseEntity
    {
        public int RoleID { get; set; }
        public string RoleTitle { get; set; }
        public IEnumerable<User> Users { get; set;}

    }
}
