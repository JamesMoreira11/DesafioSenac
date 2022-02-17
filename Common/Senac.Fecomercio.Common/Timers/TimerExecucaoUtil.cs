using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Senac.Fecomercio.Common.Timers
{
    public abstract class TimerExecucaoUtil
    {
        #region Propriedades
        protected List<DateTime> TotalErrosPorDia { get; set; }
        protected DateTime? DataProximaGeracao { get; set; }
        protected string ChaveIntervalo { get; set; }
        protected bool ManterServicoExecutando { get; set; }

        protected int IntervaloGeracaoMinuto
        {
            get
            {
                int ret = 30;

                string intervaloConf = Extension.GetValueConfig(ChaveIntervalo, true);

                if (intervaloConf.IsInt())
                {
                    ret = intervaloConf.ToInt();
                }

                return ret;
            }
        }
        #endregion

        #region MetodosAbstratos
        protected abstract void ExecutarRotina(DateTime dataExecucao);
        #endregion

        #region Metodos
        public void Processar()
        {
            DateTime dataAtual = DateTime.MinValue;
            ManterServicoExecutando = true;

            while (ManterServicoExecutando)
            {
                try
                {
                    dataAtual = DateTime.Now;
                    dataAtual = new DateTime(dataAtual.Year, dataAtual.Month, dataAtual.Day, dataAtual.Hour, dataAtual.Minute, 0, 0);

                    if (PodeExecutar(dataAtual))
                    {
                        Logger.LogInfo("Executando a rotina em: '{0}'".ToFormat(dataAtual.ToString("HH:mm:ss")));
                        ExecutarRotina(dataAtual);
                        CalcularProximaExecucao(dataAtual);
                        Logger.LogInfo("Rotina executada com êxito!!! A próxima execução da rotina em: '{0}'".ToFormat(DataProximaGeracao.Value.ToString("HH:mm:ss")));
                    }
                }
                catch (Exception exInt)
                {
                    Logger.LogError("Ocorreu um erro na geração da rotina. Erro: '{0}'".ToFormat(exInt.GetAllErrorDetail()));

                    try
                    {
                        ExpurgoErroSomenteDia();

                        int totalErroHoje = GetTotalErroHoje();

                        Logger.LogInfo("Total de erros do dia '{0}'", totalErroHoje);

                        if (totalErroHoje < 100)
                        {
                            AddErroHoje();
                        }
                        else
                        {
                            throw exInt;
                        }
                    }
                    catch { }
                }

                Thread.Sleep(60000);
            }
        }

        protected bool PodeExecutar(DateTime dataAtual)
        {
            bool ret = false;

            if (System.Diagnostics.Debugger.IsAttached)
            {
                ret = true;
            }
            else
            {
                if (!DataProximaGeracao.HasValue)
                {
                    CalcularProximaExecucao(dataAtual);
                }

                if (dataAtual >= DataProximaGeracao.Value)
                {
                    ret = true;
                }
            }

            return ret;
        }

        protected void CalcularProximaExecucao(DateTime dataAtual)
        {
            if (!DataProximaGeracao.HasValue)
            {
                DateTime? ultimaGeracao = null;
                while (true)
                {
                    if (!ultimaGeracao.HasValue && !DataProximaGeracao.HasValue)
                    {
                        ultimaGeracao = new DateTime(dataAtual.Year, dataAtual.Month, dataAtual.Day, dataAtual.Hour, 0, 0);
                        ultimaGeracao = ultimaGeracao.Value.AddMinutes((IntervaloGeracaoMinuto * -1));
                    }

                    /* SE POR VENTURA O SERVIÇO FOR EXECUTADO ENTRE O PERÍODO DE GERAÇÃO (INTERVALO CONFIG) ELE CONSIDERA NA METADE DO TEMPO. EX. EXECUTA DE 30 EM 30 MINUTOS.
                     * SE O SERVIÇO FOR LIGADO ÁS 11:15 ELE EXECUTA CASO FOR SUPERIOR ELE AGUARDA A PRÓXIMA JANELA
                     */
                    if (dataAtual > ultimaGeracao.Value.AddMinutes((IntervaloGeracaoMinuto / 2)))
                    {
                        ultimaGeracao = ultimaGeracao.Value.AddMinutes(IntervaloGeracaoMinuto);
                    }
                    else
                    {
                        break;
                    }
                }

                DataProximaGeracao = ultimaGeracao.Value;
            }
            else
            {
                while (true)
                {
                    if (DataProximaGeracao.Value <= dataAtual)
                    {
                        DataProximaGeracao = DataProximaGeracao.Value.AddMinutes(IntervaloGeracaoMinuto);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        protected void ExpurgoErroSomenteDia()
        {
            if (TotalErrosPorDia.IsNull())
            {
                TotalErrosPorDia = new List<DateTime>();
            }
            else if (TotalErrosPorDia.IsNotNull() && TotalErrosPorDia.Count > 0)
            {
                List<DateTime> listaNova = new List<DateTime>();
                listaNova.AddRange((from a in TotalErrosPorDia
                                    where a.Date.Equals(DateTime.Now.Date)
                                    select a).ToList());
                TotalErrosPorDia.Clear();
                TotalErrosPorDia.AddRange(listaNova);
            }
        }

        protected int GetTotalErroHoje()
        {
            int ret = 0;

            if (TotalErrosPorDia == null)
            {
                TotalErrosPorDia = new List<DateTime>();
            }
            else
            {
                List<DateTime> listaHoje = (from a in TotalErrosPorDia
                                            where a.Date.Equals(DateTime.Now.Date)
                                            select a).ToList();

                ret = listaHoje.Count;
            }

            return ret;
        }

        protected void AddErroHoje()
        {
            if (TotalErrosPorDia == null)
            {
                TotalErrosPorDia = new List<DateTime>();
            }

            TotalErrosPorDia.Add(DateTime.Now);
        }
        #endregion
    }
}