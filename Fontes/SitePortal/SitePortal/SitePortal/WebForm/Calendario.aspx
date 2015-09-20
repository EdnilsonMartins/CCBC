<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title></title>
</head>
<body>
    <div>
        <asp:Calendar ID="Calendar1" runat="server" Height="30px" Width="220px" BorderColor="#EEEEEE" CssClass="agenda-topo" ForeColor="White" ShowGridLines="True" TodayDayStyle-BackColor="#e8999d" Font-Size="Small" DayHeaderStyle-Height="20px" DayNameFormat="Shortest">
            <dayheaderstyle backcolor="#F7F7F7" forecolor="Black" bordercolor="#EEEEEE" height="20px" />
            <daystyle bordercolor="#EEEEEE" forecolor="#999999" height="28px" />
            <selecteddaystyle forecolor="#FFFFFF" backcolor="#3333FF" />
            <titlestyle height="45px" backcolor="#CC1F26" forecolor="White" />

            <todaydaystyle backcolor="Red" forecolor="White" font-bold="False"></todaydaystyle>
        </asp:Calendar>
    </div>
</body>
</html>
