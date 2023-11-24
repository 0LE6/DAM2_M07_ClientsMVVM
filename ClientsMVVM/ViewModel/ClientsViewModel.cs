using ClientsMVVM.Model;
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
        public string Nom {  get; set; }
        public string Cognom {  get; set; }
        public string Saldo { get; set; }
        public string NomComplet { get; set; }
    }
}
