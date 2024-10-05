using Microsoft.AspNetCore.Mvc;
using products.application.Services.Interface;
using products.application.ViewModel;

namespace products.api.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public Task<IEnumerable<ProductViewModel>> GetAll()
        {
            return _productService.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<ProductViewModel> Get(string id)
        {
            var detail =  _productService.GetById(id);
            return Ok(detail);
        }

        [HttpPost]
        public ActionResult<ProductViewModel> Post([FromBody] ProductViewModel product)
        {
            var detail = _productService.Add(product);
            return Ok(detail);
        }

        [HttpPut("{id}")]
        public ActionResult<ProductViewModel> Put([FromBody] ProductViewModel product)
        {
            var detail = _productService.Update(product);
            return Ok(detail);
        }

        [HttpDelete("{id}")]
        public ActionResult<ProductViewModel> Delete(string id)
        {
            var detail = _productService.Remove(id);
            return Ok(detail);
        }
    }
}
