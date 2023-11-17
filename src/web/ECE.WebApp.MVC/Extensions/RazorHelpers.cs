using Microsoft.AspNetCore.Mvc.Razor;
using System.Security.Cryptography;
using System.Text;

namespace ECE.WebApp.MVC.Extensions
{
    public static class RazorHelpers
    {
        public static string StockMessage(this RazorPage page, int amount)
        {
            return amount > 0 ? $"Apenas {amount} em estoque!" : "Produto esgotado";
        }

        public static string FormatCurrency(this RazorPage page, decimal value)
        {
            return value > 0 ? string.Format(Thread.CurrentThread.CurrentCulture, "{0:C}", value) : "Gratuito";
        }

        public static string HashEmailForGravatar(this RazorPage page, string email)
        {
            var md5Haser = MD5.Create();
            var data = md5Haser.ComputeHash(Encoding.Default.GetBytes(email));
            var sb = new StringBuilder();
            foreach(var item in data)
            {
                sb.Append(item.ToString("x2"));
            }
            return sb.ToString();
        }

        public static string PiecesByProduct(this RazorPage page, int amount)
        {
            return amount > 1 ? $"{amount} pieces" : $"{amount} piece";
        }

        public static string SelectOptionsByAmount(this RazorPage page, int amount, int selectedValue = 0)
        {
            var sb = new StringBuilder();

            for (var i = 1; i <= amount; i++)
            {
                var selected = "";

                if (i == selectedValue)
                {
                    selected = "selected";
                }

                sb.Append($"<option {selected} value='{i}'>{i}</option>");
            }

            return sb.ToString();
        }
    }
}
