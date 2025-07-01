using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BO
{
    public class ValidOrderStatusAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is not string status)
                return false;

            // Liste des statuts valides (doit correspondre à AvailableStatuses)
            string[] validStatuses = { "EnAttente", "EnCours", "Expédié", "Livré", "Annulé" };

            return validStatuses.Contains(status);
        }
    }
}