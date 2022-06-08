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
            CreateMap<Product, ProductView>()
                 .ForMember(
                res => res.Id,
                opt => opt.MapFrom(src => $"{src.Id }"))
                .ForMember(
                res => res.Name,
                opt => opt.MapFrom(src => $"{src.Name}"))
                .ForMember(
                res => res.Provider,
                opt => opt.MapFrom(src => $"{src.Provider}"))
                .ForMember(
                res => res.CategoryName,
                opt => opt.MapFrom(src => $"{src.Category.Name}"));
            CreateMap<ProductAddModel, Product>()
                .ForMember(
               res => res.Name,
               opt => opt.MapFrom(src => $"{src.Name }"))
                .ForMember(
               res => res.Provider,
               opt => opt.MapFrom(src => $"{src.Provider }"))
                .ForMember(
               res => res.CategoryID,
               opt => opt.MapFrom(src => $"{src.CategoryId }"));
            CreateMap<ProductUpdateModel, Product>()
                .ForMember(
               res => res.Id,
               opt => opt.MapFrom(src => $"{src.Id }"))
                .ForMember(
               res => res.Name,
               opt => opt.MapFrom(src => $"{src.Name }"))
                .ForMember(
               res => res.Provider,
               opt => opt.MapFrom(src => $"{src.Provider }"))
                .ForMember(
               res => res.CategoryID,
               opt => opt.MapFrom(src => $"{src.CategoryID }"));
            CreateMap<Product, ProductUpdateModel>()
                .ForMember(
               res => res.Id,
               opt => opt.MapFrom(src => $"{src.Id }"))
                .ForMember(
               res => res.Name,
               opt => opt.MapFrom(src => $"{src.Name }"))
                .ForMember(
               res => res.Provider,
               opt => opt.MapFrom(src => $"{src.Provider }"))
                .ForMember(
               res => res.CategoryID,
               opt => opt.MapFrom(src => $"{src.CategoryID }"));


        }
    }
}
