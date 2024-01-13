using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web2.API
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        static List<Product> products = new List<Product>()
        {
            new Product { Id = 1, Name = "Iphone X"},
            new Product { Id = 2, Name = "Google Pixel 8"},
            new Product { Id = 3, Name = "Samsung Galaxy S10"}
        };

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return Ok(products);
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetbyId(int id)
        {
            var product = products.Find(x => x.Id == id);
            return product == null ? NotFound() : Ok(product);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            var newProduct = new Product 
            { 
                Id = products.Select(x => x.Id).Max() + 1, 
                Name = product.Name, 
                Description = product.Description 
            };

            products.Add(newProduct);

            return CreatedAtAction(nameof(GetbyId), new {id =  newProduct.Id}, newProduct);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var product = products.Find(x => x.Id == id);

            if(product != null) 
            {
                products.Remove(product);
 
            }
            return NoContent();
        }
    }
}
