using dEz.SkeletonCQRSES.Query.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dEz.SkeletonCQRSES.Query.Infrastructure.DataAccess
{
    public class DatabaseContextFactory
    {
        private readonly Action<DbContextOptionsBuilder> _optionsBuilder;

        public DatabaseContextFactory(Action<DbContextOptionsBuilder> optionsBuilder)
        {
            _optionsBuilder = optionsBuilder;
        }

        public DatabaseContext CreateDbContext()
        {
            DbContextOptionsBuilder<DatabaseContext> opts = new();
            _optionsBuilder(opts);

            return new DatabaseContext(opts.Options);
        }
    }
}
