﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GTI619_Lab5.Models
{
    public class UpdateConfigModel
    {
        public int NbrTry { get; set; }
        public int TryDownPeriod { get; set; }
        public bool IsBlockAfterTwoTries { get; set; }

        public bool IsPeriodic { get; set; }
        public int PeriodPeriodic { get; set; }
        // Donner la possibilité de date ?

        public int MaxLenght { get; set; }
        public int MinLenght { get; set; }
        public bool IsUpperCase { get; set; }
        public bool IsLowerCase { get; set; }
        public bool IsSpecialCase { get; set; }
        public bool IsNumber { get; set; }

        public int TimeOutSession { get; set; }
    }
}