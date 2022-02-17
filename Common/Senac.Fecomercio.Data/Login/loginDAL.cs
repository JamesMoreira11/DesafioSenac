using Senac.Fecomercio.Common;
using Senac.Fecomercio.Data.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Senac.Fecomercio.Data.Login
{
    public class LoginDAL : BaseDAL
    {
        public static void AtualizarValorParametro(string nomeProcesso, string nomeParametro, int numValorParametro, string valorParametro)
        {
            try
            {
                string numFieldParam = numValorParametro.ToString();

                SqlParameter param01 = new SqlParameter("@P_NM_PROCESSO", SqlDbType.VarChar, 60);
                param01.Direction = ParameterDirection.Input;
                param01.Value = nomeProcesso;

                SqlParameter param02 = new SqlParameter("@P_NM_PARAMETRO", SqlDbType.VarChar, 60);
                param02.Direction = ParameterDirection.Input;
                param02.Value = nomeParametro;

                SqlParameter param03 = new SqlParameter("@P_NUM_VAL_PARAM", SqlDbType.VarChar, 2);
                param03.Direction = ParameterDirection.Input;
                param03.Value = numFieldParam;

                SqlParameter param04 = new SqlParameter("@P_VAL_PARAM", SqlDbType.VarChar, 100);
                param04.Direction = ParameterDirection.Input;
                param04.Value = valorParametro;

                SqlParameter[] parametrosInclusao = { param01, param02, param03, param04 };

                ConexaoPadrao().ExecuteNonQuery("PR_GT_GTEC_PARAMETROS_UPD_VAL_PARAM", CommandType.StoredProcedure, ref parametrosInclusao);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("AtualizarValorParametro() - Exception: {0}", ex.Message));
            }
            finally
            {
                EncerrarConexao();
            }
        }

        public static List<LoginSenac> LoginSenac(string nm_usuario_nt, string nm_dominio_nt)
        {
            try
            {
                SqlParameter pNM_USUARIO_NT = new SqlParameter("@pNM_USUARIO_NT", SqlDbType.VarChar, 20);
                SqlParameter pNM_DOMINIO_NT = new SqlParameter("@pNM_DOMINIO_NT", SqlDbType.VarChar, 20);
                string sql = "Select  NM_USUARIO_NT, NM_DOMINIO_NT, IN_ATIVO From TBGTR_USUARIO With(nolock) Where NM_USUARIO_NT = @pNM_USUARIO_NT and NM_DOMINIO_NT = @pNM_DOMINIO_NT";
                if (nm_usuario_nt.IsNotNull())
                {
                    pNM_USUARIO_NT.Value = nm_usuario_nt;
                }
                else
                {
                    pNM_USUARIO_NT.Value = DBNull.Value;
                }
                if (nm_dominio_nt.IsNotNull())
                {
                    pNM_DOMINIO_NT.Value = nm_dominio_nt;
                }
                else
                {
                    pNM_DOMINIO_NT.Value = DBNull.Value;
                }


                SqlParameter[] parametros = { pNM_USUARIO_NT, pNM_DOMINIO_NT };

                DataTable dt = ConexaoPadrao().ExecuteDataTable(sql, CommandType.Text, ref parametros);
                return PopularParametroSenac(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (!ConexaoPadrao().transacaoAtiva)
                {
                    EncerrarConexao();
                }
            }
        }



        private static List<LoginSenac> PopularParametroSenac(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                List<LoginSenac> listaParametro = new List<LoginSenac>();

                foreach (DataRow dr in dt.Rows)
                {
                    var parametro = new LoginSenac
                    {
                        NM_USUARIO_NT = dr["NM_USUARIO_NT"].ToStringCheckDBNull(),
                        NM_DOMINIO_NT = dr["NM_DOMINIO_NT"].ToStringCheckDBNull(),
                        IN_ATIVO = dr["IN_ATIVO"].ToBool()
                    };
                    //evento.TimeStamp = dr["NU_TIMESTAMP"];

                    listaParametro.Add(parametro);
                }

                return listaParametro;
            }
            return null;
        }

        //public void Inserir(LoginSenac entrada)
        //{
        //    try
        //    {
        //        SqlParameter p1 = new SqlParameter("@NM_USUARIO_NT", SqlDbType.NVarChar, 60);
        //        p1.Direction = ParameterDirection.Input;
        //        p1.Value = entrada.nomeProcesso;

        //        SqlParameter p2 = new SqlParameter("@NM_PARAMETRO", SqlDbType.NVarChar, 60);
        //        p2.Direction = ParameterDirection.Input;
        //        p2.Value = entrada.nomeParametro;

        //        SqlParameter p3 = new SqlParameter("@DESCR_PARAMETROS", SqlDbType.NVarChar, 200);
        //        p3.Direction = ParameterDirection.Input;
        //        p3.Value = entrada.descricaoParametro;

        //        SqlParameter p4 = new SqlParameter("@DESC_PRM_1", SqlDbType.NVarChar, 200);
        //        p4.Direction = ParameterDirection.Input;
        //        p4.Value = entrada.descricaoParametro1;

        //        SqlParameter p5 = new SqlParameter("@PRM_VLR_PARAM1", SqlDbType.NVarChar, 100);
        //        p5.Direction = ParameterDirection.Input;
        //        p5.Value = entrada.valorParametro1;

        //        SqlParameter p6 = new SqlParameter("@DESC_PRM_2", SqlDbType.NVarChar, 200);
        //        p6.Direction = ParameterDirection.Input;
        //        p6.Value = entrada.descricaoParametro2;

        //        SqlParameter p7 = new SqlParameter("@PRM_VLR_PARAM2", SqlDbType.NVarChar, 100);
        //        p7.Direction = ParameterDirection.Input;
        //        p7.Value = entrada.valorParametro2;

        //        SqlParameter p8 = new SqlParameter("@DESC_PRM_3", SqlDbType.NVarChar, 200);
        //        p8.Direction = ParameterDirection.Input;
        //        p8.Value = entrada.descricaoParametro3;

        //        SqlParameter p9 = new SqlParameter("@PRM_VLR_PARAM3", SqlDbType.NVarChar, 100);
        //        p9.Direction = ParameterDirection.Input;
        //        p9.Value = entrada.valorParametro3;

        //        SqlParameter p10 = new SqlParameter("@DESC_PRM_4", SqlDbType.NVarChar, 200);
        //        p10.Direction = ParameterDirection.Input;
        //        p10.Value = entrada.descricaoParametro4;

        //        SqlParameter p11 = new SqlParameter("@PRM_VLR_PARAM4", SqlDbType.NVarChar, 100);
        //        p11.Direction = ParameterDirection.Input;
        //        p11.Value = entrada.valorParametro4;

        //        SqlParameter p12 = new SqlParameter("@DESC_PRM_5", SqlDbType.NVarChar, 200);
        //        p12.Direction = ParameterDirection.Input;
        //        p12.Value = entrada.descricaoParametro5;

        //        SqlParameter p13 = new SqlParameter("@PRM_VLR_PARAM5", SqlDbType.NVarChar, 100);
        //        p13.Direction = ParameterDirection.Input;
        //        p13.Value = entrada.valorParametro5;

        //        SqlParameter p14 = new SqlParameter("@DESC_PRM_6", SqlDbType.NVarChar, 200);
        //        p14.Direction = ParameterDirection.Input;
        //        p14.Value = entrada.descricaoParametro6;

        //        SqlParameter p15 = new SqlParameter("@PRM_VLR_PARAM6", SqlDbType.NVarChar, 100);
        //        p15.Direction = ParameterDirection.Input;
        //        p15.Value = entrada.valorParametro6;

        //        SqlParameter p16 = new SqlParameter("@DESC_PRM_7", SqlDbType.NVarChar, 200);
        //        p16.Direction = ParameterDirection.Input;
        //        p16.Value = entrada.descricaoParametro7;

        //        SqlParameter p17 = new SqlParameter("@PRM_VLR_PARAM7", SqlDbType.NVarChar, 100);
        //        p17.Direction = ParameterDirection.Input;
        //        p17.Value = entrada.valorParametro7;

        //        SqlParameter p18 = new SqlParameter("@DESC_PRM_8", SqlDbType.NVarChar, 200);
        //        p18.Direction = ParameterDirection.Input;
        //        p18.Value = entrada.descricaoParametro8;

        //        SqlParameter p19 = new SqlParameter("@PRM_VLR_PARAM8", SqlDbType.NVarChar, 100);
        //        p19.Direction = ParameterDirection.Input;
        //        p19.Value = entrada.valorParametro8;

        //        SqlParameter p20 = new SqlParameter("@DESC_PRM_9", SqlDbType.NVarChar, 200);
        //        p20.Direction = ParameterDirection.Input;
        //        p20.Value = entrada.descricaoParametro9;

        //        SqlParameter p21 = new SqlParameter("@PRM_VLR_PARAM9", SqlDbType.NVarChar, 100);
        //        p21.Direction = ParameterDirection.Input;
        //        p21.Value = entrada.valorParametro9;

        //        SqlParameter p22 = new SqlParameter("@DESC_PRM_10", SqlDbType.NVarChar, 200);
        //        p22.Direction = ParameterDirection.Input;
        //        p22.Value = entrada.descricaoParametro10;

        //        SqlParameter p23 = new SqlParameter("@PRM_VLR_PARAM10", SqlDbType.NVarChar, 100);
        //        p23.Direction = ParameterDirection.Input;
        //        p23.Value = entrada.valorParametro10;

        //        SqlParameter p24 = new SqlParameter("@PRM_STAT_COD", SqlDbType.Bit);
        //        p24.Direction = ParameterDirection.Input;

        //        if (entrada.statuscodigo.HasValue && entrada.statuscodigo.Value.Equals(1))
        //        {
        //            p24.Value = 1;
        //        }
        //        else
        //        {
        //            p24.Value = 0;
        //        }

        //        SqlParameter[] parametrosInclusao = { p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24 };

        //        ConexaoPadrao().ExecuteNonQuery("PR_GT_GTEC_PARAMETROS_I_001", CommandType.StoredProcedure, ref parametrosInclusao);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (!ConexaoPadrao().transacaoAtiva)
        //        {
        //            EncerrarConexao();
        //        }
        //    }
        //}

        //public void Atualizar(LoginSenac entrada)
        //{
        //    try
        //    {
        //        SqlParameter p1 = new SqlParameter("@NM_PROCESSO", SqlDbType.NVarChar, 60);
        //        p1.Direction = ParameterDirection.Input;
        //        p1.Value = entrada.nomeProcesso;

        //        SqlParameter p2 = new SqlParameter("@NM_PARAMETRO", SqlDbType.NVarChar, 60);
        //        p2.Direction = ParameterDirection.Input;
        //        p2.Value = entrada.nomeParametro;

        //        SqlParameter p3 = new SqlParameter("@DESCR_PARAMETROS", SqlDbType.NVarChar, 200);
        //        p3.Direction = ParameterDirection.Input;
        //        p3.Value = entrada.descricaoParametro;

        //        SqlParameter p4 = new SqlParameter("@DESC_PRM_1", SqlDbType.NVarChar, 200);
        //        p4.Direction = ParameterDirection.Input;
        //        p4.Value = entrada.descricaoParametro1;

        //        SqlParameter p5 = new SqlParameter("@PRM_VLR_PARAM1", SqlDbType.NVarChar, 100);
        //        p5.Direction = ParameterDirection.Input;
        //        p5.Value = entrada.valorParametro1;

        //        SqlParameter p6 = new SqlParameter("@DESC_PRM_2", SqlDbType.NVarChar, 200);
        //        p6.Direction = ParameterDirection.Input;
        //        p6.Value = entrada.descricaoParametro2;

        //        SqlParameter p7 = new SqlParameter("@PRM_VLR_PARAM2", SqlDbType.NVarChar, 100);
        //        p7.Direction = ParameterDirection.Input;
        //        p7.Value = entrada.valorParametro2;

        //        SqlParameter p8 = new SqlParameter("@DESC_PRM_3", SqlDbType.NVarChar, 200);
        //        p8.Direction = ParameterDirection.Input;
        //        p8.Value = entrada.descricaoParametro3;

        //        SqlParameter p9 = new SqlParameter("@PRM_VLR_PARAM3", SqlDbType.NVarChar, 100);
        //        p9.Direction = ParameterDirection.Input;
        //        p9.Value = entrada.valorParametro3;

        //        SqlParameter p10 = new SqlParameter("@DESC_PRM_4", SqlDbType.NVarChar, 200);
        //        p10.Direction = ParameterDirection.Input;
        //        p10.Value = entrada.descricaoParametro4;

        //        SqlParameter p11 = new SqlParameter("@PRM_VLR_PARAM4", SqlDbType.NVarChar, 100);
        //        p11.Direction = ParameterDirection.Input;
        //        p11.Value = entrada.valorParametro4;

        //        SqlParameter p12 = new SqlParameter("@DESC_PRM_5", SqlDbType.NVarChar, 200);
        //        p12.Direction = ParameterDirection.Input;
        //        p12.Value = entrada.descricaoParametro5;

        //        SqlParameter p13 = new SqlParameter("@PRM_VLR_PARAM5", SqlDbType.NVarChar, 100);
        //        p13.Direction = ParameterDirection.Input;
        //        p13.Value = entrada.valorParametro5;

        //        SqlParameter p14 = new SqlParameter("@DESC_PRM_6", SqlDbType.NVarChar, 200);
        //        p14.Direction = ParameterDirection.Input;
        //        p14.Value = entrada.descricaoParametro6;

        //        SqlParameter p15 = new SqlParameter("@PRM_VLR_PARAM6", SqlDbType.NVarChar, 100);
        //        p15.Direction = ParameterDirection.Input;
        //        p15.Value = entrada.valorParametro6;

        //        SqlParameter p16 = new SqlParameter("@DESC_PRM_7", SqlDbType.NVarChar, 200);
        //        p16.Direction = ParameterDirection.Input;
        //        p16.Value = entrada.descricaoParametro7;

        //        SqlParameter p17 = new SqlParameter("@PRM_VLR_PARAM7", SqlDbType.NVarChar, 100);
        //        p17.Direction = ParameterDirection.Input;
        //        p17.Value = entrada.valorParametro7;

        //        SqlParameter p18 = new SqlParameter("@DESC_PRM_8", SqlDbType.NVarChar, 200);
        //        p18.Direction = ParameterDirection.Input;
        //        p18.Value = entrada.descricaoParametro8;

        //        SqlParameter p19 = new SqlParameter("@PRM_VLR_PARAM8", SqlDbType.NVarChar, 100);
        //        p19.Direction = ParameterDirection.Input;
        //        p19.Value = entrada.valorParametro8;

        //        SqlParameter p20 = new SqlParameter("@DESC_PRM_9", SqlDbType.NVarChar, 200);
        //        p20.Direction = ParameterDirection.Input;
        //        p20.Value = entrada.descricaoParametro9;

        //        SqlParameter p21 = new SqlParameter("@PRM_VLR_PARAM9", SqlDbType.NVarChar, 100);
        //        p21.Direction = ParameterDirection.Input;
        //        p21.Value = entrada.valorParametro9;

        //        SqlParameter p22 = new SqlParameter("@DESC_PRM_10", SqlDbType.NVarChar, 200);
        //        p22.Direction = ParameterDirection.Input;
        //        p22.Value = entrada.descricaoParametro10;

        //        SqlParameter p23 = new SqlParameter("@PRM_VLR_PARAM10", SqlDbType.NVarChar, 100);
        //        p23.Direction = ParameterDirection.Input;
        //        p23.Value = entrada.valorParametro10;

        //        SqlParameter p24 = new SqlParameter("@PRM_STAT_COD", SqlDbType.Bit);
        //        p24.Direction = ParameterDirection.Input;

        //        if (entrada.statuscodigo.HasValue && entrada.statuscodigo.Value.Equals(1))
        //        {
        //            p24.Value = 1;
        //        }
        //        else
        //        {
        //            p24.Value = 0;
        //        }

        //        SqlParameter[] parametrosInclusao = { p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24 };

        //        ConexaoPadrao().ExecuteNonQuery("PR_GT_GTEC_PARAMETROS_U_001", CommandType.StoredProcedure, ref parametrosInclusao);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (!ConexaoPadrao().transacaoAtiva)
        //        {
        //            EncerrarConexao();
        //        }
        //    }
        //}

        //public void Excluir(string nomeProcesso, string nomeParametro)
        //{
        //    try
        //    {
        //        SqlParameter p1 = new SqlParameter("@NM_PROCESSO", SqlDbType.NVarChar, 60);
        //        p1.Value = nomeProcesso;

        //        SqlParameter p2 = new SqlParameter("@NM_PARAMETRO", SqlDbType.NVarChar, 60);
        //        p2.Value = nomeParametro;

        //        SqlParameter[] parametrosInclusao = { p1, p2 };

        //        ConexaoPadrao().ExecuteNonQuery("PR_GT_GTEC_PARAMETROS_D_001", CommandType.StoredProcedure, ref parametrosInclusao);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (!ConexaoPadrao().transacaoAtiva)
        //        {
        //            EncerrarConexao();
        //        }
        //    }
        //}
    }

    public class LoginSenac
    {
        public string NM_USUARIO_NT { get; internal set; }
        public string NM_DOMINIO_NT { get; internal set; }
        public bool IN_ATIVO { get; internal set; }
    }
}