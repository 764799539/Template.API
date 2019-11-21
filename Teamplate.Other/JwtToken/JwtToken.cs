using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Teamplate.NuGet
{
    public sealed class JwtToken
    {
        // Fields
        private JwtSecurityToken token;

        // Methods
        internal JwtToken(JwtSecurityToken token)
        {
            this.token = token;
        }

        // Properties
        public DateTime ValidTo =>
            this.token.ValidTo;

        public string Value =>
            new JwtSecurityTokenHandler().WriteToken(this.token);
    }

}
