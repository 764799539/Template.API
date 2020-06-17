using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Template.NuGet
{
    public sealed class JwtTokenBuilder
    {
        private SecurityKey SecurityKey;
        private string Subject = "";
        private string Issuer = "";
        private string Audience = "";
        private Dictionary<string, string> Claims = new Dictionary<string, string>();
        private int ExpiryInMinutes = 5;


        public JwtTokenBuilder AddAudience(string Audience)
        {
            this.Audience = Audience;
            return this;
        }

        public JwtTokenBuilder AddClaim(string type, string value)
        {
            Claims.Add(type, value);
            return this;
        }

        public JwtTokenBuilder AddClaims(Dictionary<string, string> Claims)
        {
            Enumerable.Union(this.Claims, Claims);
            return this;
        }

        public JwtTokenBuilder AddExpiry(int ExpiryInMinutes)
        {
            this.ExpiryInMinutes = ExpiryInMinutes;
            return this;
        }

        public JwtTokenBuilder AddIssuer(string Issuer)
        {
            this.Issuer = Issuer;
            return this;
        }

        public JwtTokenBuilder AddSecurityKey(SecurityKey SecurityKey)
        {
            this.SecurityKey = SecurityKey;
            return this;
        }

        public JwtTokenBuilder AddSubject(string Subject)
        {
            this.Subject = Subject;
            return this;
        }

        public JwtToken Build()
        {
            EnsureArguments();
            List<Claim> list = new List<Claim> {
                new Claim("sub", Subject),
                new Claim("jti", Guid.NewGuid().ToString())
            };
            IEnumerable<Claim> enumerable = Enumerable.Union(list, Enumerable.Select(Claims, delegate (KeyValuePair<string, string> item)
            {
                return new Claim(item.Key, item.Value);
            }));
            return new JwtToken(new JwtSecurityToken(Issuer, Audience, enumerable, null, new DateTime?(DateTime.UtcNow.AddMinutes(ExpiryInMinutes)), new SigningCredentials(SecurityKey, "HS256")));
        }

        private void EnsureArguments()
        {
            if (SecurityKey == null)
            {
                throw new ArgumentNullException("Security Key Is Null");
            }
            if (string.IsNullOrEmpty(Subject))
            {
                throw new ArgumentNullException("Subject Is NullOrEmpty");
            }
            if (string.IsNullOrEmpty(Issuer))
            {
                throw new ArgumentNullException("Issuer Is NullOrEmpty");
            }
            if (string.IsNullOrEmpty(Audience))
            {
                throw new ArgumentNullException("Audience Is NullOrEmpty");
            }
        }
    }

}
