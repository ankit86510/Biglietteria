﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Client.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/LogInUtente", ReplyAction="http://tempuri.org/IService1/LogInUtenteResponse")]
        Server.Utente LogInUtente(string User, string Password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/LogInUtente", ReplyAction="http://tempuri.org/IService1/LogInUtenteResponse")]
        System.Threading.Tasks.Task<Server.Utente> LogInUtenteAsync(string User, string Password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/LogInAdmin", ReplyAction="http://tempuri.org/IService1/LogInAdminResponse")]
        Server.Admin LogInAdmin(string User, string Password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/LogInAdmin", ReplyAction="http://tempuri.org/IService1/LogInAdminResponse")]
        System.Threading.Tasks.Task<Server.Admin> LogInAdminAsync(string User, string Password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/RegistrazioneUtente", ReplyAction="http://tempuri.org/IService1/RegistrazioneUtenteResponse")]
        bool RegistrazioneUtente(string Nome, string Cognome, string Email, string Psw, string DataNascita);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/RegistrazioneUtente", ReplyAction="http://tempuri.org/IService1/RegistrazioneUtenteResponse")]
        System.Threading.Tasks.Task<bool> RegistrazioneUtenteAsync(string Nome, string Cognome, string Email, string Psw, string DataNascita);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/ListaPartite", ReplyAction="http://tempuri.org/IService1/ListaPartiteResponse")]
        System.Data.DataTable ListaPartite();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/ListaPartite", ReplyAction="http://tempuri.org/IService1/ListaPartiteResponse")]
        System.Threading.Tasks.Task<System.Data.DataTable> ListaPartiteAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/ListaPrenotazioni", ReplyAction="http://tempuri.org/IService1/ListaPrenotazioniResponse")]
        System.Data.DataTable ListaPrenotazioni(string s);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/ListaPrenotazioni", ReplyAction="http://tempuri.org/IService1/ListaPrenotazioniResponse")]
        System.Threading.Tasks.Task<System.Data.DataTable> ListaPrenotazioniAsync(string s);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/CarrelloTabelle", ReplyAction="http://tempuri.org/IService1/CarrelloTabelleResponse")]
        System.Data.DataTable CarrelloTabelle(int v, decimal q);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/CarrelloTabelle", ReplyAction="http://tempuri.org/IService1/CarrelloTabelleResponse")]
        System.Threading.Tasks.Task<System.Data.DataTable> CarrelloTabelleAsync(int v, decimal q);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/RicercaPer", ReplyAction="http://tempuri.org/IService1/RicercaPerResponse")]
        System.Data.DataTable[] RicercaPer(string scelta, string value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/RicercaPer", ReplyAction="http://tempuri.org/IService1/RicercaPerResponse")]
        System.Threading.Tasks.Task<System.Data.DataTable[]> RicercaPerAsync(string scelta, string value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/RegistraPrenotazione", ReplyAction="http://tempuri.org/IService1/RegistraPrenotazioneResponse")]
        bool RegistraPrenotazione(Server.Utente u, System.Data.DataTable dt);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/RegistraPrenotazione", ReplyAction="http://tempuri.org/IService1/RegistraPrenotazioneResponse")]
        System.Threading.Tasks.Task<bool> RegistraPrenotazioneAsync(Server.Utente u, System.Data.DataTable dt);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : Client.ServiceReference1.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<Client.ServiceReference1.IService1>, Client.ServiceReference1.IService1 {
        
        public Service1Client() {
        }
        
        public Service1Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service1Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Server.Utente LogInUtente(string User, string Password) {
            return base.Channel.LogInUtente(User, Password);
        }
        
        public System.Threading.Tasks.Task<Server.Utente> LogInUtenteAsync(string User, string Password) {
            return base.Channel.LogInUtenteAsync(User, Password);
        }
        
        public Server.Admin LogInAdmin(string User, string Password) {
            return base.Channel.LogInAdmin(User, Password);
        }
        
        public System.Threading.Tasks.Task<Server.Admin> LogInAdminAsync(string User, string Password) {
            return base.Channel.LogInAdminAsync(User, Password);
        }
        
        public bool RegistrazioneUtente(string Nome, string Cognome, string Email, string Psw, string DataNascita) {
            return base.Channel.RegistrazioneUtente(Nome, Cognome, Email, Psw, DataNascita);
        }
        
        public System.Threading.Tasks.Task<bool> RegistrazioneUtenteAsync(string Nome, string Cognome, string Email, string Psw, string DataNascita) {
            return base.Channel.RegistrazioneUtenteAsync(Nome, Cognome, Email, Psw, DataNascita);
        }
        
        public System.Data.DataTable ListaPartite() {
            return base.Channel.ListaPartite();
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> ListaPartiteAsync() {
            return base.Channel.ListaPartiteAsync();
        }
        
        public System.Data.DataTable ListaPrenotazioni(string s) {
            return base.Channel.ListaPrenotazioni(s);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> ListaPrenotazioniAsync(string s) {
            return base.Channel.ListaPrenotazioniAsync(s);
        }
        
        public System.Data.DataTable CarrelloTabelle(int v, decimal q) {
            return base.Channel.CarrelloTabelle(v, q);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> CarrelloTabelleAsync(int v, decimal q) {
            return base.Channel.CarrelloTabelleAsync(v, q);
        }
        
        public System.Data.DataTable[] RicercaPer(string scelta, string value) {
            return base.Channel.RicercaPer(scelta, value);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable[]> RicercaPerAsync(string scelta, string value) {
            return base.Channel.RicercaPerAsync(scelta, value);
        }
        
        public bool RegistraPrenotazione(Server.Utente u, System.Data.DataTable dt) {
            return base.Channel.RegistraPrenotazione(u, dt);
        }
        
        public System.Threading.Tasks.Task<bool> RegistraPrenotazioneAsync(Server.Utente u, System.Data.DataTable dt) {
            return base.Channel.RegistraPrenotazioneAsync(u, dt);
        }
    }
}