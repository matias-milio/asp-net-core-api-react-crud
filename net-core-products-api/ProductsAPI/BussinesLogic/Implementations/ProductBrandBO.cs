using System.Collections.Generic;
using AutoMapper;
using DataAccess.Entities;
using DTO;
using Microsoft.Extensions.Configuration;

namespace BussinesLogic.Implementations
{
    public class ProductBrandBO : BaseBoClass<ProductoBrand>, Interfaces.IProductBrandsBO
    {
        private readonly IMapper mapper;

        public ProductBrandBO(IConfiguration configuration, IMapper _mapper) : base(configuration) =>
            mapper = _mapper;

        public List<ProductBrandDTO> GetAll()
        {            
            var queryResult = repository.Get();
            return mapper.Map<List<ProductBrandDTO>>(queryResult);
        }

        public ProductBrandDTO GetById(int id)
        {           
            var queryResult = repository.GetByID(id);
            return mapper.Map<ProductBrandDTO>(queryResult);
        }

    }
}
