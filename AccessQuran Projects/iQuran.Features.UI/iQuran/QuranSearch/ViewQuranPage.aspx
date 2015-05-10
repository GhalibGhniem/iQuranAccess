<%@ Page Language="C#" MasterPageFile="~/App_MasterPages/layout.Master" AutoEventWireup="false" CodeBehind="ViewQuranPage.aspx.cs" Inherits="iQuran.Web.UI.QuranSearch.ViewQuranPage" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" />
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">

    <asp:HiddenField ID="hdnQuranID" Value="-1" runat="server" />
    <asp:HiddenField ID="hdnSuraID" Value="-1" runat="server" />
    <asp:HiddenField ID="hdnsuraOrder" Value="-1" runat="server" />
    <asp:HiddenField ID="hdnVerseID" Value="-1" runat="server" />
    <asp:HiddenField ID="hdnverseOrder" Value="-1" runat="server" />
    <asp:HiddenField ID="hdnIsTranslation" Value="-1" runat="server" />


    <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
        
        <div id="divDescription" runat="server" class="msgnoteborder">
            <div class="msgnote">
                <asp:Literal ID="litSearchDescription" runat="server" />
            </div>
        </div>	
         <asp:Label ID="lblmessage" Visible="false" runat="server" ForControl="" CssClass="txterror" />
        <asp:HyperLink ID="hlViewSura" runat="server" EnableViewState="false" Visible="false"
                    Text='<%$ Resources:iQuranResources,ViewWholeSura %>'
                    NavigateUrl='' CssClass="jqbutton ui-button ui-widget ui-state-default ui-corner-all" >
                </asp:HyperLink>
        <div class="modulecontent">
            <asp:Panel  ID="pnlSura" runat="server"  SkinID="plain">
            

            <asp:Repeater ID="rptVerses"  runat="server" >
                <HeaderTemplate><div  class="art-article">
                <table>
                </HeaderTemplate>
                <FooterTemplate> </table></div></FooterTemplate>
                <ItemTemplate>
                   <tbody id="trBism" runat="server" Visible='<%# (bool)(int.Parse(DataBinder.Eval(Container.DataItem, "VerseOrder").ToString()) == 1) %>'>
                       <tr  >
                            <td colspan="2">
                                    <span class='SuraTitle'><mp:SiteLabel ID="SiteLabelSuraHeader" ForControl="" 
							        ConfigKey="SuratHeader" ResourceFile="iQuranResources" runat="server"  /> &nbsp; <%# DataBinder.Eval(Container.DataItem, "sTitle").ToString()%></span>
                            </td>
                        </tr>
                        <tr  >
                            <td colspan="2">
                                    <span class='quran-brawn-meq'><asp:Literal Text='<%# GetBismVerse(Convert.ToInt32(DataBinder.Eval(Container.DataItem,"QuranID")), Convert.ToInt32(DataBinder.Eval(Container.DataItem,"SuraID")) )  %>' ID="litBism" runat="server" /></span>
                            </td>
                        </tr>
                    </tbody>
                    <tbody>
                        <tr>
                            <td>
                                <asp:Literal ID="Literal1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "VerseOrder").ToString()%>'></asp:Literal>
                            </td>
                            <td>
                                <div class="form_row">
                                    <div class="right" >
                                        <span class='quran-brawn-meq'><%# DataBinder.Eval(Container.DataItem, "VerseText")%></span>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </ItemTemplate>
            </asp:Repeater> 

            
            <div class="projectpager">
                <portal:mojoCutePager ID="pgr" runat="server"  />
            </div>

             
           </asp:Panel>
        </div>
    </portal:InnerBodyPanel>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server" />
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />

