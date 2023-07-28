using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace TPLOCAL1.Models
{
	public class FormulaireModel
    {
		public static IEnumerable<string> Sexes = new List<string> {
			"Sélectionner un sexe",
			"Homme",
			"Femme",
			"Autre"
		};

		public static IEnumerable<string> Formations = new List<string> {
			"Sélectionner une formation",
			"Formation Cobol",
			"Formation objet",
			"Formation double compétence"
		};

		// Infos personelles
		[Required(ErrorMessage = "Nom obligatoire !")]
		public string Nom { get; set; }

		[Required(ErrorMessage = "Prénom obligatoire !")]
		public string Prenom { get; set; }

		[Required]
		public string Sexe { get; set; }

		[Required(ErrorMessage = "Adresse obligatoire !")]
		public string Adresse { get; set; }

		[Required]
		[RegularExpression(@"^\d{5}$", ErrorMessage = "Code postal non valide !")]
		public string CodePostal { get; set; }

		[Required(ErrorMessage = "Ville obligatoire !")]
		public string Ville { get; set; }

		[Required(ErrorMessage = "Adresse mail obligatoire !")]
		[RegularExpression(@"^([\w]+)@([\w]+)\.([\w]+)$", ErrorMessage = "Adresse mail non valide.")]
		public string Mail { get; set; }

		// Infos formation suivie
		[Required(ErrorMessage = "Date obligatoire !")]
		[DataType(DataType.Date)]
		public DateTime Date { get; set; }

		[Required]
		public string Formation { get; set; }

		// Avis formation
		public string AvisCobol { get; set; }

		public string AvisSharp { get; set; }
	}
}