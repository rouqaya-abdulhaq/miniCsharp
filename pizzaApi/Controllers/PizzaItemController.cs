using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace pizzaApi.Controllers
{
    [ApiController]
    [Route("[CreatePizzaItem]")]
    public class PizzaItemController
    {

        private readonly ILogger<PizzaItemController> _logger;

        public PizzaItemController(ILogger<PizzaItemController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            return "loading the pizaa ingredients";
        }

        [HttpPost]
        public string Post()
        {
            return "submitting the pizza ingredients";
        }

    }
}
