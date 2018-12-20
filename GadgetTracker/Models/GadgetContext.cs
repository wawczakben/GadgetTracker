using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GadgetTracker.Models
{
    public class GadgetContext : DbContext
    {
        public DbSet<Gadget> Gadgets { get; set; }
        public DbSet<GadgetLocation> GadgetLocations { get; set; }
        public DbSet<Household> Households { get; set; }
        public DbSet<Roommate> Roommates { get; set; }

        public GadgetContext(DbContextOptions<GadgetContext> options)
       : base(options)
        {
            //create but leave empty to call the base class constructor
        }
    }
}
