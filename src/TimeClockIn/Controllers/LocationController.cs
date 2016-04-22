using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
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
        /// 
        [HttpGet]
        [ResponseType(typeof(Location))]
        public HttpResponseMessage GetLocation()
        {
            try
            {
                List<Location> locC = LR.Get();
                if (locC != null)
                {
                    return Request.CreateResponse<List<Location>>(HttpStatusCode.OK, locC);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Null Returned - Error retrieving Locations");
                }
            }
            catch (Exception ex)
            {
                // Log exception code goes here 
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling  
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Error retrieving Locations");

            }
        }
        /*
        public List<Location> GetLocation()
        {
            return LR.Get();
        }*/

        //search location by id
        /// <summary>
        /// This returns the Location with the specific ID
        /// </summary>
        /// <param name="id">
        /// The ID parameter filters the search for a specific loaction
        /// </param>
        /// <returns></returns>
        /// 
        [HttpGet]
        [ResponseType(typeof(Location))]
        public HttpResponseMessage GetLocation(int id)
        {
            try
            {
                Location locC = LR.Get(id);
                if (locC != null)
                {
                    return Request.CreateResponse<Location>(HttpStatusCode.OK, locC);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Null Returned - Error retrieving Location with ID " + id);
                }
            }
            catch (Exception ex)
            {
                // Log exception code goes here 
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling 
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Error retrieving Location with ID " + id);

            }
        }
       /* public Location GetLocation(int id)
        {
            return LR.Get(id);
        }*/

        //search location by Name
        /// <summary>
        /// Search for location by the Location name
        /// </summary>
        /// <param name="LocationName">
        /// This parameter can represent the exact name of location
        /// or possible words to search for in all Locations
        /// </param>
        /// <returns>This returns a list of Locations are similar or exactly alike to the parameter specified</returns>
        [HttpGet]
        [ResponseType(typeof(Location))]
        public HttpResponseMessage GetLocation(string LocationName)
        {
            try
            {

                List<Location> locC = LR.Get(LocationName.Trim());
                if (locC != null)
                {
                    return Request.CreateResponse<List<Location>>(HttpStatusCode.OK, locC);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Null Returned - Error retrieving Location(s) with name : " + LocationName);
                }
            }
            catch (Exception ex)
            {
                // Log exception code goes here 
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling 
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Error retrieving Location(s) with name: "+LocationName);

            }
        }
        /*
        public List<Location> GetLocation (string LocationName)
        {
            return LR.Get(LocationName);
        }
        */
        //Add new location - only by super admin
        /// <summary>
        /// Add a new location
        /// </summary>
        /// <param name="Loc">
        /// This parameter represents the location object to be added.
        /// Expected to have ONLY  LocationName, Latitude, Longitude, Address
        /// </param>
        /// 
        [HttpPost]
        public HttpResponseMessage PostLocation(Location Loc)
        {

            try
            {
                //expecting Loc to have - LocationName, Latitude, Longitude, Address
                Location newLoc = new Location();
                newLoc.Latitude = Loc.Latitude;
                newLoc.Longitude = Loc.Longitude;
                newLoc.LocationName = Loc.LocationName.Trim();
                newLoc.Address = Loc.Address.Trim();
                LR.Add(newLoc);
                var response = Request.CreateResponse(HttpStatusCode.Created, Loc);
                return response;
            }
            catch (Exception ex)
            {
                // Log exception code goes here 
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling 
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "Location could not be added");

            }
        }
        /*
        public void PostLocation(Location Loc)
        {
            //expecting Loc to have - LocationName, Latitude, Longitude, Address
            LR.Add(Loc);
        }*/

        //Update Location
        /// <summary>
        /// This is an update function which can be used to make changes to an existing location record
        /// </summary>
        /// <param name="id">This is the ID to the record being updated</param>
        /// <param name="Loc">This is the Location record with the updated attributes to be inserted</param>
        [HttpPut]
        public HttpResponseMessage UpdateLocation(int id, Location Loc)
        {

            try
            {
                //expecting Loc to have - LocationName, Latitude, Longitude, Address
                LR.Put(id, Loc);
                var response = Request.CreateResponse(HttpStatusCode.Created, Loc);
                return response;
            }
            catch (Exception ex)
            {
                // Log exception code goes here 
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling 
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "Location with ID " + id + " could not be Updated");

            }
        }
   
    }
}
