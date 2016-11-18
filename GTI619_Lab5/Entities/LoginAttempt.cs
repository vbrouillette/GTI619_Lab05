using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GTI619_Lab5.Entities
{
    public class LoginConfig
    {
        public int Id { get; set; }

        public String DelayBetweenBlocks { get; set; }
        public int NbAttemptsBeforeBlocking { get; set; }
        public int MaxBlocksBeforeAdmin { get; set; }
        public int DelayBetweenFailedAuthentication { get; set; }

        public LoginConfig() { }
    }
}