using System.Web;
using System.Web.Mvc;

namespace MVCSepetIslemleri_Tekrar
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}
