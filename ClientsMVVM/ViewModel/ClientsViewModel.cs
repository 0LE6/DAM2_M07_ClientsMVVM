using ClientsMVVM.Model;
using ClientsMVVM.Repositori;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClientsMVVM.ViewModel
{
    // Añadimos la interficie de que nuestra propiedad sabe notificar los cambios
    public class ClientsViewModel : ObservableBase // mirar este cambios en los commits, antes era -> INotifyPropertyChanged
    {
        string nom;
        string cognom;
        string saldo;
        ObservableCollection<Client> clients;
        IRepositoriDeClients repositoriDeClients;

        public ICommand CreaClientsCommand { get; set; }
        public ICommand AfegeixClientCommand { get; set; }
        public ICommand EditaClientCommand { get; set; }
        public ICommand ConfirmaEdicioCommand { get; set; }
        public ICommand DescartaEdicioCommand { get; set; }
        public ICommand EliminaClientCommand { get; set; }

        // Nos lo genera la interficie
        //public event PropertyChangedEventHandler? PropertyChanged;

        // lo siguiente que creamos es esto:
        //private void OnCanviEnLaPropietat([System.Runtime.CompilerServices.CallerMemberName] string nomPropietat = "") // para no volver a ponerlo
        //{

        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomPropietat));
        //}


        public ClientsViewModel() 
        {
            repositoriDeClients = Repo.ObreBDClients();

            // Lo haremos una vez y luego lo quitamos
            repositoriDeClients.CreaClients(30);

            // Los obtenemos después de crearlos
            Clients = repositoriDeClients.Obten();

            #region COMMANDS

            CreaClientsCommand = new RelayCommand<string>(
                nClients => CreaClients(Convert.ToInt32(nClients)),
                nClients => Clients.Count != 0
                ); // pasamos string que luego convertiremos a int, la segunta parte es un CanExecute (!= para que pueda eliminar, luego se cambiará)

            #endregion
        }
        // otra forma de hacerlo:
        // nClients => repositoriDeClients.CreaClients(Convert.ToInt32(nClients)));
        private void CreaClients(int nClients)
        {
            repositoriDeClients.CreaClients(nClients);
            Clients = repositoriDeClients.Obten();
        }

        #region PROPIEDADES

        // Nuestras propiedaades
        public ObservableCollection<Client> Clients  // esto para que vez de cargarnos los clientes que teníamos y ponerlos los nuevos, nos los añada
        { 
            get => clients; 
            set
            {
                SetProperty(ref clients, value);
            }
        }

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
        #endregion
    }
}
