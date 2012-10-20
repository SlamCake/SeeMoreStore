using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeeMoreApp.Domain.Entities;

namespace SeeMoreApp.WebUI.Binders
{
    public class CartModelBinder : IModelBinder
    {


//        The ControllerContext provides access to all of the 
//information that the controller class has, which includes details of the request from the client. The 
//ModelBindingContext gives you information about the model object you are being asked to build and 
//tools for making it easier.
//For our purposes, the ControllerContext class is the one we’re interested in. It has the HttpContext 
//property, which in turn has a Session property that lets us get and set session data. We obtain the Cart 
//by reading a key value from the session data, and create a Cart if there isn’t one there already.

        private const string sessionKey = "Cart";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
            Cart cart = (Cart)controllerContext.HttpContext.Session[sessionKey];
            if (cart == null) { 
                cart = new Cart(); 
                controllerContext.HttpContext.Session[sessionKey] = cart; 
            }
            return cart;
        }
    }
}