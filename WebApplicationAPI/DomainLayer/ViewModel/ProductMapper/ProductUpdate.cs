using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.ViewModel.ProductMapper
{
    public class ProductUpdate
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        /*[Required(ErrorMessage = "Tên nhà cung cấp không được để trống")]
        [StringLength(100)]*/
        [Required]
        public string? Provider { get; set; }
        [Required]
        public int CategoryID { get; set; }
    }
}
