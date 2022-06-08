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
        [Display(Name = "Tên nhà cung cấp")]
        public string? Provider { get; private set; }       
        public int CategoryID { get; private set; }
        [Display(Name = "Loại sản phẩm")]
        public Category? Category { get; private set; }
        public Product() { }
        public void SetName (string Name)
        {
            this.Name = Name;
        }
        public void SetProvider(string Provider)
        {
            this.Provider = Provider;
        }
        public void SetId(int Id)
        {
            this.Id = Id;
        }
        public void SetCategoryId(int CategoryId)
        {
            CategoryID = CategoryId;
        }
        public void SetCategoryId(Category Category)
        {
            this.Category = Category;
        }
    }
}
