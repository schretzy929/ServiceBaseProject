using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceBase.JSON_Objects
{
    public class ProblemRecordJSON
    {
        public int id { get; set; }
        public int index { get; set; }
        public string affectedService { get; set; }
        public string leadTicket { get; set; }
        public string startDateTime { get; set; }
        public string endDateTime { get; set; }
        public string description { get; set; }
        public int problemCondition { get; set; } // This needs to be an int for value
        public string plannedEndDateTime { get; set; }
        public string reportedBy { get; set; }
        public string plannedStartDateTime { get; set; }
        public int plannedImpact { get; set; }
        public int actualImpact { get; set; }

        public ProblemRecordJSON() { }

    }
}