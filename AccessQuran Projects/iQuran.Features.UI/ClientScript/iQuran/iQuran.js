/* Author:					Ghalib Ghniem ghalib@ItInfoPlus.com */
/* Created:				    2015-04-19  */
/* Last Modified:			2015-04-19  */
/* ======================================= */

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

function iQClearSelection() {
    if (document.selection)
        document.selection.empty();
    else if (window.getSelection)
        window.getSelection().removeAllRanges();
}

function iQRemeber(suraOrder, verseOrder, verseID) {

    var image = eval("document.all.imgRemeber" + verseID);

    if (image.getAttribute('src') == '/Data/SiteImages/iQuran/add.png') {
        document.getElementById("hdnRememberVerse" + verseID).innerHTML = verseID;
        eval("document.all.imgRemeber" + verseID).src = "/Data/SiteImages/iQuran/accept.png";

    }
    else {
        document.getElementById("hdnRememberVerse" + verseID).innerHTML = "";
        eval("document.all.imgRemeber" + verseID).src = "/Data/SiteImages/iQuran/add.png";
    }

}

function iQRestrictCriteria() {
    var oRadioToRestrict = document.forms[0].elements[rbWordOthmaniCriteria];
    var oRadioSender = document.forms[0].elements[rbSearchedTextType];
    // disable these if part of sentence chosen :OptionSearchWordCriteriaWordOthmaniNMValue OptionSearchWordCriteriaWordOthmaniNMAlifValue 

    for (var i = 0; i < oRadioSender.length; i++) {
        if (oRadioSender[i].checked) {
            var rVal1 = oRadio[i].value;
            if (rVal1 == "p") {
                for (var j = 0; j < oRadioToRestrict.length; j++) {
                    var rVal2 = oRadioToRestrict[i].value;
                    if (rVal2 == "WordOthmaniNM") {
                        oRadioToRestrict.Items.FindByValue(rVal2).Enabled = false
                    }
                    if (rVal2 == "WordOthmaniNMAlif") {
                        oRadioToRestrict.Items.FindByValue(rVal2).Enabled = false
                    }
                }
            }
            else {
                for (var k = 0; k < oRadioToRestrict.length; k++) {
                    var rVal3 = oRadioToRestrict[i].value;
                    if (rVal3 == "WordOthmaniNM") {
                        oRadioToRestrict.Items.FindByValue(rVal3).Enabled = true
                    }
                    if (rVal3 == "WordOthmaniNMAlif") {
                        oRadioToRestrict.Items.FindByValue(rVal3).Enabled = true
                    }
                }
            }
        }
    }
}

function iQSelectCriteria() {

    var oRadioFontType = document.getElementById("<%=rbWordForntType.ClientID%>");
    var oRadioFontTypeInput = oRadioFontType.getElementsByTagName("input");

    //alert("hi3");
    for (var i = 0; i < oRadioFontTypeInput.length; i++) {
        if (oRadioFontTypeInput[i].checked) {
            var rVal = oRadioFontTypeInput[i].value;
            if (rVal == "TypeOthm") {
                eval("document.all.divOtmCriteria").style.display = "block";
                eval("document.all.divDictCriteria").style.display = "none";
            }
            else {
                eval("document.all.divOtmCriteria").style.display = "none";
                eval("document.all.divDictCriteria").style.display = "block";
            }
            break;
        }
    }
}
