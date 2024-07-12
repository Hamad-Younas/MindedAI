using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCoins.Services
{
	public class UserAmount
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int BNB { get; set; }
		public int ETH { get; set; }
		public int SOL { get; set; }
		public int BTC { get; set; }
		public int USDT { get; set; }
		public int XRP { get; set; }
		public DateTime Date { get; set; }
	}
}
