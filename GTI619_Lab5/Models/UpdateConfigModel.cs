using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GTI619_Lab5.Models
{
    public class UpdateConfigModel
    {
        [Required]
        [Display(Name = "Délai entre les bloquages :")]
        public String DelayBetweenBlocks { get; set; }
        [Required]
        [Display(Name = "Nombre d'essai avant bloquage :")]
        public int NbAttemptsBeforeBlocking { get; set; }
        [Required]
        [Display(Name = "Maximum de bloquage avant de devoir contacter l'administrateur :")]
        public int MaxBlocksBeforeAdmin { get; set; }
        [Required]
        [Display(Name = "Délai entre chaque authentification échoué :")]
        public int DelayBetweenFailedAuthentication { get; set; }

        [Required]
        [Display(Name = "Y a-t-il des changement de mot de passe périodique :")]
        public bool IsPeriodic { get; set; }
        [Required]
        [Display(Name = "Quel est le temps de changement périodique :")]
        public int PeriodPeriodic { get; set; }
        [Required]
        [Display(Name = "Nombre de password qu'on ne peut pas reprendre :")]
        public int NbrLastPasswords { get; set; }

        [Required]
        [Display(Name = "Grandeur maximum :")]
        public int MaxLenght { get; set; }
        [Required]
        [Display(Name = "Grandeur minimum :")]
        public int MinLenght { get; set; }
        [Required]
        [Display(Name = "Doit contenir des caractères majuscules :")]
        public bool IsUpperCase { get; set; }
        [Required]
        [Display(Name = "Doit contenir des caractères minuscules :")]
        public bool IsLowerCase { get; set; }
        [Required]
        [Display(Name = "Doit contenir des caractères spéciales :")]
        public bool IsSpecialCase { get; set; }
        [Required]
        [Display(Name = "Doit contenir des caractères numérique :")]
        public bool IsNumber { get; set; }

        [Required]
        [Display(Name = "Durée de la session :")]
        public int TimeOutSession { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe :")]
        public string Password { get; set; }
    }
}