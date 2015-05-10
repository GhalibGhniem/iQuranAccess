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
using iQuran.Web.UI.Controls;

namespace iQuran.Web.UI.AdminUI
{

    public partial class iVerseEdit : NonCmsBasePage
    {

        private int quranID = -1;
		private int suraID = -1;
        private int verseID = -1;
        private string qLanguage = string.Empty;
        private bool userCanEdit = false;
        private QuranVerse verse;
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

            verseID = WebUtils.ParseInt32FromQueryString("vid", -1);
            if (verseID > -1)
                hdnVerseID.Value = verseID.ToString();
            else
                verseID = int.Parse(hdnVerseID.Value);


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
			Title = SiteUtils.FormatPageTitle(siteSettings, Resources.iQuranResources.EditiVerseHeader);
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
      
            lnkiVerseManagerAdmin.Text = Resources.iQuranResources.AdminMenuiVerseManagerAdminLink;
            lnkiVerseManagerAdmin.ToolTip = Resources.iQuranResources.AdminMenuiVerseManagerAdminLink;
            lnkiVerseManagerAdmin.NavigateUrl = SiteRoot + "/Admin/AccessQuran/iQuran/iVerseManager.aspx?qid=" + this.quranID + "&sid=" + this.suraID + "&vid=" + this.verseID;

            btnSave.Text = Resources.iQuranCommandsResources.Apply;

            lnkCancel.Text = Resources.Resource.RoleManagerCancelButton;
            lnkCancel.NavigateUrl = Request.RawUrl;
        }

        private void PopulateControls()
        {
            //edTranslation edOthmaniText edVerseTextNM edVerseTextNMAlif

            // info
            Quran quran = null;
            quran = new Quran(siteSettings.SiteId, this.quranID);
            lbliQuranVesrion.Text = quran.Title.ToString();
             
            ddLang.SetValue(quran.QLanguage.Trim());
            string selLangVal = ddLang.GetText().Trim();
            lbliQuranLanguage.Text = selLangVal;
            this.qLanguage = quran.QLanguage.Trim();

            QuranSura sura = null;
            sura = new QuranSura(siteSettings.SiteId, this.suraID);
            lblSuraTitle.Text = sura.Title.ToString().Trim();
            lblSuraSortOrder.Text = sura.SuraOrder.ToString().Trim();
            hdnOrigArabSuraID.Value = sura.SuraOrder.ToString();
             bool isTranslation =false;

            if (this.qLanguage == "ar")
            {
                isTranslation= false;
                // VerseOrder SuraOrder ddHalfNo txtPartNo txtHizbNo txtQuraterNo cbIsActive ddPlace
                divArabicFields.Visible = true;
                divTranslation.Visible = false;
                edTranslation.Visible = false;

                divOthmani.Visible = true;
                edOthmaniText.Visible = true;
                edVerseTextNM.Visible = true;
                edVerseTextNMAlif.Visible = true;
            }
            else
            {
                isTranslation=true;
                divArabicFields.Visible = false;
                divTranslation.Visible = true;
                edTranslation.Visible = true;

                divOthmani.Visible = false;
                edOthmaniText.Visible = false;
                edVerseTextNM.Visible = false;
                edVerseTextNMAlif.Visible = false;
            }



            verse = new QuranVerse(siteSettings.SiteId, this.quranID, this.verseID, isTranslation);
            if (verseID <= 0) { return; }

            // Verse Details
            txtVerseSortOrder.Text = verse.VerseOrder.ToString().Trim();
            ddHalfNo.ClearSelection();
            ddHalfNo.Items.FindByValue(verse.HalfNo.ToString()).Selected = true;
            txtPartNo.Text = verse.PartNo.ToString();
            txtHizbNo.Text = verse.HizbNo.ToString();
            txtQuraterNo.Text = verse.QuraterNo.ToString();

            ddPlace.ClearSelection();
            ddPlace.Items.FindByValue(verse.Place.Trim().ToString()).Selected = true;

            cbIsActive.Checked = verse.IsActive;
            quranID = int.Parse(verse.QuranID.ToString());
            suraID = int.Parse(verse.SuraID.ToString());
            verseID = int.Parse(verse.VerseID.ToString());
			hdnQuranID.Value = quranID.ToString();
			hdnSuraID.Value = suraID.ToString();
            hdnVerseID.Value = verseID.ToString();

            //if (this.qLanguage == "ar")
            //    hdnSuraID.Value = txtSortOrder.Text.ToString();
            //else
            //    hdnSuraID.Value = ddSelSura.SelectedItem.Value.ToString();
            

            if (this.qLanguage == "ar")
            {
                edOthmaniText.Text = verse.VerseText;
                edVerseTextNM.Text = verse.QuranVerseTxt.VerseTextNM;
                edVerseTextNMAlif.Text = verse.QuranVerseTxt.VerseTextNMAlif;
            }
            else
            {
                edTranslation.Text = verse.VerseText;
            }
			
		}

        void btnSave_Click(object sender, EventArgs e)
        {
            this.qLanguage = ddLang.GetValue().Trim();
            string tmpEditorValue = string.Empty;
            bool isTranslation = false;
            bool isExists = false;
            int verseorder = -1;

            if (this.qLanguage == "ar")
            {
                isTranslation = false;
                tmpEditorValue = edTranslation.Text.ToString().Trim();
                if ((edOthmaniText.Text.ToString().Trim().Length < 1) ||
                    (edVerseTextNM.Text.ToString().Trim().Length < 1) ||
                    (edVerseTextNMAlif.Text.ToString().Trim().Length < 1))
                {
                    lblmessage.Visible = true;
                    lblmessage.Text = Resources.iQuranMessagesResources.VerseTextsRequired;
                    return;
                }

                // For arabic original version we can check from the begining for existance of verse or order
                // because the AutoID is inside the arabic version only 
                isExists = QuranVerse.Exists(siteSettings.SiteId, this.quranID, this.suraID, this.verseID, edVerseTextNMAlif.Text.ToString().Trim());
                if (isExists)
                {
                    lblmessage.Visible = true;
                    lblmessage.Text = Resources.iQuranMessagesResources.VerseExists;
                    return;
                }
                
                verseorder = int.Parse(txtVerseSortOrder.Text.ToString());
                isExists = QuranVerse.OrderExists(siteSettings.SiteId, this.quranID, this.suraID, this.verseID, verseorder);
                if (isExists)
                {
                    lblmessage.Visible = true;
                    lblmessage.Text = Resources.iQuranMessagesResources.VerseOrderExists;
                    return;
                }


            }
            else
            {
                isTranslation = true;
                tmpEditorValue = edTranslation.Text.ToString().Trim();
                if (tmpEditorValue.Length < 1)
                {
                    lblmessage.Visible = true;
                    lblmessage.Text = Resources.iQuranMessagesResources.VerseTranslationRequired;
                    return;
                }
            }
	
			int siteid = siteSettings.SiteId;
			currentUser = SiteUtils.GetCurrentSiteUser();
            QuranVerse verse = new QuranVerse(siteid, this.quranID, this.verseID, isTranslation);
            
            verse.SuraID = this.suraID;
			verse.QuranID = this.quranID;
			verse.SiteID = siteid;
            verse.QLanguage = this.qLanguage;
            verse.SuraOrder = int.Parse(lblSuraSortOrder.Text.ToString());

            if (this.qLanguage == "ar")
            {
                // for original rabic verses we relate  VerseID as identity! so we seperate it from translation
                // where it is there related to tables QuranVerses 
                verse.VerseID = this.verseID;

                // fields related to original arabic verses  
                verse.VerseOrder = int.Parse(txtVerseSortOrder.Text.ToString());
                verse.HalfNo = int.Parse(ddHalfNo.SelectedItem.Value.ToString());
                verse.PartNo = int.Parse(txtPartNo.Text.ToString());
                verse.HizbNo = int.Parse(txtHizbNo.Text.ToString());
                verse.QuraterNo = int.Parse(txtQuraterNo.Text.ToString());
                verse.IsActive = bool.Parse(this.cbIsActive.Checked.ToString());
                verse.Place = ddPlace.SelectedItem.Value.Trim();
                verse.CreatedByUserId = currentUser.UserId;
                verse.VerseText = edOthmaniText.Text.ToString().Trim();
                verse.QuranVerseTxt.VerseText = edOthmaniText.Text.ToString().Trim();
                verse.QuranVerseTxt.VerseTextNM = edVerseTextNM.Text.ToString().Trim();
                verse.QuranVerseTxt.VerseTextNMAlif = edVerseTextNMAlif.Text.ToString().Trim();
            }
            else
            {
                // the translation must be for an arabic Sura - Arabic verse:
                // if it is new we have to relate it to some VerseID from the original quran!!!
                // We need to retrive the original arabic verse id from it's order
                int verseOrigOrder = int.Parse(txtVerseSortOrder.Text.ToString()); ;
                verse.VerseID = QuranVerse.GetDefaultVerseID(siteid,verse.SuraOrder, verseOrigOrder);
                // if there is nomsuch realative verse in arabic 
                if (verse.VerseID == 0)
                {
                    lblmessage.Visible = true;
                    lblmessage.Text = Resources.iQuranMessagesResources.VerseNoSuchOrder;
                    return;
                }

                // For the translations we can only here check for existance or order because we need the Verse ID !!
                isExists = QuranVerse.Exists_Translation(siteSettings.SiteId, this.quranID, this.suraID, this.verseID, edTranslation.Text.ToString().Trim());
                if (isExists)
                {
                    lblmessage.Visible = true;
                    lblmessage.Text = Resources.iQuranMessagesResources.VerseExists;
                    return;
                }

                verseorder = int.Parse(txtVerseSortOrder.Text.ToString());
                isExists = QuranVerse.OrderExists_Translation(siteSettings.SiteId, this.quranID, this.suraID, this.verseID, verseorder);
                if (isExists)
                {
                    lblmessage.Visible = true;
                    lblmessage.Text = Resources.iQuranMessagesResources.VerseOrderExists;
                    return;
                }

                verse.VerseText = edTranslation.Text.ToString().Trim();
            }

            bool res = false;
            
            // is it arabic default with auto verseID ? and nomatter false or true we are sending!!
            if (this.qLanguage == "ar")
            {
                res = verse.Save(res);
            }
            else
            {
                bool exists = false;
                // check the translation status is it insert or update?
                exists = QuranVerse.CheckTranslationStatus(verse.SiteID, verse.QuranID, verse.SuraID, verse.VerseID);
                // it is translation with no Auto ID so we have to tell him to insert or update
                res = verse.Save(exists);
            }
            
            if (res == true)
			{
				string addDate = String.Format(DateTime.Now.ToString(), "mm dd yyyy");
				log.Info("user " + currentUser.Name + " Added Verse Version  at:  " + addDate);
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
