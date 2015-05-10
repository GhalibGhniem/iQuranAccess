<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="iQuranIndex.aspx.cs" MasterPageFile="~/App_MasterPages/layout.Master" Inherits="iQuran.Web.UI.QuranSearch.iQuranIndex" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" />
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">

    <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
        <h3 class="moduletitlle"><span id="spnTitle" runat="server"></span></h3><br />
        <div id="divDescription" runat="server" class="msgnoteborder">
            <div class="msgnote">
                <asp:Literal ID="litSearchDescription" runat="server" />
            </div>
        </div>	
         <asp:Label ID="lblmessage" Visible="false" runat="server" ForControl="" CssClass="txterror" />
        <div class="modulecontent">
           <asp:Panel  ID="pnlSearchList" runat="server"  SkinID="plain">

               
               <!-- quran version Select -->
                <div class="form_row">
                    <div class="right right_lable">
                        <mp:SiteLabel ID="SiteLabel9" runat="server" ForControl="ddSelQuran" CssClass="settinglabel"
											        ResourceFile="iQuranResources" ConfigKey="SelectiQuran"></mp:SiteLabel>
                        <portal:mojoHelpLink ID="MojoHelpLink1" runat="server" HelpKey="iquran-FrontSearch-SelectQuranVersion-help" />
                            
                    </div>
                    <div class="right">
		                <asp:DropDownList ID="ddQuran" AutoPostBack="true"
                                DataValueField="QuranID" DataTextField="Title" runat="server" OnSelectedIndexChanged="ddQuran_SelectedIndexChanged"></asp:DropDownList>
		                <mp:SiteLabel 
                                ID="SiteLabel10" runat="server" ForControl="ddSelQuran" CssClass="txterror needsattention"
                            ConfigKey="StarLabel" ResourceFile="iQuranResources" />
                            
		            <br />
                    </div>
                </div>
                   
                <asp:Repeater ID="rptVerses"  runat="server" >
                    <HeaderTemplate><div  class="art-article">
                  
                    <table>
                         <thead >
    	                    <tr>
                                <th scope="col">
                                    <mp:SiteLabel ID="SiteLabel4" ForControl="" 
							            ConfigKey="Number" ResourceFile="iQuranResources" runat="server"  />
                                </th>
        	                    <th scope="col">
                                    <mp:SiteLabel ID="SiteLabel1" ForControl="" 
							            ConfigKey="SuraHeader" ResourceFile="iQuranResources" runat="server"  />
                                </th>
                                 <th scope="col">
                                    <mp:SiteLabel ID="SiteLabel2" ForControl="" 
							            ConfigKey="VersesCountHeader" ResourceFile="iQuranResources" runat="server"  />
                                </th>
                                 <th scope="col">
                                    <mp:SiteLabel ID="SiteLabel3" ForControl="" 
							            ConfigKey="PlaceHeader" ResourceFile="iQuranResources" runat="server"  />
                                </th>
                            </tr>
                        </thead>
                    </HeaderTemplate>
                    <FooterTemplate> </table></div></FooterTemplate>
                    <ItemTemplate>
                       
                        <tbody>
                            <tr>
                                <td >
                                    <asp:Literal ID="Literal1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SuraOrder").ToString()%>'></asp:Literal>
                                </td>
                                <td >
                                    <asp:HyperLink Font-Underline="true" ID="HyperLink1" Target="_blank" runat="server" EnableViewState="false"
                                        Text='<%# DataBinder.Eval(Container.DataItem,"Title") %>' 
                                        NavigateUrl='<%# FormatSuraTitleUrl("", Convert.ToInt32(DataBinder.Eval(Container.DataItem,"QuranID")), Convert.ToInt32(DataBinder.Eval(Container.DataItem,"SuraID")), Convert.ToInt32(DataBinder.Eval(Container.DataItem,"PageNumber")) )  %>' 
                                        >
                                    </asp:HyperLink>

                                </td>
                                <td >
                                    <asp:Literal ID="Literal3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "VersesCount").ToString()%>'></asp:Literal>
                                </td>
                                <td >
                                    <asp:Literal ID="Literal4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Place").ToString()%>'></asp:Literal>
                                </td>
                            </tr>
                        </tbody>
                    </ItemTemplate>
                </asp:Repeater> 
            
           </asp:Panel>
        </div>
    </portal:InnerBodyPanel>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server" />
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />

