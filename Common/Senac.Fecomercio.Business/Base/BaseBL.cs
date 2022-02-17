using Senac.Fecomercio.Data.Base;

namespace Senac.Fecomercio.Business.Base
{
    public class BaseBL
    {
        public void EncerrarConexaoAberta()
        {
            BaseDAL.EncerrarTodasConexao();
        }
    }
}
