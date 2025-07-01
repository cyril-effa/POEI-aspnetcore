namespace ex02_BatailleCorse
{
    internal class Joueur
    {
        public string Name { get; set; }
        public Anneau<Carte> AnneauCartes { get; set; } = new Anneau<Carte>(); // Anneau pour les cartes du joueur

        public Joueur(string name)
        {
            Name = name;
        }
    }
}