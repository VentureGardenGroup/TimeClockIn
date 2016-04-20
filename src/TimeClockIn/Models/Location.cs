using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TimeClockIn.Models
{
    /// <summary>
    /// This table keeps recored of all registered office addresses.
    /// This should be used to populate the ClockIn Location Menu
    /// </summary>
    [Table("Location")]
    public class Location
    {
        /// <summary>
        /// Unique ID
        /// </summary>
        [Key]
        public int id { get; set; }

        /// <summary>
        /// The name given to the registered office location. e.g ("VGG Oregun")
        /// </summary>
        public string LocationName { get; set; }

        /// <summary>
        /// Latitude coordinate of the location
        /// </summary>
        public decimal Latitude { get; set; }

        /// <summary>
        /// Longitude coordinate of the location
        /// </summary>
        public decimal Longitude { get; set; }

        /// <summary>
        /// Physical Address of the registered location. e.g ("17B Y Street, Z, Nigeria")
        /// </summary>
        public string Address { get; set; }
    }
}