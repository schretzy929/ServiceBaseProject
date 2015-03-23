<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/ServiceBase.Master" AutoEventWireup="true" CodeBehind="ProblemTrackerReporting.aspx.cs" Inherits="ServiceBase.Pages.ProblemTrackerReporting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 244px;
        }
        .auto-style2 {
            width: 202px;
        }
        .date-width{
            width: 155px;
        }
        .affected-service{
             width: 200px;
        }
        .Known-Downtime-Row{
            display:none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<form id="PageForm" runat="server" class="form-horizontal">
     <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server"></asp:ScriptManager>
     <div class="text-center">

        <div class="row">
            <div class="col-md-12">
                <asp:Image ID="ServiceBaseLogo" ImageUrl="~/images/ServiceBaseV Title.PNG" runat="server" />
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <h2>Problem Tracker</h2>
            </div>
        </div>

         <div class="row">
             <div class="col-md-3">
                <h3>Last updated record </h3>
             </div>
         </div>
                
                <div class="row">
                    <div class="col-md-12">
                        <table id="UpdatedRecordTable" class="table table-hover">
                            <tr>
                                <td class="affected-service"><label>Affected Service</label></td>
                                <td class="date-width"><label>Lead Ticket</label></td>
                                <td class="date-width"><label>End Time</label></td>
                                <td><label>Description</label></td>
                            </tr>
                            <tr>
                                <td><div id="URT-Affected-Service"></div></td>
                                <td><div id="URT-Lead-Ticket"></div></td>
                                <td><div id="URT-End-Time"></div></td>
                                <td><div id="URT-Issue-Description"></div></td>
                            </tr>
                        </table>                      
                    </div>
                </div> <!-- End Last Updated Record Table Div -->
                

                <hr/>
                <div class="row">
                    <div class="col-md-8 col-md-offset-2">
                      <asp:Table ID="FindTable" runat="server" Width="100%">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left">
                                <input id="New-Btn" type="button" value="New Document" class="btn btn-info" onclick="cleanForm();" /> 
                                <a id="emailLink" class="btn btn-primary">Send Email</a>
                            </asp:TableCell>
                             <asp:TableCell></asp:TableCell>
                        
                            <asp:TableCell HorizontalAlign="Right" ColumnSpan="2">
                                <div class="input-group">            
                                    <input id="Find-Text" class="form-control" type="text" placeholder="Search Alias" aria-describedby="find-btn-addon" />
                                    <span class="input-group-btn"> <input id="Find-Btn" type="button" class="btn btn-info" value="Find" /></span>
                                </div>
                            </asp:TableCell>     
                       
                        </asp:TableRow>
                    </asp:Table>
                    </div>

                 </div>

                  <div id="Find-List">

                    </div>
        <br />
                        <div class="row">
                            <div class="col-md-8 col-md-offset-2"> 
                                <table class="table" style="width: 100%; background-color:#F2F2F2;">
                                    <tr>
                                        <td colspan="3">
                                         <div class="btn-group" role="group" aria-label="...">  
                                              <input id="firstBtn" type="button" class="btn btn-default" value="first" onclick="getFirst();"  />
                                              <input id="previousBtn" type="button" class="btn btn-default" value="previous" onclick="getPrevious();"  />
                                              <input id="NextButton" type="button" class="btn btn-default" value="next" onclick="getNext();" /> 
                                              <input id="lasttBtn" type="button" class="btn btn-default" value="last" onclick="getLast();"  />
                                         </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style1"><label>Affected Service</label></td>
                                        <td class="auto-style2" colspan="2">
                                           <div class="col-sm-10">
                                                <div class="form-group">
                                                     <textarea id="Affected-Service-Text" class="form-control" rows="3" cols="40" onfocus="showUpdateBtn();"></textarea>
                                                </div>
                                            </div><!-- end Col-sm-10 -->
                                       </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style1"><label>Start Date / Time</label></td>
                                        <td class="auto-style2" colspan="2">
                                    
                                                <div class='col-sm-10'>
                                                    <div class="form-group">
                                                        <div class='input-group date' id='Start-Date-Time'>
                                                            <input id="Start-Date-Time-Text" type='text' class="form-control" />
                                                            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style1"><label>Select Status</label></td>
                                        <td class="auto-style2" colspan="2"> 
                                            <div class="col-sm-10">
                                                <div class="form-group">
                                                     <select id="Condition-Dropdown" class="form-control">
                                                     <!-- Problem Conditions Populated from DB by JS -->
                                                     </select>
                                                </div>
                                            </div>
                                            
                                        </td>
                                    </tr>
<!-- Known Downtime Required Rows -->
                                    <tr class="Known-Downtime-Row">
                                         <td class="auto-style1"> <div class="Known-Downtime-Row" > <label>Planned CC</label> </div> </td>
                                         <td colspan="2"> <div class="Known-Downtime-Row" > Required for all Known Downtime/Change Controls </div> </td>
                                    </tr>
                                    <tr class="Known-Downtime-Row">
                                        <td class="auto-style1"> <div class="Known-Downtime-Row" > <label>Planned Start Date / Time</label> </div> </td>
                                        <td colspan="2">
                                            <div class="Known-Downtime-Row" >
                                              <div class='col-sm-10'>
                                                    <div class="form-group">
                                                        <div class='input-group date' id='Planned-Start-Date-Time'>
                                                            <input id="Planned-Start-Date-Time-Text" type='text' class="form-control" />
                                                            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                             </div> <!-- End Known downtime Div -->
                                        </td>

                                    </tr>
                                    
                                    <tr class="Known-Downtime-Row">
                                        <td class="auto-style1">
                                           <div class="Known-Downtime-Row" > <label>Planned End Date / Time</label> </div> </td>
                                        <td colspan="2">
                                            <div class="Known-Downtime-Row" >
                                                <div class='col-sm-10'>
                                                    <div class="form-group">
                                                        <div class='input-group date' id='Planned-End-Date-Time'>
                                                            <input id="Planned-End-Date-Time-Text" type='text' class="form-control" />
                                                            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div><!-- End known Downtime Div -->
                                        </td>
                                    </tr>

                                    <tr class="Known-Downtime-Row">
                                        <td class="auto-style1">
                                          <div class="Known-Downtime-Row" >
                                           <label> Planned Impact</label>
                                          </div>    
                                        </td>
                                        <td colspan="2"> 
                                          <div class="Known-Downtime-Row" >
                                             <div class="col-sm-10">
                                                    <div class="form-group">                                                   
                                                        <select id="Planned-Impact-Dropdown" class="form-control">
                                                         <!-- Problem Impact Populated from DB by JS -->                                                   
                                                        </select>
                                                    </div>
                                                 </div><!-- End Col-sm-10-->
                                              </div>
                                        </td>
                                    </tr>

<!-- End Known Downtime Required Rows -->

                                    <tr>
                                        <td class="auto-style1"><label>Lead/CC Ticket</label></td>
                                        <td class="auto-style2" colspan="2">
                                          <div class="col-sm-10">
                                              <div class="form-group">
                                                 <input id="Ticket-Text" class="form-control" type="text" />   
                                               </div>
                                           </div><!-- End Col-Sm-10-->
                                        </td>                                       
                                    </tr>
                                    <tr>
                                        <td class="auto-style1"><label>Problem Impact/CC Actual Impact</label></td>
                                        <td class="auto-style2" colspan="2"> 
                                         <div class="col-sm-10">
                                                <div class="form-group">
                                                    <select id="Actual-Impact-Dropdown" class="form-control">
                                                        <!-- Problem Impact Populated from DB by JS -->
                                                    </select>
                                                </div>
                                             </div> <!-- End-col-Sm-10 -->
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style1"><label>Reported By</label></td>
                                        <td colspan="2">  
                                            <div class="col-sm-10">
                                                <div class="form-group">
                                                    <input id="Reported-By-Text" class="form-control" type="text" /> 
                                                 </div>
                                             </div> <!-- End Col-Sm-10-->
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style1"><label>End Date / Time </label></td>
                                        <td class="auto-style2" colspan="2"> 
                                            <div class='col-sm-10'>
                                                    <div class="form-group">
                                                        <div class='input-group date' id='End-Date-Time'>
                                                            <input id="End-Date-Time-Text" type='text' class="form-control" />
                                                            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style1"><label>Issue Description</label></td>
                                        <td class="auto-style2" colspan="2">
                                            <div class="col-sm-10">
                                                <div class="form-group">
                                                     <textarea id="Issue-Description-Text" class="form-control" rows="4" cols="40" ></textarea>
                                                </div>
                                            </div><!-- end Col-sm-10 -->
                                        </td>
                                    </tr>
                                     <tr>
                                        <td class="auto-style1">
                                            <input id="Submit-Btn" type="button" class="btn btn-primary" value="Submit New" />
                                        </td>
                                         <td></td>
                                        <td>
                                            <input id="Update-Btn" type="button" class="btn btn-primary" value="Update Document" /> 
                                        </td>
                                    </tr>                                 

                                </table> 


                    </div> <!--End Table Span Div -->

                </div><!-- End Table Row Div -->

       </div> <!-- End Text Center Div-->

     <script src="../Scripts/ProblemTrackerReporting.js" type="text/javascript"></script>
</form>
</asp:Content>
