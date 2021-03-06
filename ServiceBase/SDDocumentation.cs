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
    
    public partial class SDDocumentation
    {
        public SDDocumentation()
        {
            this.SDDocumentation1 = new HashSet<SDDocumentation>();
            this.SDDocLogs = new HashSet<SDDocLog>();
        }
    
        public int id { get; set; }
        public string alias { get; set; }
        public Nullable<int> DocumentLink_id { get; set; }
        public Nullable<int> PasswordHyperlink_id { get; set; }
        public int Status_id { get; set; }
        public int LearningCenterCategory_id { get; set; }
        public Nullable<int> Original_id { get; set; }
        public string documentTitle { get; set; }
        public string passwordTitle { get; set; }
        public Nullable<int> learningCenterOrder { get; set; }
    
        public virtual DocumentLink DocumentLink { get; set; }
        public virtual DocumentLink DocumentLink1 { get; set; }
        public virtual DocumentStatu DocumentStatu { get; set; }
        public virtual LearningCenterCategory LearningCenterCategory { get; set; }
        public virtual ICollection<SDDocumentation> SDDocumentation1 { get; set; }
        public virtual SDDocumentation SDDocumentation2 { get; set; }
        public virtual ICollection<SDDocLog> SDDocLogs { get; set; }
    }
}
