<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DataHora.ascx.cs" Inherits="Controles_DataHora" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Panel ID="pnlEdicao" runat="server" style="display:inline;"><asp:TextBox ID="txtData" runat="server" Width="100px" ></asp:TextBox>

    <asp:CalendarExtender ID="txtData_CalendarExtender" runat="server" 
        TargetControlID="txtData">
    </asp:CalendarExtender>

    <asp:MaskedEditExtender ID="txtData_MaskedEditExtender" runat="server" Mask="99/99/9999" MaskType="Date"  
        TargetControlID="txtData">
    </asp:MaskedEditExtender>

    <%--<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtData" Display="Dynamic" ErrorMessage="Data Inválida." Font-Names="Verdana" Font-Size="X-Small" Operator="GreaterThan" Type="Date" ValueToCompare="01/01/1900" />
    --%>

    &nbsp;&nbsp;
    <img src="../imagens/ico/calendar_04.gif" alt="Selecionar data" border="0" onmouseover="window.status='Calendario';return true;" onmouseout="window.status='';return true;" WIDTH="24" HEIGHT="22" onclick="document.all('<%= txtData.ClientID %>').focus();" />

    &nbsp;&nbsp;

    <asp:DropDownList ID="drpHora" runat="server">
        <asp:ListItem Value="00"  Selected="True">00</asp:ListItem>
        <asp:ListItem Value="01" >01</asp:ListItem>
        <asp:ListItem Value="02" >02</asp:ListItem>
        <asp:ListItem Value="03" >03</asp:ListItem>
        <asp:ListItem Value="04" >04</asp:ListItem>
        <asp:ListItem Value="05" >05</asp:ListItem>
        <asp:ListItem Value="06" >06</asp:ListItem>
        <asp:ListItem Value="07" >07</asp:ListItem>
        <asp:ListItem Value="08" >08</asp:ListItem>
        <asp:ListItem Value="09" >09</asp:ListItem>
        <asp:ListItem Value="10" >10</asp:ListItem>
        <asp:ListItem Value="11" >11</asp:ListItem>
        <asp:ListItem Value="12" >12</asp:ListItem>
        <asp:ListItem Value="13" >13</asp:ListItem>
        <asp:ListItem Value="14" >14</asp:ListItem>
        <asp:ListItem Value="15" >15</asp:ListItem>
        <asp:ListItem Value="16" >16</asp:ListItem>
        <asp:ListItem Value="17" >17</asp:ListItem>
        <asp:ListItem Value="18" >18</asp:ListItem>
        <asp:ListItem Value="19" >19</asp:ListItem>
        <asp:ListItem Value="20" >20</asp:ListItem>
        <asp:ListItem Value="21" >21</asp:ListItem>
        <asp:ListItem Value="22" >22</asp:ListItem>
        <asp:ListItem Value="23" >23</asp:ListItem>			
    </asp:DropDownList>

    <b><asp:Label ID="lblDoisPontos" runat="server" Text=":"></asp:Label></b>

    <asp:DropDownList ID="drpMinuto" runat="server">
        <asp:ListItem Value="00"  Selected="True">00</asp:ListItem>
        <asp:ListItem Value="01" >01</asp:ListItem>
        <asp:ListItem Value="02" >02</asp:ListItem>
        <asp:ListItem Value="03" >03</asp:ListItem>
        <asp:ListItem Value="04" >04</asp:ListItem>
        <asp:ListItem Value="05" >05</asp:ListItem>
        <asp:ListItem Value="06" >06</asp:ListItem>
        <asp:ListItem Value="07" >07</asp:ListItem>
        <asp:ListItem Value="08" >08</asp:ListItem>
        <asp:ListItem Value="09" >09</asp:ListItem>
        <asp:ListItem Value="10" >10</asp:ListItem>
        <asp:ListItem Value="11" >11</asp:ListItem>
        <asp:ListItem Value="12" >12</asp:ListItem>
        <asp:ListItem Value="13" >13</asp:ListItem>
        <asp:ListItem Value="14" >14</asp:ListItem>
        <asp:ListItem Value="15" >15</asp:ListItem>
        <asp:ListItem Value="16" >16</asp:ListItem>
        <asp:ListItem Value="17" >17</asp:ListItem>
        <asp:ListItem Value="18" >18</asp:ListItem>
        <asp:ListItem Value="19" >19</asp:ListItem>
        <asp:ListItem Value="20" >20</asp:ListItem>
        <asp:ListItem Value="21" >21</asp:ListItem>
        <asp:ListItem Value="22" >22</asp:ListItem>
        <asp:ListItem Value="23" >23</asp:ListItem>
        <asp:ListItem Value="24" >24</asp:ListItem>
        <asp:ListItem Value="25" >25</asp:ListItem>
        <asp:ListItem Value="26" >26</asp:ListItem>
        <asp:ListItem Value="27" >27</asp:ListItem>
        <asp:ListItem Value="28" >28</asp:ListItem>
        <asp:ListItem Value="29" >29</asp:ListItem>
        <asp:ListItem Value="30" >30</asp:ListItem>
        <asp:ListItem Value="31" >31</asp:ListItem>
        <asp:ListItem Value="32" >32</asp:ListItem>
        <asp:ListItem Value="33" >33</asp:ListItem>
        <asp:ListItem Value="34" >34</asp:ListItem>
        <asp:ListItem Value="35" >35</asp:ListItem>
        <asp:ListItem Value="36" >36</asp:ListItem>
        <asp:ListItem Value="37" >37</asp:ListItem>
        <asp:ListItem Value="38" >38</asp:ListItem>
        <asp:ListItem Value="39" >39</asp:ListItem>
        <asp:ListItem Value="40" >40</asp:ListItem>
        <asp:ListItem Value="41" >41</asp:ListItem>
        <asp:ListItem Value="42" >42</asp:ListItem>
        <asp:ListItem Value="43" >43</asp:ListItem>
        <asp:ListItem Value="44" >44</asp:ListItem>
        <asp:ListItem Value="45" >45</asp:ListItem>
        <asp:ListItem Value="46" >46</asp:ListItem>
        <asp:ListItem Value="47" >47</asp:ListItem>
        <asp:ListItem Value="48" >48</asp:ListItem>
        <asp:ListItem Value="49" >49</asp:ListItem>
        <asp:ListItem Value="50" >50</asp:ListItem>
        <asp:ListItem Value="51" >51</asp:ListItem>
        <asp:ListItem Value="52" >52</asp:ListItem>
        <asp:ListItem Value="53" >53</asp:ListItem>
        <asp:ListItem Value="54" >54</asp:ListItem>
        <asp:ListItem Value="55" >55</asp:ListItem>
        <asp:ListItem Value="56" >56</asp:ListItem>
        <asp:ListItem Value="57" >57</asp:ListItem>
        <asp:ListItem Value="58" >58</asp:ListItem>
        <asp:ListItem Value="59" >59</asp:ListItem>			
    </asp:DropDownList>
    
</asp:Panel><font face="Verdana" size="1" COLOR=#336699><b><asp:Label ID="lblData" runat="server" Text="" Visible=false></asp:Label></b></font>
