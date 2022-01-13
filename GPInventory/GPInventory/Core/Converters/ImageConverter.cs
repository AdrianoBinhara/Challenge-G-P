using System;
using System.Globalization;
using Xamarin.Forms;

namespace GPInventory.Core.Converters
{
    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var category = value.ToString();

            if (!string.IsNullOrEmpty(category))
            {
                switch (category)
                {
                    case "Mercado":
                        return "Mercado.png";
                    case "Ferramentas":
                        return "Ferramentas.png";
                    case "Eletrônicos":
                        return "Eletronicos.png";
                    case "Saúde":
                        return "Saude.png";
                }
            }

            return category;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
