using Microsoft.EntityFrameworkCore;

namespace ClientsAPI
{
    public class ClientsDbContext : DbContext
    {
        public ClientsDbContext(DbContextOptions<ClientsDbContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
    }
}
