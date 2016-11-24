using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GTI619_Lab5.Models
{
    public class UpdateConfigModel
    {
        [Display(Name = "Délai entre les bloquages :")]
        public String DelayBetweenBlocks { get; set; }
        [Display(Name = "Nombre d'essai avant bloquage :")]
        public int NbAttemptsBeforeBlocking { get; set; }
        [Display(Name = "Maximum de bloquage avant de devoir contacter l'administrateur :")]
        public int MaxBlocksBeforeAdmin { get; set; }
        [Display(Name = "Délai entre chaque authentification échoué :")]
        public int DelayBetweenFailedAuthentication { get; set; }

        [Display(Name = "Y a-t-il des changement de mot de passe périodique :")]
        public bool IsPeriodic { get; set; }
        [Display(Name = "Quel est le temps de changement périodique :")]
        public int PeriodPeriodic { get; set; }
        [Display(Name = "Nombre de password qu'on ne peut pas reprendre :")]
        public int NbrLastPasswords { get; set; }

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