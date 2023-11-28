"# Clients MVVM" 



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
