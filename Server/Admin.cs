using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ComponentModel;

namespace Server
{
	[DataContract]
	public class Admin
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

		//Costruttore
		public Admin(string n, string c, string e, string psw) { Nome = n; Cognome = c; Email = e; Psw = psw; }

		//Getter
		public string getNome() { return Nome; }
		public string getEmail() { return Email; }
		public string getPsw() { return Psw; }
		public string getCognome() { return Cognome; }
	}
}
