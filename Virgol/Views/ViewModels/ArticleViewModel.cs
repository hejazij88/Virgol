using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Virgol_Model;

namespace Virgol.Areas.Admin.Views.ViewModels
{
    public class ArticleViewModel
    {
        [Required]
        [Key]
        public int ArticleId { get; set; }

        [Required]
        [DisplayName("Subject")]
        public int CtegoryId { get; set; }
        [Required]

        public string Title { get; set; }
        [Required]

        public string Content { get; set; }
        public string ImageName { get; set; }
        [Required]

        public DateTime DateRegester { get; set; }
        [Required]

        public bool IsActive { get; set; }
        [Required]

        public int UserId { get; set; }
        public int Like { get; set; }
        public int ComentId { get; set; }

        public User User { get; set; }
        public Ctegory Ctegory { get; set; }
        public IEnumerable<Coment> Coments { get; set; }
    }
}