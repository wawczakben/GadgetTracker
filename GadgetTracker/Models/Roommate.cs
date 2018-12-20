using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GadgetTracker.Models
{
    public class Roommate
    {
        public int Id { get; set; }
        public string f_name { get; set; }
        public string l_name { get; set; }
        public int age { get; set; }

        public ICollection<Gadget> Gadgets { get; set; } = new List<Gadget>();
        public Household Household { get; set; }
    }
}
