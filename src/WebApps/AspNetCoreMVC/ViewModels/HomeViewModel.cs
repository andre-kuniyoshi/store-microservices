using AspNetCoreMVC.Models;

namespace AspNetCoreMVC.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Product> Products { get; set; }
    }
}
