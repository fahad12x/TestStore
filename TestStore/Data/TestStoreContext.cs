using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestStore.Models;

namespace TestStore.Data
{
    public class TestStoreContext : DbContext
    {
        public TestStoreContext (DbContextOptions<TestStoreContext> options)
            : base(options)
        {
        }
        public DbSet<TestStore.Models.usersaccounts> usersaccounts { get; set; } = default!;
        public DbSet<TestStore.Models.orderline> orderline { get; set; } = default!;

        public DbSet<TestStore.Models.orders> orders { get; set; } = default!;

        public DbSet<TestStore.Models.items> items { get; set; } = default!;
    }
}
