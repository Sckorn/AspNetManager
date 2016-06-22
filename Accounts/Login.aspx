<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master/Main.master" CodeFile="Login.aspx.cs" Inherits="Accounts_Login" %>

<asp:Content ID="MainLogin" ContentPlaceHolderID="MainBodyContent" runat="server">
    <asp:Login ID="MainLoginForm" CssClass="form-horizontal" runat="server" EnableViewState="false" RenderOuterTable="true" style="width:100%;" OnAuthenticate="MainLoginForm_Authenticate">
        <LayoutTemplate>
              <div class="form-group">
                <label for="inputEmail3" class="col-sm-2 control-label">Email</label>
                <div class="col-sm-10">
                    <asp:TextBox ID="UserName" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
              </div>
              <div class="form-group">
                <label for="inputPassword3" class="col-sm-2 control-label">Password</label>
                <div class="col-sm-10">
                    <asp:TextBox ID="Password" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
              </div>
              <div class="form-group">
                <div class="col-sm-offset-4 col-sm-6">
                  <div class="checkbox">
                    <label>
                      <input type="checkbox" /> Remember me
                    </label>
                  </div>
                </div>
              </div>
              <div class="form-group">
                <div class="col-sm-offset-2 col-sm-8 col-sm-push-2">
                  <button type="submit" class="btn btn-default">Sign in</button>
                </div>
              </div>
            <div class="form-group">
                <div class="col-sm-offset-1 col-sm-12">
                    Don't have an account? <asp:LinkButton runat="server" ID="RegisterLinkButton" PostBackUrl="/Register/">Register Now!</asp:LinkButton>
                </div>
            </div>
        </LayoutTemplate>
    </asp:Login>
</asp:Content>
