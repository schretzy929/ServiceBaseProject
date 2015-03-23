using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Serialization;

namespace ServiceBase.Pages
{
    public partial class LearningCenter : System.Web.UI.Page
    {
        static List<DocumentLink> HyperlinkList;

        protected void Page_Load(object sender, EventArgs e)
        {
            using (var curDB = new HelpDesk_DB())
            {
                HyperlinkList = curDB.DocumentLinks.ToList();
            }

        }

        [WebMethod]
        public static List<string> getCategoryList(string categoryTitle)
        {
            List<string> docListJSON = new List<string>();

            using (var curDB = new HelpDesk_DB())
            {
                int categoryNumber = curDB.LearningCenterCategories.SingleOrDefault(p => p.name == categoryTitle).id;

                List<SDDocumentation> docList = curDB.SDDocumentations.Where(p => p.LearningCenterCategory_id == categoryNumber).ToList();
                foreach(SDDocumentation doc in docList)
                {
                    docListJSON.Add(convertSerializedJSON(doc));
                }
            }

            return docListJSON;
        }

        public static string convertSerializedJSON(SDDocumentation doc)
        {
            LearningCenterDocJSON docJSON = new LearningCenterDocJSON();
            docJSON.alias = doc.alias;
            if (doc.DocumentLink_id != null)
            {
                int linkId = (int)doc.DocumentLink_id;
                docJSON.documentLink = HyperlinkList[linkId-1].link;
            }

            var json = new JavaScriptSerializer().Serialize(docJSON);
            return json;
        }

    } //end main

    public class LearningCenterDocJSON
    {
        public string alias { get; set; }
        public string documentLink { get; set; }

        public LearningCenterDocJSON() { }
    }

}