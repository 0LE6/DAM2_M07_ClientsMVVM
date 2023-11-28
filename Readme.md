"# Clients MVVM" 

Hay que instalar el communitytool kit mvvm

![image](https://github.com/0LE6/DAM2_M07_ClientsMVVM/assets/135649528/fd4b1d03-b1a9-488a-bd03-a82bf441d4d3)


![image](https://github.com/0LE6/DAM2_M07_ClientsMVVM/assets/135649528/29dd2eeb-abd5-4b5c-aaf9-9c3cee20523a)

![image](https://github.com/0LE6/DAM2_M07_ClientsMVVM/assets/135649528/e3890122-08b3-4fe1-9305-9847d1b092db)

![image](https://github.com/0LE6/DAM2_M07_ClientsMVVM/assets/135649528/581cbaf1-ebb5-4378-a5db-a1144b32e467)
```csharp
namespace ClientsMVVM.Converters
{
    public class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Decimal? valor = (Decimal?)value;
            if (valor != null)
            {
                if (valor < 0)
                {
                    return Brushes.DarkRed;
                }
            }
            return Brushes.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class GruixConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Decimal? valor = (Decimal?)value;
            if (valor != null)
            {
                if (valor < 0)
                {
                    return FontWeights.Bold;
                }
            }
            return FontWeights.Normal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ImatgeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Decimal? valor = (Decimal?)value;
            if (valor != null)
            {
                if (valor < 0)
                {
                    return "../Imatges/cross1.png";
                }
            }
            return "../Imatges/check1.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
```

---------

![image](https://github.com/0LE6/DAM2_M07_ClientsMVVM/assets/135649528/7fae07bf-a4cc-44d5-bcdb-657df0f53e91)

``` csharp
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClientsMVVM.ViewModel
{
    public class FrameworkClientViewModel : ObservableObject
    {

    }
}

```
![image](https://github.com/0LE6/DAM2_M07_ClientsMVVM/assets/135649528/3a00824b-d6d8-4dec-8479-a7714791f4aa)

``` csharp
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
        string cognom;
        string saldo;
        ObservableCollection<Client> clients;
        IRepositoriDeClients repositoriDeClients;
        int posicio;
        bool estemEditant = false; // para editar
        Client clientEnEdicio;


    }
}
```

``` csharp
// Copiamos esto del ClientsViewModel
public FrameworkClientViewModel()
{
    repositoriDeClients = Repo.ObreBDClients();
    Clients = repositoriDeClients.Obten();
}
```
![image](https://github.com/0LE6/DAM2_M07_ClientsMVVM/assets/135649528/749c1103-b88c-4a6b-8a12-e2c2f91c0982)

Ahora pasamos toda la region de CODI DEL COMMANDS

``` chsarp
#region CODI DELS COMMANDS
private bool PotDescartarEdicio()
{
    return EstemEditant;
}

private bool PotConfirmarEdicio()
{
    return EstemEditant;
}

private bool PotEditarClient()
{
    return !EstemEditant && Posicio != -1; // si no estamos editando, se puede editar
}

private bool PotAfegirClient()
{
    return EsValid && true;
}

private bool PotEliminarClient()
{
    return Posicio != -1;
}

private bool PotCrearClients()
{
    return Clients.Count == 0;
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

private void EditaClient()
{
    EstemEditant = true;
    clientEnEdicio = new Client { Id = Clients[Posicio].Id }; // !!!
    Nom = Clients[Posicio].Nom;
    Cognom = Clients[Posicio].Cognom;
    Saldo = Clients[Posicio].Saldo.ToString();
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

#endregion
```

Ahora pasamos los commands:

ponemos los atributos, las propiedades y decimos que puedan notificar

cambiar los nombres de los binding despues de pasar los commands
