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
    public partial class ProblemTrackerReporting : System.Web.UI.Page
    {
        HelpDesk_DB db = new HelpDesk_DB();
        static List<ProblemTracker> ProblemList;
        static List<ProblemCondition> ConditionList;
        static List<ProblemImpact> ImpactList;
        static int UserID;
        protected void Page_Load(object sender, EventArgs e)
        {
            UserID = (int)Session["userID"];
            ProblemList = db.ProblemTrackers.Where(p => p.ProblemStatus_id == 2).ToList();
            ConditionList = db.ProblemConditions.ToList();
            ImpactList = db.ProblemImpacts.ToList();

        }

        protected static void log()
        {
            using (var curDB = new HelpDesk_DB())
            {
                ProbTrackLog log = new ProbTrackLog();
                log.NTID_id = UserID;
                log.dateTime = DateTime.Now;
                log.Problemtracker_id = ProblemList.Last().id;

                curDB.ProbTrackLogs.Add(log);
                curDB.SaveChanges();
            }
        }

        [WebMethod]
        public static List<string> getProblemImpacts()
        {
            List<string> impactListJSON = new List<string>();
            foreach(ProblemImpact impact in ImpactList)
            {
                string impactJSON = convertSerializedJSON(impact);
                impactListJSON.Add(impactJSON);
            }

            return impactListJSON;
        }

        [WebMethod]
        public static List<string> getProblemConditions()
        {
            List<string> conditionsListJSON = new List<string>();
            foreach(ProblemCondition condition in ConditionList)
            {
                string conditionJSON = convertSerializedJSON(condition);
                conditionsListJSON.Add(conditionJSON);
            }

            return conditionsListJSON;
        }

        /// <summary>
        /// Converts ProblemCondition to serialzed ConditionJSON form for JS Processing
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        [WebMethod]
        public static string convertSerializedJSON(ProblemCondition condition)
        {
            ProblemConditionJSON conditionJSON = new ProblemConditionJSON();
            conditionJSON.id = condition.id;
            conditionJSON.name = condition.name;

            var json = new JavaScriptSerializer().Serialize(conditionJSON);
            return json;
        }

        /// <summary>
        /// Converts ProblemCondition to serialzed ConditionJSON form for JS Processing
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        [WebMethod]
        public static string convertSerializedJSON(ProblemImpact impact)
        {
            ProblemImpactJSON impactJSON = new ProblemImpactJSON();
            impactJSON.id = impact.id;
            impactJSON.impactLevel = impact.impactLevel;

            var json = new JavaScriptSerializer().Serialize(impactJSON);
            return json;
        }

        [WebMethod]
        public static string convertSerializedJSON(ProblemTracker record)
        {
            JSON_Objects.ProblemRecordJSON recordJSON = new JSON_Objects.ProblemRecordJSON();
            recordJSON.id = record.id;
            recordJSON.index = ProblemList.IndexOf(record);
            recordJSON.affectedService = record.affectedService;
            recordJSON.leadTicket = record.leadTicket;
            recordJSON.startDateTime = record.startDateTime.ToString();
            recordJSON.endDateTime = record.endDateTime.ToString();
            recordJSON.description = record.description;
            recordJSON.problemCondition = record.ProblemCondition_id;
            recordJSON.plannedEndDateTime = record.plannedEndDateTime.ToString();
            recordJSON.reportedBy = record.reportedBy;
            recordJSON.plannedStartDateTime = record.plannedStartDateTime.ToString();
            if (record.plannedImpact_id != null) recordJSON.plannedImpact = (int)record.plannedImpact_id;
            if (record.actualImpact_id != null) recordJSON.actualImpact = (int)record.actualImpact_id;

            var json = new JavaScriptSerializer().Serialize(recordJSON);
            return json;

        }

        [WebMethod]
        public static string getProblem(int index)
        {
            ProblemTracker record = ProblemList[index];
            string problemJSON = convertSerializedJSON(record);
            return problemJSON;
        }

        [WebMethod]
        public static string getLastProblem()
        {
            ProblemTracker record = ProblemList.Last();
            string problemJSON = convertSerializedJSON(record);
            return problemJSON;
        }

        [WebMethod]
        public static int findLastIndex()
        {
            return ProblemList.Count - 1;
        }

        [WebMethod]
        public static List<string> find(string alias)
        {
            List<string> listJSON = new List<string>();
            foreach(ProblemTracker record in ProblemList)
            {
                //String comparison ignor case
                //Searches Affected Service and Ticket Number
                if (record.affectedService.IndexOf(alias, StringComparison.OrdinalIgnoreCase) >= 0 || record.affectedService.IndexOf(alias, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    string docJson = convertSerializedJSON(record);
                    listJSON.Add(docJson);
                }
            }

            return listJSON;
        }

        [WebMethod]
        public static void updateDoc(JSON_Objects.ProblemRecordJSON doc)
        {
            ProblemTracker newRecord = convertToDBAO(doc);

            using(var curDB = new HelpDesk_DB())
            {
                //update old document to historic
                ProblemTracker oldRecord = ProblemList[doc.index];
                var originalDoc = curDB.ProblemTrackers.Find(oldRecord.id);
                originalDoc.ProblemStatus_id = 1; //historic

                //Transfer persisting information to new record
                newRecord.Original_id = oldRecord.Original_id;

                curDB.SaveChanges();
            }

            using(var curDB = new HelpDesk_DB())
            {
                curDB.ProblemTrackers.Add(newRecord);
                curDB.SaveChanges();

                //update problemtracker list  
                ProblemList = curDB.ProblemTrackers.Where(p => p.ProblemStatus_id == 2).ToList();
            }

            log();
        }

        [WebMethod]
        public static void submit(JSON_Objects.ProblemRecordJSON doc)
        {
            ProblemTracker problemRecord = convertToDBAO(doc);
            addNewProblem(problemRecord);
        }

        public static void addNewProblem(ProblemTracker record)
        {
            using(var curDb = new HelpDesk_DB())
            {
                curDb.ProblemTrackers.Add(record);
                curDb.SaveChanges();

                //rebuild problem list for find references
                ProblemList = curDb.ProblemTrackers.Where(p => p.ProblemStatus_id == 2).ToList();
            }

            log();
        }

        public static ProblemTracker convertToDBAO(JSON_Objects.ProblemRecordJSON JSON)
        {
            ProblemTracker problemRecord = new ProblemTracker();

            problemRecord.affectedService = JSON.affectedService;
            problemRecord.leadTicket = JSON.leadTicket;
            if (JSON.startDateTime!= "") problemRecord.startDateTime = DateTime.Parse(JSON.startDateTime);
            if (JSON.endDateTime != "") problemRecord.endDateTime = DateTime.Parse(JSON.endDateTime);
            problemRecord.description = JSON.description;
            problemRecord.ProblemCondition_id = JSON.problemCondition;
            if (JSON.plannedEndDateTime != "") problemRecord.plannedEndDateTime = DateTime.Parse(JSON.plannedEndDateTime);
            problemRecord.reportedBy = JSON.reportedBy;
            if (JSON.plannedStartDateTime != "") problemRecord.plannedStartDateTime = DateTime.Parse(JSON.plannedStartDateTime);
            if (JSON.plannedImpact != 0) problemRecord.plannedImpact_id = JSON.plannedImpact;
            if (JSON.actualImpact != 0) problemRecord.actualImpact_id = JSON.actualImpact;
            problemRecord.ProblemStatus_id = 2;

            return problemRecord;
        }
    }//End Main

    

    public class ProblemImpactJSON
    {
        public int id { get; set; }
        public string impactLevel { get; set; }

        public ProblemImpactJSON() { }

        public ProblemImpactJSON(ProblemImpact impact)
        {
            this.id = impact.id;
            this.impactLevel = impact.impactLevel;
        }
    }

    public class ProblemConditionJSON
    {
        public int id { get; set; }
        public string name { get; set; }

        public ProblemConditionJSON() { }

        public ProblemConditionJSON(ProblemCondition condition)
        {
            this.id = condition.id;
            this.name = condition.name;
        }
    }
}