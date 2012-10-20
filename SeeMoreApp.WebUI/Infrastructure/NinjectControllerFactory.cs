using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SeeMoreApp.Domain.Entities;
using SeeMoreApp.Domain.Concrete;
using SeeMoreApp.Domain.Abstract;
using Moq;
using System.Configuration;
using SeeMoreApp.WebUI.Infrastructure.Abstract;
using SeeMoreApp.WebUI.Infrastructure.Concrete;

namespace SeeMoreApp.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory() 
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null
                ? null
                : (IController)ninjectKernel.Get(controllerType);
        }
        private void AddBindings() { 
            // Mock implementation of the IProductRepository Interface 
    //Mock<IProductRepository> mock = new Mock<IProductRepository>(); 
    //mock.Setup(m => m.Products).Returns(new List<Product> { 
    //    new Product { Name = "Football", Price = 25 }, 
    //    new Product { Name = "Surf board", Price = 179 }, 
    //    new Product { Name = "Running shoes", Price = 95 } }.AsQueryable());
    ninjectKernel.Bind<IProductRepository>().To<EFProductRepository>();

    EmailSettings emailSettings = new EmailSettings
    {

//         we specified a value for 
//only one of the EmailSettings properties: WriteAsFile. We read the value of this property using the 
//ConfigurationManager.AppSettings property, which allows us to access application settings we’ve 
//placed in the Web.config file (the one in the root project folder)

        WriteAsFile
            = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
    };

    ninjectKernel.Bind<IOrderProcessor>()
        .To<EmailOrderProcessor>().WithConstructorArgument("settings", emailSettings);

    ninjectKernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
        }
    }
}