using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bingol.Data
{
    public class BingolContext : DbContext
    {
        public BingolContext(DbContextOptions<BingolContext> options): base(options)
        { }
    }
}
