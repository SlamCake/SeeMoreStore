using System.Web.Mvc;
using SeeMoreApp.Domain.Abstract;
using SeeMoreApp.Domain.Entities;
using System.Linq;

namespace SeeMoreApp.WebUI.Controllers
{
    [Authorize] 
    public class AdminController : Controller
    {
        private IProductRepository repository;

        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }
        public ViewResult Index()
        {
            return View(repository.Products);
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);

//                After we have saved the changes in the repository, we store a message using the Temp Data feature. 
//This is a key/value dictionary, similar to the session data and View Bag features we have used previously. 
//The key difference is that TempData is deleted at the end of the HTTP request

//                Notice that we return the ActionResult type from the Edit method. We’ve been using the 
//ViewResult type until now. ViewResult is derived from ActionResult, and it is used when you want the 
//framework to render a view. However, other types of ActionResults are available, and one of them is 
//returned by the RedirectToAction method.

//                We can’t use ViewBag in this situation because the user is being redirected. ViewBag passes data 
//between the controller and view, and it can’t hold data for longer than the current HTTP request. We 
//could have used the session data feature, but then the message would be persistent until we explicitly 
//removed it, which we would rather not have to do. So, the Temp Data feature is the perfect fit. The data 
//is restricted to a single user’s session (so that users don’t see each other’s TempData) and will persist until 
//we have read it. We will read the data in the view rendered by the action method to which we have 
//redirected the user. 

                TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values 
                return View(product);
            }
        }
        public ViewResult Create()
        {
            return View("Edit", new Product());
        }

//        The Create method doesn’t render its default view. Instead, it specifies that the Edit view should be 
//used. It is perfectly acceptable for one action method to use a view that is usually associated with 
//another view. In this case, we inject a new Product object as the view model so that the Edit view is 
//populated with empty fields
//        This leads us to the modification. We would usually expect a form to postback to the action that 
//rendered it, and this is what the Html.BeginForm assumes by default when it generates an HTML form. 
//However, this doesn’t work for our Create method, because we want the form to be posted back to the 
//Edit action so that we can save the newly created product data. To fix this, we can use an overloaded 
//version of the Html.BeginForm helper method to specify that the target of the form generated in the Edit 
//view is the Edit action method of the Admin controller

        [HttpPost]
        public ActionResult Delete(int productId)
        {
            Product prod = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (prod != null)
            {
                repository.DeleteProduct(prod);
                TempData["message"] = string.Format("{0} was deleted", prod.Name);
            }
            return RedirectToAction("Index");
        }
    }
}