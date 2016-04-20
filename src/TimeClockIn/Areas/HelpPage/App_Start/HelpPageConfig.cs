// Uncomment the following to provide samples for PageResult<T>. Must also add the Microsoft.AspNet.WebApi.OData
// package to your project.
////#define Handle_PageResultOfT

using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web;
using System.Web.Http;
using TimeClockIn.Models;
#if Handle_PageResultOfT
using System.Web.Http.OData;
#endif

namespace TimeClockIn.Areas.HelpPage
{
    /// <summary>
    /// Use this class to customize the Help Page.
    /// For example you can set a custom <see cref="System.Web.Http.Description.IDocumentationProvider"/> to supply the documentation
    /// or you can provide the samples for the requests/responses.
    /// </summary>
    public static class HelpPageConfig
    {
        [SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
            MessageId = "TimeClockIn.Areas.HelpPage.TextSample.#ctor(System.String)",
            Justification = "End users may choose to merge this string with existing localized resources.")]
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly",
            MessageId = "bsonspec",
            Justification = "Part of a URI.")]
        public static void Register(HttpConfiguration config)
        {
            //// Uncomment the following to use the documentation from XML documentation file.
           config.SetDocumentationProvider(new XmlDocumentationProvider(HttpContext.Current.Server.MapPath("~/App_Data/XmlDocument.xml")));

            //// Uncomment the following to use "sample string" as the sample for all actions that have string as the body parameter or return type.
            //// Also, the string arrays will be used for IEnumerable<string>. The sample objects will be serialized into different media type 
            //// formats by the available formatters.
            //set example formats
            EmployeeClockIn xx = new EmployeeClockIn() { id = 1, EmployeeUserId = "dd@dd.com", LocationName = "VGG Oregun" , ClockInDateTime = new DateTime()};
            EmployeeClockIn xy = new EmployeeClockIn() { id = 2, EmployeeUserId = "cc@cc.com", LocationName = "Site", ClockInDateTime = new DateTime() };
            Location zz = new Location() { id = 1, LocationName = "VGG EAN Hangar", Latitude = 6.5555555, Longitude = 3.3333333, Address = "17 Paper Close, Ikeja" };
            Location zy = new Location() { id = 2, LocationName = "VGG Oregun", Latitude = 6.5555555, Longitude = 3.3333333, Address = "8 Rubber Road, VI" };
           config.SetSampleObjects(new Dictionary<Type, object>
            {
                {typeof(EmployeeClockIn), xx},
                {typeof(IEnumerable<EmployeeClockIn>), new List<EmployeeClockIn>() { xx, xy} },
                {typeof(Location), zz },
                {typeof(IEnumerable<Location>), new List<Location>(){ zz, zy} },
            });



            // Extend the following to provide factories for types not handled automatically (those lacking parameterless
            // constructors) or for which you prefer to use non-default property values. Line below provides a fallback
            // since automatic handling will fail and GeneratePageResult handles only a single type.
#if Handle_PageResultOfT
            config.GetHelpPageSampleGenerator().SampleObjectFactories.Add(GeneratePageResult);
#endif

            // Extend the following to use a preset object directly as the sample for all actions that support a media
            // type, regardless of the body parameter or return type. The lines below avoid display of binary content.
            // The BsonMediaTypeFormatter (if available) is not used to serialize the TextSample object.
            // config.SetSampleForMediaType(
            //    new TextSample("Binary JSON content. See http://bsonspec.org for details."),
            //    new MediaTypeHeaderValue("application/bson"));

            //// Uncomment the following to use "[0]=foo&[1]=bar" directly as the sample for all actions that support form URL encoded format
            //// and have IEnumerable<string> as the body parameter or return type.
            // config.SetSampleForType("[0]=foo&[1]=bar", new MediaTypeHeaderValue("application/x-www-form-encoding"), typeof(string));

            //// Uncomment the following to use "1234" directly as the request sample for media type "text/plain" on the controller named "Values"
            //// and action named "Put".
            /// config.SetSampleRequest("1234", new MediaTypeHeaderValue("text/plain"), "Values", "Put");
            /// Add new request types for various endpoints
            var postObj = new
            {
                EmployeeClockIn = new { EmployeeUserId = "dd@dd.com", LocationName = "VGG Oregun" },
                EmployeeLocationDetails = "null"
            };
            var postObj1 = new
            {
                EmployeeClockIn = new { EmployeeUserId = "dd@dd.com", LocationName = "Site" },
                EmployeeLocationDetails = new { LocationName = "The View Hotel", Latitude = 6.5555555M, Longitude = 3.4444444M , Address = "12, Paper Road, Lagos" }
            };
            string sampleClockIn = JsonConvert.SerializeObject(postObj, Formatting.Indented);
            string sampleClockIn1 = JsonConvert.SerializeObject(postObj1, Formatting.Indented);

            config.SetSampleRequest(sampleClockIn, new MediaTypeHeaderValue("application/json"), "ClockIn", "PostClockIn");
            config.SetSampleRequest(sampleClockIn1, new MediaTypeHeaderValue("text/json"), "ClockIn", "PostClockIn");

            var postObj3 = new
            {
                EmployeeUserId = "dd@dd.com",
                LocationName = "VGG Oregun",
                FromDateTime = "2016-04-07",
                ToDateTime = "2016-04-08"
            };
            config.SetSampleRequest(JsonConvert.SerializeObject(postObj3, Formatting.Indented), new MediaTypeHeaderValue("application/json"), "ClockIn", "GetClockIn");

            var locObj = new
            {
                LocationName = "VGG V.Island", Latitude = 6.4444444M, Longitude = 3.5555555M, Address = "Plot X, Eastern Side, Victoria Island, Lagos Nigeria"
            };
            config.SetSampleRequest(JsonConvert.SerializeObject(locObj, Formatting.Indented), 
                new MediaTypeHeaderValue("application/json"), "Location", "PostLocation");
            config.SetSampleRequest(JsonConvert.SerializeObject(locObj, Formatting.Indented),
                new MediaTypeHeaderValue("text/json"), "Location", "PostLocation");


            //// Uncomment the following to use the image on "../images/aspNetHome.png" directly as the response sample for media type "image/png"
            //// on the controller named "Values" and action named "Get" with parameter "id".
            // config.SetSampleResponse(new ImageSample("../images/aspNetHome.png"), new MediaTypeHeaderValue("image/png"), "Values", "Get", "id");
            config.SetSampleResponse("Clock-In Successful.", new MediaTypeHeaderValue("text/plain"), "ClockIn", "PostClockIn");
            config.SetSampleResponse("New Location Added.", new MediaTypeHeaderValue("text/plain"), "Location", "PostLocation");
            config.SetSampleResponse("Record Updated.", new MediaTypeHeaderValue("text/plain"), "Location", "UpdateLocation");


            //// Uncomment the following to correct the sample request when the action expects an HttpRequestMessage with ObjectContent<string>.
            //// The sample will be generated as if the controller named "Values" and action named "Get" were having string as the body parameter.
            ///config.SetActualRequestType(typeof(string), "Values", "Get");

            //// Uncomment the following to correct the sample response when the action returns an HttpResponseMessage with ObjectContent<string>.
            //// The sample will be generated as if the controller named "Values" and action named "Post" were returning a string.
            //config.SetActualResponseType(typeof(string), "Values", "Post");


        }

#if Handle_PageResultOfT
        private static object GeneratePageResult(HelpPageSampleGenerator sampleGenerator, Type type)
        {
            if (type.IsGenericType)
            {
                Type openGenericType = type.GetGenericTypeDefinition();
                if (openGenericType == typeof(PageResult<>))
                {
                    // Get the T in PageResult<T>
                    Type[] typeParameters = type.GetGenericArguments();
                    Debug.Assert(typeParameters.Length == 1);

                    // Create an enumeration to pass as the first parameter to the PageResult<T> constuctor
                    Type itemsType = typeof(List<>).MakeGenericType(typeParameters);
                    object items = sampleGenerator.GetSampleObject(itemsType);

                    // Fill in the other information needed to invoke the PageResult<T> constuctor
                    Type[] parameterTypes = new Type[] { itemsType, typeof(Uri), typeof(long?), };
                    object[] parameters = new object[] { items, null, (long)ObjectGenerator.DefaultCollectionSize, };

                    // Call PageResult(IEnumerable<T> items, Uri nextPageLink, long? count) constructor
                    ConstructorInfo constructor = type.GetConstructor(parameterTypes);
                    return constructor.Invoke(parameters);
                }
            }

            return null;
        }
#endif
    }
}