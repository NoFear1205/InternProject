using DomainLayer.ViewModel.CategoryView;
using DomainLayer.ViewModel.ProductMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Model
{
    public class CategoryAdd
    {
        [Required]
        [MaxLength(100)]    
        public string Name { get; set; }
        public List<ProductAdd>? Products { get; set; }
    }
}
