using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers;

[ApiController]
[ApiVersion("1.0",Deprecated = true)]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductController : ControllerBase
{
    [HttpGet("{productId}")]
    public IActionResult GetProductV1([FromRoute]int productId)
    {
        var product = new Product
        {
            Id = productId,
            Name = "Product_1", 
            Price = 5
        };
        return Ok(product);
    }
    
    [MapToApiVersion("2.0")]
    [HttpGet("{productId}")]
    public IActionResult GetProductV2([FromRoute]int productId)
    {
        var product = new Product
        {
            Id = productId,
            Name = "Product_2",
            Price = 10
        };
        return Ok(product);
    }
}