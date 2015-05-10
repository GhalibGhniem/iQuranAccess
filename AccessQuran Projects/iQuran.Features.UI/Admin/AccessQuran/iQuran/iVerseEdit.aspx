<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master"
    CodeBehind="iVerseEdit.aspx.cs" Inherits="iQuran.Web.UI.AdminUI.iVerseEdit" %>

<%@ Register Assembly="mojoPortal.Web" Namespace="mojoPortal.Web.Editor" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/iQuran/LanguageSetting.ascx" TagPrefix="portal" TagName="Languages" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" >
</asp:Content>
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
  <div class="modulecontent">
        <asp:HiddenField ID="hdnFromWhere" runat="server" />
        <asp:HiddenField ID="hdnQuranID" Value="-1" runat="server" />
	  <asp:HiddenField ID="hdnSuraID" Value="-1" runat="server" />
      <asp:HiddenField ID="hdnVerseID" Value="-1" runat="server" />
      <asp:HiddenField ID="hdnOrigArabSuraID" Value="-1" runat="server" />
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
					<asp:HyperLink ID="lnkiSuraManagerAdmin" runat="server" />&nbsp;&gt;
					<asp:HyperLink ID="lnkiVerseManagerAdmin" runat="server" />
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
							<mp:SiteLabel ID="SiteLabel22" runat="server" ForControl="lblSuraTitle" CssClass="settinglabeltight"
							ConfigKey="TitleHeader" ResourceFile="iQuranResources" />
							<asp:Label ID="lblSuraTitle" runat="server" ForControl="" CssClass="txterror needsattention" />
						</div>
                        <div class="settingrow">
							<mp:SiteLabel ID="SiteLabel21" runat="server" ForControl="lblSuraSortOrder" CssClass="settinglabeltight"
							ConfigKey="SuraSortOrderHeader" ResourceFile="iQuranResources" />
							<asp:Label ID="lblSuraSortOrder" runat="server" ForControl="" CssClass="txterror needsattention" />
						</div>
					</div>
				</div>
				<div >
					&nbsp;
				</div>
				<div class="settingrow">
					<mp:SiteLabel ID="lblSortOrder" runat="server" ForControl="txtVerseSortOrder" CssClass="settinglabeltight"
                    ConfigKey="VerseSortOrderHeader" ResourceFile="iQuranResources" /> <mp:SiteLabel 
                     ID="lblStar1" runat="server" ForControl="txtVerseSortOrder" CssClass="txterror needsattention"
                    ConfigKey="StarLabel" ResourceFile="iQuranResources" /> <portal:mojoHelpLink ID="MojoHelpLink17" runat="server" HelpKey="iquran-VerseTranslationOrder-help" />
					<asp:Textbox id="txtVerseSortOrder" width="50" MaxLength="3"  CssClass="verywidetextbox forminput" runat="server" />
					<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtVerseSortOrder"  CssClass="txterror"
                     ErrorMessage="<%$ Resources:iQuranMessagesResources,Required %>" ></asp:RequiredFieldValidator>
					<asp:RangeValidator ID="RangeValidator1" Type="Integer" Display="Dynamic" MinimumValue="1" MaximumValue="286" 
                        ControlToValidate="txtVerseSortOrder" runat="server" ErrorMessage="Range Error, must be between 1 and 286 !"></asp:RangeValidator>
				</div>
                <div id="divArabicFields" runat="server">
                <div class="settingrow">
					<mp:SiteLabel ID="SiteLabel13" runat="server" ForControl="ddHalfNo" CssClass="settinglabeltight"
                    ConfigKey="SelectHalfNoHeader" ResourceFile="iQuranResources" /> <mp:SiteLabel 
                     ID="SiteLabel14" runat="server" ForControl="ddHalfNo" CssClass="txterror needsattention"
                    ConfigKey="StarLabel" ResourceFile="iQuranResources" />
					<asp:DropDownList ID="ddHalfNo" runat="server">
						<asp:ListItem Selected="True" Text="<%$ Resources:iQuranOptionsResources,OptionHalfNoSelectHalfNo %>" Value="na"></asp:ListItem>
                        <asp:ListItem Text="<%$ Resources:iQuranOptionsResources,OptionHalfNoFirst %>" Value="<%$ Resources:iQuranOptionsResources,OptionHalfNoFirst %>"></asp:ListItem>
                        <asp:ListItem Text="<%$ Resources:iQuranOptionsResources,OptionHalfNoSecond %>" Value="<%$ Resources:iQuranOptionsResources,OptionHalfNoSecond %>"></asp:ListItem>
					</asp:DropDownList>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddHalfNo"
                        ErrorMessage="<%$ Resources:iQuranMessagesResources,Required %>" InitialValue="na"></asp:RequiredFieldValidator>
				</div>
                <div class="settingrow">
					<mp:SiteLabel ID="SiteLabel15" runat="server" ForControl="txtPartNo" CssClass="settinglabeltight"
                    ConfigKey="PartNoHeader" ResourceFile="iQuranResources" /> <mp:SiteLabel 
                     ID="SiteLabel16" runat="server" ForControl="txtPartNo" CssClass="txterror needsattention"
                    ConfigKey="StarLabel" ResourceFile="iQuranResources" />
					<asp:Textbox id="txtPartNo" width="50" MaxLength="2"  CssClass="verywidetextbox forminput" runat="server" />
					<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="txtPartNo"  CssClass="txterror"
                     ErrorMessage="<%$ Resources:iQuranMessagesResources,Required %>" ></asp:RequiredFieldValidator>
					<asp:RangeValidator ID="RangeValidator3" Type="Integer" Display="Dynamic" MinimumValue="1" MaximumValue="30" 
                        ControlToValidate="txtPartNo" runat="server" ErrorMessage="Range Error, must be between 1 and 30 !"></asp:RangeValidator>
				</div>
                <div class="settingrow">
					<mp:SiteLabel ID="SiteLabel17" runat="server" ForControl="txtHizbNo" CssClass="settinglabeltight"
                    ConfigKey="HizbNoHeader" ResourceFile="iQuranResources" /> <mp:SiteLabel 
                     ID="SiteLabel18" runat="server" ForControl="txtHizbNo" CssClass="txterror needsattention"
                    ConfigKey="StarLabel" ResourceFile="iQuranResources" />
					<asp:Textbox id="txtHizbNo" width="50" MaxLength="2"  CssClass="verywidetextbox forminput" runat="server" />
					<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                    ControlToValidate="txtHizbNo"  CssClass="txterror"
                     ErrorMessage="<%$ Resources:iQuranMessagesResources,Required %>" ></asp:RequiredFieldValidator>
					<asp:RangeValidator ID="RangeValidator4" Type="Integer" Display="Dynamic" MinimumValue="1" MaximumValue="60" 
                        ControlToValidate="txtHizbNo" runat="server" ErrorMessage="Range Error, must be between 1 and 60 !"></asp:RangeValidator>
				</div>
                <div class="settingrow">
					<mp:SiteLabel ID="SiteLabel5" runat="server" ForControl="txtQuraterNo" CssClass="settinglabeltight"
                    ConfigKey="QuraterNoHeader" ResourceFile="iQuranResources" /> <mp:SiteLabel 
                     ID="SiteLabel6" runat="server" ForControl="txtQuraterNo" CssClass="txterror needsattention"
                    ConfigKey="StarLabel" ResourceFile="iQuranResources" />
					<asp:Textbox id="txtQuraterNo" width="50" MaxLength="3"  CssClass="verywidetextbox forminput" runat="server" />
					<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtQuraterNo"  CssClass="txterror"
                     ErrorMessage="<%$ Resources:iQuranMessagesResources,Required %>" ></asp:RequiredFieldValidator>
					<asp:RangeValidator ID="RangeValidator2" Type="Integer" Display="Dynamic" MinimumValue="1" MaximumValue="120" 
                        ControlToValidate="txtQuraterNo" runat="server" ErrorMessage="Range Error, must be between 1 and 120 !"></asp:RangeValidator>
				</div>
                <div class="settingrow">
					<mp:SiteLabel ID="SiteLabel19" runat="server" ForControl="ddPlace" CssClass="settinglabeltight"
                    ConfigKey="SelectPlaceHeader" ResourceFile="iQuranResources" /> <mp:SiteLabel 
                     ID="SiteLabel20" runat="server" ForControl="ddPlace" CssClass="txterror needsattention"
                    ConfigKey="StarLabel" ResourceFile="iQuranResources" />
					<asp:DropDownList ID="ddPlace" runat="server">
						<asp:ListItem Selected="True" Text="<%$ Resources:iQuranOptionsResources,OptionPlaceSelectPlace %>" Value="na"></asp:ListItem>
                        <asp:ListItem Text="<%$ Resources:iQuranOptionsResources,OptionPlaceMekkaText %>" Value="<%$ Resources:iQuranOptionsResources,OptionPlaceMekkaValue %>"></asp:ListItem>
                        <asp:ListItem Text="<%$ Resources:iQuranOptionsResources,OptionPlaceMadeenaText %>" Value="<%$ Resources:iQuranOptionsResources,OptionPlaceMadeenaValue %>"></asp:ListItem>
					</asp:DropDownList>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddPlace"
                        ErrorMessage="<%$ Resources:iQuranMessagesResources,Required %>" InitialValue="na"></asp:RequiredFieldValidator>
				</div>
				<div class="settingrow">
					<mp:SiteLabel ID="lblActiveHeader" runat="server" ForControl="cbIsActive" CssClass="settinglabeltight"
						ConfigKey="ActiveHeader" ResourceFile="iQuranResources" />
					<asp:CheckBox ID="cbIsActive"  runat="server" />
				</div>
                </div>
				<div id="divOthmani" runat="server" >
					<div class="settingrow">
						<mp:SiteLabel ID="SiteLabel7" runat="server" ForControl="edOthmaniText" CssClass="settinglabeltight"
						ConfigKey="OthmaniTextHeader" ResourceFile="iQuranResources" /> <mp:SiteLabel 
                     ID="SiteLabel12" runat="server" ForControl="edOthmaniText" CssClass="txterror needsattention"
                    ConfigKey="StarLabel" ResourceFile="iQuranResources" /> <br />
						<portal:mojotextarea id="edOthmaniText" runat="server"></portal:mojotextarea>
					</div>
					<div class="settingrow">
						<mp:SiteLabel ID="SiteLabel8" runat="server" ForControl="edVerseTextNM" CssClass="settinglabeltight"
						ConfigKey="VerseTextNMHeader" ResourceFile="iQuranResources" /> <mp:SiteLabel 
                     ID="SiteLabel11" runat="server" ForControl="edVerseTextNM" CssClass="txterror needsattention"
                    ConfigKey="StarLabel" ResourceFile="iQuranResources" /> <br />
						<portal:mojotextarea id="edVerseTextNM" runat="server"></portal:mojotextarea>
					</div>
					<div class="settingrow">
						<mp:SiteLabel ID="SiteLabel9" runat="server" ForControl="edVerseTextNMAlif" CssClass="settinglabeltight"
						ConfigKey="VerseTextNMAlifHeader" ResourceFile="iQuranResources" /> <mp:SiteLabel 
                     ID="SiteLabel10" runat="server" ForControl="edVerseTextNMAlif" CssClass="txterror needsattention"
                    ConfigKey="StarLabel" ResourceFile="iQuranResources" /> <br />
						<portal:mojotextarea id="edVerseTextNMAlif" runat="server"></portal:mojotextarea>
					</div>
				</div>
				<div id="divTranslation" runat="server" >
					<div class="settingrow">
						<mp:SiteLabel ID="lblTranslation" runat="server" ForControl="edTranslation" CssClass="settinglabeltight"
						ConfigKey="TranslationHeader" ResourceFile="iQuranResources" /> <mp:SiteLabel 
                     ID="SiteLabel3" runat="server" ForControl="edTranslation" CssClass="txterror needsattention"
                    ConfigKey="StarLabel" ResourceFile="iQuranResources" /> <br />
						<portal:mojotextarea id="edTranslation" runat="server"></portal:mojotextarea>
					</div>
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

