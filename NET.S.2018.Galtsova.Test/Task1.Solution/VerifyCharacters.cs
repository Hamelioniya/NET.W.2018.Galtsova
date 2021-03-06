﻿using System;
using System.Linq;

namespace Task1.Solution
{
    public class VerifyCharacters : IVerifier
    {
        public Tuple<bool, string> Verify(string password)
        {
            // check if password conatins at least one alphabetical character 
            if (!password.Any(char.IsLetter))
            {
                return Tuple.Create(false, $"{password} hasn't alphanumerical chars");
            }

            // check if password conatins at least one digit character 
            if (!password.Any(char.IsNumber))
            {
                return Tuple.Create(false, $"{password} hasn't digits");
            }

            return Tuple.Create(true, "Password is Ok. User was created");
        }
    }
}
