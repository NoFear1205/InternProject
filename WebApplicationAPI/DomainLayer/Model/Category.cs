using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Model
{
    public class Category
    {
        public int ID { get; private set; }
        public string? Name { get; private set; }
        public List<Product>? Products { get; private set; }
        public Category() { }
        public void SetName(string Name)
        {
            this.Name = Name;
        }
        public void SetProduct(List<Product>? Products)
        {
            this.Products = Products;
        }

    }
}
