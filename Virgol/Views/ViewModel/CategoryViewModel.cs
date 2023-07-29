using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Virgol_Model;

namespace Virgol.Views.ViewModel
{
    public class CategoryViewModel
    {
        public int CtegoryId { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public IEnumerable<Article> Articles { get; set; }
    }
}