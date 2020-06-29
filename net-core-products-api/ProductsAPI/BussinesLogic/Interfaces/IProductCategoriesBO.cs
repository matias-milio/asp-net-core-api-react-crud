using DTO;
using System.Collections.Generic;


namespace BussinesLogic.Interfaces
{
    /// <summary>
    /// Handles the reading operations for Products Categories
    /// </summary>
    public interface IProductCategoriesBO
    {
        List<ProductCategoryDTO> GetAll();
        ProductCategoryDTO GetById(int id);      
    }
}
