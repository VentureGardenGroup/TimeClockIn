using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TimeClockIn.Models;
using TimeClockIn.Repository;

namespace TimeClockIn.Controllers
{
    /// <summary>
    /// The Location Endpoint allows for acess to the number of registered office locations in Nigeria
    /// </summary>
    public class LocationController : ApiController
    {
        private LocationRepository LR = new LocationRepository();

        //get all location details - populate web location dropdown with this - plus Home and Site options
        /// <summary>
        /// This returns a list of all REGISTERED office Locations
        /// </summary>
        /// <returns></returns>
        public List<Location> GetLocation()
        {
            return LR.Get();
        }

        //search location by id
        /// <summary>
        /// This returns the Location with the specific ID
        /// </summary>
        /// <param name="id">
        /// The ID parameter filters the search for a specific loaction
        /// </param>
        /// <returns></returns>
        public Location GetLocation(int id)
        {
            return LR.Get(id);
        }
        
        //search location by Name
        /// <summary>
        /// Search for location by the Location name
        /// </summary>
        /// <param name="LocationName">
        /// This parameter can represent the exact name of location
        /// or possible words to search for in all Locations
        /// </param>
        /// <returns>This returns a list of Locations are similar or exactly alike to the parameter specified</returns>
        public List<Location> GetLocation (string LocationName)
        {
            return LR.Get(LocationName);
        }

        //Add new location - only by super admin
        /// <summary>
        /// Add a new location
        /// </summary>
        /// <param name="Loc">
        /// This parameter represents the location object to be added.
        /// Expected to have ONLY  LocationName, Latitude, Longitude, Address
        /// </param>
        public void PostLocation(Location Loc)
        {
            //expecting Loc to have - LocationName, Latitude, Longitude, Address
            LR.Add(Loc);
        }

        //Update Location
        /// <summary>
        /// This is an update function which can be used to make changes to an existing location record
        /// </summary>
        /// <param name="id">This is the ID to the record being updated</param>
        /// <param name="Loc">This is the Location record with the updated attributes to be inserted</param>
        public void UpdateLocation(int id, Location Loc)
        {
            LR.Put(id, Loc);
        }
    }
}
