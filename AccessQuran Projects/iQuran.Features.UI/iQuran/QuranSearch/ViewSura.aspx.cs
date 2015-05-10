// Author:					Ghalib Ghniem ghalib@ItInfoPlus.com
// Created:				    2015-04-23
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
    public partial class ViewSura : mojoBasePage
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ViewSura));
        //quran search related
        private int siteID = -1;
        private int quranID = -1;
        private int suraID = -1;
        private int suraOrder = -1;
        private int verseID = -1;
        private int verseOrder = -1;

        private bool isDefault = true;


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
            quranID = WebUtils.ParseInt32FromQueryString("qid", -1);
            if (quranID > -1)
                hdnQuranID.Value = quranID.ToString();
            else
                quranID = int.Parse(hdnQuranID.Value);

            suraID = WebUtils.ParseInt32FromQueryString("sid", -1);
            if (suraID > -1)
                hdnSuraID.Value = suraID.ToString();
            else
                suraID = int.Parse(hdnSuraID.Value);

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


            siteSettings = CacheHelper.GetCurrentSiteSettings();
            siteID = siteSettings.SiteId;


        }

        private void PopulateControls()
        {
            BindVrses();
        }


        private void BindVrses()
        {
            List<iQuranSearch> iSrchList = null;

            // Get the whole Sura without paging
            int pageNumber = 0;

            bool IsTranslation = false;

            Quran q = new Quran(this.siteID, this.quranID);
            IsTranslation = !q.IsDefault;

            QuranSura qSura = new QuranSura(this.siteID, this.suraID);
            this.suraOrder = qSura.SuraOrder;
            hdnsuraOrder.Value = this.suraOrder.ToString();


            iSrchList = iQuranSearch.GetFrontEndPage_iSura(this.siteID, this.quranID, this.suraID,this.verseID,
                                       pageNumber, IsTranslation);
           
            rptVerses.DataSource = iSrchList;
            rptVerses.DataBind();

            if (iSrchList.Count > 0)
            {
                if ((this.suraOrder != 1) && (this.suraOrder != 9))
                {
                    litBism.Text = QuranVerse.GetVerseBism(this.siteID, this.quranID, IsTranslation);
                    divBism.Visible = true;
                    litBism.Visible = true;
                }
            }

            if (iSrchList.Count > 0)
            {
                rptVerses.Visible = true;


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
                rptVerses.Visible = false;
                lblmessage.Visible = true;
                lblmessage.Text = Resources.iQuranMessagesResources.NoSearchResult + "<br /><br />";
              
            }
        }
    }
}