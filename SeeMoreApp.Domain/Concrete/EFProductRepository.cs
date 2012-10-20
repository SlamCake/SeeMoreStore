using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using SeeMoreApp.Domain.Entities;
using SeeMoreApp.Domain.Abstract;
using SeeMoreApp.Domain.Concrete;

namespace SeeMoreApp.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Product> Products {
            get { return context.Products; }
        }
        
        public void SaveProduct(Product product) { 
            if (product.ProductID == 0) { 
                context.Products.Add(product); 
            } 
            context.SaveChanges(); 
        }
        public void DeleteProduct(Product product)
        {
            context.Products.Remove(product);
            context.SaveChanges();
        }
 
    }
}

