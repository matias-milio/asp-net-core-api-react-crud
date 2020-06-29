using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using ProductsAPI.ConfigModels;
using ProductsAPI.Models;
using ProductsAPI.Helpers;
using AutoMapper;
using DTO;
using BussinesLogic.Interfaces;

namespace ProductsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductBO productBO;    
        private readonly IMapper mapper;
        private readonly ICacheHelper cacheHelper;
        private readonly ILogger<ProductsController> logger;
        private readonly CacheConfigurationOptions cacheConfigurationOptions;

        public ProductsController(IOptions<CacheConfigurationOptions> options,
                                  IMapper _mapper,
                                  IProductBO _productBO,
                                  ICacheHelper _cacheHelper,
                                  ILogger<ProductsController> _logger
                                  )
        {
            cacheConfigurationOptions = options.Value;
            mapper = _mapper;
            productBO = _productBO;
            cacheHelper = _cacheHelper;
            logger = _logger;
        }
                
        [HttpGet]
        public ActionResult<IEnumerable<ProductOverviewResponse>> Get()
        {
            List<ProductOverviewResponse> response = new List<ProductOverviewResponse>();
            try
            {
                List<ProductDTO> products = new List<ProductDTO>();
                if (cacheConfigurationOptions.UseCache)
                    products = cacheHelper.GetOrSet(cacheConfigurationOptions.ProductKey, () => productBO.GetAll(), cacheConfigurationOptions.CacheDuration);
                else
                    products = productBO.GetAll();

                response = mapper.Map<List<ProductOverviewResponse>>(products);                
                
            }
            catch (Exception ex)
            {
                logger.LogError(MessageHelper.ErrorMessages.GetError.ToString(), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "There was an error within the server.");
            }
            return response;
        }        
        
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<ProductDTO> Get(int id)
        {
            ProductDTO response = new ProductDTO();          
            try
            {
                var product = this.GetProduct(id);
                response = mapper.Map<ProductDTO>(product);
            }
            catch (Exception ex)
            {
                logger.LogError(MessageHelper.ErrorMessages.GetByError.ToString(), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "There was an error within the server.");
            }
            return response; 
        }

        [HttpPost]
        public ActionResult<ProductDTO> Post(ProductDTO newProduct)
        {            
            try
            {
                productBO.Create(newProduct);
                if (cacheConfigurationOptions.UseCache)
                    cacheHelper.UpdateCache<List<ProductDTO>>(cacheConfigurationOptions.ProductKey, () => productBO.GetAll(), cacheConfigurationOptions.CacheDuration);                
            }
            catch (Exception ex)
            {
                logger.LogError(MessageHelper.ErrorMessages.PostError.ToString(), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "There was an error within the server.");
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, ProductDTO updatedProduct)
        {            
            try
            {
                var product = this.GetProduct(id);
                productBO.Update(id,updatedProduct);

                if (cacheConfigurationOptions.UseCache)
                    cacheHelper.UpdateCache<List<ProductDTO>>(cacheConfigurationOptions.ProductKey, () => productBO.GetAll(), cacheConfigurationOptions.CacheDuration);
            }
            catch (Exception ex)
            {
                logger.LogError(MessageHelper.ErrorMessages.PutError.ToString(), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "There was an error within the server.");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<ProductDTO> Delete(int id)
        {
            ProductDTO deletedProduct = new ProductDTO();
            try
            {
                var product = this.GetProduct(id);
                productBO.Delete(id);

                if (cacheConfigurationOptions.UseCache)
                    cacheHelper.UpdateCache<List<ProductDTO>>(cacheConfigurationOptions.ProductKey, () => productBO.GetAll(), cacheConfigurationOptions.CacheDuration);
            }
            catch (Exception ex)
            {
                logger.LogError(MessageHelper.ErrorMessages.DeleteError.ToString(), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "There was an error within the server.");
            }
            return deletedProduct;
        }

        /// <summary>
        /// Gets a product with the given id and returns NotFound if not exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private ActionResult<ProductDTO> GetProduct(int id)
        {
            ProductDTO product = new ProductDTO();
            if (cacheConfigurationOptions.UseCache)
                product = cacheHelper.GetOrSet<ProductDTO>(cacheConfigurationOptions.ProductKey, () => productBO.GetById(id), cacheConfigurationOptions.CacheDuration);
            else
                product = productBO.GetById(id);

            if (product == null) return NotFound();

            return product;
        }
    }
}
