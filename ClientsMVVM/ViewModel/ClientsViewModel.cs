using ClientsMVVM.Model;
using ClientsMVVM.Repositori;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsMVVM.ViewModel
{
    // Añadimos la interficie de que nuestra propiedad sabe notificar los cambios
    public class ClientsViewModel : INotifyPropertyChanged
    {
        string nom;
        string cognom;
        string saldo;

        // Nos lo genera la interficie
        public event PropertyChangedEventHandler? PropertyChanged;

        // lo siguiente que creamos es esto:
        private void OnCanviEnLaPropietat(string nomPropietat = "")
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomPropietat));
        }

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
        public string Nom {  get => nom; set { nom = value; OnCanviEnLaPropietat(nameof(Nom)); } } // primer ejemplo del uso de cuando se hace un cambio que se sepa 
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
