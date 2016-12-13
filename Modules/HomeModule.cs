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
				return View["/index.cshtml"];
			};
			Get["/add-new-stylist"] = _ => {
				return View["/add-new-stylist.cshtml"];
			};
			Get["/add-new-client"] = _ => {
				return View["/add-new-client.cshtml"];
			};
			Get["/success-stylist"] = _ => {
				return View["/success-stylist.cshtml"];
			};
			Get["/stylists"] = _ => {
				return View["/stylists.cshtml"];
			};
			Get["/clients"] = _ => {
				return View["/clients.cshtml"];
			};
			Post["/added-stylist"] = _ => {
				Stylist newStylist = new Stylist(Request.Form["stylist-name"], Request.Form["stylist-details"]);
				newStylist.Save();
				return View["/success-stylist.cshtml"];
			};
			Post["/added-client"] = _ => {
				Client newClient = new Client(Request.Form["client-name"], Request.Form["client-details"], Request.Form["client-stylist"]);
				newClient.Save();
				return View["/success-client.cshtml"];
			};
			Get["/stylist/update/{id}"] = parameters => {
				Stylist SelectedStylist = Stylist.Find(parameters.id);
				return View["/stylist-update.cshtml", SelectedStylist];
			};
			Patch["/stylist/update/{id}"] = parameters => {
				Stylist SelectedStylist = Stylist.Find(parameters.id);
				SelectedStylist.Update(Request.Form["stylist-details"]);
				return View["/success-stylist-update.cshtml"];
			};
			Get["/client/update/{id}"] = parameters => {
				Client SelectedClient = Client.Find(parameters.id);
				return View["client-update.cshtml", SelectedClient];
			};
			Patch["/client/update/{id}"] = parameters => {
				Client SelectedClient = Client.Find(parameters.id);
				SelectedClient.Update(Request.Form["client-details"]);
				return View["/success-stylist-update.cshtml"];
			};
			Get["/stylist/delete/{id}"] = parameters => {
				Stylist SelectedStylist = Stylist.Find(parameters.id);
				return View["/stylist-delete.cshtml", SelectedStylist];
			};
			Delete["/stylist/delete/{id}"] = parameters => {
				Stylist SelectedStylist = Stylist.Find(parameters.id);
				SelectedStylist.Delete();
				return View["/success-stylist-delete.cshtml"];
			};
			Get["/client/delete/{id}"] = parameters => {
				Client SelectedClient = Client.Find(parameters.id);
				return View["/client-delete.cshtml", SelectedClient];
			};
			Delete["/client/delete/{id}"] = parameters => {
				Client SelectedClient = Client.Find(parameters.id);
				SelectedClient.Delete();
				return View["/success-client-delete.cshtml"];
			};
		}
	}
}
