<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Accounts_Register" MasterPageFile="~/Master/Main.master" %>

<asp:Content ID="MainRegisterContent" runat="server" ContentPlaceHolderID="MainBodyContent">
    <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" ActiveStepIndex="0" Width="100%">
        <WizardSteps>
            <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server">
                <ContentTemplate>
                    <div class="form-group">
                        <label for="UserName" class="col-sm-2 control-label">Login</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="UserName" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                      </div>
                      <div class="form-group">
                        <label for="Password" class="col-sm-2 control-label">Password</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="Password" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                      </div>
                    <div class="form-group">
                        <label for="Email" class="col-sm-2 control-label">Email</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="Email" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                      </div>
                    <div class="form-group">
                        <label for="Question" class="col-sm-2 control-label">Secret Question</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="Question" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="Answer" class="col-sm-2 control-label">Answer</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="Answer" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:CreateUserWizardStep>
            <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server">
                <ContentTemplate>
                    <h3>Thank you for your registration! You will receive email with the link to approve your account!</h3>
                </ContentTemplate>
            </asp:CompleteWizardStep>
        </WizardSteps>
        <FinishNavigationTemplate>
            
        </FinishNavigationTemplate>
        <ContinueButtonStyle CssClass="btn-default" />
        <FinishCompleteButtonStyle CssClass="btn-default" />
        <StepNextButtonStyle CssClass="btn-default" />
    </asp:CreateUserWizard>
</asp:Content>
