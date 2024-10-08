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

            var getAllProductsService = new Mock<IProductService>();

            getAllProductsService.Setup(_ => _.GetAll())
                .Returns(Task.FromResult((IEnumerable<ProductViewModel>)listResult));

            var productController = new ProductsController(getAllProductsService.Object);

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

            var getAllProductsService = new Mock<IProductService>();

            getAllProductsService.Setup(_ => _.GetById(It.IsAny<string>()))
                .Returns(expectedResult);

            var productController = new ProductsController(getAllProductsService.Object);

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

            var getAllProductsService = new Mock<IProductService>();

            getAllProductsService.Setup(_ => _.Remove(It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            var productController = new ProductsController(getAllProductsService.Object);

            //act
            var result = productController.Delete(productId);

            //assert 
            var createdResult = (result.Result as ObjectResult)!.Value;
            var createdResultStatusCode = (result.Result as ObjectResult)!.StatusCode;
            Assert.NotNull(result);
            Assert.NotNull(createdResult);
            Assert.Equal(200, createdResultStatusCode);
        }


        [Fact]
        public void SHOULD_UPDATE_PRODUCT()
        {
            //Arrage
            var product = new ProductViewModel("3SC", "ProductTest", 10.87m, 10);
            var updatedProduct = new ProductViewModel("3SC", "ProductTest", 27.80m, 7);
            var getAllProductsService = new Mock<IProductService>();

            getAllProductsService.Setup(_ => _.Update(It.IsAny<ProductViewModel>()))
                .Returns(Task.FromResult(updatedProduct));

            var productController = new ProductsController(getAllProductsService.Object);

            //act
            var result = productController.Put(updatedProduct);

            //assert
            Assert.NotNull(result);
            var createdResult = (result.Result as ObjectResult)!.Value;
            var createdResultStatusCode = (result.Result as ObjectResult)!.StatusCode;
            Assert.NotNull(createdResult);
            Assert.Equal(200, createdResultStatusCode);
        }
    }
}