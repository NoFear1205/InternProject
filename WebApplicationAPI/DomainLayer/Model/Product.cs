using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Model
{
    public class Product
    {
        public int Id { get;private set; }
        [Display(Name ="Tên sản phẩm")]
        public string? Name { get; private set; }
        /*[Required(ErrorMessage = "Tên nhà cung cấp không được để trống")]
        [StringLength(100)]*/
        [Display(Name = "Tên nhà cung cấp")]
        public string? Provider { get; private set; }       
        public int CategoryID { get; private set; }
        [Display(Name = "Loại sản phẩm")]
        public Category? Category { get; private set; }
        public Product() { }
        public void setName (string Name)
        {
            this.Name = Name;
        }
        public void setProvider(string Provider)
        {
            this.Provider = Provider;
        }
        public void setId(int Id)
        {
            this.Id = Id;
        }
        public void setCategoryId(int CategoryId)
        {
            CategoryID = CategoryId;
        }
        public void setCategoryId(List<Category> Category)
        {
            Category = Category;
        }


    }
}
