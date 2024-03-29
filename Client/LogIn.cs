﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Client.ServiceReference1;

namespace Client
{
    public partial class LogIn : Form
    {
        ServiceReference1.Service1Client client;

        //Costruttore
        public LogIn( ServiceReference1.Service1Client Client)
        {
            client = Client;
            InitializeComponent();
        }

        //Gestione l'evento on-click per il pulsante LOGIN
        //Verifica se le credenziali inserite sono admin o user e, se corrette, mostra la rispettiva Home
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtemail.Text != string.Empty && txtpsw.Text != string.Empty)
                {
                    if (client.LogInAdmin(txtemail.Text, txtpsw.Text) != null)
                    {
                        Server.Admin a = client.LogInAdmin(txtemail.Text, txtpsw.Text);
                        this.Hide();
                        var admin = new HomeAdmin(client , a);
                        admin.Closed += (s, args) => this.Close();
                        admin.Show();
                        MessageBox.Show("Benvenuto/a " + a.getNome() + " " + a.getCognome() + "\r\n" + 
                            "Admin Succesfully loged in", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (client.LogInUtente(txtemail.Text, txtpsw.Text) != null)
                    {
                        Server.Utente u = client.LogInUtente(txtemail.Text, txtpsw.Text);
                        this.Hide();
                        var utente = new HomeUtente(client, u);
                        utente.Closed += (s, args) => this.Close();
                        utente.Show();
                        MessageBox.Show("Benvenuto/a " + u.getNome() + " " + u.getCognome() + "\r\n" +
                            "User Succesfully loged in", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("No Account avilable with this username and password ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                    MessageBox.Show("Please enter value in all field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("503 Service Unavailable!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

        }

        //Gestione l'evento on-click per il pulsante Utente non Registrato
        //Rinvia alla finestra di registrazione nuovo utente
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            var registrazione = new Registrazione(client);
            registrazione.Closed += (s, args) => this.Close();
            registrazione.Show();
        }
    }
}
