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
            #region =========== System Account Mapping ===========
            CreateMap<SystemAccount, AccountDTO>().ReverseMap();
            CreateMap<AccountDTO, ViewAccountViewModel>().ReverseMap();
            CreateMap<AccountDTO, AddNewAccountViewModel>().ReverseMap();
            CreateMap<AccountDTO, UpdateAccountViewModel>().ReverseMap();
            #endregion

            #region =========== Category Mapping ===========
            CreateMap<CategoryDTO, CategoryViewModel>()
                .ForMember(
                    dest => dest.NewsArticleCount,
                    opt => opt.MapFrom(src => src.NewsArticles.Count)
                );
            CreateMap<CategoryDTO, Category>().ReverseMap();
            CreateMap<CategoryDTO, AddNewCategoryViewModel>().ReverseMap();
            CreateMap<CategoryDTO, UpdateCategoryViewModel>().ReverseMap();

            #endregion

            #region =========== News Article Mapping ===========
            CreateMap<NewsArticleDTO, NewsArticle>();
            CreateMap<NewsArticle, NewsArticleDTO>()
                .ForMember(
                    n => n.TagIds,
                    opt =>
                        opt.MapFrom(src =>
                            src.NewsTags.Where(x => x.NewsArticleId == src.NewsArticleId)
                                .Select(x => x.TagId)
                        )
                );
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
                .ForMember(n => n.ModifiedDate, opt => opt.MapFrom(src => src.ModifiedDate));
            CreateMap<NewsArticleDTO, AddNewsArticleViewModel>().ReverseMap();
            CreateMap<NewsArticleDTO, UpdateNewsArticleViewModel>()
                .ForMember(
                    n => n.CreatedByName,
                    opt => opt.MapFrom(src => src.CreatedBy.AccountName)
                )
                .ForMember(
                    n => n.UpdatedByName,
                    opt => opt.MapFrom(src => src.UpdatedBy.AccountName)
                )
                .ReverseMap();

            #endregion

            #region =========== News Article Mapping ===========
            CreateMap<TagDTO, Tag>().ReverseMap();

            #endregion
        }
    }
}
