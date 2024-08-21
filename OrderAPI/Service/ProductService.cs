using OrderAPI.Models.Dto;
using OrderAPI.Service.IService;
using Newtonsoft.Json;
using Steeltoe.Common.Discovery;
using Steeltoe.Discovery;

namespace OrderAPI.Service
{
    public class ProductService : IProductService
    {
        private readonly DiscoveryHttpClientHandler _handler;

        private readonly ILogger<ProductService> _logger;

        public ProductService(IDiscoveryClient discoveryClient, ILogger<ProductService> logger)
        {
            _handler = new DiscoveryHttpClientHandler(discoveryClient);
            _logger = logger;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            try
            {
                var client = new HttpClient(_handler, false);
                var response = await client.GetAsync("http://ProductAPI/api/product");
                var apiContent = await response.Content.ReadAsStringAsync();

                var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                if (resp.IsSuccess)
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(Convert.ToString(resp.Result));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Issue in GetProducts() using Eureka Server. " + ex.Message);
            }
            return new List<ProductDto>();
        }
    }
}
