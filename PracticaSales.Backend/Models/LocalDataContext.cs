using PracticaSales.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PracticaSales.Backend.Models
{
    public class LocalDataContext : DataContext
    {
        public System.Data.Entity.DbSet<PracticaSales.Common.Models.Product> Products { get; set; }
    }
}