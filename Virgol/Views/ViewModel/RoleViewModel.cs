using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Virgol_Model;

namespace Virgol.Views.ViewModel
{
    public class RoleViewModel
    {
        public int RoleID { get; set; }
        public string RoleTitle { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}