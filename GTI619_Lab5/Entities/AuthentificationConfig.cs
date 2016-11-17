using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GTI619_Lab5.Entities
{
    public class AuthentificationConfig
    {
        public Guid Id { get; set; }

        private int? NbrTry;
        private int? TryDownPeriod;
        public bool IsBlockAfterTwoTries { get; set; }

        public bool IsPeriodic { get; set; }
        private int? PeriodPeriodic;

        private int? MaxLenght;
        private int? MinLenght;
        public bool IsUpperCase { get; set; }
        public bool IsLowerCase { get; set; }
        public bool IsSpecialCase { get; set; }
        public bool IsNumber { get; set; }

        private int? TimeOutSession;

        public AuthentificationConfig() { }

        public void SetNbrTry(int? nbrTry)
        {
            this.NbrTry = nbrTry;
        }

        public int GetNbrTry()
        {
            return this.NbrTry.HasValue ? this.NbrTry.Value : 3;
        }

        public void SetTryDownPeriod(int? tryDownPeriod)
        {
            this.TryDownPeriod = tryDownPeriod;
        }

        public int GetTryDownPeriod()
        {
            return this.TryDownPeriod.HasValue ? this.TryDownPeriod.Value : 30;
        }

        public void SetPeriodPeriodic(int? periodPeriodic)
        {
            this.PeriodPeriodic = periodPeriodic;
        }

        public int GetPeriodPeriodic()
        {
            return this.PeriodPeriodic.HasValue ? this.PeriodPeriodic.Value : 30;
        }

        public void SetMaxLenght(int? maxLenght)
        {
            this.MaxLenght = maxLenght;
        }

        public int GetMaxLenght()
        {
            return this.MaxLenght.HasValue ? this.MaxLenght.Value : 20;
        }

        public void SetMinLenght(int? minLenght)
        {
            this.MinLenght = minLenght;
        }

        public int GetMinLenght()
        {
            return this.MinLenght.HasValue ? this.MinLenght.Value : 8;
        }

        public void SetTimeOutSession(int? timeOutSession)
        {
            this.TimeOutSession = timeOutSession;
        }

        public int GetTimeOutSession()
        {
            return this.TimeOutSession.HasValue ? this.TimeOutSession.Value : 20;
        }
    }
}