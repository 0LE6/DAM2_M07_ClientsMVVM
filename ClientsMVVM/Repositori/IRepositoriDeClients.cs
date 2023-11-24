using ClientsMVVM.Model;
using System.Collections.ObjectModel;

namespace ClientsMVVM.Repositori
{
    public interface IRepositoriDeClients
    {
        bool Afegeix(Client client);
        void CreaClients(int quantitat = 100);
        void Desa(ObservableCollection<Client> clients);
        bool Esborra(string id);
        bool Modifica(Client client);
        ObservableCollection<Client> Obten();
    }
}