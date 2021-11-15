using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
	[DataContract]
	public class Utente
	{
		//Attributi
		[DataMember]
		private string Email;
		[DataMember]
		private string Nome;
		[DataMember]
		private string Psw;
		[DataMember]
		private string Cognome;
		[DataMember]
		private string DataNascita;

		//Costruttore
		public Utente(string n, string c, string e, string pw, string dn) { Nome = n; Cognome = c; Email = e; Psw = pw; DataNascita = dn; }

		public Utente()
        {
        }

        //Getter
        public string getNome() { return Nome; }
		public string getEmail() { return Email; }
		public string getPsw() { return Psw; }
		public string getCognome() { return Cognome; }
		public string getDataNascita() { return DataNascita; }

	}
}
