// Author:					Ghalib Ghniem ghalib@ItInfoPlus.com
// Created:				    2015-03-22
// Last Modified:			2015-04-14
// 
 
using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using log4net;
using mojoPortal.Data;
using mojoPortal.Business;
using iQuran.Data;

namespace iQuran.Business
{
	/// <summary>
	/// Represents a Quran Version
	/// Description: Quran Version - means a translation or the arabic original Othmani quran
	/// PROPERTIES:
    /// QuranID , SiteID , Title , Description , SuraCount , IsDefault , QLanguage , IsActive , TRanslatorDetUrl , TranslationSrc , CreatedDate , CreatedBy
	/// Note: createddate, SuraCount  will be valued in the SQL in  SP
	/// </summary>
	
	public class Quran
	{


		#region Constructors

		public Quran()
		{ }

		public Quran(int siteId, int quranId)
		{
			if (quranId > -1)
			{
                GetiQuran(siteId, quranId);
			}
		}

		#endregion

		#region Private Properties

		private static readonly ILog log = LogManager.GetLogger(typeof(Quran));

		private int quranID = -1;
		private int siteID = -1;
		private string title = string.Empty;
		private string description = string.Empty;
		// For statistics : how much Suras was entered till now ?
		private int suraCount = 0;
		
		private bool isDefault = false;
		// the Language of the Quran Version? the default and the unique one is the original Othmani
		// others are only trnslations
		private string qLanguage = string.Empty;
		private bool isActive = true;
        private string tRanslatorDetUrl = string.Empty;
        private string translationSrc = string.Empty;

		private DateTime createdDate = DateTime.UtcNow;
		private string createdByUserName = string.Empty;
		private int createdByUserID;

		//for recursev calling quran with all its suras data
		private QuranSura iQuranSura = null;

		// For future using of user settings
		private int surasPerPage = int.Parse(ConfigurationManager.AppSettings["iQuranSurasPerPage"].ToString());
		private int versesPerPage = int.Parse(ConfigurationManager.AppSettings["iQuranVersesPerPage"].ToString());
		private int surasPerPageUser = int.Parse(ConfigurationManager.AppSettings["iQuranSurasPerPageUser"].ToString());
		private int versesPerPageUser = int.Parse(ConfigurationManager.AppSettings["iQuranVersesPerPageUser"].ToString());
		
		#endregion

		#region Public Properties

		public int QuranID
		{
			get { return quranID; }
			set { quranID = value; }
		}

		public int SiteId
		{
			get { return siteID; }
			set { siteID = value; }
		}

		public string Title
		{
			get { return title; }
			set { title = value; }
		}

		public string Description
		{
			get { return description; }
			set { description = value; }
		}

        public int SuraCount
		{
			get { return suraCount; }
			set { suraCount = value; }
		}
		
		public bool IsDefault
		{
			get { return isDefault; }
			set { isDefault = value; }
		}

		public string QLanguage
		{
			get { return qLanguage; }
			set { qLanguage = value; }
		}

		public bool IsActive
		{
			get { return isActive; }
			set { isActive = value; }
		}

        
        public string TRanslatorDetUrl
        {
            get { return tRanslatorDetUrl; }
            set { tRanslatorDetUrl = value; }
        }

        public string TranslationSrc
        {
            get { return translationSrc; }
            set { translationSrc = value; }
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

		public QuranSura IQuransura
		{
			get { return iQuranSura; }
			set { iQuranSura = value; }
		}


		public int SurasPerPage
		{
			get { return surasPerPage; }
			set { surasPerPage = value; }
		}

		public int VersesPerPage
		{
			get { return versesPerPage; }
			set { versesPerPage = value; }
		}
		public int SurasPerPageUser
		{
			get { return surasPerPageUser; }
			set { surasPerPageUser = value; }
		}

		public int VersesPerPageUser
		{
			get { return versesPerPageUser; }
			set { versesPerPageUser = value; }
		}



		#endregion

		#region Private Methods

        private void GetiQuran(int siteId, int quranId)
		{
            using (IDataReader reader = DBiQuran.iQuran_GetiQuran(siteId, quranId))
			{
				if (reader.Read())
				{

					this.quranID = int.Parse(reader["QuranID"].ToString());
					this.siteID = int.Parse(reader["SiteID"].ToString());
                    this.title = reader["Title"].ToString().Trim();
					this.description = reader["Description"].ToString();
                    this.tRanslatorDetUrl = reader["TRanslatorDetUrl"].ToString();
                    this.translationSrc = reader["TranslationSrc"].ToString();

					this.suraCount = int.Parse(reader["SuraCount"].ToString());

					string isdefault = reader["IsDefault"].ToString();
					this.isDefault = (isdefault == "True" || isdefault == "1");
                    this.qLanguage = reader["QLanguage"].ToString().Trim();
					string active = reader["IsActive"].ToString();
					this.isActive = (active == "True" || active == "1");
					this.createdDate = Convert.ToDateTime(reader["CreatedDate"]);

                    this.createdByUserName = reader["UserName"].ToString().Trim();
					this.createdByUserID = int.Parse(reader["CreatedBy"].ToString());

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

		private bool Create()
		{
			int newID = -1;


			newID = DBiQuran.iQuran_Create(this.siteID, this.title, this.description, this.isDefault, this.qLanguage,
                this.tRanslatorDetUrl, this.translationSrc, this.isActive, this.createdByUserID);


			this.quranID = newID;

			return (newID > -1);

		}

		private bool Update()
		{
            return DBiQuran.iQuran_Update(this.siteID, this.quranID, this.title, this.description, this.isDefault, this.qLanguage,
                this.isActive, this.tRanslatorDetUrl, this.translationSrc, this.createdByUserID);
		}

		private static List<Quran> LoadListFromReader(IDataReader reader , bool iSThereSubInstancesInside)
		{
			List<Quran> iQList = new List<Quran>();
			try
			{
				while (reader.Read())
				{
					Quran iQur = new Quran();

					iQur.quranID = int.Parse(reader["QuranID"].ToString());
					iQur.siteID = int.Parse(reader["SiteID"].ToString());
                    iQur.title = reader["Title"].ToString().Trim();
					iQur.description = reader["Description"].ToString();
                    iQur.tRanslatorDetUrl = reader["TRanslatorDetUrl"].ToString();
                    iQur.translationSrc = reader["TranslationSrc"].ToString();

					iQur.suraCount = int.Parse(reader["SuraCount"].ToString());

					string isdefault = reader["IsDefault"].ToString();
                    iQur.isDefault = (isdefault == "True" || isdefault == "1");
                    iQur.qLanguage = reader["QLanguage"].ToString().Trim();
					string active = reader["IsActive"].ToString();
					iQur.isActive = (active == "True" || active == "1");
					iQur.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                    iQur.createdByUserName = reader["UserName"].ToString().Trim();
					iQur.createdByUserID = int.Parse(reader["CreatedBy"].ToString());

					//Get Sub Data ?
					if (iSThereSubInstancesInside == true)
					{
                        using (IDataReader reader1 = DBiQuran.iSura_GetSuras_Dr(iQur.siteID, iQur.quranID))
						{
							while (reader1.Read())
							{
								QuranSura iQuranSura = new QuranSura();
								
								iQur.iQuranSura.SuraID = int.Parse(reader1["SuraID"].ToString());
								iQur.iQuranSura.SiteID = int.Parse(reader1["SiteID"].ToString());
								iQur.iQuranSura.QuranID = int.Parse(reader1["QuranID"].ToString());
                                iQur.iQuranSura.Title = reader1["Title"].ToString().Trim();
                                iQur.iQuranSura.Place = reader1["Place"].ToString().Trim();
								iQur.iQuranSura.SoraOrderReverse = int.Parse(reader1["SoraOrderReverse"].ToString());
								iQur.iQuranSura.VersesCount = int.Parse(reader1["VersesCount"].ToString());
								iQur.iQuranSura.SuraOrder = int.Parse(reader1["SuraOrder"].ToString());
								string active1 = reader1["IsActive"].ToString();
								iQur.iQuranSura.IsActive = (active1 == "True" || active1 == "1");
								iQur.iQuranSura.CreatedDate = Convert.ToDateTime(reader1["CreatedDate"]);
                                iQur.iQuranSura.CreatedByUserName = reader1["UserName"].ToString().Trim();
								iQur.iQuranSura.CreatedByUserId = int.Parse(reader1["CreatedBy"].ToString());
								
							}
						}
					}
					iQList.Add(iQur);
				}
			}
			finally
			{
				reader.Close();
			}

			return iQList;

		}

		#endregion

		#region Public Methods

		public bool Save()
		{
			if (this.quranID > -1)
			{
				return Update();
			}
			else
			{
				return Create();
			}
		}

		#endregion

		#region Static Methods

        public static bool CheckDefaultExists(int siteId)
        {
            return DBiQuran.iQuran_DefaultExists(siteId);
        }

		public static bool Exists(int siteId, int quranID, string qLanguage, string title)
		{
            return DBiQuran.iQuran_Exists(siteId, quranID, qLanguage, title);
		}

		public static bool Delete(int siteId, int quranId)
		{
            return DBiQuran.iQuran_Delete(siteId, quranId);
		}

		public static bool DeleteBySite(int siteId)
		{
			return DBiQuran.iQuran_Delete_BySite(siteId);
		}

		public static IDataReader GetiQurans(int siteId)
		{
			return DBiQuran.iQuran_GetiQurans_Dr(siteId);
		}

		public static bool DecrementSuraCount(int siteId, int quranId)
		{
            return DBiQuran.iQuran_Decrement_SuraCount(siteId, quranId);
		}

        public static bool IncrementSuraCount(int siteId, int quranId)
		{
            return DBiQuran.iQuran_Increment_SuraCount(siteId, quranId);
		}


		#endregion
		
		#region "SEARCH"
		
		/// <summary>
		/// Search All - iQuran version instances as IList with paging.
		/// </summary>
		/// <param name="WithSuras"> used to retrive the QuranSuras - Sura - instances of the Quran Revesion </param>
		/// <param name="siteId"></param>
		/// <param name="isAdmin"> used to know to retrive data for front end or for back end pages</param>
		/// <param name="pageNumber"></param>
		/// <param name="pageSize"></param>
		/// <param name="totalPages"></param>
		/// <returns></returns>
		public static List<Quran> GetPage_iQuran_All(bool WithSuras, int siteId, bool isAdmin, int pageNumber, int pageSize, out int totalPages)
		{
			totalPages = 1;
            IDataReader reader = DBiQuran.iQuran_GetPage_All(siteId, isAdmin, pageNumber, pageSize, out totalPages);
			return LoadListFromReader(reader, WithSuras);
		}

		/// <summary>
		/// Search By Title - iQuran version instances as IList with paging.
		/// </summary>
		/// <param name="WithSuras"> used to retrive the QuranSuras - Sura - instances of the Quran Revesion </param>
		/// <param name="siteId"></param>
		/// <param name="isAdmin">used to know to retrive data for front end or for back end pages</param>
		/// <param name="Title">Search Criteria</param>
		/// <param name="pageNumber"></param>
		/// <param name="pageSize"></param>
		/// <param name="totalPages"></param>
		/// <returns></returns>
		public static List<Quran> GetPage_iQuran_ByTitle(bool WithSuras, int siteId, bool isAdmin, string Title, int pageNumber, int pageSize, out int totalPages)
		{
			totalPages = 1;
            IDataReader reader = DBiQuran.iQuran_GetPage_ByTitle(siteId, isAdmin, Title, pageNumber, pageSize, out totalPages);
			return LoadListFromReader(reader, WithSuras);
		}

		/// <summary>
		/// Search By Criteria - iQuran version instances as IList with paging
		/// </summary>
		/// <param name="WithSuras"> used to retrive the QuranSuras - Sura - instances of the Quran Revesion </param>
		/// <param name="siteId"></param>
		/// <param name="isActive"></param>
		/// <param name="pageNumber"></param>
		/// <param name="pageSize"></param>
		/// <param name="totalPages"></param>
		/// <returns></returns>
        public static List<Quran> GetPage_iQuran_ByActive(bool WithSuras, int siteId, bool isActive, int pageNumber, int pageSize, out int totalPages)
		{
			totalPages = 1;
            IDataReader reader = DBiQuran.iQuran_GetPage_ByActive(siteId, isActive, pageNumber, pageSize, out totalPages);
			return LoadListFromReader(reader, WithSuras);
		}

		#endregion

	}
}
