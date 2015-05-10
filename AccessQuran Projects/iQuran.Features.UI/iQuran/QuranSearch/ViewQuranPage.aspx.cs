// Author:					Ghalib Ghniem ghalib@ItInfoPlus.com
// Created:				    2015-05-06
// Last Modified:			2015-05-09
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
    public partial class ViewQuranPage : mojoBasePage
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ViewQuranPage));
        //quran search related
        private int siteID = -1;
        private int quranID = -1;
        private int suraID = -1;
        private int suraOrder = -1;
        private int verseID = -1;
        private int verseOrder = -1;

        private bool isDefault = true;

        //this param to know if we came from search result to see specific verse or 
        // just paging throgh Quran pages
        private bool isSearchResult = true;
        
        //pager
        private int pageNumber = 0;
        private int totalPages = 604;

        bool IsTranslation = false;

        override protected void OnInit(EventArgs e)
        {
            this.Load += new System.EventHandler(this.Page_Load);
            base.OnInit(e);
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
            
            lblmessage.Visible = false;

            if (!Page.IsPostBack)
                this.rptVerses.Visible = false;
        }

        protected virtual void LoadSettings()
        {
            siteSettings = CacheHelper.GetCurrentSiteSettings();
            siteID = siteSettings.SiteId;

            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", 0);

            if (pageNumber != 0) 
                isSearchResult = false;

            quranID = WebUtils.ParseInt32FromQueryString("qid", -1);
            if (quranID > -1)
                hdnQuranID.Value = quranID.ToString();
            else
                quranID = int.Parse(hdnQuranID.Value);

            Quran q = new Quran(this.siteID, this.quranID);
            IsTranslation = !q.IsDefault;

            if (IsTranslation == true)
                hdnIsTranslation.Value = "1";
            

            suraID = WebUtils.ParseInt32FromQueryString("sid", -1);
            if (suraID > -1)
                hdnSuraID.Value = suraID.ToString();
            else
                suraID = int.Parse(hdnSuraID.Value);

             //if we came from Paging, first get the [First Sura ID ] in that Page
            if ((isSearchResult == false) && (suraID <= 0))
            {
                suraID = QuranSura.GetSuraIdFromPageNumber(this.siteID, this.quranID, this.pageNumber, this.IsTranslation);
                hdnSuraID.Value = suraID.ToString();
            }

            if (isSearchResult == true)
            {
                verseID = WebUtils.ParseInt32FromQueryString("vid", -1);
                if (verseID > -1)
                    hdnVerseID.Value = verseID.ToString();
                else
                    verseID = int.Parse(hdnVerseID.Value);

                verseOrder = WebUtils.ParseInt32FromQueryString("vo", -1);
                if (verseOrder > -1)
                    hdnverseOrder.Value = verseOrder.ToString();
                else
                    verseOrder = int.Parse(hdnverseOrder.Value);
            }

           


        }

        private void PopulateControls()
        {
            BindVrses();
        }




        protected string GetBismVerse(int quranid, int suraID)
        {
            bool locIstranslation = false;
            locIstranslation = int.Parse(hdnIsTranslation.Value) > 0;
             string res = string.Empty;

             this.suraOrder = int.Parse(hdnsuraOrder.Value);
             if ((this.suraOrder != 1) && (this.suraOrder != 9))
            {
                res = QuranVerse.GetVerseBism(this.siteID, quranid, locIstranslation);
            }

            return res;
        }

        private void BindVrses()
        {
            List<iQuranSearch> iSrchList = null;
            QuranSura qSura = new QuranSura();
          

           

           



            //if we camefrom search, first get the pageNumber have thats verse
            if (isSearchResult == true)
                pageNumber = QuranVerse.GetVersePageNumber(this.siteID, this.suraID, this.verseID);

            qSura = new QuranSura(this.siteID, this.suraID);
            this.suraOrder = qSura.SuraOrder;
            hdnsuraOrder.Value = this.suraOrder.ToString();

            if (isSearchResult == true)
                iSrchList = iQuranSearch.GetFrontEnd_QuranPage_WithSearchVerse(this.siteID, this.quranID, this.verseID, pageNumber, IsTranslation);
            else
                iSrchList = iQuranSearch.GetFrontEnd_QuranPage(this.siteID, this.quranID, pageNumber, IsTranslation);

            rptVerses.DataSource = iSrchList;
            rptVerses.DataBind();


            string pageUrl = string.Empty;

            pageUrl = SiteRoot
                    + "/iQuran/QuranSearch/ViewQuranPage.aspx?"
                    + "qid=" + this.quranID.ToInvariantString()
                    + "&pagenumber={0}";

            // + "&sid=" + this.suraID.ToInvariantString()
            pgr.PageURLFormat = pageUrl;
            pgr.ShowFirstLast = true;
            pgr.PageCount = totalPages;
            pgr.CurrentIndex = pageNumber;

            pgr.Visible = (totalPages > 1);

            //Show hide Bism Allah Verse
            //if (iSrchList.Count > 0)
            //{
            //    if ((suraID != 1) && (suraID != 9))
            //    {
            //        litBism.Text = QuranVerse.GetVerseBism(this.siteID, this.quranID, IsTranslation);
            //        divBism.Visible = true;
            //        litBism.Visible = true;
            //    }
            //}

            //show the link view whole Sura or not ?
            if ((iSrchList.Count > 0) && (isSearchResult == true))
            {
                if (suraOrder != 1)
                {
                    hlViewSura.NavigateUrl = SiteRoot + "/iQuran/QuranSearch/ViewSura.aspx?qid=" + this.quranID.ToInvariantString()
                        + "&sid=" + this.suraID.ToInvariantString() + "&vid=" + this.verseID.ToInvariantString()
                        + "&vo=" + this.verseOrder.ToInvariantString(); ;

                    hlViewSura.Visible = true;
                }

                //rptVerses.Visible = true;

                //QuranSura qSura = new QuranSura(this.siteID, this.suraID);

                string info = string.Empty;
                CultureInfo defaultCulture = SiteUtils.GetDefaultUICulture();
                info = ResourceHelper.GetMessageTemplate(defaultCulture,
                                    "SuraViewSearchInfo.config");

                litSearchDescription.Text = string.Format(
                            defaultCulture,
                            info,
                            qSura.Title,
                            qSura.SuraOrder,
                            qSura.VersesCount,
                            this.verseOrder);

                litSearchDescription.Visible = true;
                divDescription.Visible = true;
                lblmessage.Visible = false;
            }
            else
            {
                // set the page Header for paging
                string info = string.Empty;
                string titles = string.Empty;
                QuranPage qPage = new QuranPage(this.siteID, this.quranID, this.pageNumber, IsTranslation);

                if ((qPage.Titles.Split('-').Length - 1) == 1)
                {
                    info = Resources.iQuranResources.PartHeader + "&nbsp;" + qPage.PartNo + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Resources.iQuranResources.SuratHeader + "&nbsp;" + qPage.Titles.Replace("  -  ", "");
                }
                else
                {

                    //qPage.Titles.Remove(qPage.Titles.Length, 1);
                    //qPage.Titles.Remove(qPage.Titles.ToString().LastIndexOf("-"), 1);
                    titles= qPage.Titles.Remove(qPage.Titles.ToString().IndexOf("-"), 1);
                    titles = Resources.iQuranResources.SuratHeader + "&nbsp;" + titles.Replace("-", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Resources.iQuranResources.SuratHeader + "&nbsp;");
                    info = Resources.iQuranResources.PartHeader + "&nbsp;" + qPage.PartNo + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + titles;
                }
                litSearchDescription.Text = info;

                litSearchDescription.Visible = true;
                divDescription.Visible = true;
                lblmessage.Visible = false;
            }

            if (iSrchList.Count > 0)
            { 
                rptVerses.Visible = true; 
            }
            else
            {
                rptVerses.Visible = false;
                lblmessage.Visible = true;
                lblmessage.Text = Resources.iQuranMessagesResources.NoSearchResult + "<br /><br />";
              
            }
        }

    }
}