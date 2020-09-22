using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pizzaApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePizzaItem(long orderNumber)
        {
            if(orderNumber <=0)
            {
                return BadRequest("Invalid order number");
            }

            var pizzaOrders = from p in _pizzaContext.PizaaItems
                                    select p;

            var pizzaOrder = pizzaOrders.Where(p => p.OrderNumber == orderNumber);

            if(pizzaOrder == null)
            {
                return NotFound("the order number does not match with any of the avaliable orders");
            }

            _pizzaContext.Entry(pizzaOrder).State = EntityState.Deleted;
            await _pizzaContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> EditPizzaItem(long orderNumber, PizaaItem pizaaItem)
        {
            if(orderNumber != pizaaItem.OrderNumber)
            {
                return BadRequest("the order number provided doesn't match the pizza's order number");
            }

            _pizzaContext.Entry(pizaaItem).State = EntityState.Modified;

            try
            {
                await _pizzaContext.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!PizzaItemExists(orderNumber))
                {
                    return NotFound("the provided order number doesn't match with any of the orders");
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        private bool PizzaItemExists(long orderNumber)
        {
            return _pizzaContext.PizaaItems.Any(e => e.OrderNumber == orderNumber);
        }
    }
}
