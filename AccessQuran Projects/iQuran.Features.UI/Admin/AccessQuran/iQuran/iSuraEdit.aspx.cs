// Author:					Ghalib Ghniem ghalib@ItInfoPlus.com
// Created:				    2015-03-26
// Last Modified:			2015-04-14
// 
 
using System;
using System.Web;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web.Framework;
using mojoPortal.Web;
using mojoPortal.Web.AdminUI;
using mojoPortal.Web.Editor;
using mojoPortal.Web.UI;
using Resources;
using log4net;
using iQuran.Business;

namespace iQuran.Web.UI.AdminUI
{

	public partial class iSuraEdit : NonCmsBasePage
    {

        private int quranID = -1;
		private int suraID = -1;
        private bool userCanEdit = false;
        private QuranSura sura;
		private SiteUser currentUser = null;
       //private string fromwhere = string.Empty;
        protected string EditPropertiesImage = "~/Data/SiteImages/" + WebConfigSettings.EditPropertiesImage;
		private static readonly ILog log = LogManager.GetLogger(typeof(iSuraEdit));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current == null) { return; }
            LoadSettings();
            if (!userCanEdit)
            {
                SiteUtils.RedirectToAccessDeniedPage(this, false);
                return;
            }

            PopulateLabels();
            if (!Page.IsPostBack)
            {
                hdnFromWhere.Value = Request.RawUrl;
                PopulateControls();
            }
        }

        private void LoadSettings()
        {

            if (WebUser.IsAdminOrContentAdmin) { userCanEdit = true; }
            else if (SiteUtils.UserIsSiteEditor()) { userCanEdit = true; }
            else if (WebUser.IsInRoles(siteSettings.RolesThatCanEditContentTemplates)) { userCanEdit = true; }

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

            //lblmessage
            string status1=WebUtils.ParseStringFromQueryString("st","");
            if (status1.Trim() == "ok")
            {
                lblmessage.Visible = true;
                lblmessage.Text = Resources.iQuranMessagesResources.SuccessfullSave;
            }
			if (status1.Trim() == "no")
			{
				lblmessage.Visible = true;
				lblmessage.Text = Resources.iQuranMessagesResources.FailedSave;
			}
            AddClassToBody("administration");
            AddClassToBody("securityroles");
        }

        private void PopulateLabels()
        {
			Title = SiteUtils.FormatPageTitle(siteSettings, Resources.iQuranResources.EditiSuraHeader);
            spnTitle.Text = Title;
            lnkAdminMenu.Text = Resource.AdminMenuLink;
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
            lnkiSuraManagerAdmin.NavigateUrl = SiteRoot + "/Admin/AccessQuran/iQuran/iSuraManager.aspx?qid=" + this.quranID + "&sid=" + this.suraID;

            btnSave.Text = Resources.iQuranCommandsResources.Apply;

            lnkCancel.Text = Resources.Resource.RoleManagerCancelButton;
            lnkCancel.NavigateUrl = Request.RawUrl;

        }

        private void PopulateControls()
        {
            // info
            Quran quran = null;
            quran = new Quran(siteSettings.SiteId, this.quranID);
            lbliQuranVesrion.Text = quran.Title.ToString();
            ddLang.SetValue(quran.QLanguage.Trim());
            string selLangVal = ddLang.GetText().Trim();
            lbliQuranLanguage.Text = selLangVal;
            lbliSuraVersesCount.Text = "0";

            //divTransl
            if (ddLang.GetValue().Trim() == "ar")
            { 
                divTransl.Visible = false;
                divArabic.Visible = true;
            }
            else
            {
                FilliSura();
                divTransl.Visible = true;
                divArabic.Visible = false;
            }

            sura = new QuranSura(siteSettings.SiteId, this.suraID);
            if (suraID <= 0) 
            {return; }

            
            lbliSuraVersesCount.Text = sura.VersesCount.ToString();

            // Sura Details
            txtTitle.Text = sura.Title.ToString();
            ddPlace.ClearSelection();
            ddPlace.Items.FindByValue(sura.Place.Trim().ToString()).Selected = true;

            if (ddLang.GetValue().Trim() != "ar")
            {
                ddSelSura.ClearSelection();
                ddSelSura.Items.FindByValue(sura.SuraOrder.ToString()).Selected = true;
            }

            txtSortOrder.Text = sura.SuraOrder.ToString();
			cbIsActive.Checked = sura.IsActive;
			quranID = int.Parse(sura.QuranID.ToString());
			suraID = int.Parse(sura.SuraID.ToString());
            
			hdnQuranID.Value = quranID.ToString();
            hdnSuraID.Value = suraID.ToString();

            if (ddLang.GetValue().Trim() == "ar")
                hdnSuraOrder.Value = txtSortOrder.Text.ToString();
            else
                hdnSuraOrder.Value = ddSelSura.SelectedItem.Value.ToString();

            if ((this.suraID > 0) && (ddLang.GetValue().Trim() != "ar"))
            {
                ddSelSura.ClearSelection();
                ddSelSura.Items.FindByValue(this.hdnSuraOrder.Value.ToString()).Selected = true;
            }
            

		}

        private void FilliSura()
        {
            //fill ddSelSura
            this.ddSelSura.DataSource = QuranSura.GetSuras(this.SiteId, 1);
            this.ddSelSura.DataBind();

           

        }

        void btnSave_Click(object sender, EventArgs e)
        {

            bool isExists = false;
            isExists = QuranSura.Exists(siteSettings.SiteId, this.quranID, this.suraID, this.txtTitle.Text.Trim());
            if (isExists)
            {
                lblmessage.Visible = true;
                lblmessage.Text = Resources.iQuranMessagesResources.TitleExists;
                return;
            }

            int suraorder = -1;
            if (ddLang.GetValue().Trim() == "ar")
                suraorder = int.Parse(txtSortOrder.Text.ToString());
            else
                suraorder = int.Parse(ddSelSura.SelectedItem.Value.ToString());

            isExists = QuranSura.OrderExists(siteSettings.SiteId, this.quranID, this.suraID, suraorder);
            if (isExists)
            {
                lblmessage.Visible = true;
                lblmessage.Text = Resources.iQuranMessagesResources.SuraOrderExists;
                return;
            }

			if (this.txtTitle.Text.Trim().Length < 1)
            {
                lblmessage.Visible = true;
				lblmessage.Text = Resources.iQuranMessagesResources.TitleRequired;
                return;
            }

			int siteid = siteSettings.SiteId;
			currentUser = SiteUtils.GetCurrentSiteUser();
            QuranSura sura = new QuranSura(siteid, suraID);

            sura.SuraID = this.suraID;
            sura.SiteID = siteid;
            sura.QuranID = this.quranID;
            sura.Title = SecurityHelper.RemoveMarkup(this.txtTitle.Text);
            sura.Place = ddPlace.SelectedItem.Value.Trim();
           
            sura.IsActive = bool.Parse(this.cbIsActive.Checked.ToString());
            sura.CreatedByUserId = currentUser.UserId;

            if (ddLang.GetValue().Trim() == "ar")
                sura.SuraOrder = int.Parse(txtSortOrder.Text.ToString());
            else
                sura.SuraOrder = int.Parse(ddSelSura.SelectedItem.Value.ToString());
            

            bool res = sura.Save();
			if (res == true)
			{
				string addDate = String.Format(DateTime.Now.ToString(), "mm dd yyyy");
				log.Info("user " + currentUser.Name + " Added Sura : " + sura.Title + " at:  " + addDate);
                if (hdnFromWhere.Value.Contains("?"))
                    WebUtils.SetupRedirect(this, hdnFromWhere.Value + "&st=ok");
                else
                    WebUtils.SetupRedirect(this, hdnFromWhere.Value + "?st=ok");
            }
            else
            {
                if (hdnFromWhere.Value.Contains("?"))
                    WebUtils.SetupRedirect(this, hdnFromWhere.Value + "&st=no");
                else
                    WebUtils.SetupRedirect(this, hdnFromWhere.Value + "?st=no");
            }
        }

        #region OnInit

        override protected void OnInit(EventArgs e)
        {
            if (HttpContext.Current == null) { return; }
            base.OnInit(e);
           
            this.Load += new EventHandler(this.Page_Load);
            btnSave.Click += new EventHandler(btnSave_Click);

        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
        }


        #endregion
    }
}
