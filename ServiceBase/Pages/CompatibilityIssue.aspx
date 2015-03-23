<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompatibilityIssue.aspx.cs" Inherits="ServiceBase.Pages.CompatiblityIssue" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../Content/bootstrap.css" />

</head>
<body>
    <form id="form1" runat="server">
<div class="container">
    <div class="row">
        <div class="jumbotron">
            <h1>Browser Combatiblity Issues</h1>
            <p>It seems you are using an outdated browser, IE 7 or less. If you are not using an outdated browser then please follow the steps below 
                to fix your combatiblity issues. 
            </p>


        </div>
    </div>

    <div class="row">
        <div class="col-md-6">
            <p>To fix the issue click on Tools and then Compatibility View Settings... </p>
             <asp:Image ID="ServiceBaseImage" runat="server" ImageUrl="~/images/ToolsMenu.jpg"/>
        </div>
         <div class="col-md-6">
             <p>Ensure that Display intranet site in Compatibility
             View is <strong>unchecked.</strong> If this option is checked the site will display in compatibility view and will not display properly. </p>
             <asp:Image ID="Image1" runat="server" ImageUrl="~/images/CompViewSettings.jpg"/>
             <p>The page will redirect once this is complete.</p>
         </div>      
    </div>

 </div> <!-- End container -->  
    </form>
</body>
</html>
