using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TimeClockIn.Models;
using TimeClockIn.Repository;

namespace TimeClockIn.Controllers
{
    /// <summary>
    /// This Endpoint Gets details of External Location for a ClockIn Event
    /// </summary>
    public class LocationDetailsController : ApiController
    {
        private EmployeeLocationDetailsRepository ELDR = new EmployeeLocationDetailsRepository();
        /// <summary>
        /// When a user clocks in at an external location, extra location details are inserted.
        /// in order to recover the details, specify the EmployeeClockInId
        /// </summary>
        /// <param name="EmployeeClockInId">
        /// This parameter represents the ClockIn event that has extra details in EmployeeLocationDetails.
        /// The endpoint retrieves the location details to an external clock in event
        /// </param>
        /// <returns></returns>
        public EmployeeLocationDetails Get(int EmployeeClockInId)
        {
            return ELDR.Get(EmployeeClockInId);
        }
    }
}
