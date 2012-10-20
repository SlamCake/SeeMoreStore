using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeeMoreApp.Domain.Abstract;
using SeeMoreApp.Domain.Entities;
using SeeMoreApp.WebUI.Models;

namespace SeeMoreApp.WebUI.Controllers
{
    public class CartController : Controller
    {

//        For the AddToCart and RemoveFromCart methods, we have used parameter names that match the 
//input elements in the HTML forms we created in the ProductSummary.cshtml view. This allows the MVC 
//Framework to associate incoming form POST variables with those parameters, meaning we don’t need to 
//process the form ourselves.
        

//        The final point to note about the Cart controller is that both the AddToCart and RemoveFromCart methods 
//call the RedirectToAction method. This has the effect of sending an HTTP redirect instruction to the 
//client browser, asking the browser to request a new URL. In this case, we have asked the browser to 
//request a URL that will call the Index action method of the Cart controller. 

//        We have removed the GetCart method and added a Cart parameter to each of the action methods.  
//When the MVC Framework receives a request that requires, say, the  AddToCart method to be 
//invoked, it begins by looking at the parameters for the action method. It looks at the list of binders 
//available and tries to find one that can create instances of each parameter type. Our custom binder is 
//asked to create a Cart object, and it does so by working with the session state feature. Between our 
//binder and the default binder, the MVC Framework is able to create the set of parameters required to call 
//the action method. And so it does, allowing us to refactor the controller so that it has no view as to how 
//Cart objects are created when requests are received. 

//        There are a few benefits to using a custom model binder like this. The first is that we have separated 
//the logic used to create a Cart from that of the controller, which allows us to change the way we store 
//Cart objects without needing to change the controller. The second benefit is that any controller class 
//that works with Cart objects can simply declare them as action method parameters and take advantage 
//of the custom model binder. The third benefit, and the one we think is most important, is that we can 
//now unit test the Cart controller without needing to mock a lot of ASP.NET plumbing.

        private IProductRepository repository;
        private IOrderProcessor orderProcessor;

        public CartController(IProductRepository repo, IOrderProcessor proc) { repository = repo; orderProcessor = proc; }

//        You can see that the Checkout action method we’ve added is decorated with the HttpPost attribute, 
//which means that it will be invoked for a POST request—in this case, when the user submits the form. 
//Once again, we are relying on the model binder system, both for the ShippingDetails parameter (which 
//is created automatically using the HTTP form data) and the Cart parameter (which is created using our 
//custom binder).
        
        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }
        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl) {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if (product != null) {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl) {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if (product != null) {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public ViewResult Summary(Cart cart)
        {
            return View(cart);
        }
        public ViewResult Checkout() {
            return View(new ShippingDetails());
        }

        public ViewResult Index(Cart cart, string returnUrl) { 
            return View (new CartIndexViewModel { Cart = cart, ReturnUrl = returnUrl} );
        }

        //private Cart GetCart() {
        //    Cart cart = (Cart)Session["Cart"];
        //    if (cart == null) {
        //        cart = new Cart();
        //        Session["Cart"] = cart;
        //    }
        //    return cart;
        //}

    }
}
