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
    public class ProductBrandsController : ControllerBase
    {        
        private readonly IProductBrandsBO productBrandBO;        
        private readonly IMapper mapper;
        private readonly ICacheHelper cacheHelper;
        private readonly ILogger<ProductBrandsController> logger;
        private readonly CacheConfigurationOptions cacheConfigurationOptions;

        public ProductBrandsController(IOptions<CacheConfigurationOptions> options,
                                  IMapper _mapper,                                  
                                  ICacheHelper _cacheHelper,
                                  IProductBrandsBO _productBrandBO,
                                  ILogger<ProductBrandsController> _logger
                                  )
        {
            cacheConfigurationOptions = options.Value;
            mapper = _mapper;
            cacheHelper = _cacheHelper;
            productBrandBO = _productBrandBO;
            logger = _logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductBrandDTO>> Get()
        {
            List<ProductBrandDTO> response = new List<ProductBrandDTO>();
            try
            {
                List<ProductBrandDTO> productBrands =  new List<ProductBrandDTO>();
                if (cacheConfigurationOptions.UseCache)
                    productBrands = cacheHelper.GetOrSet<List<ProductBrandDTO>>(cacheConfigurationOptions.ProductKey, () => productBrandBO.GetAll(), cacheConfigurationOptions.CacheDuration);
                else
                    productBrands = productBrandBO.GetAll();
                response = mapper.Map<List<ProductBrandDTO>>(productBrands);
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
