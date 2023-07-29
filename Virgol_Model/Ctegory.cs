using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Virgol_Model
{
    public class Ctegory:BaseEntity
    {
        public int CtegoryId { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public IEnumerable<Article> Articles { get; set; }

    }
}
