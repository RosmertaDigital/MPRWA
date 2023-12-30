<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="BMHSRPv2.RazorPay.test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css">
    <link rel="stylesheet" href="../assets/css/bootstrap-datepicker3.min.css">
    <link rel="stylesheet" href="../assets/css/owl.carousel.min.css">
    <link rel="stylesheet" href="../assets/css/app.css?1603186240">
    <link rel="shortcut icon" href="../assets/img/fav.png">

    <script type="text/javascript">
        function ValidateForm() {
            //alert("test");
            //document.getElementById('customerData').submit();
            //return true;

            var edit = document.querySelector(".razorpay-payment-button");

            edit.addEventListener("click", function () { console.log("clicked") });

            setTimeout(function () {
                edit.click();
            }, 1000);

        }

        function isNumberKey(evt) { // Numbers only
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }

    </script>

    <style>
        .razorpay-payment-button {
     color: #fff;
    background: #39435b;
	font-weight: 400;
    border: 1px solid #39435b;
    border-radius: 50px;
    transition: all .3s;
    display: inline-block;
    padding: 14px 35px;
    padding: 10px 25px;
    font-size: 12.86pt;
    font-family: 'Muli', sans-serif;
    cursor: pointer;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>

    
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>

    <%--<form id="customerData" action="http://localhost:55098//plate/PaymentReceipt.aspx" method="post">
        <script  src="https://checkout.razorpay.com/v1/checkout.js" 
            data-key="rzp_test_Uy1r8Av2FjQdBP" 
            data-amount="100" 
            data-name="Razorpay" 
            data-description="Purchase Description" \
            data-order_id="NTTOTDEVHB" 
            data-image="https://razorpay.com/favicon.png" 
            data-prefill.name="Gaurav Kumar" 
            data-prefill.email="gaurav.kumar@example.com" 
            data-prefill.contact="9123456789" 
            data-theme.color="#F37254"></script>
            <input type="hidden" value="Hidden Element" name="hidden">
     </form>--%>

    <%--<form id="customerData" name="customerData" action="http://localhost:55098//RazorPay/test_response.aspx" method="post">
	<script src="https://checkout.razorpay.com/v1/checkout.js" 
		data-key="rzp_test_Uy1r8Av2FjQdBP" 
		data-amount="8000" 
		data-name="Razorpay" 
		data-notes.order_id="order_G2bfh703c5PFwU" 
		data-notes.order="BMDLAC72D8C2-8703-4B-8703" 
		data-notes.cust_name="Dheerendra Kumar Singh Patel" 
		data-notes.cust_mobile="+91 8882359687" 
		data-notes.vehicleregno="DL7CP0362" 
		data-notes.slotdate="2020-11-09" 
		data-notes.slottime="02:00 PM-06:00 PM" 
		data-notes.slotid="4688" 
		data-notes.appointmenttype="Dealer" 
		data-notes.platesticker="plate" 
		data-description="Purchase Description" 
		data-redirect=true
		data-order_id='order_G2bfh703c5PFwU'" 
		data-image="https://razorpay.com/favicon.png" 
		data-prefill.name="Gaurav Kumar" 
		data-prefill.email="gaurav.kumar@example.com" 
		data-prefill.contact="9123456789" 
		data-theme.color="#F37254">
	</script>
	<input type="submit" value="Pay Now" class="razorpay-payment-button">
	<input type="hidden" value="Hidden Element" name="hidden">
</form>--%>

</body>
</html>
