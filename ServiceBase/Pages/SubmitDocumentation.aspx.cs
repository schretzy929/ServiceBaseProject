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
    public partial class SubmitDocumentation : System.Web.UI.Page
    {
        HelpDesk_DB db = new HelpDesk_DB();
        static List<SDDocumentation> DocumentList;
        static int UserID; //Used for faster logging

        protected void Page_Load(object sender, EventArgs e)
        {           
            /**
             * Chris' Input
             * Sort by ABC
             * 
             * Check hyperlinks to see if they are unique
             */
            UserID = (int)Session["userID"];
            DocumentList = db.SDDocumentations.Where(p => p.Status_id == 2).OrderBy(p => p.alias).ToList();
        }

        protected static void log()
        {
            using (var curDB = new HelpDesk_DB())
            {
                SDDocLog log = new SDDocLog();
                log.NTID_id = UserID;
                log.dateTime = DateTime.Now;
                log.SDDocumentation_id = curDB.SDDocumentations.OrderByDescending(e => e.id).First().id; //Last Fn not allowed

                curDB.SDDocLogs.Add(log);
                curDB.SaveChanges();
            }
        }

        /// <summary>
        /// Checks for uniqueness of document link, adds new unique docs to the db. Returns id of doc link found in the db.
        /// </summary>
        /// <param name="docLink"></param>
        /// <returns></returns>
        public static int addDocumentLink(DocumentLink docLink)
        {
            using (var curDB = new HelpDesk_DB())
            {
                //Check if db contains the link to retain uniqueness
                if (curDB.DocumentLinks.Where(p => p.link == docLink.link).SingleOrDefault() == null)
                {
                    //Add New Link
                    curDB.DocumentLinks.Add(docLink);
                    curDB.SaveChanges();       
                }

                //TODO: Update document access if its changed 

                return curDB.DocumentLinks.SingleOrDefault(p => p.link == docLink.link).Id;
            }
        }

        /// <summary>
        /// Adds new SD Doc to db as well updates its original id
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// 
        public static void addSDDocumentation(SDDocumentation doc)
        {
            using (var curDB = new HelpDesk_DB())
            {
                curDB.SDDocumentations.Add(doc);
                curDB.SaveChanges();
            }
            using (var curDB = new HelpDesk_DB())
            {
                //Update original id
                var originalDoc = curDB.SDDocumentations.Find(doc.id);
                if (originalDoc != null)
                {
                    originalDoc.Original_id = doc.id;
                    curDB.SaveChanges();
                }

                //Rebuilds document list for find reference
                DocumentList = curDB.SDDocumentations.Where(p => p.Status_id == 2).OrderBy(p => p.alias).ToList();
            }

        }

        [WebMethod]
        public static string getDoc(int index)
        {
            SDDocumentation doc = DocumentList[index];
            string docJSON = convertSerializedJSON(doc); //TODO: ONE LINE
            return docJSON;
        }

        [WebMethod]
        public static int getLast()
        {
            return DocumentList.Count - 1;
        }

        [WebMethod]
        public static List<string> find(string alias)
        {
            List<string> listJSON = new List<string>();
            foreach (SDDocumentation doc in DocumentList)
            {
                // Comparison ignor case
                if (doc.alias.IndexOf(alias, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    string docJSON = convertSerializedJSON(doc);
                    listJSON.Add(docJSON);
                }
            }

            return listJSON;
        }

        /// <summary>
        /// SDDocumentation converted to a serialized sdDocJSON 
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        [WebMethod]
        public static string convertSerializedJSON(SDDocumentation doc)
        {
            sdDocJSON docJSON = new sdDocJSON();

            docJSON.id = doc.id;
            docJSON.index = DocumentList.IndexOf(doc);
            docJSON.alias = doc.alias;
            docJSON.documentTitle = doc.documentTitle;
            if (doc.DocumentLink_id != null)
            {
                using (var curDB = new HelpDesk_DB())
                {
                    DocumentLink pdfDoc = curDB.DocumentLinks.SingleOrDefault(p => p.Id == doc.DocumentLink_id);
                    docJSON.documentLink = pdfDoc.link;
                    docJSON.PDFaccess = pdfDoc.Access_id;
                }
            }
            docJSON.passwordTitle = doc.passwordTitle;
            if (doc.PasswordHyperlink_id != null)
            {
                using (var curDB = new HelpDesk_DB())
                {
                    DocumentLink pwDoc = curDB.DocumentLinks.SingleOrDefault(p => p.Id == doc.PasswordHyperlink_id);
                    docJSON.passwordHyperlink = pwDoc.link;
                    docJSON.PWaccess = pwDoc.Access_id;
                }
            }

            var json = new JavaScriptSerializer().Serialize(docJSON);
            return json;

        }

        /// <summary>
        /// Used for submitting new documents to the database
        /// </summary>
        /// <param name="doc"></param>
        [WebMethod]
        public static void submit(sdDocJSON doc)
        {
            DocumentLink pdfLink = new DocumentLink();
            pdfLink.link = doc.documentLink;
            pdfLink.Access_id = doc.PDFaccess;

            DocumentLink pwLink = new DocumentLink();
            pwLink.link = doc.passwordHyperlink;
            pwLink.Access_id = doc.PWaccess;

            SDDocumentation sdDoc = new SDDocumentation();
            sdDoc.alias = doc.alias;
            sdDoc.Status_id = 2;

            //TODO: Change if this Learning Center option is added to the info gathering page 
            sdDoc.LearningCenterCategory_id = 1;                

            sdDoc.documentTitle = doc.documentTitle;
           //Check for null document links
            //TODO: You can fix this so that null are handled in addDocumentLink Method
            if (doc.documentLink != "")
            {
                sdDoc.DocumentLink_id = addDocumentLink(pdfLink);
            }
           
            sdDoc.passwordTitle = doc.passwordTitle;

            //check for null pw links
            if (doc.passwordHyperlink != "")
            {
                sdDoc.PasswordHyperlink_id = addDocumentLink(pwLink); 
            }

            addSDDocumentation(sdDoc);

            log();
        }


        [WebMethod]
        public static void updateDoc(sdDocJSON doc)
        {
            SDDocumentation newSDDoc = new SDDocumentation();
            newSDDoc.Status_id = 2; 

            using (var curDB = new HelpDesk_DB())
            {
                SDDocumentation oldDoc = DocumentList[doc.index];
                var originalDoc = curDB.SDDocumentations.Find(oldDoc.id);
                originalDoc.Status_id = 4;  //Historic 

                //Transfer persisting information to new document
                newSDDoc.LearningCenterCategory_id = oldDoc.LearningCenterCategory_id;
                newSDDoc.Original_id = oldDoc.Original_id;
                if (oldDoc.learningCenterOrder != null)
                {
                    newSDDoc.learningCenterOrder = oldDoc.learningCenterOrder; 
                }

                curDB.SaveChanges();   
                //Rebuilds document list for find reference
                DocumentList = curDB.SDDocumentations.Where(p => p.Status_id == 2).OrderBy(p => p.alias).ToList();
            }

            DocumentLink pdfLink = new DocumentLink();
            pdfLink.link = doc.documentLink;
            pdfLink.Access_id = doc.PDFaccess;
            
            //Check for null document links
            if (doc.documentLink != "")
            {
                newSDDoc.DocumentLink_id = addDocumentLink(pdfLink);
            }
            
            DocumentLink pwLink = new DocumentLink();
            pwLink.link = doc.passwordHyperlink;
            pwLink.Access_id = doc.PWaccess;
            //check for null pw links
            if (doc.passwordHyperlink != "")
            {
                newSDDoc.PasswordHyperlink_id = addDocumentLink(pwLink);
            }

            newSDDoc.alias = doc.alias;

            newSDDoc.documentTitle = doc.documentTitle;

           
            newSDDoc.passwordTitle = doc.passwordTitle;


            using (var curDB = new HelpDesk_DB())
            {
                curDB.SDDocumentations.Add(newSDDoc);
                curDB.SaveChanges();

                //Rebuilds document list for find reference
                DocumentList = curDB.SDDocumentations.Where(p => p.Status_id == 2).OrderBy(p => p.alias).ToList();
            }

            log();
        }

    }//End Class


    public class sdDocJSON
    {
        public int id { get; set; }
        public int index { get; set; }
        public string alias { get; set; }
        public string documentLink { get; set; }
        public int PDFaccess { get; set; }
        public string passwordHyperlink { get; set; }
        public int PWaccess { get; set; }
        public string documentTitle { get; set; }
        public string passwordTitle { get; set; }

        public sdDocJSON()
        {

        }
    }
}