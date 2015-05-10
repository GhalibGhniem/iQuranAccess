// Author:					Ghalib Ghniem ghalib@ItInfoPlus.com
// Created:				    2015-03-31
// Last Modified:			2015-05-07
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
    /// Represents a Quran Front End Search
    /// </summary>
    public class iQuranSearch
    {
        #region Constructors

        public iQuranSearch()
        { }

        #endregion

        #region Private Properties

        private static readonly ILog log = LogManager.GetLogger(typeof(iQuranSearch));

        #region "common"

        private int siteID = -1;
        private int quranID = -1;
        private int suraID = -1;
        private int verseID = -1;

        private int verseOrder = 1;
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

        // used for MP3 quran suras to lesten
        private float startTime = 0;
        private float endTime = 0;
        private string endTimeText = string.Empty;
        private string place = string.Empty;

        //Administration fields:
        private bool isActive = true;
        private int verseNo = -1;
        // Verses Text :
        
        private QuranVerseText quranVerseTxt = new QuranVerseText();

        // this will be used to pass both arabic or translation VerseText
        private string verseText = string.Empty;

        #endregion

        #region "Quran Related"

        private string qTitle = string.Empty;
        private int qSuraCount = 0;
        private string qDescription = string.Empty;
        private bool qIsDefault = false;
        private string qLanguage = string.Empty;

        #endregion

        #region "Sura Related"

        private string sTitle = string.Empty;
        private string sPlace = string.Empty;
        private int suraOrderReverse = -1;
        private int sVersesCount = 0;
        private int suraOrder = 1;

        #endregion

        #endregion


        #region Public Properties

        #region "common"

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

        public string Place
        {
            get { return place; }
            set { place = value; }
        }

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        public int VerseNo
        {
            get { return verseNo; }
            set { verseNo = value; }
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

        #region "Quran Related"
        
        public string QTitle
        {
            get { return qTitle; }
            set { qTitle = value; }
        }

        public int QSuraCount
        {
            get { return qSuraCount; }
            set { qSuraCount = value; }
        }

        public string QDescription
        {
            get { return qDescription; }
            set { qDescription = value; }
        }

        public bool QIsDefault
        {
            get { return qIsDefault; }
            set { qIsDefault = value; }
        }

        public string QLanguage
        {
            get { return qLanguage; }
            set { qLanguage = value; }
        }

        #endregion

        #region "Sura Related"

        public string STitle
        {
            get { return sTitle; }
            set { sTitle = value; }
        }

        public string SPlace
        {
            get { return sPlace; }
            set { sPlace = value; }
        }

        public int SuraOrderReverse
        {
            get { return suraOrderReverse; }
            set { suraOrderReverse = value; }
        }

        public int SVersesCount
        {
            get { return sVersesCount; }
            set { sVersesCount = value; }
        }

        public int SuraOrder
        {
            get { return suraOrder; }
            set { suraOrder = value; }
        }

        #endregion
        


        #endregion

        #region Private Methods

        private static List<iQuranSearch> LoadListFromReader(IDataReader reader, int selectedVerseID,  bool isTranslation)
        {
            List<iQuranSearch> iSrchList = new List<iQuranSearch>();
            try
            {
                while (reader.Read())
                {
                    iQuranSearch iSrch = new iQuranSearch();

                    iSrch.verseID = int.Parse(reader["VerseID"].ToString());
                    iSrch.suraID = int.Parse(reader["SuraID"].ToString());
                    iSrch.quranID = int.Parse(reader["QuranID"].ToString());
                    iSrch.siteID = int.Parse(reader["SiteID"].ToString());

                    iSrch.verseOrder = int.Parse(reader["VerseOrder"].ToString());
                    iSrch.sortOrderInQuran = int.Parse(reader["SortOrderInQuran"].ToString());

                    iSrch.halfNo = int.Parse(reader["HalfNo"].ToString());
                    iSrch.partNo = int.Parse(reader["PartNo"].ToString());
                    iSrch.hizbNo = int.Parse(reader["HizbNo"].ToString());
                    iSrch.quraterNo = int.Parse(reader["QuraterNo"].ToString());

                    if (isTranslation == false)
                    {
                        iSrch.startTime = float.Parse(reader["StartTime"].ToString());
                        iSrch.endTime = float.Parse(reader["EndTime"].ToString());
                        iSrch.endTimeText = reader["EndTimeText"].ToString();
                        iSrch.endTimeText = reader["Place"].ToString();
                    }
                    //string active = reader["IsActive"].ToString();
                    //iSrch.isActive = (active == "True" || active == "1");

                    // Quran related
                    iSrch.qSuraCount = int.Parse(reader["QuranSuraCount"].ToString());
                    iSrch.qTitle = reader["QuranTitle"].ToString();
                    iSrch.qDescription = reader["QuranDescription"].ToString();
                    iSrch.qLanguage = reader["QLanguage"].ToString();
                    //string qssdefault = reader["QIsDefault"].ToString();
                    //iSrch.qIsDefault = (qssdefault == "True" || qssdefault == "1");

                    if (selectedVerseID == 0)
                    iSrch.verseNo = int.Parse(reader["VerseNo"].ToString());

                    // Sura related
                    iSrch.sTitle = reader["SuraTitle"].ToString();
                    iSrch.sPlace = reader["SuraPlace"].ToString();
                    iSrch.suraOrder = int.Parse(reader["SuraOrder"].ToString());

                    if (iSrch.suraOrder != 57)
                        iSrch.suraOrderReverse = 115 - iSrch.suraOrder;
                    
                    iSrch.sVersesCount = int.Parse(reader["SuraVersesCount"].ToString());

                    if ((selectedVerseID > 0) && (iSrch.VerseID == selectedVerseID))
                    {
                        iSrch.verseText = "<a name = 'searched'><span class='searchedVerse'>" + reader["VerseText"].ToString() + "</span></a>";
                    }
                    else
                    {
                        iSrch.verseText = reader["VerseText"].ToString();
                    }

                    if (isTranslation == false)
                    {
                        iSrch.quranVerseTxt.VerseText = reader["VerseText"].ToString();
                        iSrch.quranVerseTxt.VerseTextNM = reader["VerseTextNM"].ToString();
                        iSrch.quranVerseTxt.VerseTextNMAlif = reader["VerseTextNMAlif"].ToString();
                        
                    }

                    iSrchList.Add(iSrch);
                }
            }
            finally
            {
                reader.Close();
                
            }

            return iSrchList;

        }

        #endregion

        #region "FRONT PAGES"

        public static List<iQuranSearch> GetFrontEndPage_iSura(int siteId, int quranID, int suraID, int selectedVerseID, int pageNumber, bool isTranslation)
        {
            IDataReader reader = DBiQuran.iSura_GetFrontPage_Sura(siteId, quranID, suraID, pageNumber, isTranslation);
            return LoadListFromReader(reader, selectedVerseID, isTranslation);
        }

        /// <summary>
        /// View Page of Quran for reading in front end
        /// </summary>
        public static List<iQuranSearch> GetFrontEnd_QuranPage(int siteId, int quranID, int pageNumber, bool isTranslation)
        {
            IDataReader reader = DBiQuran.iQuran_FrontEnd_GetPage(siteId, quranID, pageNumber, isTranslation);
            return LoadListFromReader(reader, 0,isTranslation);
        }

        /// <summary>
        /// View Page of Quran for reading in front end with selected searched verse
        /// </summary>
        public static List<iQuranSearch> GetFrontEnd_QuranPage_WithSearchVerse(int siteId, int quranID, int selectedVerseID, int pageNumber, bool isTranslation)
        {
            IDataReader reader = DBiQuran.iQuran_FrontEnd_GetPage(siteId, quranID, pageNumber, isTranslation);
            return LoadListFromReader(reader, selectedVerseID, isTranslation);
        }
        #endregion

        #region "SIMPLE SEARCH"



        /// <summary>
        /// Search Arabic Quran for Word root From AccessQuran Root recordset
        /// </summary>
        public static List<iQuranSearch> iSearch_GetPage_Default_ByRoot_Table(int siteId, int quranID, int strtSuraID, int endSuraID, int strtVerseOrder, int endVerseOrder,
           string TextWordRoot, int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBiQuran.iSearch_GetPage_Default_ByRoot_Table(siteId, quranID, strtSuraID, endSuraID, strtVerseOrder, endVerseOrder,
                             TextWordRoot, pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader,0, false);
        }
      
        /// <summary>
        /// Search Arabic Quran for Exact "one" word 
        /// </summary>
        public static List<iQuranSearch> iSearch_GetPage_Default_ByOnlyWord(string searchCriteria, int siteId, int quranID, int strtSuraID, int endSuraID, int strtVerseOrder, int endVerseOrder,
           string TextSearchWord, int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBiQuran.iSearch_GetPage_Default_ByOnlyWord(searchCriteria, siteId, quranID, strtSuraID, endSuraID, strtVerseOrder, endVerseOrder,
                             TextSearchWord, pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader,0, false);
        }

        /// <summary>
        /// Search Arabic Quran for word as a part from word
        /// </summary>
        public static List<iQuranSearch> iSearch_GetPage_Default_ByWordSentence(string searchCriteria, int siteId, int quranID, int strtSuraID, int endSuraID, int strtVerseOrder, int endVerseOrder,
          string TextSearchWord, int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBiQuran.iSearch_GetPage_Default_ByWordSentence(searchCriteria, siteId, quranID, strtSuraID, endSuraID, strtVerseOrder, endVerseOrder,
                             TextSearchWord, pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader,0, false);
        }

        /// <summary>
        /// Search Arabic Quran for word as part from Verse 
        /// </summary>
        public static List<iQuranSearch> iSearch_GetPage_Default_ByWordVerse(string searchCriteria, int siteId, int quranID, int strtSuraID, int endSuraID, int strtVerseOrder, int endVerseOrder,
          string TextSearchWord, int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBiQuran.iSearch_GetPage_Default_ByWordVerse(searchCriteria, siteId, quranID, strtSuraID, endSuraID, strtVerseOrder, endVerseOrder,
                             TextSearchWord, pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader,0, false);
        }

        /// <summary>
        /// Search Translation for word or sentence  or part from 
        /// </summary>
        public static List<iQuranSearch> iSearch_GetPage_Translation_ByWordSentence(int siteId, int quranID, int strtSuraID, int endSuraID, int strtVerseOrder, int endVerseOrder,
           string TextSearchWord, int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBiQuran.iSearch_GetPage_Translation_ByWordSentence(siteId, quranID, strtSuraID, endSuraID, strtVerseOrder, endVerseOrder,
                            TextSearchWord, pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader,0, true);
        }

        #endregion

        #region "ADVANCED SEARCH"

        /// <summary>
        /// Search Arabic Quran for Word root 
        /// </summary>
        public static List<iQuranSearch> iSearch_GetPage_Default_ByRoot(string searchCriteria, int siteId, int quranID, int strtSuraID, int endSuraID, int strtVerseOrder, int endVerseOrder,
           string TextSearchWord, int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBiQuran.iSearch_GetPage_Default_ByRoot(searchCriteria, siteId, quranID, strtSuraID, endSuraID, strtVerseOrder, endVerseOrder,
                             TextSearchWord, pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader,0, false);
        }


        #endregion
    }
}
