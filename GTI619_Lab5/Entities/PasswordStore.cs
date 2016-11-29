using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GTI619_Lab5.Entities
{
    public class PasswordStore
    {
        public int id { get; set; }
        public String passwordHash { get; set; }
        public String userId { get; set; }
        public DateTime creationDate { get; set; }

        public PasswordStore() { }

        public PasswordStore(String uid, String pass, DateTime now) {
            this.userId = uid;
            this.passwordHash = pass;
            this.creationDate = now;
        }
    }
}