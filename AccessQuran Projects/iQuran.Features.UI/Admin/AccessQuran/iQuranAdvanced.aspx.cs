// Author:					Ghalib Ghniem ghalib@ItInfoPlus.com
// Created:				    2015-03-22
// Last Modified:			2015-04-12
// 

using System;
using System.Web.UI;
using mojoPortal.Web.Framework;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Business;
using mojoPortal.Web;
using mojoPortal.Web.UI;
using mojoPortal.Web.AdminUI;
using iQuran.Business;
using iQuran.Web.Helper;
using Resources;

namespace iQuran.Web.UI.AdminUI
{
	public partial class iQuranAdvanced : NonCmsBasePage
	{

		#region Properties

		private string currUserFullName = string.Empty;
		private int currUserID = -1;
		private bool canAdministrate = false;
		private string rolesWhoCanAdministrate = ConfigHelper.GetStringProperty("iQuranAdvancedRoles", string.Empty);

		#endregion

		protected void Page_Load(object sender, EventArgs e)
		{
			//Decide if the user can administratie Libraries?
			//if ((WebUser.IsInRole("uhsContentsAdmins")) || (WebUser.IsInRole("uhsProposalManagers")) || (WebUser.IsAdmin))
			//if ((WebUser.IsInRole("uhsRfpManagers")) || (WebUser.IsInRole("uhsProposalManagers")) || (WebUser.IsInRole("uhsContentsAdmins") || (WebUser.IsAdmin))
			//if (WebUser.IsInRoles(rolesWhoCanAdministrate) || (WebUser.IsInRole("uhsContentsAdmins") || (WebUser.IsAdminOrContentAdminOrContentPublisherOrContentAuthor) || (WebUser.IsAdmin))
			// liServerInfo.Visible = (IsAdminOrContentAdmin || isSiteEditor) && (siteSettings.IsServerAdminSite || WebConfigSettings.ShowSystemInformationInChildSiteAdminMenu);
			//uhsRfpManagers;uhsProposalManagers;uhsContentsAdmins;uhsRfpReviewers

			LoadSettings();

			if (!this.canAdministrate)
			{
				SiteUtils.RedirectToAccessDeniedPage(this);
				return;
			}

			PopulateLabels();
		}

		private void PopulateLabels()
		{

			Control c = Master.FindControl("Breadcrumbs");
			if (c != null)
			{
				BreadcrumbsControl crumbs = (BreadcrumbsControl)c;
				crumbs.ForceShowBreadcrumbs = true;
			}

			Title = SiteUtils.FormatPageTitle(siteSettings, iQuranResources.AdvancedToolsHeading);
			heading.Text = iQuranResources.AdvancedToolsHeading;

			lnkAdminMenu.Text = Resource.AdminMenuLink;
			lnkAdminMenu.ToolTip = Resource.AdminMenuLink;
			lnkAdminMenu.NavigateUrl = SiteRoot + "/Admin/AdminMenu.aspx";

			if (!WebUser.IsAdmin)
				lnkAdminMenu.Visible = false;

			//Admins;iQuranManagers;iQuranContentsAdmins
			if ((WebUser.IsInRole("iQuranManagers")) || (WebUser.IsInRole("iQuranContentsAdmins")) || (WebUser.IsAdmin))
			{
				liiQuranManager.Visible = true;
				lnkiQuranManager.Text = iQuranResources.AdminMenuiQuranManagerAdminLink;
				lnkiQuranManager.NavigateUrl = SiteRoot + "/Admin/AccessQuran/iQuran/iQuranManager.aspx";

				liiSuraManager.Visible = true;
				lnkiSuraManager.Text = iQuranResources.AdminMenuiSuraManagerAdminLink;
				lnkiSuraManager.NavigateUrl = SiteRoot + "/Admin/AccessQuran/iQuran/iSuraManager.aspx";

                liiVerseManager.Visible = true;
                lnkiVerseManager.Text = iQuranResources.AdminMenuiVerseManagerAdminLink;
                lnkiVerseManager.NavigateUrl = SiteRoot + "/Admin/AccessQuran/iQuran/iVerseManager.aspx";

			}

			//if ((WebUser.IsInRole("uhsRfpManagers")) || (WebUser.IsAdmin))
			//{
			//	liCreateRFP.Visible = true;
			//	lnkCreateRFP.Text = SfaAdminResource.CreateRfp;
			//	lnkCreateRFP.NavigateUrl = SiteRoot + "/Admin/UHSRFP/RFP/RfpInitiate.aspx";

			//}


			//if ((WebUser.IsInRole("uhsRfpManagers")) || (WebUser.IsInRole("uhsRfpReviewers")) || (WebUser.IsAdmin))
			//{
			//	liRFPManager.Visible = true;
			//	lnkRFPManager.Text = AdminResource.AdminMenuRFPManagerAdminLink;
			//	lnkRFPManager.NavigateUrl = SiteRoot + "/Admin/UHSRFP/RFP/RfpManager.aspx";

			//}

			//if (WebUser.IsAdmin)
			//{
			//	liProposalReportsManager.Visible = true;
			//	lnkProposalReportsManager.Text = AdminResource.ProposalReportsManagerAdminLink;
			//	lnkProposalReportsManager.NavigateUrl = SiteRoot + "/Admin/UHSRFP/Reports/ProposalReportsManager.aspx";

			//	liRfpReportsManager.Visible = true;
			//	lnkRfpReportsManager.Text = AdminResource.RfpReportsManagerAdminLink;
			//	lnkRfpReportsManager.NavigateUrl = SiteRoot + "/Admin/UHSRFP/Reports/RfpReportsManager.aspx";
			//}

			//

			heading.Text = Resources.iQuranResources.AdvancedToolsHeading;



		}

		private void LoadSettings()
		{
			if ((WebUser.IsInRole("SfaManagers")) || (WebUser.IsInRole("sfaContentsAdmins"))
				|| (WebUser.IsAdmin))
				this.canAdministrate = true;
			else
				this.canAdministrate = false;

			SecurityHelper.DisableBrowserCache();

			AddClassToBody("administration");
			AddClassToBody("advtools");



			// Get the Current Loged on UserID
			SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
			this.currUserID = siteUser.UserId;
			if (this.currUserID == -1) { return; }
			this.currUserFullName = GetUserFullName(this.currUserID);


		}

		private string GetUserFullName(int userid)
		{
			int userID = userid;
			SiteUser mSiteUser = new SiteUser(siteSettings, userID);

			string mUserFullName = string.Empty;

			if (mSiteUser.UserId == -1) { return ""; }

			mUserFullName = mSiteUser.FirstName + " " + mSiteUser.LastName;

			if (mUserFullName.Trim() == string.Empty)
				mUserFullName = mSiteUser.Name;

			if (mUserFullName.Trim() == string.Empty)
				mUserFullName = mSiteUser.LoginName;

			if (mUserFullName.Trim() == string.Empty)
				mUserFullName = mSiteUser.Email;

			return mUserFullName;
		}

		#region OnInit

		override protected void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.Load += new EventHandler(this.Page_Load);

			SuppressMenuSelection();
			SuppressPageMenu();


		}

		#endregion
	}
}