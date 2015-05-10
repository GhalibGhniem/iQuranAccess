// Author:					Ghalib Ghniem ghalib@ItInfoPlus.com
// Created:				    2015-04-17
// Last Modified:			2015-05-07
// 

using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Collections;
using log4net;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web.Framework;
using mojoPortal.Web;
using Resources;
using iQuran.Web;
using iQuran.Business;
using iQuran.Web.Helper;

namespace iQuran.Web.UI.QuranSearch
{
    public partial class iQuranSimpleSearchView : mojoBasePage
    {

        #region Properties
        private static readonly ILog log = LogManager.GetLogger(typeof(iQuranSimpleSearchView));

        //social
        protected string addThisAccountId = string.Empty;
        protected bool ShowSocialDiv = false;
        protected bool ShowAddThisButton = false;

        //page global
        private bool useFriendlyUrls = true;
       
        //quran search related
        private int siteID = -1;
        private int quranID = -1;
        
        private string textSearchWordOrRoot = string.Empty;
        private string textSearchWord = string.Empty;
        private string searchedTextType = string.Empty;
        private string serachCriteria = string.Empty;
        private string fontType = string.Empty;

        private int suraIDFrom = 1;
        private int suraIDTo = 1;
        private int verseOrderFrom = 1;
        private int verseOrderTo = 1;
        
        //1 yes, 0 no
        private int isDefault = -1;

        //pager
        private int pageSize = -1;
        private int pageNumber = 1;
        private int totalPages = 1;

        //for highlighting:
        string resSearchedWords = string.Empty;
        #endregion


        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
        }

        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);

            //this.Load += new System.EventHandler(this.Page_Load);
        }

        private void Page_Load(object sender, System.EventArgs e)
        {
            LoadSettings();

            PopulateLabels();

            if (!Page.IsPostBack)
                PopulateControls();

           
        }

        private void PopulateLabels()
        {
            spnTitle.InnerText = Resources.iQuranResources.SimpleSearchPageResultTitle;
            lblmessage.Visible = false;
           
            if (!Page.IsPostBack)
            this.rptVerses.Visible = false;
        }

        protected virtual void LoadSettings()
        {
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", 1);

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


            textSearchWordOrRoot = WebUtils.ParseStringFromQueryString("tor", string.Empty);
            if (!string.IsNullOrEmpty(textSearchWordOrRoot))
                hdnTextSearchWordOrRoot.Value = textSearchWordOrRoot.ToString();
            else
                textSearchWordOrRoot = hdnTextSearchWordOrRoot.Value;

            if (isDefault == 1)
            {
                textSearchWord = WebUtils.ParseStringFromQueryString("sw", string.Empty);
                if (!string.IsNullOrEmpty(textSearchWord))
                    hdnTextSearchWord.Value = textSearchWord.ToString();
                else
                    textSearchWord = hdnTextSearchWord.Value;
            }
            else
            {
                textSearchWord = WebUtils.ParseStringFromQueryString("sw", string.Empty);
                if (!string.IsNullOrEmpty(textSearchWord))
                {
                    hdnTextSearchWord.Value = textSearchWord.ToString();
                    resSearchedWords =textSearchWord.ToString();
                }
                else
                {
                    textSearchWord = hdnTextSearchWord.Value;
                    resSearchedWords = textSearchWord.ToString();
                }

                divTextSearchWord.Attributes.Add("title", resSearchedWords);
            }

            
            searchedTextType = WebUtils.ParseStringFromQueryString("tt", string.Empty);
            if (!string.IsNullOrEmpty(searchedTextType))
                hdnSearchedTextType.Value = searchedTextType.ToString();
            else
                searchedTextType = hdnSearchedTextType.Value;

            fontType = WebUtils.ParseStringFromQueryString("ft", string.Empty);
            if (!string.IsNullOrEmpty(fontType))
                hdnFontType.Value = fontType.ToString();
            else
                fontType = hdnFontType.Value;


            serachCriteria = WebUtils.ParseStringFromQueryString("sc", string.Empty);
            if (!string.IsNullOrEmpty(serachCriteria))
                hdnSerachCriteria.Value = serachCriteria.ToString();
            else
                serachCriteria = hdnSerachCriteria.Value;



            siteSettings = CacheHelper.GetCurrentSiteSettings();
            siteID = siteSettings.SiteId;
       
            addThisAccountId = siteSettings.AddThisDotComUsername;

            if (ConfigHelper.GetStringProperty("AddThisAccountId", string.Empty).ToString().Length > 0)
            {
                addThisAccountId = ConfigHelper.GetStringProperty("AddThisAccountId", string.Empty).ToString();
            }

       
            ShowSocialDiv = bool.Parse(ConfigHelper.GetStringProperty("ShowSocialDetailsFromUnauthencticated", "false").ToString());
            ShowAddThisButton = bool.Parse(ConfigHelper.GetStringProperty("ShowAddThisButton", "false").ToString()) && ShowSocialDiv;

            

            useFriendlyUrls = true;
            if (!WebConfigSettings.UseUrlReWriting) { useFriendlyUrls = false; }

        }

        private void PopulateControls()
        {
            BindVrses();
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
        private void BindVrses()
        {
            List<iQuranSearch> iSrchList = null;
            string redirectUrl = string.Empty;

            if (this.isDefault == 1)
            {

                #region [Arabic Default]
                
                if (textSearchWordOrRoot == "r")
                {
                    iSrchList = iQuranSearch.iSearch_GetPage_Default_ByRoot_Table(this.siteID, this.quranID, this.suraIDFrom,
                                            this.suraIDTo, this.verseOrderFrom, this.verseOrderTo, this.textSearchWord, pageNumber, pageSize, out totalPages);
                }
                else
                {
                    switch (searchedTextType)
                    {
                        case "v":
                            iSrchList = iQuranSearch.iSearch_GetPage_Default_ByWordVerse(this.serachCriteria, this.siteID, this.quranID, this.suraIDFrom,
                                        this.suraIDTo, this.verseOrderFrom, this.verseOrderTo, this.textSearchWord, pageNumber, pageSize, out totalPages);
                            break;

                        case "p":
                        default:
                            iSrchList = iQuranSearch.iSearch_GetPage_Default_ByWordSentence(this.serachCriteria, this.siteID, this.quranID, this.suraIDFrom,
                                        this.suraIDTo, this.verseOrderFrom, this.verseOrderTo, this.textSearchWord, pageNumber, pageSize, out totalPages);
                            break;

                        case "i":
                            iSrchList = iQuranSearch.iSearch_GetPage_Default_ByOnlyWord(this.serachCriteria, this.siteID, this.quranID, this.suraIDFrom,
                                        this.suraIDTo, this.verseOrderFrom, this.verseOrderTo, this.textSearchWord, pageNumber, pageSize, out totalPages);
                            break;

                    }

                }
                #endregion
            }
            else
            {
                #region [Translation]

                iSrchList = iQuranSearch.iSearch_GetPage_Translation_ByWordSentence(this.siteID, this.quranID, this.suraIDFrom,
                            this.suraIDTo, this.verseOrderFrom, this.verseOrderTo, this.textSearchWord, pageNumber, pageSize, out totalPages);

                #endregion
            }

            rptVerses.DataSource = iSrchList;
            rptVerses.DataBind();

            

            string pageUrl = string.Empty;

            if (this.isDefault == 1)
            {
                if (textSearchWordOrRoot == "r")
                {
                    //Root
                    pageUrl = SiteRoot
                      + "/iQuran/QuranSearch/iQuranSimpleSearchView.aspx?"
                      + "qid=" + this.quranID.ToInvariantString()
                      + "&sf=" + this.suraIDFrom.ToInvariantString()
                      + "&st=" + this.suraIDTo.ToInvariantString()
                      + "&vf=" + this.verseOrderFrom.ToInvariantString()
                      + "&vt=" + this.verseOrderTo.ToInvariantString()
                      + "&tor=" + this.textSearchWordOrRoot
                      + "&d=" + this.isDefault
                      + "&ps=" + this.pageSize
                      + "&sw=" + this.textSearchWord
                      + "&pagenumber={0}";
                }
                else
                {
                    //Word
                    pageUrl = SiteRoot
                        + "/iQuran/QuranSearch/iQuranSimpleSearchView.aspx?"
                        + "qid=" + this.quranID.ToInvariantString()
                        + "&sf=" + this.suraIDFrom.ToInvariantString()
                        + "&st=" + this.suraIDTo.ToInvariantString()
                        + "&vf=" + this.verseOrderFrom.ToInvariantString()
                        + "&vt=" + this.verseOrderTo.ToInvariantString()
                        + "&tor=" + this.textSearchWordOrRoot
                        + "&d=" + this.isDefault
                        + "&ps=" + this.pageSize
                        + "&tt=" + this.searchedTextType
                        + "&ft=" + this.fontType
                        + "&sc=" + this.serachCriteria
                        + "&sw=" + this.textSearchWord
                        + "&pagenumber={0}";

                }
            }
            else
            {
                // transl
                pageUrl = SiteRoot
                     + "/iQuran/QuranSearch/iQuranSimpleSearchView.aspx?"
                     + "qid=" + this.quranID.ToInvariantString()
                     + "&sf=" + this.suraIDFrom.ToInvariantString()
                     + "&st=" + this.suraIDTo.ToInvariantString()
                     + "&vf=" + this.verseOrderFrom.ToInvariantString()
                     + "&vt=" + this.verseOrderTo.ToInvariantString()
                     + "&tor=" + this.textSearchWordOrRoot
                     + "&d=" + this.isDefault
                     + "&ps=" + this.pageSize
                     + "&sw=" + this.textSearchWord
                     + "&pagenumber={0}";
            }
            
            pgr.PageURLFormat = pageUrl;
            pgr.ShowFirstLast = true;
            pgr.PageSize = this.pageSize; 
            pgr.PageCount = totalPages;
            pgr.CurrentIndex = pageNumber;
            
            pgr.Visible = (totalPages > 1);

            if (iSrchList.Count > 0)
            {
                rptVerses.Visible = true;


                if (this.isDefault == 1)
                {

                    #region [Arabic Default]

                    string info = string.Empty;
                    string VerseQty = string.Empty;
                    string WordQty = string.Empty;

                    CultureInfo defaultCulture = SiteUtils.GetDefaultUICulture();
                    info = ResourceHelper.GetMessageTemplate(defaultCulture,
                                       "SimpleSearchViewInfo.config");

                    string SearchWord = this.textSearchWord;
                    SearchInfo srchInfo = new SearchInfo();

                    if (textSearchWordOrRoot == "r")
                    {
                        srchInfo = new SearchInfo(this.siteID, this.quranID, this.suraIDFrom,
                                           this.suraIDTo, this.verseOrderFrom, this.verseOrderTo, this.textSearchWord);
                    }
                    else
                    {
                        srchInfo = new SearchInfo(this.searchedTextType, this.serachCriteria, this.siteID, this.quranID, this.suraIDFrom,
                                          this.suraIDTo, this.verseOrderFrom, this.verseOrderTo, this.textSearchWord);
                    }

                    // fill info region:

                    VerseQty = srchInfo.VerseQty;
                    WordQty = srchInfo.WordQty;
                    divTextSearchWord.Attributes.Add("title", srchInfo.SearchedWords);

                    litSearchDescription.Text = string.Format(
                            defaultCulture,
                            info,
                            SearchWord,
                            VerseQty,
                            WordQty);
                    litSearchDescription.Visible = true;
                    divDescription.Visible = true;
                    lblmessage.Visible = false;

                    #endregion
                }
                else
                {
                    #region [Translation]

                    litSearchDescription.Visible = false;
                    divDescription.Visible = false;
                 
                    #endregion
                }
             
            }
            else 
            {
                rptVerses.Visible = false;
                lblmessage.Visible = true;
                lblmessage.Text = Resources.iQuranMessagesResources.NoSearchResult + "<br /><br />";
                litSearchDescription.Visible = false;
                divDescription.Visible = false;
            }


            #region[BackLink]

            if (this.isDefault == 1)
            {
                #region [Arabic Default]

                if (textSearchWordOrRoot == "r")
                {
                    //Root
                    redirectUrl = SiteRoot
                      + "/iQuran/QuranSearch/iQuranSimpleSearch.aspx?"
                      + "qid=" + this.quranID.ToInvariantString()
                      + "&sf=" + int.Parse(hdnSuraIDFrom.Value).ToInvariantString()
                      + "&st=" + int.Parse(hdnSuraIDTo.Value).ToInvariantString()
                      + "&vf=" + int.Parse(hdnVerseOrderFrom.Value).ToInvariantString()
                      + "&vt=" + int.Parse(hdnVerseOrderTo.Value).ToInvariantString()
                      + "&tor=" + this.hdnTextSearchWordOrRoot.Value
                      + "&d=" + this.hdnIsDefault.Value
                      + "&sw=" + this.hdnTextSearchWord.Value
                      + "&ps=" + this.pageSize;
                }
                else
                {
                    //Word
                    redirectUrl = SiteRoot
                      + "/iQuran/QuranSearch/iQuranSimpleSearch.aspx?"
                      + "qid=" + this.quranID.ToInvariantString()
                      + "&sf=" + int.Parse(hdnSuraIDFrom.Value).ToInvariantString()
                      + "&st=" + int.Parse(hdnSuraIDTo.Value).ToInvariantString()
                      + "&vf=" + int.Parse(hdnVerseOrderFrom.Value).ToInvariantString()
                      + "&vt=" + int.Parse(hdnVerseOrderTo.Value).ToInvariantString()
                      + "&tor=" + this.hdnTextSearchWordOrRoot.Value
                      + "&d=" + this.hdnIsDefault.Value
                      + "&ps=" + this.pageSize
                      + "&tt=" + this.hdnSearchedTextType.Value
                      + "&ft=" + this.hdnFontType.Value
                      + "&sc=" + this.hdnSerachCriteria.Value
                      + "&sw=" + this.hdnTextSearchWord.Value;

                }

                #endregion
            }
            else
            {
                #region [Translation]
                // transl
                redirectUrl = SiteRoot
                      + "/iQuran/QuranSearch/iQuranSimpleSearch.aspx?"
                      + "qid=" + this.quranID.ToInvariantString()
                      + "&sf=" + int.Parse(hdnSuraIDFrom.Value).ToInvariantString()
                      + "&st=" + int.Parse(hdnSuraIDTo.Value).ToInvariantString()
                      + "&vf=" + int.Parse(hdnVerseOrderFrom.Value).ToInvariantString()
                      + "&vt=" + int.Parse(hdnVerseOrderTo.Value).ToInvariantString()
                      + "&tor=" + this.hdnTextSearchWordOrRoot.Value
                      + "&d=" + this.hdnIsDefault.Value
                      + "&sw=" + this.hdnTextSearchWord.Value
                      + "&ps=" + this.pageSize;

                #endregion

            }

            hlBack1.NavigateUrl = redirectUrl;
            hlBack2.NavigateUrl = redirectUrl;

       
            #endregion
        }

        protected string FormatSuraTitleUrl(string itemUrl, int quranid, int suraid, int verseid, int verseOrder)
        {
            if (useFriendlyUrls && (itemUrl.Length > 0))
                return SiteRoot + itemUrl.Replace("~", string.Empty);

            return SiteRoot + "/iQuran/QuranSearch/ViewQuranPage.aspx?qid=" + quranid.ToInvariantString()
            + "&sid=" + suraid.ToInvariantString() + "&vid=" + verseid.ToInvariantString() + "&vo=" + verseOrder.ToInvariantString();

        }

        protected string FormatVerseTitleUrl(string itemUrl, int quranid, int verseid)
        {
            if (useFriendlyUrls && (itemUrl.Length > 0))
                return SiteRoot + itemUrl.Replace("~", string.Empty);

            return SiteRoot + "/iQuran/QuranSearch/ViewVerse.aspx?qid=" + quranid.ToInvariantString()
            + "&vid=" + verseid.ToInvariantString() + "&sid=" + SiteId.ToInvariantString() + "&d=" + isDefault.ToInvariantString();

        }

        protected void btnSaveCompContact_Click(object sender, EventArgs e)
        {
            mdlPopupRemembered.Hide();
        }

       

    }
}