<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Data.ascx.cs" Inherits="Controles_Data" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:TextBox ID="txtData" runat="server">
</asp:TextBox>

<asp:CalendarExtender ID="txtData_CalendarExtender" runat="server" 
    TargetControlID="txtData">
</asp:CalendarExtender>

<asp:MaskedEditExtender ID="txtData_MaskedEditExtender" runat="server" Mask="99/99/9999" MaskType="Date"  
    TargetControlID="txtData">
</asp:MaskedEditExtender>

<%--<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtData" Display="Dynamic" ErrorMessage="Data Inválida." Font-Names="Verdana" Font-Size="X-Small" Operator="GreaterThan" Type="Date" ValueToCompare="01/01/1900" />
--%>