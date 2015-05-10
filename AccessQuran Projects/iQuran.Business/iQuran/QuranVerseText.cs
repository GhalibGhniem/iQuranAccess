// Author:					Ghalib Ghniem ghalib@ItInfoPlus.com
// Created:				    2015-03-22
// Last Modified:			2015-04-06
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
	/// Represents a Quran Verse - Ayah Arabic Texts
	/// PROPERTIES:
    /// // SiteID , QuranID , SuraID , VerseID , SuraOrder , VerseOrder , VerseText , VerseTextNM , VerseTextNMAlif
	/// 
	/// Explain :
	/// VerseText : The Original "OTHMANI" text
	/// VerseTextNM : The Verse Text without Marks - Harakat "7arakat"
	/// VerseTextNMAlif : The Verse Text without Marks - Harakat "7arakat" and all "Alef" types are equal - without Hamzeh
	/// </summary>
	public class QuranVerseText
	{
		#region Constructors

		public QuranVerseText()
		{ }

		//public QuranVerseText(int verseId, int suraId, int quranId, int siteId)
		//{
		//	if (verseId > -1)
		//	{
		//		GetVerseText(verseId, suraId, quranId, siteId);
		//	}
		//}

		#endregion

		#region Private Properties

		private static readonly ILog log = LogManager.GetLogger(typeof(QuranVerseText));

        
        private int siteID = -1;
        private int quranID = -1;
        private int suraID = -1;
        private int verseID = -1;
        private int suraOrder = -1;
        private int verseOrder = -1;
		
        private string verseText = string.Empty;
		private string verseTextNM = string.Empty;
		private string verseTextNMAlif = string.Empty;

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

		public string VerseTextNM
		{
			get { return verseTextNM; }
			set { verseTextNM = value; }
		}

		public string VerseTextNMAlif
		{
			get { return verseTextNMAlif; }
			set { verseTextNMAlif = value; }
		}

		#endregion

		#region Private Methods

		//private void GetVerseText(int verseId, int suraId, int quranId, int siteId)
		//{
		//	using (IDataReader reader = DBiQuran.GetVerseTexts(verseId, suraId, quranId, siteId))
		//	{
		//		if (reader.Read())
		//		{
		//			this.verseID = int.Parse(reader["VerseID"].ToString());
		//			this.suraID = int.Parse(reader["SuraID"].ToString());
		//			this.quranID = int.Parse(reader["QuranID"].ToString());
		//			this.siteID = int.Parse(reader["SiteID"].ToString());

		//			this.VerseText = reader["VerseText"].ToString();
		//			this.VerseTextNM = reader["VerseTextNM"].ToString();
		//			this.VerseTextNMAlif = reader["VerseTextNMAlif"].ToString();
					

		//		}
		//		else
		//		{
		//			if (log.IsErrorEnabled)
		//			{
		//				log.Error("IDataReader didn't read in Quran.GetiQuran");
		//			}


		//		}
		//	}



		//}

		

		#endregion


	}
}
