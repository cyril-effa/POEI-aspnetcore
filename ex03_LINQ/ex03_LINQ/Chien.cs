namespace ex03_LINQ
{
    internal class Chien
    {
        public string Nom { get; set; }
        public string Surnom { get; set; }
        public string Race { get; set; }
        public string Couleur { get; set; }
        public string Genre { get; set; }
        public int Age { get; set; }
        public int Poids { get; set; }

        public Chien(string nom, string surnom, string race, string couleur, string genre, int age, int poids)
        {
            Nom = nom;
            Surnom = surnom;
            Race = race;
            Couleur = couleur;
            Genre = genre;
            Age = age;
            Poids = poids;
        }
    }
}