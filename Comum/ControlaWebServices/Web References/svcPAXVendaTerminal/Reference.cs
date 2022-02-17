﻿//------------------------------------------------------------------------------
// <auto-generated>
//     O código foi gerado por uma ferramenta.
//     Versão de Tempo de Execução:4.0.30319.42000
//
//     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
//     o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// Este código-fonte foi gerado automaticamente por Microsoft.VSDesigner, Versão 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace Senac.Fecomercio.ControlaWebServices.svcPAXVendaTerminal {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    using Senac.Fecomercio.ControlaWebServices.Base;


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3761.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="VendaTerminal_out_SIBinding", Namespace="urn:cielo:gtec:pax:VendaTerminal")]
    public partial class Gtec_HBS_VendaTerminal_out_SI : ServiceReferenceBase {
        
        private System.Threading.SendOrPostCallback VendaTerminal_out_SIOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public Gtec_HBS_VendaTerminal_out_SI() {
            this.Url = global::Senac.Fecomercio.ControlaWebServices.Properties.Settings.Default.Cielo_Gtec_ControlaWebServices_svcPAXVendaTerminal_v2_Gtec_HBS_VendaTerminal_out_SI;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event VendaTerminal_out_SICompletedEventHandler VendaTerminal_out_SICompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://sap.com/xi/WebService/soap1.1", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("VendaTerminal_Return_MT", Namespace="urn:cielo:gtec:pax:VendaTerminal")]
        public Header_Return_VendaTerminal_DT VendaTerminal_out_SI([System.Xml.Serialization.XmlElementAttribute(Namespace="urn:cielo:gtec:pax:VendaTerminal")] Header_VendaTerminal_DT VendaTerminal_MT) {
            object[] results = this.Invoke("VendaTerminal_out_SI", new object[] {
                        VendaTerminal_MT});
            return ((Header_Return_VendaTerminal_DT)(results[0]));
        }
        
        /// <remarks/>
        public void VendaTerminal_out_SIAsync(Header_VendaTerminal_DT VendaTerminal_MT) {
            this.VendaTerminal_out_SIAsync(VendaTerminal_MT, null);
        }
        
        /// <remarks/>
        public void VendaTerminal_out_SIAsync(Header_VendaTerminal_DT VendaTerminal_MT, object userState) {
            if ((this.VendaTerminal_out_SIOperationCompleted == null)) {
                this.VendaTerminal_out_SIOperationCompleted = new System.Threading.SendOrPostCallback(this.OnVendaTerminal_out_SIOperationCompleted);
            }
            this.InvokeAsync("VendaTerminal_out_SI", new object[] {
                        VendaTerminal_MT}, this.VendaTerminal_out_SIOperationCompleted, userState);
        }
        
        private void OnVendaTerminal_out_SIOperationCompleted(object arg) {
            if ((this.VendaTerminal_out_SICompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.VendaTerminal_out_SICompleted(this, new VendaTerminal_out_SICompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:cielo:gtec:pax:VendaTerminal")]
    public partial class Header_VendaTerminal_DT : IEntradaServico {
        
        private string codigoProtocoloField;
        
        private string numeroPedidoField;
        
        private Item_VendaTerminal_DT[] serialField;
        
        private string celularField;
        
        private string emailField;
        
        private string inscricaoEstadualField;
        
        private string inscricaoMunicipalField;
        
        private string cpfCnpjField;
        
        private string logradouroField;
        
        private string numeroLogradouroField;
        
        private string complementoField;
        
        private string bairroField;
        
        private string estadoField;
        
        private string cepField;
        
        private string municipioField;
        
        private string paisField;
        
        private string codigoClienteField;
        
        private bool contribuinteField;
        
        private bool contribuinteFieldSpecified;
        
        private string documentoField;
        
        private string nomeField;
        
        private string nomeReduzidoField;
        
        private string sexoField;
        
        private string telefoneField;
        
        private string tipoPessoaField;
        
        private string valor_declaradoField;
        
        private string credencialField;
        
        private string identificacaoField;
        
        private string senhaField;
        
        private string cnpjField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CodigoProtocolo {
            get {
                return this.codigoProtocoloField;
            }
            set {
                this.codigoProtocoloField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string NumeroPedido {
            get {
                return this.numeroPedidoField;
            }
            set {
                this.numeroPedidoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Serial", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public Item_VendaTerminal_DT[] Serial {
            get {
                return this.serialField;
            }
            set {
                this.serialField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Celular {
            get {
                return this.celularField;
            }
            set {
                this.celularField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Email {
            get {
                return this.emailField;
            }
            set {
                this.emailField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string InscricaoEstadual {
            get {
                return this.inscricaoEstadualField;
            }
            set {
                this.inscricaoEstadualField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string InscricaoMunicipal {
            get {
                return this.inscricaoMunicipalField;
            }
            set {
                this.inscricaoMunicipalField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CpfCnpj {
            get {
                return this.cpfCnpjField;
            }
            set {
                this.cpfCnpjField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Logradouro {
            get {
                return this.logradouroField;
            }
            set {
                this.logradouroField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string NumeroLogradouro {
            get {
                return this.numeroLogradouroField;
            }
            set {
                this.numeroLogradouroField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Complemento {
            get {
                return this.complementoField;
            }
            set {
                this.complementoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Bairro {
            get {
                return this.bairroField;
            }
            set {
                this.bairroField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Estado {
            get {
                return this.estadoField;
            }
            set {
                this.estadoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Cep {
            get {
                return this.cepField;
            }
            set {
                this.cepField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Municipio {
            get {
                return this.municipioField;
            }
            set {
                this.municipioField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Pais {
            get {
                return this.paisField;
            }
            set {
                this.paisField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CodigoCliente {
            get {
                return this.codigoClienteField;
            }
            set {
                this.codigoClienteField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool Contribuinte {
            get {
                return this.contribuinteField;
            }
            set {
                this.contribuinteField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ContribuinteSpecified {
            get {
                return this.contribuinteFieldSpecified;
            }
            set {
                this.contribuinteFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Documento {
            get {
                return this.documentoField;
            }
            set {
                this.documentoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Nome {
            get {
                return this.nomeField;
            }
            set {
                this.nomeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string NomeReduzido {
            get {
                return this.nomeReduzidoField;
            }
            set {
                this.nomeReduzidoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Sexo {
            get {
                return this.sexoField;
            }
            set {
                this.sexoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Telefone {
            get {
                return this.telefoneField;
            }
            set {
                this.telefoneField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string TipoPessoa {
            get {
                return this.tipoPessoaField;
            }
            set {
                this.tipoPessoaField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string valor_declarado {
            get {
                return this.valor_declaradoField;
            }
            set {
                this.valor_declaradoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Credencial {
            get {
                return this.credencialField;
            }
            set {
                this.credencialField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Identificacao {
            get {
                return this.identificacaoField;
            }
            set {
                this.identificacaoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string senha {
            get {
                return this.senhaField;
            }
            set {
                this.senhaField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string cnpj {
            get {
                return this.cnpjField;
            }
            set {
                this.cnpjField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:cielo:gtec:pax:VendaTerminal")]
    public partial class Item_VendaTerminal_DT {
        
        private string codigoAcessoField;
        
        private string logicoField;
        
        private string numeroSerieField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CodigoAcesso {
            get {
                return this.codigoAcessoField;
            }
            set {
                this.codigoAcessoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Logico {
            get {
                return this.logicoField;
            }
            set {
                this.logicoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string NumeroSerie {
            get {
                return this.numeroSerieField;
            }
            set {
                this.numeroSerieField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:cielo:gtec:pax:VendaTerminal")]
    public partial class Item_Return_VendaTermial_DT {
        
        private string descricaoField;
        
        private string serieField;
        
        private bool sucessoField;
        
        private bool sucessoFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Descricao {
            get {
                return this.descricaoField;
            }
            set {
                this.descricaoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Serie {
            get {
                return this.serieField;
            }
            set {
                this.serieField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool Sucesso {
            get {
                return this.sucessoField;
            }
            set {
                this.sucessoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool SucessoSpecified {
            get {
                return this.sucessoFieldSpecified;
            }
            set {
                this.sucessoFieldSpecified = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:cielo:gtec:pax:VendaTerminal")]
    public partial class Header_Return_VendaTerminal_DT : ISaidaServico {
        
        private string codigoObjetoField;
        
        private string codigoRetornoField;
        
        private string descricaoRetornoField;
        
        private Item_Return_VendaTermial_DT[] itensReversaField;
        
        private string protocoloReversaField;
        
        private bool sucessoField;
        
        private bool sucessoFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CodigoObjeto {
            get {
                return this.codigoObjetoField;
            }
            set {
                this.codigoObjetoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CodigoRetorno {
            get {
                return this.codigoRetornoField;
            }
            set {
                this.codigoRetornoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DescricaoRetorno {
            get {
                return this.descricaoRetornoField;
            }
            set {
                this.descricaoRetornoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItensReversa", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public Item_Return_VendaTermial_DT[] ItensReversa {
            get {
                return this.itensReversaField;
            }
            set {
                this.itensReversaField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ProtocoloReversa {
            get {
                return this.protocoloReversaField;
            }
            set {
                this.protocoloReversaField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool Sucesso {
            get {
                return this.sucessoField;
            }
            set {
                this.sucessoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool SucessoSpecified {
            get {
                return this.sucessoFieldSpecified;
            }
            set {
                this.sucessoFieldSpecified = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3761.0")]
    public delegate void VendaTerminal_out_SICompletedEventHandler(object sender, VendaTerminal_out_SICompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3761.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class VendaTerminal_out_SICompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal VendaTerminal_out_SICompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public Header_Return_VendaTerminal_DT Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((Header_Return_VendaTerminal_DT)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591