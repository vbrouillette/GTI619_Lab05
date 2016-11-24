using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GTI619_Lab5.Entities
{
    public class UserLoginLog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public String userId { get; set; }
        public DateTime loginTime { get; set; }
        public bool success { get; set; }

        public UserLoginLog() { }

        public UserLoginLog(String userId, DateTime loginTime, bool success)
        {
            this.userId = userId;
            this.loginTime = loginTime;
            this.success = success;
        }

    }
}