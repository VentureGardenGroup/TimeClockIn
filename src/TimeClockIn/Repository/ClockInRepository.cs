using Elmah;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Http;
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
               //log error - display why search couldn't happen
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling 
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
                tcx.EmployeeClockIn.Add(ClockInData);
                tcx.SaveChanges();

                ClockInDataId = ClockInData.id;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling 
                //log error - Post Error , EmployeeUSerId , details
            }

            return ClockInDataId;
        }

        public void Add(ClockInWithDetails ClockInData)
        {
            
                //trim data
                ClockInWithDetails CIW = new ClockInWithDetails();
                CIW.EmployeeClockIn = new EmployeeClockIn();
                CIW.EmployeeClockIn.EmployeeUserId = ClockInData.EmployeeClockIn.EmployeeUserId.Trim();
                CIW.EmployeeClockIn.LocationName = ClockInData.EmployeeClockIn.LocationName.Trim();
                CIW.EmployeeClockIn.Latitude = ClockInData.EmployeeClockIn.Latitude;
                CIW.EmployeeClockIn.Longitude = ClockInData.EmployeeClockIn.Longitude;

                //format date entering this method
                string sDate = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt");
                DateTime ClockInDates = DateTime.ParseExact(sDate, "yyyy-MM-dd hh:mm:ss tt", CultureInfo.InvariantCulture);
                CIW.EmployeeClockIn.ClockInDateTime = ClockInDates;

                if (ClockInData.EmployeeLocationDetails != null)
                {
                        CIW.EmployeeLocationDetails = new EmployeeLocationDetails();
                        CIW.EmployeeLocationDetails.LocationName = ClockInData.EmployeeLocationDetails.LocationName.Trim();
                        CIW.EmployeeLocationDetails.Address = ClockInData.EmployeeLocationDetails.Address.Trim();
                        CIW.EmployeeLocationDetails.Latitude = ClockInData.EmployeeLocationDetails.Latitude;
                        CIW.EmployeeLocationDetails.Longitude = ClockInData.EmployeeLocationDetails.Longitude;

                        //check if clock-in has been done already
                        if (!isPosted(CIW))
                        {
                            CIW.EmployeeLocationDetails.EmployeeClockInId = Add(CIW.EmployeeClockIn); //post the clockin details in EmployeeClockIn; and the details here
                            tcx.EmployeeLocationDetails.Add(CIW.EmployeeLocationDetails);
                            tcx.SaveChanges();
                        }
                        else {
                                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotAcceptable)
                                {
                                    Content = new StringContent("Clock-In Event already exists for this location"),
                                    ReasonPhrase = "You have Already Clocked-In to " + CIW.EmployeeClockIn.LocationName + " - " + CIW.EmployeeLocationDetails.LocationName + " today."
                                });
                             }
                }
                else //ClockInData == null
                {
                    //check for proximity between user location and vgg location
                    if (isLocated(CIW.EmployeeClockIn.LocationName, CIW.EmployeeClockIn.Latitude, CIW.EmployeeClockIn.Longitude))
                    {
                        if (!isPosted(CIW))
                        {
                            Add(CIW.EmployeeClockIn);
                        }
                        else
                        {
                                HttpResponseException ex = new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotAcceptable)
                                {
                                    Content = new StringContent("Clock-In Event already exists for this location"),
                                    ReasonPhrase = "You have Already Clocked-In to " + CIW.EmployeeClockIn.LocationName + " today."
                                });
                                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling 
                                throw ex;
                         } //end else isPosted = true
                    }
                    else
                    {   HttpResponseException ex = new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotAcceptable)
                            {
                            Content = new StringContent("Location does not Match"),
                            ReasonPhrase = "Location Conflict : You are not around " + CIW.EmployeeClockIn.LocationName
                            });
                        ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling 
                        throw ex;
                        
                    } //end else isLocated = false

                }//end else ClockInData == null

        }


        //this function checks if clock-in has been made already before every new clock-in event
        private bool isPosted(ClockInWithDetails ClockInData)
        {
            int x = 0, y = 0;
            try
            {
                x = tcx.EmployeeClockIn.Where(ci => ci.EmployeeUserId == ClockInData.EmployeeClockIn.EmployeeUserId
                                                 & ci.LocationName == ClockInData.EmployeeClockIn.LocationName
                                                 & ci.ClockInDateTime.Year == ClockInData.EmployeeClockIn.ClockInDateTime.Year
                                                 & ci.ClockInDateTime.Month == ClockInData.EmployeeClockIn.ClockInDateTime.Month
                                                 & ci.ClockInDateTime.Day == ClockInData.EmployeeClockIn.ClockInDateTime.Day).Count();
                if (ClockInData.EmployeeLocationDetails != null)
                {
                    y = tcx.EmployeeLocationDetails.Where(eld => eld.LocationName == ClockInData.EmployeeLocationDetails.LocationName
                                                          & eld.Address == ClockInData.EmployeeLocationDetails.Address).Count();
                    x = y;
                }


            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling 
            }

            return x < 1 ? false : true;
        }


        //this function compares user's current location to the selected location before clockin
        //this feature is only available for VGG location clock-in
        private bool isLocated(string LocationName, double lati, double longi)
        {
            Location loc;
            double distance = 0, KM = 0.1; //KM is the defined and accepted distance in kilometers from ccentral vgg location

            try
            {
                loc = tcx.Location.Single(l => l.LocationName == LocationName);
                if (loc != null)
                { distance = LatLongDistance(loc.Latitude, loc.Longitude, lati, longi); }
                return (distance <= KM) ? true : false;
            }
            catch (HttpResponseException ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                return false;
            }
            
        }

        //calculate the distance between user's location and vgg location
        private double LatLongDistance(double centerLat, double centerLong, double posLat, double posLong)
        {
            var ky = 40000 / 360;
            var kx = Math.Cos(Math.PI * centerLat / 180.0) * ky;
            var dx = Math.Abs(centerLong - posLong) * kx;
            var dy = Math.Abs(centerLat - posLat) * ky;
            return Math.Sqrt(dx * dx + dy * dy);
        }

    

    }
}