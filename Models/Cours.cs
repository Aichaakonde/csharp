public class Cours
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan HeureDebut { get; set; }
    public TimeSpan HeureFin { get; set; }
    public List<Etudiant> Etudiants { get; set; } = new List<Etudiant>();
}