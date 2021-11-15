using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class FunzioniServer
    {
		public List<Utente> SelectUtenti()
		{
			/*Torna la lista di tutti i clienti se tutto va bene, altrimenti torna null*/

			var ls = new List<Utente>();
			try
			{
				//Esegui e incapsula query
				using (var result = Connessioni.Select("SELECT * FROM Utente"))
					while (result.Read())
						ls.Add(new Utente(result.GetString("Nome"), result.GetString("Cognome"), result.GetString("Email"), result.GetString("Psw"), result.GetMySqlDateTime("DataNascita").ToString()));
			}
			catch (Exception)
			{
				Console.WriteLine("Errore nella creazione della lista dei utenti.\n");
				return null;
			}
			return ls;
		}
		public List<Admin> SelectAdmin()
		{
			/*Torna la lista di tutti i Admin se tutto va bene, altrimenti torna null*/

			var ls = new List<Admin>();
			try
			{
				//Esegui e incapsula query
				using (var result = Connessioni.Select("SELECT * FROM Admin"))
					while (result.Read())
						ls.Add(new Admin(result.GetString("Nome"), result.GetString("Cognome"), result.GetString("Email"), result.GetString("Psw")));
			}
			catch (Exception)
			{
				Console.WriteLine("Errore nella creazione della lista dei admin.\n");
				return null;
			}
			return ls;
		}
		public List<Partita> SelectPartite()
			{
				/*Torna la lista di tutti i Admin se tutto va bene, altrimenti torna null*/

				var ls = new List<Partita>();
				try
				{
				//Esegui e incapsula query
				using (var result = Connessioni.Select("SELECT * FROM Partita"))
					while (result.Read())
						ls.Add(new Partita(result.GetInt32("Codice"), DateTime.Parse(result.GetString("DataPartita")), result.GetString("OraInizioPartita"), result.GetString("Incontro"), result.GetInt32("IDStadio")));
				}
			catch (Exception)
				{
					Console.WriteLine("Errore nella creazione della lista delle partite.\n");
					return null;
				}
				return ls;
			}
		public List<Stadio> SelectStadio()
		{
			/*Torna la lista di tutti i Admin se tutto va bene, altrimenti torna null*/

			var ls = new List<Stadio>();
			try
			{
				//Esegui e incapsula query
				using (var result = Connessioni.Select("SELECT * FROM Stadio"))
					while (result.Read())
						ls.Add(new Stadio(result.GetInt32("ID"), result.GetString("Nome"), result.GetString("Citta"), result.GetString("Regione"), result.GetInt32("PostiTotali")));
			}
			catch (Exception)
			{
				Console.WriteLine("Errore nella creazione della lista dei stadii.\n");
				return null;
			}
			return ls;
		}
		public List<int> AssegnaPosti(int codicePartita, int np)
        {
			var ls = new List<int>();
			int TotalePostiOccupati = 0;
			int TotalePostiDisponibli = 0;
			int TotalePostiRimanenti = 0;

			try
			{
				//Esegui e incapsula query
				using (var result = Connessioni.Select("SELECT NumeroBiglietti FROM Prenotazione WHERE CodicePartita = " + codicePartita))
					while (result.Read())
						TotalePostiOccupati += result.GetInt32("NumeroBiglietti");
				using (var result = Connessioni.Select("SELECT Stadio.PostiTotali FROM Partita INNER JOIN Stadio ON Partita.IDStadio = Stadio.ID WHERE Codice = " + codicePartita))
					while (result.Read())
						TotalePostiDisponibli = result.GetInt32("PostiTotali");
				TotalePostiRimanenti = TotalePostiDisponibli - TotalePostiOccupati;
				if (TotalePostiRimanenti > 0)
					for (int i = 1; i <= np; i++)
						ls.Add(TotalePostiOccupati + i);


			}
			catch (Exception)
			{
				Console.WriteLine("Errore nell'inserimento dei dati della prenotazione.\n");
				return null;
			}
			return ls;


		}
	}
}
