using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using TPLOCAL1.Models;

//Subject is find at the root of the project and the logo in the wwwroot/ressources folders of the solution
//--------------------------------------------------------------------------------------
//Careful, the MVC model is a so-called convention model instead of configuration,
//The controller must imperatively be name with "Controller" at the end !!!
namespace TPLOCAL1.Controllers
{
	public class HomeController : Controller
	{
		//methode "naturally" call by router
		public ActionResult Index(string id)
		{
			if (string.IsNullOrWhiteSpace(id))
				//retourn to the Index view (see routing in Program.cs)
				return View();
			else
			{
				//Call different pages, according to the id pass as parameter
				switch (id)
				{
					case "AvisList":
                        //TODO : code reading of the xml files provide
                        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "XlmFile", "DataAvis.xml");
                        var liste = new OpinionList();
                        var avis = liste.GetAvis(filePath);

                        return View(id, avis);

					case "Formulaire":
						//TODO : call the Form view with data model empty
						return View(id);
					default:
						//retourn to the Index view (see routing in Program.cs)
						return View();
				}
			}
		}


		//methode to send datas from form to validation page
		[HttpPost]
		public ActionResult Formulaire(FormulaireModel model)
		{
			//condition du cahier des charges :
			if (model.Sexe.Equals(FormulaireModel.Sexes.ToArray()[0]))
			{
				ModelState.AddModelError("Sexe", "Selectionnez un sexe.");
			}

			if (model.Date > DateTime.Parse("01/01/2021"))
			{
				ModelState.AddModelError("Date", "La formation doit avoir commencée avant le 01/01/2021.");
			}

			if (model.Formation.Equals(FormulaireModel.Formations.ToArray()[0]))
			{
				ModelState.AddModelError("Formation", "Selectionnez une formation.");
			}
			else if (model.Formation.Equals(FormulaireModel.Formations.ToArray()[1]) && model.AvisCobol == null)
			{
				ModelState.AddModelError("AvisCobol", "Donnez un avis pour la formation Cobol.");
			}
			else if (model.Formation.Equals(FormulaireModel.Formations.ToArray()[2]) && model.AvisSharp == null)
			{
				ModelState.AddModelError("AvisCobol", "Donnez un avis pour la formation C#.");
			}
			else if (model.Formation.Equals(FormulaireModel.Formations.ToArray()[3]) &&
				(model.AvisCobol == null || model.AvisSharp == null))
			{
				ModelState.AddModelError("AvisCobol", "Donnez un avis pour les deux formations.");
			}

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			return View(nameof(ValidationFormulaire), model);
		}

		//méthode envoie données
		[HttpPost]
		public ActionResult ValidationFormulaire(FormulaireModel model)
		{
			return View(model);
		}
	}
}