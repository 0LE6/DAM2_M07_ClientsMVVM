using ClientsMVVM.Dades.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ClientsMVVM.Repositori
{
    public static class Repo
    {
        private static IRepositoriDeClients? repositoriDeClients = null;
        public static IRepositoriDeClients ObreBDClients()
        {
            if (repositoriDeClients == null)
            {
                repositoriDeClients = new ClientsXML();
            }
            return repositoriDeClients;
        }
    }
}
