using DomainLayer.ViewModel.ProductMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.ViewModel.CategoryViewModel
{
    public class CategoryUpdateModel
    {
        public int Id { get; set; } 
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public List<ProductUpdateModel>? Products { get; set; }
    }
}
