﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TimeClockIn.Models
{
    [Table("EmployeeClockIn")]
    public class EmployeeClockIn
    {
        /// <summary>
        /// Clock In ID
        /// </summary>
        [Key][Required]
        public int id { get; set; }
        /// <summary>
        /// User Id => Email Address
        /// </summary>
        [Required]
        public string EmployeeUserId { get; set; }
        /// <summary>
        /// Name of Office Location 
        /// </summary>
        [Required]
        public string LocationName { get; set; }
        /// <summary>
        /// ClockIn Date and Time
        /// </summary>
        [Required]
        public DateTime ClockInDateTime { get; set; }
    }
}