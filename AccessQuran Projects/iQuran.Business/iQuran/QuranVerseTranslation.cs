// Author:					Ghalib Ghniem ghalib@ItInfoPlus.com
// Created:				    2015-04-13
// Last Modified:			2015-04-13
// 
 
using System;
using System.Data;
using System.Configuration;
using log4net;
using mojoPortal.Data;
using mojoPortal.Business;
using iQuran.Data;

namespace iQuran.Business
{
	/// <summary>
	/// Represents a Quran Translation Verse 
	/// PROPERTIES:
    /// // SiteID , QuranID , SuraID , VerseID , SuraOrder , VerseOrder , VerseText
	/// 
	/// Explain :
	/// VerseText : The Translation of the Original "OTHMANI" text
	/// </summary>
	public class QuranVerseTranslation
	{
		#region Constructors

        public QuranVerseTranslation()
		{ }

        public QuranVerseTranslation(int siteId, int quranId, int verseId)
        {
            if (verseId > -1)
            {
                GetVerseTranslation(siteId, quranId, verseId);
            }
        }

		#endregion

		#region Private Properties

        private static readonly ILog log = LogManager.GetLogger(typeof(QuranVerseTranslation));

        
        private int siteID = -1;
        private int quranID = -1;
        private int suraID = -1;
        private int verseID = -1;
        private int suraOrder = -1;
        private int verseOrder = -1;
        
        private string verseText = string.Empty;


		#endregion

		#region Public Properties

        public int SiteID
        {
            get { return siteID; }
            set { siteID = value; }
        }

		public int QuranID
		{
			get { return quranID; }
			set { quranID = value; }
		}

        public int SuraID
        {
            get { return suraID; }
            set { suraID = value; }
        }

        public int VerseID
        {
            get { return verseID; }
            set { verseID = value; }
        }

        public int SuraOrder
        {
            get { return suraOrder; }
            set { suraOrder = value; }
        }

        public int VerseOrder
        {
            get { return verseOrder; }
            set { verseOrder = value; }
        }

		public string VerseText
		{
			get { return verseText; }
			set { verseText = value; }
		}


		#endregion

		#region Private Methods

        private void GetVerseTranslation(int siteId, int quranId, int verseId)
        {
            using (IDataReader reader = DBiQuran.iVerse_GetVerse_Translation_Dr(siteId, quranId, verseId))
            {
                if (reader.Read())
                {
                    this.verseID = int.Parse(reader["VerseID"].ToString());
                    this.suraID = int.Parse(reader["SuraID"].ToString());
                    this.quranID = int.Parse(reader["QuranID"].ToString());
                    this.siteID = int.Parse(reader["SiteID"].ToString());
                    this.suraOrder = int.Parse(reader["SuraOrder"].ToString());
                    this.verseOrder = int.Parse(reader["VerseOrder"].ToString());
                    
                    this.VerseText = reader["VerseText"].ToString();
                }
                else
                {
                    if (log.IsErrorEnabled)
                    {
                        log.Error("IDataReader didn't read in Quran.GetiQuran");
                    }


                }
            }



        }

		

		#endregion


	}
}
