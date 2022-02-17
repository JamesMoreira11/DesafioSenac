namespace Senac.Fecomercio.Common
{
    public enum OrigemChamado
    {
        SEC = 1,
        STAR = 2,
        URA = 3,
        CRM = 4,
        CRD = 5,
        SMART = 6,
        CONTROL_TOWER = 7,
        VSC = 8,
        SUA = 9,
        GRANDE_CONTA = 10,
        GTEC = 11,
        GPO = 12
    }

    public enum StatusChamado
    {
        Exception = -1,
        EDSOkGTECOk = 0,
        EDSOkGTECNaoOk = 1,
        EDSNaoOk = 2
    }

    public enum ServicoTipo
    {
        Nullable = 0,
        Instalacao = 1,
        Manutencao = 2,
        Desinstalacao = 3,
        TrocaTecnologia = 4,
        FeirasEventos = 5
    }

    public enum SituacaoGeracaoLogico : int
    {
        /// <summary>
        /// Lógico não gerado
        /// </summary>
        NaoGerado = 0,

        /// <summary>
        /// Lógico gerado pelo GTeC
        /// </summary>
        Gtec = 1,

        /// <summary>
        /// Gerado por redes, internamente
        /// </summary>
        RedesInterno = 2,

        /// <summary>
        /// Gerado por redes, externamente
        /// </summary>
        RedesExterno = 3,
    }

    /// <summary>
    /// Codigo de cada tipo de chamado. Estão separados por tipo operação e versão do equipamento.
    /// </summary>
    public enum TipoChamado
    {
        // Chave de pesquisa V32
        Instalacao_V32 = 49,
        Bloqueio_V32 = 65,
        Desbloqueio_V32 = 108,
        Exclusao_V32 = 107,
        Confirmacao_V32 = 161,
        Manutencao_V32 = 212,

        // Chave de pesquisa V30
        Instalacao_V30 = 24,
        Bloqueio_V30 = 30,
        Desbloqueio_V30 = 47,
        Exclusao_V30 = 31,
        Confirmacao_V30 = 109,
        Manutencao_V30 = 184,
        ManutencaoTEF = 504
    }

    /// <summary>
    /// Status de envio e retorno para cada mensagem enviada.
    /// </summary>
    public enum StatusProcessamentoChamado
    {
        ChamadoNaoProcessado = 0,

        //Inclusao
        InclusaoCadastroEstabelecimentoEnvio = 110,
        InclusaoCadastroEstabelecimentoRetorno = 111,
        InclusaoCadastroProdutoEnvio = 120,
        InclusaoCadastroProdutoRetorno = 121,
        InclusaoCadastroTerminalEnvio = 130,
        InclusaoCadastroTerminalRetorno = 131,
        InclusaoErro = 199,

        //Bloqueio
        BloqueioTerminalEnvio = 210,
        BloqueioTerminalRetorno = 211,
        BloqueioErro = 299,

        //Desbloqueio
        DesbloqueioTerminalEnvio = 310,
        DesbloqueioTerminalRetorno = 311,
        DesbloquearErro = 399,

        //Exclusão
        ExclusaoTerminalEnvio = 410,
        ExclusaoTerminalRetorno = 411,
        ExclusaoErro = 499,

        //Confirmação
        ConfirmacaoTerminalEnvio = 510,
        ConfirmacaoTerminalRetorno = 511,
        ConfirmacaoErro = 599,

        //Manutenção
        ManutencaoTerminalEnvio = 610,
        ManutencaoTerminalRetorno = 611,
        ManutencaoErro = 699,

        //Consulta de Terminal
        ConsultaTerminalEnvio = 710,
        ConsultaTerminalRetorno = 711,
        ConsultaTerminalErro = 799,

        //TEF
        ManutencaoTefEnvio = 810,
        ManutencaoTefRetorno = 811,
        ManutencaoTefErro = 999
    }

    public enum ETipoAcaoChamadoRedes
    {
        Bloqueio = 1,
        Desbloqueio = 2,
        Inclusao = 3,
        Exclusao = 4
    }

    public enum EAcaoMensageriaRetorno
    {
        IncluirLoja = 0,
        IncluirProduto = 1,
        IncluirTerminal = 2,
        NaoEnviarEPularMensagem = 8,
        NaoEnviarEExcluirProcessManager = 9,
        ConsultarTerminal = 10,
        ExcluirTerminal = 11
    }

    public enum WhatsAppSituacao
    {
        Atendido = 1,
        Cancelado = 2,
        Reagendado = 3
    }
}