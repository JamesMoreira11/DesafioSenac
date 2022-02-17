using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using UT = Cielo.Gtec.BLL.Utilities.GtecAspUtil;

namespace Cielo.Gtec.BLL.Model
{
    public class ST_PERMISSOES
    {
        #region Propriedades
        public int IN_EDITAR_01;
        public int IN_EDITAR_02;
        public int IN_EDITAR_03;
        public int IN_EDITAR_04;
        public int IN_EDITAR_05;
        public int IN_EDITAR_06;
        public int IN_EDITAR_07;
        public int IN_EDITAR_08;
        public int IN_EDITAR_09;
        public int IN_EDITAR_10;
        public int IN_EDITAR_11;
        public int IN_EDITAR_12;
        public int IN_EDITAR_13;
        public int IN_EDITAR_14;
        public int IN_EDITAR_15;
        public int IN_EDITAR_16;

        private int IN_CONSULTAR_01;
        private int IN_CONSULTAR_02;
        private int IN_CONSULTAR_03;
        private int IN_CONSULTAR_04;
        private int IN_CONSULTAR_05;
        private int IN_CONSULTAR_06;
        private int IN_CONSULTAR_07;
        private int IN_CONSULTAR_08;
        private int IN_CONSULTAR_09;
        private int IN_CONSULTAR_10;
        private int IN_CONSULTAR_11;
        private int IN_CONSULTAR_12;
        private int IN_CONSULTAR_13;
        private int IN_CONSULTAR_14;
        private int IN_CONSULTAR_15;
        private int IN_CONSULTAR_16;

        private int IN_SALVAR_EXECUTAR_01;
        private int IN_SALVAR_EXECUTAR_02;
        private int IN_SALVAR_EXECUTAR_03;
        private int IN_SALVAR_EXECUTAR_04;
        private int IN_SALVAR_EXECUTAR_05;
        private int IN_SALVAR_EXECUTAR_06;
        private int IN_SALVAR_EXECUTAR_07;
        private int IN_SALVAR_EXECUTAR_08;
        private int IN_SALVAR_EXECUTAR_09;
        private int IN_SALVAR_EXECUTAR_10;
        private int IN_SALVAR_EXECUTAR_11;
        private int IN_SALVAR_EXECUTAR_12;
        private int IN_SALVAR_EXECUTAR_13;
        private int IN_SALVAR_EXECUTAR_14;
        private int IN_SALVAR_EXECUTAR_15;
        private int IN_SALVAR_EXECUTAR_16;

        #endregion

        #region Construtor
        public ST_PERMISSOES()
        {
            IN_EDITAR_01 = 0;
            IN_EDITAR_02 = 0;
            IN_EDITAR_03 = 0;
            IN_EDITAR_04 = 0;
            IN_EDITAR_05 = 0;
            IN_EDITAR_06 = 0;
            IN_EDITAR_07 = 0;
            IN_EDITAR_08 = 0;
            IN_EDITAR_09 = 0;
            IN_EDITAR_10 = 0;
            IN_EDITAR_11 = 0;
            IN_EDITAR_12 = 0;
            IN_EDITAR_13 = 0;
            IN_EDITAR_14 = 0;
            IN_EDITAR_15 = 0;
            IN_EDITAR_16 = 0;
            IN_CONSULTAR_01 = 0;
            IN_CONSULTAR_02 = 0;
            IN_CONSULTAR_03 = 0;
            IN_CONSULTAR_04 = 0;
            IN_CONSULTAR_05 = 0;
            IN_CONSULTAR_06 = 0;
            IN_CONSULTAR_07 = 0;
            IN_CONSULTAR_08 = 0;
            IN_CONSULTAR_09 = 0;
            IN_CONSULTAR_10 = 0;
            IN_CONSULTAR_11 = 0;
            IN_CONSULTAR_12 = 0;
            IN_CONSULTAR_13 = 0;
            IN_CONSULTAR_14 = 0;
            IN_CONSULTAR_15 = 0;
            IN_CONSULTAR_16 = 0;
            IN_SALVAR_EXECUTAR_01 = 0;
            IN_SALVAR_EXECUTAR_02 = 0;
            IN_SALVAR_EXECUTAR_03 = 0;
            IN_SALVAR_EXECUTAR_04 = 0;
            IN_SALVAR_EXECUTAR_05 = 0;
            IN_SALVAR_EXECUTAR_06 = 0;
            IN_SALVAR_EXECUTAR_07 = 0;
            IN_SALVAR_EXECUTAR_08 = 0;
            IN_SALVAR_EXECUTAR_09 = 0;
            IN_SALVAR_EXECUTAR_10 = 0;
            IN_SALVAR_EXECUTAR_11 = 0;
            IN_SALVAR_EXECUTAR_12 = 0;
            IN_SALVAR_EXECUTAR_13 = 0;
            IN_SALVAR_EXECUTAR_14 = 0;
            IN_SALVAR_EXECUTAR_15 = 0;
            IN_SALVAR_EXECUTAR_16 = 0;
        }
        #endregion

        #region Métodos
        // Traduzido da versão original em VbScript
        public void AvaliarArray(List<int> permissoes, ModelChamadoItem ch, ST_CONTROLE ct)
        {
            for (int i = 0; i < permissoes.Count; i += 3)
            {
                int iGPA77 = i;

                switch ((iGPA77))
                {
                    case 0:
                        //	Alteração de Status S/Baixa
                        if (ct.IN_TRANSICAO == 0 && ch.CD_SERVICO_TIPO != 5)
                        {
                            IN_EDITAR_01 = permissoes[iGPA77];
                            IN_CONSULTAR_01 = permissoes[iGPA77 + 1];
                            IN_SALVAR_EXECUTAR_01 = permissoes[iGPA77 + 2];
                        }
                        else
                        {
                            IN_EDITAR_01 = 0;
                            IN_CONSULTAR_01 = 0;
                            IN_SALVAR_EXECUTAR_01 = 0;
                        }
                        break;
                    case 3:
                        //	Alteração de Status C/Baixa S/Cancelamento
                        if (ct.IN_TRANSICAO == 0 && ch.CD_SERVICO_TIPO != 5)
                        {
                            IN_EDITAR_02 = permissoes[iGPA77];
                            IN_CONSULTAR_02 = permissoes[iGPA77 + 1];
                            IN_SALVAR_EXECUTAR_02 = permissoes[iGPA77 + 2];
                        }
                        else
                        {
                            IN_EDITAR_02 = 0;
                            IN_CONSULTAR_02 = 0;
                            IN_SALVAR_EXECUTAR_02 = 0;
                        }
                        break;
                    case 6:
                        //	Alteração de Status C/Baixa C/Cancelamento
                        if (ct.IN_TRANSICAO == 0)
                        {
                            IN_EDITAR_03 = permissoes[iGPA77];
                            IN_CONSULTAR_03 = permissoes[iGPA77 + 1];
                            IN_SALVAR_EXECUTAR_03 = permissoes[iGPA77 + 2];
                        }
                        else
                        {
                            IN_EDITAR_03 = 0;
                            IN_CONSULTAR_03 = 0;
                            IN_SALVAR_EXECUTAR_03 = 0;
                        }
                        break;
                    case 9:
                        //	Alteração de Motivo de Retorno (Somente Chamados Baixados)
                        if (!UT.IsNull(ch.DH_BAIXA) && ch.CD_SERVICO_TIPO != 5)
                        {
                            IN_EDITAR_04 = permissoes[iGPA77];
                            IN_CONSULTAR_04 = permissoes[iGPA77 + 1];
                            IN_SALVAR_EXECUTAR_04 = permissoes[iGPA77 + 2];
                        }
                        else
                        {
                            IN_EDITAR_04 = 0;
                            IN_CONSULTAR_04 = 0;
                            IN_SALVAR_EXECUTAR_04 = 0;
                        }
                        break;
                    case 12:
                        //	Alteração de Data de Baixa Logística
                        if (!UT.IsNull(ch.DH_ENVIO) && ch.CD_SERVICO_TIPO != 5)
                        {
                            IN_EDITAR_05 = permissoes[iGPA77];
                            IN_CONSULTAR_05 = permissoes[iGPA77 + 1];
                            IN_SALVAR_EXECUTAR_05 = permissoes[iGPA77 + 2];
                        }
                        else
                        {
                            IN_EDITAR_05 = 0;
                            IN_CONSULTAR_05 = 0;
                            IN_SALVAR_EXECUTAR_05 = 0;
                        }
                        break;
                    case 15:
                        //	Alteração de Data de Agendamento
                        IN_EDITAR_06 = permissoes[iGPA77];
                        IN_CONSULTAR_06 = permissoes[iGPA77 + 1];
                        IN_SALVAR_EXECUTAR_06 = permissoes[iGPA77 + 2];
                        break;
                    case 18:
                        //	Transferência de Chamados Entre ATs (Somente Chamados Designados)
                        if (!UT.IsNull(ch.CD_AT_DESIGNADO) && ch.CD_SERVICO_TIPO != 5)
                        {
                            IN_EDITAR_07 = permissoes[iGPA77];
                            IN_CONSULTAR_07 = permissoes[iGPA77 + 1];
                            IN_SALVAR_EXECUTAR_07 = permissoes[iGPA77 + 2];
                        }
                        else
                        {
                            IN_EDITAR_04 = 0;
                            IN_CONSULTAR_04 = 0;
                            IN_SALVAR_EXECUTAR_04 = 0;
                        }
                        break;
                    case 21:
                        //	Designação Manual (Somente Chamados de Manutenção Sem Designação de Atendimento)
                        if ((ch.CD_SERVICO_TIPO == 2 || (ch.CD_SERVICO_TIPO == 3)) && UT.IsNull(ch.CD_AT_DESIGNADO) && ct.IN_TRANSICAO == 0)
                        {
                            IN_EDITAR_08 = permissoes[iGPA77];
                            IN_CONSULTAR_08 = permissoes[iGPA77 + 1];
                            IN_SALVAR_EXECUTAR_08 = permissoes[iGPA77 + 2];
                        }
                        else
                        {
                            IN_EDITAR_08 = 0;
                            IN_CONSULTAR_08 = 0;
                            IN_SALVAR_EXECUTAR_08 = 0;
                        }
                        break;
                    case 24:
                        //	Alteração de Ocorrências (Somente Chamados de Manutenção)
                        if (ch.CD_SERVICO_TIPO == 2)
                        {
                            IN_EDITAR_09 = permissoes[iGPA77];
                            IN_CONSULTAR_09 = permissoes[iGPA77 + 1];
                            IN_SALVAR_EXECUTAR_09 = permissoes[iGPA77 + 2];
                        }
                        else
                        {
                            IN_EDITAR_09 = 0;
                            IN_CONSULTAR_09 = 0;
                            IN_SALVAR_EXECUTAR_09 = 0;
                        }
                        break;
                    case 27:
                        //	Alteração de Modelo/Solução (Chamado de Manutenção/Desinstalação Pendentes e Sem Envio)
                        if ((ch.CD_SERVICO_TIPO == 2 || ch.CD_SERVICO_TIPO == 3) && UT.IsNull(ch.DH_BAIXA) && ct.IN_TRANSICAO == 0)
                        {
                            IN_EDITAR_10 = permissoes[iGPA77];
                            IN_CONSULTAR_10 = permissoes[iGPA77 + 1];
                            IN_SALVAR_EXECUTAR_10 = permissoes[iGPA77 + 2];
                        }
                        else
                        {
                            IN_EDITAR_10 = 0;
                            IN_CONSULTAR_10 = 0;
                            IN_SALVAR_EXECUTAR_10 = 0;
                        }
                        break;
                    case 30:
                        //	Re-Ativação do Chamado (Somente Chamados Baixados)

                        /*  Expressão original do ASP é a que segue: ... AND IN_OPERADOR_LOGISTICO <> 1 ...
                         *  
                         *  Não importa se IN_OPERADOR_LOGISTICO = true ou IN_OPERADOR_LOGISTICO = false, ASP retorna TRUE pois:
                         *      - equivale a CInt(IN_OPERADOR_LOGISTICO) <> 1
                         *      - CInt(IN_OPERADOR_LOGISTICO) retornará [0] ou [-1], respectivamente para false e true, portanto sempre diferente de 1.
                         *      - CInt(IN_OPERADOR_LOGISTICO) será sempre DIFERENTE de 1.
                         *      - Portanto retornará sempre TRUE pois será sempre DIFERENTE.
                         *      
                         *  Portanto, com objetivo de apresentar mesmo comportamento que versão original, será fixado em "true".
                         */

                        //if (!UT.IsNull(ch.DH_BAIXA) && ct.IN_REATIVADO == 0 && ch.CD_SERVICO_TIPO != 5 && ch.IN_OPERADOR_LOGISTICO != 1)
                        if (!UT.IsNull(ch.DH_BAIXA) && ct.IN_REATIVADO == 0 && ch.CD_SERVICO_TIPO != 5)
                        {
                            IN_EDITAR_11 = permissoes[iGPA77];
                            IN_CONSULTAR_11 = permissoes[iGPA77 + 1];
                            IN_SALVAR_EXECUTAR_11 = permissoes[iGPA77 + 2];
                        }
                        else
                        {
                            IN_EDITAR_11 = 0;
                            IN_CONSULTAR_11 = 0;
                            IN_SALVAR_EXECUTAR_11 = 0;
                        }
                        break;
                    case 45:
                        //	Re-Envio de Chamados (Somente Chamados Enviados e Não Baixados)
                        //	Funcionalidade Desabilitada a Pedido do Usuário
                        //	If (Len(ch.DH_ENVIO) > 0 And Not UT.IsNull(ch.DH_ENVIO)) And (Len(ch.DH_BAIXA) = 0 Or UT.IsNull(ch.DH_BAIXA)) AND (ch.CD_SERVICO_TIPO <> "5") Then
                        //	IN_EDITAR_12 =					permissoes[iGPA77)
                        //	IN_CONSULTAR_12 =				permissoes[iGPA77+1)
                        //	IN_SALVAR_EXECUTAR_12 =	permissoes[iGPA77+2)
                        //	Else
                        IN_EDITAR_12 = 0;
                        IN_CONSULTAR_12 = 0;
                        IN_SALVAR_EXECUTAR_12 = 0;
                        break;
                    //	End If
                    case 33:
                        //	Re-Tentativa de visita
                        if (UT.IsNull(ch.DH_BAIXA) && !UT.IsNull(ch.DH_ENVIO))
                        {
                            IN_EDITAR_13 = permissoes[iGPA77];
                            IN_CONSULTAR_13 = permissoes[iGPA77 + 1];
                            IN_SALVAR_EXECUTAR_13 = permissoes[iGPA77 + 2];
                        }
                        else
                        {
                            IN_EDITAR_13 = 0;
                            IN_CONSULTAR_13 = 0;
                            IN_SALVAR_EXECUTAR_13 = 0;
                        }
                        break;
                    case 36:
                        //	BAIXA LOGÍSTICA (Retorno À Base)
                        if (!UT.IsNull(ch.DH_BAIXA) && !UT.IsNull(ch.DH_ENVIO) && !ch.IN_CANCELAMENTO)
                        {
                            IN_EDITAR_14 = permissoes[iGPA77];
                            IN_CONSULTAR_14 = permissoes[iGPA77 + 1];
                            IN_SALVAR_EXECUTAR_14 = permissoes[iGPA77 + 2];
                        }
                        else
                        {
                            IN_EDITAR_14 = 0;
                            IN_CONSULTAR_14 = 0;
                            IN_SALVAR_EXECUTAR_14 = 0;
                        }
                        break;
                    case 39:
                        //	BAIXA OS

                        // => VALIDAR COM ALDO, se será mantida a alteração realizada, conforme descrito abaixo: 
                        //    Por Sandro: NA TRADUÇÃO DO ORIGINAL PERCEBE-SE QUE O "SEGUNDO" CAMPO "ch.DH_BAIXA" DEVERIA SER "ch.DH_BXLOGISTICA"

                        // If (Len(ch.DH_BAIXA) = 0 Or UT.IsNull(ch.DH_BAIXA)) And (Len(ch.DH_BXLOGISTICA) = 0 Or UT.IsNull(ch.DH_BAIXA)) And (Len(ch.DH_ENVIO) > 0 And Not UT.IsNull(ch.DH_ENVIO)) And ch.IN_OPERADOR_LOGISTICO Then
                        if (UT.IsNull(ch.DH_BAIXA) && UT.IsNull(ch.DH_BXLOGISTICA) && !UT.IsNull(ch.DH_ENVIO) && ch.IN_OPERADOR_LOGISTICO)
                        {
                            IN_EDITAR_15 = permissoes[iGPA77];
                            IN_CONSULTAR_15 = permissoes[iGPA77 + 1];
                            IN_SALVAR_EXECUTAR_15 = permissoes[iGPA77 + 2];
                        }
                        else
                        {
                            IN_EDITAR_15 = 0;
                            IN_CONSULTAR_15 = 0;
                            IN_SALVAR_EXECUTAR_15 = 0;
                        }
                        break;
                    case 42:
                        //	COBRANÇA DE CHAMADO (OPERADOR, PENDENTE, ENVIADO, SEM BAIXA OS)
                        if (UT.IsNull(ch.DH_BAIXA) && !UT.IsNull(ch.DH_ENVIO) && ch.IN_OPERADOR_LOGISTICO && !(ch.CD_SERVICO_TIPO == 4 && ch.IN_GERA_NU_LOGICO == false))
                        {
                            IN_EDITAR_16 = permissoes[iGPA77];
                            IN_CONSULTAR_16 = permissoes[iGPA77 + 1];
                            IN_SALVAR_EXECUTAR_16 = permissoes[iGPA77 + 2];
                        }
                        else
                        {
                            IN_EDITAR_16 = 0;
                            IN_CONSULTAR_16 = 0;
                            IN_SALVAR_EXECUTAR_16 = 0;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public void AplicarRegrasAdicionais(ModelChamadoItem ch, ST_CONTROLE ct)
        {
            if (!UT.IsNull(ch.DH_PROCESS_EDS_ST))
            {
                ct.IN_SALVAR_EXECUTAR = 0;
                //ct.IN_SALVAR_EXECUTAR2 = 0;
                IN_EDITAR_01 = 0;
                IN_EDITAR_02 = 0;
                IN_EDITAR_03 = 0;
                IN_EDITAR_04 = 0;
                IN_EDITAR_05 = 0;
                IN_EDITAR_06 = 0;
                IN_EDITAR_07 = 0;
                IN_EDITAR_08 = 0;
                IN_EDITAR_09 = 0;
                IN_EDITAR_10 = 0;
                IN_EDITAR_11 = 0;
                IN_EDITAR_12 = 0;
                IN_EDITAR_13 = 0;
                IN_EDITAR_14 = 0;
                IN_EDITAR_15 = 0;
                IN_EDITAR_16 = 0;
            }

            // Função desabilitada em Função do Operador Logistico
            // Não deixa alterar a data de baixa logistica em nenhum caso
            IN_EDITAR_05 = 0;
        }

        #endregion
    }
}
