using Microsoft.AspNetCore.Mvc;
using products.application.Services.Interface;
using products.crosscutting.ViewModel;

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

        [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status200OK)]
        [HttpGet]
        public Task<IEnumerable<ProductViewModel>> GetAll()
        {
            return _productService.GetAll();
        }

        [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<ProductViewModel> GetProductById(string id)
        {
            var detail = _productService.GetById(id);
            return Ok(detail);
        }

        //[ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        [HttpPost]
        public ActionResult<ProductViewModel> Post([FromBody] AddProductViewModel product)
        {
            var result = _productService.Add(product);
            if (result.IsError)
                return UnprocessableEntity(result.Errors);

            return Ok(result);
        }

        [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status201Created)]
        [HttpPut("{id}")]
        public ActionResult<ProductViewModel> Put([FromBody] ProductViewModel product)
        {
            var detail = _productService.Update(product);
            return Ok(detail);
        }

        //[ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status412PreconditionFailed)]
        [HttpDelete("{id}")]
        public ActionResult<ProductViewModel> Delete(string id)
        {
            var detail = _productService.Remove(id);
            return Ok(detail);
        }
    }
}
