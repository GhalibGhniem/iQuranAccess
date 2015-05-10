<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master"
    CodeBehind="iSuraEdit.aspx.cs" Inherits="iQuran.Web.UI.AdminUI.iSuraEdit" %>

<%@ Register Src="~/Controls/iQuran/LanguageSetting.ascx" TagPrefix="portal" TagName="Languages" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" >
</asp:Content>
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
  <div class="modulecontent">
        <asp:HiddenField ID="hdnFromWhere" runat="server" />
        <asp:HiddenField ID="hdnQuranID" runat="server" Value="-1" />
	  <asp:HiddenField ID="hdnSuraID" Value="-1" runat="server" />
      <asp:HiddenField ID="hdnSuraOrder" Value="-1" runat="server" />
    </div>

    <mp:CornerRounderTop ID="ctop1" runat="server" EnableViewState="false" />
    <asp:Panel ID="pnl1" runat="server" CssClass="panelwrapper contenttemplates yui-skin-sam">
        <div class="modulecontent">
            <fieldset>
				<legend>
					<asp:HyperLink ID="lnkAdminMenu" runat="server" Visible="true" NavigateUrl="~/Admin/AdminMenu.aspx" />&nbsp;&gt;
					<asp:HyperLink ID="lnkAdvanced" runat="server" NavigateUrl="" />&nbsp;&gt;
					<asp:HyperLink ID="lnkiQuranManagerAdmin" runat="server" />&nbsp;&gt;
					<asp:HyperLink ID="lnkiSuraManagerAdmin" runat="server" />
					<br /><br /><asp:Label ID="spnTitle"  runat="server" ForControl=""   /><br /><br />
                </legend>
				<div class="settingrow">
					<asp:Label ID="lblmessage" Visible="false" runat="server" ForControl="" CssClass="txterror needsattention" /><br />
				</div>
				<!-- INFO -->
				<div class="msgnoteborder">
					<div class="msgnote">
						<div class="settingrow">
							<mp:SiteLabel ID="SiteLabel4" runat="server" ForControl="lbliQuranVesrion" CssClass="settinglabeltight"
							ConfigKey="iQuranHeader" ResourceFile="iQuranResources" />
							<asp:Label ID="lbliQuranVesrion" runat="server" ForControl="" CssClass="txterror needsattention" />
						</div>
						<div class="settingrow">
							<mp:SiteLabel ID="SiteLabel2" runat="server" ForControl="lbliQuranLanguage" CssClass="settinglabeltight"
							ConfigKey="LanguageofVersionHeader" ResourceFile="iQuranResources" />
							<asp:Label ID="lbliQuranLanguage" runat="server" ForControl="" CssClass="txterror needsattention" />
                            <portal:Languages id="ddLang" enabled="false" visible="false" runat="server"></portal:Languages>
						</div>
						<div class="settingrow">
							<mp:SiteLabel ID="SiteLabel13" runat="server" ForControl="lbliSuraVersesCount" CssClass="settinglabeltight"
							ConfigKey="VersesCountHeader" ResourceFile="iQuranResources" />
							<asp:Label ID="lbliSuraVersesCount" runat="server" ForControl="" CssClass="txterror needsattention" />
						</div>
					</div>
				</div>
				<div >
					&nbsp;
				</div>
                <div id="divArabic" runat="server">
				    <div class="settingrow">
					    <mp:SiteLabel ID="lblSortOrder" runat="server" ForControl="txtSortOrder" CssClass="settinglabeltight"
                        ConfigKey="SortOrderHeader" ResourceFile="iQuranResources" /> <mp:SiteLabel 
                         ID="lblStar1" runat="server" ForControl="txtSortOrder" CssClass="txterror needsattention"
                        ConfigKey="StarLabel" ResourceFile="iQuranResources" />
					    <asp:Textbox id="txtSortOrder" width="50" MaxLength="3"  CssClass="verywidetextbox forminput" runat="server" />
					    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtSortOrder"  CssClass="txterror"
                         ErrorMessage="<%$ Resources:iQuranMessagesResources,Required %>" ></asp:RequiredFieldValidator>
					    <asp:RangeValidator ID="RangeValidator1" Type="Integer" Display="Dynamic"  MinimumValue="1" SetFocusOnError="true" MaximumValue="114" ControlToValidate="txtSortOrder" 
                            runat="server" ErrorMessage="Range Error, must be between 1 and 114 !"></asp:RangeValidator>
				    </div>
                </div>
                <div id="divTransl" runat="server">
                    <div class="settingrow">
					    <mp:SiteLabel ID="SiteLabel8" runat="server" ForControl="ddSelSura" CssClass="settinglabeltight"
                        ConfigKey="SortOrderHeader" ResourceFile="iQuranResources" /> <mp:SiteLabel 
                         ID="SiteLabel9" runat="server" ForControl="ddSelSura" CssClass="txterror needsattention"
                        ConfigKey="StarLabel" ResourceFile="iQuranResources" />
                        <asp:DropDownList ID="ddSelSura" DataValueField="SuraOrder" DataTextField="Title" runat="server"></asp:DropDownList>
                         <portal:mojoHelpLink ID="MojoHelpLink17" runat="server" HelpKey="iquran-SuraTranslationOrder-help" />
				    </div>
                </div>
				<div class="settingrow">
					<mp:SiteLabel ID="SiteLabel6" runat="server" ForControl="ddPlace" CssClass="settinglabeltight"
                    ConfigKey="SelectPlaceHeader" ResourceFile="iQuranResources" /> <mp:SiteLabel 
                     ID="SiteLabel7" runat="server" ForControl="ddPlace" CssClass="txterror needsattention"
                    ConfigKey="StarLabel" ResourceFile="iQuranResources" />
					<asp:DropDownList ID="ddPlace" runat="server">
						<asp:ListItem Selected="True" Text="<%$ Resources:iQuranOptionsResources,OptionPlaceSelectPlace %>" Value="na"></asp:ListItem>
                        <asp:ListItem Text="<%$ Resources:iQuranOptionsResources,OptionPlaceMekkaText %>" Value="<%$ Resources:iQuranOptionsResources,OptionPlaceMekkaValue %>"></asp:ListItem>
                        <asp:ListItem Text="<%$ Resources:iQuranOptionsResources,OptionPlaceMadeenaText %>" Value="<%$ Resources:iQuranOptionsResources,OptionPlaceMadeenaValue %>"></asp:ListItem>
					</asp:DropDownList>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddPlace"
                        ErrorMessage="<%$ Resources:iQuranMessagesResources,Required %>" InitialValue="na"></asp:RequiredFieldValidator>
				</div>
				<div class="settingrow">
					<mp:SiteLabel ID="SiteLabel3" runat="server" ForControl="txtTitle" CssClass="settinglabeltight"
                    ConfigKey="TitleHeader" ResourceFile="iQuranResources" /> <mp:SiteLabel 
                     ID="SiteLabel5" runat="server" ForControl="txtTitle" CssClass="txterror needsattention"
                    ConfigKey="StarLabel" ResourceFile="iQuranResources" />
					<asp:Textbox id="txtTitle" width="200" MaxLength="255"  CssClass="verywidetextbox forminput" runat="server" />
					<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtTitle"  CssClass="txterror"
                     ErrorMessage="<%$ Resources:iQuranMessagesResources,Required %>" ></asp:RequiredFieldValidator>
				</div>

				<div class="settingrow">
					<mp:SiteLabel ID="lblActiveHeader" runat="server" ForControl="cbIsActive" CssClass="settinglabeltight"
						ConfigKey="ActiveHeader" ResourceFile="iQuranResources" />
					<asp:CheckBox ID="cbIsActive"  runat="server" />
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

