// Author:					Ghalib Ghniem ghalib@ItInfoPlus.com
// Created:				    2015-05-04
// Last Modified:			2015-05-04
// 

using System;
using System.Data;
using System;
using System.Web.UI;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web.Framework;
using Resources;
using iQuran.Web;
using iQuran.Business;
using iQuran.Web.Helper;

namespace iQuran.Web.UI.Pages
{
    public partial class ViewVerse : Page
    {

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += new EventHandler(Page_Load);
        }

        private int siteId = -1;
        private int quranId = -1;
        private int verseId = -1;
        //1 yes, 0 no
        private int isDefault = -1;
            
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params.Get("vid") != null)
            {
                verseId = int.Parse(Request.Params.Get("vid").ToString());
            }

            if (Request.Params.Get("qid") != null)
            {
                quranId = int.Parse(Request.Params.Get("qid").ToString());
            }

            if (Request.Params.Get("sid") != null)
            {
                siteId = int.Parse(Request.Params.Get("sid").ToString());
            }

            if (Request.Params.Get("d") != null)
            {
                isDefault = int.Parse(Request.Params.Get("d").ToString());
            }
           
            if ((verseId > 0) && (quranId > 0))
            {
                ShowVerse(siteId, quranId, verseId, isDefault);
            }
        }

        protected void ShowVerse(int siteid, int quranid, int verseid, int isdefault)
        {
            String verseText = string.Empty;
            bool isTranslation = false;
            if (isdefault == 0)
                isTranslation = true;

            QuranVerse qv = new QuranVerse(siteid, quranid, verseid, isTranslation);
            QuranSura qs = new QuranSura(siteid, qv.SuraID);

            verseText = qv.VerseText;

            if (verseText == String.Empty)
            {
                litVerse.Text = Resources.iQuranMessagesResources.HelpNoVerseAvailable;
            }
            else
            {
                litVerse.Text = " <span class='quran-brawn-meq'> " + verseText + "</span>";
                litInfo.Text = "[" + Resources.iQuranResources.SuraHeader + " - " + qs.Title + " - " +
                                                Resources.iQuranResources.VerseHeader + " - " + qv.VerseOrder + "]";
                this.MetaDescription = verseText;
            }
        }



    }
}