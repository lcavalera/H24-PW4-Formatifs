using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web2.API
{
    [Route("api/products")]
    [ApiController]
    [Produces("application/json")]
    public class ProductController : ControllerBase
    {
        static List<Product> products = new List<Product>()
        {
            new Product { Id = 1, Name = "Iphone X"},
            new Product { Id = 2, Name = "Google Pixel 8"},
            new Product { Id = 3, Name = "Samsung Galaxy S10"}
        };

        /// <summary>
        /// Retourne une liste des produits 
        /// </summary>
        /// <remarks>
        /// 
        ///     GET api/products
        ///
        /// </remarks>
        /// <response code="200">produits trouvés et retournés</response>
        /// <response code="404">produits introuvables</response>
        /// <response code="500">service indisponible pour le moment</response>
        /// <returns></returns>
        // GET: api/<ProductController>
        [HttpGet]
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return Ok(products);
        }

        /// <summary>
        /// Retourne un produit spécifique à partir de son id
        /// </summary>
        /// <param name="id">id du produit à retourner</param>
        /// <remarks>
        /// 
        ///     GET /Product
        ///
        /// </remarks>
        /// <response code="200">produit trouvé et retourné</response>
        /// <response code="404">produit introuvable pour l'id spécifié</response>
        /// <response code="500">service indisponible pour le moment</response>
        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Product> GetbyId(int id)
        {
            var product = products.Find(x => x.Id == id);
            return product == null ? NotFound() : Ok(product);
        }

        /// <summary>
        /// Ajoute un produit à la base de donnée
        /// </summary>
        /// <param name="product">produit à ajouter</param>
        /// <returns>Un neuveau produit a été créé</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Product
        ///     {
        ///        "id": 1,
        ///        "name": "nom du produit",
        ///        "description": "description du produit"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">produit ajouté avec succès</response>
        /// <response code="200">traitement executé avec succès, contenu retourné</response>
        /// <response code="204">traitement executé avec succès, aucune contenu retourné</response>
        /// <response code="400">Model Invalide, mauvaise requête</response>
        /// <response code="404">produit introuvable</response>
        /// <response code="500">service indisponible pour le moment</response>
        // POST api/<ProductController>
        [HttpPost]
        [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Product), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] Product product)
        {
            var newProduct = new Product
            {
                Id = products.Select(x => x.Id).Max() + 1,
                Name = product.Name,
                Description = product.Description
            };

            products.Add(newProduct);

            return CreatedAtAction(nameof(GetbyId), new { id = newProduct.Id }, newProduct);
        }

        /// <summary>
        /// Supprime un produit
        /// </summary>
        /// <param name="id">id du produit à supprimer</param>
        /// <response code="200">produit supprimé avec succès</response>
        /// <response code="204">produit supprimé avec succès, aucune contenu retourné</response>
        /// <response code="404">produit introuvable pour l'id spécifié</response>
        /// <response code="500">service indisponible pour le moment</response>
        // DELETE api/<ProductController>/5
        [HttpDelete]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Product), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Delete(int id)
        {
            var product = products.Find(x => x.Id == id);

            if (product != null)
            {
                products.Remove(product);

            }
            return NoContent();
        }
    }
}
