// Author:					Ghalib Ghniem ghalib@ItInfoPlus.com
// Created:				    2015-05-07
// Last Modified:			2015-05-07
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
	/// Represents a Quran Page 
	/// PROPERTIES:
    /// SiteID , QuranID , PageNumber , PartNo, Titles
	/// 
	/// Explain :
    /// Titles: Some pages of Quran contains more than one Sura, max of 3 in lat pages of Quran only
	/// </summary>
	public class QuranPage
	{
		#region Constructors

        public QuranPage()
		{ }

        public QuranPage(int siteId, int quranId, int pagenumber, bool isTranslation)
        {
            if (pagenumber > 0)
            {
                GetQuranPage(siteId, quranId, pagenumber, isTranslation);
            }
        }

		#endregion

		#region Private Properties

        private static readonly ILog log = LogManager.GetLogger(typeof(QuranVerseTranslation));

        private int partNo = -1;
        private string titles = string.Empty;

		#endregion
        
		#region Public Properties


        public int PartNo
        {
            get { return partNo; }
            set { partNo = value; }
        }

        public string Titles
        {
            get { return titles; }
            set { titles = value; }
        }

		#endregion

		#region Private Methods

        private void GetQuranPage(int siteId, int quranId, int pagenumber, bool isTranslation)
        {
            using (IDataReader reader = DBiQuran.iQuran_FrontEnd_GetPage_Header(siteId, quranId, pagenumber, isTranslation))
            {
                if (reader.Read())
                {
                    this.partNo = int.Parse(reader["PartNo"].ToString());
                    this.titles = reader["Titles"].ToString();
                    
                }
                else
                {
                    if (log.IsErrorEnabled)
                    {
                        log.Error("IDataReader didn't read in QuranPage.GetQuranPage");
                    }


                }
            }



        }

		

		#endregion


	}
}
