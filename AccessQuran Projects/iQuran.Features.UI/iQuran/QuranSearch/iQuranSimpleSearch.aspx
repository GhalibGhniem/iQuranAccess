<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="iQuranSimpleSearch.aspx.cs" MasterPageFile="~/App_MasterPages/layout.Master" Inherits="iQuran.Web.UI.QuranSearch.iQuranSimpleSearch" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" />
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
    
    <script type="text/javascript" >
        function iQRestrictCriteria() {
            /*
            sender rbSearchedTextType if he chosed i - independet word or p - part from word
            control to restrict : txtSearchWord 
            To pervent write spaces - more than one word if he chosed i or p
            */

            var oRadioCriterias = document.getElementById("<%=rbSearchedTextType.ClientID%>");
            var oRadioCriteriasInput = oRadioCriterias.getElementsByTagName("input");
            var rValCriteria;

            var oTxtToRestrict = document.getElementById("<%=txtSearchWord.ClientID%>");

            for (var i = 0; i < oRadioCriteriasInput.length; i++) {
                if (oRadioCriteriasInput[i].checked) {
                    rValCriteria = oRadioCriteriasInput[i].value;
                    //alert(rValCriteria);
                    if (rValCriteria == 'v') {
                        oTxtToRestrict.attributes.removeNamedItem('onkeydown');
                        // alert(rValCriteria);
                    }
                    else {
                        oTxtToRestrict.setAttribute('onkeydown', 'javascript:iQSpacesKeyHandler(event)');
                        //alert(rValCriteria);
                        // if there is words with spaces from previous search remove them 
                        var enteredTxt = oTxtToRestrict.value;
                        if (enteredTxt.indexOf(' ') >= 0) {
                            // alert(enteredTxt);
                            oTxtToRestrict.value = '';
                        }

                    }
                }
            }
        }
    </script>

    <script type="text/javascript" >
        function iQSelectCriteria() {

            var oRadioFontType = document.getElementById("<%=rbWordFontType.ClientID%>");
            var oRadioFontTypeInput = oRadioFontType.getElementsByTagName("input");

            var odivOthmCriteria = document.getElementById("<%=divOthmCriteria.ClientID%>");
            var odivDictCriteria = document.getElementById("<%=divDictCriteria.ClientID%>");

            //alert("hi3");
            for (var i = 0; i < oRadioFontTypeInput.length; i++) {
                if (oRadioFontTypeInput[i].checked) {
                    var rVal = oRadioFontTypeInput[i].value;
                    if (rVal == "TypeOthm") {
                        odivOthmCriteria.style.display = "block";
                        odivDictCriteria.style.display = "none";
                    }
                    else {
                        odivOthmCriteria.style.display = "none";
                        odivDictCriteria.style.display = "block";
                    }
                    break;
                }
            }
        }

    </script>

    <script type="text/javascript">
        function iQSpacesKeyHandler(e) {
            if (e.which == 32) {
                var txtSearch = document.getElementById("<%=txtSearchWord.ClientID%>")
                var enteredTxt = txtSearch.value;
                newEnteredTxt = enteredTxt.substr(0, enteredTxt.length + 1);
                //alert('/' + newEnteredTxt + '/');
                var aMsg = document.getElementById("<%=divTextMsgSpacesToSend.ClientID%>").title;
                alert(aMsg);
                document.getElementById("<%=txtSearchWord.ClientID%>").value = newEnteredTxt;
            }
        }
</script>

    <style type="text/css">
        .Test {font-size: 18px;padding:10px 30px 16px 30px;background:#fefefe;box-shadow: 0 1px 3px rgba(30, 50, 70, 0.3);width:500px;	height:323px;margin:50px auto;}
        
    </style>
    <asp:HiddenField ID="hdnPageSize" Value="20" runat="server" />

      <asp:HiddenField ID="hdnFromWhere" runat="server" />
    <asp:HiddenField ID="hdnQuranID" Value="-1" runat="server" />
    <asp:HiddenField ID="hdnSuraIDFrom" Value="-1" runat="server" />
    <asp:HiddenField ID="hdnSuraIDTo" Value="-1" runat="server" />
    <asp:HiddenField ID="hdnVerseOrderFrom" Value="1" runat="server" />
    <asp:HiddenField ID="hdnVerseOrderTo" Value="6" runat="server" />
    <asp:HiddenField ID="hdnIsDefault" Value="-1" runat="server" />

    <asp:HiddenField ID="hdnTextSearchWordOrRoot" Value="" runat="server" />
    <asp:HiddenField ID="hdnTextSearchWord" Value="" runat="server" />
    <asp:HiddenField ID="hdnSearchedTextType" Value="" runat="server" />
    <asp:HiddenField ID="hdnFontType" Value="" runat="server" />
    <asp:HiddenField ID="hdnSerachCriteria" Value="" runat="server" />

    <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">

       
        <div class="modulecontent">
            <!-- Simple Search Box-->
            <asp:Panel ID="pnlSearchBox" runat="server"  SkinID="plain">
                <h3 class="moduletitlle"><span id="spnTitle" runat="server"></span></h3><br />
                <asp:Label ID="lblmessage" Visible="false" runat="server" ForControl="" CssClass="txterror" />

                <div class="SearchBox">
		
                    <div class="form_row top_row">
                        <mp:SiteLabel ID="SiteLabel1" runat="server" ForControl="" CssClass="settinglabel"
                            ConfigKey="SearchForWordSenetence" ResourceFile="iQuranResources" /><portal:mojoHelpLink ID="MojoHelpLink7" runat="server" HelpKey="iquran-FrontSearch-SimpleSearch-help" />
                    </div>

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
                   
                    <!-- text search -->
                    <div class="form_row2" >
                        <div id="divTextSelection" runat="server" visible="true">
	                        <div class="right right_lable">
                                <mp:SiteLabel ID="lblreqtxtSearchWord" runat="server" ForControl="" CssClass="settinglabel"
                                    ConfigKey="SearchFor" ResourceFile="iQuranResources" />
                                <portal:mojoHelpLink ID="MojoHelpLink2" runat="server" HelpKey="iquran-FrontSearch-TextBoxSearch-help" />
	                        </div> 
		                    <div class="right">
                                <asp:Textbox id="txtSearchWord"  onkeydown='javascript:iQSpacesKeyHandler(event)'
                                     width="200" MaxLength="250"  CssClass="verywidetextbox forminput" runat="server" />
			                    
		                    </div>
                        </div>
                        
		                <div class="right center_lable">
                            <portal:mojoButton ID="btnSearch" runat="server" />
		                </div>
                        <br />
                        <div id="divWantedWord" runat="server">
	                        <div class="right right_lable">
                                <mp:SiteLabel ID="SiteLabel3" runat="server" ForControl="" CssClass="settinglabel"
                                    ConfigKey="SearchWantedWord" ResourceFile="iQuranResources" />
                                <portal:mojoHelpLink ID="MojoHelpLink3" runat="server" HelpKey="iquran-FrontSearch-TextBoxSearch-TextType-help" />
	                        </div> 
		                    <div class="right" >
                                <asp:RadioButtonList ID="rbSearchedTextType" runat="server"  RepeatDirection="Horizontal" onclick ="javascript:iQRestrictCriteria()" >
                                    <asp:ListItem Selected="True" Text="<%$ Resources:iQuranOptionsResources,OptionSearchedTextTypeStandAloneText %>"
                                        Value="<%$ Resources:iQuranOptionsResources,OptionSearchedTextTypeStandAloneValue %>"></asp:ListItem>
                                    <asp:ListItem  Text="<%$ Resources:iQuranOptionsResources,OptionSearchedTextTypePartFromWordText %>" 
                                        Value="<%$ Resources:iQuranOptionsResources,OptionSearchedTextTypePartFromWordValue %>"></asp:ListItem>
                                     <asp:ListItem Text="<%$ Resources:iQuranOptionsResources,OptionSearchedTextTypePartFromVerseText %>" 
                                        Value="<%$ Resources:iQuranOptionsResources,OptionSearchedTextTypePartFromVerseValue %>"></asp:ListItem>
                                </asp:RadioButtonList>
	                        </div>
                        </div>
	                </div>
                     
                    <!-- root search -->
                    <div class="form_row"  id="divRootSelection" runat="server">
                        <div class="right right_lable">
                            <mp:SiteLabel ID="SiteLabel13" runat="server" ForControl="" CssClass="settinglabel"
                                ConfigKey="SelectRoot" ResourceFile="iQuranResources" />
                            <portal:mojoHelpLink ID="MojoHelpLink4" runat="server" HelpKey="iquran-FrontSearch-RootSearch-help" />
	                    </div> 
		                <div class="right">
                            <asp:DropDownList ID="ddRoot" DataValueField="Root" DataTextField="Root" runat="server"></asp:DropDownList>
		                </div>
                         <div class="right center_lable">
                            <portal:mojoButton ID="btnSearchByRoot" text="search root" runat="server" />
		                </div>
	                </div>
                    
                    <!-- Selct Font types -->
                    <div class="form_row2" id="divSearchedTextType" runat="server" >
                        <div class="right right_lable">
                            <mp:SiteLabel ID="SiteLabel5" runat="server" ForControl="" CssClass="settinglabel"
                                ConfigKey="SearchWordFontType" ResourceFile="iQuranResources" />
                            <portal:mojoHelpLink ID="MojoHelpLink5" runat="server" HelpKey="iquran-FrontSearch-FontType-help" />
                        </div>
                        <div class="right">
                           <!--   Dictional Or Othmani? : TypeOthm, TypeDict 
                               toview or hide : rbWordDictCriteria rbWordOthmCriteria -->
                            <asp:RadioButtonList ID="rbWordFontType" runat="server"  RepeatDirection="Horizontal"  onclick ="javascript:iQSelectCriteria()" >
                                 <asp:ListItem Selected="True" Text="<%$ Resources:iQuranOptionsResources,OptionSearchFontTypeDictionalText %>" 
                                    Value="<%$ Resources:iQuranOptionsResources,OptionSearchFontTypeDictionalValue %>"></asp:ListItem>  
                                <asp:ListItem  Text="<%$ Resources:iQuranOptionsResources,OptionSearchFontTypeOthmaniText %>" 
                                    Value="<%$ Resources:iQuranOptionsResources,OptionSearchFontTypeOthmaniValue %>"></asp:ListItem>
                            </asp:RadioButtonList>
	                    </div>
                        <br />
                        
                        <div id="divDictCriteria" runat="server" style='display:block'>
                        <div class="rightOptions" >
                            <!--   Dictional : DictNMAlif, DictNM " -->
                            <asp:RadioButtonList ID="rbWordDictCriteria" runat="server"  RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Text="<%$ Resources:iQuranOptionsResources,OptionSearchWordCriteriaWordDictationalNMAlifText %>" 
                                    Value="<%$ Resources:iQuranOptionsResources,OptionSearchWordCriteriaWordDictationalNMAlifValue %>"></asp:ListItem>
                                 <asp:ListItem Text="<%$ Resources:iQuranOptionsResources,OptionSearchWordCriteriaWordDictationalNMText %>" 
                                    Value="<%$ Resources:iQuranOptionsResources,OptionSearchWordCriteriaWordDictationalNMValue %>"></asp:ListItem>
                            </asp:RadioButtonList>
	                    </div>
                        </div>
                        <br />

                        
                        <div id="divOthmCriteria"  runat="server" class="OtmCriteria" style='display:none'>
                        <div class="rightOptions">
                           <!--  Othmani: OthmNMAlif, OthmNM, Othm  "  -->
                            <asp:RadioButtonList ID="rbWordOthmCriteria" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True"  Text="<%$ Resources:iQuranOptionsResources,OptionSearchWordCriteriaWordOthmaniNMAlifText %>" 
                                    Value="<%$ Resources:iQuranOptionsResources,OptionSearchWordCriteriaWordOthmaniNMAlifValue %>"></asp:ListItem>
                                 <asp:ListItem Text="<%$ Resources:iQuranOptionsResources,OptionSearchWordCriteriaWordOthmaniNMText %>" 
                                    Value="<%$ Resources:iQuranOptionsResources,OptionSearchWordCriteriaWordOthmaniNMValue %>"></asp:ListItem>
                                <asp:ListItem Text="<%$ Resources:iQuranOptionsResources,OptionSearchWordCriteriaWordOthmaniText %>" 
                                    Value="<%$ Resources:iQuranOptionsResources,OptionSearchWordCriteriaWordOthmaniValue %>"></asp:ListItem>
                                 
                                
                            </asp:RadioButtonList>
	                    </div>
                        </div>
                    </div>

                    <!-- Search From To -->
	                <div class="form_row">
	                    <div class="right right_lable">
                            <mp:SiteLabel ID="SiteLabel4" runat="server" ForControl="" CssClass="settinglabel"
                                ConfigKey="SearchStartFromSura" ResourceFile="iQuranResources" />
	                    </div>
	                    <div class="right">
                            <asp:DropDownList ID="ddSurasFrom" DataValueField="SuraID" DataTextField="Title" runat="server"></asp:DropDownList>
                        </div>
	                    <div class="right center_lable">
                            <mp:SiteLabel ID="SiteLabel6" runat="server" ForControl="" CssClass="settinglabel"
                                ConfigKey="SearchStartFromVerse" ResourceFile="iQuranResources" />
	                    </div>
	                    <div class="right">
                            <asp:Textbox id="txtSearchVerseFrom" Text="1" width="50" MaxLength="3"  CssClass="verywidetextbox forminput" runat="server" />
					                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                    ControlToValidate="txtSearchVerseFrom"  CssClass="txterror"
                                     ErrorMessage="<%$ Resources:iQuranMessagesResources,Required %>" ></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RangeValidator2" Type="Integer" Display="Dynamic" MinimumValue="1" MaximumValue="286" 
                                   ControlToValidate="txtSearchVerseFrom" runat="server" 
                                    ErrorMessage="<%$ Resources:iQuranMessagesResources,SearchVerseFromToLimit %>"></asp:RangeValidator>
	                    </div>
	                </div>
	                <div class="form_row ">
	                    <div class="right right_lable">
                            <mp:SiteLabel ID="SiteLabel7" runat="server" ForControl="" CssClass="settinglabel"
                                ConfigKey="SearchEndSura" ResourceFile="iQuranResources" />
	                    </div>
	                    <div class="right">
                            <asp:DropDownList ID="ddSurasTo" DataValueField="SuraID" DataTextField="Title" runat="server"></asp:DropDownList>
                        </div>
	                    <div class="right center_lable">
                            <mp:SiteLabel ID="SiteLabel8" runat="server" ForControl="" CssClass="settinglabel"
                                ConfigKey="SearchEndVerse" ResourceFile="iQuranResources" />
	                    </div>
	                    <div class="right">
                            <asp:Textbox id="txtSearchVerseTo" Text="6" width="50" MaxLength="3"  CssClass="verywidetextbox forminput" runat="server" />
					                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                    ControlToValidate="txtSearchVerseTo"  CssClass="txterror"
                                     ErrorMessage="<%$ Resources:iQuranMessagesResources,Required %>" ></asp:RequiredFieldValidator>
                               <asp:RangeValidator ID="RangeValidator1" Type="Integer" Display="Dynamic" MinimumValue="1" MaximumValue="286" 
                                   ControlToValidate="txtSearchVerseTo" runat="server" 
                                   ErrorMessage="<%$ Resources:iQuranMessagesResources,SearchVerseFromToLimit %>"></asp:RangeValidator>
	                    </div>
	                </div>
	   
                     <!-- user personalization -->
                    <div class="form_row no_border">
                        <div class="right right_lable">
                            <mp:SiteLabel ID="SiteLabel2" runat="server" ForControl="" CssClass="settinglabel"
											            ResourceFile="iQuranResources" ConfigKey="PageSize"></mp:SiteLabel>
                            <portal:mojoHelpLink ID="MojoHelpLink6" runat="server" HelpKey="iquran-FrontSearch-PageSize-help" />
                            
                        </div>
                        <div class="right">
		                    <asp:Textbox id="txtPageSize" Text="20" width="50" MaxLength="3"  CssClass="verywidetextbox forminput" runat="server" />
		                    <mp:SiteLabel 
                                 ID="SiteLabel11" runat="server" ForControl="txtPageSize" CssClass="txterror needsattention"
                                ConfigKey="StarLabel" ResourceFile="iQuranResources" />
                             <asp:RangeValidator ID="RangeValidator3" Type="Integer" Display="Dynamic" MinimumValue="1" MaximumValue="100" 
                                   ControlToValidate="txtPageSize" runat="server" 
                                 ErrorMessage="<%$ Resources:iQuranMessagesResources,PageSizeLimit %>"></asp:RangeValidator>
                            
		                <br />
                        </div>
                    </div>

	                <div class="form_row back_url1"></div>
	  
                </div>

            </asp:Panel>
            <!-- end Simple Search Box -->
            <div id="divDescription" runat="server" class="msgnoteborder">
            <div class="msgnote">
                <asp:Literal ID="litSearchDescription" runat="server" />
                <img src="/Data/SiteImages/iQuran/searchresult.jpg" alt"iQuranAccess" /><portal:mojoHelpLink ID="MojoHelpLink8" runat="server" HelpKey="iquran-FrontSearch-SimpleSearch-help" />
            </div>
        </div>	
        </div>
         
        <div ID="divTextMsgSpacesToSend" title="" style="display:none" runat="server" ></div>
    </portal:InnerBodyPanel>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server" />
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />
