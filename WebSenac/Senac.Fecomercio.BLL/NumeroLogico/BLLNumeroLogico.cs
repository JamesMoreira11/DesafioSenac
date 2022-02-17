using Cielo.Gtec.BLL.Business.NumeroLogico.Entities;
using Cielo.Gtec.BLL.Dao.NumeroLogico;
using Cielo.Gtec.BLL.Utilities.Exceptions;
using CieloGtecDAL.DAL;
using LogviewHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cielo.Gtec.BLL.Business.NumeroLogico
{
    public class BLLNumeroLogico
    {
        /// <summary>
        /// Classe de Log
        /// </summary>
        private static LogClass logger = new LogClass();

        /// <summary>
        /// Exclui Numero Lógico na Base do GTeC
        /// </summary>
        public static bool Excluir(int numeroLogico, string numeroEstabelecimento)
        {
            logger.log(Level.INFO, "BLLNumeroLogico.Excluir", "numeroLogico[" + numeroLogico + "], numeroEstabelecimento[" + numeroEstabelecimento + "]");

            BDConexao db = BDConexao.ConexaoBDTGTC();
            bool ret = false;

            db.BeginTransaction("EXCLUIR_LOGICO");

            try
            {
                if (numeroLogico <= 0) 
                    throw new BusinessException("Parâmetro obrigatório: numeroLogico");
                if (String.IsNullOrEmpty(numeroEstabelecimento)) 
                    throw new BusinessException("Parâmetro obrigatório: numeroEstabelecimento");

                //Consulta se o Número Lógico informado Existe e Pertence ao EC informado
                DadosNumeroLogico dados=DAONumeroLogico.Consultar(numeroLogico);
                if (dados == null) 
                    throw new BusinessException("Número Lógico [" + numeroLogico + "] não localizado!");

                if (dados.NumeroEstabelecimento != numeroEstabelecimento) 
                    throw new BusinessException("Número Lógico [" + numeroLogico + "] não pertence ao EC [" + numeroEstabelecimento + "]");

                ret = DAONumeroLogico.Excluir(numeroLogico);

                db.CommitTransaction();

                logger.log(Level.INFO, "BLLNumeroLogico.Excluir", "numeroLogico[" + numeroLogico + "]. Sucesso. Retorno[" + ret.ToString() + "]");

                return ret;
            }
            catch (BusinessException buEx)
            {
                logger.log(Level.ERROR,
                           "BLLNumeroLogico.Excluir",
                           "Dados da Exceção .... [" + buEx.ToString() + "]");

                db.RollbackTransaction();
                throw;
            }
            catch (Exception ex)
            {
                logger.log(Level.ERROR,
                          "BLLNumeroLogico.Excluir",
                          "Dados da Exceção .... [" + ex.ToString() + "]");

                db.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Inclui o Número Lógico nas Bases do GTeC
        /// </summary>
        /// <param name="dadosNumeroLogico"></param>
        /// <returns></returns>
        public static bool Incluir(Entities.DadosNumeroLogico dadosNumeroLogico)
        {
            logger.log(Level.INFO, "BLLNumeroLogico.Incluir");

            //BDConexao db = BDConexao.ConexaoBDTGTC();
            bool ret = false;

            //db.BeginTransaction("INCLUIR_LOGICO");

            try
            {
                if (dadosNumeroLogico == null) throw new BusinessException("Parâmetro obrigatório: dadosNumeroLogico");
                validarDadosIncluir(dadosNumeroLogico);

                ret = DAONumeroLogico.Incluir(dadosNumeroLogico);

                //db.CommitTransaction();

                logger.log(Level.INFO, "BLLNumeroLogico.Incluir", "Sucesso. Retorno[" + ret.ToString() + "]");

                return ret;
            }
            catch (BusinessException buEx)
            {
                logger.log(Level.ERROR,
                           "BLLNumeroLogico.Incluir",
                           "Dados da Exceção .... [" + buEx.ToString() + "]");

                //db.RollbackTransaction();
                throw;
            }
            catch (Exception ex)
            {
                logger.log(Level.ERROR,
                         "BLLNumeroLogico.Incluir",
                         "Dados da Exceção .... [" + ex.ToString() + "]");

                //db.RollbackTransaction();
                throw;
            }
        }

        private static void validarDadosIncluir(Entities.DadosNumeroLogico dadosNumeroLogico)
        {
            logger.log(Level.INFO, "BLLNumeroLogico.validarDados()", "Mobile=" + (dadosNumeroLogico.DadosSolucaoMobile != null));

            logger.log(Level.INFO, "BLLNumeroLogico.validarDados()", "dadosNumeroLogico.NumeroLogico");
            if (dadosNumeroLogico.NumeroLogico <= 0)
                throw new BusinessException("Campo [dadosNumeroLogico.NumeroLogico]: preenchimento obrigatório, maior que zero.");

            logger.log(Level.INFO, "BLLNumeroLogico.validarDados()", "dadosNumeroLogico.NumeroEstabelecimento");
            if (string.IsNullOrEmpty(dadosNumeroLogico.NumeroEstabelecimento))
                throw new BusinessException("Campo [dadosNumeroLogico.NumeroEstabelecimento]: preenchimento obrigatório.");

            if (dadosNumeroLogico.DadosSolucaoMobile != null)
            {
                logger.log(Level.INFO, "BLLNumeroLogico.validarDados()", "dadosNumeroLogico.DadosSolucaoMobile.CodigoModeloSolucaoMobile");
                if (string.IsNullOrEmpty(dadosNumeroLogico.DadosSolucaoMobile.CodigoModeloSolucaoMobile))
                    throw new BusinessException("Campo [CodigoModeloSolucaoMobile]: preenchimento obrigatório.");

                logger.log(Level.INFO, "BLLNumeroLogico.validarDados()", "dadosNumeroLogico.DadosSolucaoMobile.NumeroDDD");
                if (string.IsNullOrEmpty(dadosNumeroLogico.DadosSolucaoMobile.NumeroDDD))
                    throw new BusinessException("Campo [NumeroDDD]: preenchimento obrigatório.");

                logger.log(Level.INFO, "BLLNumeroLogico.validarDados()", "dadosNumeroLogico.DadosSolucaoMobile.NumeroTelefone");
                if (string.IsNullOrEmpty(dadosNumeroLogico.DadosSolucaoMobile.NumeroTelefone))
                    throw new BusinessException("Campo [NumeroTelefone]: preenchimento obrigatório.");
            }
            else
            {
                logger.log(Level.INFO, "BLLNumeroLogico.validarDados()", "dadosNumeroLogico.CodigoModeloSolucao");
                if (string.IsNullOrEmpty(dadosNumeroLogico.CodigoModeloSolucao))
                    throw new BusinessException("Campo [dadosNumeroLogico.CodigoModeloSolucao]: preenchimento obrigatório.");
            }
        }
    }
}
