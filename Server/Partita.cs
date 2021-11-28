using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    [DataContract]
    class Partita
    {
        //Attributi
        [DataMember]
        private int Codice;
        [DataMember]
        private DateTime DataPartita;
        [DataMember]
        private string OraInzioPartita;
        [DataMember]
        private string Incontro;
        [DataMember]
        private int IDStadio;

        //Costruttore
        public Partita(int c, DateTime dp, string op, string i, int ids) { Codice = c; DataPartita = dp; OraInzioPartita = op; Incontro = i; IDStadio = ids; }

        //Getter
        public int getCodice() { return Codice; }
        public DateTime getDataPartita() { return DataPartita; }
        public string getOraInizioPartia() { return OraInzioPartita; }
        public string getIncontro() { return Incontro; }
        public int getIDStadio() { return IDStadio; }
    }
}
