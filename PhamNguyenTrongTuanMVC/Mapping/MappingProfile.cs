using AutoMapper;
using PhamNguyenTrongTuanMVC.Models.Account;
using PhamNguyenTrongTuanMVC.Models.Category;
using PhamNguyenTrongTuanMVC.Models.NewsArticle;
using RepositoryLayer.Entities;
using ServiceLayer.Models;

namespace PhamNguyenTrongTuanMVC.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //System Account
            CreateMap<SystemAccount, AccountDTO>()
                .ReverseMap();
            CreateMap<AccountDTO, ViewAccountViewModel>().ReverseMap();
            CreateMap<AccountDTO, AddNewAccountViewModel>().ReverseMap();
            CreateMap<AccountDTO, UpdateAccountViewModel>().ReverseMap();

            //Category
            CreateMap<CategoryDTO, CategoryViewModel>()
                .ForMember(
                    dest => dest.NewsArticleCount,
                    opt => opt.MapFrom(src => src.NewsArticles.Count)
                );
            CreateMap<CategoryDTO, Category>().ReverseMap();
            CreateMap<CategoryDTO, AddNewCategoryViewModel>().ReverseMap();
            CreateMap<CategoryDTO, UpdateCategoryViewModel>().ReverseMap();

            //News Article
            CreateMap<NewsArticleDTO, NewsArticle>()
                .ReverseMap();
            CreateMap<NewsArticleDTO, NewsArticleViewModel>().ReverseMap();
            CreateMap<NewsArticleDTO, ViewNewsArticleViewModel>()
                .ForMember(
                    n => n.CategoryName,
                    opt => opt.MapFrom(src => src.Category.CategoryName)
                )
                .ForMember(
                    n => n.CreatedByName,
                    opt => opt.MapFrom(src => src.CreatedBy.AccountName)
                )
                .ForMember(
                    n => n.UpdatedByName,
                    opt => opt.MapFrom(src => src.UpdatedBy.AccountName)
                )
                .ForMember(
                    n => n.ModifiedDate,
                    opt => opt.MapFrom(src => src.ModifiedDate ?? src.CreatedDate)
                );
            ;
        }
    }
}
