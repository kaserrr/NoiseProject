using Microsoft.EntityFrameworkCore;
namespace ASP.NET_API.Models
{
    public class ContextModels : DbContext
    {
        public ContextModels(DbContextOptions<ContextModels> Options) 
            :base(Options)
        {
        }
        public DbSet<Models> SensorData { get; set; } = null!;

    }
}
