using DataAccess.Entities;
using DTO;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace BussinesLogic.Implementations
{
    public class ProductBO : BaseBoClass<Product>, Interfaces.IProductBO
    {
        private readonly IMapper mapper;

        public ProductBO(IConfiguration configuration, IMapper _mapper) : base(configuration) =>        
            mapper = _mapper;                                        

        public List<ProductDTO> GetAll()
        {
            var queryResult = repository.Get();
            return mapper.Map<List<ProductDTO>>(queryResult);
        }

        public ProductDTO GetById(int id)
        {
            var queryResult = repository.Get()
                                        .Where(x => x.Id.Equals(id)).FirstOrDefault();

            return mapper.Map<ProductDTO>(queryResult);
        }

        public void Create(ProductDTO newProduct)
        {
            var newProductEntity = mapper.Map<Product>(newProduct);
            repository.Insert(newProductEntity);       
        }

        public void Delete(int id)
        {
            repository.Delete(id);        
        }

        public void Update(int id, ProductDTO updatedDTOProduct)
        {
            var product = mapper.Map<Product>(updatedDTOProduct);
            repository.LazyUpdate(id, product);       
        }
}
}
