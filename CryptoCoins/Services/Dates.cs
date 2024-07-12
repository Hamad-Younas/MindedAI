using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCoins.Services
{
	public class Dates
	{
		public TimePeriod today { get; set; }
		public TimePeriod sevendays { get; set; }
		public TimePeriod thirtydays { get; set; }
		public TimePeriod sixmonths { get; set; }
		public TimePeriod year { get; set; }
	}
}
