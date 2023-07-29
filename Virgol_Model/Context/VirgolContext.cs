using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Virgol_Model.Context
{
    public class VirgolContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Ctegory> Ctegories { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Role> Roles { get; set; }
		public DbSet<Slider> Sliders { get; set; }




	}
}
