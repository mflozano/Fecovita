using Fecovita.Core.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fecovita.Persistence
{
    public class FecovitaContext : DbContext
    {
        public FecovitaContext() : base("Data Source=localhost;Initial Catalog=FecovitaDB; Integrated Security=true; User Id=sa;password=123456;MultipleActiveResultSets=true")
        {
            //DB Initializer
            //Database.SetInitializer(new DBInitializer());            
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;                    
        }


        
        public DbSet<Product> Product { get; set; }
       

        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();          
        }
    }   
}
