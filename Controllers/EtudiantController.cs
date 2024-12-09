using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ecole221.Controllers
{
    public class EtudiantController : Controller
    {
        // Exemple de données en mémoire pour les étudiants et les cours
        private static List<Etudiant> etudiants = new List<Etudiant>
        {
            new Etudiant { Matricule = 1, NomComplet = "Ali Cissokho", Adresse = "123 Rue A" },
            new Etudiant { Matricule = 2, NomComplet = "Sophie Kebe", Adresse = "46 Rue B" }
        };

        private static List<Cours> cours = new List<Cours>
        {
            new Cours { Id = 1, Nom = "C#", Date = DateTime.Now, HeureDebut = new TimeSpan(8, 0, 0), HeureFin = new TimeSpan(12, 0, 0), Etudiants = new List<Etudiant>() },
            new Cours { Id = 2, Nom = "Java", Date = DateTime.Now.AddDays(1), HeureDebut = new TimeSpan(13, 0, 0), HeureFin = new TimeSpan(17, 0, 0), Etudiants = new List<Etudiant>() }
        };

        // Exemple de données pour les absences des étudiants
        private static List<Absence> absences = new List<Absence>
        {
            new Absence { Date = DateTime.Now, Etudiant = etudiants[0], Cours = cours[0] },
            new Absence { Date = DateTime.Now.AddDays(1), Etudiant = etudiants[1], Cours = cours[1] }
        };

        // Action pour lister les cours d'un étudiant
        public IActionResult ListerCours(int matricule)
        {
            var etudiant = etudiants.FirstOrDefault(e => e.Matricule == matricule);
            if (etudiant == null)
                return NotFound("Étudiant non trouvé.");

            // Lister les cours auxquels l'étudiant est inscrit
            var coursEtudiant = cours.Where(c => c.Etudiants.Contains(etudiant)).ToList();

            // Vérifiez si l'étudiant est inscrit à des cours
            if (!coursEtudiant.Any())
            {
                return NotFound("Aucun cours trouvé pour cet étudiant.");
            }

            return View(coursEtudiant);  // Afficher la liste des cours
        }

        // Action pour lister les absences d'un étudiant
        public IActionResult ListerAbsences(int matricule)
        {
            var etudiant = etudiants.FirstOrDefault(e => e.Matricule == matricule);
            if (etudiant == null)
                return NotFound("Étudiant non trouvé.");

            // Lister les absences de l'étudiant
            var absencesEtudiant = absences.Where(a => a.Etudiant.Matricule == matricule).ToList();

            // Vérifiez si l'étudiant a des absences
            if (!absencesEtudiant.Any())
            {
                return NotFound("Aucune absence trouvée pour cet étudiant.");
            }

            return View(absencesEtudiant);  // Afficher la liste des absences
        }

        // Action pour ajouter un étudiant à un cours
       public IActionResult AjouterEtudiantAuCours(int matricule, int coursId)
        {
            var etudiant = etudiants.FirstOrDefault(e => e.Matricule == matricule);
            var coursTrouve = cours.FirstOrDefault(c => c.Id == coursId); // Renommage de la variable locale

            if (etudiant == null || coursTrouve == null)  // Utilisation de 'coursTrouve'
                return NotFound("Étudiant ou cours non trouvé.");

            // Ajouter l'étudiant au cours trouvé
            coursTrouve.Etudiants.Add(etudiant);  // 'coursTrouve' au lieu de 'cours'

            return RedirectToAction("ListerCours", new { matricule = matricule });
        }

    }
}
