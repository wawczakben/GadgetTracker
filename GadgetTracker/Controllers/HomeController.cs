using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GadgetTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace GadgetTracker.Controllers
{
    public class HomeController : Controller
    {
        private GadgetContext context;

        public HomeController(GadgetContext c)
        {
            context = c;
        }

        public IActionResult Index(int houseid)
        {
            
            var AllGadgets = context.Gadgets
                .Include(g => g.GadgetLocation)
                .Include(g => g.Roommate)
                    .ThenInclude(h => h.Household)
                .ToList();
            
            var AllHouseholds = context.Households
                //.Include(h => h.Id)
                //.Include(h => h.Roommates)
                //.Include(h => h.Name)
                .ToList();

            List < Gadget > GadgetsID = new List<Gadget>(); 
            if(houseid > 0)
            {
                GadgetsID = context.Gadgets
                 // .Include(g => g.Name)
                 //.Include(g => g.Roommate)
                 .Where(g => g.Roommate.Household.Id == houseid)
                 .ToList();
            }
            

            ViewData["GadgetsID"] = GadgetsID;
            ViewData["AllGadgets"] = AllGadgets;
            ViewData["AllHouseholds"] = AllHouseholds;
            
            return View();
        }

        [HttpPost]
        public IActionResult HandleViewHousehold(int householdId)
        {
            return RedirectToAction("Index", new { houseid = householdId});
        }

        public IActionResult AddRoommate()
        {
            var allHouseholds = context.Households.Include(household => household.Roommates).ToList();
            return View(allHouseholds);
        }

        [HttpPost]
        public IActionResult HandleNewRoommate(int hh, string fName, string lName, int a)
        {
            Household existingHousehold = context.Households.Where(house => house.Id == hh).Single();
            Roommate newRoommate = new Roommate() { f_name = fName, l_name = lName, age = a, Household = existingHousehold};
            context.Roommates.Add(newRoommate);
            context.SaveChanges();

            return RedirectToAction("AddRoommate");
        }

        public IActionResult UpdateLocation()
        {
            List<Object> allGadgets = new List<object>();

            var allRoommates = context.Roommates.Include(r => r.Gadgets).ToList();
            foreach (Roommate r in allRoommates)
            {
                foreach (Gadget g in r.Gadgets)
                {
                    var gadgetInfo = new { Name = $"{g.Name} owned by {r.f_name}  {r.l_name}", gadgetId = g.Id };
                    allGadgets.Add(gadgetInfo);
                }
            }

            ViewData["allGadgets"] = allGadgets;
            ViewData["locations"] = context.GadgetLocations.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult HandleUpdateLocation(int gadgetID, int locationID)
        {
            GadgetLocation gadgetLocation = context.GadgetLocations.Where(gl => gl.Id == locationID).Single();
            //Roommate roommate = context.Roommates.Where(r => r.Id == roommateID).Single();

            //Gadget newGadget = new Gadget() { Name = n, Type = t, Roommate = roommate, GadgetLocation = gadgetLocation };
            Gadget gadget = context.Gadgets.Where(g => g.Id == gadgetID).Single();
            gadget.GadgetLocation = gadgetLocation;
            //context.Gadgets.Update(gadget);


            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult AddHousehold()
        {
            return View(context.Households.ToList());
        }

        [HttpPost]
        public IActionResult HandleNewHousehold(string n, string a)
        {
            Household newHousehold = new Household() { Name = n, Address = a };
            context.Households.Add(newHousehold);
            context.SaveChanges();

            return RedirectToAction("AddHousehold");

        }

        public IActionResult AddGadgetLocation()
        {
            return View(context.GadgetLocations.ToList());
        }

        [HttpPost]
        public IActionResult HandleNewGadgetLocation(string n)
        {
            GadgetLocation ngl = new GadgetLocation() { Name = n };
            context.GadgetLocations.Add(ngl);
            context.SaveChanges();

            return RedirectToAction("AddGadgetLocation");
        }

        public IActionResult AddGadget()
        {
            List<Object> roommateNames = new List<object>();

            var allHouseholds = context.Households.Include(house => house.Roommates).ToList();
            foreach (Household household in allHouseholds)
            {
                foreach (Roommate roommate in household.Roommates)
                {
                    var roommateInfo = new { fName = $"{roommate.f_name} in {household.Name}", roommateId = roommate.Id };
                    roommateNames.Add(roommateInfo);
                }
            }

            ViewData["roommateNames"] = roommateNames;
            ViewData["locations"] = context.GadgetLocations.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult HandleNewGadget(string n, string t, int roommateID, int locationID)
        {
            GadgetLocation gadgetLocation = context.GadgetLocations.Where(gl => gl.Id == locationID).Single();
            Roommate roommate = context.Roommates.Where(r => r.Id == roommateID).Single();

            Gadget newGadget = new Gadget() { Name = n, Type = t, Roommate = roommate, GadgetLocation = gadgetLocation };
            context.Gadgets.Add(newGadget);
            context.SaveChanges();

            return RedirectToAction("AddGadget");
        }

        public IActionResult DeleteGadget()
        {
            List<Object> allGadgets = new List<object>();

            var allRoommates = context.Roommates.Include(r => r.Gadgets).ToList();
            foreach (Roommate r in allRoommates)
            {
                foreach (Gadget g in r.Gadgets)
                {
                    var gadgetInfo = new { Name = $"{g.Name} owned by {r.f_name}  {r.l_name}", gadgetId = g.Id };
                    allGadgets.Add(gadgetInfo);
                }
            }

            ViewData["allGadgets"] = allGadgets;

            return View();
        }

        [HttpPost]
        public IActionResult HandleDeleteGadget(int gadgetId)
        {

            Gadget gadget = context.Gadgets.Where(g => g.Id == gadgetId).Single();
            context.Gadgets.Remove(gadget);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}