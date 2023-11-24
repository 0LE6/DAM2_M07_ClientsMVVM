﻿using ClientsMVVM.Model;
using ClientsMVVM.Repositori;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsMVVM.ViewModel
{
    public class ClientsViewModel
    {
        public ClientsViewModel() 
        {
            IRepositoriDeClients repositoriDeClients = Repo.ObreBDClients();

            // Lo haremos una vez y luego lo quitamos
            repositoriDeClients.CreaClients(30);

            // Los obtenemos después de crearlos
            Clients = repositoriDeClients.Obten();
        }

        // Nuestras propiedaades
        public ObservableCollection<Client> Clients { get; set; }

        // Añadidos para poder gestionar lo que se escribe en los textbox
        public string Nom {  get; set; } = "Pere";
        public string Cognom { get; set; } = "Pomma";
        public string Saldo { get; set; } = "6969";// string porque del textbox sale como tal 
        public string NomComplet { get => Nom + " " + Cognom; } // así ya devuelve el cambio del nombre completo

        /* Haciéndolo así te aparece en el preview los valores por defecto
         
            public string Nom {  get; set; } = "Pere";
            public string Cognom { get; set; } = "Pomma";
            public string Saldo { get; set; } = "6969";// string porque del textbox sale como tal 
            public string NomComplet { get => Nom + " " + Cognom; } // así ya devuelve el cambio del nombre completo
         */
    }
}
