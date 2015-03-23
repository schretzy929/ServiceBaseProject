<%@ Page Language="C#" MasterPageFile="~/Pages/ServiceBase.Master" AutoEventWireup="true" CodeBehind="NewUser.aspx.cs" Inherits="ServiceBase.Pages.NewUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<form id="PageForm" runat="server" class="form-horizontal">
   <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server"></asp:ScriptManager>
    
    <div class="row">
         <div class="jumbotron">
                <h1>Welcome to ServiceBase!</h1>
                <p>We detect that this is your first time visiting ServiceBase, so please enter your first and last name for our records.</p>
         </div>
    </div>

    <div class="row">
        <div class="col-md-12">

                    <div class="col-md-6 col-md-offset-3" >
                        <br />
                        <div class="form-group">
                            <label for="First-Name" class="col-md-3 control-label">First Name</label>
                            <div class="col-md-9">
                                <input type="text" class="form-control" id="First-Name" placeholder="First Name" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="Last-Name" class="col-md-3 control-label">Last Name</label>
                            <div class="col-md-9">
                                <input type="text" class="form-control" id="Last-Name" placeholder="Last Name" />
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="col-md-offset-2 col-md-10">
                                 <input id="submit-btn" type="button" class="btn btn-lg btn-primary" value="Submit" />
                            </div>
                        </div>

                     </div>
        </div>
    </div>
    
     <script src="../Scripts/NewUser.js" type="text/javascript"></script>
</form>
</asp:Content>
