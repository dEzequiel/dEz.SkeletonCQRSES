using AutoMapper;
using dEz.SkeletonCQRSES.Query.Domain.Entities;
using dEz.SkeletonCQRSES.Shared.DTOs;

namespace dEz.Skeleton.CQRSES.Query.Api.Mappers
{
    /// <summary>
    /// AutoMapper profile for company-related mappings.
    /// </summary>
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyForGet>()
                .ForCtorParam("FullAddress",
                    opt => opt.MapFrom(x =>
                        string.Join(' ', x.Address, x.Country)));

            CreateMap<CompanyForAdd, Company>();
            CreateMap<CompanyForUpdate, Company>().ReverseMap();

        }
    }
}
