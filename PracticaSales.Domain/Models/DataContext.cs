using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaSales.Domain.Models
{
    public class DataContext : DbContext 
    {
        public DataContext() : base("DefaultConnection")
        {

        }

        public System.Data.Entity.DbSet<PracticaSales.Common.Models.Product> Products { get; set; }
    }
}
