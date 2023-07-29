using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Virgol_Model;

namespace Virgol.Views.ViewModel
{
    public class ArticleViewModel
    {
        public int ArticleId { get; set; }

        public int CtegoryId { get; set; }

        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Content { get; set; }
        public string ImageName { get; set; }
        public DateTime DateRegester { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
        public int Like { get; set; }
        public int ComentId { get; set; }

        public User User { get; set; }
        public Ctegory Ctegory { get; set; }
    }
}