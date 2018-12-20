using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GadgetTracker.Models
{
    public class GadgetLocation
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Gadget> Gadgets { get; set; } = new List<Gadget>();
    }
}
