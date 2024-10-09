using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Moq;
using products.api.Controllers;
using products.application.Services.Interface;
using products.crosscutting.ViewModel;

namespace products.test.Controller
{
    public class ProductControllerTest
    {
        [Fact]
        public async void SHOULD_GETALL_PRODUCTS()
        {
            //Arrage
            var listResult = new List<ProductViewModel>()
            {
                new ProductViewModel(Guid.NewGuid().ToString(), "ProductTest", 10.87m, 10),
                new ProductViewModel(Guid.NewGuid().ToString(), "ProductTest2", 11.0m, 7)
            };

            var productsService = new Mock<IProductService>();

            productsService.Setup(_ => _.GetAll())
                .Returns(Task.FromResult((IEnumerable<ProductViewModel>)listResult));

            var productController = new ProductsController(productsService.Object);

            //act
            var result = await productController.GetAll();

            //assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(result.ToArray()[0].Name, listResult.ToArray()[0].Name);
            Assert.Equal(result.ToArray()[0].Price, listResult.ToArray()[0].Price);
            Assert.Equal(result.ToArray()[0].Stock, listResult.ToArray()[0].Stock);

        }

        [Theory]
        [InlineData("3SC")]
        public void SHOULD_GET_PRODUCTBYID(string productId)
        {
            //Arrage
            var expectedResult = new ProductViewModel("3SC", "ProductTest", 10.87m, 10);

            var productsService = new Mock<IProductService>();

            productsService.Setup(_ => _.GetById(It.IsAny<string>()))
                .Returns(expectedResult);

            var productController = new ProductsController(productsService.Object);

            //act
            var result = productController.GetProductById(productId);

            //assert
            Assert.NotNull(result);
            var createdResult = (result.Result as ObjectResult)!.Value;
            var endpointResult = (ProductViewModel)createdResult!;
            Assert.Equal(expectedResult, createdResult);
            Assert.Equal(expectedResult.Id, endpointResult.Id);
            Assert.Equal(expectedResult.Name, endpointResult.Name);
            Assert.Equal(expectedResult.Price, endpointResult.Price);
            Assert.Equal(expectedResult.Stock, endpointResult.Stock);
        }

        [Theory]
        [InlineData("3SC")]
        public void SHOULD_REMOVE_PRODUCTBYID(string productId)
        {
            //Arrage
            var expectedResult = new ProductViewModel("3SC", "ProductTest", 10.87m, 10);

            var productsService = new Mock<IProductService>();

            productsService.Setup(_ => _.Remove(It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            var productController = new ProductsController(productsService.Object);

            //act
            var result = productController.Delete(productId);

            //assert 
            var createdResult = ((StatusCodeResult)result.Result!).StatusCode;
            Assert.NotNull(result);
            Assert.Equal(204, createdResult);
        }

        [Fact]
        public void SHOULD_UPDATE_PRODUCT()
        {
            //Arrage
            var product = new ProductViewModel("3SC", "ProductTest", 10.87m, 10);
            var updatedProduct = new ProductViewModel("3SC", "ProductTest", 27.80m, 7);
            var productsService = new Mock<IProductService>();

            productsService.Setup(_ => _.Update(It.IsAny<ProductViewModel>()))
                .Returns(Task.FromResult((ErrorOr.ErrorOr<ProductViewModel>)updatedProduct));

            var productController = new ProductsController(productsService.Object);

            //act
            var result = productController.Put(updatedProduct);

            //assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result.Result);
        }

        [Fact]
        public void SHOULD_ADD_PRODUCT()
        {
            //Arrage
            var product = new AddProductViewModel("ProductTest", 10.87m, 10);
            var newProduct = new ProductViewModel(Guid.NewGuid().ToString(), "ProductTest", 10.87m, 10);
            var productsService = new Mock<IProductService>();

            productsService.Setup(_ => _.Add(It.IsAny<AddProductViewModel>()))
                .Returns(newProduct);

            var productController = new ProductsController(productsService.Object);

            //act
            var result = productController.Post(product);

            //assert
            Assert.NotNull(result);
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedProduct = Assert.IsType<ProductViewModel>(createdResult.Value);
            Assert.Equal(newProduct.Id, returnedProduct.Id);
            Assert.Equal(newProduct.Name, returnedProduct.Name);
            Assert.Equal(newProduct.Price, returnedProduct.Price);
            Assert.Equal(newProduct.Stock, returnedProduct.Stock);
        }

        [Theory]
        [InlineData("123")]
        public void SHOULD_NOT_GET_PRODUCTBYID(string productId)
        {
            //Arrage
            var expectedResult = new ProductViewModel("3SC", "ProductTest", 10.87m, 10);

            var productsService = new Mock<IProductService>();

            productsService.Setup(_ => _.GetById(It.IsAny<string>()))
                .Returns((ProductViewModel)null);

            var productController = new ProductsController(productsService.Object);

            //act
            var result = productController.GetProductById(productId);

            //assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void SHOULD_NOT_ADD_PRODUCT()
        {
            //Arrage
            var product = new AddProductViewModel("", 10.87m, 10);
            var validationErrors = new List<Error> { Error.Validation("400") };
            var productsService = new Mock<IProductService>();

            productsService.Setup(_ => _.Add(It.IsAny<AddProductViewModel>()))
                .Returns(ErrorOr.ErrorOr<ProductViewModel>.From(validationErrors));

            var productController = new ProductsController(productsService.Object);

            //act
            var result = productController.Post(product);

            //assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result.Result);
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.Equal(validationErrors, badRequestResult?.Value);
        }

        [Fact]
        public void SHOULD_NOT_UPDATE_PRODUCT()
        {
            //Arrage
            var product = new ProductViewModel("", "", 10.87m, 10);
            var validationErrors = new List<Error> { Error.Validation("400") };
            var productsService = new Mock<IProductService>();

            productsService.Setup(_ => _.Update(It.IsAny<ProductViewModel>()))
                .Returns(Task.FromResult(ErrorOr.ErrorOr<ProductViewModel>.From(validationErrors)));

            var productController = new ProductsController(productsService.Object);

            //act
            var result = productController.Put(product);

            //assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result.Result);
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.Equal(validationErrors, badRequestResult?.Value);
        }
    }
}