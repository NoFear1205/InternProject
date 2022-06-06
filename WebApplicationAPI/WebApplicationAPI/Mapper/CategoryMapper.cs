using AutoMapper;
using DomainLayer.Model;
using DomainLayer.ViewModel.CategoryView;

namespace WebApplicationAPI.Mapper
{
    public class CategoryMapper : Profile
    {
        public CategoryMapper()
        {
            CreateMap<Category,CategoryView>()
                 .ForMember(
                res => res.Id,
                opt => opt.MapFrom(src => $"{src.ID }"))
                .ForMember(
                res => res.name,
                opt => opt.MapFrom(src => $"{src.Name}"));
            CreateMap<CategoryAdd, Category>()
                .ForMember(
                res => res.Name,
                opt => opt.MapFrom(src => $"{src.name}"));
            CreateMap<CategoryView, Category>()
                 .ForMember(
                res => res.ID,
                opt => opt.MapFrom(src => $"{src.Id }"))
                .ForMember(
                res => res.Name,
                opt => opt.MapFrom(src => $"{src.name}"));
        }
    }
}
