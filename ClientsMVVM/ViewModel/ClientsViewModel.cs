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
    public class ClientsViewModel : ObservableBase // mirar este cambios en los commits, antes era -> INotifyPropertyChanged
    {
        string nom;
        string cognom;
        string saldo;

        // Nos lo genera la interficie
        //public event PropertyChangedEventHandler? PropertyChanged;

        // lo siguiente que creamos es esto:
        //private void OnCanviEnLaPropietat([System.Runtime.CompilerServices.CallerMemberName] string nomPropietat = "") // para no volver a ponerlo
        //{

        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomPropietat));
        //}

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
        public string Nom 
        {  
            get => nom; 
            set 
            {
                SetProperty(ref nom, value);
                NotifyPropertyChanged(nameof(NomComplet));
                //if (nom != value) // entra si se ha hecho un cambio a algo nuevo (es la forma estandar, luego con ObserbableBase como abajo se usa, es mas sencillo)
                //{
                //    nom = value; 
                //    NotifyPropertyChanged(nameof(Nom));
                //    NotifyPropertyChanged(nameof(NomComplet)); 
                //}                
            } 
        } // primer ejemplo del uso de cuando se hace un cambio que se sepa 

        // habiendo puesto lo raro de ante [System.Runtime.CompilerServices.CallerMemberName] no hace falta ponerlo
        public string Cognom { get => cognom; 
            set { cognom = value; NotifyPropertyChanged(""); NotifyPropertyChanged(nameof(NomComplet)); } } // !!
        public string Saldo 
        { 
            get => saldo; 
            set => SetProperty(ref saldo, value); // este cambio tb es por la implmenetación de ObservableBase
            // mira que el value sea igual que la ref, si no cambia, no hace nada, si no, pues lo cambia
        } 
        public string NomComplet { get => Nom + " " + Cognom; } // así ya devuelve el cambio del nombre completo

        

        /* Haciéndolo así te aparece en el preview los valores por defecto
         
            public string Nom {  get; set; } = "Pere";
            public string Cognom { get; set; } = "Pomma";
            public string Saldo { get; set; } = "6969";// string porque del textbox sale como tal 
            public string NomComplet { get => Nom + " " + Cognom; } // así ya devuelve el cambio del nombre completo

        */
    }
}
