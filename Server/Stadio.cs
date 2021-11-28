using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    [DataContract]
    class Stadio
    {
        //Attributi
        [DataMember]
        private int ID;
        [DataMember]
        private string Nome;
        [DataMember]
        private string Citta;
        [DataMember]
        private string Regione;
        [DataMember]
        private int PostiTotali;

        //Costruttore
        public Stadio(int i, string n, string c, string r, int pt) { ID = i; Nome = n; Citta = c; Regione = r; PostiTotali = pt; }

        //Getter
        public int getID() { return ID; }
        public string getNome() { return Nome; }
        public string getCitta() { return Citta; }
        public string getRegione() { return Regione; }
        public int getPostiTotali() { return PostiTotali; }
    }
}
