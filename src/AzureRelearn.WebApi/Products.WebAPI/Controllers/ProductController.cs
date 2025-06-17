using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Products.WebAPI.Controllers
{
    public class ProductController :ControllerBase
    {
        private string[] productMaster = new[] { "Washing Soap", "Sugar", "Rice Bran Oil" , "Vim Soap Bar"
            ,"Salt", "Tea Powder" , "Jaggery" , "Bathing Soap" , "Rice", "Wheat Floor", "Red Chilli Powder",
            "Rice flakes" , "Corriander" , "Green Chilli", "Ginger", "Onion", "Tomato", "Lemon", "Potato",
            "Amul Butter", "Curd","Buffalo Milk", "Cow Ghee" , "Govardhan Cow Ghee", "Brown Bread", "Lady Fingers",
            "Cumin Seeds", "Cabbage", "Cheese Cube"};
        public ProductController() { }

        [HttpGet("All", Name ="GetAllProducts")]
        public IActionResult GetProducts()
        {
            return Ok(productMaster);
        }
         
    }
}
