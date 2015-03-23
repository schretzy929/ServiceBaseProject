using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceBase.JSON_Objects
{
    public class sdDocJSON
    {
        public int id{get; set; }
        public string alias{ get; set; }
        public string documentLink { get; set; }
        public string passwordHyperlink { get; set; }
      //  public string status { get; set; }
      //  public string learningCenterCategory { get; set; }
      //  public int original_id { get; set; }
        public string documentTitle { get; set; }
        public string passwordTitle { get; set; }
      //  public string learningCenterOrder { get; set; }

        public sdDocJSON()
        {
 
        }
    }
}