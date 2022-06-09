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
        public void SetName (string name)
        {
            this.Name = name;
        }
        public void SetProvider(string provider)
        {
            this.Provider = provider;
        }
        public void SetId(int id)
        {
            this.Id = id;
        }
        public void SetCategoryId(int categoryId)
        {
            CategoryID = categoryId;
        }
        public void SetCategoryId(Category category)
        {
            this.Category = category;
        }
    }
}
