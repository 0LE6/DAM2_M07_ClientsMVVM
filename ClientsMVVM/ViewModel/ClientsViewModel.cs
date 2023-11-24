using ClientsMVVM.Model;
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
        // Nuestras propiedaades
        public ObservableCollection<Client> Clients { get; set; }
    }
}
