using Senac.Fecomercio.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Senac.Fecomercio.Business.Base
{
    public abstract class ServiceBaseBL : ServiceBase
    {
        #region Propriedades
        protected DateTime DataInicioProcessamento { get; set; }

        protected Thread ThrPrincipal { get; set; }

        protected List<DateTime> TotalErrosPorDia { get; set; }
        #endregion

        #region Metodos
        protected void ExpurgoErroSomenteDia()
        {
            if (TotalErrosPorDia == null)
            {
                TotalErrosPorDia = new List<DateTime>();
            }
            else if (TotalErrosPorDia != null && TotalErrosPorDia.Count > 0)
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

        protected bool PodeAdicionarThread(int threadID)
        {
            bool ret = true;
            //Verifica se já existe a Thread aberta
            IEnumerable<Thread> listaThread = TrackedThread.ThreadList;

            if (listaThread != null && listaThread.Count() > 0)
            {
                foreach (var itemThread in listaThread)
                {
                    if (itemThread.Name.Equals(threadID.ToString()) && itemThread.IsAlive)
                    {
                        ret = false;
                        break;
                    }
                    else if (itemThread.Name.Equals(threadID.ToString()) && !itemThread.IsAlive)
                    {
                        //Kill Thread e remove da lista
                        Logger.LogInfo(String.Format("Thread '{0}' indisponível e encerrada para iniciar novamente.", threadID));
                        itemThread.Abort();
                        itemThread.Name = string.Empty;
                        break;
                    }
                }
            }

            return ret;
        }
        #endregion
    }
}
