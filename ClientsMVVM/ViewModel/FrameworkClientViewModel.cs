using ClientsMVVM.Model;
using ClientsMVVM.Repositori;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClientsMVVM.ViewModel
{
    partial class FrameworkClientViewModel : ObservableObject
    {
        // Copiamos esto del ClientsViewModel
        public FrameworkClientViewModel()
        {
            repositoriDeClients = Repo.ObreBDClients();
            Clients = repositoriDeClients.Obten();
        }

        // Ponemos una anotación de ObservableProperty (la clase tiene que ser PARCIAL)
        [ObservableProperty]
        string nom;

        [ObservableProperty]
        string cognom;

        [ObservableProperty]
        string saldo;

        [ObservableProperty]
        ObservableCollection<Client> clients;

        [ObservableProperty]
        int posicio;

        [ObservableProperty]
        bool estemEditant = false; // para editar

        Client clientEnEdicio;
        IRepositoriDeClients repositoriDeClients;

        // ------------------------------------------------------

        #region CODI DELS COMMANDS

        private bool PotDescartarEdicio()
        {
            return EstemEditant;
        }

        private bool PotConfirmarEdicio()
        {
            return EstemEditant;
        }


        private bool PotAfegirClient()
        {
            return EsValid && true;
        }

        private bool PotEliminarClient()
        {
            return Posicio != -1;
        }

        private void DescartaEdicio()
        {
            EstemEditant = false;
            Nom = ""; Cognom = ""; Saldo = "";
        }

        private void ConfirmaEdicio()
        {
            // lógica de implmentación de la confirmación de un cliente
            // usamos el clientEnEdicio

            clientEnEdicio.Nom = Nom;
            clientEnEdicio.Cognom = Cognom;
            clientEnEdicio.Saldo = Convert.ToDecimal(Saldo);
            repositoriDeClients.Modifica(clientEnEdicio);

            // una vez modificado, se obtiene
            Clients = repositoriDeClients.Obten();
            EstemEditant = false;

            // después borrar los datos
            Nom = ""; Cognom = ""; Saldo = "";
        }

        

        private bool EsValid
        {
            get
            {
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

        // Ya tenemos el command pasado
        [RelayCommand (CanExecute = nameof(PotCrearClients))]
        private void CreaClients(int nClients)
        {
            repositoriDeClients.CreaClients(nClients);
            Clients = repositoriDeClients.Obten(); // refrescar lista de clients
        }

        private bool PotCrearClients()
        {
            return Clients.Count == 0;
        }

        // Command EditarClient
        [RelayCommand (CanExecute = nameof(PotEditarClient))]
        private void EditaClient()
        {
            EstemEditant = true;
            clientEnEdicio = new Client { Id = Clients[Posicio].Id }; // !!!
            Nom = Clients[Posicio].Nom;
            Cognom = Clients[Posicio].Cognom;
            Saldo = Clients[Posicio].Saldo.ToString();
        }

        private bool PotEditarClient()
        {
            return !EstemEditant && Posicio != -1; // si no estamos editando, se puede editar
        }
        #endregion
    }
}
