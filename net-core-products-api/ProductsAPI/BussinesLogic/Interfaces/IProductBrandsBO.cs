using DTO;
using System.Collections.Generic;

namespace BussinesLogic.Interfaces
{
    /// <summary>
    /// Handles the reading operations for ProductsBrands
    /// </summary>
    public interface IProductBrandsBO
    {
        List<ProductBrandDTO> GetAll();
        ProductBrandDTO GetById(int id);
    }
}
