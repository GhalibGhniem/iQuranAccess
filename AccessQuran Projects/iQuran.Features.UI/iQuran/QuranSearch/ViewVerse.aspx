<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="ViewVerse.aspx.cs" Inherits="iQuran.Web.UI.Pages.ViewVerse" %>
<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>الباحث القرءاني المتقدم</title>
    <meta name="keywords" content="القرءان، القرآن، الباحث القرآني، المنقب القرآني" />
    <meta name="author" content="www.iQuranAccess.com" />
  <meta name="description" content="" />
    <portal:StyleSheetCombiner id="StyleSheetCombiner" runat="server" />
</head>
<body class="verse-page">
    <form id="form1" runat="server">
    <asp:Panel ID="pnlVerse" runat="server">
        <asp:Literal ID="litVerse" runat="server" /><br />
        <asp:Literal ID="litInfo" runat="server" />
    </asp:Panel>
    <portal:mojoGoogleAnalyticsScript ID="mojoGoogleAnalyticsScript1" runat="server" />
    </form>
</body>
</html>