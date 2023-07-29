using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Virgol_Model
{
    public class User:BaseEntity
    {
        public int UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
        public DateTime RegesterDate{ get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
 
        public Role Role { get; set; }
        public IEnumerable<Article> Articles { get; set;}

    }
}
