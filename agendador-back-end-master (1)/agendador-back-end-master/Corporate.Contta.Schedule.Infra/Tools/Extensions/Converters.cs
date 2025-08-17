using System.Text.RegularExpressions;

namespace OmniXProduct.Scrapper.Extensions
{
    public static class Converters
    {
        public static decimal? ToWeight(this string weightText)
        {
            decimal? weigth = null;

            if (weightText.Contains("kg"))
            {
                weightText = weightText.Replace("kg", string.Empty).Replace(" ", string.Empty).Replace(".", ",");

                if (decimal.TryParse(weightText, out decimal result))
                {
                    weigth = result;
                }

            }
            else if (weightText.Contains("Quilogramas"))
            {
                weightText = weightText.Replace("Quilogramas", string.Empty).Replace(" ", string.Empty).Replace(".", ",");

                if (decimal.TryParse(weightText, out decimal result))
                {
                    weigth = result;
                }
            }
            else if (weightText.Contains("g"))
            {
                weightText = weightText.Replace("g", string.Empty).Replace(" ", string.Empty).Replace(".", ",");

                if (decimal.TryParse(weightText, out decimal result))
                {
                    weigth = result / 1000;
                }
            }

            return weigth;
        }

        public static decimal? ToDimension(this string dimensionText)
        {
            decimal? dimension = null;

            if (dimensionText.Contains("milímetros"))
            {
                dimensionText = dimensionText
                    .Replace("milímetros", string.Empty)
                    .Replace(".", ",")
                    .Replace(" ", string.Empty);

                if (decimal.TryParse(dimensionText, out decimal result))
                {
                    dimension = result / 10;
                }
            }
            else if (dimensionText.Contains("centímetros"))
            {
                dimensionText = dimensionText
                    .Replace("centímetros", string.Empty)
                    .Replace(".", ",")
                    .Replace(" ", string.Empty);

                if (decimal.TryParse(dimensionText, out decimal result))
                {
                    dimension = result;
                }
            }

            return dimension;
        }

        public static decimal? OnlyNumbers(this string text)
        {
            decimal? number = null;
            var newText = Regex.Replace(text, "[^0-9]", string.Empty);

            if (decimal.TryParse(newText, out decimal result))
            {
                number = result;
            }

            return number;
        }
    }
}
