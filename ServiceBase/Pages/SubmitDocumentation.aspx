<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/ServiceBase.Master" AutoEventWireup="true" CodeBehind="SubmitDocumentation.aspx.cs" Inherits="ServiceBase.Pages.SubmitDocumentation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<form id="PageForm" runat="server" class="form-horizontal">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" >
    </asp:ScriptManager>

    <div class="text-center">
        <div class="row">
            <div class="col-md-12">
                <asp:Image ID="ServiceBaseLogo" ImageUrl="~/images/ServiceBaseV Title.PNG" runat="server" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <h2>SD Documentation Submission Form</h2>
            </div>
        </div>
        <hr />

        <div class="row">
            <div class="col-md-8 col-md-offset-2">
                
                    <asp:Table ID="FindTable" runat="server" Width="100%">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left"><input id="New-Btn" type="button" value="New Document" class="btn btn-info" onclick="cleanForm();" /> </asp:TableCell>
                             <asp:TableCell ColumnSpan="2"></asp:TableCell>
                        
                            <asp:TableCell HorizontalAlign="Right" >
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

                        <asp:Table ID="SubmissionTable" CssClass="table" runat="server" BackColor="#F2F2F2" BorderColor="#E9E9E9" BorderStyle="Solid">
                            <asp:TableRow>
                                <asp:TableCell ColumnSpan="2"><label>Add Article</label> </asp:TableCell>
                                <asp:TableCell ColumnSpan="4"> <a href="file://WINSVBNSPRD01/ServiceBase/Documentation/SD%20Documentation" >Click here to open SD Documentation Folder</a></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell ColumnSpan="2"><label>Alias</label></asp:TableCell>
                                <asp:TableCell ColumnSpan="4"> <textarea id="AliasInput" class="form-control" rows="3" cols="40" onfocus="showUpdateBtn();"></textarea></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell ColumnSpan="2"><label>PDF Document Title</label></asp:TableCell>
                                <asp:TableCell ColumnSpan="4"> <input id="TitleInput" class="form-control" type="text"  onfocus="showUpdateBtn();"/></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell ColumnSpan="2"><label>PDF Hyperlink </label></asp:TableCell> 
                                <asp:TableCell ColumnSpan="4"> <input id="PDFHyperlinkInput" class="form-control" type="text"  onfocus="showUpdateBtn();" /> </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell ColumnSpan="2"><label>Public Access</label></asp:TableCell>
                                <asp:TableCell ColumnSpan="4"><input id="PDF-Public-Checkbox" type="checkbox"  onfocus="showUpdateBtn();" />*Check to allow users access from Self-Help</asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell ColumnSpan="2"><label>Password Document Title</label></asp:TableCell>
                                <asp:TableCell ColumnSpan="4"> <input id="PwTitleInput" class="form-control" type="text"  onfocus="showUpdateBtn();" /> </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell ColumnSpan="2"><label>Password Hyperlink</label></asp:TableCell>
                                <asp:TableCell ColumnSpan="4"> <input id="PwHyperlinkInput" class="form-control" type="text"  onfocus="showUpdateBtn();"/> </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell ColumnSpan="2"><label>Public Access</label></asp:TableCell>
                                <asp:TableCell ColumnSpan="4"><input id="PW-Public-Checkbox" type="checkbox"  onfocus="showUpdateBtn();" />*Check to allow users access from Self-Help</asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell ColumnSpan="6">
                                   <div class="btn-group" role="group" aria-label="...">  
                                       <input id="firstBtn" type="button" class="btn btn-default" value="first" onclick="getFirst();"  />
                                       <input id="previousBtn" type="button" class="btn btn-default" value="previous" onclick="getPrevious();"  />
                                       <input id="NextButton" type="button" class="btn btn-default" value="Next" onclick="getNext();" /> 
                                        <input id="lasttBtn" type="button" class="btn btn-default" value="last" onclick="getLast();"  />
                                   </div>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell ColumnSpan="3">  <input id="Submit-Btn" type="button" class="btn btn-primary" value="Submit New" /> </asp:TableCell>
                                
                                 <asp:TableCell ColumnSpan="3" HorizontalAlign="Right"><input id="Update-Btn" type="button" class="btn btn-primary" value="Update Document" />  </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
  
                </div><!-- End Table Div -->

         </div><!-- End Row Div -->
        <div id="testRow">

        </div>
    </div>

        <script type="text/javascript" src="../Scripts/SubmitDocumentation.js"></script>
 </form>           
</asp:Content>
