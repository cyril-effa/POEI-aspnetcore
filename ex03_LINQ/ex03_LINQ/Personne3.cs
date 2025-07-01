namespace ex03_LINQ
{
    internal class Personne3
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public bool EstIngenieur { get; set; }
        public Personne3(string nom, string prenom, bool estIngenieur)
        {
            Nom = nom;
            Prenom = prenom;
            EstIngenieur = estIngenieur;
        }
    }
}