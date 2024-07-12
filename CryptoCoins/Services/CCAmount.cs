using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCoins.Services
{
    public class CCAmount
    {
        public static decimal totalAmount { get; set; } = 0;
        public static decimal total30DaysAmount { get; set; } = 0;
        public static decimal BTC30DaysAmount { get; set; } = 0;
        public static decimal BNB30DaysAmount { get; set; } = 0;
        public static decimal ETH30DaysAmount { get; set; } = 0;
        public static decimal SOL30DaysAmount { get; set; } = 0;
        public static decimal USDT30DaysAmount { get; set; } = 0;
        public static decimal XRP30DaysAmount { get; set; } = 0;
    }
}
