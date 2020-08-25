using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SQL_Entity_Framework.Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base("conn") { }
        public DbSet<Products> Products { get; set; }
    }
}