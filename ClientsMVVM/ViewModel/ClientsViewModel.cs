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

    /* TODO (before 28/11/2023):
     * [] Edita
     * [] Confirma
     * [] Descarta
     */
    // Añadimos la interficie de que nuestra propiedad sabe notificar los cambios
    public class ClientsViewModel : ObservableBase // mirar este cambios en los commits, antes era -> INotifyPropertyChanged
    {
        string nom;
        string cognom;
        string saldo;
        ObservableCollection<Client> clients;
        IRepositoriDeClients repositoriDeClients;
        int posicio;

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

            //// Lo haremos una vez y luego lo quitamos (en este punto lo comentamos para que no nos aparezcan los 30 creados, sino vacío y luego le demos a crear)
            //repositoriDeClients.CreaClients(30);

            // Los obtenemos después de crearlos
            Clients = repositoriDeClients.Obten();

            #region COMMANDS

            CreaClientsCommand = new RelayCommand<string>(
                nClients => CreaClients(Convert.ToInt32(nClients)),
                nClients => Clients.Count == 0
                ); // pasamos string que luego convertiremos a int, la segunta parte es un CanExecute (!= para que pueda eliminar, luego se cambiará)

            // Selected index del listBox
            EliminaClientCommand = new RelayCommand(
                obj => EliminaClient(), //repositoriDeClients.Esborra(Clients[Posicio].Id) // le pasamos la id del cliente segun su posicion de la lista 
                obj => Posicio != -1 // CanExecute siempre y cuando haya alguno seleccionado, -1 significa que no hay ninguno
                );

            AfegeixClientCommand = new RelayCommand(
                obj => AfageixClientNou(),
                obj => EsValid
                );

            // TODO : Edita
            EditaClientCommand = new RelayCommand(
                obj => EditaClient(),
                obj => EsValid
                );

            // TODO : Confirma


            // TODO : Descarta

            #endregion
        }

        private void EditaClient()
        {
            
        }

        private bool EsValid 
        { 
            get {
                decimal saldo;

                return (
                    !string.IsNullOrEmpty(Nom) &&
                    !string.IsNullOrEmpty(Cognom) &&
                    Decimal.TryParse(Saldo, out saldo)
                    );
            } 
        }

        private void AfageixClientNou()
        {
            Client clientNou = new Client()
            {
                Id = Guid.NewGuid().ToString(), // lol ... genera clave aleatoria
                Nom = Nom,
                Cognom = Cognom,
                Saldo = Convert.ToDecimal(Saldo)
            };
            repositoriDeClients.Afegeix(clientNou);
            Clients = repositoriDeClients.Obten(); // refrescar lista de clients
        }

        private void EliminaClient()
        {
            repositoriDeClients.Esborra(Clients[Posicio].Id);
            if (posicio == Clients.Count - 1)
            {
                Posicio--; // que se quede en la anterior
            }
            Clients = repositoriDeClients.Obten(); // refrescar lista de clients
        }

        // otra forma de hacerlo:
        // nClients => repositoriDeClients.CreaClients(Convert.ToInt32(nClients)));
        private void CreaClients(int nClients)
        {
            repositoriDeClients.CreaClients(nClients);
            Clients = repositoriDeClients.Obten(); // refrescar lista de clients
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

        public int Posicio { get => posicio; set { SetProperty(ref posicio, value); } }

        /* Haciéndolo así te aparece en el preview los valores por defecto
         
            public string Nom {  get; set; } = "Pere";
            public string Cognom { get; set; } = "Pomma";
            public string Saldo { get; set; } = "6969";// string porque del textbox sale como tal 
            public string NomComplet { get => Nom + " " + Cognom; } // así ya devuelve el cambio del nombre completo

        */
        #endregion
    }
}
