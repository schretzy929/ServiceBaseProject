<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs"  MasterPageFile="~/Pages/ServiceBase.Master" Inherits="ServiceBase.Pages.Home" %>


<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<form id="PageForm" runat="server" class="form-horizontal" defaultbutton="FindButton">
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server"></asp:ScriptManager>
    <div class="row">
        <div id="ServiceBase-Image" class="text-center">
            <asp:Image ID="ServiceBaseImage" runat="server" ImageUrl="~/images/ServiceBaseV Title.PNG"/>
        </div>
    </div>

    
    <div id="Search-Box" class="text-center">           
            <div class="row">
                <div class="col-md-12">
                     <asp:TextBox ID="FindTextBox" runat="server" Width="484px" Height="29px"></asp:TextBox>
                     <asp:Button ID="FindButton" CssClass="btn btn-primary btn-large" runat="server" Text="Find" OnClick="FindButton_Click" />   
                </div>
            </div>

            <div class="row">

                <div class="col-md-12">
                    <asp:Button ID="FindHostname" CssClass="btn btn-primary" runat="server" Text="Find Hostname" OnClick="FindHostname_Click" />
                    <asp:Button ID="FindPrinter" CssClass="btn btn-primary" runat="server" Text="Find Printer" OnClick="FindPrinter_Click" />
                </div>
           </div>
        
        <hr />     
            <div class="row">
                <div class="col-md-3">
                    <input id="Facility-Directory-Btn" class="btn btn-info" type="button" value="Support and Facility Directory" />
                </div>
                <div class="col-md-3">
                    <input id="Abbreviation-Btn" class="btn btn-info" type="button" value="Site Abbreviations" />
                </div>
                <div class="col-md-3">
                    <input id="Email-Templates-Btn" class="btn btn-info" type="button" value="Ready to Go Email Templates" />
                </div>
                <div class="col-md-3">
                    <input id="Helpful-Hyperlink-Btn" class="btn btn-info" type="button" value="Helpful Hyperlinks" />
                </div>
            </div>
        <hr />

        <div class="row">
            <div id="Quick-References-Div" class="col-md-12">

            </div>
        </div>

            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="PrinterResultArea" runat="server" Text=""></asp:Label>
                    <asp:Table ID="PrinterInfoTable" runat="server" Width="100%">

                    </asp:Table>
               </div> <!-- End Col md 12 -->
            </div>
</div><!--End Text Center -->
        <div class="row">
            <div class="col-md-12">
                <h4>Problem Tracker</h4>
                <table id="Downtime-Degradation-Table" class="table table-condensed table-hover" style="width: 100%; background-color:#F2F2F2;" >
                    <tr>
                        <td> <label>Affected Service</label></td>
                        <td class="auto-style2" ><label>Lead Ticket</label></td>
                        <td class="auto-style1" ><label>Start Time</label></td>
                        <td class="issue-description"><label>Issue Description</label></td>
                    </tr>
                </table>
            </div>  
        </div>
            <div class="row">
            <div class="col-md-12">
                <h4>Known Downtime</h4>
                <div class="auto-style-overflow">
                    <table id="Known-Downtime-Table" class="table table-condensed table-hover" style="width: 150%; background-color:#F2F2F2;">
                        <tr>
                            <th><label>Affected Service</label></th>
                            <th class="auto-style2"><label>CC#</label></th>
                            <th class="auto-style1"><label>Planned Start Time</label></th>
                            <th class="auto-style1"><label>Planned End Time</label></th>
                            <th><label>Issue Description</label></th>
                            <th class="auto-style1"><label>Actual Start Time</label></th>
                            <th class="auto-style1"><label>Actual End Time</label></th>
                        </tr>
                    </table>
                </div>
            </div>  
        </div>
        <div class="row">
            <div class="col-md-12">
                <h4>Resolved - Last 24 Hours</h4>
                <table id="Resolved-Table" class="table table-condensed table-hover" style="background-color:#F2F2F2;">
                    <tr>
                        <td class="auto-style3"><label>Affected Service</label></td>
                        <td class="auto-style2"><label>Lead Ticket</label></td>
                        <td class="auto-style1"><label>Start Time</label></td>
                        <td class="auto-style1"><label>End Time</label></td>
                        <td class="issue-description"><label>Issue Description</label></td>
                    </tr>
                </table>
            </div>
         </div>

 


<script src="../Scripts/Home.js" type="text/javascript"></script>

</form>
</asp:Content>



<asp:Content ID="Content1" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style1 {
            width: 165px;
            text-align:center;
        }
        .auto-style2 {
            width: 125px;
        }
        .auto-style3 {
            width: 200px;
        }
        .auto-style-overflow{
            overflow: auto;
        }
        .issue-description{
            text-align:center;
        }
    </style>
</asp:Content>




