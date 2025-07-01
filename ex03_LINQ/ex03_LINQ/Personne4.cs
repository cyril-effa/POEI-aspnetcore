namespace ex03_LINQ
{
    internal class Personne4
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public int Age { get; set; }
        public string Genre { get; set; }

        public Personne4(string nom, string prenom, int age, string genre)
        {
            Nom = nom;
            Prenom = prenom;
            Age = age;
            Genre = genre;
        }
    }
}