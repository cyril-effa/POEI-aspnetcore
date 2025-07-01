using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex02_BatailleCorse
{
    internal class Carte
    {
        public Figure Figure { get; set; }
        public Couleur Couleur { get; set; }
        public bool IsHabillee { get; set; }

        public Carte(Figure figure, Couleur couleur)
        {
            Figure = figure;
            Couleur = couleur;
            IsHabillee = figure == Figure.VALET || figure == Figure.DAME || figure == Figure.ROI || figure == Figure.AS;
        }

        public override string ToString()
        {
            return $"{Figure} de {Couleur}";
        }
    }
}
