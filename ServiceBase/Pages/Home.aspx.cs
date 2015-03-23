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
    public partial class Home : System.Web.UI.Page
    {
        //Global Reference to the Database
        HelpDesk_DB db = new HelpDesk_DB();
        static DateTime refreshTimeStamp = DateTime.Now;
        static List<ProblemTracker> ProblemList;

        protected void Page_Init(object sender, EventArgs e)
        {
            ProblemList = db.ProblemTrackers.Where(p => p.ProblemStatus_id == 2).ToList();
            FindTextBox.Focus();
        }

        protected void logUsage()
        {
            using (var curDB = new HelpDesk_DB())
            {
                UsageLog log = new UsageLog();
                log.searchDateTime = DateTime.Now;
                log.NTID_id = (int)Session["userID"];
                curDB.UsageLogs.Add(log);
                curDB.SaveChanges();
            }
        }

        protected void FindButton_Click(object sender, EventArgs e)
        {
            //TODO: Add Drop Down Button here for different Find critera instead of buttons below bar

            logUsage();

            Response.Redirect("SearchResults.aspx?find=" + FindTextBox.Text.ToString());
        }

        protected void FindHostname_Click(object sender, EventArgs e)
        {
            logUsage();

            string find = FindTextBox.Text.ToString();
            if (find != "")
            {
                try
                {
                    /* If this is returning - 'Hostanem not found' for hostname's that exists
                     * Check server settings if there are issues here Under IPv4 settings ensure that NetBIOS 
                     * Settings is set to Default. 'use NetBIOS Setting from the DHCP server. If static IP 
                     * address is used or the DHCP server does not provide NEtBIOS setting, enable NetBIOS over TCP/IP.
                    */
                   // var test = System.Net.Dns.GetHostByAddress(find);
                   // var test2 = System.DirectoryServices.AccountManagement.
                    System.Net.IPHostEntry ip = System.Net.Dns.GetHostEntry(find);
                    string hostnameFull = ip.HostName;
                    string hostname = hostnameFull.ToString().Split('.')[0];
                    FindTextBox.Text = hostname.ToString();
                }
                catch
                {
                    FindTextBox.Text = find + " - Hostname not found";
                }
                
            }
            else
            {
                FindTextBox.Text = "Please enter a hostname here";
            }

        }

        /**
         * Find printer searches the printers table and breaks as soon as a match is found in any of these categories
         * First searches the ipAddress Column 
         * Second searches serialNumber Column
         * Thirdly checks the user has entered a Chip Printer name
         * Then it finds the IP of this name and checks the printers table's IP Address for this name
         */
        protected void FindPrinter_Click(object sender, EventArgs e)
        {

            logUsage();

            string find = FindTextBox.Text;
            if (FindTextBox.Text == null || FindTextBox.Text == "")
               
            {
                FindTextBox.Text = "Please enter Printer description";
                return;
            }
            Printer printerObj = db.Printers.SingleOrDefault(p => p.ipAddress == find);

            if (printerObj == null)
            {
                printerObj = db.Printers.SingleOrDefault(p => p.serialNumber == find);
            }
            //childrens printer
            if (printerObj == null)
            {
                try
                {
                    find += ".dhcp.chp.edu";
                    System.Net.IPHostEntry hostEntry = System.Net.Dns.GetHostEntry(find);
                    string ipAddress = hostEntry.AddressList[0].ToString();
                    printerObj = db.Printers.SingleOrDefault(p => p.ipAddress == ipAddress);
                }
                catch
                {
                    FindTextBox.Text = "Entry Not Found";
                    return;
                }
            }
            if (printerObj == null) { FindTextBox.Text = "Entry Not Found"; }
            else { this.displayPrinterInfo(printerObj); }

        }

        /**
         * Displays results for find printer 
         */
        private void displayPrinterInfo(Printer printerObj)
        {
           //Correct Copy and paste format here
            string[] printerInfo = {"Description of Issue: \n", 
                 "\n",
                "\nSerial Number: " + printerObj.serialNumber,
                "\nIP Address: " + printerObj.ipAddress,
                "\nModel: " + printerObj.modelNumber,
                "\nLocation: " + printerObj.location,
                "\nFloor:",
                "\nRoom: ",
                "\nEnd User Name: " ,
                "\nEnd User Phone:" ,
                "\n" ,
                "\n" ,
                "\nPriority: " + printerObj.ciDescription ,
                "\n" ,
                "\nPC Support Group:" + printerObj.supportGroup ,
                "\nLast Ping:" + printerObj.lastHardwareScan };

            PrinterResultArea.Text = "";

            for(int i=0; i < printerInfo.Length; i++)
            {
                PrinterResultArea.Text += printerInfo[i] + " </br>";
            } 

            /*
            string[] rowInfo = new string[]
            {
                "Description of Issue:", "",
                "", "",
                "Serial Number:", printerObj.serialNumber,
                "IP Address:", printerObj.ipAddress,
                "Model:", printerObj.modelNumber,
                "Location:", printerObj.location,
                "Floor:", "",
                "Room:", "",
                "End User Name:", "",
                "End User Phone:", "",
                "", "",
                "", "",
                "Priority:", printerObj.ciDescription,
                "", "",
                "PC Support Group:", printerObj.supportGroup,
                "Last Ping:", printerObj.lastHardwareScan,
            };

            for (int i = 0; i < rowInfo.Length; i = i + 2)
            {
                this.addRow(PrinterInfoTable, rowInfo[i], rowInfo[i + 1]);
            } 
            */

        }

        /**
         * Add a single Title Row
         */
        private void addTitleRow(Table table, string title, int numRows)
        {


            TableHeaderRow row = new TableHeaderRow();
            table.Rows.Add(row);

            TableCell cell = new TableCell();
            cell.Text = title;
            cell.ColumnSpan = numRows;
            cell.Height = 40;
            cell.Font.Size = 40;
            cell.Font.Size = System.Web.UI.WebControls.FontUnit.Large;
            row.Cells.Add(cell);
        }

        /**
         * Add a single celled Hyperlink row to specified table
         */
        private void addHyperRow(Table table, string url)
        {


            TableRow row = new TableRow();
            table.Rows.Add(row);

            System.Web.UI.WebControls.HyperLink link = new HyperLink();
            link.Text = url;
            link.NavigateUrl = url;

            TableCell linkCell = new TableCell();
            linkCell.Controls.Add(link);
            row.Cells.Add(linkCell);
        }

        /**
         * Add a two celled row to specified table
         */
        private void addRow(Table table, string leftColumn, string rightColumn)
        {
            TableRow row = new TableRow();
            table.Rows.Add(row);
            TableCell cellLeft = new TableCell();
            cellLeft.Text = leftColumn;
            row.Cells.Add(cellLeft);
            TableCell cellRight = new TableCell();
            cellRight.Text = rightColumn;
            row.Cells.Add(cellRight);
        }

        /**
         * Add a Three celled row to specified table
         */
        private void addRow(Table table, string leftColumn, string middleColumn, string rightColumn)
        {
            TableRow row = new TableRow();
            table.Rows.Add(row);
            TableCell cellLeft = new TableCell();
            cellLeft.Text = leftColumn;
            row.Cells.Add(cellLeft);
            TableCell cellMiddle = new TableCell();
            cellMiddle.Text = middleColumn;
            row.Cells.Add(cellMiddle);
            TableCell cellRight = new TableCell();
            cellRight.Text = rightColumn;
            row.Cells.Add(cellRight);
        }

        /// <summary>
        /// Checks problem log table for added records since last call
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<string> refreshProblemTables()
        {
            List<string> problemListJSON = new List<string>();
            using (var curDB = new HelpDesk_DB())
            {
                List<ProbTrackLog> recentLogList = curDB.ProbTrackLogs.Where(p => p.dateTime >= refreshTimeStamp).ToList();
                foreach (ProbTrackLog log in recentLogList)
                {
                    int problemID = log.Problemtracker_id;
                    ProblemTracker problem = curDB.ProblemTrackers.First(p => p.id == problemID);
                    problemListJSON.Add(convertSerializedJSON(problem));
                }
            }
            //update refresh time
            refreshTimeStamp = DateTime.Now;

            return problemListJSON;
        }

        [WebMethod]
        public static List<string> findCurrentProblems()
        {
            List<string> problemListJSON = new List<string>();
            using (var curDB = new HelpDesk_DB())
            {
                foreach (ProblemTracker problem in ProblemList)
                {
                    //Downtime/Deg Condition
                    if (problem.ProblemCondition_id == 1)
                    {
                        problemListJSON.Add(convertSerializedJSON(problem));
                    }
                    //Known Downtime 
                    else if (problem.ProblemCondition_id == 2 && problem.plannedStartDateTime != null)
                    {
                        //Only Show Known Downtimes within 24 hour range
                        DateTime startTime = (DateTime)problem.plannedStartDateTime;
                        DateTime testMinusDay = startTime.AddDays(-1);
                        Boolean testcondition1 = (testMinusDay < DateTime.Now);
                        Boolean testcondition2 = (DateTime.Now < startTime);

                        if (startTime.AddDays(-1) < DateTime.Now && DateTime.Now < startTime)
                        {
                            problemListJSON.Add(convertSerializedJSON(problem));
                        }
                    }
                    //Resolved/Normal Condition
                    else if (problem.ProblemCondition_id == 3 && problem.endDateTime != null)
                    {
                        //Show All resolved problems until 24 hours after end time
                        DateTime endTime = (DateTime)problem.endDateTime;
                        if (DateTime.Now < endTime.AddDays(1))
                        {
                            problemListJSON.Add(convertSerializedJSON(problem));
                        }
                    }
                }

            }

            return problemListJSON;
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
        public static List<string> getFacilities()
        {
            List<string> facilityListJSON = new List<string>();
            using (var curDB = new HelpDesk_DB())
            {
                List<PhoneNumber> facilitytList = curDB.PhoneNumbers.ToList();
                foreach (PhoneNumber facility in facilitytList)
                {
                    facilityListJSON.Add(convertSerializedJSON(facility));
                }
            }
            return facilityListJSON;
        }



        [WebMethod]
        public static List<string> getAbbreviations()
        {
            List<string> abbreviationListJSON = new List<string>();
            using (var curDB = new HelpDesk_DB())
            {
                List<Abbreviation> abbreviationList = curDB.Abbreviations.ToList();
                foreach (Abbreviation curAbbreviation in abbreviationList)
                {
                    abbreviationListJSON.Add(convertSerializedJSON(curAbbreviation));
                }
            }

            return abbreviationListJSON;

        }

        [WebMethod]
        public static List<string> getEmailTmeplates()
        {
            List<string> emailListJSON = new List<string>();
            using (var curDB = new HelpDesk_DB())
            {
                List<EmailTemplate> emailList = curDB.EmailTemplates.ToList();
                foreach (EmailTemplate email in emailList)
                {
                    emailListJSON.Add(convertSerializedJSON(email));
                }
            }
            return emailListJSON;
        }

        [WebMethod]
        public static List<string> getHelpfulHyperlinks()
        {
            List<string> hyperlinkListJSON = new List<string>();
            using (var curDB = new HelpDesk_DB())
            {
                List<HelpfulHyperlink> linkList = curDB.HelpfulHyperlinks.ToList();
                foreach (HelpfulHyperlink link in linkList)
                {
                    hyperlinkListJSON.Add(convertSerializedJSON(link));
                }
            }

            return hyperlinkListJSON;
        }

        public static string convertSerializedJSON(HelpfulHyperlink link)
        {
            HelpfulHyperlinkJSON linkJSON = new HelpfulHyperlinkJSON(link);
            var json = new JavaScriptSerializer().Serialize(linkJSON);
            return json;
        }

        public static string convertSerializedJSON(EmailTemplate email)
        {
            EmailTemplateJSON emailJSON = new EmailTemplateJSON(email);
            var json = new JavaScriptSerializer().Serialize(emailJSON);
            return json;
        }

        public static string convertSerializedJSON(Abbreviation abr)
        {
            AbbreviationJSON abrJSON = new AbbreviationJSON(abr);
            var json = new JavaScriptSerializer().Serialize(abrJSON);
            return json;
        }

        public static string convertSerializedJSON(PhoneNumber facility)
        {
            PhoneNumberJSON facilityJSON = new PhoneNumberJSON(facility);
            var json = new JavaScriptSerializer().Serialize(facilityJSON);
            return json;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

    } //End Main

    public class HelpfulHyperlinkJSON
    {
        public int id { get; set; }
        public string title { get; set; }
        public string hyperlink { get; set; }

        public HelpfulHyperlinkJSON() { }

        public HelpfulHyperlinkJSON(HelpfulHyperlink link)
        {
            this.id = link.id;
            this.title = link.title;
            this.hyperlink = link.hyperlink;
        }
    }

    public class EmailTemplateJSON
    {
        public int id { get; set; }
        public string title { get; set; }
        public string template { get; set; }

        public EmailTemplateJSON() { }

        public EmailTemplateJSON(EmailTemplate email)
        {
            this.id = email.id;
            this.title = email.title;
            this.template = email.template;
        }
    }

    public class AbbreviationJSON
    {
        public int id { get; set; }
        public string abbreviation { get; set; }
        public string location { get; set; }

        public AbbreviationJSON() { }

        public AbbreviationJSON(Abbreviation abbreviation)
        {
            this.id = abbreviation.id;
            this.abbreviation = abbreviation.abbreviation1;
            this.location = abbreviation.location;
        }
    }

    public class PhoneNumberJSON
    {
        public int id { get; set; }
        public string location { get; set; }
        public string phoneNumber { get; set; }
        public string details { get; set; }

        public PhoneNumberJSON() { }

        public PhoneNumberJSON(PhoneNumber facility)
        {
            this.id = facility.id;
            this.location = facility.location;
            this.phoneNumber = facility.number;
            this.details = facility.details;
        }
    }

}