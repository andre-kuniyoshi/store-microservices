using AspNetCoreMVC.Models;

namespace AspNetCoreMVC.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<ProductModel> Products { get; set; }
    }
}
