<%@ Page Title="Welcome to ServiceBase" Language="C#" MasterPageFile="~/Pages/ServiceBase.Master" AutoEventWireup="true" CodeBehind="LearningCenter.aspx.cs" Inherits="ServiceBase.Pages.LearningCenter" %>
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
                <h2>Learning Center</h2>
            </div>
        </div>

<ul class="nav nav-tabs">
  <li id="Administrative-Tab" role="presentation" class="active"><a id="Administrative-Tab-Link" href="#">Administrative</a></li>
  <li id="Operating-Systems-Tab" role="presentation"><a id="Operating-Systems-Tab-Link" href="#">Operating Systems</a></li>
  <li id="Clinical-Tab" role="presentation"><a id="Clinical-Tab-Link" href="#">Clinical</a></li>
  <li id="Mobile-Devices-Tab" role="presentation"><a id="Mobile-Devices-Tab-Link" href="#">Mobile Devices</a></li>
  <li id="Software-Tab" role="presentation"><a id="Software-Tab-Link" href="#">Software</a></li>
  <li id="Tools-Tab" role="presentation"><a id="Tools-Tab-Link" href="#">Tools</a></li>
  <li id="Misc-Tab" role="presentation"><a id="Misc-Tab-Link" href="#">Misc</a></li>
</ul>
        <br/> 

        <div id="Administrative-Div" style="display:none;" class="row"><!-- Row 1 -->
            <div class="col-md-6 col-md-offset-3">
                <ul class="list-group">
                     <li class="list-group-item" style="background-color:lightblue"><h3> Administrative</h3></li>
                     <li id="Pre-Orientation-Li" class="list-group-item" style="background-color:#F2F2F2"> <a id="Pre-Orientation" name="Sub-Group" href="#" class="list-group-item" >Pre-Orientation</a> </li>
                     <li id="Orientation-Li" class="list-group-item" style="background-color:#F2F2F2"> <a id="Orientation" name="Sub-Group" href="#" class="list-group-item">Orientation</a>  </li>
                </ul>
            </div>
        </div>

        <div id="Operating-Systems-Div" style="display:none;" class="row"> <!-- Row 2 -->
            <div class="col-md-6 col-md-offset-3">
               <ul class="list-group">
                    <li class="list-group-item" style="background-color:lightblue"><h3> Operating Systems </h3></li>
                    <li id="Windows-Li" class="list-group-item" style="background-color:#F2F2F2"><a id="Windows" name="Sub-Group" href="#4" class="list-group-item" >Windows</a></li>
                    <li id="Mac-Os-X-Li" class="list-group-item" style="background-color:#F2F2F2"><a id="Mac-Os-X" name="Sub-Group" href="#5" class="list-group-item" >Mac OS X</a></li>
                </ul>
            </div>
        </div>

        <div id="Clinical-Div" style="display:none;" class="row"> <!-- Row 3 -->
            <div class="col-md-6 col-md-offset-3">
                    <ul class="list-group">
                        <li class="list-group-item" style="background-color:lightblue"><h3> Clinical </h3></li>
                        <li id="Tap-N-Go-and-Single-Signon-Li"  class="list-group-item" style="background-color:#F2F2F2"><a id="Tap-N-Go-and-Single-Signon" name="Sub-Group" href="#" class="list-group-item" >Tap N Go and Single SignOn</a></li>
                        <li id="Clinical-Workstation-Li" class="list-group-item" style="background-color:#F2F2F2"><a id="Clinical-Workstation" name="Sub-Group" href="#" class="list-group-item" >Clinical Workstation</a></li>
                        <li id="Clinical-Applications-Li" class="list-group-item" style="background-color:#F2F2F2"><a id="Clinical-Applications" name="Sub-Group" href="#" class="list-group-item" >Clinical Applications</a></li>
                    </ul>
            </div>
        </div>

        <div id="Mobile-Devices-Div" style="display:none;" class="row"> <!-- Row 4 -->
            <div class="col-md-6 col-md-offset-3">
                <ul class="list-group">
                     <li id="Mobile-Devices-Li" class="list-group-item" style="background-color:lightblue"><h3>Mobile Devices</h3></li>
                     <li id="BlackBerry-Li" class="list-group-item" style="background-color:#F2F2F2"> <a id="BlackBerry" name="Sub-Group" href="#" class="list-group-item">BlackBerry</a></li>
                     <li id="Apple-iOS-Li" class="list-group-item" style="background-color:#F2F2F2"><a id="Apple-iOS" name="Sub-Group" href="#" class="list-group-item">Apple iOS</a></li>
                     <li id="Telephony-Li" class="list-group-item" style="background-color:#F2F2F2"><a id="Telephony" name="Sub-Group" href="#" class="list-group-item">Telephony</a></li>
                </ul>
            </div>
        </div>

        <div id="Software-Div" style="display:none;" class="row"> <!-- Row 5 -->
            <div class="col-md-6 col-md-offset-3"">
                <ul class="list-group">
                    <li class="list-group-item" style="background-color:lightblue"><h3>Software</h3></li>
                    <li id="Citrix-Li" class="list-group-item" style="background-color:#F2F2F2"><a id="Citrix" name="Sub-Group" href="#" class="list-group-item">Citrix: MyApps and UPMC Access</a></li>
                    <li id="Email-Li" class="list-group-item" style="background-color:#F2F2F2"><a id="Email" name="Sub-Group" href="#" class="list-group-item">Email</a></li>
                    <li id="MyCloud-Li" class="list-group-item" style="background-color:#F2F2F2"><a id="MyCloud" name="Sub-Group" href="#" class="list-group-item">MyCloud</a></li>
                    <li id="Password-Li" class="list-group-item" style="background-color:#F2F2F2"><a id="Password" name="Sub-Group" href="#" class="list-group-item">Password Resets and Locks</a></li>
                    <li id="Kronos-Li" class="list-group-item" style="background-color:#F2F2F2"><a id="Kronos" name="Sub-Group" href="#" class="list-group-item">Kronos</a></li>
                    <li id="My-Hub-Li" class="list-group-item" style="background-color:#F2F2F2"><a id="My-Hub" name="Sub-Group" href="#" class="list-group-item">My HUB</a></li>
                    <li id="Nuance-Li" class="list-group-item" style="background-color:#F2F2F2"><a id="Nuance" name="Sub-Group" href="#" class="list-group-item">Nuance Transcription</a></li>
                </ul>
            </div>
        </div>

        <div id="Tools-Div" style="display:none;" class="row"> <!-- Row 6 -->
            <div class="col-md-6 col-md-offset-3">
                    <ul class="list-group">
                        <li class="list-group-item" style="background-color:lightblue"><h3> Tools </h3></li>
                        <li id="IMS-Li" class="list-group-item" style="background-color:#F2F2F2"><a id="IMS" name="Sub-Group" href="#" class="list-group-item" >IMS</a></li>
                        <li id="Tech-Tools-Li" class="list-group-item" style="background-color:#F2F2F2"><a id="Tech-Tools" name="Sub-Group" href="#" class="list-group-item" >Tech Tools</a></li>
                        <li id="Service-Manager-Li" class="list-group-item" style="background-color:#F2F2F2"><a id="Service-Manager" name="Sub-Group" href="#" class="list-group-item" >Service Manager</a></li>
                        <li id="Chat-Li" class="list-group-item" style="background-color:#F2F2F2" ><a id="Chat" name="Sub-Group" href="#" class="list-group-item">Chat</a></li>
                    </ul>
            </div>
        </div>

        <div id="Misc-Div" style="display:none;" class="row"> <!-- Row 7 -->
            <div class="col-md-6 col-md-offset-3"">
                    <ul class="list-group">
                        <li class="list-group-item" style="background-color:lightblue"><h3> Misc </h3></li>
                        <li id="Printing-Li" class="list-group-item" style="background-color:#F2F2F2" ><a id="Printing"  name="Sub-Group" href="#" class="list-group-item" >Printing</a></li>
                        <li id="telephony-Li" class="list-group-item" style="background-color:#F2F2F2" ><a id="telephony"  name="Sub-Group" href="#" class="list-group-item" >Telephony</a></li>
                       <li id="Facilities-Li" class="list-group-item" style="background-color:#F2F2F2"> <a id="Facilities" name="Sub-Group" href="#" class="list-group-item">Facilites/Maintenance</a></li>
                    </ul>
            </div>
        </div>
  
    </div><!-- End Div Text Center  -->

    <script src="../Scripts/LearningCenter.js" type="text/javascript"></script>
</form>
</asp:Content>
