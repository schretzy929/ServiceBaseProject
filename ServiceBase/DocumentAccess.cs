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
    
    public partial class DocumentAccess
    {
        public DocumentAccess()
        {
            this.DocumentLinks = new HashSet<DocumentLink>();
        }
    
        public int id { get; set; }
        public string access { get; set; }
    
        public virtual ICollection<DocumentLink> DocumentLinks { get; set; }
    }
}
