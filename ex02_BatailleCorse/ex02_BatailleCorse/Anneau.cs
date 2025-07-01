using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex02_BatailleCorse
{
    internal class Anneau<T>
    {
        public Maillon<T>? Premier { get; set; }

        public Anneau()
        {
            Premier = null;
        }

        public void Ajouter(T valeur)
        {
            var nouveauMaillon = new Maillon<T>(valeur);
            if (Premier == null)
            {
                Premier = nouveauMaillon;
                nouveauMaillon.Suivant = Premier; // Forme un anneau
            }
            else
            {
                var dernier = Premier;
                while (dernier.Suivant != Premier)
                {
                    dernier = dernier.Suivant;
                }
                dernier.Suivant = nouveauMaillon;
                nouveauMaillon.Suivant = Premier; // Forme un anneau
            }
        }

        public void Retirer(T valeur)
        {
            if (Premier == null) return; // Anneau vide

            Maillon<T>? courant = Premier;
            Maillon<T>? precedent = null;

            do
            {
                if (courant.Valeur.Equals(valeur))
                {
                    if (precedent == null) // Si c'est le premier maillon
                    {
                        if (Premier.Suivant == Premier) // Si c'est le seul maillon
                        {
                            Premier = null;
                        }
                        else
                        {
                            var dernier = Premier;
                            while (dernier.Suivant != Premier)
                            {
                                dernier = dernier.Suivant;
                            }
                            Premier = Premier.Suivant; // Met à jour le premier maillon
                            dernier.Suivant = Premier; // Met à jour le dernier maillon
                        }
                    }
                    else
                    {
                        precedent.Suivant = courant.Suivant; // Retire le maillon courant
                    }
                    return;
                }
                precedent = courant;
                courant = courant.Suivant;
            } while (courant != Premier);
        }

        public T RetirerPremier()
        {
            var valeur = Premier.Valeur;
            Retirer(valeur);
            return valeur;
        }

        public int Count()
        {
            if (Premier == null) return 0; // Anneau vide
            int count = 0;
            Maillon<T>? courant = Premier;
            do
            {
                count++;
                courant = courant.Suivant;
            } while (courant != Premier);
            return count;
        }
    }
}
