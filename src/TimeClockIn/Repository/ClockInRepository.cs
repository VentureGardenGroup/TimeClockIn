using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using TimeClockIn.Models;

namespace TimeClockIn.Repository
{
    public class ClockInRepository
    {

        private TimeClockInContext tcx = new TimeClockInContext();

        //get all clockin details
        private IQueryable<EmployeeClockIn> Get()
        {
            var clockin = tcx.EmployeeClockIn;
            return clockin;
            //log error if any
        }

        //get Clockin details for one employee id

        public EmployeeClockIn Get(int id)
        {
            EmployeeClockIn empC = tcx.EmployeeClockIn.Single(e => e.id == id);
            //check for null return
            // if (empC == null) { throw new HttpResponseException(HttpStatusCode.BadRequest); }
            return empC;
        }

        //get clockin details based on several seach parameters
        public List<EmployeeClockIn> Get(string EmployeeUserId = "", string LocationName = "", DateTime? FromDateTime = null, DateTime? ToDateTime = null)
        {

            IQueryable<EmployeeClockIn> ClockInQuery = tcx.EmployeeClockIn;
            List<EmployeeClockIn> EmpClockIn = new List<EmployeeClockIn>();

            try
            {
                //check if employeeclockinid is set
                if (EmployeeUserId != string.Empty)
                {
                    ClockInQuery = ClockInQuery.Where(e => e.EmployeeUserId == EmployeeUserId);
                }

                //check if location is set
                if (LocationName != string.Empty)
                {
                    ClockInQuery = ClockInQuery.Where(e => e.LocationName.Contains(LocationName));
                }

                //check if fromdate AND todate are available
                //else check for fromdate OR todate

                //convert the dates //null datetimee require .Value.ToString to convert date format
                string fdt, tdt;
                DateTime LastMinute;
                if ((FromDateTime != null) & (ToDateTime != null))
                {
                    //convert date format, search between dates from date1 00:00 - date 2 11:59
                    fdt = FromDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss tt");
                    tdt = ToDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss tt");
                    FromDateTime = DateTime.ParseExact(fdt, "yyyy-MM-dd hh:mm:ss tt", CultureInfo.InvariantCulture);
                    ToDateTime = DateTime.ParseExact(tdt, "yyyy-MM-dd hh:mm:ss tt", CultureInfo.InvariantCulture);
                    LastMinute = ToDateTime.Value.AddHours(23).AddMinutes(59).AddSeconds(29);
                    ClockInQuery = ClockInQuery.Where(e => e.ClockInDateTime <= LastMinute & e.ClockInDateTime >= FromDateTime);
                }
                else if (FromDateTime != null)
                {
                    //convert date format, search date from 00:00 - 11:59
                    fdt = FromDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss tt");
                    FromDateTime = DateTime.ParseExact(fdt, "yyyy-MM-dd hh:mm:ss tt", CultureInfo.InvariantCulture);
                    LastMinute = FromDateTime.Value.AddHours(23).AddMinutes(59).AddSeconds(29);
                    ClockInQuery = ClockInQuery.Where(e => e.ClockInDateTime <= LastMinute & e.ClockInDateTime >= FromDateTime);
                }
                else if (ToDateTime != null)
                {
                    //convert date format, search date from 00:00 - 11:59 
                    tdt = ToDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss tt");
                    ToDateTime = DateTime.ParseExact(tdt, "yyyy-MM-dd hh:mm:ss tt", CultureInfo.InvariantCulture);
                    LastMinute = ToDateTime.Value.AddHours(23).AddMinutes(59).AddSeconds(29);
                    ClockInQuery = ClockInQuery.Where(e => e.ClockInDateTime <= LastMinute & e.ClockInDateTime >= ToDateTime);
                }

                //if nothing is set, return all data
                if ((EmployeeUserId == string.Empty) & (LocationName == string.Empty) & (FromDateTime == null) & (ToDateTime == null))
                {
                    ClockInQuery = Get();
                }


            }
            catch (Exception ex)
            {
                string x = ex.ToString();
                //log error - display why search couldn't happen
            }

            EmpClockIn = ClockInQuery.ToList();
            return EmpClockIn;
        }

        //add clockin event here 
        //for vgg offices, there is a table for address and coordinates
        //for Site and Home location, allow user to specify Address
        private int Add(EmployeeClockIn ClockInData)
        {
            int ClockInDataId = 0;

            try
            {
                /* previously assigned to a new variable - not needed
                 * EmployeeClockIn ECI = new EmployeeClockIn();
                 ECI.EmployeeUserId = ClockInData.EmployeeUserId;
                 ECI.LocationName = ClockInData.LocationName; */

                //format date entering this method
                string sDate = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt");
                DateTime ClockInDates = DateTime.ParseExact(sDate, "yyyy-MM-dd hh:mm:ss tt", CultureInfo.InvariantCulture);

                //ECI.ClockInDateTime = ClockInDates;
                ClockInData.ClockInDateTime = ClockInDates;

                tcx.EmployeeClockIn.Add(ClockInData);
                tcx.SaveChanges();

                //tcx.Entry(ECI).Reload(); - no need to reload, context is auto updated

                ClockInDataId = ClockInData.id;
            }
            catch (Exception ex)
            {
                String x = ex.ToString();
                //log error - Post Error , EmployeeUSerId , details
            }

            return ClockInDataId;
        }

        public void Add(ClockInWithDetails ClockInData)
        {

            try
            {
                //trim data
                ClockInWithDetails CIW = new ClockInWithDetails();
                CIW.EmployeeClockIn = new EmployeeClockIn();
                CIW.EmployeeClockIn.EmployeeUserId = ClockInData.EmployeeClockIn.EmployeeUserId.Trim();
                CIW.EmployeeClockIn.LocationName = ClockInData.EmployeeClockIn.LocationName.Trim();

               

                if (ClockInData.EmployeeLocationDetails != null)
                {
                    if ((ClockInData.EmployeeClockIn.LocationName.Equals("Home")) || (ClockInData.EmployeeClockIn.LocationName.Equals("Site")))
                    {
                        CIW.EmployeeLocationDetails = new EmployeeLocationDetails();
                        CIW.EmployeeLocationDetails.LocationName = ClockInData.EmployeeLocationDetails.LocationName.Trim();
                        CIW.EmployeeLocationDetails.Address = ClockInData.EmployeeLocationDetails.Address.Trim();
                        CIW.EmployeeLocationDetails.Latitude = ClockInData.EmployeeLocationDetails.Latitude;
                        CIW.EmployeeLocationDetails.Longitude = ClockInData.EmployeeLocationDetails.Longitude;

                        CIW.EmployeeLocationDetails.EmployeeClockInId = Add(CIW.EmployeeClockIn); //post the clockin details in EmployeeClockIn; and the details here
                        tcx.EmployeeLocationDetails.Add(CIW.EmployeeLocationDetails);
                        tcx.SaveChanges();
                    }
                    else {
                        //what ever happens, if you are not at home or site, add a normal clock in event in vgg location
                        Add(CIW.EmployeeClockIn);
                    }
                }
                else
                {
                    Add(CIW.EmployeeClockIn);
                }
            }
            catch (Exception ex)
            {
                string x = ex.ToString();
                //log error - display error in user friendly way
            }
        }

    }
}