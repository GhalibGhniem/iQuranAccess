// Author:					Ghalib Ghniem ghalib@ItInfoPlus.com
// Created:				    2015-03-26
// Last Modified:			2015-05-04
// 
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using Resources;
using iQuran.Business;
using log4net;

namespace iQuran.Web.UI.QuranSearch
{
    public partial class iQuranSimpleSearch : mojoBasePage
    {

        #region Properties

        private static readonly ILog log = LogManager.GetLogger(typeof(iQuranSimpleSearch));
        
        private int siteID = -1;
        private int quranID = -1;

        private string textSearchWordOrRoot = string.Empty;
        private string textSearchWord = string.Empty;
        private string searchedTextType = string.Empty;
        private string serachCriteria = string.Empty;
        private string fontType = string.Empty;
        private int suraIDFrom = 1;
        private int suraIDTo = 114;
        private int verseOrderFrom = 1;
        private int verseOrderTo = 6;

        //1 yes, 0 no
        private int isDefault = -1;
        private int pageSize = -1;

        #endregion


        override protected void OnInit(EventArgs e)
        {
            this.Load += new System.EventHandler(this.Page_Load);
            base.OnInit(e);
            btnSearch.Click += new EventHandler(btnSearch_Click);
            btnSearchByRoot.Click += new EventHandler(btnSearchByRoot_Click);
            
        }

        private void Page_Load(object sender, System.EventArgs e)
        {
            divTextMsgSpacesToSend.Attributes.Add("title", Resources.iQuranMessagesResources.TextSearchSpacesNotAllowed);

            siteID = siteSettings.SiteId;
            PopulateLabels();

            if (!Page.IsPostBack)
            {
                LoadSettings();
                PopulateControls();
                
            }
        }

        protected virtual void LoadSettings()
        {
            

            //qid sf st vf vt
            //tor sw tt ft sc
            //d
            //quranID  suraIDFrom suraIDTo verseOrderFrom verseOrderTo  
            //textSearchWordOrRoot textSearchWord searchedTextType FontType serachCriteria
            //isDefault


            pageSize = WebUtils.ParseInt32FromQueryString("ps", -1);
            if (pageSize > -1)
                hdnPageSize.Value = pageSize.ToString();
            else
                pageSize = int.Parse(hdnPageSize.Value);

            quranID = WebUtils.ParseInt32FromQueryString("qid", -1);
            if (quranID > -1)
                hdnQuranID.Value = quranID.ToString();
            else
                quranID = int.Parse(hdnQuranID.Value);

            suraIDFrom = WebUtils.ParseInt32FromQueryString("sf", -1);
            if (suraIDFrom > -1)
                hdnSuraIDFrom.Value = suraIDFrom.ToString();
            else
                suraIDFrom = int.Parse(hdnSuraIDFrom.Value);

            suraIDTo = WebUtils.ParseInt32FromQueryString("st", -1);
            if (suraIDTo > -1)
                hdnSuraIDTo.Value = suraIDTo.ToString();
            else
                suraIDTo = int.Parse(hdnSuraIDTo.Value);

            verseOrderFrom = WebUtils.ParseInt32FromQueryString("vf", -1);
            if (verseOrderFrom > -1)
                hdnVerseOrderFrom.Value = verseOrderFrom.ToString();
            else
                verseOrderFrom = int.Parse(hdnVerseOrderFrom.Value);

            verseOrderTo = WebUtils.ParseInt32FromQueryString("vt", -1);
            if (verseOrderTo > -1)
                hdnVerseOrderTo.Value = verseOrderTo.ToString();
            else
                verseOrderTo = int.Parse(hdnVerseOrderTo.Value);

            isDefault = WebUtils.ParseInt32FromQueryString("d", -1);
            if (isDefault > -1)
                hdnIsDefault.Value = isDefault.ToString();
            else
                isDefault = int.Parse(hdnIsDefault.Value);


            //textSearchWordOrRoot textSearchWord searchedTextType serachCriteria

            textSearchWordOrRoot = WebUtils.ParseStringFromQueryString("tor", string.Empty);
            if (!string.IsNullOrEmpty(textSearchWordOrRoot))
                hdnTextSearchWordOrRoot.Value = textSearchWordOrRoot.ToString();
            else
                textSearchWordOrRoot = hdnTextSearchWordOrRoot.Value;

            textSearchWord = WebUtils.ParseStringFromQueryString("sw", string.Empty);
            if (!string.IsNullOrEmpty(textSearchWord))
                hdnTextSearchWord.Value = textSearchWord.ToString();
            else
                textSearchWord = hdnTextSearchWord.Value;

            fontType = WebUtils.ParseStringFromQueryString("ft", string.Empty);
            if (!string.IsNullOrEmpty(fontType))
                hdnFontType.Value = fontType.ToString();
            else
                fontType = hdnFontType.Value;

            

            searchedTextType = WebUtils.ParseStringFromQueryString("tt", string.Empty);
            if (!string.IsNullOrEmpty(searchedTextType))
                hdnSearchedTextType.Value = searchedTextType.ToString();
            else
                searchedTextType = hdnSearchedTextType.Value;

            serachCriteria = WebUtils.ParseStringFromQueryString("sc", string.Empty);
            if (!string.IsNullOrEmpty(serachCriteria))
                hdnSerachCriteria.Value = serachCriteria.ToString();
            else
                serachCriteria = hdnSerachCriteria.Value;

            if (!string.IsNullOrEmpty(textSearchWordOrRoot) && (textSearchWordOrRoot == "w") )
                txtSearchWord.Text = textSearchWord;

            if (!string.IsNullOrEmpty(searchedTextType))
                rbSearchedTextType.Items.FindByValue(searchedTextType).Selected = true;


            if (!string.IsNullOrEmpty(fontType))
            {
             if (fontType == "d")
                 rbWordFontType.Items.FindByValue("TypeDict").Selected = true;
             else
                 rbWordFontType.Items.FindByValue("TypeOthm").Selected = true;
            
            }


            if ((!string.IsNullOrEmpty(serachCriteria)) && (!string.IsNullOrEmpty(fontType)) )
            {
                if (fontType == "d")
                {
                    rbWordDictCriteria.Items.FindByValue(serachCriteria).Selected = true;
                    divOthmCriteria.Attributes.Add("style", "display:none");
                    divDictCriteria.Attributes.Add("style", "display:block");

                }
                else
                {
                    rbWordOthmCriteria.Items.FindByValue(serachCriteria).Selected = true;
                    divOthmCriteria.Attributes.Add("style", "display:block");
                    divDictCriteria.Attributes.Add("style", "display:none");
                }
            }

        }

        private void PopulateLabels()
        {
            spnTitle.InnerText = SiteUtils.FormatPageTitle(siteSettings, iQuranResources.SimpleSearchPageTitle);
            litSearchDescription.Text = iQuranMessagesResources.SimpleSearchInfo;

            btnSearch.Text = Resources.iQuranCommandsResources.Search;
            btnSearchByRoot.Text = Resources.iQuranCommandsResources.SearchByRoot;
            lblmessage.Visible = false;
        }

        private void PopulateControls()
        {
            FilliQuran();

            if (quranID < 1)
            {
                quranID = int.Parse(ddQuran.SelectedItem.Value.ToString());
                hdnQuranID.Value = quranID.ToString();
            }
            else
            {
                ddQuran.ClearSelection();
                ddQuran.Items.FindByValue(quranID.ToString()).Selected = true;
            }

            if (quranID > 0)
                BindDropDown();

        }

        private void FilliQuran()
        {
            this.ddQuran.DataSource = Quran.GetiQurans(this.siteID);
            this.ddQuran.DataBind();
        }

        private void FillRoot()
        {
            this.ddRoot.DataSource = iQLibrary.GetRoots(this.siteID, this.quranID);
            this.ddRoot.DataBind();

            if (textSearchWordOrRoot == "r")
            {
                if (!string.IsNullOrEmpty(textSearchWord))
                {
                    ddRoot.ClearSelection();
                    ddRoot.Items.FindByValue(textSearchWord).Selected = true;
                }
            }

        }

        private void BindDropDown()
        {
            ddSurasFrom.DataSource = QuranSura.GetSuras(this.siteID, this.quranID);
            ddSurasFrom.DataBind();
            

            if (suraIDFrom < 1)
            {
                
                suraIDFrom = int.Parse(ddSurasFrom.SelectedItem.Value.ToString());
                hdnSuraIDFrom.Value = ddSurasFrom.ToString();
                ddSurasFrom.ClearSelection();
                ddSurasFrom.Items.FindByValue(suraIDFrom.ToString()).Selected = true;
            }
            else
            {
                ddSurasFrom.ClearSelection();
                ddSurasFrom.Items.FindByValue(suraIDFrom.ToString()).Selected = true;
            }

            ddSurasTo.DataSource = QuranSura.GetSuras(this.siteID, this.quranID);
            ddSurasTo.DataBind();

            if (suraIDTo < 1)
            {
                int cnt = ddSurasTo.Items.Count;
                int lastVal = int.Parse(ddSurasTo.Items[cnt - 1].Value);

                suraIDTo = lastVal;
                hdnSuraIDTo.Value = ddSurasTo.ToString();
                ddSurasTo.ClearSelection();
                ddSurasTo.Items.FindByValue(suraIDTo.ToString()).Selected = true;
            }
            else
            {
                ddSurasTo.ClearSelection();
                ddSurasTo.Items.FindByValue(suraIDTo.ToString()).Selected = true;
            }
            
            txtSearchVerseFrom.Text = verseOrderFrom.ToString();
            txtSearchVerseTo.Text = verseOrderTo.ToString();
            txtPageSize.Text = hdnPageSize.Value.ToString();

            Quran iquran = new Quran(this.siteID, this.quranID);
            if (iquran.IsDefault == true)
            {
                divWantedWord.Visible = true;
                hdnIsDefault.Value = "1";
                divRootSelection.Visible = true;
                divSearchedTextType.Visible = true;
                FillRoot();
            }
            else
            {
                divWantedWord.Visible = false;
                hdnIsDefault.Value = "0";
                divRootSelection.Visible = false;
                divSearchedTextType.Visible = false;
            }
        }

        protected void ddQuran_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.quranID = int.Parse(ddQuran.SelectedItem.Value.ToString());
            hdnQuranID.Value = quranID.ToString();
            hdnSuraIDFrom.Value = "-1";
            hdnSuraIDTo.Value = "-1";
            suraIDFrom = -1;
            suraIDTo=-1;
            
            if (this.quranID > 0)
                BindDropDown();
        }

        /// <summary>
        /// textSearchWordOrRoot : - r : root  - w: word
        /// searchedTextType : Searched Text Type : Part From Word - p , Part From Verse - v , Exact word - i 
        /// textSearchWord : the word we are searching for , no matter, root or word
        /// wordFontType - Word Fornt Type  : Dictational (field : TypeDict) , Othmani (Field : TypeOthm)
        /// serachCriteria : the criteria no matter Otmani or Dictional :
        ///     Word Dictional Criteria : DictNMAlif , DictNM
        ///     Word Othmani Criteria : OthmNMAlif , OthmNM , Othm
        ///     
        /// Parameteres: qid sf st vf vt sw tor tt sc ft d ps
        /// pageSize (ps)
        /// quranID (qid)  suraIDFrom (sf) suraIDTo (st) verseOrderFrom (vf) verseOrderTo (vt)  textSearchWord (sw)
        /// textSearchWordOrRoot (tor) : w , r 
        /// isDefault (d) : 0 -no, 1 - yes
        /// searchedTextType (tt) : Part From - p , Exact word - i 
        /// serachCriteria (sc) : * Word Dictional Criteria : DictNMAlif , DictNM / * Word Othmani Criteria : OthmNMAlif , OthmNM , Othm
        /// Font Type (ft) : o - Othmani , d- Dictional
        /// </summary>
        void btnSearch_Click(object sender, EventArgs e)
        {
            string textSearchWord = string.Empty;

            pageSize = int.Parse(txtPageSize.Text);
            textSearchWord = txtSearchWord.Text.Trim();
            if (textSearchWord.Length <= 0)
            {
                lblmessage.Visible = true;
                lblmessage.Text = Resources.iQuranMessagesResources.SearchTextsRequired;
                return;
            }

            this.quranID = int.Parse(ddQuran.SelectedItem.Value.ToString());
            Quran iquran = new Quran(this.siteID, this.quranID);
            int isdef = 1;
            string redirectUrl = string.Empty;

            if (iquran.IsDefault == false)
                isdef = 0;

            if (iquran.IsDefault == true)
            {
                string wordFontType = string.Empty;
                string wordCriteria = string.Empty;
                string searchedTextType = string.Empty;

                searchedTextType = rbSearchedTextType.SelectedItem.Value;
                wordFontType = rbWordFontType.SelectedItem.Value;

                if (wordFontType == "TypeDict")
                {
                    wordCriteria = rbWordDictCriteria.SelectedItem.Value;
                    fontType = "d";
                }
                else
                {
                    wordCriteria = rbWordOthmCriteria.SelectedItem.Value;
                    fontType = "o";
                }

                redirectUrl = SiteRoot
                  + "/iQuran/QuranSearch/iQuranSimpleSearchView.aspx?"
                  + "qid=" + this.quranID.ToInvariantString()
                  + "&sf=" + int.Parse(ddSurasFrom.SelectedItem.Value).ToInvariantString()
                  + "&st=" + int.Parse(ddSurasTo.SelectedItem.Value).ToInvariantString()
                  + "&vf=" + int.Parse(txtSearchVerseFrom.Text.Trim()).ToInvariantString()
                  + "&vt=" + int.Parse(txtSearchVerseTo.Text.Trim()).ToInvariantString()
                  + "&tor=" + "w"
                  + "&d=" + isdef
                  + "&ps=" + pageSize
                  + "&tt=" + searchedTextType
                  + "&ft=" + fontType
                  + "&sc=" + wordCriteria
                  + "&sw=" + textSearchWord;

            }
            else 
            {
            // translation:
                redirectUrl = SiteRoot
                 + "/iQuran/QuranSearch/iQuranSimpleSearchView.aspx?"
                 + "qid=" + this.quranID.ToInvariantString()
                 + "&sf=" + int.Parse(ddSurasFrom.SelectedItem.Value).ToInvariantString()
                 + "&st=" + int.Parse(ddSurasTo.SelectedItem.Value).ToInvariantString()
                 + "&vf=" + int.Parse(txtSearchVerseFrom.Text.Trim()).ToInvariantString()
                 + "&vt=" + int.Parse(txtSearchVerseTo.Text.Trim()).ToInvariantString()
                 + "&tor=" + "w"
                 + "&d=" + isdef
                 + "&ps=" + pageSize
                 + "&sw=" + textSearchWord;
            }

            WebUtils.SetupRedirect(this, redirectUrl);

        }
        
        void btnSearchByRoot_Click(object sender, EventArgs e)
        {
            // Parameteres: qid sf st vf vt sw tor d ps
            // pageSize (ps)
            // quranID (qid)  suraIDFrom (sf) suraIDTo (st) verseOrderFrom (vf) verseOrderTo (vt)  textSearchWord (sw)
            // textSearchWordOrRoot (tor) : w , r 
            // isDefault (d) : 0 -no, 1 - yes

            pageSize = int.Parse(txtPageSize.Text);

            this.quranID = int.Parse(ddQuran.SelectedItem.Value.ToString());
            Quran iquran = new Quran(this.siteID, this.quranID);

            int isdef = 1;
            if (iquran.IsDefault == false)
                isdef = 0;

            string redirectUrl = string.Empty;
            redirectUrl = SiteRoot
              + "/iQuran/QuranSearch/iQuranSimpleSearchView.aspx?"
              + "qid=" + this.quranID.ToInvariantString()
              + "&sf=" + int.Parse(ddSurasFrom.SelectedItem.Value).ToInvariantString()
              + "&st=" + int.Parse(ddSurasTo.SelectedItem.Value).ToInvariantString()
              + "&vf=" + int.Parse(txtSearchVerseFrom.Text.Trim()).ToInvariantString()
              + "&vt=" + int.Parse(txtSearchVerseTo.Text.Trim()).ToInvariantString()
              + "&tor=" + "r"
              + "&d=" + isdef
              + "&ps=" + pageSize
              + "&sw=" + ddRoot.SelectedItem.Value;

            WebUtils.SetupRedirect(this, redirectUrl);
          
        }

       

    }
}