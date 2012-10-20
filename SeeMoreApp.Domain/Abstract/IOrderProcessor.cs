//In 
//keeping with the principles of the MVC model, we are going to define an interface for this functionality, 
//write an implementation of the interface, and then associate the two using our DI container, Ninject. 


using SeeMoreApp.Domain.Entities;

namespace SeeMoreApp.Domain.Abstract
{

    public interface IOrderProcessor
    {

        void ProcessOrder(Cart cart, ShippingDetails shippingDetails);
    }
}