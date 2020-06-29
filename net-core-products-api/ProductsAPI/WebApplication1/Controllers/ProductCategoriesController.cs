using System;
using System.Collections.Generic;
using AutoMapper;
using BussinesLogic.Interfaces;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProductsAPI.ConfigModels;
using ProductsAPI.Helpers;

namespace ProductsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoriesController : ControllerBase
    {        
        private readonly IProductCategoriesBO productCategoriesBO;
        private readonly IMapper mapper;
        private readonly ICacheHelper cacheHelper;
        private readonly ILogger<ProductCategoriesController> logger;
        private readonly CacheConfigurationOptions cacheConfigurationOptions;

        public ProductCategoriesController(IOptions<CacheConfigurationOptions> options,
                                  IMapper _mapper,                                 
                                  ICacheHelper _cacheHelper,
                                  IProductCategoriesBO _productCategoriesBO,
                                  ILogger<ProductCategoriesController> _logger
                                  )
        {
            cacheConfigurationOptions = options.Value;
            mapper = _mapper;            
            cacheHelper = _cacheHelper;
            productCategoriesBO = _productCategoriesBO;
            logger = _logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductCategoryDTO>> Get()
        {
            List<ProductCategoryDTO> response = new List<ProductCategoryDTO>();
            try
            {
                List<ProductCategoryDTO> products = new List<ProductCategoryDTO>();
                if (cacheConfigurationOptions.UseCache)
                    products = cacheHelper.GetOrSet<List<ProductCategoryDTO>>(cacheConfigurationOptions.ProductKey, () => productCategoriesBO.GetAll(), cacheConfigurationOptions.CacheDuration);
                else
                    products = productCategoriesBO.GetAll();

                response = mapper.Map<List<ProductCategoryDTO>>(products);

            }
            catch (Exception ex)
            {
                logger.LogError(MessageHelper.ErrorMessages.GetError.ToString(), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "There was an error within the server.");
            }
            return response;
        }
    }
}
