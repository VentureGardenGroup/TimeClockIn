using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeClockIn.Models;

namespace TimeClockIn.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class EmployeeLocationDetailsRepository
    {
        private TimeClockInContext tcx = new TimeClockInContext();

        public EmployeeLocationDetails Get(int EmployeeClockInId)
        {
            return tcx.EmployeeLocationDetails.Single(eld => eld.EmployeeClockInId == EmployeeClockInId);
        }
    }
}