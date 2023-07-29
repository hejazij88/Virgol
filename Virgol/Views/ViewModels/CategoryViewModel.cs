using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Virgol_Model;

namespace Virgol.Areas.Admin.Views.ViewModels
{
    public class CategoryViewModel
    {
        [Key]
        [Required]
        public int CtegoryId { get; set; }

        [Required]
        [MaxLength(70,ErrorMessage = "Max Lengh 70 character")]
        public string Title { get; set; }

        [DisplayName("Image")]
        public string ImageName { get; set; }
        public IEnumerable<Article> Articles { get; set; }
    }
}