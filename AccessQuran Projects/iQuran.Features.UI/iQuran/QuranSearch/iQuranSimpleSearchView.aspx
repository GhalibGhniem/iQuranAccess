<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="iQuranSimpleSearchView.aspx.cs" MasterPageFile="~/App_MasterPages/layout.Master" Inherits="iQuran.Web.UI.QuranSearch.iQuranSimpleSearchView" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" />
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">

    <script type="text/javascript" >
        function iQSelectVerse(mDivID) {

            var doc = document
                    , text = doc.getElementById("iVerse" + mDivID)
                    , range, selection
            ;


            if (doc.body.createTextRange) {
                range = document.body.createTextRange();
                range.moveToElementText(text);
                range.select();
            } else if (window.getSelection) {
                selection = window.getSelection();
                range = document.createRange();
                range.selectNodeContents(text);
                selection.removeAllRanges();
                selection.addRange(range);
            }
        }
    </script>
    <script type="text/javascript" >
        function iQClearSelection() {
            if (document.selection)
                document.selection.empty();
            else if (window.getSelection)
                window.getSelection().removeAllRanges();
        }
    </script>
    <script type="text/javascript" >
        function iQRemeber(suraOrder, verseOrder, verseID) {


            var image = eval("document.all.imgRemeber" + verseID);

            var aReturn = document.body.getElementsByTagName('input');

            if (image.getAttribute('src') == '/Data/SiteImages/iQuran/add.png') {
                for (i = 0; i < aReturn.length; i++) {
                    if ((aReturn[i].value.toString()) == verseID.toString())
                        aReturn[i].checked = true;
                }
                eval("document.all.imgRemeber" + verseID).src = "/Data/SiteImages/iQuran/accept.png";
                eval("document.all.divCopyAllRememberd").style.display = "";
            }
            else {
                for (i = 0; i < aReturn.length; i++) {
                    if ((aReturn[i].value.toString()) == verseID.toString())
                        aReturn[i].checked = false;
                }
                eval("document.all.imgRemeber" + verseID).src = "/Data/SiteImages/iQuran/add.png";

                var flag = "false";

                for (j = 0; j < aReturn.length; j++) {
                    if (aReturn[j].checked == true) {
                        //alert("found one");
                        flag = "true";
                        break;
                    }
                }
                if (flag == "false")
                    eval("document.all.divCopyAllRememberd").style.display = "none";
            }

        }
    </script>
    <script type="text/javascript" >
        function iQCollectRemembered() {

            var aData = document.getElementById("<%=txtRemembered.ClientID%>");
            var aReturn = document.body.getElementsByTagName('input');
            aData.innerHTML = "";
            for (j = 0; j < aReturn.length; j++) {
                if (aReturn[j].checked == true) {
                    aData.innerHTML += aReturn[j].getAttribute('data-');
                }
            }

        }
    </script>
    <script type="text/javascript" >
        /*
         * This is the function that actually highlights a text string by
         * adding HTML tags before and after all occurrences of the search
         * term. You can pass your own tags if you'd like, or if the
         * highlightStartTag or highlightEndTag parameters are omitted or
         * are empty strings then the default <font> tags will be used.
         */
        function doHighlight(bodyText, searchTerm, highlightStartTag, highlightEndTag) {
            // the highlightStartTag and highlightEndTag parameters are optional
            if ((!highlightStartTag) || (!highlightEndTag)) {
                highlightStartTag = "<font style='color:blue; background-color:yellow;'>";
                highlightEndTag = "</font>";
            }

            // find all occurences of the search term in the given text,
            // and add some "highlight" tags to them (we're not using a
            // regular expression search, because we want to filter out
            // matches that occur within HTML tags and script blocks, so
            // we have to do a little extra validation)
            var newText = "";
            var i = -1;
            var lcSearchTerm = searchTerm.toLowerCase();
            var lcBodyText = bodyText.toLowerCase();

            while (bodyText.length > 0) {
                i = lcBodyText.indexOf(lcSearchTerm, i + 1);
                if (i < 0) {
                    newText += bodyText;
                    bodyText = "";
                } else {
                    // skip anything inside an HTML tag
                    if (bodyText.lastIndexOf(">", i) >= bodyText.lastIndexOf("<", i)) {
                        // skip anything inside a <script> block
                        if (lcBodyText.lastIndexOf("/script>", i) >= lcBodyText.lastIndexOf("<script", i)) {
                            newText += bodyText.substring(0, i) + highlightStartTag + bodyText.substr(i, searchTerm.length) + highlightEndTag;
                            bodyText = bodyText.substr(i + searchTerm.length);
                            lcBodyText = bodyText.toLowerCase();
                            i = -1;
                        }
                    }
                }
            }

            return newText;
        }

        /*
 * This is sort of a wrapper function to the doHighlight function.
 * It takes the searchText that you pass, optionally splits it into
 * separate words, and transforms the text on the current web page.
 * Only the "searchText" parameter is required; all other parameters
 * are optional and can be omitted.
 */
        function highlightSearchTerms(searchText, treatAsPhrase, warnOnFailure, highlightStartTag, highlightEndTag) {
            // if the treatAsPhrase parameter is true, then we should search for 
            // the entire phrase that was entered; otherwise, we will split the
            // search string so that each word is searched for and highlighted
            // individually
            if (treatAsPhrase) {
                searchArray = [searchText];
            } else {
                searchArray = searchText.split("-");
            }

            if (!document.body || typeof (document.body.innerHTML) == "undefined") {
                if (warnOnFailure) {
                    alert("Sorry, for some reason the text of this page is unavailable. Searching will not work.");
                }
                return false;
            }

            var bodyText = document.body.innerHTML;
            for (var i = 0; i < searchArray.length; i++) {
                bodyText = doHighlight(bodyText, searchArray[i], highlightStartTag, highlightEndTag);
            }

            document.body.innerHTML = bodyText;
            return true;
        }


    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            var aSearch = document.getElementById("<%=divTextSearchWord.ClientID%>").title;
            //alert(aSearch);
            if (aSearch.length > 0)
                highlightSearchTerms(aSearch, false);
        });
    </script> 
    
    <asp:HiddenField ID="hdnPageSize" Value="-1" runat="server" />

    <asp:HiddenField ID="hdnQuranID" Value="-1" runat="server" />
    <asp:HiddenField ID="hdnSuraIDFrom" Value="-1" runat="server" />
    <asp:HiddenField ID="hdnSuraIDTo" Value="-1" runat="server" />
    <asp:HiddenField ID="hdnVerseOrderFrom" Value="-1" runat="server" />
    <asp:HiddenField ID="hdnVerseOrderTo" Value="-1" runat="server" />
    <asp:HiddenField ID="hdnIsDefault" Value="-1" runat="server" />

    <asp:HiddenField ID="hdnTextSearchWordOrRoot" Value="" runat="server" />
    <asp:HiddenField ID="hdnTextSearchWord" Value="" runat="server" />
    <asp:HiddenField ID="hdnSearchedTextType" Value="" runat="server" />
    <asp:HiddenField ID="hdnFontType" Value="" runat="server" />
    <asp:HiddenField ID="hdnSerachCriteria" Value="" runat="server" />

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
              
            <div class="form_row back_url">
                 <asp:HyperLink ID="hlBack1" runat="server" EnableViewState="false"
                    Text='<%$ Resources:iQuranCommandsResources,Back %>'
                    NavigateUrl='' CssClass="jqbutton ui-button ui-widget ui-state-default ui-corner-all" >
                </asp:HyperLink>

	        </div>
            
            <div class="btnSrchHeader" id="divCopyAllRememberd" style="display:none" > 
                <asp:Button id="btnShowRemembered" Text="<%$ Resources:iQuranResources,CopyAllRemebered %>" runat="server"
                    CssClass="jqbutton ui-button ui-widget ui-state-default ui-corner-all" 
                    OnClientClick='javascript:iQCollectRemembered()' />
                <ajaxToolKit:ModalPopupExtender ID="mdlPopupRemembered" runat="server"  TargetControlID="btnShowRemembered" 
                    PopupControlID="pnlRemembered" CancelControlID="btnCloseRemembered" />
            </div>
            <div id="divResltsOfSearch" >
            <asp:Repeater ID="rptVerses"  runat="server" >
                <HeaderTemplate><div  class="art-article">
                   
                <table>
                </HeaderTemplate>
                <FooterTemplate> </table></div></FooterTemplate>
                <ItemTemplate>
                    <thead >
    	                <tr>
                            <th scope="col">
                                <asp:Literal ID="litNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "VerseNo").ToString()%>'></asp:Literal>
                            </th>
        	                <th scope="col">
                                
                                <a href='javascript:iQRemeber(<%# Eval("suraOrder") +"," + Eval("VerseOrder") +"," + Eval("VerseID") %>)'>
                                    <img name='imgRemeber<%# Eval("VerseID") %>' 
                                    border='0' src='/Data/SiteImages/iQuran/add.png'  >
                                </a>

                                 <input type="checkbox" name='cbRememberVerse' id='cbRememberVerse'
                                     visible="true" style="display:none"      runat="server" 
                                     value='<%# DataBinder.Eval(Container.DataItem, "VerseID").ToString()%>'
                                     data-='<%# 
                                               Resources.iQuranResources.SuraHeader + " - " +  DataBinder.Eval(Container.DataItem,"sTitle")  + " - " +  
                                                Resources.iQuranResources.VerseHeader  + " - " +   DataBinder.Eval(Container.DataItem, "VerseOrder")
                                                 + "<br /> <br />" +
                                                 " <span class=" + "quran-brawn-meq" + "> " + 
                                                DataBinder.Eval(Container.DataItem, "VerseText")
                                                + " </span> "
                                                + "<br /> <br />" 
                                                %>'
                                     />   
                              

                            </th>
        	                <th scope="col"> 
                                <asp:HyperLink Font-Underline="true" ID="hlSuraTitle" Target="_blank" runat="server" EnableViewState="false"
                                    Text='<%# DataBinder.Eval(Container.DataItem,"sTitle") %>' 
                                    NavigateUrl='<%# FormatSuraTitleUrl("", Convert.ToInt32(DataBinder.Eval(Container.DataItem,"QuranID")), Convert.ToInt32(DataBinder.Eval(Container.DataItem,"SuraID")), Convert.ToInt32(DataBinder.Eval(Container.DataItem,"VerseID")), Convert.ToInt32(DataBinder.Eval(Container.DataItem,"VerseOrder")) )  %>' 
                                    >
                                </asp:HyperLink>
                                 <mp:SiteLabel ID="SiteLabelSuraHeader" ForControl="hlSuraTitle" 
							        ConfigKey="SuraHeader" ResourceFile="iQuranResources" runat="server"  />
							    <asp:Label ID="lblSuraSortOrder" ForControl="SiteLabelSuraHeader"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "SuraOrder")%>' runat="server" /> 
                                <mp:SiteLabel ID="lblseperator1" ForControl="lblSuraSortOrder"   
							        ConfigKey="SeperatorDash" ResourceFile="iQuranResources" runat="server" /> 
                                <mp:SiteLabel ID="SiteLabelVerseHeader" ForControl="lblseperator1"  
							        ConfigKey="VerseHeader" ResourceFile="iQuranResources" runat="server" />
							    <asp:Label ID="lblVerseSortOrder" ForControl="SiteLabelVerseHeader"
                                     Text='<%# DataBinder.Eval(Container.DataItem, "VerseOrder")%>' runat="server"  />

                                <mp:SiteLabel ID="lblseperator2" ForControl="lblVerseSortOrder"  
							        ConfigKey="SeperatorDash" ResourceFile="iQuranResources" runat="server"  /> 
                                <mp:SiteLabel ID="SiteLabelPartHeader" ForControl="lblseperator2" runat="server" 
							        ConfigKey="PartNoHeader" ResourceFile="iQuranResources" />
							    <asp:Label ID="lblVersePartNo" ForControl="SiteLabelPartHeader"
                                    Text='<%# DataBinder.Eval(Container.DataItem, "partNo")%>' runat="server"  />

                                 <mp:SiteLabel ID="lblseperator3" ForControl="lblVersePartNo"  
							        ConfigKey="SeperatorDash" ResourceFile="iQuranResources" runat="server"  /> 
                                <mp:SiteLabel ID="SiteLabelHizbHeader" ForControl="lblseperator3" runat="server" 
							        ConfigKey="HizbNoHeader" ResourceFile="iQuranResources" />
							    <asp:Label ID="lblVerseHizbNo" ForControl="SiteLabelHizbHeader"
                                    Text='<%# DataBinder.Eval(Container.DataItem, "hizbNo")%>' runat="server"  />
                               
                            </th>
        	                
                            <th scope="col"> 

                              <div class="bsocial" id="bsocial" runat="server" Visible='<%# ShowSocialDiv %>'>
                           <portal:AddThisWidget  ID="addThisWidget" runat="server" AccountId='<%# addThisAccountId %>' 
                               SkinID="BlogList"  TitleOfUrlToShare='<%# DataBinder.Eval(Container.DataItem,"VerseText") %>' 
                               UrlToShare='<%# FormatVerseTitleUrl("", Convert.ToInt32(DataBinder.Eval(Container.DataItem,"QuranID")), Convert.ToInt32(DataBinder.Eval(Container.DataItem,"VerseID"))  )  %>' 
                               Visible='<%# ShowAddThisButton %>' EnableViewState="false" />
                      
                       </div>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td colspan="4">
                                <div class="form_row">
                                    <div class="right" >
                                        <div class="selectVerse" id='iVerse<%# Eval("VerseID") %>'   
                                            onmouseover='javascript:iQSelectVerse(<%# Eval("VerseID") %>)'  >
                                            <span class='quran-brawn-meq'> <%# DataBinder.Eval(Container.DataItem, "VerseText")%> </span>
                                        </div>
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
            <div class="form_row back_url">
                <asp:HyperLink ID="hlBack2" runat="server" EnableViewState="false"
                    Text='<%$ Resources:iQuranCommandsResources,Back %>'
                    NavigateUrl='' CssClass="jqbutton ui-button ui-widget ui-state-default ui-corner-all" >
                </asp:HyperLink>
	        </div>
            </asp:Panel>

            <!-- Pop up for remembered verses-->
            <asp:Panel ID="pnlRemembered" runat="server" style="display:none" Width="500px">
            <asp:UpdatePanel ID="updPnlRemembered" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modalpopup" >
                        
                        <div class="modalpopuptitle">
                            <mp:SiteLabel ID="lblRememberedHeader" runat="server" ForControl="" 
                            ResourceFile="iQuranResources" ConfigKey="RemeberedVersesHeader"></mp:SiteLabel>
                        </div>
                        
                        <div class="modalcontent">
                            <div class="selectVerse" >
                                <asp:Label ID="txtRemembered" Visible="true" runat="server" ForControl="" Text="" />
                            </div>
                        </div>

                        <div class="modalbuttonspane">
                                <div class="modalbuttonset">
                            <asp:Button ID="btnCloseRemembered" CommandName="Cancel" runat="server" 
                                Text="<%$ Resources:iQuranCommandsResources,Close %>"
                                 CssClass="jqbutton ui-button ui-widget ui-state-default ui-corner-all"
                                 onclick="btnSaveCompContact_Click" />
                                   
                        </div>
                    </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            </asp:Panel>
        </div>
     
        <div ID="divTextSearchWord" title="" style="display:none" runat="server" ></div>
    </portal:InnerBodyPanel>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server" />
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />

