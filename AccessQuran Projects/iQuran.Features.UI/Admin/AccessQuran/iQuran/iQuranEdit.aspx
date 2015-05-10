<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master"
    CodeBehind="iQuranEdit.aspx.cs" Inherits="iQuran.Web.UI.AdminUI.iQuranEdit" %>
<%@ Register Src="~/Controls/iQuran/LanguageSetting.ascx" TagPrefix="portal" TagName="Languages" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" >
</asp:Content>
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
  <div class="modulecontent">
        <asp:HiddenField ID="hdnFromWhere" runat="server" />
        <asp:HiddenField ID="hdnQuranID" runat="server" Value="-1" />
    </div>

    <mp:CornerRounderTop ID="ctop1" runat="server" EnableViewState="false" />
    <asp:Panel ID="pnl1" runat="server" CssClass="panelwrapper contenttemplates yui-skin-sam">
        <div class="modulecontent">
            <fieldset>
				<legend>
					<asp:HyperLink ID="lnkAdminMenu" runat="server"  />&nbsp;&gt;<asp:HyperLink ID="lnkAdvanced" runat="server"  />&nbsp;&gt;
					<asp:HyperLink ID="lnkiQuranManagerAdmin" runat="server"  />&nbsp;&gt;
					<br /><br /><asp:Label ID="spnTitle"  runat="server" ForControl=""   /><br /><br />
                </legend>
				<div class="settingrow">
					<asp:Label ID="lblmessage" Visible="false" runat="server" ForControl="" CssClass="txterror needsattention" /><br />
				</div>
				<!-- INFO -->
				<div class="msgnoteborder">
					<div class="msgnote">
						<div class="settingrow">
							<mp:SiteLabel ID="SiteLabel4" runat="server" ForControl="lblSuraCount" CssClass="settinglabeltight"
							ConfigKey="SuraCountHeader" ResourceFile="iQuranResources" />
							<asp:Label ID="lblSuraCount" runat="server" ForControl="" CssClass="txterror needsattention" />
						</div>
					</div>
				</div>
				<div >
					&nbsp;
				</div>
				 <div class="settingrow">
                        <mp:SiteLabel ID="lblLanguage" runat="server" ForControl="ddLang" CssClass="settinglabel"
                                ResourceFile="iQuranResources" ConfigKey="LanguageofVersionHeader"></mp:SiteLabel>
                                <a name="anchddLang"></a>
                            <portal:Languages id="ddLang" runat="server"></portal:Languages><mp:SiteLabel 
                     ID="SiteLabel5" runat="server" ForControl="ddLang" CssClass="txterror needsattention"
                    ConfigKey="StarLabel" ResourceFile="iQuranResources" />
                     
                </div>
				<div class="settingrow">
					<mp:SiteLabel ID="lblTitle" runat="server" ForControl="txtTitle" CssClass="settinglabeltight"
                    ConfigKey="TitleHeader" ResourceFile="iQuranResources" /> <mp:SiteLabel 
                     ID="lblStar1" runat="server" ForControl="txtTitle" CssClass="txterror needsattention"
                    ConfigKey="StarLabel" ResourceFile="iQuranResources" />
					<asp:Textbox id="txtTitle" width="200" MaxLength="250"  CssClass="verywidetextbox forminput" runat="server" />
					<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtTitle"  CssClass="txterror"
                     ErrorMessage="<%$ Resources:iQuranMessagesResources,Required %>" ></asp:RequiredFieldValidator>
				</div>
				<div class="settingrow" id="divDefault" runat="server">
					<mp:SiteLabel ID="lblDefaultHeader" runat="server" ForControl="cbIsDefault" CssClass="settinglabeltight"
						ConfigKey="IsDefaultHeader" ResourceFile="iQuranResources" />
					<asp:CheckBox ID="cbIsDefault"  runat="server" />
				</div>
				<div class="settingrow">
					<mp:SiteLabel ID="lblActiveHeader" runat="server" ForControl="cbIsActive" CssClass="settinglabeltight"
						ConfigKey="ActiveHeader" ResourceFile="iQuranResources" />
					<asp:CheckBox ID="cbIsActive"  runat="server" />
				</div>
                <div class="settingrow">
					<mp:SiteLabel ID="lblTranslationSrc" runat="server" ForControl="txtTranslationSrc" CssClass="settinglabeltight"
                    ConfigKey="TranslationSrcHeader" ResourceFile="iQuranResources" /> <portal:mojoHelpLink ID="MojoHelpLink17" runat="server" HelpKey="iquran-translationsrc-help" /> <br />
					<asp:Textbox id="txtTranslationSrc" width="200" MaxLength="250"  CssClass="verywidetextbox forminput" runat="server" />
				</div>
				<div class="settingrow">
					<mp:SiteLabel ID="lblDescription" runat="server" ForControl="edDescription" CssClass="settinglabeltight"
                    ConfigKey="DescriptionHeader" ResourceFile="iQuranResources" /> <portal:mojoHelpLink ID="MojoHelpLink2" runat="server" HelpKey="iquran-translationsrc-help" /><mp:SiteLabel 
                     ID="SiteLabel3" runat="server" ForControl="edDescription" CssClass="txterror needsattention"
                    ConfigKey="StarLabel" ResourceFile="iQuranResources" /> <br />
					<mpe:EditorControl ID="edDescription" runat="server"  ></mpe:EditorControl>
				</div>
                <div class="settingrow">
					<mp:SiteLabel ID="lblTRanslatorDetUrl" runat="server" ForControl="edTRanslatorDetUrl" CssClass="settinglabeltight"
                    ConfigKey="TRanslatorDetUrlHeader" ResourceFile="iQuranResources" /> <portal:mojoHelpLink ID="MojoHelpLink1" runat="server" HelpKey="iquran-translatordeturl-help" /> <br />
					<mpe:EditorControl ID="edTRanslatorDetUrl" runat="server"  ></mpe:EditorControl>
				</div>
                
				<div class="settingrow">
					&nbsp;
				</div>
				<div class="settingrow">
					<mp:SiteLabel ID="SiteLabel1" runat="server" CssClass="settinglabel" ConfigKey="spacer"
						ResourceFile="Resource" />
					<portal:mojoButton ID="btnSave" runat="server" />
					<asp:HyperLink ID="lnkCancel" runat="server" />
				</div>
			</fieldset>
        </div>
    </asp:Panel>
    <mp:CornerRounderBottom ID="cbottom1" runat="server" EnableViewState="false" />
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" 
    runat="server" >
</asp:Content>
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" 
    runat="server" >
</asp:Content>

