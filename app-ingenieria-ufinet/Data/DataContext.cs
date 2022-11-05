using Microsoft.EntityFrameworkCore;

namespace app_ingenieria_ufinet.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }
}
