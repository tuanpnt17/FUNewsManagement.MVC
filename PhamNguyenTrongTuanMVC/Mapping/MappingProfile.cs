using AutoMapper;

namespace PhamNguyenTrongTuanMVC.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RepositoryLayer.Entities.SystemAccount, ServiceLayer.Models.AccountDTO>();
        }
    }
}
