// Author:					Ghalib Ghniem ghalib@ItInfoPlus.com
// Created:				    2015-04-12
// Last Modified:			2015-04-12
// 
 
using System;
using System.Collections.ObjectModel;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using log4net;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web.Framework;
using mojoPortal.Web.AdminUI;
using mojoPortal.Web;
using iQuran.Business;
using iQuran.Web.Helper;
using Resources;
using mojoPortal.Web.Editor;

namespace iQuran.Web.UI.AdminUI
{
    public partial class iVerseManager : NonCmsBasePage
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(iVerseManager));

		protected string AddNewPropertiesImage = "~/Data/SiteImages/add.gif";
        protected string EditPropertiesImage = "~/Data/SiteImages/" + WebConfigSettings.EditPropertiesImage;
        protected string DeleteLinkImage = "~/Data/SiteImages/" + WebConfigSettings.DeleteLinkImage;
		
        private bool canAdministrate = false;
        protected bool IsUserAdmin = false;
        private SiteUser currentUser = null;
		private int quranID = 0;
        private int suraID = 0;
        private string quranLanguage = string.Empty;
        private bool isTranslation = false;

        #region Pager Properties

        private string BindByWhat = "all";
        private string sortParam = string.Empty;
        private string sort = "VerseOrder";

        private string searchParam = string.Empty;
        private string search = string.Empty;

        private string searchTitleParam = string.Empty;
        private string searchTitle = string.Empty;

        private bool searchIsActive = false;
        private int pageNumber = 1;
        private int pageSize = 0;
        private int totalPages = 0;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadSettings();

            if (!this.canAdministrate)
            {
                SiteUtils.RedirectToAccessDeniedPage(this);
                return;
            }

            pageSize = 30;
            LoadPager();

            if (!Page.IsPostBack)
            {
                PopulateLabels();
                PopulateControls();
            }
			//FilliQuran();

        }

        private void LoadSettings()
        {
			if ((WebUser.IsInRole("iQuranManagers")) || (WebUser.IsInRole("iQuranContentsAdmins")) || (WebUser.IsAdmin))
                this.canAdministrate = true;
            else
                this.canAdministrate = false;

            if (WebUser.IsAdmin)
                IsUserAdmin = true;
            
            SecurityHelper.DisableBrowserCache();

            lnkAdminMenu.Visible = (WebUser.IsAdmin);

            AddClassToBody("administration");
            AddClassToBody("rolemanager");

        }

        private void LoadPager()
        {
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", 1);

            if (Page.Request.Params["sort"] != null)
            {
                sort = Page.Request.Params["sort"];
            }

			if (Page.Request.Params["qid"] != null)
			{
				this.quranID = int.Parse(Page.Request.Params["qid"].ToString());
                hdnQuranID.Value = this.quranID.ToString();
			}

            if (Page.Request.Params["sid"] != null)
            {
                this.suraID = int.Parse(Page.Request.Params["sid"].ToString());
                hdnSuraID.Value = this.suraID.ToString();
            }

            //lblmessage
            string status1 = WebUtils.ParseStringFromQueryString("st", "");
            if (status1.Trim() == "ok")
            {
                divMsg.Visible = true;
                lblmessage.Visible = true;
                lblmessage.Text = Resources.iQuranMessagesResources.SuccessfullDelete;
            }
            if (status1.Trim() == "no")
            {
                lblmessage.Visible = true;
                lblmessage.Text = Resources.iQuranMessagesResources.FailedDelete;
            }

            searchTitleParam = "searchtitle";
            if ((Page.Request.Params[searchTitleParam] != null) && (Page.Request.Params[searchTitleParam].Length > 0))
            {
                searchTitle = Page.Request.Params[searchTitleParam];
                BindByWhat = "title";
                search = "0";
            }
            else
            {
                searchTitle = string.Empty;
                searchParam = "search";
                if (Page.Request.Params[searchParam] != null)
                {
                    search = Page.Request.Params[searchParam];

                        switch (search)
                        {
							//0: all ,    1.active  ,     2.notactive  

                            case "0":
                            default:
                                BindByWhat = "all";
                                break;

                            case "1":
                                searchIsActive = true;
                                BindByWhat = "active";
                                break;

                            case "2":
                                searchIsActive = false;
                                BindByWhat = "notactive";
                                break;

                        }

                }
                else
                {
                    search = "0";
                    BindByWhat = "all";
                }
            }

            sortParam = "sort";
            if (Page.Request.Params[sortParam] != null)
                sort = Page.Request.Params[sortParam];
            else
				sort = "VerseOrder";

        }

        private void PopulateLabels()
        {
			Title = SiteUtils.FormatPageTitle(siteSettings, Resources.iQuranResources.AdminMenuiVerseManagerAdminLink);
            spnTitle.InnerText = Title;

            lnkAdminMenu.Text =  Resource.AdminMenuLink;
            lnkAdminMenu.ToolTip = Resource.AdminMenuLink;
            lnkAdminMenu.NavigateUrl = SiteRoot + "/Admin/AdminMenu.aspx";

			lnkAdvanced.Text = Resources.iQuranResources.AdvancedLink;
			lnkAdvanced.ToolTip = Resources.iQuranResources.AdvancedToolsHeading;
            lnkAdvanced.NavigateUrl = SiteRoot + "/Admin/AccessQuran/iQuranAdvanced.aspx";

			lnkiQuranManagerAdmin.Text = Resources.iQuranResources.AdminMenuiQuranManagerAdminLink;
			lnkiQuranManagerAdmin.ToolTip = Resources.iQuranResources.AdminMenuiQuranManagerAdminLink;
			lnkiQuranManagerAdmin.NavigateUrl = SiteRoot + "/Admin/AccessQuran/iQuran/iQuranManager.aspx";

			lnkiSuraManagerAdmin.Text = Resources.iQuranResources.AdminMenuiSuraManagerAdminLink;
			lnkiSuraManagerAdmin.ToolTip = Resources.iQuranResources.AdminMenuiSuraManagerAdminLink;
			lnkiSuraManagerAdmin.NavigateUrl = SiteRoot + "/Admin/AccessQuran/iQuran/iSuraManager.aspx";

            lnkiVerseManagerAdmin.Text = Resources.iQuranResources.AdminMenuiVerseManagerAdminLink;
            lnkiVerseManagerAdmin.ToolTip = Resources.iQuranResources.AdminMenuiVerseManagerAdminLink;
            lnkiVerseManagerAdmin.NavigateUrl = SiteRoot + "/Admin/AccessQuran/iQuran/iVerseManager.aspx";

            lnkAddNew.Text = Resources.iQuranResources.AddNewiVerseHeader;
            lnkAddNew.ToolTip = Resources.iQuranResources.AddNewiVerseHeader;
            

            lnkAddNew.Visible = (WebUser.IsInRole("iQuranManagers")) || (WebUser.IsAdmin);

			btnSearchTitle.AlternateText = Resources.iQuranResources.SearchByTitleHeader;
			btnSearchTitle.ToolTip = Resources.iQuranResources.SearchByTitleHeader;

            //lblmessage.Visible = false;
            //divMsg.Visible = false;

        }

        private void PopulateControls()
        {
           

			FilliQuran();
            ddStatus.ClearSelection();
            ddStatus.Items.FindByValue(search.ToString()).Selected = true;
        }
	
		private void FilliQuran()
		{
			//fill ddSelQuran
			this.ddSelQuran.DataSource = Quran.GetiQurans(this.SiteId);
			this.ddSelQuran.DataBind();
            if (int.Parse(hdnQuranID.Value) > 0)
                this.quranID = int.Parse(hdnQuranID.Value);
            else
                this.quranID = int.Parse(ddSelQuran.SelectedItem.Value.ToString());
			
            hdnQuranID.Value = this.quranID.ToString();

            ddSelQuran.ClearSelection();
            ddSelQuran.Items.FindByValue(this.quranID.ToString()).Selected = true;

            Quran q = new Quran(SiteId, this.quranID);
            this.quranLanguage = q.QLanguage;
            hdnQLang.Value = q.QLanguage;
            this.isTranslation = !q.IsDefault;

            if (this.quranID > 0)
                FilliSura();
           
		}

        private void FilliSura()
        {
            //fill ddSelSura
            this.ddSelSura.DataSource = QuranSura.GetSuras(this.SiteId, this.quranID);
            this.ddSelSura.DataBind();
            if (int.Parse(hdnSuraID.Value) > 0)
                this.suraID = int.Parse(hdnSuraID.Value);
            else
                this.suraID = int.Parse(ddSelSura.SelectedItem.Value.ToString());

            hdnQuranID.Value = this.suraID.ToString();

            ddSelSura.ClearSelection();
            ddSelSura.Items.FindByValue(this.suraID.ToString()).Selected = true;


            if (this.suraID > 0)
                BindGrid();
            
        }

        private void BindGrid()
        {
            lnkAddNew.NavigateUrl = SiteRoot + "/Admin/AccessQuran/iQuran/iVerseEdit.aspx?qid=" + this.quranID + "&sid=" + this.suraID;

            //bool isAdmin = ((WebUser.IsInRole("iQuranManagers")) || (WebUser.IsInRole("iQuranContentsAdmins")) || (WebUser.IsAdmin));
            // Get the Current Loged on UserID
            SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
            int userID = siteUser.UserId;

            List<QuranVerse> dataList = new List<QuranVerse>();


            //0: all ,    1.active  ,     2.notactive   
            if (this.BindByWhat == "all")
                dataList = QuranVerse.GetPage_iVerse_All(siteSettings.SiteId, this.quranID, this.suraID, this.isTranslation, pageNumber, pageSize, out totalPages);

            //Search For Title
            if (this.BindByWhat == "title")
                dataList = QuranVerse.GetPage_iVerse_ByTitle(siteSettings.SiteId, this.quranID, this.suraID, this.isTranslation, this.searchTitle, pageNumber, pageSize, out totalPages);

            if ((this.BindByWhat == "active") || (this.BindByWhat == "notactive"))
                dataList = QuranVerse.GetPage_iVerse_ByActive(siteSettings.SiteId, this.quranID, this.suraID, searchIsActive, this.isTranslation, pageNumber, pageSize, out totalPages);


            string pageUrl = string.Empty;

            pageUrl = SiteRoot
            + "/Admin/AccessQuran/iQuran/iVerseManager.aspx?"
            + "qid=" + this.quranID.ToInvariantString()
            + "&amp;sid=" + this.suraID.ToInvariantString()
            + "&amp;sort=" + this.sort
            + "&amp;search=" + this.search
            + "&amp;searchtitle=" + Server.UrlEncode(this.searchTitle)
            + "&amp;pagenumber={0}";



            amsPager.PageURLFormat = pageUrl;
            amsPager.ShowFirstLast = true;
            amsPager.CurrentIndex = pageNumber;
            amsPager.PageSize = pageSize;
            amsPager.PageCount = totalPages;
            amsPager.Visible = (totalPages > 1);

            iQVGrid.DataSource = dataList;
            iQVGrid.PageIndex = pageNumber;
            iQVGrid.PageSize = pageSize;
            iQVGrid.DataBind();


            if (iQVGrid.Rows.Count == 0)
            {
                lblmessage.Visible = true;
                divMsg.Visible = true;
                lblmessage.Text = Resources.iQuranMessagesResources.NoDataYet;

            }

        }


        private void SetRedirectString(bool withSuraID, bool withmsg, string Msg)
        {
            string redirectUrl = string.Empty;
			redirectUrl = SiteRoot
			  + "/Admin/AccessQuran/iQuran/iVerseManager.aspx?"
			  + "qid=" + int.Parse(hdnQuranID.Value.ToString()).ToInvariantString()
              + "&pagenumber=" + pageNumber.ToInvariantString()
              + "&sort=" + this.sort
              + "&search=" + this.search
              + "&searchtitle=" + Server.UrlEncode(this.searchTitle);

            if (withSuraID == true)
                redirectUrl += "&sid=" + int.Parse(hdnSuraID.Value.ToString()).ToInvariantString();

            if (withmsg == true)
                redirectUrl += "&st=" + Msg;

            WebUtils.SetupRedirect(this, redirectUrl);
        }

        #region GRID EVENTS

		void iQVGrid_Sorting(object sender, GridViewSortEventArgs e)
        {
            this.sort = e.SortExpression;
            SetRedirectString(true,false,"");
        }

        protected void iQVGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //DO Nothing;

        }

		void iQVGrid_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (log.IsDebugEnabled)
			{
				log.Debug(this.PageTitle + " fired event iQVGrid_RowCommand");
			}

            int iverseID = int.Parse(e.CommandArgument.ToString());
            int siteid = siteSettings.SiteId;
            string dleteDate = String.Format(DateTime.Now.ToString(), "mm dd yyyy");
            currentUser = SiteUtils.GetCurrentSiteUser();
            int suraid = -1;
            QuranVerse iverse = null;
          // QuranVerseTranslation iverseTransl = null;

            if (this.hdnQLang.Value == "ar")
                iverse = new QuranVerse(siteid, int.Parse(hdnQuranID.Value), iverseID, false);
            else
                iverse = new QuranVerse(siteid, int.Parse(hdnQuranID.Value), iverseID, true);
                //iverseTransl = new QuranVerseTranslation(siteid, int.Parse(hdnQuranID.Value), iverseID);

            

            switch (e.CommandName)
            {
                case "delete":
                default:
                    if (this.hdnQLang.Value == "ar")
                    {
                        QuranVerse.DeleteVerse(siteid, iverse.QuranID, iverse.SuraID, iverseID);
                        suraid = iverse.SuraID;
                    }
                    else
                    {
                        QuranVerseTranslation iverseTransl = new QuranVerseTranslation(siteid, int.Parse(hdnQuranID.Value), iverseID);
                        QuranVerse.DeleteVerse_Translation(siteid, iverseTransl.QuranID, iverseTransl.SuraID, iverseTransl.VerseID);
                        suraid = iverseTransl.SuraID;
                    }

                    //iQVGrid.EditIndex = +1;
                    //log it:
                    log.Info("user " + currentUser.Name + " deleted Verse Version : " + iverse.QuranVerseTxt + " at:  " + dleteDate);
                    //BindGrid();
                    //lblmessage.Text = iQuranMessagesResources.DeletedMsg + "<br />";
                    //lblmessage.Visible = true;
                    //divMsg.Visible = true;
                    hdnSuraID.Value = suraid.ToString();
                    SetRedirectString(true, true, "ok");
                    break;
            }

		}

        #endregion

        #region SEARCH

        private void CheckSearchWhat()
        {
            if (txtSearchByTitle.Text.Trim() == string.Empty)
                this.searchTitle = string.Empty;
            else
                this.searchTitle = SecurityHelper.RemoveMarkup(txtSearchByTitle.Text);

            search = ddStatus.SelectedValue.ToString();

			//0: all ,    1.active  ,     2.notactive  

           
        }

        protected void btnSearchTitle_Click(Object sender, EventArgs e)
        {
            
			
				if (txtSearchByTitle.Text.Trim() == string.Empty)
					return;
				this.searchTitle = SecurityHelper.RemoveMarkup(txtSearchByTitle.Text);
				this.search = "VerseOrder";
                SetRedirectString(true, false, "");
		
        }

        protected void ddStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
				CheckSearchWhat();
                SetRedirectString(true, false, "");
        }

        protected void ddSelQuran_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.quranID = int.Parse(ddSelQuran.SelectedItem.Value.ToString());
            //hdnQuranID.Value = this.quranID.ToString();
            //if (this.quranID > 0)
            //{
            //    hdnSuraID.Value = "-1";
            //    FilliSura();

            //}

            this.quranID = int.Parse(ddSelQuran.SelectedItem.Value.ToString());
            hdnQuranID.Value = this.quranID.ToString();
            this.suraID = -1;
            hdnSuraID.Value = "-1";
            this.searchTitle = string.Empty;
            search = "0";
            SetRedirectString(false, false, "");
        }

        protected void ddSelSura_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.quranID = int.Parse(ddSelQuran.SelectedItem.Value.ToString());
            hdnQuranID.Value = this.quranID.ToString();
            this.suraID = int.Parse(ddSelSura.SelectedItem.Value.ToString());
            hdnSuraID.Value = this.suraID.ToString();
            this.searchTitle = string.Empty;
            search = "0";
            SetRedirectString(true, false, "");
        }

        #endregion

        #region OnInit

        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += new EventHandler(this.Page_Load);

			this.iQVGrid.Sorting += new GridViewSortEventHandler(iQVGrid_Sorting);
			this.iQVGrid.RowCommand += new GridViewCommandEventHandler(iQVGrid_RowCommand);
            this.iQVGrid.RowDeleting += new GridViewDeleteEventHandler(iQVGrid_RowDeleting);
            
			SuppressMenuSelection();
            SuppressPageMenu();
            
        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
        }

        #endregion

        

		
    }
}
