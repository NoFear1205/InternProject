using AutoMapper;
using DomainLayer.Model;
using DomainLayer.ViewModel.CategoryView;
using DomainLayer.ViewModel.ProductMapper;

namespace WebApplicationAPI.Mapper
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<Product, ProductView>();
            CreateMap<ProductAdd, Product>();
            CreateMap<ProductUpdate, Product>();
        }
    }
}
