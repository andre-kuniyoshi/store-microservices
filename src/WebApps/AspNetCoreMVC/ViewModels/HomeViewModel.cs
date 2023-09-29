using AspNetCoreMVC.Models;

namespace AspNetCoreMVC.ViewModels
{
    public class HomeViewModel
    {
        public int CartItemsCount { get; set; } = 0;
        public IEnumerable<ProductModel> Products { get; set; }
    }
}
