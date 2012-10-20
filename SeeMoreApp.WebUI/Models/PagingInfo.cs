using System;

namespace SeeMoreApp.WebUI.Models
{

     
//A view model isn’t part of our domain model. It is just a convenient class for passing data between 
//the controller and the view. To emphasize this, we have put this class in the SportsStore.WebUI project to 
//keep it separate from the domain model classes. 

    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }

        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }
    }
}