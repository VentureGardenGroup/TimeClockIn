using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TimeClockIn.Models;
using TimeClockIn.Repository;

namespace TimeClockIn.Controllers
{
   ///<summary>
   ///The ClockIn Endpoint allows for inserting new ClockIn events and als access into the registered ClockIn Events<br/>
   ///Results can be filtered based on the parameters supplied.
   /// </summary> 
    public class ClockInController : ApiController
    {
        private ClockInRepository CIR = new ClockInRepository();

        //get Clockin details for one employee id
        /// <summary>
        /// This endpoint returns the ClockIn event with the specified by "id"
        /// </summary>
        /// 
        /// <param name="id">This is a Key value used
        /// to carry out a database search with a specefic ID
        /// </param>
        [HttpGet]
        [ResponseType(typeof(EmployeeClockIn))]
        public HttpResponseMessage GetClockIn(int id)
        {
            try
            {
                EmployeeClockIn empC = CIR.Get(id);
                if (empC != null)
                {
                    return Request.CreateResponse<EmployeeClockIn>(HttpStatusCode.OK, empC);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, " Clock-In Event with ID = '"+id+"' not found");
                } 
            }
            catch (Exception ex)
            {
                // Log exception code goes here  
               // return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Error occured while executing GetClockIn(id) ---"+ex.ToString());
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, " Clock-In Event with ID = '" + id + "' not found");

            }
        }

        /* public EmployeeClockIn GetClockIn(int id)
       {
          return CIR.Get(id);
       }*/


        //get clockin details based on several seach parameters
        //get all clockin details
        /// <summary>
        /// This is a function that helps to perform dynamic search on the 
        /// on the ClockIn table using various parameters.<br/>
        /// When <b>ALL</b> parameters are specified, the result generated is filtered based on the specified values.<br/> 
        /// When <b>NO</b> parameter is specified, It returns ALL clockin events. <br/>
        /// When <b>ONE</b> of two dates are specified, the results returned are filtered by ONLY the specified date.<br/>
        /// When <b>TWO DATES</b> are specified, the ClockIn results returned are BETWEEN the two dates.<br/>
        /// For a User's specific ClockIn History, this parameter only requires the <c>EmployeeUserId</c> parameter.
        /// </summary>
        /// 
        /// <param name="EmployeeUserId">This is an OPTIONAL parameter <b>(representing employee email address)</b>.<br/> 
        /// When specified, it checks for all clockin events with the specified EmployeeUserId.
        /// </param>
        /// <param name="LocationName">This is an OPTIONAL parameter <b>(representing VGG office locations)</b>. 
        /// When specified, it checks for all clockin events with the specified 
        /// LocationName.
        /// </param>
        /// <param name="FromDateTime">This is an OPTIONAL parameter which when specified <b>ALONE</b> returns
        /// ClockIn Events on this particular date. When used with ToDateTime, It must be less than.
        /// </param>
        ///  <param name="ToDateTime">This is an OPTIONAL parameter which when specified ALONE returns
        /// ClockIn Events on this particular date. when used with FromDateTime, It must be greater than 
        /// </param>
        /// <returns>This returns a list of clockin events based of the specified filters</returns>
        /// 
        [HttpGet]
        [ResponseType(typeof(List<EmployeeClockIn>))]
        public HttpResponseMessage GetClockIn(string EmployeeUserId = "", string LocationName = "", DateTime? FromDateTime = null, DateTime? ToDateTime = null)
        {
            try
            {
                //trim string attributes with .Trim()

               List<EmployeeClockIn> empC = CIR.Get(EmployeeUserId.Trim(), LocationName.Trim(), FromDateTime, ToDateTime);
                if (!String.IsNullOrEmpty(empC.ToString()))
                {
                    return Request.CreateResponse<List<EmployeeClockIn>>(HttpStatusCode.OK, empC);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, " Record Not Found - Check your search parameter(s)");
                }
            }
            catch (Exception ex)
            {
                // Log exception code goes here  
                // return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Error occured while executing GetClockIn(id) ---"+ex.ToString());
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Record Not Found - Check your search parameter(s)");

            }
        }

        //add clockin event here 
        //for vgg offices, there is a table for address and coordinates
        //for Site and Home location, allow user to specify Address and co-ordinates
        /// <summary>
        /// This endpoint adds a ClockIn event. It requires a complex parameter 
        /// representing the ClockIn parameters and Extra Details when required.
        /// </summary>
        /// <param name="ClockInData">
        /// This requires only <c>EmployeeClockIn</c> parameter.<br/> when the clockin location is a registered location
        /// Optional parameter, <c>EmployeeLocationDetails</c> is supplied based on 
        /// ClockIn Locations outside the registered office Locations i.e Home or Site
        /// </param>
        /// 
        [HttpPost]
        [ResponseType(typeof(EmployeeClockIn))]
        public HttpResponseMessage PostClockIn(ClockInWithDetails ClockInData)
        {
            
            try
            {
                //trim data
                ClockInWithDetails CIW = new ClockInWithDetails();
                CIW.EmployeeClockIn.EmployeeUserId = ClockInData.EmployeeClockIn.EmployeeUserId.Trim();
                CIW.EmployeeClockIn.LocationName = ClockInData.EmployeeClockIn.LocationName.Trim();

                CIW.EmployeeLocationDetails.LocationName = ClockInData.EmployeeLocationDetails.LocationName.Trim();
                CIW.EmployeeLocationDetails.Address = ClockInData.EmployeeLocationDetails.Address.Trim();
                CIW.EmployeeLocationDetails.Latitude = ClockInData.EmployeeLocationDetails.Latitude;
                CIW.EmployeeLocationDetails.Longitude = ClockInData.EmployeeLocationDetails.Longitude;

                CIR.Add(CIW);
                var response = Request.CreateResponse(HttpStatusCode.Created, ClockInData);
                return response;
            }
            catch (Exception ex)
            {
                // Log exception code goes here  
                // return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Error occured while executing GetClockIn(id) ---"+ex.ToString());
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "Clock - In Event could not be added");

            }
        }
      
    }
}
