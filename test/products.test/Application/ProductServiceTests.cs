using AutoMapper;
using Moq;
using products.application.Services;
using products.application.Services.Interface;
using products.crosscutting.ViewModel;
using products.domain.Entities;
using products.domain.Repository;

namespace products.test.Application
{
    public class ProductServiceTests
    {
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
        private readonly Mock<IProductRepository> _productRepository = new Mock<IProductRepository>();

        [Fact]
        public async Task SHOULD_GETALLPRODUCTS()
        {
            //arrange
            var listResult = new List<ProductViewModel>()
            {
                new ProductViewModel(Guid.NewGuid().ToString(), "ProductTest", 10.87m, 10),
                new ProductViewModel(Guid.NewGuid().ToString(), "ProductTest2", 11.0m, 7)
            };

            var productService = new Mock<IProductService>();

            productService.Setup(_ => _.GetAll())
                .Returns(Task.FromResult((IEnumerable<ProductViewModel>)listResult));
            var projectServiceMock = new ProductService(_productRepository.Object, _mapper.Object);

            //act
            var result = await projectServiceMock.GetAll();

            //assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<ProductViewModel>>(result);

        }

        [Theory]
        [InlineData("123")]
        public void SHOULD_GETPRODUCTBYID(string productId)
        {
            //arrange
            var expectedProduct = new ProductViewModel(productId, "ProductTest2", 11.0m, 7);

            var productService = new Mock<IProductService>();

            productService.Setup(_ => _.GetById(It.IsAny<string>()))
                .Returns(expectedProduct);
            _mapper.Setup(map => map.Map<ProductViewModel>(It.IsAny<Product>())).Returns(expectedProduct);

            var projectServiceMock = new ProductService(_productRepository.Object, _mapper.Object);

            //act
            var result = projectServiceMock.GetById(productId);

            //assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<ProductViewModel>(result);
            Assert.Equal(expectedProduct.Id, result.Id);
            Assert.Equal(expectedProduct.Name, result.Name);
            Assert.Equal(expectedProduct.Price, result.Price);
            Assert.Equal(expectedProduct.Stock, result.Stock);
        }

        [Theory]
        [InlineData("123")]
        public async Task SHOULD_REMOVEPRODUCTBYID(string productId)
        {
            //arrange
            var expectedProduct = new ProductViewModel(productId, "ProductTest2", 11.0m, 7);

            var productService = new Mock<IProductService>();

            _productRepository.Setup(_ => _.RemoveProductAsync(It.IsAny<string>())).Returns(Task.FromResult(true));

            var projectServiceMock = new ProductService(_productRepository.Object, _mapper.Object);

            //act
            var result = await projectServiceMock.Remove(productId);

            //assert
            Assert.True(result);
        }

        [Fact]
        public async Task SHOULD_UPDATEPRODUCT()
        {
            //arrange
            var expectedProduct = new ProductViewModel("123", "ProductTest2", 11.0m, 7);
            var productEntity = Product.Create("ProductTest2", 11.0m, 7);

            _mapper.Setup(map => map.Map<Product>(It.IsAny<ProductViewModel>())).Returns(productEntity);
            _mapper.Setup(map => map.Map<ProductViewModel>(It.IsAny<Product>())).Returns(expectedProduct);
            _productRepository.Setup(_ => _.Update(It.IsAny<Product>()));

            var productService = new Mock<IProductService>();

            productService.Setup(_ => _.Update(It.IsAny<ProductViewModel>()))
                .Returns(Task.FromResult((ErrorOr.ErrorOr<ProductViewModel>)expectedProduct));

            var projectServiceMock = new ProductService(_productRepository.Object, _mapper.Object);

            //act
            var result = await projectServiceMock.Update(expectedProduct);

            //assert
            Assert.False(result.IsError);
            Assert.Equal(expectedProduct.Id, result.Value.Id);
            Assert.Equal(expectedProduct.Name, result.Value.Name);
            Assert.Equal(expectedProduct.Price, result.Value.Price);
            Assert.Equal(expectedProduct.Stock, result.Value.Stock);
            Assert.IsType<ProductViewModel>(result.Value);
        }

        [Fact]
        public void SHOULD_ADDPRODUCT()
        {
            //arrange
            var product = new AddProductViewModel("ProductTest", 10.87m, 10);
            var expectedProduct = new ProductViewModel("123", "ProductTest2", 11.0m, 7);
            var productEntity =  Product.Create("ProductTest2",11.0m, 7 );

            _mapper.Setup(map => map.Map<Product>(It.IsAny<AddProductViewModel>())).Returns(productEntity);
            _mapper.Setup(map => map.Map<ProductViewModel>(It.IsAny<Product>())).Returns(expectedProduct);
            _productRepository.Setup(_ => _.Add(It.IsAny<Product>()));

            var productService = new Mock<IProductService>();

            productService.Setup(_ => _.Add(It.IsAny<AddProductViewModel>())).Returns(expectedProduct);

            var projectServiceMock = new ProductService(_productRepository.Object, _mapper.Object);

            //act
            var result =  projectServiceMock.Add(product);

            //assert
            Assert.False(result.IsError);
            Assert.Equal(expectedProduct, result.Value);
            Assert.Equal(expectedProduct.Name, result.Value.Name);
            Assert.Equal(expectedProduct.Price, result.Value.Price);
            Assert.Equal(expectedProduct.Stock, result.Value.Stock);
            Assert.IsType<ProductViewModel>(result.Value);
        }

    }
}
