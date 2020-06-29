using Repository.Implementations;
using DataAccess.Entities;
using Microsoft.Extensions.Configuration;

namespace BussinesLogic
{
    /// <summary>
    /// Base class for BO that uses database services
    /// </summary>
    /// <typeparam name="TEntity">Database entity related</typeparam>    
    public class BaseBoClass<TEntity> where TEntity : class
    {
        private IConfiguration Configuration { get; }
        protected private BaseRepository<TEntity> repository;        
        protected private string cacheKey;


        public BaseBoClass(IConfiguration configuration)
        {
            Configuration = configuration;
            repository = new BaseRepository<TEntity>(new MyStoreDBContext(Configuration));
        }




    }
}
