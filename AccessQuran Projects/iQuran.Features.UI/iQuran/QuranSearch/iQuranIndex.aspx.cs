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
    public partial class iQuranIndex : mojoBasePage
    {

        #region Properties
        private static readonly ILog log = LogManager.GetLogger(typeof(iQuranIndex));

        //page global
        private bool useFriendlyUrls = true;

        //quran search related
        private int siteID = -1;
        private int quranID = -1;

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
            siteSettings = CacheHelper.GetCurrentSiteSettings();
            siteID = siteSettings.SiteId;


            useFriendlyUrls = true;
            if (!WebConfigSettings.UseUrlReWriting) { useFriendlyUrls = false; }
   
        }

        private void PopulateControls()
        {
            FilliQuran();

            quranID = int.Parse(ddQuran.SelectedItem.Value.ToString());

            if (quranID > 0)
                BindVrses();
        }

        private void FilliQuran()
        {
            this.ddQuran.DataSource = Quran.GetiQurans(this.siteID);
            this.ddQuran.DataBind();
        }

        protected void ddQuran_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.quranID = int.Parse(ddQuran.SelectedItem.Value.ToString());

            if (this.quranID > 0)
                BindVrses();
        }

        private void BindVrses()
        {
            List<QuranSura> iSuraList = null;
            string redirectUrl = string.Empty;

            iSuraList = QuranSura.GetiSura_All(this.siteID, this.quranID);

            rptVerses.DataSource = iSuraList;
            rptVerses.DataBind();

           
            if (iSuraList.Count > 0)
            {
                rptVerses.Visible = true;
                litSearchDescription.Visible = true;
                divDescription.Visible = true;
                lblmessage.Visible = false;
             
            }
            else 
            {
                rptVerses.Visible = false;
                lblmessage.Visible = true;
                lblmessage.Text = Resources.iQuranMessagesResources.NoSearchResult + "<br /><br />";
                litSearchDescription.Visible = false;
                divDescription.Visible = false;
            }
        }

        protected string FormatSuraTitleUrl(string itemUrl, int quranid, int suraid, int pagenumber)
        {
            if (useFriendlyUrls && (itemUrl.Length > 0))
                return SiteRoot + itemUrl.Replace("~", string.Empty);

            return SiteRoot + "/iQuran/QuranSearch/ViewQuranPage.aspx?qid=" + quranid.ToInvariantString()
            + "&sid=" + suraid.ToInvariantString() + "&pagenumber=" + pagenumber.ToInvariantString();

        }

    }
}