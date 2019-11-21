using System.Collections.Generic;
using System.Linq;

namespace Queues.Desktop.ViewModels
{
    public class OrderViewModel
    {
        public string UserName { get; set; }

        public List<ProductViewModel> Products { get; set; }

        public override string ToString()
        {
            return $"Total: {GetTotal()}, No: {GetNumberOfProducts()}";
        }

        private int GetNumberOfProducts()
        {
            return Products.Count;
        }

        private decimal GetTotal()
        {
            return Products.Sum(p => p.Price);
        }
    }
}
