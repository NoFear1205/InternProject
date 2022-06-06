using DomainLayer.ViewModel.CategoryView;
using DomainLayer.ViewModel.ProductMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Model
{
    public class CategoryAdd
    {
        public string name { get; set; }
        public List<ProductAdd>? Products { get; set; }
    }
}
