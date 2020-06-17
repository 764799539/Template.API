using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Template.NuGet
{
    public sealed class JwtToken
    {
        private JwtSecurityToken Token;

        internal JwtToken(JwtSecurityToken Token)
        {
            this.Token = Token;
        }

        public DateTime ValidTo => Token.ValidTo;

        public string Value => new JwtSecurityTokenHandler().WriteToken(Token);
    }

}
