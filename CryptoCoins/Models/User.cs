using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCoins.Models
{
    public class User
    {
        public string ID { get; set; } = "";
        public string email { get; set; } = "";
        public string password { get; set; } = "";
        public string role { get; set; } = "";
        public static string username { get; set; } = "";
    }
}
