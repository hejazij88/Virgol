using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Virgol_Model
{
    public class Article:BaseEntity
    {
        public int ArticleId { get; set; }

        public int CtegoryId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
        public string ImageName { get; set; }
        public DateTime DateRegester { get; set; }
        public bool IsActive { get; set; }
        public int UserId {get; set; }
        public int Like { get; set; }
        public int ComentId {get; set; }

        public User User { get; set; }
        public Ctegory Ctegory { get; set; }
    }
}
