<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BulkUpload.aspx.cs" Inherits="BMHSRPv2.plate.BulkUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>
function isNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .btn {
            font-weight: 250 !important;
        }

        input[type=file] {
            display: block;
            font-size: larger;
        }
    </style>

   <%-- <div class="container-fluid" style="background-color: #ccebf5; height: 70vh!important">--%>
     <div class="container-fluid" style="background-color: #ccebf5; height: 100vh!important">
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
        <div class="container" style="margin-top: 15px!important;">
            <div class="row" id="div1" runat="server">
                <div class="col-md-4"><span style="font-size: 20px; font-weight: bold;">
                    <asp:Label ID="lblPasscode" runat="server" Text="Please Enter Passcode:"></asp:Label></span></div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtPasscode" runat="server" CssClass="form-control" onkeypress="return isNumber(event)" TextMode="Password"></asp:TextBox></div>
                <div class="col-md-4">
                    <asp:Button ID="btnValidate" runat="server" Text="Submit" OnClick="btnValidate_Click" class="btn btn-primary" ForeColor="Black" /></div>
            </div>
            <div class="row" id="div2" runat="server">
                <div class="col-md-12">
                       <span style="float:right;"><a href="download sample.xlsx" style="color: blue">Download Sample</a></span>
                </div>
                <div class="col-md-5">
                    <div class="row"><span style="font-size: 20px; font-weight: bold;" id="step1" runat="server">Step 1:</span></div>
                    <div class="row" style="margin-top: 10px;">
                        <div class="col-md-5">
                            <asp:FileUpload ID="flUpload" runat="server" /></div>
                        <div class="col-md-7">
                            <asp:Button ID="btnUpload" runat="server" Text="Upload" class="btn btn-primary" ForeColor="Black" OnClick="btnUpload_Click" /></div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="row"><span style="font-size: 20px; font-weight: bold;" id="step2" runat="server">Step 2:</span></div>
                    <div class="row" style="margin-top: 10px;">
                        <div class="col md-12">
                            <asp:Button ID="btnValidateVahan" runat="server" Text="Validate Vahan" class="btn btn-primary" ForeColor="Black" OnClick="btnValidateVahan_Click" /></div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="row"><span style="font-size: 20px; font-weight: bold;" id="step3" runat="server">Step 3:</span></div>
                    <div class="row" style="margin-top: 10px;">
                        <div class="col md-6">
                            <asp:Button ID="btnPayNow" runat="server" Text="Pay Now" class="btn btn-primary" ForeColor="Black" OnClick="btnPayNow_Click" />
                            <%--<span style="padding-left: 40px;"><a href="download sample.xlsx" style="color: blue">Download Sample</a></span>--%>
                        </div>
                        <div class="color1 md-6"></div>
                    </div>
                </div>
            </div>
        </div>
        <div class="container" style="margin-top: 25px!important;">
            <asp:GridView ID="grdUploadFiles" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true"
                AllowPaging="true" PageSize="10"
                align="center" CellPadding="8" ForeColor="#333333" GridLines="Both" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" Width="100%" Height="100%" OnPageIndexChanging="grdUploadFiles_PageIndexChanging">
                <AlternatingRowStyle BackColor="White" />
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" Position="TopAndBottom" FirstPageText="First" LastPageText="Last" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <RowStyle ForeColor="#000066" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            Sno
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1 %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            Vehicle Reg No
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%--<asp:TextBox runat="server" ID="txtVehicleRegNo" Text='<%#Eval("VehicleRegNo") %>'></asp:TextBox>--%>
                            <asp:Label ID="lblVehicleRegNo" runat="server" Text='<%#Eval("VehicleRegNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            Chassis No
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblChassisNo" runat="server" Text='<%#Eval("ChassisNo") %>'></asp:Label>
                            <%--<asp:TextBox runat="server" ID="txtChassisNo" Text='<%#Eval("ChassisNo") %>' Width="80px"></asp:TextBox>--%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            Engine No
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblEngineNo" runat="server" Text='<%#Eval("EngineNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            Owner Name
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblOwnerName" runat="server" Text='<%#Eval("OwnerName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            Email ID
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAppointmenttype" runat="server" Text='<%#Eval("EmailID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            Mobile No
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblMobileNo" runat="server" Text='<%#Eval("MobileNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            Billing Address
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblBillingAddress" runat="server" Text='<%#Eval("BillingAddress") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            Vehicle Type
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblVehicleType" runat="server" Text='<%#Eval("VehicleType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            Vehicle Class
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblVehicleType" runat="server" Text='<%#Eval("VehicleClass") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            Bharat Stage
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblBharatStage" runat="server" Text='<%#Eval("BharatStage") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            Remarks
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("Remarks") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--<asp:TemplateField>
                                            <HeaderTemplate>
                                                VehicleType
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                               <asp:Label ID="lblVehicleType" Visible="false" runat="server" Text='<%#Eval("VehicleType") %>'></asp:Label>
                                               <asp:DropDownList runat="server" ID="ddlVehicleType" DataTextField="vehicleTypevalue" DataValueField="vehicleTypevalue"></asp:DropDownList>
                                                 </ItemTemplate>
                                        </asp:TemplateField>--%>
                </Columns>
                <EmptyDataTemplate>
                    <div align="center">No records found.</div>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>
</asp:Content>