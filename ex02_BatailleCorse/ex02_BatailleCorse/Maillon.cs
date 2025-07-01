using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex02_BatailleCorse
{
    internal class Maillon<T>
    {
        public T Valeur { get; set; }
        public Maillon<T>? Suivant { get; set; }

        public Maillon(T valeur, Maillon<T>? suivant = null)
        {
            Valeur = valeur;
            Suivant = suivant;
        }
    }
}
