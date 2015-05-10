// Author:					Ghalib Ghniem ghalib@ItInfoPlus.com
// Created:				    2015-03-25
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
using iQuran.Web.UI.Controls;

namespace iQuran.Web.UI.AdminUI
{

	public partial class iQuranEdit : NonCmsBasePage
    {
        private static global::System.Globalization.CultureInfo resourceCulture;
        private int quranID = -1;
        private bool userCanEdit = false;
        private Quran quran;
		private SiteUser currentUser = null;
       //private string fromwhere = string.Empty;
        protected string EditPropertiesImage = "~/Data/SiteImages/" + WebConfigSettings.EditPropertiesImage;
		private static readonly ILog log = LogManager.GetLogger(typeof(iQuranEdit));

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
			Title = SiteUtils.FormatPageTitle(siteSettings, Resources.iQuranResources.EditiQuranHeader);
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

            btnSave.Text = Resources.iQuranCommandsResources.Apply;

            lnkCancel.Text = Resources.Resource.RoleManagerCancelButton;
            lnkCancel.NavigateUrl = Request.RawUrl;

            edDescription.WebEditor.ToolBar = ToolBar.Newsletter;
            edDescription.WebEditor.FullPageMode = true;
            edDescription.WebEditor.EditorCSSUrl = string.Empty;

            edDescription.WebEditor.Width = Unit.Percentage(90);
            edDescription.WebEditor.Height = Unit.Pixel(400);

            edTRanslatorDetUrl.WebEditor.ToolBar = ToolBar.Newsletter;
            edTRanslatorDetUrl.WebEditor.FullPageMode = true;
            edTRanslatorDetUrl.WebEditor.EditorCSSUrl = string.Empty;

            edTRanslatorDetUrl.WebEditor.Width = Unit.Percentage(90);
            edTRanslatorDetUrl.WebEditor.Height = Unit.Pixel(400);
   
        }

        private void PopulateControls()
        {

            if (quranID > -1)
            {
                quran = new Quran(siteSettings.SiteId, quranID);
            }
            else 
            {
                CheckIfDefault(false);
                return; 
            }

			lblSuraCount.Text = quran.SuraCount.ToString();
			txtTitle.Text = quran.Title;
			cbIsDefault.Checked = quran.IsDefault;
			cbIsActive.Checked = quran.IsActive;
			quranID = int.Parse(quran.QuranID.ToString());
            edDescription.Text = quran.Description;
            edTRanslatorDetUrl.Text = quran.TRanslatorDetUrl;
            txtTranslationSrc.Text = quran.TranslationSrc;
            hdnQuranID.Value=quranID.ToString();
            ddLang.SetValue(quran.QLanguage.Trim());
            CheckIfDefault(quran.IsDefault);

        }


        private void CheckIfDefault(bool isFilled)
        {
            bool isThereDefault = false;
            if (isFilled == true)
            {
                divDefault.Visible = true;
                cbIsDefault.Enabled = true;
            }
            else
            { 
            // new quran or not default
                isThereDefault = Quran.CheckDefaultExists( SiteId);
                if (isThereDefault == true)
                {
                    cbIsDefault.Enabled = false;
                    divDefault.Visible = false;
                }
                else
                {
                    cbIsDefault.Enabled = true;
                    divDefault.Visible = true;
                }
            }
        }

        void btnSave_Click(object sender, EventArgs e)
        {

            bool isExists = false;
            string qLanguage=ddLang.GetValue().Trim();

            isExists = Quran.Exists(siteSettings.SiteId, this.quranID, qLanguage, this.txtTitle.Text.Trim());
            if (isExists)
            {
                lblmessage.Visible = true;
                lblmessage.Text = Resources.iQuranMessagesResources.TitleExists;
                return;
            }
			
            if (this.txtTitle.Text.Trim().Length < 1)
            {
                lblmessage.Visible = true;
				lblmessage.Text = Resources.iQuranMessagesResources.TitleRequired;
                return;
            }
            
			string tmpEditorValue = string.Empty;
			tmpEditorValue = Regex.Replace((edDescription.Text).ToString(), @"<[^>]*>", String.Empty).Replace("\r\n", String.Empty).Trim();

			if (tmpEditorValue.Length < 1)
			{
				lblmessage.Visible = true;
				lblmessage.Text = Resources.iQuranMessagesResources.QuranDescriptionRequired;
				return;
			}

            if (ddLang.GetValue() == "na")
            {
                lblmessage.Visible = true;
                lblmessage.Text = Resources.iQuranMessagesResources.QuranLanguageRequired;
                return;
            }
           
				
			int siteid = siteSettings.SiteId;
			currentUser = SiteUtils.GetCurrentSiteUser();
            Quran quran = new Quran(siteid, quranID);
            
            quran.QuranID = this.quranID;
            quran.Title = SecurityHelper.RemoveMarkup(this.txtTitle.Text);
			quran.IsActive = bool.Parse(this.cbIsActive.Checked.ToString());
			quran.IsDefault = bool.Parse(this.cbIsDefault.Checked.ToString());
            quran.Description = this.edDescription.Text;
            quran.TRanslatorDetUrl = Regex.Replace((edTRanslatorDetUrl.Text).ToString(), @"<[^>]*>", String.Empty).Replace("\r\n", String.Empty).Trim(); 
            quran.TranslationSrc = txtTranslationSrc.Text + "";
			quran.SiteId = siteid;
			quran.CreatedByUserId = currentUser.UserId;
            quran.QLanguage = qLanguage;
			

			//quran.SuraCount = lblSuraCount.Text;
            bool res = quran.Save();
			if (res == true)
			{
				string addDate = String.Format(DateTime.Now.ToString(), "mm dd yyyy");
				log.Info("user " + currentUser.Name + " Added Quran Version : " + quran.Title + " at:  " + addDate);
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

            SiteUtils.SetupNewsletterEditor(edDescription);
            edDescription.WebEditor.UseFullyQualifiedUrlsForResources = true;

            SiteUtils.SetupNewsletterEditor(edTRanslatorDetUrl);
            edTRanslatorDetUrl.WebEditor.UseFullyQualifiedUrlsForResources = true;

        }


        #endregion
    }
}
