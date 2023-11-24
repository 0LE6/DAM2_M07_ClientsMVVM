using ClientsMVVM.Model;
using ClientsMVVM.Repositori;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ClientsMVVM.Dades.Json
{
    public class ClientsXML : IRepositoriDeClients
    {
        const string NOM_FITXER_XML = @"BBDD\Clients.xml";
        string RUTA_FITXER_XML = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), NOM_FITXER_XML);

        /// <summary>
        /// Crea un dades fictícies de clients i les desa en un fitxer XML
        /// </summary>
        /// <param name="quantitat">És la quantitat de clients que ha de crear</param>
        public void CreaClients(int quantitat = 100)
        {
            Random random = new Random();
            List<string> noms = new List<string> { "Joan", "Pere", "Maria", "Sílvia", "Ricard", "Lluís", "Roser", "Laura" };
            List<string> cognoms = new List<string> { "Xurriguera", "Palol", "Murri", "Sala", "Roca", "Lladó", "Ridaura", "Dalmau" };

            ObservableCollection<Client> clients = new ObservableCollection<Client>();
            Client clientActual = null;


            for (int nClient = 0; nClient < quantitat; nClient++)
            {
                clientActual = new Client()
                {
                    Id = Guid.NewGuid().ToString(),
                    Nom = noms[random.Next(noms.Count)],
                    Cognom = cognoms[random.Next(cognoms.Count)],
                    Saldo = random.Next(-100000, 100000)
                };
                clients.Add(clientActual);
            }

            Desa(clients);
        }

        /// <summary>
        /// Desa els clients en un fitxer XML
        /// </summary>
        /// <param name="clients">Dades dels clients que ha de desar</param>
        public void Desa(ObservableCollection<Client> clients)
        {
            using (TextWriter fitxer = new StreamWriter(RUTA_FITXER_XML))
            {

                XmlSerializer serialitzador = new XmlSerializer(typeof(ObservableCollection<Client>));
                serialitzador.Serialize(fitxer, clients);
            }
        }

        /// <summary>
        /// Obté els clients desats en un fitxer XML
        /// </summary>
        /// <returns>Les dades dels clients en format de llista observable</returns>
        public ObservableCollection<Client> Obten()
        {
            ObservableCollection<Client> clients;

            using (TextReader fitxer = new StreamReader(RUTA_FITXER_XML))
            {
                if (fitxer.Peek() != -1)
                {
                    XmlSerializer serialitzador = new XmlSerializer(typeof(ObservableCollection<Client>));
                    clients = (ObservableCollection<Client>)serialitzador.Deserialize(fitxer) ;
                }
                else
                {
                    clients = new ObservableCollection<Client>();
                }
            }
            return clients;
        }

        /// <summary>
        /// Elimina un client a partir del seu identificador
        /// </summary>
        /// <param name="id">Identificador del client a eliminar</param>
        /// <returns></returns>
        public bool Esborra(String id)
        {
            bool esborrat = false;
            ObservableCollection<Client> clients = Obten();
            Client clientEliminable = clients.FirstOrDefault(client => client.Id == id);
            if (clientEliminable != null)
            {
                clients.Remove(clientEliminable);
                esborrat = true;
            }
            Desa(clients);
            return esborrat;
        }

        /// <summary>
        /// Modifica un client
        /// </summary>
        /// <param name="client">Noves dades que ha de tenir el client</param>
        /// <returns></returns>
        public bool Modifica(Client client)
        {
            bool modificat = false;
            ObservableCollection<Client> clients = Obten();
            Client clientModificable = clients.FirstOrDefault(clientActual => clientActual.Id == client.Id);
            if (clientModificable != null)
            {
                clientModificable.Nom = client.Nom;
                clientModificable.Cognom = client.Cognom;
                clientModificable.Saldo = client.Saldo;
                modificat = true;
            }
            Desa(clients);
            return modificat;
        }

        /// <summary>
        /// Afegeix un client
        /// </summary>
        /// <param name="client">Dades del client a afegir</param>
        /// <returns></returns>
        public bool Afegeix(Client client)
        {
            bool afegit = false;
            ObservableCollection<Client> clients = Obten();
            Client clientModificable = clients.FirstOrDefault(clientActual => clientActual.Id == client.Id);
            if (clientModificable == null)
            {
                clients.Add(client);
                afegit = true;
            }
            Desa(clients);
            return afegit;
        }
    }
}
