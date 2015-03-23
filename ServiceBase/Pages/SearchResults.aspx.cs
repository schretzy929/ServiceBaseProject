using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;


namespace ServiceBase.Pages
{
    public partial class SearchResults : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HelpDesk_DB db = new HelpDesk_DB();
            string findItem = "";
            //Dymanically Build SD Documentation Table
            if (Request.QueryString["find"] != null)
            {
                findItem = Request.QueryString["find"].ToString();

                //TODO: Parameterize Query       
                List<SDDocumentation> docList = db.SDDocumentations.SqlQuery("SELECT * FROM SDDocumentation WHERE alias LIKE @p0 AND Status_id = '2'", "%" + findItem + "%").ToList();


                foreach (SDDocumentation doc in docList)
                {

                    TableRow row = new TableRow();
                    SDDocumentationTable.Rows.Add(row);

                    //Document Hyperlink
                    if (doc.DocumentLink_id != null)
                    {
                        DocumentLink docLink = db.DocumentLinks.FirstOrDefault(p => p.Id == doc.DocumentLink_id);
                        this.addCell(row, doc.documentTitle, docLink.link);
                    }
                    else
                    {
                        //blank cell
                        this.addCell(row, "");
                    }

                    //Password Hyperlink
                    if (doc.PasswordHyperlink_id != null)
                    {
                        DocumentLink passwordDocLink = db.DocumentLinks.FirstOrDefault(p => p.Id == doc.PasswordHyperlink_id);
                        this.addCell(row, doc.passwordTitle, passwordDocLink.link);
                    }
                    else
                    {
                        //blank cell
                        this.addCell(row, "");
                    }

                }
            
            
                List<InfoGathering> infoGathList = db.InfoGatherings.SqlQuery("SELECT * FROM InfoGathering WHERE searchableAlias LIKE @p0 AND Status_id = '2'", "%" + findItem + "%").ToList<InfoGathering>();
                foreach(InfoGathering infoDoc in infoGathList)
                {
                    TableRow row = new TableRow();
                    InfoGatheringTable.Rows.Add(row);

                    DocumentLink docLink = db.DocumentLinks.FirstOrDefault(p => p.Id == infoDoc.infoGathLink_id);
                    //Check for nulls even though there shouldn't be any
                    if (docLink != null) { this.addCell(row, infoDoc.infoGathTitle, docLink.link); }
                    else { this.addCell(row, "N/A"); }
                    this.addCell(row, infoDoc.assignmentGroup);
                    this.addCell(row, infoDoc.supportInfo);
                    this.addCell(row, infoDoc.supportHours);
                    this.addCell(row, infoDoc.contactInfo);
                    this.addCell(row, infoDoc.passwordAttributes);
                    DocumentLink pwLink = db.DocumentLinks.FirstOrDefault(p => p.Id == infoDoc.PasswordHyperlink_id);
                    if(pwLink != null) {this.addCell(row, infoDoc.passwordTitle, pwLink.link);}
                    else {this.addCell(row, "N/A"); }
                    this.addCell(row, infoDoc.aliases);
                }
            }
        }

        /**
         * Normal Cell Creator
         */
        private void addCell(TableRow row, string text)
        {
            TableCell cell = new TableCell();
            cell.Text = text;
            row.Cells.Add(cell);
        }

        /**
         * Hyperlink Cell Creator
         */ 
        private void addCell(TableRow row, string title, string url)
        {
            System.Web.UI.WebControls.HyperLink link = new HyperLink();
            link.Text = title;
            link.NavigateUrl = url;
            link.Target = "_blank";

            TableCell linkCell = new TableCell();
            linkCell.Controls.Add(link);
            row.Cells.Add(linkCell);
        }
    }
}