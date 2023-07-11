using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaApi.Interfaces;
using PizzaApi.Models;
using PizzaApi.Models.DTO;
using PizzaApi.Services;

namespace PizzaApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class PizzaController : ControllerBase
    {
        private readonly PizzaServices _pizzaServices;

        public PizzaController(PizzaServices pizzaServices)
        {
            _pizzaServices = pizzaServices;
        }

        [AllowAnonymous]
        [HttpGet]

        public ActionResult<ICollection<Pizza>> Get()
        {
            IList<Pizza> pizzas = null;
            pizzas = _pizzaServices.GetAll().ToList();
            if (pizzas == null)
                return NotFound("No Pizzas available at this moment");
            return Ok(pizzas);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<ICollection<Pizza>> Filter([FromQuery]RangeDTO rangeDTO)
        {
            IList<Pizza> pizzas = null;
            pizzas = _pizzaServices.GetProduct(rangeDTO).ToList();
            if (pizzas == null)
                return NotFound("No Pizzas available in this price range");
            return Ok(pizzas);
        }


        [HttpPost]
        public ActionResult<Pizza> Add(Pizza pizza)
        {
            Pizza newPizza = _pizzaServices.Add(pizza);
            if (newPizza == null)
                return BadRequest("Unable to add Pizza");
            return Created("Pizza Added Successfully", newPizza);
        }
    }
}
