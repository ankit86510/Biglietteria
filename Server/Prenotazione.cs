using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Prenotazione
    {
        [DataMember]
        private int ID;
        [DataMember]
        private DateTime DataOraAcquisto;
        [DataMember]
        private int PrezzoTotaleBiglietto;
        [DataMember]
        private int NumeroBiglietti;
        [DataMember]
        private string NumeroPosti;
        [DataMember]
        private int CodicePartita;

        public Prenotazione(int id, DateTime d, int i, int nb, string np, int cp) { ID = i; DataOraAcquisto = d; PrezzoTotaleBiglietto = i; NumeroBiglietti = nb;  NumeroPosti = np; CodicePartita = cp; }

        //Getter
        public int getID() { return ID; }
        public DateTime getDataOraAcquisto() { return DataOraAcquisto; }
        public int getPrezzoTotaleBiglietto() { return PrezzoTotaleBiglietto; }
        public int getNumeroBiglietti() { return NumeroBiglietti; }
        public string getNumeroPosti() { return NumeroPosti; }
        public int getCodicePartita() { return CodicePartita; }
    }
}
