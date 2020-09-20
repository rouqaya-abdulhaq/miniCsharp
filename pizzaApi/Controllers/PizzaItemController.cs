using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pizzaApi.Models;

namespace pizzaApi.Controllers
{
    [ApiController]
    [Route("[CreatePizzaItem]")]
    public class PizzaItemController : ControllerBase
    {

        private readonly ILogger<PizzaItemController> _logger;

        private readonly PizzaContext _pizzaContext;

        public PizzaItemController(ILogger<PizzaItemController> logger, PizzaContext pizzaContext)
        {
            _logger = logger;
            _pizzaContext = pizzaContext;
        }

        [HttpGet]
        public IActionResult GetPizzaItems()
        {
            var pizzas =  _pizzaContext.PizaaItems.ToList();
            if(pizzas.Count == 0)
            {
                return NotFound();
            }
            return Ok(pizzas);
        }

        [HttpGet]
        public IActionResult GetPizzaItem(long orderNumber)
        {
            var pizzaOrders = from p in _pizzaContext.PizaaItems
                                    select p;

            var pizzaOrder = pizzaOrders.Where(p => p.OrderNumber == orderNumber);

            if(pizzaOrder == null)
            {
                return NotFound();
            }
            return Ok(pizzaOrder);
        }

        [HttpPost]
        public async Task<IActionResult>  Post(PizaaItem pizzaItem)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }
            await _pizzaContext.PizaaItems.AddAsync(pizzaItem);
            await _pizzaContext.SaveChangesAsync();

            return Ok();
        }

    }
}
