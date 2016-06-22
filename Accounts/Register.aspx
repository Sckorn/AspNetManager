<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Accounts_Register" MasterPageFile="~/Master/Main.master" %>

<asp:Content ID="MainRegisterContent" runat="server" ContentPlaceHolderID="MainBodyContent">
    <asp:CreateUserWizard ID="RegisterUser" runat="server" ActiveStepIndex="0" Width="100%" CssClass="form-horizontal" MembershipProvider="FMMembershipProvider" CreateUserButtonText="Register">
        <WizardSteps>
            <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server">
                <ContentTemplate>
                    <div class="form-group">
                        <label for="UserName" class="col-sm-4 control-label">Login</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="UserName" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                      </div>
                      <div class="form-group">
                        <label for="Password" class="col-sm-4 control-label">Password</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="Password" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                        </div>
                      </div>
                    <div class="form-group">
                        <label for="PasswordApproval" class="col-sm-4 control-label">Repeat Password</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="PasswordApproval" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                        </div>
                      </div>
                    <div class="form-group">
                        <label for="Email" class="col-sm-4 control-label">Email</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="Email" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                      </div>
                    <div class="form-group">
                        <label for="Question" class="col-sm-4 control-label">Secret Question</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="Question" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="Answer" class="col-sm-4 control-label">Question Answer</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="Answer" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <asp:CompareValidator ID="cpValidator" runat="server" ErrorMessage="CompareValidator" ControlToValidate="Password" ControlToCompare="PasswordApproval"></asp:CompareValidator>
                </ContentTemplate>
            </asp:CreateUserWizardStep>
            <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server">
                <ContentTemplate>
                    <h3>Thank you for your registration! You will receive email with the link to approve your account!</h3>
                </ContentTemplate>
            </asp:CompleteWizardStep>
        </WizardSteps>
        <CreateUserButtonStyle  CssClass="btn btn-default" />
    </asp:CreateUserWizard>
</asp:Content>
