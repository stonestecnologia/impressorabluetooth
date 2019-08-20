using System;
using System.Globalization;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ImpressoraBluetooth.Service
{
    public class Utils
    {
        public static async void SavesInMemory(string key, string value)
        {
            Boolean tem = Application.Current.Properties.ContainsKey(key);

            if (tem)
            {
                Application.Current.Properties[key] = value;
            }
            else
            {
                Application.Current.Properties.Add(key, value);
            }
            await App.Current.SavePropertiesAsync();
        }
        public static void SaveInMemoriaThread(string key, string value)
        {
            Task.Run(() =>
            {
                SavesInMemory(key, value);
            });
        }
        public static string GetDataInMemory(string key)
        {
            Boolean tem = Application.Current.Properties.ContainsKey(key);
            if (!tem)
            {
                return null;
            }
            return Application.Current.Properties[key] as string;
        }
        public static void RemoveDataInMemory(string key)
        {
            Boolean tem = Application.Current.Properties.ContainsKey(key);
            if (tem)
            {
                bool isRemove = Application.Current.Properties.Remove(key);
                App.Current.SavePropertiesAsync();
            }
        }
        public static async void ClearDataInMemory()
        {
            Application.Current.Properties.Clear();
            await Task.CompletedTask;
        }
        public static string ConvertMoneyToBR(string value)
        {
            NumberFormatInfo nfi = new CultureInfo("pt-BR").NumberFormat;
            return Decimal.Parse(value).ToString("C", nfi);
        }
        public static decimal ConvertMoneyBRDecimal(string value)
        {
            var valorConvertido = Decimal.Parse(value.Replace("R$ ", "").Replace(".", "").Replace(",", "."),
                CultureInfo.GetCultureInfo("pt-BR"));
            return (decimal) valorConvertido;
        }
    }
}
