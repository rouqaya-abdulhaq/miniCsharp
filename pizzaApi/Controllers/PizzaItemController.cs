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
    public class PizzaItemController
    {

        private readonly ILogger<PizzaItemController> _logger;

        private readonly PizzaContext _pizzaContext;

        public PizzaItemController(ILogger<PizzaItemController> logger, PizzaContext pizzaContext)
        {
            _logger = logger;
            _pizzaContext = pizzaContext;
        }

        [HttpGet]
        public async  Task<ActionResult<IEnumerable<PizaaItem>>> GetPizzaItems()
        {
            return await _pizzaContext.PizaaItems.ToListAsync();
        }

        [HttpGet]
        public string Get()
        {
            return "loading the pizaa ingredients";
        }

        [HttpPost]
        public async Task<IActionResult>  Post(PizaaItem pizzaItem)
        {
            await _pizzaContext.PizaaItems.AddAsync(pizzaItem);
            await _pizzaContext.SaveChangesAsync();

            return CreatedAtActionResult("Get", new {id = pizzaItem.OrderNumber}, pizzaItem);
        }

    }
}
