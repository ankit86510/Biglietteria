using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Server
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        Utente LogInUtente(string User, string Password);
        [OperationContract]
        Admin LogInAdmin(string User, string Password);
        [OperationContract]
        bool RegistrazioneUtente(string Nome, string Cognome, string Email, string Psw, string DataNascita);
        [OperationContract]
        DataTable ListaPartite();
        [OperationContract]
        DataTable ListaUtenti();
        [OperationContract]
        DataTable ListaPrenotazioni(string s);
        [OperationContract]
        DataTable CarrelloTabelle(int v, decimal q);
        [OperationContract]
        List<DataTable> RicercaPer(string scelta, string value);
        [OperationContract]
        bool RegistraPrenotazione(Utente u, DataTable dt);

    }
}
