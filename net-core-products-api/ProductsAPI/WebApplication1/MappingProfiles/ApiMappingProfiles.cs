using DTO;
using ProductsAPI.Models;

namespace ProductsAPI.MappingProfiles
{
    public class ApiMappingProfiles : AutoMapper.Profile
    {
        public ApiMappingProfiles()
        {           
            CreateMap<ProductDTO, ProductOverviewResponse>();        }
    }


}
