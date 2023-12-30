<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VerifyDetailPay.aspx.cs" Inherits="BMHSRPv2.sticker.VerifyDetailPay" %>

<%@ Register Src="~/sticker/ucStepBar.ascx" TagPrefix="uc1" TagName="ucStepBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="app clearfix">
        <section class="after">
            <uc1:ucStepBar runat="server" id="ucStepBar" PageStep="10" BackPage="#" />

            <div class="color2 view-data">
                <div class="in_pages page1 clearfix">
                    <div class="leftside color_blue">
                        <div class="head">
                            <img src="../assets/img/h1.png" draggable="false">
                        </div>
                        <div class="card">
                            <div class="label_card bikefill">
                                <img src="../assets/img/bike-fill1.svg" draggable="false">
                            </div>
                        </div>
                        <div class="card">
                            <div class="label_card">
                                <img src="../assets/img/color0-1.svg" draggable="false">
                            </div>
                        </div>
                        <div class="card">
                            <div class="state label_card">
                                <p><span>HR</span>Haryana</p>
                            </div>
                        </div>
                        <div class="card">
                            <div class="label_card bikefill">
                                <img src="../assets/img/car-fill2.svg" draggable="false">
                                <p>Private Vehicle</p>
                            </div>
                        </div>
                        <div class="card">
                            <div class="state label_card mb0">
                                <p><span>Petrol</span></p>
                            </div>
                        </div>
                        <div class="card">
                            <div class="label_card address"><b>AMAN<br>
                                    MOTORS</b>
                                <p>City: DELHI</p>
                                <p>Address: J-128/1 PUSHTA
                                    3-1/2 KARTAR NAGAR
                                </p>
                                <p>pincode: 110053</p></div>
                        </div>
                        <div class="card">
                            <div class="label_card orderno"><b>ORDER NUMBER</b>
                                <p>DLHSRP2019</p>
                                <p>1228182008374</p>
                            </div>
                        </div>
                    </div>
                    <div class="rightside">
                        <div class="addedspace">
                            <h4>Verify Details & Pay</h4>
                            <div class="summary clearfix">
                                <div class="width50 padd0">
                                    <h5>Order number: DLHSRP20191228182008374</h5>
                                    <span>Owner Name</span>
                                    <p><asp:Literal ID="ltrlON" runat="server"></asp:Literal></p>
                                    <span>Mobile No.</span>
                                    <p><asp:Literal ID="ltrlMN" runat="server"></asp:Literal></p>
                                    <span>Address</span>
                                   <%-- <p>72-A, Basement, Jacaranda <br>
                                        Marg, DLF II, Gurgaon <br>
                                        122003, Haryana, India</p>--%>
                                    <p><asp:Literal ID="ltrlAddress" runat="server"></asp:Literal></p>
                                    <span>Email ID</span>
                                    <p><asp:Literal ID="ltrlEmail" runat="server"></asp:Literal></p>
                                    <span>Pin Code</span>
                                    <p><asp:Literal ID="ltrlPinCode" runat="server"></asp:Literal></p>

                                    <p class="notes">Note: Kindly Verify and pay HSRP Fee online.</p>
                                </div>
                                <div class="width50">
                                    <div class="right-pay">
                                        <div class="popup">
                                            <table>
                                                <thead>
                                                <tr>
                                                    <td colspan="2" class="text-center">Description</td>
                                                </tr>
                                                </thead>
                                                <tbody>
                                                <tr>
                                                    <td><span>HSRP Cost</span></td>
                                                   <td><span><asp:Literal ID="ltlCost" runat="server"></asp:Literal></span></td>                                                    
                                                </tr>
                                                <tr>
                                                    <td><span>Handling and<br>Fitment Charges</span></td>
                                                    <td><span><asp:Literal ID="ltlFCharge" runat="server"></asp:Literal></span></td>
                                                </tr>
                                                <tr>
                                                    <td><span>Convenience Fee</span></td>
                                                    <td><span><asp:Literal ID="ltlCFees" runat="server"></asp:Literal></span></td>
                                                </tr>
                                                <tr>
                                                    <td><span>Gross Total</span></td>
                                                    <td><span><asp:Literal ID="LtlGTotal" runat="server"></asp:Literal></span></td>
                                                </tr>
                                                <tr class="removeline">
                                                    <td><span>GST @ 18%</span></td>
                                                    <td><span><asp:Literal ID="ltlgst" runat="server"></asp:Literal></span></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">&nbsp;</td>
                                                </tr>
                                                </tbody>
                                                <tfoot>
                                                <tr class="btold">
                                                    <td>Total Cost</td>
                                                    <td><span><asp:Literal ID="ltltcost" runat="server"></asp:Literal></span></td>
                                                </tr>
                                                </tfoot>
                                            </table>
                                        </div>
                                        <div class="center">
                                            <a href="page15.html" style="float:left;" class="btn isforwardprint">Add More Products</a>
                                            <a href="page15.html" style="float: right" class="filled btn isforwardprint">Pay Online</a>
                                        </div>
                                    </div>
                                </div>

                            </div>

                        </div>
                    </div>
                </div>

            </div>
        </section>
    </div>
     </div>
</asp:Content>
