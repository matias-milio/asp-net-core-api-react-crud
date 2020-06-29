namespace ProductsAPI.Models
{
    public class ProductCreationRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }

        public int ProductCategory_Id { get; set; }     
        public int ProductBrand_Id { get; set; }
       
    }
}
