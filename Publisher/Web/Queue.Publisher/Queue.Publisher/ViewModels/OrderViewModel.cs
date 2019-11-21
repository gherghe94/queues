using System.Collections.Generic;

namespace Queue.Publisher.ViewModels
{
    public class OrderViewModel
    {
        public string UserName { get; set; }

        public List<ProductViewModel> Products { get; set; }
    }
}
