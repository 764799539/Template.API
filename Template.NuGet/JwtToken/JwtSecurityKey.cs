using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Template.NuGet
{
    public static class JwtSecurityKey
    {
        // Methods
        public static SymmetricSecurityKey Create(string secret) =>
            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
    }

}
