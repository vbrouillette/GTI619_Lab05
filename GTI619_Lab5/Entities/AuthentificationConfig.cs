using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GTI619_Lab5.Entities
{
    public class AuthentificationConfig
    {
        public Guid Id { get; set; }
        public int TimeOut { get; set; }

        public AuthentificationConfig() { }
    }
}