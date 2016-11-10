using GTI619_Lab5.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GTI619_Lab5.Entities
{
    public class MyUserManager : UserManager<ApplicationUser>
    {


        public MyUserManager(IUserStore<ApplicationUser> store, IIdentityValidator<string> identity)
            : base(store)
        {
        }

        private class MyPasswordValidator : IIdentityValidator<string>
        {
            private int _maxLenght;
            private int _minLenght;
            private bool _specialChar;
            private bool _digit;
            private bool _upperLetter;
            private bool _lowerLetter;

            public MyPasswordValidator(int maxLenght,
                                       int minLenght,
                                       bool specialChar,
                                       bool digit,
                                       bool upperLetter,
                                       bool lowerLetter)
            {
                _maxLenght = maxLenght;
                _minLenght = minLenght;
                _specialChar = specialChar;
                _digit = digit;
                _upperLetter = upperLetter;
                _lowerLetter = lowerLetter;
            }

            public Task<IdentityResult> ValidateAsync(string password)
            {
                //TODO
                return Task.FromResult(IdentityResult.Success);
            }

        }
    }
}