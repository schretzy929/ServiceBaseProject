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
    public partial class SubmitInfoGathering : System.Web.UI.Page
    {
        HelpDesk_DB db = new HelpDesk_DB();
        static List<InfoGathering> InfoGathList;
        static int userID; //used for faster logging
        protected void Page_Load(object sender, EventArgs e)
        {
            /**
             * Chirs' Input
             * Sort By Last Updated! 
             * //TODO: Update list order by last update
             * 
             * Check Hpyerlinks to make sure they are unique!! 
             */
            userID = (int)Session["userID"];
            InfoGathList = db.InfoGatherings.Where(p => p.Status_id == 2).OrderBy(p => p.searchableAlias).ToList();

        }

        protected static void logUsage()
        {
            using (var curDB = new HelpDesk_DB())
            {
                InfoGathLog log = new InfoGathLog();
                log.NTID_id = userID;
                log.dateTime = DateTime.Now;
                log.InfoGathering_id = curDB.InfoGatherings.OrderByDescending(e => e.id).First().id; //Last Function not allowed

                curDB.InfoGathLogs.Add(log);
                curDB.SaveChanges();
            }
        }

        [WebMethod]
        public static List<string> find(string alias)
        {
            List<string> listJSON = new List<string>();
            foreach (InfoGathering doc in InfoGathList)
            {
                //String Comparison ignore case
                if (doc.searchableAlias.IndexOf(alias, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    string docJson = convertSerializedJSON(doc);
                    listJSON.Add(docJson);
                }
            }

            return listJSON;
        }

        [WebMethod]
        public static string getDoc(int index)
        {
            InfoGathering doc = InfoGathList[index];
            string docJSON = convertSerializedJSON(doc); //TODO: One Line
            return docJSON;
        }

        [WebMethod]
        public static int getLast()
        {
            return InfoGathList.Count - 1;
        }

        [WebMethod]
        public static string convertSerializedJSON(InfoGathering doc)
        {
            infoGathDocJSON docJSON = new infoGathDocJSON();

            docJSON.id = doc.id;
            docJSON.index = InfoGathList.IndexOf(doc);
            docJSON.searchableAlias = doc.searchableAlias;

            //Checks for 0 inputed into the databse because this isn't allowed to be null
            if (doc.infoGathLink_id != 0)
            {
                using (var curDB = new HelpDesk_DB())
                {
                    DocumentLink pdfDoc = curDB.DocumentLinks.SingleOrDefault(p => p.Id == doc.infoGathLink_id);
                    if (pdfDoc != null) { docJSON.infoGathLink = pdfDoc.link; }
                    else { docJSON.infoGathLink = ""; }
                }
            }

            docJSON.infoGathTitle = doc.infoGathTitle;
            docJSON.assignmentGroup = doc.assignmentGroup;
            docJSON.supportInfo = doc.supportInfo;
            docJSON.supportHours = doc.supportHours;
            docJSON.contactInfo = doc.contactInfo;
            doc.passwordAttributes = doc.passwordAttributes;
            doc.passwordTitle = doc.passwordTitle;

            if (doc.PasswordHyperlink_id != null)
            {
                using (var curDB = new HelpDesk_DB())
                {
                    DocumentLink pwDoc = curDB.DocumentLinks.SingleOrDefault(p => p.Id == doc.PasswordHyperlink_id);
                    if (pwDoc != null) { docJSON.passwordHyperlink = pwDoc.link; }
                    else { docJSON.passwordHyperlink = ""; }
                }
            }

            docJSON.aliases = doc.aliases;

            var json = new JavaScriptSerializer().Serialize(docJSON);
            return json;

        }

        /// <summary>
        /// Input string of the link, checks db for uniqueness. 
        /// If unique its added to DocumentLinks. 
        /// Returns id of document link.
        /// returns -1 for null documents.
        /// </summary>
        /// <param name="linkTitle"></param>
        /// <returns></returns>
        public static int addDocumentLink(string linkString)
        {

            DocumentLink docLink = new DocumentLink();
            docLink.link = linkString;
            docLink.Access_id = 2; //All Info Gath Docs are private

            using (var curDB = new HelpDesk_DB())
            {
                //Check for uniqueness of link
                if (curDB.DocumentLinks.Where(p => p.link == linkString).SingleOrDefault() == null)
                {
                    //Add new link to DB
                    curDB.DocumentLinks.Add(docLink);
                    curDB.SaveChanges();
                }

                //TODO: Update document acess if its changed

                return curDB.DocumentLinks.SingleOrDefault(p => p.link == docLink.link).Id;
            }


        }

        public static void addNewInfoGathering(InfoGathering doc)
        {
            using (var curDB = new HelpDesk_DB())
            {
                curDB.InfoGatherings.Add(doc);
                curDB.SaveChanges();
            }
            //update original id
            using (var curDB = new HelpDesk_DB())
            {
                var originalDoc = curDB.InfoGatherings.Find(doc.id);
                if (originalDoc != null)
                {
                    originalDoc.Original_id = doc.id;
                    curDB.SaveChanges();
                }

                //Rebuild document list for find references
                InfoGathList = curDB.InfoGatherings.Where(p => p.Status_id == 2).OrderBy(p => p.searchableAlias).ToList();
            }

            logUsage();
        }

        [WebMethod]
        public static void submit(infoGathDocJSON doc)
        {
            InfoGathering infoGathDoc = convertToDBAO(doc);
            addNewInfoGathering(infoGathDoc);
        }

        [WebMethod]
        public static void updateDoc(infoGathDocJSON doc)
        {
            InfoGathering newInfoDoc = convertToDBAO(doc);

            using (var curDB = new HelpDesk_DB())
            {
                //Update old document to historic
                InfoGathering oldDoc = InfoGathList[doc.index];
                var originalDoc = curDB.InfoGatherings.Find(oldDoc.id);
                originalDoc.Status_id = 4; //historic

                //Transfer persisting information to new document
                newInfoDoc.Original_id = oldDoc.id;

                curDB.SaveChanges();
            }

            using (var curDB = new HelpDesk_DB())
            {
                curDB.InfoGatherings.Add(newInfoDoc);
                curDB.SaveChanges();

                //update info gath list for find and navigate references 
                InfoGathList = curDB.InfoGatherings.Where(p => p.Status_id == 2).OrderBy(p => p.searchableAlias).ToList();
            }

            logUsage();

        }

        /// <summary>
        /// Converts infoGathDocJSON to a DBAO InfoGathering object that is ready to be added to the DB. Does not touch ID, Original_ID
        /// </summary>
        /// <param name="JSON"></param>
        /// <returns></returns>
        public static InfoGathering convertToDBAO(infoGathDocJSON JSON)
        {
            InfoGathering infoGathDoc = new InfoGathering();

            infoGathDoc.searchableAlias = JSON.searchableAlias;
            //Document Links
            if (JSON.infoGathLink != "")
            { infoGathDoc.infoGathLink_id = addDocumentLink(JSON.infoGathLink); }
            if (JSON.passwordHyperlink != "")
            { infoGathDoc.PasswordHyperlink_id = addDocumentLink(JSON.passwordHyperlink); }

            infoGathDoc.infoGathTitle = JSON.infoGathTitle;
            infoGathDoc.assignmentGroup = JSON.assignmentGroup;
            infoGathDoc.supportInfo = JSON.supportInfo;
            infoGathDoc.supportHours = JSON.supportHours;
            infoGathDoc.contactInfo = JSON.contactInfo;
            infoGathDoc.passwordAttributes = JSON.passwordAttributes;
            infoGathDoc.passwordTitle = JSON.passwordTitle;
            infoGathDoc.aliases = JSON.aliases;
            infoGathDoc.Status_id = 2;

            return infoGathDoc;
        }

    }//End Main

    public class infoGathDocJSON
    {
        public int id { get; set; }
        public int index { get; set; }
        public string searchableAlias { get; set; }
        public string infoGathLink { get; set; }
        public string infoGathTitle { get; set; }
        public string assignmentGroup { get; set; }
        public string supportInfo { get; set; }
        public string supportHours { get; set; }
        public string contactInfo { get; set; }
        public string passwordAttributes { get; set; }
        public string passwordTitle { get; set; }
        public string passwordHyperlink { get; set; }
        public string aliases { get; set; }

        public infoGathDocJSON()
        {

        }
    }
}