using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Xml.Linq;

namespace TechBiz_Api_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {

        //tutorila Link :https://www.youtube.com/watch?v=svnNmKVymXA
        //Connection Postgres : https://medium.com/@saisiva249/how-to-configure-postgres-database-for-a-net-a2ee38f29372 
        //Connection Postgres (youtube) : https://www.youtube.com/watch?v=SlYf25tCCYY
        //Postgres doc : https://tembo.io/docs/getting-started/postgres_guides/connecting-to-postgres-with-c-sharp

        //https://localhost:7142/Product

        [HttpGet]
        public ActionResult<List<String>> GetProduct()
        {
            var products = new List<String>();
            products.Add("VueJs");
            products.Add("Flutter");
            products.Add("Reates");
            products.Add("Angular");

            return Ok(products);
        }
        //-----------------------------------------------
        //---       HTTP GET (Common Use For QUERY)  ----
        //-----------------------------------------------

        //****** Parameter *******//
        [HttpGet("{id}")]   // .... /1
        //https://localhost:7142/Product/1
        public ActionResult GetProductById(int id)
        {

            return Ok(new {productId = id,name = "VueJs"});
        }


        [HttpGet("search/{id}/{category}")] // .... /1/web
        //https://localhost:7142/Product/search/1/web
        public ActionResult SearchProductById(int id,string category)
        {

            return Ok(new { productId = id, name = "VueJs",cat = category });
        }

        //****** Query String *******//

        [HttpGet("query/product")] // .... /?id=12,cat=Web
        //https://localhost:7142/Product/query/product?id=1&category=web
        public ActionResult QueryProductById([FromQuery] int id, [FromQuery] string category)
        {

            return Ok(new { productId = id, name = "VueJs", cat = category });
        }

        //****** Mixes Parameter & Query String *******//

        [HttpGet("queryv2/product/{user}")] // .... /game?id=12,cat=Web
        //https://localhost:7142/Product/queryv2/product/game?id=1&category=web
        public ActionResult QueryProductById([FromQuery] int id, [FromQuery] string category,String user)
        {

            return Ok(new { productId = id, name = "VueJs", cat = category ,user = user});
        }

        //-----------------------------------------------
        //---      HTTP  POST (Common Use For INSERT)----
        //-----------------------------------------------
        [HttpPost]
        public ActionResult<Product> AddProduct([FromBody] Product product) // FromBody and FromFrom
        {
            return Ok(product);
        }

        [HttpPost("add")]
        public ActionResult<Product> AddProductV2([FromBody] Product product) // FromBody and FromFrom
        {
            return Ok(product);
        }

        public class Product
        {
            public int id { get; set; }
            public string name { get; set; }
            public double price { get; set; }
        }


        //-----------------------------------------------
        //---      HTTP  PUT (Common Use For UPDATE) ----
        //-----------------------------------------------

        [HttpPut("{id}")]  // ../product/1111
        public ActionResult UpdateProductById(int id,[FromBody] Product product)
        {
            if(id != product.id)
            {
                return BadRequest();
            }
            if(id != 1111)
            {
                return NotFound();
            }

            return Ok(product);
        }

        //-----------------------------------------------
        //---      HTTP  DELETE (Common Use For DELETED) ----
        //-----------------------------------------------
        [HttpDelete("{id}")]
        public ActionResult DeleteById(int id) { 
            if(id !=1111){
                return NotFound();
            }
            return NoContent();
        }

    }
}
