// Author:					Ghalib Ghniem ghalib@ItInfoPlus.com
// Created:				    2015-03-02
// Last Modified:			2015-04-02
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



    public static class DBiLibrary
    {

        #region ROOT WORDS

        public static IDataReader GetRootWords(int siteID, int quranID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "itinfo_iLib_Select_Root", 2);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@QuranID", SqlDbType.Int, ParameterDirection.Input, quranID);
            return sph.ExecuteReader();
        }
        

        #endregion
    }
}
