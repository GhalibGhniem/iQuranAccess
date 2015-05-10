// Author:					Ghalib Ghniem ghalib@ItInfoPlus.com
// Created:				    2015-04-18
// Last Modified:			2015-04-21
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
    /// Represents a Search result Information 
    /// PROPERTIES: verseQty , wordQty
    /// </summary>
	public class SearchInfo
	{
		#region Constructors

        public SearchInfo()
		{ }

        public SearchInfo(int siteId, int quranID, int strtSuraOrder,
           int endSuraOrder, int strtVerseOrder, int endVerseOrder, string TextWordRoot)
        {
                GetCounts_ByRoot_Table(siteId, quranID, strtSuraOrder, endSuraOrder,
                strtVerseOrder, endVerseOrder, TextWordRoot);
        }

        public SearchInfo(string searchedTextType, string searchCriteria, int siteId, int quranID, int strtSuraOrder,
           int endSuraOrder, int strtVerseOrder, int endVerseOrder, string textSearchWord)
        {
            if (searchedTextType == "i")
                GetCounts_ByOnlyWord(searchCriteria, siteId, quranID, strtSuraOrder, endSuraOrder,
                strtVerseOrder, endVerseOrder, textSearchWord);
            else if (searchedTextType == "p")
                GetCounts_ByWordSentence(searchCriteria, siteId, quranID, strtSuraOrder, endSuraOrder,
            strtVerseOrder, endVerseOrder, textSearchWord);
            else //v
                GetCounts_ByWordVerse(searchCriteria, siteId, quranID, strtSuraOrder, endSuraOrder,
            strtVerseOrder, endVerseOrder, textSearchWord);
        }

       

        		#endregion

		#region Private Properties

        private static readonly ILog log = LogManager.GetLogger(typeof(SearchInfo));


        private string verseQty = string.Empty;
        private string wordQty = string.Empty;
        private string searchedWords = string.Empty;
		#endregion

		#region Public Properties


        public string VerseQty
		{
            get { return verseQty; }
            set { verseQty = value; }
		}

        public string WordQty
        {
            get { return wordQty; }
            set { wordQty = value; }
        }

        public string SearchedWords
        {
            get { return searchedWords; }
            set { searchedWords = value; }
        }

		#endregion


        #region Private Methods

        private void GetCounts_ByRoot_Table(int siteId, int quranID, int strtSuraOrder,
           int endSuraOrder, int strtVerseOrder, int endVerseOrder, string TextWordRoot)
        {
            using (IDataReader reader = DBiQuran.iSearch_GetCounts_Default_ByRoot_Table_Dr(siteId, quranID, strtSuraOrder, endSuraOrder,
                strtVerseOrder, endVerseOrder, TextWordRoot))
            {
                if (reader.Read())
                {
                    this.verseQty = reader["VerseQty"].ToString();
                    this.wordQty = reader["WordQty"].ToString();
                    this.searchedWords = reader["SearchedWords"].ToString().Replace("--", "-");
                    if (this.searchedWords.Contains("-"))
                        this.searchedWords = searchedWords.Substring(1, searchedWords.Length - 1);
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

          private void GetCounts_ByOnlyWord(string searchCriteria, int siteId, int quranID, int strtSuraOrder,
           int endSuraOrder, int strtVerseOrder, int endVerseOrder, string TextWordRoot)
        {
            using (IDataReader reader = DBiQuran.iSearch_GetCounts_Default_ByOnlyWord_Dr(searchCriteria, siteId, quranID, strtSuraOrder, endSuraOrder,
                strtVerseOrder, endVerseOrder, TextWordRoot))
            {
                if (reader.Read())
                {
                    this.verseQty = reader["VerseQty"].ToString();
                    this.wordQty = reader["WordQty"].ToString();
                    this.searchedWords = reader["SearchedWords"].ToString().Replace("--", "-");
                    if (this.searchedWords.Contains("-"))
                        this.searchedWords = searchedWords.Substring(1, searchedWords.Length - 1);
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


          private void GetCounts_ByWordVerse(string searchCriteria, int siteId, int quranID, int strtSuraOrder,
           int endSuraOrder, int strtVerseOrder, int endVerseOrder, string TextWordRoot)
        {
            using (IDataReader reader = DBiQuran.iSearch_GetCounts_Default_ByWordVerse_Dr(searchCriteria, siteId, quranID, strtSuraOrder, endSuraOrder,
                strtVerseOrder, endVerseOrder, TextWordRoot))
            {
                if (reader.Read())
                {
                    this.verseQty = reader["VerseQty"].ToString();
                    this.wordQty = reader["WordQty"].ToString();
                    this.searchedWords = reader["SearchedWords"].ToString().Replace("--", "-");
                    if (this.searchedWords.Contains("-"))
                        this.searchedWords = searchedWords.Substring(1, searchedWords.Length - 1);
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


          private void GetCounts_ByWordSentence(string searchCriteria, int siteId, int quranID, int strtSuraOrder,
            int endSuraOrder, int strtVerseOrder, int endVerseOrder, string TextWordRoot)
          {
              using (IDataReader reader = DBiQuran.iSearch_GetCounts_Default_ByWordSentence_Dr(searchCriteria, siteId, quranID, strtSuraOrder, endSuraOrder,
                  strtVerseOrder, endVerseOrder, TextWordRoot))
              {
                  if (reader.Read())
                  {
                      this.verseQty = reader["VerseQty"].ToString();
                      this.wordQty = reader["WordQty"].ToString();
                      this.searchedWords = reader["SearchedWords"].ToString().Replace("--", "-");
                      if (this.searchedWords.Contains("-"))
                          this.searchedWords = searchedWords.Substring(1, searchedWords.Length - 1);
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
