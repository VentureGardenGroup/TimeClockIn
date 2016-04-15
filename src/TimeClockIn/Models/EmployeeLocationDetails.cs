using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TimeClockIn.Models
{
    [Table("EmployeeLocationDetails")]
    public class EmployeeLocationDetails
    {
        /// <summary>
        /// Unique Identifier
        /// </summary>
        [Key]
        public int id { get; set; }

        /// <summary>
        /// This is a foreign link to the ClockIn event with the extra details
        /// </summary>
        
        public int EmployeeClockInId { get; set; }

        /// <summary>
        /// The Location name is the name of the Venue. e.g ("FAAN")
        /// </summary>
        public string LocationName { get; set; }

        /// <summary>
        /// Latitude Coordinate of the Clockin Location
        /// </summary>
        public double Latitude { get; set; } //formally used decimal datatype but it rounded to 2.d.p in database - double insters the full values

        /// <summary>
        /// Longitude Coordinate of the Clockin Location
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// The physical address of the venue. e.g ("Plot 3, X Close, Y Street, Lagos")
        /// </summary>
        public string Address { get; set; }
    }
}