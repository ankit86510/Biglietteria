﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class FunzioniServer
    {
		//Ritorna la lista di tutti i clienti se la richiesta al DB va a buon fine, altrimenti ritorna null
		public List<Utente> SelectUtenti()
		{
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
		//Ritorna la lista di tutti i Admin se la richiesta al DB va a buon fine, altrimenti ritorna null
		public List<Admin> SelectAdmin()
		{
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

		//Ritorna la lista delle partite da giocare
		public List<Partita> SelectPartite()
			{
				var ls = new List<Partita>();
				try
				{
				//Esegui e incapsula query
				using (var result = Connessioni.Select("SELECT * FROM Partita"))
					while (result.Read())
						ls.Add(new Partita(result.GetInt32("Codice"), DateTime.Parse(result.GetString("DataPartita")), DateTime.Parse(result.GetString("OraInizioPartita")), result.GetString("Incontro"), result.GetInt32("IDStadio")));
				}
			catch (Exception)
				{
					Console.WriteLine("Errore nella creazione della lista delle partite.\n");
					return null;
				}
				return ls;
			}

		//Ritorna la lista degli stadi
		public List<Stadio> SelectStadio()
		{
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
				//Verifica i posti disponibili prima di consentire l'acquisto
				TotalePostiOccupati = PostiOccupati(codicePartita);
				TotalePostiDisponibli = TotalePostiStadio(codicePartita);
				TotalePostiRimanenti = TotalePostiDisponibli - TotalePostiOccupati;
				if (TotalePostiRimanenti - np >= 0)
					for (int i = 1; i <= np; i++)
						ls.Add(TotalePostiOccupati + i);
				else
					throw new InvalidOperationException("N° totale di biglietti superano il numero dei posti totali presenti allo stadio");


			}
			catch (InvalidOperationException ex)
			{
				Console.WriteLine(ex.ToString());
				return null;
			}
			return ls;


		}
		public int PostiOccupati(int codicePartita)
		{
			int TotalePostiOccupati = 0;

			try
			{
				//Esegui e incapsula query
				using (var result = Connessioni.Select("SELECT NumeroBiglietti FROM Prenotazione WHERE CodicePartita = " + codicePartita))
					while (result.Read())
						TotalePostiOccupati += result.GetInt32("NumeroBiglietti");
			}
			catch (Exception)
			{
				Console.WriteLine("Errore nella ricerca dei posti occupati");
			}
			return TotalePostiOccupati;
		}

		// Ritorna totale posti per stadio
		public int TotalePostiStadio(int codicePartita)
		{
			int TotalePostiDisponibli = 0;

			try
			{
				//Esegui e incapsula query
				using (var result = Connessioni.Select("SELECT Stadio.PostiTotali FROM Partita INNER JOIN Stadio ON Partita.IDStadio = Stadio.ID WHERE Codice = " + codicePartita))
					while (result.Read())
						TotalePostiDisponibli = result.GetInt32("PostiTotali");
			}
			catch (Exception)
			{
				Console.WriteLine("Errore nella ricerca dei posti totali");
			}
			return TotalePostiDisponibli;
		}

		//Verifica se esiste già uno stesso evento con la stessa data/ora o con le stesse squadre
		public bool VerficaConcomittanzeInsPartita(string opzione,DateTime Data, DateTime Ora, string Incontro, int IDStadio)
        {
			string[] Squadre = Incontro.Split('-');
			List<Partita> ls = SelectPartite();
			try
			{
				foreach (var Partita in ls)
				{
					if ((Partita.getIncontro().Contains(Squadre[0]) && Partita.getIncontro().Contains(Squadre[1])) || Partita.getIDStadio().Equals(IDStadio) || Partita.getIncontro().Contains(Squadre[0]) || Partita.getIncontro().Contains(Squadre[1]))
					{
						if (Partita.getDataPartita().ToString("yyyy'-'MM'-'dd").Equals(Data.ToString("yyyy'-'MM'-'dd")))
                        {
							if (opzione.Contains("Modifica"))
                            {
								var cp = Convert.ToInt32(opzione.Split(' ')[1]);
								if ((!Partita.getOraInizioPartia().ToString("HH':'mm':'ss").Equals(Ora.ToString("HH':'mm':'ss"))) && (Partita.getCodice() == cp))
									return true;
							}
							return false;
						}
					}

				}
			}
			catch (Exception)
			{
				Console.WriteLine("Errore nella verifica concomitanza");
				return false;
			}
			return true;

		}
	}
}
