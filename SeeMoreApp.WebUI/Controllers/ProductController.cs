using System.Linq;
using System.Web.Mvc;
using SeeMoreApp.Domain.Abstract;
using SeeMoreApp.WebUI.Models;

namespace SeeMoreApp.WebUI.Controllers
{

    /*You can see that we’ve added a constructor that takes an IProductRepository parameter. This will 
allow Ninject to inject the dependency for the product repository when it instantiates the controller 
class. Next, we are going to add an action method, called List, which will render a view showing the 
complete list of products*/
    

    public class ProductController : Controller
    {
        public int PageSize = 4;
        private IProductRepository repository;

        public ProductController(IProductRepository productRepository)
        {
            repository = productRepository;
        }

        public ViewResult List(string category, int page = 1)
        {

            ProductsListViewModel viewModel = new ProductsListViewModel
            {
                Products = repository.Products
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(p => p.ProductID)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    //TotalItems = repository.Products.Count()
                    TotalItems = category == null ?
                        repository.Products.Count() :
                        repository.Products.Where(e => e.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(viewModel);
        }
    }
}