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
                opt => opt.MapFrom(src => $"{src.Id }"))
                .ForMember(
                res => res.Name,
                opt => opt.MapFrom(src => $"{src.Name}"));
            CreateMap<CategoryAddModel, Category>()
                .ForMember(
                res => res.Name,
                opt => opt.MapFrom(src => $"{src.Name}"));
            CreateMap<CategoryView, Category>()
                 .ForMember(
                res => res.Id,
                opt => opt.MapFrom(src => $"{src.Id }"))
                .ForMember(
                res => res.Name,
                opt => opt.MapFrom(src => $"{src.Name}"));
        }
    }
}
