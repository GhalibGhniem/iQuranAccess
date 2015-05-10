// Author:					Ghalib Ghniem ghalib@ItInfoPlus.com
// Created:				    2015-04-02
// Last Modified:			2015-04-02
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
	public class iQLibrary
	{

        public static IDataReader GetRoots(int siteId, int quranid)
        {
            return DBiLibrary.GetRootWords(siteId, quranid);
        }


	}
}
