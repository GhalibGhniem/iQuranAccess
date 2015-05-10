// Author:					Ghalib Ghniem ghalib@ItInfoPlus.com
// Created:				    2015-03-22
// Last Modified:			2015-05-06
// 
 
using System;
using System.Data;
using System.Configuration;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Collections;
using log4net;
using mojoPortal.Data;
using mojoPortal.Business;
using iQuran.Data;

namespace iQuran.Business
{
	/// <summary>
	/// Represents a Quran Verse - Ayah
	/// PROPERTIES:
    /// VerseID , SiteID , SuraID , QuranID , VerseOrder , SuraOrder,  SortOrderInQuran , 
    /// HalfNo , PartNo , HizbNo , QuraterNo , Place
	/// StartTime , EndTime , EndTimeText , 
	/// IsActive , CreatedDate , CreatedBy
	/// VerseText , VerseTextNM , VerseTextNMAlif
	/// VerseText :translation
	///	Note: createddate, SortOrderInQuran will be valued in the SQL in  SP
    ///	Note2: StartTime , EndTime , EndTimeText later for mp3
	/// </summary>
	public class QuranVerse
	{
		#region Constructors

		public QuranVerse()
		{ }

        public QuranVerse(int siteId, int quranId, int verseId, bool isTranslation)
		{
			if (verseId > -1)
			{
                if (isTranslation == false)
                    GetOthmaniVerse(siteId, verseId);
                else
                    GetTranslationVerse(siteId, quranId, verseId);
			}
		}

		#endregion

		#region Private Properties

		private static readonly ILog log = LogManager.GetLogger(typeof(QuranVerse));
		
		private int verseID = -1;
		private int siteID = -1;
		private int suraID = -1;
		private int quranID = -1;

		private int verseOrder = 1;
        private int suraOrder = 1;
		private int sortOrderInQuran = 1;

		// For more details about divissioning Quran read http://ar.wikipedia.org/wiki/%D8%AC%D8%B2%D8%A1_%28%D9%82%D8%B1%D8%A2%D9%86%29
		// in which Half is it? 1 or 2 
		private int halfNo = 1;
		//in which Part - Jusu' is it ? 1-30
		private int partNo = 1;
		// in which Hizb is it ? 1-4
		private int hizbNo = 1;
		// in which Quarter - Rubu3 is it ? 1-4
		private int quraterNo = 1;
        private string place = string.Empty;

		// used for MP3 quran suras to lesten
		private float startTime = 0;
		private float endTime = 0;
		private string endTimeText = string.Empty;

		//Administration fields:
		private bool isActive = true;
		private DateTime createdDate = DateTime.UtcNow;
		private string createdByUserName = string.Empty;
		private int createdByUserID;

		// Verses Text :
		private string qLanguage = string.Empty;
		private QuranVerseText quranVerseTxt = new QuranVerseText();

        // this will be used to pass both arabic or translation VerseText
        private string verseText = string.Empty;

		#endregion

		#region Public Properties

		public int VerseID
		{
			get { return verseID; }
			set { verseID = value; }
		}

		public int SuraID
		{
			get { return suraID; }
			set { suraID = value; }
		}

		public int QuranID
		{
			get { return quranID; }
			set { quranID = value; }
		}

		public int SiteID
		{
			get { return siteID; }
			set { siteID = value; }
		}

        public int VerseOrder
		{
			get { return verseOrder; }
			set { verseOrder = value; }
		}

        public int SuraOrder
        {
            get { return suraOrder; }
            set { suraOrder = value; }
        }

		public int SortOrderInQuran
		{
			get { return sortOrderInQuran; }
			set { sortOrderInQuran = value; }
		}

		public int HalfNo
		{
			get { return halfNo; }
			set { halfNo = value; }
		}

		public int PartNo
		{
			get { return partNo; }
			set { partNo = value; }
		}

		public int HizbNo
		{
			get { return hizbNo; }
			set { hizbNo = value; }
		}

		public int QuraterNo
		{
			get { return quraterNo; }
			set { quraterNo = value; }
		}

        public string Place
        {
            get { return place; }
            set { place = value; }
        }

		public float StartTime
		{
			get { return startTime; }
			set { startTime = value; }
		}

		public float EndTime
		{
			get { return endTime; }
			set { endTime = value; }
		}

		public string EndTimeText
		{
			get { return endTimeText; }
			set { endTimeText = value; }
		}

		public bool IsActive
		{
			get { return isActive; }
			set { isActive = value; }
		}

		public DateTime CreatedDate
		{
			get { return createdDate; }
			set { createdDate = value; }
		}

		public string CreatedByUserName
		{
			get { return createdByUserName; }
			set { createdByUserName = value; }
		}

		public int CreatedByUserId
		{
			get { return createdByUserID; }
			set { createdByUserID = value; }
		}

		public string QLanguage
		{
			get { return qLanguage; }
			set { qLanguage = value; }
		}

		public QuranVerseText QuranVerseTxt
		{
			get { return quranVerseTxt; }
			set { quranVerseTxt = value; }
		}

		public string VerseText
		{
			get { return verseText; }
			set { verseText = value; }
		}


		#endregion

		#region Private Methods

        private void GetOthmaniVerse(int siteId, int verseid)
		{
            using (IDataReader reader = DBiQuran.iVerse_GetOthmaniVerseDetails(siteId, verseid))
			{
				if (reader.Read())
				{
					this.verseID = int.Parse(reader["VerseID"].ToString());
					this.suraID = int.Parse(reader["SuraID"].ToString());
					this.quranID = int.Parse(reader["QuranID"].ToString());
					this.siteID = int.Parse(reader["SiteID"].ToString());

                    this.verseOrder = int.Parse(reader["VerseOrder"].ToString());
                    this.suraOrder = int.Parse(reader["SuraOrder"].ToString());
					this.sortOrderInQuran = int.Parse(reader["SortOrderInQuran"].ToString());

					this.halfNo = int.Parse(reader["HalfNo"].ToString());
					this.partNo = int.Parse(reader["PartNo"].ToString());
					this.hizbNo = int.Parse(reader["HizbNo"].ToString());
					this.quraterNo = int.Parse(reader["QuraterNo"].ToString());

                    this.place = reader["Place"].ToString().Trim();
					this.startTime = float.Parse(reader["StartTime"].ToString());
                    this.endTime = float.Parse(reader["EndTime"].ToString());
                    this.endTimeText = reader["EndTimeText"].ToString().Trim();

					string active = reader["IsActive"].ToString();
					this.isActive = (active == "True" || active == "1");
					this.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                    this.createdByUserName = reader["UserName"].ToString().Trim();
					this.createdByUserID = int.Parse(reader["CreatedBy"].ToString());
                    this.qLanguage = reader["QLanguage"].ToString().Trim();
                    
                    this.verseText = reader["VerseText"].ToString();
                    this.quranVerseTxt.VerseText = reader["VerseText"].ToString();
					this.quranVerseTxt.VerseTextNM = reader["VerseTextNM"].ToString();
					this.quranVerseTxt.VerseTextNMAlif = reader["VerseTextNMAlif"].ToString();
					
				}
				else
				{
					if (log.IsErrorEnabled)
					{
						log.Error("IDataReader didn't read in Quran.GetVerse");
					}

				}
			}

		}

        private void GetTranslationVerse(int siteId, int quranId, int verseid)
        {
            using (IDataReader reader = DBiQuran.iVerse_GetTranslationVerseDetails(siteId, quranId, verseid))
            {
                if (reader.Read())
                {
                    this.verseID = int.Parse(reader["VerseID"].ToString());
                    this.suraID = int.Parse(reader["SuraID"].ToString());
                    this.quranID = int.Parse(reader["QuranID"].ToString());
                    this.siteID = int.Parse(reader["SiteID"].ToString());

                    this.verseOrder = int.Parse(reader["VerseOrder"].ToString());
                    this.suraOrder = int.Parse(reader["SuraOrder"].ToString());
                    this.sortOrderInQuran = int.Parse(reader["SortOrderInQuran"].ToString());

                    this.halfNo = int.Parse(reader["HalfNo"].ToString());
                    this.partNo = int.Parse(reader["PartNo"].ToString());
                    this.hizbNo = int.Parse(reader["HizbNo"].ToString());
                    this.quraterNo = int.Parse(reader["QuraterNo"].ToString());

                    this.place = reader["Place"].ToString().Trim();
                    this.startTime = float.Parse(reader["StartTime"].ToString());
                    this.endTime = float.Parse(reader["EndTime"].ToString());
                    this.endTimeText = reader["EndTimeText"].ToString().Trim();

                    string active = reader["IsActive"].ToString();
                    this.isActive = (active == "True" || active == "1");
                    this.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                    this.createdByUserName = reader["UserName"].ToString().Trim();
                    this.createdByUserID = int.Parse(reader["CreatedBy"].ToString());
                    this.qLanguage = reader["QLanguage"].ToString().Trim();
                    
                    this.verseText = reader["VerseText"].ToString();
                
                }
                else
                {
                    if (log.IsErrorEnabled)
                    {
                        log.Error("IDataReader didn't read in Quran.GetVerse");
                    }

                }
            }

        }

		private bool Create()
		{
			int newID = -1;

			if (this.qLanguage == "ar")
			{
                newID = DBiQuran.iVerse_CreateVerse_Text(this.siteID, this.quranID, this.suraID, this.verseOrder, this.suraOrder,  
					halfNo, this.partNo, this.hizbNo, this.quraterNo, this.place,
					this.startTime, this.endTime, this.endTimeText, this.isActive, this.createdByUserID,
					this.quranVerseTxt.VerseText, this.quranVerseTxt.VerseTextNM, this.quranVerseTxt.VerseTextNMAlif);
			}
			else 
			{
                newID = DBiQuran.iVerse_CreateVerse_Transl(this.siteID, this.quranID, this.suraID, this.verseID, 
                    this.suraOrder, this.verseOrder, this.VerseText);
			}

            DBiQuran.iQuran_iSura_Increment_VerseCount(this.siteID, this.quranID, this.suraID);
			this.verseID = newID;

			return (newID > -1);

		}
		
		private bool Update()
		{
			if (this.qLanguage == "ar")
			{
                return DBiQuran.iVerse_UpdateVerse_Text(this.siteID, this.quranID, this.suraID, this.verseID, this.verseOrder, this.suraOrder, 
                    this.halfNo, this.partNo, this.hizbNo, this.quraterNo, this.place,
					this.startTime, this.endTime, this.endTimeText, this.isActive, this.createdByUserID,
                    this.verseText, this.quranVerseTxt.VerseTextNM, this.quranVerseTxt.VerseTextNMAlif);
			}
			else
			{
                return DBiQuran.iVerse_UpdateVerse_Transl(this.siteID, this.quranID, this.suraID, this.verseID,
                    this.suraOrder, this.verseOrder, this.VerseText);
			}
		}


		private static List<QuranVerse> LoadListFromReader(IDataReader reader, bool isTranslation)
		{
			List<QuranVerse> iVList = new List<QuranVerse>();
			try
			{
				while (reader.Read())
				{
					QuranVerse iVerse = new QuranVerse();

					iVerse.verseID = int.Parse(reader["VerseID"].ToString());
					iVerse.suraID = int.Parse(reader["SuraID"].ToString());
					iVerse.quranID = int.Parse(reader["QuranID"].ToString());
					iVerse.siteID = int.Parse(reader["SiteID"].ToString());

                    iVerse.verseOrder = int.Parse(reader["VerseOrder"].ToString());
                    iVerse.suraOrder = int.Parse(reader["SuraOrder"].ToString());
                    iVerse.sortOrderInQuran = int.Parse(reader["SortOrderInQuran"].ToString());

                    iVerse.halfNo = int.Parse(reader["HalfNo"].ToString());
                    iVerse.partNo = int.Parse(reader["PartNo"].ToString());
                    iVerse.hizbNo = int.Parse(reader["HizbNo"].ToString());
                    iVerse.quraterNo = int.Parse(reader["QuraterNo"].ToString());
                    iVerse.place = reader["Place"].ToString().Trim();

                    iVerse.startTime = float.Parse(reader["StartTime"].ToString());
                    iVerse.endTime = float.Parse(reader["EndTime"].ToString());
                    iVerse.endTimeText = reader["EndTimeText"].ToString().Trim();

					string active = reader["IsActive"].ToString();
					iVerse.isActive = (active == "True" || active == "1");
					iVerse.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                    iVerse.createdByUserName = reader["UserName"].ToString().Trim();
					iVerse.createdByUserID = int.Parse(reader["CreatedBy"].ToString());
					iVerse.qLanguage = reader["QLanguage"].ToString().Trim();

                    if (isTranslation == false)
                    {
                        iVerse.verseText = reader["VerseText"].ToString();
                        iVerse.quranVerseTxt.VerseText = reader["VerseText"].ToString();
                        iVerse.quranVerseTxt.VerseTextNM = reader["VerseTextNM"].ToString();
                        iVerse.quranVerseTxt.VerseTextNMAlif = reader["VerseTextNMAlif"].ToString();
                    }
                    else
                    {
                        iVerse.verseText = reader["VerseText"].ToString();
                    }

					iVList.Add(iVerse);
				}
			}
			finally
			{
				reader.Close();
			}

			return iVList;

		}

		#endregion

		#region Public Methods

        public bool Save(bool exists)
		{
            bool res = false;
            // is it arabic default with auto verseID ?
            if (this.qLanguage == "ar")
            {
                if (this.verseID > -1)
                {
                    res = Update();
                }
                else
                {
                    res = Create();
                }
            }
            else 
            {
                // it is translation with not Auto ID
                if (exists == false)
                {
                    res = Create();
                }
                else
                {
                    res = Update();
                }
            }
            return res;

		}

		#endregion

		#region Static Methods

        public static bool Exists(int siteId, int quranID, int suraID, int verseID, string verseTextNMAlif)
        {
            return DBiQuran.iVerse_Exists(siteId, quranID, suraID, verseID, verseTextNMAlif);
        }

        public static bool OrderExists(int siteId, int quranID, int suraID, int verseID, int verseOrder)
        {
            return DBiQuran.iVerse_OrderExists(siteId, quranID, suraID, verseID, verseOrder);
        }

        public static bool Exists_Translation(int siteId, int quranID, int suraID, int verseID, string verseText)
        {
            return DBiQuran.iVerse_Exists_Translation(siteId, quranID, suraID, verseID, verseText);
        }

        public static bool OrderExists_Translation(int siteId, int quranID, int suraID, int verseID, int verseOrder)
        {
            return DBiQuran.iVerse_OrderExists_Translation(siteId, quranID, suraID, verseID, verseOrder);
        }

        public static bool DeleteVerse(int siteId, int quranId, int suraId, int verseid)
		{
            DBiQuran.iQuran_iSura_Decrement_VerseCount(siteId, quranId, suraId);
            return DBiQuran.iVerse_Delete(siteId, quranId, suraId, verseid);
		}

        public static bool DeleteVerse_Translation(int siteId, int quranId, int suraId, int verseid)
        {
            DBiQuran.iQuran_iSura_Decrement_VerseCount(siteId, quranId, suraId);
            return DBiQuran.iVerse_Delete_Translation(siteId, quranId, suraId, verseid);
        }

		public static IDataReader GetVersesDr(int siteId, int suraid)
		{
			return DBiQuran.iVerse_GetVerses_Dr(siteId, suraid);
		}

        public static IDataReader GetVersesTexts(int siteId, int suraid)
        {
            return DBiQuran.iVerse_GetVersesTexts(siteId, suraid);
        }

        /// <summary>
        /// Used to retrive the original arabic verse id from it's order
        /// </summary>
        public static int GetDefaultVerseID(int siteId, int suraOrder, int OrigVerseOrder)
        {
            return DBiQuran.iVerse_GetVerseDefaultID(siteId, suraOrder, OrigVerseOrder);
        }

        public static bool CheckTranslationStatus(int siteId, int quranId, int suraId, int verseid)
        {
            return DBiQuran.iVerse_Translation_CheckStatus(siteId, quranId, suraId, verseid);
        }

        public static int GetVersePageNumber(int siteId, int suraId, int verseid)
        {
            return DBiQuran.iVerse_GetVersePageNumber(siteId, suraId, verseid);
        }

        public static string GetVerseBism(int siteId, int quranId, bool isTranslation)
        {
            return DBiQuran.iVerse_GetVerseBism(siteId, quranId, isTranslation);
        }

		#endregion

		#region "SEARCH"

		/// <summary>
		/// Search All - iVerse version instances as IList with paging.
		/// </summary>
		/// <param name="siteId"></param>
		/// <param name="quranID"></param>
		/// <param name="suraID"></param>
		/// <param name="isAdmin"> used to know to retrive data for front end or for back end pages</param>
        /// <param name="isTranslation"> To tell SP to search within the translation or the arabic texts</param>
		/// <param name="pageNumber"></param>
		/// <param name="pageSize"></param>
		/// <param name="totalPages"></param>
		/// <returns></returns>
        public static List<QuranVerse> GetPage_iVerse_All(int siteId, int quranID, int suraID, bool isTranslation, int pageNumber, int pageSize, out int totalPages)
		{
			totalPages = 1;
            IDataReader reader = DBiQuran.iVerse_GetPage_All(siteId, quranID, suraID, isTranslation,pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader, isTranslation);
		}

		/// <summary>
		/// Search By Vese Word /Part- iVerse version instances as IList with paging.
		/// </summary>
		/// <param name="siteId"></param>
		/// <param name="quranID"></param>
		/// <param name="suraID"></param>
		/// <param name="isAdmin">used to know to retrive data for front end or for back end pages</param>
        /// <param name="isTranslation"> To tell SP to search within the translation or the arabic texts</param>
		/// <param name="Title">Search Criteria</param>
		/// <param name="pageNumber"></param>
		/// <param name="pageSize"></param>
		/// <param name="totalPages"></param>
		/// <returns></returns>
		public static List<QuranVerse> GetPage_iVerse_ByTitle(int siteId, int quranID, int suraID, bool isTranslation, string Title, int pageNumber, int pageSize, out int totalPages)
		{
			totalPages = 1;
            IDataReader reader = DBiQuran.iVerse_GetPage_ByTitle(siteId, quranID, suraID, isTranslation, Title, pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader, isTranslation);
		}

		/// <summary>
		/// Search By Criteria - iVerse version instances as IList with paging
		/// </summary>
		/// <param name="siteId"></param>
		/// <param name="quranID"></param>
		/// <param name="suraID"></param>
		/// <param name="isActive"></param>
        /// <param name="isTranslation"> To tell SP to search within the translation or the arabic texts</param>
		/// <param name="pageNumber"></param>
		/// <param name="pageSize"></param>
		/// <param name="totalPages"></param>
		/// <returns></returns>
        public static List<QuranVerse> GetPage_iVerse_ByActive(int siteId, int quranID, int suraID, bool isActive, bool isTranslation, int pageNumber, int pageSize, out int totalPages)
		{
			totalPages = 1;
            IDataReader reader = DBiQuran.iVerse_GetPage_ByActive(siteId, quranID, suraID, isActive, isTranslation, pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader, isTranslation);
		}

		#endregion

	}
}
