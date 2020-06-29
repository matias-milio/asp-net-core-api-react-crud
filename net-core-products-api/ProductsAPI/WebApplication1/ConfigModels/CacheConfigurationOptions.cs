namespace ProductsAPI.ConfigModels
{
    public class CacheConfigurationOptions
    {
        public bool UseCache { get; set; }
        public int CacheDuration { get; set; }
        public string ProductKey { get; set; }
    }
}
