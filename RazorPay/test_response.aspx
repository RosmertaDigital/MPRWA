<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test_response.aspx.cs" Inherits="BMHSRPv2.RazorPay.test_response" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <asp:Label ID="lblStatus" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="lblPaymentID" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="lblOrderID" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="txtSignature" runat="server" Text="Label"></asp:Label>
        <br />
    </div>
    </form>
</body>
</html>
