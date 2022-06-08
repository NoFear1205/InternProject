using AutoMapper;
using DomainLayer.Model;
using DomainLayer.ViewModel.CategoryView;

namespace WebApplicationAPI.Mapper
{
    public class CategoryMapper : Profile
    {
        public CategoryMapper()
        {
            CreateMap<Category, CategoryView>();
            CreateMap<CategoryView, Category>();
            CreateMap<CategoryAdd, Category>();

        }
    }
}
