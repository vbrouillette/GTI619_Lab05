using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GTI619_Lab5.Models
{
    public class UpdateConfigModel
    {
        [Display(Name = "Nombre d'esseai avant blocage :")]
        public int NbrTry { get; set; }
        [Display(Name = "Temps d'attente avant de pouvoir réessayer :")]
        public int TryDownPeriod { get; set; }
        [Display(Name = "Bloque l'accès après 2 suites d'erreur :")]
        public bool IsBlockAfterTwoTries { get; set; }

        [Display(Name = "Y a-t-il des changement de mot de passe périodique :")]
        public bool IsPeriodic { get; set; }
        [Display(Name = "Quel est le temps de changement périodique :")]
        public int PeriodPeriodic { get; set; }

        [Display(Name = "Grandeur maximum :")]
        public int MaxLenght { get; set; }
        [Display(Name = "Grandeur minimum :")]
        public int MinLenght { get; set; }
        [Display(Name = "Doit contenir des caractères majuscules :")]
        public bool IsUpperCase { get; set; }
        [Display(Name = "Doit contenir des caractères minuscules :")]
        public bool IsLowerCase { get; set; }
        [Display(Name = "Doit contenir des caractères spéciales :")]
        public bool IsSpecialCase { get; set; }
        [Display(Name = "Doit contenir des caractères numérique :")]
        public bool IsNumber { get; set; }

        [Display(Name = "Durée de la session :")]
        public int TimeOutSession { get; set; }
    }
}