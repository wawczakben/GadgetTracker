using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GadgetTracker.Models
{
    public class Household
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public ICollection<Roommate> Roommates { get; set; } = new List<Roommate>();
    }
}
