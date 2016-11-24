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


        public MyUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public class MyPasswordValidator : IIdentityValidator<string>
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

                if (_minLenght > password.Length || password.Length > _maxLenght) return Task.FromResult(
                    IdentityResult.Failed("Invalid password length. It must be between "+ _minLenght+" and "+_maxLenght+" characters"));

                if (_specialChar && new System.Text.RegularExpressions.Regex("[^\\w]+").IsMatch(password) == false) return Task.FromResult(
                    IdentityResult.Failed("Invalid password. It must contain special chars."));

                if (_digit && new System.Text.RegularExpressions.Regex("[\\d]+").IsMatch(password) == false) return Task.FromResult(
                    IdentityResult.Failed("Invalid password. It must contain digits."));

                if (_upperLetter && new System.Text.RegularExpressions.Regex("[A-Z]+").IsMatch(password) == false) return Task.FromResult(
                    IdentityResult.Failed("Invalid password. It must contain uppercase letters."));

                if (_lowerLetter && new System.Text.RegularExpressions.Regex("[a-z]+").IsMatch(password) == false) return Task.FromResult(
                    IdentityResult.Failed("Invalid password. It must contain lower case letters."));


                return Task.FromResult(IdentityResult.Success);
            }

        }
    }
}