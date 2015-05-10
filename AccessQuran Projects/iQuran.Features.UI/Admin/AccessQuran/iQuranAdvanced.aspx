<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_MasterPages/layout.Master" CodeBehind="iQuranAdvanced.aspx.cs" Inherits="iQuran.Web.UI.AdminUI.iQuranAdvanced" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" />
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    <div class="breadcrumbs">
       <asp:HyperLink ID="lnkAdminMenu" runat="server" CssClass="selectedcrumb" />&nbsp;&gt;
    </div>
    <portal:mojoPanel ID="mp1" runat="server" ArtisteerCssClass="art-Post" RenderArtisteerBlockContentDivs="true">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <asp:Panel ID="pnl1" runat="server" CssClass="art-Post-inner panelwrapper adminmenu">
        <portal:HeadingControl id="heading" runat="server" />
            <portal:mojoPanel ID="MojoPanel1" runat="server" ArtisteerCssClass="art-PostContent">
            <asp:Label id="lblError" runat="server" CssClass="txterror"></asp:Label>
        <div class="modulecontent">
            <ul class="simplelist">
                <li id="liiQuranManager" runat="server" visible="false">
                    <asp:HyperLink ID="lnkiQuranManager" runat="server" CssClass="lnkiQuranManager" /><br /><br />
                </li>
				 <li id="liiSuraManager" runat="server" visible="false">
                    <asp:HyperLink ID="lnkiSuraManager" runat="server" CssClass="lnkiSuraManager" /><br /><br />
                </li>
                <li id="liiVerseManager" runat="server" visible="false">
                    <asp:HyperLink ID="lnkiVerseManager" runat="server" CssClass="lnkiVerseManager" /><br /><br />
                </li>
				
                <asp:Literal ID="litSupplementalLinks" runat="server" />
            </ul>
        </div>
            </portal:mojoPanel>
        <div class="cleared"></div>
    </asp:Panel>
    <mp:CornerRounderBottom ID="cbottom1" runat="server" />
    </portal:mojoPanel>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server" />
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />

