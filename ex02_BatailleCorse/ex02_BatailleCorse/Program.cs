namespace ex02_BatailleCorse
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // SETUP: Création des joueurs

            var anneauJoueurs = new Anneau<Joueur>(); // Anneau pour les joueurs

            Joueur j1_Emma = new Joueur("Emma");
            Joueur j2_Leo = new Joueur("Léo");
            Joueur j3_Clara = new Joueur("Clara");
            Joueur j4_Mathis = new Joueur("Mathis");

            anneauJoueurs.Ajouter(j1_Emma);
            anneauJoueurs.Ajouter(j2_Leo);
            anneauJoueurs.Ajouter(j3_Clara);
            anneauJoueurs.Ajouter(j4_Mathis);

            // SETUP: Création du paquet de cartes

            List<Carte> paquet = new List<Carte>(); // Liste pour le paquet de cartes

            foreach (Figure figure in Enum.GetValues(typeof(Figure)))
            {
                foreach (Couleur couleur in Enum.GetValues(typeof(Couleur)))
                {
                    paquet.Add(new Carte(figure, couleur));
                }
            }

            paquet = paquet.OrderBy(c => Guid.NewGuid()).ToList(); // Mélange aléatoire des cartes

            // SETUP: Distribution des cartes aux joueurs

            Maillon<Joueur>? courant;
            int index = 0;

            for (int i = 0; i < 8; i++)
            {
                courant = anneauJoueurs.Premier;
                if (courant != null)
                {
                    do
                    {
                        courant.Valeur.AnneauCartes.Ajouter(paquet[index]);
                        courant = courant.Suivant;
                        index++;
                    } while (courant != anneauJoueurs.Premier);
                }
            }

            // JEU: Boucle de jeu

            Anneau<Carte> cartesJouees = new Anneau<Carte>(); // Liste pour les cartes jouées
            Joueur joueurDefi = null; // Joueur qui a donné le défi
            int tourDeDefi = 0; // Compteur de tours de défi

            courant = anneauJoueurs.Premier;
            if (courant != null)
            {
                do
                {
                    if (courant.Valeur.AnneauCartes.Premier == null)
                    {
                        Console.WriteLine($"{courant.Valeur.Name} n'a plus de cartes ! Elimination !");
                        anneauJoueurs.Retirer(courant.Valeur); // Retire le joueur de l'anneau s'il n'a plus de cartes
                        courant = courant.Suivant;
                        continue; // Passe au joueur suivant si le joueur n'a plus de cartes
                    }

                    // Chaque joueur joue une carte de son anneau de cartes
                    Carte carte = courant.Valeur.AnneauCartes.RetirerPremier(); // Retire la première carte de l'anneau du joueur
                    cartesJouees.Ajouter(carte); // Ajoute la carte à la liste des cartes jouées
                    Console.WriteLine($"{courant.Valeur.Name} joue un {carte}");
                    bool joueurRejoue = false; // Indique si un défi est en cours

                    if (tourDeDefi > 0)
                    {
                        if (!carte.IsHabillee)
                        {
                            tourDeDefi--; // Décrémente le compteur de défi si la carte n'est pas habillée

                            if (tourDeDefi == 0)
                            {
                                Console.WriteLine($"Le défi est perdu ! {joueurDefi.Name} remporte {cartesJouees.Count()} cartes !\r\n");
                                
                                for (int i = 0; i < cartesJouees.Count(); i++)
                                {
                                    Carte carteGagnee = cartesJouees.RetirerPremier(); // Retire la première carte de la liste des cartes jouées
                                    joueurDefi.AnneauCartes.Ajouter(carteGagnee); // Le joueur ramasse les cartes jouées
                                }
                            }
                            else
                            {
                                joueurRejoue = true; // Le joueur rejoue s'il n'a pas encore perdu le défi
                            }
                        }

                    }

                    if (carte.IsHabillee)
                    {
                        joueurDefi = courant.Valeur; // Définit le joueur qui a donné le défi

                        switch (carte.Figure)
                        {
                            case Figure.AS:
                                tourDeDefi = 1;
                                break;
                            case Figure.ROI:
                                tourDeDefi = 2;
                                break;
                            case Figure.DAME:
                                tourDeDefi = 3;
                                break;
                            case Figure.VALET:
                                tourDeDefi = 4;
                                break;
                        }
                    }

                    if (!joueurRejoue)
                    {
                        courant = courant.Suivant;
                    }
                } while (courant != courant.Suivant);

                Console.WriteLine($"Victoire de {courant.Valeur.Name} !");
            }
        }
    }
}
