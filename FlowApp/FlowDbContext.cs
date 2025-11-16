using FlowApp.Controllers;
using Microsoft.EntityFrameworkCore;

namespace FlowApp.Data
{
    public class FlowDbContext : DbContext
    {
        public FlowDbContext(DbContextOptions<FlowDbContext> options)
            : base(options) { }

        public DbSet<FlowNode> Nodes => Set<FlowNode>();
    }
}
