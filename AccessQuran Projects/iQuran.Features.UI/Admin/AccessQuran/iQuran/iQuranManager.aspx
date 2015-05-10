<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master" CodeBehind="iQuranManager.aspx.cs" Inherits="iQuran.Web.UI.AdminUI.iQuranManager" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" />
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
<mp:CornerRounderTop id="ctop1" runat="server" />
<asp:Panel ID="pnlWrapper" runat="server" CssClass="panelwrapper admin rolemanager">
<div class="modulecontent">
<fieldset>
<legend>
<asp:HyperLink ID="lnkAdminMenu" runat="server" Visible="true" NavigateUrl="~/Admin/AdminMenu.aspx" />&nbsp;&gt;<asp:HyperLink ID="lnkAdvanced" runat="server" NavigateUrl="" />&nbsp;&gt;
<asp:HyperLink ID="lnkiQuranManagerAdmin" runat="server" />
</legend><br /><br />
    <legend><span id="spnTitle" runat="server"></span></legend>
                <br />
    <div class="modalbuttonset"><asp:HyperLink ID="lnkAddNew" Visible="false" runat="server" NavigateUrl="" /></div>
    <br />
    <div class="settingrow">
        <mp:SiteLabel ID="lblSearchByTitle" runat="server" ForControl="txtSearchByTitle" CssClass="settinglabel"
                                         ResourceFile="iQuranResources" ConfigKey="SearchByTitleHeader"></mp:SiteLabel>&nbsp;&nbsp;
        <asp:Textbox id="txtSearchByTitle" width="200" MaxLength="100" CssClass="mediumtextbox" runat="server" />
        &nbsp;&nbsp;<asp:ImageButton  CausesValidation="false" ImageUrl="/Data/SiteImages/search.gif"
        runat="server" ID="btnSearchTitle" OnClick="btnSearchTitle_Click"  />
        <br />
    </div>
    <div class="settingrow">
    <mp:SiteLabel ID="lblSelectType"  runat="server" ForControl="ddStatus" CssClass="settinglabel"
                                         ResourceFile="iQuranResources" ConfigKey="SelectByStatusLabel"></mp:SiteLabel>
        <asp:DropDownList AutoPostBack="true" ID="ddStatus" runat="server" OnSelectedIndexChanged="ddStatus_SelectedIndexChanged">
            <asp:ListItem Value="0"  Text="<%$ Resources:iQuranResources, SelectAllHeader %>" />
            <asp:ListItem Value="1" Text="<%$ Resources:iQuranResources, ActiveHeader %>" />
            <asp:ListItem Value="2" Text="<%$ Resources:iQuranResources, NotActiveHeader %>" />
        </asp:DropDownList>
    </div>
    <div id="divMsg" runat="server" >
                <asp:Label ID="lblmessage" Visible="false" runat="server" ForControl="" CssClass="txterror needsattention" />
            </div>
    <br /><br />
    <div style="border-bottom:dashed 1px #666; ">&nbsp;</div>
<div class="settinglabel">&nbsp;</div>
<!-- Start DATA VIEW -->
    <div class="settingrow">
        <asp:GridView ID="iQGrid" runat="server"
             AllowPaging="true"
             AllowSorting="true"
             CssClass="editgrid"
	         AutoGenerateColumns="false"
             DataKeyNames="QuranID"
             EnableTheming="false"
				       >
         <Columns>
		        <asp:TemplateField SortExpression="Title"  ItemStyle-Font-Bold="true" ItemStyle-Wrap="true"  ItemStyle-Width="70%">
                    <HeaderTemplate>
                        <asp:Label Text='<%# Resources.iQuranResources.TitleHeader %>' runat="server" ID="label20"  />
                    </HeaderTemplate>
			        <ItemTemplate>
                        <img alt="" hspace="5" src="/Data/SiteImages/node.gif" /> &nbsp;
                            <%# Eval("Title") %>
                    </ItemTemplate>
		        </asp:TemplateField>
		        <asp:TemplateField SortExpression="IsActive">
                    <HeaderTemplate>
                        <asp:Label Text='<%# Resources.iQuranResources.ActiveHeader %>' runat="server" ID="label22"  /><br />
                    </HeaderTemplate>
			        <ItemTemplate>
                        <%# (System.Convert.ToBoolean(Eval("IsActive")))? "Active": "Canceled" %>
                    </ItemTemplate>
		        </asp:TemplateField>	
 		        <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label Text='<%# Resources.iQuranCommandsResources.Edit %>' runat="server" ID="lblEdit1"  />
                    </HeaderTemplate>
			        <ItemTemplate>
                             <asp:HyperLink ID="HyperLink1" runat="server" CssClass="ModuleEditLink" 
                            ImageUrl='<%# EditPropertiesImage %>' 
                            NavigateUrl='<%# SiteRoot + "/Admin/AccessQuran/iQuran/iQuranEdit.aspx?qid=" + Eval("QuranID") %>'  
                            Text='<%# Resources.iQuranCommandsResources.Edit %>' ToolTip='<%# Resources.iQuranCommandsResources.Edit %>' 
                             />
                    </ItemTemplate>
		        </asp:TemplateField>	             	
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label Text='<%# Resources.iQuranCommandsResources.Delete %>' runat="server" ID="lblDelete"  />
                    </HeaderTemplate>
			        <ItemTemplate>
                        <asp:ImageButton ImageUrl='<%# DeleteLinkImage %>' CommandArgument='<%# Eval("QuranID") %>' CommandName="delete" 
							AlternateText="<%# Resources.iQuranMessagesResources.DeleteQuranWarning %>" 
							ToolTip="<%# Resources.iQuranMessagesResources.DeleteQuranTip %>" 
							runat="server" ID="btnDelete"  />     
                    </ItemTemplate>
		        </asp:TemplateField>	 
        </Columns> 
        </asp:GridView>
    </div>
<!-- End Data VIEW -->
<!-- START PAGER -->
    <div class="modulepager">
        <mp:CutePager ID="amsPager" runat="server" />
    </div>
    <br class="clear" />
<!-- END PAGER -->
	<div style="border-bottom:dashed 1px #666; ">&nbsp;</div>
</fieldset>
</div>
</asp:Panel>
<mp:CornerRounderBottom id="cbottom1" runat="server" />

</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server" />
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />
