using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeClockIn.Models;

namespace TimeClockIn.Repository
{
    public class LocationRepository
    {
        private TimeClockInContext tcx = new TimeClockInContext();

        //get all location details - populate web location dropdown with this - plus Home and Site options
        public List<Location> Get()
        {
            List<Location> loc = tcx.Location.ToList();
            //check for null response before return
            return loc;
        }

        //search location by id
        public Location Get(int id)
        {
            Location loc = tcx.Location.Single(l => l.id == id);
            //check for null response before return
            return loc;
        }

        //search location by Name
        public List<Location> Get(string LocationName)
        {
            List<Location> loc = tcx.Location.Where(l => l.LocationName.Contains(LocationName)).ToList();
            //check for null or failed response
            return loc;
        }

        //Add new location - only by super admin
        public void Add(Location Loc)
        {
            //expecting Loc to have - LocationName, Latitude, Longitude, Address

            tcx.Location.Add(Loc);
            tcx.SaveChanges();
        }

        //Update Location
        public void Put(int id, Location Loc)
        {
            Location tempLoc = tcx.Location.Single(l => l.id == id);
            tempLoc.LocationName = Loc.LocationName;
            tempLoc.Latitude = Loc.Latitude;
            tempLoc.Longitude = Loc.Longitude;
            tempLoc.Address = Loc.Address;

            tcx.SaveChanges();
        }
    }
}