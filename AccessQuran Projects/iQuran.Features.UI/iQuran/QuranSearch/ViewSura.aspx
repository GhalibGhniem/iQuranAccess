<%@ Page Language="C#" MasterPageFile="~/App_MasterPages/layout.Master" AutoEventWireup="false" CodeBehind="ViewSura.aspx.cs" Inherits="iQuran.Web.UI.QuranSearch.ViewSura" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" />
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">

    <asp:HiddenField ID="hdnQuranID" Value="-1" runat="server" />
    <asp:HiddenField ID="hdnSuraID" Value="-1" runat="server" />
    <asp:HiddenField ID="hdnsuraOrder" Value="-1" runat="server" />
    <asp:HiddenField ID="hdnVerseID" Value="-1" runat="server" />
    <asp:HiddenField ID="hdnverseOrder" Value="-1" runat="server" />

    <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
        
        <div id="divDescription" runat="server" class="msgnoteborder">
            <div class="msgnote">
                <asp:Literal ID="litSearchDescription" runat="server" />
            </div>
        </div>	
         <asp:Label ID="lblmessage" Visible="false" runat="server" ForControl="" CssClass="txterror" />

        <div class="modulecontent">
            <asp:Panel  ID="pnlSura" runat="server"  SkinID="plain">

            <div id="divBism" Visible="false" runat="server" ><span class='quran-brawn-meq'><asp:Literal Visible="false" ID="litBism" runat="server" /></span></div>
            
            <asp:Repeater ID="rptVerses"  runat="server" >
                <HeaderTemplate><div  class="art-article">
                <table>
                </HeaderTemplate>
                <FooterTemplate> </table></div></FooterTemplate>
                <ItemTemplate>
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

             
           </asp:Panel>
        </div>
    </portal:InnerBodyPanel>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server" />
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />

