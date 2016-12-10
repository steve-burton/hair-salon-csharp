using Nancy;
using System.Collections.Generic;
using Nancy.ViewEngines.Razor;
using HairSalon.Objects;

namespace HairSalon
{
	public class HomeModule : NancyModule
	{
		public HomeModule()
		{
			Get["/"] = _ => {
				return View["index.cshtml"];
			};
			Get["/add-new-stylist"] = _ => {
				return View["add-new-stylist.cshtml"];
			};
			Get["/add-new-client"] = _ => {
				return View["add-new-client.cshtml"];
			};
			Post["/added-stylist"] = _ => {
				Stylist newStylist = new Stylist(Request.Form["stylist-name"], Request.Form["stylist-details"]);
				newStylist.Save();
				return View["success-stylist.cshtml"];
			};
			Post["/added-client"] = _ => {
				Client newClient = new Client(Request.Form["client-name"], Request.Form["client-details"], Request.Form["client-stylist"]);
				newClient.Save();
				return View["success-client.cshtml"];
			};
		}
	}
}
