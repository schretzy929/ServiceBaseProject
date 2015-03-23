<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/ServiceBase.Master" AutoEventWireup="true" CodeBehind="SubmitInfoGathering.aspx.cs" Inherits="ServiceBase.Pages.SubmitInfoGathering" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                <h2>Info Gathering Submission Form</h2>
            </div>
        </div>
        <hr />

        <div class="row">
            <div class="col-md-8 col-md-offset-2">

                <asp:Table ID="FindTable" runat="server" Width="100%">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left"><input id="New-Btn" type="button" value="New Document" class="btn btn-info" onclick="cleanForm();" /> </asp:TableCell>
                             <asp:TableCell></asp:TableCell>
                        
                            <asp:TableCell HorizontalAlign="Right" ColumnSpan="2">
                                <div class="input-group">            
                                    <input id="Find-Text" class="form-control" type="text" placeholder="Search Alias" onfocus="hideButtons();" aria-describedby="find-btn-addon" />
                                    <span class="input-group-btn"> <input id="Find-Btn" type="button" class="btn btn-info" value="Find" /></span>
                                </div>
                            </asp:TableCell>     
                       
                        </asp:TableRow>
                    </asp:Table>

                    <div id="Find-List">

                    </div>
                    <br />
                            <table ID="SubmissionTable" class="table"  style="width: 100%; background-color:#F2F2F2;">
                                <tr>
                                    <td><label>Add Article</label></td>
                                    <td colspan="2"><a href="file://WINSVBNSPRD01/ServiceBase/Documentation/InfoGathering" >Click here to open SD Documentation Folder</a></td>
                                </tr>
                                <tr>
                                    <td><label>Searchable Alias</label></td>
                                    <td colspan="2"> <textarea id="Searchable-Alias-Input" class="form-control" cols="20" rows="3" onfocus="showUpdateBtn();"></textarea></td>
                                </tr>
                                <tr>
                                    <td><label>Document Title</label></td>
                                    <td  colspan="2"><input id="PDF-Title-Input" class="form-control" type="text" onfocus="showUpdateBtn();" /></td>
                                </tr>
                                <tr>
                                    <td><label>Document Hyperlink </label></td>
                                    <td  colspan="2"><input id="PDF-Link-Input" class="form-control" type="text" onfocus="showUpdateBtn();" /> </td>
                                </tr>
                                <tr>
                                    <td><label>Assignment Group</label></td>
                                    <td colspan="2"><input id="Assignment-Group-Input" class="form-control"  type="text" onfocus="showUpdateBtn();" /> </td>
                                </tr>
                                <tr>
                                    <td><label>Support Info </label></td>
                                    <td colspan="2"> <textarea id="Support-Info-Input" class="form-control" cols="20" rows="3" onfocus="showUpdateBtn();"></textarea> </td>
                                </tr>
                                <tr>
                                    <td><label>Support Hour</label> </td>
                                    <td colspan="2"> <textarea id="Support-Hours-Input" class="form-control" cols="20" rows="3" onfocus="showUpdateBtn();"></textarea> </td>
                                </tr>
                                <tr>
                                    <td><label>Contact Info</label> </td>
                                    <td colspan="2"> <textarea id="Contact-Input" class="form-control" cols="20" rows="3" onfocus="showUpdateBtn();"></textarea></td>
                                </tr>
                                <tr>
                                    <td><label>Password Attributes</label></td>
                                    <td colspan="2"> <textarea id="PW-Attributes-Input" class="form-control" cols="20" rows="3" name="S1" onfocus="showUpdateBtn();"></textarea></td>
                                </tr>
                                <tr>
                                    <td><label>Password Document Title</label></td>
                                    <td colspan="2"> <input id="PW-Title-Input" class="form-control" type="text" onfocus="showUpdateBtn();" /></td>
                                </tr>
                                <tr>
                                    <td><label>Password Document Hyperlink</label></td>
                                    <td colspan="2"> <input id="PW-Link-Input" class="form-control" type="text" onfocus="showUpdateBtn();" /></td>
                                </tr>
                                <tr>
                                    <td><label>Aliases</label></td>
                                    <td colspan="2"> <textarea id="Aliases-Input" class="form-control" cols="20" rows="3" name="S2" onfocus="showUpdateBtn();"></textarea></td>
                                </tr>
                                <tr>
                                    <td colspan="3">                                    
                                        <div class="btn-group" role="group" aria-label="...">  
                                           <input id="firstBtn" type="button" class="btn btn-default" value="first" onclick="getFirst();"  />
                                           <input id="previousBtn" type="button" class="btn btn-default" value="previous" onclick="getPrevious();"  />
                                           <input id="NextButton" type="button" class="btn btn-default" value="Next" onclick="getNext();" /> 
                                            <input id="lasttBtn" type="button" class="btn btn-default" value="last" onclick="getLast();"  />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td > <input id="Submit-Btn" type="button" class="btn btn-primary" value="Submit New" /> </td>
                                    <td><input id="Update-Btn" type="button" class="btn btn-primary" value="Update Document" /> </td>
                                </tr>
                            </table>
            </div> <!-- End Table Span Div -->

        </div><!-- End Tabl Row Div -->
    
         <script type="text/javascript" src="../Scripts/SubmitInfoGathering.js"></script>
    </div>
</form>
</asp:Content>
