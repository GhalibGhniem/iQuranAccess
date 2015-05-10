// Author:					Ghalib Ghniem ghalib@ItInfoPlus.com
// Created:				    2015-03-22
// Last Modified:			2015-05-09
// 
 
using System;
using System.IO;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using mojoPortal.Data;

namespace iQuran.Data
{



	public static class DBiQuran
	{

		#region "iQURAN"

		/// <summary>
		/// Represents a Quran Version
		/// Description: Quran Version - means a translation or the arabic original Othmani quran
		/// PROPERTIES:
        /// QuranID , SiteID , Title , Description , SuraCount , IsDefault , QLanguage , IsActive , TRanslatorDetUrl , TranslationSrc , CreatedDate , CreatedBy
		/// Note: createddate, SuraCount  will be valued in the SQL in  SP
		/// </summary>
		
        
		public static int iQuran_Create(int siteID, string title, string description,
								bool isDefault, string qLanguage, string tRanslatorDetUrl , string translationSrc, bool isActive, int createdBy)
		{
			SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iQuran_Insert", 9);
			sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
			sph.DefineSqlParameter("@Title", SqlDbType.NVarChar, 250, ParameterDirection.Input, title);
			sph.DefineSqlParameter("@Description", SqlDbType.NVarChar, -1, ParameterDirection.Input, description);
			sph.DefineSqlParameter("@IsDefault", SqlDbType.Bit, ParameterDirection.Input, isDefault);
			sph.DefineSqlParameter("@QLanguage", SqlDbType.NChar, 10, ParameterDirection.Input, qLanguage);
			sph.DefineSqlParameter("@IsActive", SqlDbType.Bit, ParameterDirection.Input, isActive);
            sph.DefineSqlParameter("@TRanslatorDetUrl", SqlDbType.NVarChar, -1, ParameterDirection.Input, tRanslatorDetUrl);
            sph.DefineSqlParameter("@TranslationSrc", SqlDbType.NVarChar, 250, ParameterDirection.Input, translationSrc);
			sph.DefineSqlParameter("@CreatedBy", SqlDbType.Int, ParameterDirection.Input, createdBy);

			int newID = Convert.ToInt32(sph.ExecuteScalar());
			return newID;
		}
        
		public static bool iQuran_Update(int siteID, int quranId, string title, string description,
                                bool isDefault, string qLanguage, bool isActive, string tRanslatorDetUrl, string translationSrc, int createdBy)
		{
			SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iQuran_Update", 10);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID); 
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranId);
			sph.DefineSqlParameter("@Title", SqlDbType.NVarChar, 250, ParameterDirection.Input, title);
			sph.DefineSqlParameter("@Description", SqlDbType.NVarChar, -1, ParameterDirection.Input, description);
			sph.DefineSqlParameter("@IsDefault", SqlDbType.Bit, ParameterDirection.Input, isDefault);
			sph.DefineSqlParameter("@QLanguage", SqlDbType.NChar, 10, ParameterDirection.Input, qLanguage);
			sph.DefineSqlParameter("@IsActive", SqlDbType.Bit, ParameterDirection.Input, isActive);
            sph.DefineSqlParameter("@TRanslatorDetUrl", SqlDbType.NVarChar, -1, ParameterDirection.Input, tRanslatorDetUrl);
            sph.DefineSqlParameter("@TranslationSrc", SqlDbType.NVarChar, 250, ParameterDirection.Input, translationSrc);
			sph.DefineSqlParameter("@CreatedBy", SqlDbType.Int, ParameterDirection.Input, createdBy);

			int rowsAffected = sph.ExecuteNonQuery();
			return (rowsAffected > -1);
		}

        public static bool iQuran_DefaultExists(int siteID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iQuran_DefaultExists", 1);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            int count = Convert.ToInt32(sph.ExecuteScalar());
            return (count > 0);
        }

        public static bool iQuran_Exists(int siteID, int quranID, string qLanguage, string title)
		{
			SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iQuran_Exists", 4);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            sph.DefineSqlParameter("@QLanguage", SqlDbType.NChar, 10, ParameterDirection.Input, qLanguage);
			sph.DefineSqlParameter("@Title", SqlDbType.NVarChar, 250, ParameterDirection.Input, title);
            int count = Convert.ToInt32(sph.ExecuteScalar());
            return (count > 0);
		}

        public static bool iQuran_Delete(int siteID, int quranID)
		{
			SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iQuran_Delete", 2);
			sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            int rowsAffected = sph.ExecuteNonQuery();
			return (rowsAffected > -1);
		}
        
		public static bool iQuran_Delete_BySite(int siteId)
		{
			SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iQuran_DeleteBySite", 1);
			sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
			int rowsAffected = sph.ExecuteNonQuery();
			return (rowsAffected > -1);

		}
        
		public static IDataReader iQuran_GetiQuran( int siteID, int quranId)
		{
			SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "itinfo_iQuran_SelectOne", 2);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranId);
			
			return sph.ExecuteReader();
		}
        
		public static IDataReader iQuran_GetiQurans_Dr(int siteId)
		{
			SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "itinfo_iQuran_SelectAll", 1);
			sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
			return sph.ExecuteReader();
		}
        
		public static bool iQuran_Increment_SuraCount(int siteID, int quranId)
		{
			SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iQuran_Increment_SuraCount", 2);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranId);
			
			int rowsAffected = sph.ExecuteNonQuery();
			return (rowsAffected > -1);
		}
        
		public static bool iQuran_Decrement_SuraCount(int siteID, int quranId)
		{
			SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iQuran_Decrement_SuraCount", 2);
			sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranId);
            
            int rowsAffected = sph.ExecuteNonQuery();
			return (rowsAffected > -1);
		}
		 //all 
		public static int iQuran_GetCount(int siteId, bool isAdmin)
		{
			SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iQuran_GetCount", 2);
			sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
			sph.DefineSqlParameter("@IsAdmin", SqlDbType.Bit, ParameterDirection.Input, isAdmin);
			int count = Convert.ToInt32(sph.ExecuteScalar());
			return (count);
		}
		 //by title 
		public static int iQuran_GetCount(int siteId, bool isAdmin, string title)
		{
			SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iQuran_GetCount_SearchByTitle", 3);
			sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
			sph.DefineSqlParameter("@IsAdmin", SqlDbType.Bit, ParameterDirection.Input, isAdmin);
			sph.DefineSqlParameter("@Title", SqlDbType.NVarChar, 250, ParameterDirection.Input, title);
			int count = Convert.ToInt32(sph.ExecuteScalar());
			return (count);
		}
		 //by Criteria Active 
		public static int iQuran_GetCount_SearchByCriteriaActive(int siteId, bool isActive)
		{
			SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iQuran_GetCount_SearchByCriteriaActive", 2);
			sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
			sph.DefineSqlParameter("@IsActive", SqlDbType.Bit, ParameterDirection.Input, isActive);
			int count = Convert.ToInt32(sph.ExecuteScalar());
			return (count);
		}
	
		#endregion

		#region "SURA"

		/// <summary>
		/// PROPERTIES:
		/// SuraID , SiteID , QuranID , Title , Place , VersesCount , SuraOrder , 
		/// IsActive , CreatedDate , CreatedBy
		/// createddate, VersesCount will be valued in the SQL in  SP
		/// </summary>
		
		
		public static int iSura_Create(int siteID, int quranID, string title, string place,
			int suraOrder, bool isActive, int createdBy)
		{
			SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iQuranSura_Insert", 7);
			sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID); 
			sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
			sph.DefineSqlParameter("@Title", SqlDbType.NVarChar, 250, ParameterDirection.Input, title);
			sph.DefineSqlParameter("@Place", SqlDbType.NVarChar, 50, ParameterDirection.Input, place);
			sph.DefineSqlParameter("@SuraOrder", SqlDbType.Int, ParameterDirection.Input, suraOrder);
			sph.DefineSqlParameter("@IsActive", SqlDbType.Bit, ParameterDirection.Input, isActive);
			sph.DefineSqlParameter("@CreatedBy", SqlDbType.Int, ParameterDirection.Input, createdBy);

			int newID = Convert.ToInt32(sph.ExecuteScalar());
			return newID;
		}
        
        public static bool iSura_Update(int siteID, int quranID, int suraID, string title, string place,
			int suraOrder, bool isActive, int createdBy)
		{
			SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iQuranSura_Update", 8);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            sph.DefineSqlParameter("@SuraID", SqlDbType.Int, ParameterDirection.Input, suraID);
            sph.DefineSqlParameter("@Title", SqlDbType.NVarChar, 250, ParameterDirection.Input, title);
			sph.DefineSqlParameter("@Place", SqlDbType.NVarChar, 50, ParameterDirection.Input, place);
			sph.DefineSqlParameter("@SuraOrder", SqlDbType.Int, ParameterDirection.Input, suraOrder);
			sph.DefineSqlParameter("@IsActive", SqlDbType.Bit, ParameterDirection.Input, isActive);
			sph.DefineSqlParameter("@CreatedBy", SqlDbType.Int, ParameterDirection.Input, createdBy);

			int rowsAffected = sph.ExecuteNonQuery();
			return (rowsAffected > -1);
		}
        
        public static bool iSura_Exists( int siteID, int quranId, int suraID, string title)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iQuranSura_Exists", 4);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranId);
            sph.DefineSqlParameter("@SuraID", SqlDbType.Int, ParameterDirection.Input, suraID);
            sph.DefineSqlParameter("@Title", SqlDbType.NVarChar, 250, ParameterDirection.Input, title);
            int count = Convert.ToInt32(sph.ExecuteScalar());
            return (count > 0);
        }

        public static bool iSura_OrderExists(int siteID, int quranId, int suraID, int suraOrder)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iQuranSura_OrderExists", 4);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranId);
            sph.DefineSqlParameter("@SuraID", SqlDbType.Int, ParameterDirection.Input, suraID);
            sph.DefineSqlParameter("@SuraOrder", SqlDbType.Int, ParameterDirection.Input, suraOrder);
            int count = Convert.ToInt32(sph.ExecuteScalar());
            return (count > 0);
        }
        
		public static bool iSura_Delete(int siteID, int quranId, int suraId)
		{
			SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iQuranSura_Delete", 3);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranId);
            sph.DefineSqlParameter("@SuraID", SqlDbType.Int, ParameterDirection.Input, suraId);
			int rowsAffected = sph.ExecuteNonQuery();
			return (rowsAffected > -1);
		}
        
        public static IDataReader iSura_GetSura(int siteID, int suraId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "itinfo_iQuranSura_SelectOne", 2);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@SuraID", SqlDbType.Int, ParameterDirection.Input, suraId);
            return sph.ExecuteReader();
        }
        
		public static IDataReader iSura_GetSuras_Dr(int siteID, int quranId)
		{
			SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "itinfo_iQuranSura_SelectAll", 2);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranId);
			return sph.ExecuteReader();
		}
        
		public static int iSura_GetSura_VersesCount(int siteID, int suraId)
		{
			SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "itinfo_iQuranSura_VersesCount", 2);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@SuraID", SqlDbType.Int, ParameterDirection.Input, suraId);

			int count = Convert.ToInt32(sph.ExecuteScalar());
			return count;
		}

        public static bool iQuran_iSura_Increment_VerseCount(int siteID, int quranId, int suraId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iQuranSura_Increment_VersesCount", 3);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranId);
            sph.DefineSqlParameter("@SuraID", SqlDbType.Int, ParameterDirection.Input, suraId);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > -1);
        }

        public static bool iQuran_iSura_Decrement_VerseCount(int siteID, int quranId, int suraId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iQuranSura_Decrement_VerseCount", 3);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranId);
            sph.DefineSqlParameter("@SuraID", SqlDbType.Int, ParameterDirection.Input, suraId);

            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > -1);
        }

		 //all 
		public static int iSura_GetCount(int siteId, int quranID, bool isAdmin)
		{
			SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iQuranSura_GetCount", 3);
			sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
			sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
			sph.DefineSqlParameter("@IsAdmin", SqlDbType.Bit, ParameterDirection.Input, isAdmin);
			int count = Convert.ToInt32(sph.ExecuteScalar());
			return (count);
		}
		 //by title 
		public static int iSura_GetCount(int siteId, int quranID, bool isAdmin, string title)
		{
			SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iQuranSura_GetCount_SearchByTitle", 4);
			sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
			sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
			sph.DefineSqlParameter("@IsAdmin", SqlDbType.Bit, ParameterDirection.Input, isAdmin);
			sph.DefineSqlParameter("@Title", SqlDbType.NVarChar, 250, ParameterDirection.Input, title);
			int count = Convert.ToInt32(sph.ExecuteScalar());
			return (count);
		}
		 //by Criteria Active 
		public static int iSura_GetCount_SearchByCriteriaActive(int siteId, int quranID, bool isActive)
		{
			SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iQuranSura_GetCount_SearchByCriteriaActive", 3);
			sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
			sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
			sph.DefineSqlParameter("@IsActive", SqlDbType.Bit, ParameterDirection.Input, isActive);
			int count = Convert.ToInt32(sph.ExecuteScalar());
			return (count);
		}

        /// <summary>
        /// Used to retrive the original arabic verse id from it's order
        /// </summary>
        /// <param name="siteID"></param>
        /// <param name="quranID"></param>
        /// <param name="origVerseOrder"></param>
        /// <returns></returns>
        public static int iVerse_GetVerseDefaultID(int siteID, int suraOrder, int origVerseOrder)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "itinfo_iQuranSura_Verse_Default_GetID", 3);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@SuraOrder", SqlDbType.Int, ParameterDirection.Input, suraOrder);
            sph.DefineSqlParameter("@OrigVerseOrder", SqlDbType.Int, ParameterDirection.Input, origVerseOrder);

            int res = Convert.ToInt32(sph.ExecuteScalar());
            return res;

        }

       
		#endregion

		#region "SHARED Sura-Verses"

		public static IDataReader iVerse_GetVerses_Dr(int siteID, int suraID)
		{
			SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "itinfo_iQuranSura_VersesSelectAll", 2);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@SuraID", SqlDbType.Int, ParameterDirection.Input, suraID);
			return sph.ExecuteReader();
		}

        public static IDataReader iVerse_GetVersesTexts(int siteID, int suraID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "itinfo_iQuranSura_VersesSelectAllText", 2);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@SuraID", SqlDbType.Int, ParameterDirection.Input, suraID);
            return sph.ExecuteReader();
        }

		#endregion

		#region "Verse"

		/// <summary>
		/// PROPERTIES:
        /// VerseID , SiteID , SuraID , QuranID , VerseOrder, SuraOrder , SortOrderInQuran , 
		/// HalfNo , PartNo , HizbNo , QuraterNo , 
		/// StartTime , EndTime , EndTimeText , 
		/// IsActive , CreatedDate , CreatedBy
		/// VerseText , VerseTextNM , VerseTextNMAlif
		///	Note: createddate, SortOrderInQuran will be valued in the SQL in  SP
        ///	Note2: StartTime , EndTime , EndTimeText will be imported directly from excell sheet to the table for this current project
        ///	but i put it in case future needs, you can pass them zeros alternativly
		/// </summary>


        public static int iVerse_CreateVerse_Text(int siteID, int quranID, int suraID, int verseOrder, int suraOrder, 
                            int halfNo, int partNo, int hizbNo, int quraterNo, string place,
							float startTime, float endTime, string endTimeText, bool isActive, int createdBy,
							string verseText, string verseTextNM, string verseTextNMAlif)
		{
			SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iQuranVerses_InsertText", 18);
			sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
			sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            sph.DefineSqlParameter("@SuraID", SqlDbType.Int, ParameterDirection.Input, suraID);
            sph.DefineSqlParameter("@VerseOrder", SqlDbType.Int, ParameterDirection.Input, verseOrder);
            sph.DefineSqlParameter("@SuraOrder", SqlDbType.Int, ParameterDirection.Input, suraOrder);
			sph.DefineSqlParameter("@HalfNo", SqlDbType.Int, ParameterDirection.Input, halfNo);
			sph.DefineSqlParameter("@PartNo", SqlDbType.Int, ParameterDirection.Input, partNo);
			sph.DefineSqlParameter("@HizbNo", SqlDbType.Int, ParameterDirection.Input, hizbNo);
			sph.DefineSqlParameter("@QuraterNo", SqlDbType.Int, ParameterDirection.Input, quraterNo);
            sph.DefineSqlParameter("@Place", SqlDbType.NVarChar, 50, ParameterDirection.Input, place);
			sph.DefineSqlParameter("@StartTime", SqlDbType.Float, ParameterDirection.Input, startTime);
			sph.DefineSqlParameter("@EndTime", SqlDbType.Float, ParameterDirection.Input, endTime);
			sph.DefineSqlParameter("@EndTimeText", SqlDbType.NVarChar, 50, ParameterDirection.Input, endTimeText);
			sph.DefineSqlParameter("@IsActive", SqlDbType.Bit, ParameterDirection.Input, isActive);
			sph.DefineSqlParameter("@CreatedBy", SqlDbType.Int, ParameterDirection.Input, createdBy);
			sph.DefineSqlParameter("@VerseText", SqlDbType.NVarChar, -1, ParameterDirection.Input, verseText);
			sph.DefineSqlParameter("@VerseTextNM", SqlDbType.NVarChar, -1, ParameterDirection.Input, verseTextNM);
			sph.DefineSqlParameter("@VerseTextNMAlif", SqlDbType.NVarChar, -1, ParameterDirection.Input, verseTextNMAlif);

			int newID = Convert.ToInt32(sph.ExecuteScalar());
			return newID;
		}

        public static bool iVerse_UpdateVerse_Text(int siteID, int quranID, int suraID, int verseID, int verseOrder, int suraOrder,
                int halfNo, int partNo, int hizbNo, int quraterNo, string place,
                    float startTime, float endTime, string endTimeText, bool isActive, int createdBy,
                    string verseText, string verseTextNM, string verseTextNMAlif)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iQuranVerses_UpdateText", 19);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            sph.DefineSqlParameter("@SuraID", SqlDbType.Int, ParameterDirection.Input, suraID);
            sph.DefineSqlParameter("@VerseID", SqlDbType.Int, ParameterDirection.Input, verseID);
            sph.DefineSqlParameter("@VerseOrder", SqlDbType.Int, ParameterDirection.Input, verseOrder);
            sph.DefineSqlParameter("@SuraOrder", SqlDbType.Int, ParameterDirection.Input, suraOrder);
            sph.DefineSqlParameter("@HalfNo", SqlDbType.Int, ParameterDirection.Input, halfNo);
            sph.DefineSqlParameter("@PartNo", SqlDbType.Int, ParameterDirection.Input, partNo);
            sph.DefineSqlParameter("@HizbNo", SqlDbType.Int, ParameterDirection.Input, hizbNo);
            sph.DefineSqlParameter("@QuraterNo", SqlDbType.Int, ParameterDirection.Input, quraterNo);
            sph.DefineSqlParameter("@Place", SqlDbType.NVarChar, 50, ParameterDirection.Input, place);
            sph.DefineSqlParameter("@StartTime", SqlDbType.Float, ParameterDirection.Input, startTime);
            sph.DefineSqlParameter("@EndTime", SqlDbType.Float, ParameterDirection.Input, endTime);
            sph.DefineSqlParameter("@EndTimeText", SqlDbType.NVarChar, 50, ParameterDirection.Input, endTimeText);
            sph.DefineSqlParameter("@IsActive", SqlDbType.Bit, ParameterDirection.Input, isActive);
            sph.DefineSqlParameter("@CreatedBy", SqlDbType.Int, ParameterDirection.Input, createdBy);
            sph.DefineSqlParameter("@VerseText", SqlDbType.NVarChar, -1, ParameterDirection.Input, verseText);
            sph.DefineSqlParameter("@VerseTextNM", SqlDbType.NVarChar, -1, ParameterDirection.Input, verseTextNM);
            sph.DefineSqlParameter("@VerseTextNMAlif", SqlDbType.NVarChar, -1, ParameterDirection.Input, verseTextNMAlif);

            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > -1);
        }

        /// <summary>
        /// VerseText is "translation" here
        /// </summary>
		public static int iVerse_CreateVerse_Transl(int siteID, int quranID, int suraID, int verseID, 
            int suraOrder, int verseOrder, string verseText)
		{
			SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iQuranVerses_InsertTransl", 7);
			sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            sph.DefineSqlParameter("@SuraID", SqlDbType.Int, ParameterDirection.Input, suraID);
            sph.DefineSqlParameter("@VerseID", SqlDbType.Int, ParameterDirection.Input, verseID);
            sph.DefineSqlParameter("@SuraOrder", SqlDbType.Int, ParameterDirection.Input, suraOrder);
            sph.DefineSqlParameter("@VerseOrder", SqlDbType.Int, ParameterDirection.Input, verseOrder);
            sph.DefineSqlParameter("@VerseText", SqlDbType.NVarChar, -1, ParameterDirection.Input, verseText);

			int newID = Convert.ToInt32(sph.ExecuteScalar());
			return newID;
		}

        public static bool iVerse_UpdateVerse_Transl(int siteID, int quranID, int suraID, int verseID,
    int suraOrder, int verseOrder, string verseText)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iQuranVerses_UpdateTransl", 7);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            sph.DefineSqlParameter("@SuraID", SqlDbType.Int, ParameterDirection.Input, suraID);
            sph.DefineSqlParameter("@VerseID", SqlDbType.Int, ParameterDirection.Input, verseID);
            sph.DefineSqlParameter("@SuraOrder", SqlDbType.Int, ParameterDirection.Input, suraOrder);
            sph.DefineSqlParameter("@VerseOrder", SqlDbType.Int, ParameterDirection.Input, verseOrder);
            sph.DefineSqlParameter("@VerseText", SqlDbType.NVarChar, -1, ParameterDirection.Input, verseText);

            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > -1);
        }

        public static bool iVerse_Exists(int siteID, int quranId, int suraID, int verseID, string verseTextNMAlif)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iQuranVerse_Exists", 5);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranId);
            sph.DefineSqlParameter("@SuraID", SqlDbType.Int, ParameterDirection.Input, suraID);
            sph.DefineSqlParameter("@VerseID", SqlDbType.Int, ParameterDirection.Input, verseID);
            sph.DefineSqlParameter("@VerseTextNMAlif", SqlDbType.NVarChar, -1, ParameterDirection.Input, verseTextNMAlif);
            int count = Convert.ToInt32(sph.ExecuteScalar());
            return (count > 0);
        }

        public static bool iVerse_OrderExists(int siteID, int quranId, int suraID, int verseID, int verseOrder)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iQuranVerse_OrderExists", 5);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranId);
            sph.DefineSqlParameter("@SuraID", SqlDbType.Int, ParameterDirection.Input, suraID);
            sph.DefineSqlParameter("@VerseID", SqlDbType.Int, ParameterDirection.Input, verseID);
            sph.DefineSqlParameter("@VerseOrder", SqlDbType.Int, ParameterDirection.Input, verseOrder);
            int count = Convert.ToInt32(sph.ExecuteScalar());
            return (count > 0);
        }

        public static bool iVerse_Exists_Translation(int siteID, int quranId, int suraID, int verseID, string verseText)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iQuranVerse_Exists_Translation", 5);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranId);
            sph.DefineSqlParameter("@SuraID", SqlDbType.Int, ParameterDirection.Input, suraID);
            sph.DefineSqlParameter("@VerseID", SqlDbType.Int, ParameterDirection.Input, verseID);
            sph.DefineSqlParameter("@VerseText", SqlDbType.NVarChar, -1, ParameterDirection.Input, verseText);
            int count = Convert.ToInt32(sph.ExecuteScalar());
            return (count > 0);
        }

        public static bool iVerse_OrderExists_Translation(int siteID, int quranId, int suraID, int verseID, int verseOrder)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iQuranVerse_OrderExists_Translation", 5);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranId);
            sph.DefineSqlParameter("@SuraID", SqlDbType.Int, ParameterDirection.Input, suraID);
            sph.DefineSqlParameter("@VerseID", SqlDbType.Int, ParameterDirection.Input, verseID);
            sph.DefineSqlParameter("@VerseOrder", SqlDbType.Int, ParameterDirection.Input, verseOrder);
            int count = Convert.ToInt32(sph.ExecuteScalar());
            return (count > 0);
        }

        public static bool iVerse_Delete(int siteID, int quranID, int suraID, int verseID)
		{
			SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iQuranVerses_Delete", 4);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            sph.DefineSqlParameter("@SuraID", SqlDbType.Int, ParameterDirection.Input, suraID);
            sph.DefineSqlParameter("@VerseID", SqlDbType.Int, ParameterDirection.Input, verseID);
			
			int rowsAffected = sph.ExecuteNonQuery();
			return (rowsAffected > -1);
		}

        public static bool iVerse_Delete_Translation(int siteID, int quranID, int suraID, int verseID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iQuranVerses_Delete_Translation", 4);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            sph.DefineSqlParameter("@SuraID", SqlDbType.Int, ParameterDirection.Input, suraID);
            sph.DefineSqlParameter("@VerseID", SqlDbType.Int, ParameterDirection.Input, verseID);

            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > -1);
        }

        public static IDataReader iVerse_GetOthmaniVerseDetails(int siteID, int verseID)
		{
			SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "itinfo_iQuranVerses_SelectOne_Othmani", 2);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@VerseID", SqlDbType.Int, ParameterDirection.Input, verseID);
			
			return sph.ExecuteReader();
		}

        public static IDataReader iVerse_GetTranslationVerseDetails(int siteID, int quranID, int verseID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "itinfo_iQuranVerses_SelectOne_Translation", 3);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            sph.DefineSqlParameter("@VerseID", SqlDbType.Int, ParameterDirection.Input, verseID);

            return sph.ExecuteReader();
        }

        public static IDataReader iVerse_GetVerse_Text(int siteID, int quranID, int suraID, int verseID)
		{
			SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "itinfo_iQuranVerses_Select_Text", 4);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            sph.DefineSqlParameter("@SuraID", SqlDbType.Int, ParameterDirection.Input, suraID);
            sph.DefineSqlParameter("@VerseID", SqlDbType.Int, ParameterDirection.Input, verseID);
			return sph.ExecuteReader();
		}
        
        public static string iVerse_GetVerse_Translation(int siteID, int quranID, int suraID, int verseID)
		{
			SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iQuranVerses_Select_Translation", 4);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            sph.DefineSqlParameter("@SuraID", SqlDbType.Int, ParameterDirection.Input, suraID);
            sph.DefineSqlParameter("@VerseID", SqlDbType.Int, ParameterDirection.Input, verseID);

			string res = sph.ExecuteScalar().ToString();
			return res;
		}

        public static IDataReader iVerse_GetVerse_Translation_Dr(int siteID, int quranID, int verseID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iQuranVerses_Select_Translation_Details", 3);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            sph.DefineSqlParameter("@VerseID", SqlDbType.Int, ParameterDirection.Input, verseID);
            return sph.ExecuteReader();

        }
		
		 //all 
        // isTranslation :Search within translation or text of the original arabic verse 
        public static int iVerse_GetCount(int siteID, int quranID, int suraID, bool isTranslation)
		{
			SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iQuranVerses_GetCount", 4);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            sph.DefineSqlParameter("@SuraID", SqlDbType.Int, ParameterDirection.Input, suraID);
            sph.DefineSqlParameter("@IsTranslation", SqlDbType.Bit, ParameterDirection.Input, isTranslation);
			int count = Convert.ToInt32(sph.ExecuteScalar());
			return (count);
		}

        /// <param name="isTranslation">Search within translation or text of the original arabic verse </param>
        /// <param name="title">word or sentence from the verse without Harakat - No Marks</param>
        /// <returns></returns>
        public static int iVerse_GetCount(int siteID, int quranID, int suraID, bool isTranslation, string title)
		{
			SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iQuranVerses_GetCount_SearchByTitle", 5);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            sph.DefineSqlParameter("@SuraID", SqlDbType.Int, ParameterDirection.Input, suraID);
            sph.DefineSqlParameter("@IsTranslation", SqlDbType.Bit, ParameterDirection.Input, isTranslation);
			sph.DefineSqlParameter("@Title", SqlDbType.NVarChar, 250, ParameterDirection.Input, title);
			int count = Convert.ToInt32(sph.ExecuteScalar());
			return (count);
		}
		 //by Criteria Active 
        public static int iVerse_GetCount_SearchByCriteriaActive(int siteID, int quranID, int suraID, bool isActive, bool isTranslation)
		{
			SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iQuranVerses_GetCount_SearchByCriteriaActive", 5);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            sph.DefineSqlParameter("@SuraID", SqlDbType.Int, ParameterDirection.Input, suraID);
			sph.DefineSqlParameter("@IsActive", SqlDbType.Bit, ParameterDirection.Input, isActive);
            sph.DefineSqlParameter("@IsTranslation", SqlDbType.Bit, ParameterDirection.Input, isTranslation);
			int count = Convert.ToInt32(sph.ExecuteScalar());
			return (count);
		}

        public static bool iVerse_Translation_CheckStatus(int siteID, int quranID, int suraID, int verseID)
		{
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iQuranVerses_Translation_CheckStatus", 4);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            sph.DefineSqlParameter("@SuraID", SqlDbType.Int, ParameterDirection.Input, suraID);
            sph.DefineSqlParameter("@VerseID", SqlDbType.Int, ParameterDirection.Input, verseID);

            int count = Convert.ToInt32(sph.ExecuteScalar());
            return (count > 0);
		}

        
        public static int iVerse_GetVersePageNumber(int siteID, int suraID, int verseID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "itinfo_iQuranVerses_GetVerse_PageNumber", 3);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@SuraID", SqlDbType.Int, ParameterDirection.Input, suraID);
            sph.DefineSqlParameter("@VerseID", SqlDbType.Int, ParameterDirection.Input, verseID);

            int res = Convert.ToInt32(sph.ExecuteScalar());
            return res;

        }

        public static string iVerse_GetVerseBism(int siteID, int quranID, bool isTranslation)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iQuranVerses_Select_VerseBism", 3);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            sph.DefineSqlParameter("@IsTranslation", SqlDbType.Bit, ParameterDirection.Input, isTranslation);

            string res = sph.ExecuteScalar().ToString();
            return res;
        }

		#endregion

        #region ADMIN SEARCH AND PAGING

        #region "iQuran"

		public static IDataReader iQuran_GetPage_All(int siteID, bool isAdmin, int pageNumber, int pageSize, out int totalPages)
		{
			totalPages = 1;
			int totalRows
				= iQuran_GetCount(siteID, isAdmin);

			if (pageSize > 0) totalPages = totalRows / pageSize;

			if (totalRows <= pageSize)
			{
				totalPages = 1;
			}
			else
			{
				int remainder;
				Math.DivRem(totalRows, pageSize, out remainder);
				if (remainder > 0)
				{
					totalPages += 1;
				}
			}

			SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "itinfo_iQuran_SelectPage", 4);
			sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
			sph.DefineSqlParameter("@IsAdmin", SqlDbType.Bit, ParameterDirection.Input, isAdmin);
			sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
			sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
			return sph.ExecuteReader();

		}

        public static IDataReader iQuran_GetPage_ByTitle(int siteID, bool isAdmin, string title, int pageNumber, int pageSize, out int totalPages)
		{
			totalPages = 1;
			int totalRows
				= iQuran_GetCount(siteID, isAdmin, title);

			if (pageSize > 0) totalPages = totalRows / pageSize;

			if (totalRows <= pageSize)
			{
				totalPages = 1;
			}
			else
			{
				int remainder;
				Math.DivRem(totalRows, pageSize, out remainder);
				if (remainder > 0)
				{
					totalPages += 1;
				}
			}

			SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "itinfo_iQuran_SelectPage_SearchByTitle", 5);
			sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
			sph.DefineSqlParameter("@IsAdmin", SqlDbType.Bit, ParameterDirection.Input, isAdmin);
			sph.DefineSqlParameter("@Title", SqlDbType.NVarChar, 250, ParameterDirection.Input, title);
			sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
			sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
			return sph.ExecuteReader();

		}

        public static IDataReader iQuran_GetPage_ByActive(int siteID, bool isActive, int pageNumber, int pageSize, out int totalPages)
		{
			totalPages = 1;
			int totalRows = 0;
			totalRows = iQuran_GetCount_SearchByCriteriaActive(siteID, isActive);

			if (pageSize > 0) totalPages = totalRows / pageSize;

			if (totalRows <= pageSize)
			{
				totalPages = 1;
			}
			else
			{
				int remainder;
				Math.DivRem(totalRows, pageSize, out remainder);
				if (remainder > 0)
				{
					totalPages += 1;
				}
			}

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "itinfo_iQuran_SelectPage_SearchByCriteriaActive", 4);
			sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
			sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
			sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
			sph.DefineSqlParameter("@IsActive", SqlDbType.Bit, ParameterDirection.Input, isActive);

			return sph.ExecuteReader();

		}

		#endregion

		#region "iSura"

		public static IDataReader iSura_GetPage_All(int siteID, int quranID, bool isAdmin, int pageNumber, int pageSize, out int totalPages)
		{
			totalPages = 1;
			int totalRows
				= iSura_GetCount(siteID, quranID, isAdmin);

			if (pageSize > 0) totalPages = totalRows / pageSize;

			if (totalRows <= pageSize)
			{
				totalPages = 1;
			}
			else
			{
				int remainder;
				Math.DivRem(totalRows, pageSize, out remainder);
				if (remainder > 0)
				{
					totalPages += 1;
				}
			}

			SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "itinfo_iQuranSura_SelectPage", 5);
			sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
			sph.DefineSqlParameter("@IsAdmin", SqlDbType.Bit, ParameterDirection.Input, isAdmin);
			sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
			sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
			return sph.ExecuteReader();

		}

		public static IDataReader iSura_GetPage_ByTitle(int siteID, int quranID, bool isAdmin, string title, int pageNumber, int pageSize, out int totalPages)
		{
			totalPages = 1;
			int totalRows
				= iSura_GetCount(siteID, quranID, isAdmin, title);

			if (pageSize > 0) totalPages = totalRows / pageSize;

			if (totalRows <= pageSize)
			{
				totalPages = 1;
			}
			else
			{
				int remainder;
				Math.DivRem(totalRows, pageSize, out remainder);
				if (remainder > 0)
				{
					totalPages += 1;
				}
			}

			SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "itinfo_iQuranSura_SelectPage_SearchByTitle", 6);
			sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
			sph.DefineSqlParameter("@IsAdmin", SqlDbType.Bit, ParameterDirection.Input, isAdmin);
			sph.DefineSqlParameter("@Title", SqlDbType.NVarChar, 250, ParameterDirection.Input, title);
			sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
			sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
			return sph.ExecuteReader();

		}

		public static IDataReader iSura_GetPage_ByActive(int siteID, int quranID, bool isActive, int pageNumber, int pageSize, out int totalPages)
		{
			totalPages = 1;
			int totalRows = 0;
			totalRows = iSura_GetCount_SearchByCriteriaActive(siteID, quranID, isActive);

			if (pageSize > 0) totalPages = totalRows / pageSize;

			if (totalRows <= pageSize)
			{
				totalPages = 1;
			}
			else
			{
				int remainder;
				Math.DivRem(totalRows, pageSize, out remainder);
				if (remainder > 0)
				{
					totalPages += 1;
				}
			}

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "itinfo_iQuranSura_SelectPage_SearchByCriteriaActive", 5);
			sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
			sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
			sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
			sph.DefineSqlParameter("@IsActive", SqlDbType.Bit, ParameterDirection.Input, isActive);

			return sph.ExecuteReader();

		}

		#endregion

		#region "iVerse"

        public static IDataReader iVerse_GetPage_All(int siteID, int quranID, int suraID, bool isTranslation, int pageNumber, int pageSize, out int totalPages)
		{
			totalPages = 1;
			int totalRows
                = iVerse_GetCount(siteID, quranID, suraID, isTranslation);

			if (pageSize > 0) totalPages = totalRows / pageSize;

			if (totalRows <= pageSize)
			{
				totalPages = 1;
			}
			else
			{
				int remainder;
				Math.DivRem(totalRows, pageSize, out remainder);
				if (remainder > 0)
				{
					totalPages += 1;
				}
			}

			SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "itinfo_iQuranVerses_SelectPage", 6);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            sph.DefineSqlParameter("@SuraID", SqlDbType.Int, ParameterDirection.Input, suraID);
            sph.DefineSqlParameter("@IsTranslation", SqlDbType.Bit, ParameterDirection.Input, isTranslation);
			sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
			sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
			return sph.ExecuteReader();

		}

		public static IDataReader iVerse_GetPage_ByTitle(int siteID, int quranID, int suraID, bool isTranslation, string title, int pageNumber, int pageSize, out int totalPages)
		{
			totalPages = 1;
			int totalRows
				= iVerse_GetCount(siteID, quranID, suraID, isTranslation, title);

			if (pageSize > 0) totalPages = totalRows / pageSize;

			if (totalRows <= pageSize)
			{
				totalPages = 1;
			}
			else
			{
				int remainder;
				Math.DivRem(totalRows, pageSize, out remainder);
				if (remainder > 0)
				{
					totalPages += 1;
				}
			}

			SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "itinfo_iQuranVerses_SelectPage_SearchByTitle", 7);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            sph.DefineSqlParameter("@SuraID", SqlDbType.Int, ParameterDirection.Input, suraID);
            sph.DefineSqlParameter("@IsTranslation", SqlDbType.Bit, ParameterDirection.Input, isTranslation);
			sph.DefineSqlParameter("@Title", SqlDbType.NVarChar, 250, ParameterDirection.Input, title);
			sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
			sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
			return sph.ExecuteReader();

		}
        
        public static IDataReader iVerse_GetPage_ByActive(int siteID, int quranID, int suraID, bool isActive, bool isTranslation, int pageNumber, int pageSize, out int totalPages)
		{
			totalPages = 1;
			int totalRows = 0;

            totalRows = iVerse_GetCount_SearchByCriteriaActive(siteID, quranID, suraID, isActive, isTranslation);

			if (pageSize > 0) totalPages = totalRows / pageSize;

			if (totalRows <= pageSize)
			{
				totalPages = 1;
			}
			else
			{
				int remainder;
				Math.DivRem(totalRows, pageSize, out remainder);
				if (remainder > 0)
				{
					totalPages += 1;
				}
			}

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "itinfo_iQuranVerses_SelectPage_SearchByCriteriaActive", 7);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            sph.DefineSqlParameter("@SuraID", SqlDbType.Int, ParameterDirection.Input, suraID);
            sph.DefineSqlParameter("@IsActive", SqlDbType.Bit, ParameterDirection.Input, isActive);
            sph.DefineSqlParameter("@IsTranslation", SqlDbType.Bit, ParameterDirection.Input, isTranslation);
			sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
			sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            
			

			return sph.ExecuteReader();

		}

		#endregion

		#endregion

        #region "FRONT END VIEWING"



        /// <summary>
        /// Front End Viewr Sura
        /// </summary>
        public static IDataReader iSura_GetFrontPage_Sura(int siteId, int quranID, int suraID,
            int pageNumber, bool isTranslation)
        {

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "itinfo_iQuranSura_FrontEnd_SelectSura", 5);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            sph.DefineSqlParameter("@SuraID", SqlDbType.Int, ParameterDirection.Input, suraID);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@IsTranslation", SqlDbType.Bit, ParameterDirection.Input, isTranslation);
            return sph.ExecuteReader();

        }

         /// <summary>
        /// Front End Get The First SuraID in that page
        /// </summary>
        public static int iVerse_GetSuraIdFromPageNumber(int siteID, int quranID, int pageNumber, bool isTranslation)
		{
			SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "itinfo_iQuranSura_GetSuraID_FromPageNumber", 4);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@IsTranslation", SqlDbType.Bit, ParameterDirection.Input, isTranslation);

			int count = Convert.ToInt32(sph.ExecuteScalar());
			return count;
		}

        /// <summary>
        /// Front End Get Suras Index
        /// </summary>
        public static IDataReader iSura_FrontEnd_GetSurasIndex(int siteID, int quranID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "itinfo_iQuranSura_FrontEnd_Select_SurasIndex", 2);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            return sph.ExecuteReader();

        }

        /// <summary>
        /// Front End View Quran Page
        /// </summary>
        public static IDataReader iQuran_FrontEnd_GetPage(int siteId, int quranID, int pageNumber, bool isTranslation)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "itinfo_iQuran_FrontEnd_SelectPage", 4);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@IsTranslation", SqlDbType.Bit, ParameterDirection.Input, isTranslation);
            return sph.ExecuteReader();

        }

        /// <summary>
        /// Front End View Quran Page Details - Header!
        /// </summary>
        public static IDataReader iQuran_FrontEnd_GetPage_Header(int siteId, int quranID, int pageNumber, bool isTranslation)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "itinfo_iQuran_FrontEnd_SelectPage_Header", 4);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@IsTranslation", SqlDbType.Bit, ParameterDirection.Input, isTranslation);
            return sph.ExecuteReader();

        }
      


        #endregion

        #region FRONT END SIMPLE SEARCH AND PAGING
        /// <summary>
        /// WORDS TYPES : WordOthmani , WordOthmaniNM, WordOthmaniNMAlif , WordDictNM , WordDictNMAlif  See description and more details in class QuranWord
        /// Searched Text Type : Part From - p , Exact word - i , Root - r
        /// Word Fornt Type  : Dict - Dictational (field : WordDictNM) , Oth - Othmani (Field : WordOthmani)
        /// Word Dictional Criteria : WordDictNMAlif , WordDictNM
        /// rbWord Othmani Criteria : WordOthmaniNMAlif , WordOthmaniNM , WordOthmani
        /// </summary>
        ///  

        #region "COUNTS"

        
        public static IDataReader iSearch_GetCounts_Default_ByRoot_Table_Dr(int siteId, int quranID, int strtSuraID, int endSuraID,
    int strtVerseSortOrder, int endVerseSortOrder, string rootWord)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iSearch_GetCount_Default_ByRoot_Table_Dr", 7);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            sph.DefineSqlParameter("@StrtSuraID", SqlDbType.Int, ParameterDirection.Input, strtSuraID);
            sph.DefineSqlParameter("@EndSuraID", SqlDbType.Int, ParameterDirection.Input, endSuraID);
            sph.DefineSqlParameter("@StrtVerseSortOrder", SqlDbType.Int, ParameterDirection.Input, strtVerseSortOrder);
            sph.DefineSqlParameter("@EndVerseSortOrder", SqlDbType.Int, ParameterDirection.Input, endVerseSortOrder);
            sph.DefineSqlParameter("@RootWord", SqlDbType.NVarChar, 50, ParameterDirection.Input, rootWord);
            
            return sph.ExecuteReader();
        }

        public static IDataReader iSearch_GetCounts_Default_ByOnlyWord_Dr(string searchCriteria, int siteId, int quranID, int strtSuraID, int endSuraID,
            int strtVerseSortOrder, int endVerseSortOrder, string textSearchWord)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iSearch_GetCount_Default_ByOnlyWord_Dr", 8);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            sph.DefineSqlParameter("@StrtSuraID", SqlDbType.Int, ParameterDirection.Input, strtSuraID);
            sph.DefineSqlParameter("@EndSuraID", SqlDbType.Int, ParameterDirection.Input, endSuraID);
            sph.DefineSqlParameter("@StrtVerseSortOrder", SqlDbType.Int, ParameterDirection.Input, strtVerseSortOrder);
            sph.DefineSqlParameter("@EndVerseSortOrder", SqlDbType.Int, ParameterDirection.Input, endVerseSortOrder);
            sph.DefineSqlParameter("@TextSearchWord", SqlDbType.NVarChar, 250, ParameterDirection.Input, textSearchWord);
            sph.DefineSqlParameter("@SearchCriteria", SqlDbType.NChar, 20, ParameterDirection.Input, searchCriteria);

            return sph.ExecuteReader();
        }

        public static IDataReader iSearch_GetCounts_Default_ByWordSentence_Dr(string searchCriteria, int siteId, int quranID, int strtSuraID, int endSuraID,
            int strtVerseSortOrder, int endVerseSortOrder, string textSearchWord)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iSearch_GetCount_Default_ByWordSentence_Dr", 8);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            sph.DefineSqlParameter("@StrtSuraID", SqlDbType.Int, ParameterDirection.Input, strtSuraID);
            sph.DefineSqlParameter("@EndSuraID", SqlDbType.Int, ParameterDirection.Input, endSuraID);
            sph.DefineSqlParameter("@StrtVerseSortOrder", SqlDbType.Int, ParameterDirection.Input, strtVerseSortOrder);
            sph.DefineSqlParameter("@EndVerseSortOrder", SqlDbType.Int, ParameterDirection.Input, endVerseSortOrder);
            sph.DefineSqlParameter("@TextSearchWord", SqlDbType.NVarChar, 250, ParameterDirection.Input, textSearchWord);
            sph.DefineSqlParameter("@SearchCriteria", SqlDbType.NChar, 20, ParameterDirection.Input, searchCriteria);

            return sph.ExecuteReader();
        }

        public static IDataReader iSearch_GetCounts_Default_ByWordVerse_Dr(string searchCriteria, int siteId, int quranID, int strtSuraID, int endSuraID,
            int strtVerseSortOrder, int endVerseSortOrder, string textSearchWord)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iSearch_GetCount_Default_ByWordVerse_Dr", 8);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            sph.DefineSqlParameter("@StrtSuraID", SqlDbType.Int, ParameterDirection.Input, strtSuraID);
            sph.DefineSqlParameter("@EndSuraID", SqlDbType.Int, ParameterDirection.Input, endSuraID);
            sph.DefineSqlParameter("@StrtVerseSortOrder", SqlDbType.Int, ParameterDirection.Input, strtVerseSortOrder);
            sph.DefineSqlParameter("@EndVerseSortOrder", SqlDbType.Int, ParameterDirection.Input, endVerseSortOrder);
            sph.DefineSqlParameter("@TextSearchWord", SqlDbType.NVarChar, 250, ParameterDirection.Input, textSearchWord);
            sph.DefineSqlParameter("@SearchCriteria", SqlDbType.NChar, 20, ParameterDirection.Input, searchCriteria);

            return sph.ExecuteReader();
        }

        
        public static int iSearch_GetCount_Default_ByRoot_Table(int siteId, int quranID, int strtSuraID, int endSuraID,
            int strtVerseSortOrder, int endVerseSortOrder, string rootWord)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iSearch_GetCount_Default_ByRoot_Table", 7);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            sph.DefineSqlParameter("@StrtSuraID", SqlDbType.Int, ParameterDirection.Input, strtSuraID);
            sph.DefineSqlParameter("@EndSuraID", SqlDbType.Int, ParameterDirection.Input, endSuraID);
            sph.DefineSqlParameter("@StrtVerseSortOrder", SqlDbType.Int, ParameterDirection.Input, strtVerseSortOrder);
            sph.DefineSqlParameter("@EndVerseSortOrder", SqlDbType.Int, ParameterDirection.Input, endVerseSortOrder);
            sph.DefineSqlParameter("@RootWord", SqlDbType.NVarChar, 50, ParameterDirection.Input, rootWord);
            int count = Convert.ToInt32(sph.ExecuteScalar());
            return (count);
        }
        
        public static int iSearch_GetCount_Default_ByOnlyWord(string searchCriteria, int siteId, int quranID, int strtSuraID, int endSuraID,
            int strtVerseSortOrder, int endVerseSortOrder, string textSearchWord)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iSearch_GetCount_Default_ByOnlyWord", 8);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            sph.DefineSqlParameter("@StrtSuraID", SqlDbType.Int, ParameterDirection.Input, strtSuraID);
            sph.DefineSqlParameter("@EndSuraID", SqlDbType.Int, ParameterDirection.Input, endSuraID);
            sph.DefineSqlParameter("@StrtVerseSortOrder", SqlDbType.Int, ParameterDirection.Input, strtVerseSortOrder);
            sph.DefineSqlParameter("@EndVerseSortOrder", SqlDbType.Int, ParameterDirection.Input, endVerseSortOrder);
            sph.DefineSqlParameter("@TextSearchWord", SqlDbType.NVarChar, 250, ParameterDirection.Input, textSearchWord);
            sph.DefineSqlParameter("@SearchCriteria", SqlDbType.NChar, 20, ParameterDirection.Input, searchCriteria);
            int count = Convert.ToInt32(sph.ExecuteScalar());
            return (count);
        }
        
        public static int iSearch_GetCount_Default_ByWordSentence(string searchCriteria, int siteId, int quranID, int strtSuraID, int endSuraID,
            int strtVerseSortOrder, int endVerseSortOrder, string textSearchWord)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iSearch_GetCount_Default_ByWordSentence", 8);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            sph.DefineSqlParameter("@StrtSuraID", SqlDbType.Int, ParameterDirection.Input, strtSuraID);
            sph.DefineSqlParameter("@EndSuraID", SqlDbType.Int, ParameterDirection.Input, endSuraID);
            sph.DefineSqlParameter("@StrtVerseSortOrder", SqlDbType.Int, ParameterDirection.Input, strtVerseSortOrder);
            sph.DefineSqlParameter("@EndVerseSortOrder", SqlDbType.Int, ParameterDirection.Input, endVerseSortOrder);
            sph.DefineSqlParameter("@TextSearchWord", SqlDbType.NVarChar, 250, ParameterDirection.Input, textSearchWord);
            sph.DefineSqlParameter("@SearchCriteria", SqlDbType.NChar, 20, ParameterDirection.Input, searchCriteria);
            int count = Convert.ToInt32(sph.ExecuteScalar());
            return (count);
        }

        public static int iSearch_GetCount_Default_ByWordVerse(string searchCriteria, int siteId, int quranID, int strtSuraID, int endSuraID,
           int strtVerseSortOrder, int endVerseSortOrder, string textSearchWord)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iSearch_GetCount_Default_ByWordVerse", 8);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            sph.DefineSqlParameter("@StrtSuraID", SqlDbType.Int, ParameterDirection.Input, strtSuraID);
            sph.DefineSqlParameter("@EndSuraID", SqlDbType.Int, ParameterDirection.Input, endSuraID);
            sph.DefineSqlParameter("@StrtVerseSortOrder", SqlDbType.Int, ParameterDirection.Input, strtVerseSortOrder);
            sph.DefineSqlParameter("@EndVerseSortOrder", SqlDbType.Int, ParameterDirection.Input, endVerseSortOrder);
            sph.DefineSqlParameter("@TextSearchWord", SqlDbType.NVarChar, 250, ParameterDirection.Input, textSearchWord);
            sph.DefineSqlParameter("@SearchCriteria", SqlDbType.NChar, 20, ParameterDirection.Input, searchCriteria);
            int count = Convert.ToInt32(sph.ExecuteScalar());
            return (count);
        }
        
        public static int iSearch_GetCount_Translation_ByWordSentence(int siteId, int quranID, int strtSuraID, int endSuraID,
            int strtVerseSortOrder, int endVerseSortOrder, string textSearchWord)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iSearch_GetCount_Translation_ByWordSentence", 7);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            sph.DefineSqlParameter("@StrtSuraID", SqlDbType.Int, ParameterDirection.Input, strtSuraID);
            sph.DefineSqlParameter("@EndSuraID", SqlDbType.Int, ParameterDirection.Input, endSuraID);
            sph.DefineSqlParameter("@StrtVerseSortOrder", SqlDbType.Int, ParameterDirection.Input, strtVerseSortOrder);
            sph.DefineSqlParameter("@EndVerseSortOrder", SqlDbType.Int, ParameterDirection.Input, endVerseSortOrder);
            sph.DefineSqlParameter("@TextSearchWord", SqlDbType.NVarChar, 250, ParameterDirection.Input, textSearchWord);
            int count = Convert.ToInt32(sph.ExecuteScalar());
            return (count);
        }

        #endregion
        
        #region "SEARCHES"
        
        /// <summary>
        /// Search accrding to Root words in table [QuranArabicWordsAll] field: Root - so No criteria are available here
        /// </summary>
        public static IDataReader iSearch_GetPage_Default_ByRoot_Table(int siteId, int quranID, int strtSuraID, int endSuraID,
           int strtVerseSortOrder, int endVerseSortOrder, string rootWord, int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            int totalRows = iSearch_GetCount_Default_ByRoot_Table(siteId, quranID, strtSuraID, endSuraID,
                strtVerseSortOrder, endVerseSortOrder, rootWord);

            if (pageSize > 0) totalPages = totalRows / pageSize;

            if (totalRows <= pageSize)
            {
                totalPages = 1;
            }
            else
            {
                int remainder;
                Math.DivRem(totalRows, pageSize, out remainder);
                if (remainder > 0)
                {
                    totalPages += 1;
                }
            }

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "itinfo_iSearch_SelectPage_Default_ByRoot_Table", 9);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            sph.DefineSqlParameter("@StrtSuraID", SqlDbType.Int, ParameterDirection.Input, strtSuraID);
            sph.DefineSqlParameter("@EndSuraID", SqlDbType.Int, ParameterDirection.Input, endSuraID);
            sph.DefineSqlParameter("@StrtVerseSortOrder", SqlDbType.Int, ParameterDirection.Input, strtVerseSortOrder);
            sph.DefineSqlParameter("@EndVerseSortOrder", SqlDbType.Int, ParameterDirection.Input, endVerseSortOrder);
            sph.DefineSqlParameter("@RootWord", SqlDbType.NVarChar, 50, ParameterDirection.Input, rootWord);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }

       
        /// <summary>
        /// All criteria are available here : WordDictNMAlif , WordDictNM WordOthmaniNMAlif , WordOthmaniNM , WordOthmani
        /// </summary>
        public static IDataReader iSearch_GetPage_Default_ByOnlyWord(string searchCriteria, int siteId, int quranID, int strtSuraID, int endSuraID,
            int strtVerseSortOrder, int endVerseSortOrder, string textSearchWord, int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            int totalRows = iSearch_GetCount_Default_ByOnlyWord(searchCriteria, siteId, quranID, strtSuraID, endSuraID,
                strtVerseSortOrder, endVerseSortOrder, textSearchWord);

            if (pageSize > 0) totalPages = totalRows / pageSize;

            if (totalRows <= pageSize)
            {
                totalPages = 1;
            }
            else
            {
                int remainder;
                Math.DivRem(totalRows, pageSize, out remainder);
                if (remainder > 0)
                {
                    totalPages += 1;
                }
            }

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "itinfo_iSearch_SelectPage_Default_ByOnlyWord", 10);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            sph.DefineSqlParameter("@StrtSuraID", SqlDbType.Int, ParameterDirection.Input, strtSuraID);
            sph.DefineSqlParameter("@EndSuraID", SqlDbType.Int, ParameterDirection.Input, endSuraID);
            sph.DefineSqlParameter("@StrtVerseSortOrder", SqlDbType.Int, ParameterDirection.Input, strtVerseSortOrder);
            sph.DefineSqlParameter("@EndVerseSortOrder", SqlDbType.Int, ParameterDirection.Input, endVerseSortOrder);
            sph.DefineSqlParameter("@TextSearchWord", SqlDbType.NVarChar, 250, ParameterDirection.Input, textSearchWord);
            sph.DefineSqlParameter("@SearchCriteria", SqlDbType.NChar, 20, ParameterDirection.Input, searchCriteria);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }
        
        /// <summary>
        /// Since we are searching for part of word
        /// Only these criteria are available here : WordDictNMAlif WordDictNM WordOthmani
        /// because we will search direct the verses texts and not words tables
        /// </summary>
        public static IDataReader iSearch_GetPage_Default_ByWordSentence(string searchCriteria, int siteId, int quranID, int strtSuraID, int endSuraID,
            int strtVerseSortOrder, int endVerseSortOrder, string textSearchWord, int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            int totalRows = iSearch_GetCount_Default_ByWordSentence(searchCriteria, siteId, quranID, strtSuraID, endSuraID,
                strtVerseSortOrder, endVerseSortOrder, textSearchWord);

            if (pageSize > 0) totalPages = totalRows / pageSize;

            if (totalRows <= pageSize)
            {
                totalPages = 1;
            }
            else
            {
                int remainder;
                Math.DivRem(totalRows, pageSize, out remainder);
                if (remainder > 0)
                {
                    totalPages += 1;
                }
            }

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "itinfo_iSearch_SelectPage_Default_ByWordSentence", 10);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            sph.DefineSqlParameter("@StrtSuraID", SqlDbType.Int, ParameterDirection.Input, strtSuraID);
            sph.DefineSqlParameter("@EndSuraID", SqlDbType.Int, ParameterDirection.Input, endSuraID);
            sph.DefineSqlParameter("@StrtVerseSortOrder", SqlDbType.Int, ParameterDirection.Input, strtVerseSortOrder);
            sph.DefineSqlParameter("@EndVerseSortOrder", SqlDbType.Int, ParameterDirection.Input, endVerseSortOrder);
            sph.DefineSqlParameter("@TextSearchWord", SqlDbType.NVarChar, 250, ParameterDirection.Input, textSearchWord);
            sph.DefineSqlParameter("@SearchCriteria", SqlDbType.NChar, 20, ParameterDirection.Input, searchCriteria);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            
            return sph.ExecuteReader();

        }

        /// <summary>
        /// Since we are searching for part of verse
        /// Only these criteria are available here : WordDictNMAlif WordDictNM WordOthmani
        /// because we will search direct the verses texts and not words tables
        /// </summary>
        public static IDataReader iSearch_GetPage_Default_ByWordVerse(string searchCriteria, int siteId, int quranID, int strtSuraID, int endSuraID,
            int strtVerseSortOrder, int endVerseSortOrder, string textSearchWord, int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            int totalRows = iSearch_GetCount_Default_ByWordVerse(searchCriteria, siteId, quranID, strtSuraID, endSuraID,
                strtVerseSortOrder, endVerseSortOrder, textSearchWord);

            if (pageSize > 0) totalPages = totalRows / pageSize;

            if (totalRows <= pageSize)
            {
                totalPages = 1;
            }
            else
            {
                int remainder;
                Math.DivRem(totalRows, pageSize, out remainder);
                if (remainder > 0)
                {
                    totalPages += 1;
                }
            }

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "itinfo_iSearch_SelectPage_Default_ByWordVerse", 10);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            sph.DefineSqlParameter("@StrtSuraID", SqlDbType.Int, ParameterDirection.Input, strtSuraID);
            sph.DefineSqlParameter("@EndSuraID", SqlDbType.Int, ParameterDirection.Input, endSuraID);
            sph.DefineSqlParameter("@StrtVerseSortOrder", SqlDbType.Int, ParameterDirection.Input, strtVerseSortOrder);
            sph.DefineSqlParameter("@EndVerseSortOrder", SqlDbType.Int, ParameterDirection.Input, endVerseSortOrder);
            sph.DefineSqlParameter("@TextSearchWord", SqlDbType.NVarChar, 250, ParameterDirection.Input, textSearchWord);
            sph.DefineSqlParameter("@SearchCriteria", SqlDbType.NChar, 20, ParameterDirection.Input, searchCriteria);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);

            return sph.ExecuteReader();

        }

        
        public static IDataReader iSearch_GetPage_Translation_ByWordSentence(int siteId, int quranID, int strtSuraID, int endSuraID,
            int strtVerseSortOrder, int endVerseSortOrder, string textSearchWord, int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            int totalRows = iSearch_GetCount_Translation_ByWordSentence(siteId, quranID, strtSuraID, endSuraID,
                strtVerseSortOrder, endVerseSortOrder, textSearchWord);

            if (pageSize > 0) totalPages = totalRows / pageSize;

            if (totalRows <= pageSize)
            {
                totalPages = 1;
            }
            else
            {
                int remainder;
                Math.DivRem(totalRows, pageSize, out remainder);
                if (remainder > 0)
                {
                    totalPages += 1;
                }
            }

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "itinfo_iSearch_SelectPage_Translation_ByWordSentence", 9);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            sph.DefineSqlParameter("@StrtSuraID", SqlDbType.Int, ParameterDirection.Input, strtSuraID);
            sph.DefineSqlParameter("@EndSuraID", SqlDbType.Int, ParameterDirection.Input, endSuraID);
            sph.DefineSqlParameter("@StrtVerseSortOrder", SqlDbType.Int, ParameterDirection.Input, strtVerseSortOrder);
            sph.DefineSqlParameter("@EndVerseSortOrder", SqlDbType.Int, ParameterDirection.Input, endVerseSortOrder);
            sph.DefineSqlParameter("@TextSearchWord", SqlDbType.NVarChar, 250, ParameterDirection.Input, textSearchWord);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }

        #endregion

        #endregion

        #region FRONT END ADVANCED SEARCH AND PAGING

        // All criteria are available here
        // TODO before this root you have to find the roots words may result and wiew them then after selecting one of them come here !
        // because it may be more than one root according to the word entered !!
        public static int iSearch_GetCount_Default_ByRoot(string searchCriteria, int siteId, int quranID, int strtSuraID, int endSuraID,
            int strtVerseSortOrder, int endVerseSortOrder, string textSearchWord)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "itinfo_iSearch_GetCount_Default_ByRoot", 8);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            sph.DefineSqlParameter("@StrtSuraID", SqlDbType.Int, ParameterDirection.Input, strtSuraID);
            sph.DefineSqlParameter("@EndSuraID", SqlDbType.Int, ParameterDirection.Input, endSuraID);
            sph.DefineSqlParameter("@strtVerseSortOrder", SqlDbType.Int, ParameterDirection.Input, strtVerseSortOrder);
            sph.DefineSqlParameter("@endVerseSortOrder", SqlDbType.Int, ParameterDirection.Input, endVerseSortOrder);
            sph.DefineSqlParameter("@TextSearchWord", SqlDbType.NVarChar, 250, ParameterDirection.Input, textSearchWord);
            sph.DefineSqlParameter("@SearchCriteria", SqlDbType.NChar, 20, ParameterDirection.Input, searchCriteria);
            int count = Convert.ToInt32(sph.ExecuteScalar());
            return (count);
        }


        //TODO multi work !
        public static IDataReader iSearch_GetPage_Default_ByRoot(string searchCriteria, int siteId, int quranID, int strtSuraID, int endSuraID,
            int strtVerseSortOrder, int endVerseSortOrder, string textSearchWord, int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            int totalRows = iSearch_GetCount_Default_ByRoot(searchCriteria, siteId, quranID, strtSuraID, endSuraID,
                strtVerseSortOrder, endVerseSortOrder, textSearchWord);

            if (pageSize > 0) totalPages = totalRows / pageSize;

            if (totalRows <= pageSize)
            {
                totalPages = 1;
            }
            else
            {
                int remainder;
                Math.DivRem(totalRows, pageSize, out remainder);
                if (remainder > 0)
                {
                    totalPages += 1;
                }
            }

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "itinfo_iSearch_SelectPage_Default_ByRoot", 10);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            sph.DefineSqlParameter("@StrtSuraID", SqlDbType.Int, ParameterDirection.Input, strtSuraID);
            sph.DefineSqlParameter("@EndSuraID", SqlDbType.Int, ParameterDirection.Input, endSuraID);
            sph.DefineSqlParameter("@strtVerseSortOrder", SqlDbType.Int, ParameterDirection.Input, strtVerseSortOrder);
            sph.DefineSqlParameter("@endVerseSortOrder", SqlDbType.Int, ParameterDirection.Input, endVerseSortOrder);
            sph.DefineSqlParameter("@TextSearchWord", SqlDbType.NVarChar, 250, ParameterDirection.Input, textSearchWord);
            sph.DefineSqlParameter("@SearchCriteria", SqlDbType.NChar, 20, ParameterDirection.Input, searchCriteria);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }


        #endregion
    }
}
