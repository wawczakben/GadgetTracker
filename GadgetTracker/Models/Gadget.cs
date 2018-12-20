using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GadgetTracker.Models
{
    public class Gadget
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public GadgetLocation GadgetLocation { get; set; }
        public Roommate Roommate { get; set; }
    }
}
