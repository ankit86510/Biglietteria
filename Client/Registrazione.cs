﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Registrazione : Form
    {
        public ServiceReference1.Service1Client client;

        //Costruttore
        public Registrazione(ServiceReference1.Service1Client Client)
        {
            client = Client;
            InitializeComponent();
        }

        //Gestione evento on-click per il pulasnte "Utente registrato? Clicca qui"
        //Rinvia a form di login chiudendo finestra di registrazione
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            var login = new LogIn(client);
            login.Closed += (s, args) => this.Close();
            login.Show();
        }

        //Gestione evento on-click per il pulasnte Registrati Ora
        /*Verifica che i campi di registrazioni siano pieni, converte la data nel formato del DB
         * e chiama la funzione per Registrare l'utente passando i dati inseriti*/
        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine(dateTimePicker1.Text);
            if (txtNome.Text != string.Empty && txtCognome.Text != string.Empty && txtEmail.Text != string.Empty && txtPassword.Text != string.Empty && dateTimePicker1.Text != string.Empty)
            {
                try
                {
                    DateTime dn = Convert.ToDateTime(dateTimePicker1.Text);
                    if (client.RegistrazioneUtente(txtNome.Text, txtCognome.Text, txtEmail.Text, txtPassword.Text, dn.ToString("yyyy-MM-dd")))
                    {
                        this.Hide();
                        var login = new LogIn(client);
                        login.Closed += (s, args) => this.Close();
                        login.Show();
                        MessageBox.Show("Utente registrato corretamente", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    else
                        MessageBox.Show("Utente con la Email già presente nel database! Inserisci una email diversa ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                catch (Exception)
                {
                    MessageBox.Show("Qualcosa è andato storto!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter value in all field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
    }
}
