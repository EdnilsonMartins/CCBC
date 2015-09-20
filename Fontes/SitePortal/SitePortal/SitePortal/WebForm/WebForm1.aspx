<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="SitePortal.WebForm.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script>
        $(document).ready(function () {
            $('[data-toggle="popover"]').popover({
                placement: 'top'
            });
        });
    </script>
</head>
<body style="background: none;">
    <form id="form1" runat="server">
    <div class="lateral-conteudo-agenda-conteudo">
            <asp:Calendar ID="Calendar1" runat="server" Height="30px" Width="220px" OnDayRender="Calendar1_DayRender" BorderColor="#EEEEEE" CssClass="agenda-topo" ForeColor="White" ShowGridLines="True"
                 onSubmit="return false;"
                TodayDayStyle-BackColor="#e8999d" Font-Size="Small" DayHeaderStyle-Height="20px" DayNameFormat="Shortest"
                NextMonthText="<a href='' id='nextCalendar' onClick='return false;' style='color: #d61541;' >►</a>"
                PrevMonthText="<a href='' id='prevCalendar' onClick='return false;' style='color: #d61541;' >◄</a>"
                >
                <dayheaderstyle  backcolor="#F7F7F7" forecolor="Black" bordercolor="#EEEEEE" height="20px" />
                <daystyle bordercolor="#EEEEEE" forecolor="#999999" height="28px" />
                <selecteddaystyle  backcolor="#FFFFFF" />
                <titlestyle height="45px" backcolor="#FFFFFF" forecolor="#d61541"   />
                <NextPrevStyle ForeColor="#d61541" Font-Names="Arial"  />
            
                <todaydaystyle backcolor="#FFFFFF" font-bold="False"></todaydaystyle>

            
            </asp:Calendar>
    </div>
    </form>
</body>
</html>
