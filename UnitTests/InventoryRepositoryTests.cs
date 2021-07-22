using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;
using Xunit;
using DL;



namespace UnitTests
{
    class InventoryRepositoryTests
    {
        private readonly DbContextOptions<DBContext> _options;
        public InventoryRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<DBContext>().UseSqlite("Filename = Test.db").Options;
            this.Seed();
        }












        private void Seed()
        {
            using (var context = new DBContext(_options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }
    }
}
