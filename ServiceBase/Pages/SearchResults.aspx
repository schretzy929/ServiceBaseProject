<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/ServiceBase.Master" AutoEventWireup="true" CodeBehind="SearchResults.aspx.cs" Inherits="ServiceBase.Pages.SearchResults" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<form id="PageForm" runat="server" class="form-horizontal">
    <div id="Page-Title-Container" class="text-center">
        <h1>Results</h1>
    </div>

    <div class="row-fluid">
        <div class="span1"></div>
        <div id="SD-Documentation-Container" class="span10">
            <h4>SD Documentation Results</h4>
            <asp:Table ID="SDDocumentationTable" CssClass="table" runat="server" HorizontalAlign="Center" >
                <asp:TableHeaderRow HorizontalAlign="Center">
                    <asp:TableHeaderCell>Hyperlink</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Passwork Hyperlink</asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>
        </div>
        <div class="span1"></div>
     </div> <!--End Row 1 -->

    <div class="row-fluid">
        <div class="span1"></div>
        <div id="Info-Gathering-Container" class="span10">
            <h4>Info Gathering Forms</h4>
            <asp:Table ID="InfoGatheringTable" CssClass="table" runat="server" HorizontalAlign="Center">
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>PDF</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Assignment Group</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Support Info</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Support Hours</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Contact Info</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Password Attributes</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Password Hyperlink</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Aliases</asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>
        </div>
        <div class="span1"></div>
    </div>
</form>
</asp:Content>
