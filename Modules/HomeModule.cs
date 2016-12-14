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
				var allStylists = Stylist.GetAll();
				return View["/add-new-client.cshtml", allStylists];
			};
			Get["/success-stylist"] = _ => {
				return View["/success-stylist.cshtml"];
			};
			Get["/stylists"] = _ => {
				var allStylists = Stylist.GetAll();
				return View["/stylists.cshtml", allStylists];
			};
			Get["/clients"] = _ => {
				List<Client> allClients = Client.GetAll();
				return View["/clients.cshtml", allClients];
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
			Get["/stylist/{id}"] = parameters => {
				Dictionary<string, object> model = new Dictionary<string, object>();
        var selectedStylist = Stylist.Find(parameters.id);
        var stylistClients = selectedStylist.GetClients();
        var AllClients = Client.GetAll();
        model.Add("stylist", selectedStylist);
        model.Add("stylistClients", stylistClients);
        model.Add("allClients", AllClients);
        return View["stylist.cshtml", model];
			};
			// Get["/client/{id}"] = parameters => {
			// 	Dictionary<string, object> model = new Dictionary<string, object>();
			// 	Client selectedClient = Client.Find(parameters.id);
			// 	Client clientDetails = Client.GetClientDetails();
			// 	List<Stylist> clientStylist = selectedClient.GetClientStylistId();
			// 	model.Add("client", selectedClient);
			// 	model.Add("clientDetails", clientDetails);
			// 	model.Add("clientStylist", clientStylist);
			// 	return View["client.cshtml", model];
			// };
			Get["/stylist/delete/{id}"] = parameters => {
				Stylist SelectedStylist = Stylist.Find(parameters.id);
				SelectedStylist.Delete();
				return View["/success-stylist-delete.cshtml"];
			};
			Get["/client/delete/{id}"] = parameters => {
				Client SelectedClient = Client.Find(parameters.id);
				SelectedClient.Delete();
				return View["/success-client-delete.cshtml"];
			};
		}
	}
}
