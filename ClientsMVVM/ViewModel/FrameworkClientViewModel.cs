using ClientsMVVM.Model;
using ClientsMVVM.Repositori;
using CommunityToolkit.Mvvm.ComponentModel;
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
    }
}
