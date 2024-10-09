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
            var result = _productService.GetById(id);

            if (result is null)
                return NotFound();
            return Ok(result);
        }

        [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<ProductViewModel> Post([FromBody] AddProductViewModel product)
        {
            var result = _productService.Add(product);

            if (result.IsError)
                return BadRequest(result.Errors);
            return CreatedAtAction(nameof(GetProductById), new { id = result.Value.Id }, result.Value);
        }

        [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [HttpPut("{id}")]
        public ActionResult<ProductViewModel> Put(string id, [FromBody] ProductViewModel product)
        {
            if (product.Id != id)
                return BadRequest("The product ID does not match.");

            var result = _productService.Update(product);

            return result.Result.IsError ? BadRequest(result.Result.Errors) : Ok();
        }

        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status412PreconditionFailed)]
        [HttpDelete("{id}")]
        public ActionResult<ProductViewModel> Delete(string id)
        {
            _productService.Remove(id);
            return NoContent();
        }
    }
}
