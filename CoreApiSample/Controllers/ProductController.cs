using CoreApiSample.Db;
using CoreApiSample.Db.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Channels;

namespace CoreApiSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ShopSolutionDbContext _db;
        public ProductController() 
        {
            _db = new ShopSolutionDbContext();
        }

        [HttpGet]
        public ActionResult<List<Product>> Get()
        {
            try
            {
                var productList = _db.Products.ToList();
                return Ok(productList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Add([FromBody] Product product)
        {
            try
            {
                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                } 
                product.CreatedDate = DateTime.Now;
                _db.Products.Add(product);
                var changes = _db.SaveChanges();
                if (changes > 0)
                {
                    return Ok("Product Added Successfully!");
                }
                return NotFound("Product Adding Failed!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{Id}")]
        public ActionResult<Product> Get(int Id)
        {
            try
            {
                var productObj = _db.Products.Where(x => x.Id == Id).FirstOrDefault();
                if (productObj != null)
                {
                    return Ok(productObj);
                }
                return NotFound("Product Not Found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{Id}")]
        public ActionResult Update(int Id, [FromBody] Product product)
        {
            try
            {
                // Validating Model
                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }
                // checking product
                var productObj = _db.Products.Where(x => x.Id == Id).FirstOrDefault();
                if (productObj == null)
                {
                    return NotFound("Product Not Found");
                }
                // updating product
                productObj.Name = product.Name;
                productObj.Description = product.Description;
                productObj.Price = product.Price;
                productObj.ManufacturingDate = product.ManufacturingDate;
                productObj.ExpireDate = product.ExpireDate;
                productObj.CreatedDate = DateTime.Now;
                _db.Products.Update(product);
                int changes = _db.SaveChanges();
                if (changes > 0)
                {
                    return Ok("Product Updated Successfully!");
                }
                return BadRequest("Product Updating Failed!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{Id}")]
        public ActionResult Delete(int Id)
        {
            try
            {
                var productObj = _db.Products.Where(x => x.Id == Id).FirstOrDefault();
                if (productObj == null)
                {
                    return NotFound("Product Not Found");
                }
                _db.Remove(productObj);
                int changes = _db.SaveChanges();
                if (changes > 0)
                {
                    return Ok("Product Deleted Successfully!");
                }
                return NotFound("Product Deleting Failed!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
