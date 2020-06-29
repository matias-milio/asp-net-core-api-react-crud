using DataAccess.Entities;
using DTO;

namespace ProductsAPI.MappingProfiles
{
    public class BussinesMappingProfile : AutoMapper.Profile
    {
        public BussinesMappingProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();
            CreateMap<ProductCategory, ProductCategoryDTO>();
            CreateMap<ProductoBrand, ProductBrandDTO>();
        }
    }
}
