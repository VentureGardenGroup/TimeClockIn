using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace TimeClockIn.Models
{
    public class TimeClockInInitializer :  CreateDatabaseIfNotExists<TimeClockInContext>
    {
        protected override void Seed(TimeClockInContext context)
        {

            var filepath = HttpContext.Current.Server.MapPath("~/App_Data/TIME_TimeClockInData.sql");
            context.Database.ExecuteSqlCommand(File.ReadAllText(filepath));
        }
    }
}