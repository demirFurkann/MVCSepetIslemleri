using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCSepetIslemleri_Tekrar.Models.PureVMs
{
	public class ProductVM
	{
		public int ID { get; set; }

		public string ProductName { get; set; }

		public decimal? UnitPrice { get; set; }
	}
}