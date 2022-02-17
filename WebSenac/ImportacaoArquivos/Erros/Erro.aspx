<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Erro.aspx.cs" Inherits="Erros_Erro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title></title>
</head>
<body>
  <form id="form1" runat="server">
  <br />
  <br />
  <br />
  <h1>
    <center>
      <img border="0" src="../Imagens/ico/error_big.gif" alt="Erro" />
      <font color="red">
        Erro executando sua solicitação, favor entrar em contato com o Suporte Técnico.
        <br />
        <br />
        <asp:Label ID="lblErro" runat="server" Text=""></asp:Label>
      </font>
    </center>
  </h1>
  </form>
</body>
</html>
