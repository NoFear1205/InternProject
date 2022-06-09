using AutoMapper;
using DomainLayer.Model;
using DomainLayer.ViewModel.CategoryView;
using DomainLayer.ViewModel.CategoryViewModel;

namespace WebApplicationAPI.Mapper
{
    public class CategoryMapper : Profile
    {
        public CategoryMapper()
        {
            CreateMap<Role, CategoryView>();
            CreateMap<CategoryView, Role>();
            CreateMap<CategoryAddModel, Role>();
            CreateMap<CategoryUpdateModel, Role>();
        }
    }
}
