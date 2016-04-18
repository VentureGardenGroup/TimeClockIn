using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace TimeClockIn.Models
{
    public class TimeClockInInitializer : CreateDatabaseIfNotExists<TimeClockInContext>
    {
        protected override void Seed(TimeClockInContext context)
        {
            context.Database.ExecuteSqlCommand(File.ReadAllText("~/~/scripts/TIME_TimeClockInData.sql"));
        }
    }
}