// Author:					Ghalib Ghniem ghalib@ItInfoPlus.com
// Created:				    2015-03-25
// Last Modified:			2015-04-14
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
	public partial class iQuranManager : NonCmsBasePage
    {
		private static readonly ILog log = LogManager.GetLogger(typeof(iQuranManager));
 
        protected string EditPropertiesImage = "~/Data/SiteImages/" + WebConfigSettings.EditPropertiesImage;
        protected string DeleteLinkImage = "~/Data/SiteImages/" + WebConfigSettings.DeleteLinkImage;
		
        private bool canAdministrate = false;
        protected bool IsUserAdmin = false;
        private SiteUser currentUser = null;

        #region Pager Properties

        private string BindByWhat = "all";
        private string sortParam = string.Empty;
        private string sort = "Title";

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

            pageSize = int.Parse(ConfigHelper.GetStringProperty("PagerPageSize", "20"));
            LoadPager();
            PopulateLabels();
            PopulateControls();

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
                sort = "Title";
        }

        private void PopulateLabels()
        {
			Title = SiteUtils.FormatPageTitle(siteSettings, Resources.iQuranResources.AdminMenuiQuranManagerAdminLink);
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

			lnkAddNew.Text = Resources.iQuranResources.AddNewiQuranHeader;
			lnkAddNew.ToolTip = Resources.iQuranResources.AddNewiQuranHeader;
			lnkAddNew.NavigateUrl = SiteRoot + "/Admin/AccessQuran/iQuran/iQuranEdit.aspx";

			lnkAddNew.Visible = (WebUser.IsInRole("iQuranManagers")) || (WebUser.IsAdmin);

			btnSearchTitle.AlternateText = Resources.iQuranResources.SearchByTitleHeader;
			btnSearchTitle.ToolTip = Resources.iQuranResources.SearchByTitleHeader;

            lblmessage.Visible = false;
            divMsg.Visible = false;

        }

        private void PopulateControls()
        {
            if (Page.IsPostBack) return;

            BindGrid();
            ddStatus.ClearSelection();
            ddStatus.Items.FindByValue(search.ToString()).Selected = true;
        }

        private void BindGrid()
        {
			bool isAdmin = ((WebUser.IsInRole("iQuranManagers")) || (WebUser.IsInRole("iQuranContentsAdmins")) || (WebUser.IsAdmin));
            // Get the Current Loged on UserID
            SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
            int userID = siteUser.UserId;

			List<Quran> dataList = new List<Quran>();


			//0: all ,    1.active  ,     2.notactive  ,   


            if (this.BindByWhat == "all")
				dataList = Quran.GetPage_iQuran_All(false, siteSettings.SiteId, isAdmin, pageNumber, pageSize, out totalPages);

			//Search For Title
			if (this.BindByWhat == "title")
				dataList = Quran.GetPage_iQuran_ByTitle(false, siteSettings.SiteId, isAdmin, this.searchTitle, pageNumber, pageSize, out totalPages);

			if ((this.BindByWhat == "active") || (this.BindByWhat == "notactive"))
				dataList = Quran.GetPage_iQuran_ByActive(false, siteSettings.SiteId, searchIsActive, pageNumber, pageSize, out totalPages);


            string pageUrl = string.Empty;

            pageUrl = SiteRoot
			+ "/Admin/AccessQuran/iQuran/iQuranManager.aspx?"
            + "sort=" + this.sort
            + "&amp;search=" + this.search
            + "&amp;searchtitle=" + Server.UrlEncode(this.searchTitle)
            + "&amp;pagenumber={0}";

            amsPager.PageURLFormat = pageUrl;
            amsPager.ShowFirstLast = true;
            amsPager.CurrentIndex = pageNumber;
            amsPager.PageSize = pageSize;
            amsPager.PageCount = totalPages;
            amsPager.Visible = (totalPages > 1);

            iQGrid.DataSource = dataList;
            iQGrid.PageIndex = pageNumber;
            iQGrid.PageSize = pageSize;
            iQGrid.DataBind();
          
            if (iQGrid.Rows.Count == 0)
            {
                lblmessage.Visible = true;
                divMsg.Visible = true;
                lblmessage.Text = Resources.iQuranMessagesResources.NoDataYet;

            }

        }

        private void SetRedirectString()
        {
            string redirectUrl = string.Empty;
			redirectUrl = SiteRoot
			  + "/Admin/AccessQuran/iQuran/iQuranManager.aspx?"
              + "pagenumber=" + pageNumber.ToInvariantString()
              + "&sort=" + this.sort
              + "&search=" + this.search
              + "&searchtitle=" + Server.UrlEncode(this.searchTitle);
            
            WebUtils.SetupRedirect(this, redirectUrl);
        }

        #region GRID EVENTS

        void iQGrid_Sorting(object sender, GridViewSortEventArgs e)
        {
            this.sort = e.SortExpression;
            SetRedirectString();
        }

        protected void iQGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //DO Nothing;

        }

		void iQGrid_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (log.IsDebugEnabled)
			{
				log.Debug(this.PageTitle + " fired event iQGrid_RowCommand");

			}

			int iquranID = int.Parse(e.CommandArgument.ToString());
			int siteid = siteSettings.SiteId;
			string dleteDate = String.Format(DateTime.Now.ToString(), "mm dd yyyy");
			currentUser = SiteUtils.GetCurrentSiteUser();
            Quran iquran = new Quran(siteid, iquranID);

			switch (e.CommandName)
			{
				case "delete":
				default:
                    Quran.Delete(siteid, iquranID);
					//iQGrid.EditIndex = +1;
					//log it:
					log.Info("user " + currentUser.Name + " deleted Quran Version : " + iquran.Title + " at:  " + dleteDate);
					BindGrid();
					lblmessage.Text = iQuranMessagesResources.DeletedMsg + "<br />";
					lblmessage.Visible = true;
					divMsg.Visible = true;
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
            this.search = "title";
            SetRedirectString();
        }

        protected void ddStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckSearchWhat();
            SetRedirectString();
        }

        #endregion

        #region OnInit

        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += new EventHandler(this.Page_Load);

			this.iQGrid.Sorting += new GridViewSortEventHandler(iQGrid_Sorting);
			this.iQGrid.RowCommand += new GridViewCommandEventHandler(iQGrid_RowCommand);
            this.iQGrid.RowDeleting += new GridViewDeleteEventHandler(iQGrid_RowDeleting);
            
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
