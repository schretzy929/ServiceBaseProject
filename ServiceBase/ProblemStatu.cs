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
    
    public partial class ProblemStatu
    {
        public ProblemStatu()
        {
            this.ProblemTrackers = new HashSet<ProblemTracker>();
        }
    
        public int id { get; set; }
        public string status { get; set; }
    
        public virtual ICollection<ProblemTracker> ProblemTrackers { get; set; }
    }
}