using System.Linq;
using SeeMoreApp.Domain.Entities;
 
namespace SeeMoreApp.Domain.Abstract { 
    public interface IProductRepository { 
 
        IQueryable<Product> Products { get; }

        void SaveProduct(Product product);

        void DeleteProduct(Product product);
    } 
}