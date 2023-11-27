using Microsoft.EntityFrameworkCore;
using static ASP.NET_API.Models.Models;

namespace ASP.NET_API.Models;

    public class ContextUplinkData : DbContext
{
    public ContextUplinkData(DbContextOptions<ContextUplinkData> options)
        : base(options)
    {
    }
    public DbSet<UplinkData> UplinkData { get; set; } = null!;
}
