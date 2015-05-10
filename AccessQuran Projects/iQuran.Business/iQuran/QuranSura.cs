// Author:					Ghalib Ghniem ghalib@ItInfoPlus.com
// Created:				    2015-03-22
// Last Modified:			2015-05-09
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
	/// Represents a Quran Sura
	/// PROPERTIES:
	///	SuraID , SiteID , QuranID , Title , Place , VersesCount , SuraOrder   , IsActive , CreatedDate , CreatedBy
	///	Note: createddate, VersesCount will be valued in the SQL in  SP
	/// </summary>
	public class QuranSura
	{

		#region Constructors

		public QuranSura()
		{ }

		public QuranSura(int siteId, int suraId)
		{
			if (suraId > -1)
			{
                GetSura(siteId, suraId);
			}
		}

		#endregion

		#region Private Properties

		private static readonly ILog log = LogManager.GetLogger(typeof(QuranSura));

		private int suraID = -1;
		private int siteID = -1;
		private int quranID = -1;
		// The Name of Sura
		private string title = string.Empty;
		// Theplace per Turath - madeeneh or Mekkah
		private string place = string.Empty;
		private int versesCount = 0;
		// the number of Surah in Quran 1-114
		private int suraOrder = 1;
		// the reverse number of Surah in Quran - starting from the end "Al3alaq" to the Fatihah
		private int soraOrderReverse = -1;
        private int pageNumber = -1;
        private bool isActive = true;

		//Who and when the data entered
		private DateTime createdDate = DateTime.UtcNow;
		private string createdByUserName = string.Empty;
		private int createdByUserID;
		private int totalPages = 1;

		//for recursev calling Sura with all its verses data
		private QuranVerse iQuranVerses = null;

		//private Collection<QuranVerse> quranVerses;

		#endregion

		#region Public Properties

		public int SuraID
		{
			get { return suraID; }
			set { suraID = value; }
		}

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

		public string Title
		{
			get { return title; }
			set { title = value; }
		}

		public string Place
		{
			get { return place; }
			set { place = value; }
		}

		public int SoraOrderReverse
		{
            get { return 115 - suraOrder; }
			set { soraOrderReverse = value; }
		}

		public int VersesCount
		{
			get { return versesCount; }
			set { versesCount = value; }
		}

		public int SuraOrder
		{
			get { return suraOrder; }
			set { suraOrder = value; }
		}

        public int PageNumber
        {
            get { return pageNumber; }
            set { pageNumber = value; }
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

		public int TotalPages
		{
			get { return totalPages; }
		}

		public QuranVerse IQuranVerses
		{
			get { return iQuranVerses; }
			set { iQuranVerses = value; }
		}


		//public Collection<QuranVerse> QuranVerses
		//{
		//	get { return quranVerses; }
		//	set { quranVerses = value; }
		//}
		#endregion

		#region Private Methods

		private void GetSura(int siteId, int suraid)
		{
            using (IDataReader reader = DBiQuran.iSura_GetSura(siteId, suraid))
			{
				if (reader.Read())
				{
					this.suraID = int.Parse(reader["SuraID"].ToString());
					this.siteID = int.Parse(reader["SiteID"].ToString());
					this.quranID = int.Parse(reader["QuranID"].ToString());
					this.title = reader["Title"].ToString();
					this.place = reader["Place"].ToString();
                    this.soraOrderReverse = 115 - int.Parse(reader["SuraOrder"].ToString());
					this.versesCount = int.Parse(reader["VersesCount"].ToString());
					this.suraOrder = int.Parse(reader["SuraOrder"].ToString());
                    this.pageNumber = int.Parse(reader["PageNumber"].ToString());
					string active = reader["IsActive"].ToString();
					this.isActive = (active == "True" || active == "1");
					this.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
					this.createdByUserName = reader["UserName"].ToString();
					this.createdByUserID = int.Parse(reader["CreatedBy"].ToString());

					//this.iquranVerse = new QuranVerse();

					Quran quranVers = new Quran(this.quranID, this.siteID);
					int surasPerPage = 0;
					
					if (quranVers.SurasPerPageUser > 0)
						surasPerPage = quranVers.SurasPerPageUser;
					else
						surasPerPage = quranVers.SurasPerPage;



					if (this.versesCount > surasPerPage)
					{
						this.totalPages = this.versesCount / surasPerPage;
						int remainder = 0;
						Math.DivRem(this.versesCount, surasPerPage, out remainder);
						if (remainder > 0)
						{
							this.totalPages += 1;
						}
					}
					else
					{
						this.totalPages = 1;
					}

				}
				else
				{
					if (log.IsErrorEnabled)
					{
						log.Error("IDataReader didn't read in Quran.GetSura");
					}


				}
			}



		}

		private bool Create()
		{
			int newID = -1;

			newID = DBiQuran.iSura_Create(this.siteID, this.quranID, this.title, this.place, 
				this.suraOrder, this.isActive, this.createdByUserID);

            DBiQuran.iQuran_Increment_SuraCount(this.siteID, this.quranID);

			this.suraID = newID;

			return (newID > -1);

		}

		private bool Update()
		{
			return DBiQuran.iSura_Update(this.siteID, this.quranID, this.suraID, this.title, this.place,
				this.suraOrder, this.isActive, this.createdByUserID);
		}

		private static List<QuranSura> LoadListFromReader(IDataReader reader, bool iSThereSubInstancesInside)
		{
			List<QuranSura> iSList = new List<QuranSura>();
			try
			{
				while (reader.Read())
				{
					QuranSura iSur = new QuranSura();

					iSur.SuraID = int.Parse(reader["SuraID"].ToString());
					iSur.SiteID = int.Parse(reader["SiteID"].ToString());
					iSur.QuranID = int.Parse(reader["QuranID"].ToString());
                    iSur.Title = reader["Title"].ToString().Trim();
                    iSur.Place = reader["Place"].ToString().Trim();
                    iSur.SoraOrderReverse = 115 - int.Parse(reader["SuraOrder"].ToString());
					iSur.VersesCount = int.Parse(reader["VersesCount"].ToString());
					iSur.SuraOrder = int.Parse(reader["SuraOrder"].ToString());
                    iSur.pageNumber = int.Parse(reader["PageNumber"].ToString());
					string active = reader["IsActive"].ToString();
					iSur.IsActive = (active == "True" || active == "1");
					iSur.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                    iSur.CreatedByUserName = reader["UserName"].ToString().Trim();
					iSur.CreatedByUserId = int.Parse(reader["CreatedBy"].ToString());

					//Get Sub Data ?
					if (iSThereSubInstancesInside == true)
					{
                        using (IDataReader reader1 = DBiQuran.iVerse_GetVerses_Dr(iSur.siteID, iSur.suraID))
						{
							while (reader1.Read())
							{
								QuranVerse iQuranVerse = new QuranVerse();




								iSur.iQuranVerses.VerseID = int.Parse(reader1["VerseID"].ToString());
								iSur.iQuranVerses.SuraID = int.Parse(reader1["SuraID"].ToString());
								iSur.iQuranVerses.QuranID = int.Parse(reader1["QuranID"].ToString());
								iSur.iQuranVerses.SiteID = int.Parse(reader1["SiteID"].ToString());

                                iSur.iQuranVerses.VerseOrder = int.Parse(reader1["VerseOrder"].ToString());
								iSur.iQuranVerses.SortOrderInQuran = int.Parse(reader1["SortOrderInQuran"].ToString());

								iSur.iQuranVerses.HalfNo = int.Parse(reader1["HalfNo"].ToString());
								iSur.iQuranVerses.PartNo = int.Parse(reader1["PartNo"].ToString());
								iSur.iQuranVerses.QuraterNo = int.Parse(reader1["QuraterNo"].ToString());

								iSur.iQuranVerses.StartTime = float.Parse(reader1["StartTime"].ToString());
								iSur.iQuranVerses.EndTime = float.Parse(reader1["EndTime"].ToString());
                                iSur.iQuranVerses.EndTimeText = reader1["EndTimeText"].ToString().Trim();

								string active1 = reader1["IsActive"].ToString();
								iSur.iQuranVerses.IsActive = (active1 == "True" || active1 == "1");
								iSur.iQuranVerses.CreatedDate = Convert.ToDateTime(reader1["CreatedDate"]);
                                iSur.iQuranVerses.CreatedByUserName = reader1["UserName"].ToString().Trim();
								iSur.iQuranVerses.CreatedByUserId = int.Parse(reader1["CreatedBy"].ToString());
                                iSur.iQuranVerses.QLanguage = reader1["QLanguage"].ToString().Trim();

								if (iSur.iQuranVerses.QLanguage == "ar")
								{
									using (IDataReader reader2 = DBiQuran.iVerse_GetVerse_Text( iSur.iQuranVerses.SiteID, iSur.iQuranVerses.QuranID, iSur.iQuranVerses.SuraID, iSur.iQuranVerses.VerseID))
									{
										if (reader2.Read())
										{
                                            iSur.iQuranVerses.VerseText = reader2["VerseText"].ToString();
											iSur.iQuranVerses.QuranVerseTxt.VerseTextNM = reader2["VerseTextNM"].ToString();
											iSur.iQuranVerses.QuranVerseTxt.VerseTextNMAlif = reader2["VerseTextNMAlif"].ToString();
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
								else
								{
                                    iSur.iQuranVerses.VerseText = DBiQuran.iVerse_GetVerse_Translation(iSur.iQuranVerses.SiteID, iSur.iQuranVerses.QuranID, iSur.iQuranVerses.SuraID, iSur.iQuranVerses.VerseID);

								}

							}
						}
					}
					iSList.Add(iSur);
				}
			}
			finally
			{
				reader.Close();
			}

			return iSList;

		}

		#endregion

		#region Public Methods

		public bool Save()
		{
			if (this.suraID > -1)
			{
				return Update();
			}
			else
			{
				return Create();
			}
		}

		//public DataTable GetVerseIdList()
		//{
		//	DataTable dataTable = new DataTable();
		//	dataTable.Columns.Add("VesreID", typeof(int));

		//	using (IDataReader reader = DBiQuran.GetVerses(this.suraID, 0))
		//	{
		//		while (reader.Read())
		//		{
		//			DataRow row = dataTable.NewRow();
		//			row["VesreID"] = reader["VesreID"];
		//			dataTable.Rows.Add(row);

		//		}
		//	}

		//	return dataTable;

		//}

		#endregion

		#region Static Methods

        public static bool Exists(int siteId, int quranID, int suraID, string title)
        {
            return DBiQuran.iSura_Exists(siteId, quranID, suraID, title);
        }

        public static bool OrderExists(int siteId, int quranID, int suraID, int suraOrder)
        {
            return DBiQuran.iSura_OrderExists(siteId, quranID, suraID, suraOrder);
        }

		public static bool DeleteSura(int siteId, int quranID, int suraid)
		{
            DBiQuran.iQuran_Decrement_SuraCount(siteId, quranID);
            return DBiQuran.iSura_Delete(siteId, quranID, suraid);
		}

		public static int GetSuraVersesCount(int siteId, int suraid)
		{
            return DBiQuran.iSura_GetSura_VersesCount(siteId, suraid);
		}

		public static IDataReader GetSuras(int siteId,int quranid)
		{
			return DBiQuran.iSura_GetSuras_Dr(siteId, quranid);
		}

		#endregion

		#region "SEARCH"

		/// <summary>
		/// Search All - iSura version instances as IList with paging.
		/// </summary>
		/// <param name="WithVerses"> used to retrive the SuraVerse - Verse - instances of the Sura </param>
		/// <param name="siteId"></param>
		/// <param name="quranID"></param>
		/// <param name="isAdmin"> used to know to retrive data for front end or for back end pages</param>
		/// <param name="pageNumber"></param>
		/// <param name="pageSize"></param>
		/// <param name="totalPages"></param>
		/// <returns></returns>
		public static List<QuranSura> GetPage_iSura_All(bool WithVerses, int siteId, int quranID, bool isAdmin, int pageNumber, int pageSize, out int totalPages)
		{
			totalPages = 1;
            IDataReader reader = DBiQuran.iSura_GetPage_All(siteId, quranID, isAdmin, pageNumber, pageSize, out totalPages);
			return LoadListFromReader(reader, WithVerses);
		}

		/// <summary>
		/// Search By Title - iSura version instances as IList with paging.
		/// </summary>
		/// <param name="WithVerses"> used to retrive the SuraVerse - Verse - instances of the Sura </param>
		/// <param name="siteId"></param>
		/// <param name="quranID"></param>
		/// <param name="isAdmin">used to know to retrive data for front end or for back end pages</param>
		/// <param name="Title">Search Criteria</param>
		/// <param name="pageNumber"></param>
		/// <param name="pageSize"></param>
		/// <param name="totalPages"></param>
		/// <returns></returns>
		public static List<QuranSura> GetPage_iSura_ByTitle(bool WithVerses, int siteId, int quranID, bool isAdmin, string Title, int pageNumber, int pageSize, out int totalPages)
		{
			totalPages = 1;
            IDataReader reader = DBiQuran.iSura_GetPage_ByTitle(siteId, quranID, isAdmin, Title, pageNumber, pageSize, out totalPages);
			return LoadListFromReader(reader, WithVerses);
		}

		/// <summary>
		/// Search By Criteria - iSura version instances as IList with paging
		/// </summary>
		/// <param name="WithVerses"> used to retrive the SuraVerse - Verse - instances of the Sura </param>
		/// <param name="siteId"></param>
		/// <param name="quranID"></param>
		/// <param name="isActive"></param>
		/// <param name="pageNumber"></param>
		/// <param name="pageSize"></param>
		/// <param name="totalPages"></param>
		/// <returns></returns>
		public static List<QuranSura> GetPage_iSura_ByActive(bool WithVerses, int siteId, int quranID, bool isActive, int pageNumber, int pageSize, out int totalPages)
		{
			totalPages = 1;
            IDataReader reader = DBiQuran.iSura_GetPage_ByActive(siteId, quranID, isActive, pageNumber, pageSize, out totalPages);
			return LoadListFromReader(reader, WithVerses);
		}

		#endregion

        #region [FRONT END]

        public static List<QuranSura> GetiSura_All(int siteId, int quranID)
        {
            IDataReader reader = DBiQuran.iSura_FrontEnd_GetSurasIndex(siteId, quranID);
            return LoadListFromReader(reader, false);
        }

        public static int GetSuraIdFromPageNumber(int siteId, int quranId, int pageNumber, bool isTranslation)
        {
            return DBiQuran.iVerse_GetSuraIdFromPageNumber(siteId, quranId, pageNumber,  isTranslation);
        }
        #endregion
    }
}
