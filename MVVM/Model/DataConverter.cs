using System.Globalization;
using TransportationAnalyticsHub.MVVM.Model.DBModels;

namespace TransportationAnalyticsHub.MVVM.Model
{
    public static class DataConverter
    {
        private static readonly string[] DateFormats = ["MM/dd/yyyy", "M/dd/yyyy", "MM/d/yyyy", "M/d/yyyy", "H:mm d.MM.yyyy"];

        public static decimal ConvertToDecimal(string input) => decimal.Parse(input.Replace('.', ','));
        public static double ConvertToDouble(string input) => double.Parse(input.Replace('.', ','));

        public static DateTime ConvertToDateTime(string input, bool eraseTime = true)
        {
            DateTime dateT;
            if (eraseTime)
                input = input.Substring(0, input.IndexOf(' '));

            DateTime.TryParseExact(input, DateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateT);
            return dateT;
        }

        public static DateOnly ConvertToDateOnly(string input) => DateOnly.FromDateTime(ConvertToDateTime(input));

        public static string ConvertDateOnlyToString(DateOnly input)
        {
            var data = DateTime.ParseExact(input.ToString(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
            string formatedDate = data.ToString("M/d/yyyy").Replace('.', '/');
            return formatedDate + " 12:00:00 AM";
        }

        public static string ConvertAddressToString(Adresy address) => $"{address.Ulica} {address.NumerBudynku}, {address.KodPocztowy} {address.Miejscowosc}, {address.Kraj}";
    }
}
