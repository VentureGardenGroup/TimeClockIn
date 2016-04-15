using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace TimeClockIn.Models
{
    public class TimeClockInContext : DbContext
    {
        //EF link to clockin database table
        public DbSet<EmployeeClockIn> EmployeeClockIn { get; set; }

        //EF link to location table
        public DbSet<Location> Location { get; set; }

        //EF link to clockin location details table
        public DbSet<EmployeeLocationDetails> EmployeeLocationDetails { get; set;}



    }
}