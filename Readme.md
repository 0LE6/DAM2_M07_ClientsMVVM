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
