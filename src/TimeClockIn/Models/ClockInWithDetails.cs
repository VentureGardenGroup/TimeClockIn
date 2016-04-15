using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeClockIn.Models
{
    /// <summary>
    /// This is a Complex Model used to Add a new ClockInEvent.<br/>
    /// The EmployeeClockIn Object is required in order to add a new ClockIn Event.<br/>
    /// The EmployeeLocationDetails is optional only when a ClockIn is made from registered Locations.
    /// It is needed when EmployeeClockIn.LocationName is either "HOME" or "SITE" or not any of the registered locations
    /// </summary>
    public class ClockInWithDetails
    {
        private int id { get; set; }
        /// <summary>
        /// Supply EmployeeUserId and LocationName here<br/>
        /// Set EmployeeLocationDetails to null
        /// </summary>
        [Required]
        public EmployeeClockIn EmployeeClockIn { get; set; }
        /// <summary>
        /// Populate this object only when the ClockIn location is not a registered address<br/>
        /// Supply LocationName (This is not the same as the one in EmployeeClockIn, 
        /// it represents the name of the new location. e.g a client's office - "The View Hotel" ), 
        /// Address and Location Coordinates (Latitude and Longitude)
        /// </summary>
        public EmployeeLocationDetails EmployeeLocationDetails { get; set; }
    }
}