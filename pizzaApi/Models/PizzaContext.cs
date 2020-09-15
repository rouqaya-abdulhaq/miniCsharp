using Microsoft.EntityFrameworkCore;

namespace pizzaApi.Models
{
    public class PizzaContext : DbContext
    {
        public PizzaContext(DbContextOptions<PizzaContext> options)
            : base(options)
        {
        }

        public DbSet<PizaaItem> PizaaItems { get; set; }
    }
}
