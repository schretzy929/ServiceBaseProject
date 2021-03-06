//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServiceBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProblemTracker
    {
        public ProblemTracker()
        {
            this.ProblemTracker1 = new HashSet<ProblemTracker>();
            this.ProbTrackLogs = new HashSet<ProbTrackLog>();
        }
    
        public int id { get; set; }
        public string affectedService { get; set; }
        public string leadTicket { get; set; }
        public Nullable<System.DateTime> startDateTime { get; set; }
        public Nullable<System.DateTime> endDateTime { get; set; }
        public string description { get; set; }
        public int ProblemCondition_id { get; set; }
        public Nullable<System.DateTime> plannedEndDateTime { get; set; }
        public string reportedBy { get; set; }
        public Nullable<System.DateTime> plannedStartDateTime { get; set; }
        public Nullable<int> plannedImpact_id { get; set; }
        public Nullable<int> actualImpact_id { get; set; }
        public int ProblemStatus_id { get; set; }
        public Nullable<int> Original_id { get; set; }
    
        public virtual ProblemCondition ProblemCondition { get; set; }
        public virtual ProblemImpact ProblemImpact { get; set; }
        public virtual ProblemStatu ProblemStatu { get; set; }
        public virtual ICollection<ProblemTracker> ProblemTracker1 { get; set; }
        public virtual ProblemTracker ProblemTracker2 { get; set; }
        public virtual ICollection<ProbTrackLog> ProbTrackLogs { get; set; }
    }
}
