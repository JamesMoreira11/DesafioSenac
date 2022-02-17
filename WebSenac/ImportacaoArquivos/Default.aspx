<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cielo GteC</title>
    <script type="text/javascript">
        function Redirecionar() {
            window.location.href = "Importacao/CargaArquivoImportacao.aspx";
        }
    </script>
    <style type="text/css">
        .style1
        {
            font-family: Arial, Helvetica, sans-serif;
            font-weight: bold;
        }
        .style2
        {
            text-align: center;
        }
    </style>
</head>
<body onload="Redirecionar()">
    <form id="form1" runat="server">
    <div class="style1">
        <h1 class="style2">
        Aguarde...
        </h1>
    </div>
    </form>
</body>
</html>
