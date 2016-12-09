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
			Get["/"] = _ =>
			{
				return View["index.cshtml"];
			};
		}
	}
}
