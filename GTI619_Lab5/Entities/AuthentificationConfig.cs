using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GTI619_Lab5.Entities
{
    public class AuthentificationConfig
    {
        public Guid Id { get; set; }

        public bool IsPeriodic { get; set; }
        public int PeriodPeriodic { get; set; } // Période avant de devoir modifier le password
        public int NbrLastPasswords { get; set; }
        public bool StrongAuthentication { get; set; }

        public int MaxLenght { get; set; }
        public int MinLenght { get; set; }
        public bool IsUpperCase { get; set; }
        public bool IsLowerCase { get; set; }
        public bool IsSpecialCase { get; set; }
        public bool IsNumber { get; set; }

        public int TimeOutSession { get; set; }

        public AuthentificationConfig() { }
    }
}