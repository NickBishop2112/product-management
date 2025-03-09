using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductManagement.API.Controllers;
using ProductManagement.API.Model;
using ProductManagement.API.Repositories;
using Xunit;
using Assert = Xunit.Assert;

namespace ProductManagement.API.Tests.Controllers;

public class ProductsControllerTests
{
    private readonly Fixture _fixture;
    private readonly Mock<IProductRepository> _productRepository;
    private readonly ProductsController _controller;

    public ProductsControllerTests()
    {
        _fixture = new Fixture();
        _productRepository = new Mock<IProductRepository>();
        _controller = new ProductsController(_productRepository.Object);
    }
    
    [Fact]
    public async Task Register_ProductInserted()
    {
        // Arrange
        var product = _fixture.Create<Product>();

        // Act
        var result = await _controller.Register(product);

        // Assert
        result.Should().NotBeNull();
        _productRepository.Verify(x => x.Register(product), Times.Once);
    }

    [Fact]
    public async Task Register_ErrorThrown()
    {
        // Arrange
        var product = _fixture.Create<Product>();
        _productRepository.Setup(x => x.Register(product)).Throws<InvalidOperationException>();

        // Act
        var result = await _controller.Register(product);

        // Assert
        result.Should().BeOfType<ObjectResult>() 
            .Which.StatusCode.Should().Be(500); 
        _productRepository.Verify(x => x.Register(product), Times.Once);
    }
    
    [Fact]
    public async Task GetDetails_DetailsReturned()
    {
        // Arrange
        var products = _fixture.CreateMany<Product>(3);
        _productRepository.Setup(x => x.GetDetails()).ReturnsAsync(products);

        // Act
        var result = await _controller.GetDetails();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        okResult.Value.Should().BeEquivalentTo(products);
        _productRepository.Verify(x => x.GetDetails(), Times.Once);
    }
    
    [Fact]
    public async Task GetDetails_ErrorThrown()
    {
        // Arrange
        var products = _fixture.CreateMany<Product>(3);
        _productRepository.Setup(x => x.GetDetails()).Throws<InvalidOperationException>();

        // Act
        var result = await _controller.GetDetails();

        // Assert
        result.Should().BeOfType<ObjectResult>() 
            .Which.StatusCode.Should().Be(500); 
        _productRepository.Verify(x => x.GetDetails(), Times.Once);
    }
}