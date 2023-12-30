<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentFailed.aspx.cs" Inherits="BMHSRPv2.plate.PaymentFailed" %>

<!DOCTYPE html>
 
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script type="text/javascript"> 
         function preventBack() {
             window.history.forward();
         }

         setTimeout("preventBack()", 0);

         window.onunload = function () { null };
     </script> 
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Payment Failed</h2>
        </div>
    </form>
</body>
</html>
