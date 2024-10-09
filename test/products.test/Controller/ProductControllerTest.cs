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
        public async void GETALL_SHOULDRETURNPRODUCTS()
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
        public void GETBYPRODUCTID_SHOULDRETURNPRODUCT(string productId)
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
        public void REMOVE_WHENVALIDID_SHOULDREMOVEPRODUCT(string productId)
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

        [Theory]
        [InlineData("3SC")]
        public void UPDATE_WHENVALIDPRODUCT_SHOULDUPDATEPRODUCT(string productId)
        {
            //Arrage
            var product = new ProductViewModel("3SC", "ProductTest", 10.87m, 10);
            var updatedProduct = new ProductViewModel(productId, "ProductTest", 27.80m, 7);
            var productsService = new Mock<IProductService>();

            productsService.Setup(_ => _.Update(It.IsAny<ProductViewModel>()))
                .Returns(Task.FromResult((ErrorOr.ErrorOr<ProductViewModel>)updatedProduct));

            var productController = new ProductsController(productsService.Object);

            //act
            var result = productController.Put(productId, updatedProduct);

            //assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result.Result);
        }

        [Fact]
        public void ADD_SHOULDRETURNCREATEDPRODUCT()
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
        public void SHOULDNOTGETPRODUCTBYID_WHENINVALIDID_RETURNSNOTFOUND(string productId)
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
        public void SHOULDNOTADD_WHENPRODUCTFIELDSWEREINVALID_RETURNSBADREQUEST()
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

        [Theory]
        [InlineData("123")]
        public void SHOULDNOTUPDATE_WHENPRODUCTIDDOESNOTMATCH_RETURSNBADREQUEST(string productId)
        {
            //Arrage
            var product = new ProductViewModel("", "", 10.87m, 10);
            var validationErrors = new List<Error> { Error.Validation("400") };
            var productsService = new Mock<IProductService>();

            productsService.Setup(_ => _.Update(It.IsAny<ProductViewModel>()))
                .Returns(Task.FromResult(ErrorOr.ErrorOr<ProductViewModel>.From(validationErrors)));

            var productController = new ProductsController(productsService.Object);

            //act
            var result = productController.Put(productId,product);

            //assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result.Result);
            var badRequestResult = result.Result as BadRequestObjectResult;
        }

        [Theory]
        [InlineData("123")]
        public void SHOULDNOTUPDATE_WHENPRODUCTFIELDSWEREINVALID_RETURSNBADREQUEST(string productId)
        {
            //Arrage
            var product = new ProductViewModel(productId, "", 10.87m, 10);
            var validationErrors = new List<Error> { Error.Validation("400") };
            var productsService = new Mock<IProductService>();

            productsService.Setup(_ => _.Update(It.IsAny<ProductViewModel>()))
                .Returns(Task.FromResult(ErrorOr.ErrorOr<ProductViewModel>.From(validationErrors)));

            var productController = new ProductsController(productsService.Object);

            //act
            var result = productController.Put(productId, product);

            //assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result.Result);
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.Equal(validationErrors, badRequestResult?.Value);
        }
    }
}