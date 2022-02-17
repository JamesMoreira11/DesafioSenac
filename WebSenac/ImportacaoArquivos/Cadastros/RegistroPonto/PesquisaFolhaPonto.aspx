<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PesquisaFolhaPonto.aspx.cs" Inherits="Cadastros_RegistroPonto_PesquisaFolhaPonto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cielo GteC</title>
    <link href="Css/main.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .auto-style1 {
            width: 850px;
        }
        .auto-style2 {
            width: 850px;
            height: 26px;
        }
        .auto-style3 {
            height: 26px;
        }
    </style>
</head>
<body vlink="#FFFFFF" marginwidth="0" marginheight="0" link="#FFFFFF" alink="#FFFFFF"
    leftmargin="0" topmargin="0">

    <table align="center" border="0" cellpadding="0" cellspacing="0" bgcolor="#B4C9D6" width="100%">
        <tr>
            <td>
                <table align="center" border="0" cellpadding="1" cellspacing="2" bgcolor="#B4C9D6" width="100%">
                    <tr>
                        <td width="" align="right" valign="top" bgcolor="#B4C9D6">
                            <font size="" face="Verdana" color="#000000">&nbsp;&nbsp;
					    <a href="javascript:print()">
                            <img src="../../Imagens/ico/print.gif" border="0" alt="Imprimir" /></a>
                                &nbsp;|&nbsp;
					    <a href="javascript:location.reload ()">
                            <img src="../../Imagens/ico/refresh_01.gif" border="0" alt="Atualizar" /></a>
                                &nbsp;|&nbsp;
		                <a href="PesquisaFolhaPonto.aspx">
                            <img src="../../Imagens/ico/reset_01.gif" border="0" alt="Reiniciar" /></a>
                                &nbsp;|&nbsp;
                            </font>
                        </td>
                    </tr>
                    <tr>
                        <td width="" align="right" valign="bottom" bgcolor="#FFFFFF">
                            <font face="Verdana" size="2" color="#336699">
                                <strong><em>
                                    <asp:Label ID="lblTituloTela" runat="server" Text="FOLHA DE PONTO DO COLABORADOR"></asp:Label></em></strong>
                            </font>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true" />

        <font size="1" face="Verdana" color="#000000">
            <table align="left" border="0" cellpadding="0" cellspacing="0" bgcolor="#B4C9D6" width="98%">
                <tr>
                    <td>
                        <table align="left" border="0" cellpadding="1" cellspacing="2" bgcolor="#B4C9D6" width="100%">

                            <tr>
                                <td width="" align="right" valign="middle" colspan='2'>
                                    <asp:Button ID="btnOk" runat="server" Text="OK" Font-Size="Medium" OnClick="btnOk_Click" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnLimpar" runat="server" Text="LIMPAR" Font-Size="Medium" OnClick="btnLimpar_Click" />
                                </td>
                            </tr>

                            <tr>
                                <td width="15%" valign="middle" bgcolor="#e4e9ef" align="left">&nbsp;&nbsp;<b>Codigo Colaborador</b>&nbsp;
                                    <asp:TextBox ID="txtColaborador" runat="server" MaxLength="08" Width="32px"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td bgcolor="#FFFFFF" colspan="2" align="center">
                                        <font face="Verdana" size="2" color="black">
											<br />
											<b>Folha de Ponto dos Colaboradores</b>
                                        </font>
                                        &nbsp;

                                    <asp:GridView ID="grdItens" runat="server" AutoGenerateColumns="False" ShowFooter="false" BackColor="#B4C9D6" Width="100%" BorderWidth="2px" CellPadding="4" EmptyDataText="Não existe registro com o filtro utilizado" AllowPaging="true" PageSize="15"
                                        OnPageIndexChanging="grdItens_PageIndexChanging"
                                        OnRowCommand="grdItens_RowCommand"
                                        OnRowDataBound="grdItens_RowDataBound"
                                        DataKeyNames="CD_GUID">
                                        <RowStyle BackColor="White" ForeColor="#000000" />
                                        <Columns>
                                            <asp:BoundField DataField="CD_GUID" HeaderText="COD." ItemStyle-HorizontalAlign="Center" Visible="true"  ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="NM_COLABORADOR" HeaderText="NOME COLABORADOR" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40%" />
                                            <asp:BoundField DataField="DT_LANCAMENTO" HeaderText="DATA" ItemStyle-HorizontalAlign="Center" Visible="true"  ItemStyle-Width="15%" />
                                            <asp:BoundField DataField="HR_ENTRADA" HeaderText="ENTRADA" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="05%" />
                                            <asp:BoundField DataField="HR_PAUSA" HeaderText="PAUSA" ItemStyle-HorizontalAlign="Center" Visible="true"  ItemStyle-Width="05%" />
                                            <asp:BoundField DataField="HR_RETORNO" HeaderText="RETORNO" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="05%" />
                                            <asp:BoundField DataField="HR_SAIDA" HeaderText="SAIDA" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="05%" />
                                            <asp:BoundField DataField="HR_TRABALHADAS" HeaderText="HORAS TRABALHADAS" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%" />
                                        </Columns>
                                    </asp:GridView>
                            </tr>
                            <tr>
                                    <td width="15%" align="left" valign="bottom" bgcolor="#B4C9D6">

                            <font face="Verdana" size="2" color= "black">
                                <strong><em>
                                        <asp:Label ID="lbltotal" runat="server" Text="___________________________________"></asp:Label> 
                                        </em></strong>
                            </font>
                                        &nbsp;</td>
                            </tr>
                            <tr>
                                    <td width="85%" align="left" valign="bottom" bgcolor="#B4C9D6">
                            <font face="Verdana" size="2" color= "black">
                                <strong><em>
                                        <asp:Label ID="lblpaginacao" runat="server" Text="Registros por pagina : "></asp:Label>
                                        </em></strong>
                            </font>

                                        <asp:TextBox ID="txtpaginacao" runat="server" MaxLength="04" Width="32px"></asp:TextBox>
                                        <asp:Button runat="server" ID="OK" Text="ALTERAR" OnClick="OK_Click" Width="73px" />
                                    </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </font>
    </form>
</body>
</html>
