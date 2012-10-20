using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using SeeMoreApp.Domain.Entities;

namespace SeeMoreApp.Domain.Concrete
{


//    To take advantage of the code-first feature, we need to create a class that is derived from 
//System.Data.Entity.DbContext. This class then defines a property for each table that we want to work 
//with. The name of the property specifies the table, and the type parameter of the DbSet result specifies 
//the model that the Entity Framework should use to represent rows in that table. In our case, the property 
//name is Products and the type parameter is Product. We want the Product model type to be used to 
//represent rows in the Products table

    class EFDbContext : DbContext 
    {
        public DbSet<Product> Products { get; set; }
    }
}
