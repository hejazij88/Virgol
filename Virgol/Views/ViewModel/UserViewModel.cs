using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Virgol_Model;

namespace Virgol.Views.ViewModel
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        [Required(ErrorMessage ="{0} is Required")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "{0} is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
        public DateTime RegesterDate { get; set; }
        [Required(ErrorMessage = "{0} is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "{0} is Required")]
        public string Family { get; set; }

        public Role Role { get; set; }
        public IEnumerable<Article> Articles { get; set; }
    }
}