using AutoMapper;
using PhamNguyenTrongTuanMVC.Models.Account;
using PhamNguyenTrongTuanMVC.Models.Category;
using RepositoryLayer.Entities;
using ServiceLayer.Models;

namespace PhamNguyenTrongTuanMVC.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SystemAccount, AccountDTO>().ReverseMap();
            CreateMap<AccountDTO, ViewAccountViewModel>().ReverseMap();
            CreateMap<AccountDTO, AddNewAccountViewModel>().ReverseMap();
            CreateMap<AccountDTO, UpdateAccountViewModel>().ReverseMap();

            CreateMap<CategoryDTO, CategoryViewModel>()
                .ForMember(
                    dest => dest.NewsArticleCount,
                    opt => opt.MapFrom(src => src.NewsArticles.Count)
                );
            CreateMap<CategoryDTO, Category>().ReverseMap();
            CreateMap<CategoryDTO, AddNewCategoryViewModel>().ReverseMap();
            CreateMap<CategoryDTO, UpdateCategoryViewModel>().ReverseMap();

            CreateMap<NewsArticleDTO, NewsArticle>().ReverseMap();
        }
    }
}
