// Author:					Ghalib Ghniem ghalib@ItInfoPlus.com
// Created:				    2015-03-25
// Last Modified:			2015-04-14
// 

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using mojoPortal.Web.UI;

namespace iQuran.Web.UI.Controls
{

	public partial class LanguageSetting : UserControl, ISettingControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string GetText()
        {
			if (ddLanguageSetting.SelectedItem != null)
            {
				return ddLanguageSetting.SelectedItem.Text;
            }

            return string.Empty;
        }

        #region ISettingControl

        public string GetValue()
        {
			return ddLanguageSetting.SelectedValue;
        }


        public void SetValue(string val)
        {
			ListItem item = ddLanguageSetting.Items.FindByValue(val);
            if (item != null)
            {
				ddLanguageSetting.ClearSelection();
                item.Selected = true;
            }
        }

        #endregion


    }
}