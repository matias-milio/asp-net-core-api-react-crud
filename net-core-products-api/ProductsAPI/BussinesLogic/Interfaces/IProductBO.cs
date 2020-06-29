using DTO;
using System.Collections.Generic;


namespace BussinesLogic.Interfaces
{
    public interface IProductBO
    {
        List<ProductDTO> GetAll();
        ProductDTO GetById(int id);
        void Create(ProductDTO newProduct);
        void Delete(int id);
        void Update(int id, ProductDTO updatedDTOProduct);
    }
}
