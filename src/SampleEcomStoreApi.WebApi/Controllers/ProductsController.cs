using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web.Http;
using SampleEcomStoreApi.Contracts.ServiceContracts;
using SampleEcomStoreApi.Contracts.DataContracts;

namespace SampleEcomStoreApi.WebApi.Controllers
{
    /// <summary>
    /// REST API for Product operations
    /// </summary>
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private IProductService GetProductService()
        {
            var binding = new BasicHttpBinding();
            var endpoint = new EndpointAddress("http://localhost:8731/ProductService/");
            var factory = new ChannelFactory<IProductService>(binding, endpoint);
            return factory.CreateChannel();
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns>List of products</returns>
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllProducts()
        {
            try
            {
                var service = GetProductService();
                var products = service.GetAllProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get product by ID
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>Product details</returns>
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetProduct(int id)
        {
            try
            {
                var service = GetProductService();
                var product = service.GetProductById(id);
                if (product == null)
                    return NotFound();
                return Ok(product);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Create a new product
        /// </summary>
        /// <param name="product">Product to create</param>
        /// <returns>Created product ID</returns>
        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateProduct([FromBody] ProductDto product)
        {
            try
            {
                if (product == null)
                    return BadRequest("Product is required");

                var service = GetProductService();
                var productId = service.CreateProduct(product);
                return Ok(new { ProductId = productId });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Update an existing product
        /// </summary>
        /// <param name="product">Product to update</param>
        /// <returns>Success status</returns>
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateProduct([FromBody] ProductDto product)
        {
            try
            {
                if (product == null)
                    return BadRequest("Product is required");

                var service = GetProductService();
                var success = service.UpdateProduct(product);
                return Ok(new { Success = success });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="id">Product ID to delete</param>
        /// <returns>Success status</returns>
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeleteProduct(int id)
        {
            try
            {
                var service = GetProductService();
                var success = service.DeleteProduct(id);
                return Ok(new { Success = success });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Search products by name
        /// </summary>
        /// <param name="query">Search query</param>
        /// <returns>Matching products</returns>
        [HttpGet]
        [Route("search")]
        public IHttpActionResult SearchProducts([FromUri] string query)
        {
            try
            {
                var service = GetProductService();
                var products = service.SearchProducts(query ?? "");
                return Ok(products);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get products by category
        /// </summary>
        /// <param name="category">Category name</param>
        /// <returns>Products in category</returns>
        [HttpGet]
        [Route("category/{category}")]
        public IHttpActionResult GetProductsByCategory(string category)
        {
            try
            {
                var service = GetProductService();
                var products = service.GetProductsByCategory(category);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
