using Senac.Fecomercio.BLL.Utilities.Exceptions;
using Senac.Fecomercio.Business;
using Senac.Fecomercio.Common;
using Senac.Fecomercio.Entities.Enums;
using Senac.Fecomercio.Entities.WebAPI.Models;
using Senac.Fecomercio.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using BusinessException = Senac.Fecomercio.BLL.Utilities.Exceptions.BusinessException;

namespace Senac.Fecomercio.WebApi.Controllers
{
    [RoutePrefix("api/colaborador")]
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class OperadorController : ApiController
    {
        #region MetodosRotas
        [AllowAnonymous]
        [Route("consultaFolhaPonto")]
        [AcceptVerbs("POST")]
        public async Task<HttpResponseMessage> AtualizarStatus([FromBody]RequestColaboradorFolhaPonto data)
        {
            HttpResponseMessage retorno = null;

            int codColaborador = 0;
            RegistroPonto oRegistroPonto = null;

            try
            {
                if (data.IsNull())
                {
                    throw new ArgumentException("O parâmetro de entrada 'data' não foi enviado!");
                }

                #region ValidarEntrada
                if (data.colaboradorID == null)
                {
                    throw new ArgumentException("Parâmetro ID do Colaborador não fornecido");
                }

                codColaborador = 0;

                if (int.TryParse(data.colaboradorID, out codColaborador) == false)
                {
                    throw new ArgumentException("Parâmetro ID do Colaborador precisa ser numerico");
                }

                if (codColaborador < 1)
                {
                    throw new ArgumentException("Parâmetro ID do Colaborador precisa ser maior que zeros");
                }

                #endregion

                Logger.LogInfo("OperadorController > Consultar Folha Ponto > codigo: '{0}' - Inciando consulta".ToFormat(codColaborador));

                oRegistroPonto = new RegistroPonto();
                oRegistroPonto.ConsultaRegistroPonto(codColaborador);

                retorno = Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (ArgumentException bArgEx)
            {
                if (data.IsNotNull())
                {
                    Logger.LogWarn("OperadorController > Consultar Folha Ponto > data: '{0}' - Erro de Argumentos: '{1}'".ToFormat(data.JsonSerializeObject(), bArgEx.GetAllErrorDetail()));
                }
                else
                {
                    Logger.LogWarn("OperadorController > Consultar Folha Ponto - Erro de Argumentos: '{0}'".ToFormat(bArgEx.GetAllErrorDetail()));
                }

                HttpError err = new HttpError("Ocorreu um erro de validação : '{0}'".ToFormat(bArgEx.Message));
                retorno = Request.CreateResponse(HttpStatusCode.BadRequest, err);
            }
            catch (BusinessException bEx)
            {
                if (data.IsNotNull())
                {
                    Logger.LogWarn("OperadorController > Consultar Folha Ponto > data: '{0}' - Erro de Negócio: '{1}'".ToFormat(data.JsonSerializeObject(), bEx.GetAllErrorDetail()));
                }
                else
                {
                    Logger.LogWarn("OperadorController > Consultar Folha Ponto - Erro de Negócio: '{0}'".ToFormat(bEx.GetAllErrorDetail()));
                }

                HttpError err = new HttpError("Ocorreu um erro de validação : '{0}'".ToFormat(bEx.Message));
                retorno = Request.CreateResponse((HttpStatusCode)422, err);
            }

            catch (ItemNaoEncontradoException bEx)
            {
                if (data.IsNotNull())
                {
                    Logger.LogWarn("OperadorController > Consultar Folha Ponto > data: '{0}' - Erro de Negócio: '{1}'".ToFormat(data.JsonSerializeObject(), bEx.GetAllErrorDetail()));
                }
                else
                {
                    Logger.LogWarn("OperadorController > Consultar Folha Ponto - Erro de Negócio: '{0}'".ToFormat(bEx.GetAllErrorDetail()));
                }

                HttpError err = new HttpError("Ocorreu um erro de validação : '{0}'".ToFormat(bEx.Message));
                retorno = Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            catch (Exception ex)
            {
                if (data.IsNotNull())
                {
                    Logger.LogError("OperadorController > Consultar Folha Ponto > data: '{0}' - Erro: '{1}'".ToFormat(data.JsonSerializeObject(), ex.GetAllErrorDetail()));
                }
                else
                {
                    Logger.LogError("OperadorController > Consultar Folha Ponto - Erro: '{0}'".ToFormat(ex.GetAllErrorDetail()));
                }

                HttpError err = new HttpError("Ocorreu um erro : '{0}'".ToFormat(ex.Message));
                retorno = Request.CreateResponse(HttpStatusCode.InternalServerError, err);
            }
            finally
            {

            }

            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return retorno;
            });
        }
        #endregion


    [AllowAnonymous]
    [Route("registrarponto")]
    [AcceptVerbs("POST")]
    public async Task<HttpResponseMessage> registrarponto([FromBody]RequestColaboradorRegistrarPonto data)
    {
        HttpResponseMessage retorno = null;

        int codColaborador = 0;
        string nomColaborador = "";
        DateTime datRegistro = DateTime.Now;
        string indLancamento = "E";
        RegistroPonto oRegistroPonto = null;

        try
        {
            if (data.IsNull())
            {
                throw new ArgumentException("O parâmetro de entrada 'data' não foi enviado!");
            }

            if (data.colaboradorID == null)
            {
                throw new ArgumentException("Parâmetro ID do Colaborador não fornecido");
            }

            if (data.nomeColaborador == null)
            {
                throw new ArgumentException("Parâmetro Nome do Colaborador não fornecido");
            }

            if (data.dataRegistro == null)
            {
                throw new ArgumentException("Parâmetro Data Lançamento não fornecido");
            }

            if (data.indicadorRegistro == null)
            {
                throw new ArgumentException("Parâmetro Indicador de Lançamento não fornecido");
            }

            codColaborador = 0;

            if (int.TryParse(data.colaboradorID, out codColaborador) == false)
            {
                throw new ArgumentException("Parâmetro ID do Colaborador precisa ser numerico");
            }

            if (codColaborador < 1)
            {
                throw new ArgumentException("Parâmetro ID do Colaborador precisa ser maior que zeros");
            }

            if (DateTime.TryParse(data.dataRegistro, out datRegistro) == false)
            {
                throw new ArgumentException("Parâmetro Data Lançamento precisa ser uma data válida");
            }

            if (data.indicadorRegistro != "E" || data.indicadorRegistro != "S")
            {
                throw new ArgumentException("Parâmetro Indicador de Lançamento precisa ser E=Entrada ou S=Saida");
            }

            Logger.LogInfo("OperadorController > Registrar Ponto > ColaboradorID: '{0}', NomeColaborador: '{1}', DataLancamento: '{2}', Indicador: '{3}' - Inciando atualização".ToFormat(codColaborador, nomColaborador, datRegistro, indLancamento));

            string urlBase = Extension.GetValueConfig("URL_SERVICO_BASE", true);

            #region RegistrarPonto
            oRegistroPonto = new RegistroPonto();
            oRegistroPonto.CadastrarRegistroPonto(codColaborador, nomColaborador, datRegistro, indLancamento);
            #endregion

            retorno = Request.CreateResponse(HttpStatusCode.Accepted);
        }
        catch (ArgumentException bArgEx)
        {
            if (data.IsNotNull())
            {
                Logger.LogWarn("OperadorController > Registrar Ponto > data: '{0}' - Erro de Argumentos: '{1}'".ToFormat(data.JsonSerializeObject(), bArgEx.GetAllErrorDetail()));
            }
            else
            {
                Logger.LogWarn("OperadorController > Registrar Ponto - Erro de Argumentos: '{0}'".ToFormat(bArgEx.GetAllErrorDetail()));
            }

            HttpError err = new HttpError("Ocorreu um erro de validação : '{0}'".ToFormat(bArgEx.Message));
            retorno = Request.CreateResponse(HttpStatusCode.BadRequest, err);
        }
        catch (BusinessException bEx)
        {
            if (data.IsNotNull())
            {
                Logger.LogWarn("OperadorController > Registrar Ponto > data: '{0}' - Erro de Negócio: '{1}'".ToFormat(data.JsonSerializeObject(), bEx.GetAllErrorDetail()));
            }
            else
            {
                Logger.LogWarn("OperadorController > Registrar Ponto - Erro de Negócio: '{0}'".ToFormat(bEx.GetAllErrorDetail()));
            }

            HttpError err = new HttpError("Ocorreu um erro de validação : '{0}'".ToFormat(bEx.Message));
            retorno = Request.CreateResponse((HttpStatusCode)422, err);
        }
            catch (ItemNaoEncontradoException IEx)
            {
                if (data.IsNullOrEmpty())
                {
                    Logger.LogWarn("OperadorController > Registrar Ponto > data: '{0}' - Erro de Negócio: '{1}'".ToFormat(data.JsonSerializeObject(), IEx.GetAllErrorDetail()));
                }
                else
                {
                    Logger.LogWarn("OperadorController > Registrar Ponto - Erro de Negócio: '{0}'".ToFormat(IEx.GetAllErrorDetail()));
                }

                HttpError err = new HttpError("Ocorreu um erro de validação : '{0}'".ToFormat(IEx.Message));
                retorno = Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            catch (Exception ex)
        {
            if (data.IsNotNull())
            {
                Logger.LogError("OperadorController > Registrar Ponto > data: '{0}' - Erro: '{1}'".ToFormat(data.JsonSerializeObject(), ex.GetAllErrorDetail()));
            }
            else
            {
                Logger.LogError("OperadorController > Registrar Ponto - Erro: '{0}'".ToFormat(ex.GetAllErrorDetail()));
            }

            HttpError err = new HttpError("Ocorreu um erro no GTEC: '{0}'".ToFormat(ex.Message));
            retorno = Request.CreateResponse(HttpStatusCode.InternalServerError, err);
        }
        finally
        {

        }

        return await Task<HttpResponseMessage>.Factory.StartNew(() =>
        {
            return retorno;
        });
    }
    }
}