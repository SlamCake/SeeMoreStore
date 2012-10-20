using System.Collections.Generic;
using SeeMoreApp.Domain.Entities;
using SeeMoreApp.WebUI.Models;

namespace SeeMoreApp.WebUI.Models
{
    public class ProductsListViewModel
    {

        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}