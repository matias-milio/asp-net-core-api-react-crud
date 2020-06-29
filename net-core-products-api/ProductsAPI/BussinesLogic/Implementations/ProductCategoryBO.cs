using System.Collections.Generic;
using AutoMapper;
using DataAccess.Entities;
using DTO;
using Microsoft.Extensions.Configuration;

namespace BussinesLogic.Implementations
{
    public class ProductCategoryBO : BaseBoClass<ProductCategory>, Interfaces.IProductCategoriesBO
    {
        private readonly IMapper mapper;     

        public ProductCategoryBO(IConfiguration configuration, IMapper _mapper) : base(configuration) =>
            mapper = _mapper;        

        public List<ProductCategoryDTO> GetAll()
        {            
            var queryResult = repository.Get();
            return mapper.Map<List<ProductCategoryDTO>>(queryResult);
        }

        public ProductCategoryDTO GetById(int id)
        {            
            var queryResult = repository.GetByID(id);
            return mapper.Map<ProductCategoryDTO>(queryResult);
        }
    }
}
