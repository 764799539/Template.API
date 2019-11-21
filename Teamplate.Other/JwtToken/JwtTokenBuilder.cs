using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

namespace Teamplate.NuGet
{
    public sealed class JwtTokenBuilder
    {
        // Fields
        private SecurityKey securityKey;
        private string subject = "";
        private string issuer = "";
        private string audience = "";
        private Dictionary<string, string> claims = new Dictionary<string, string>();
        private int expiryInMinutes = 5;

        // Methods
        public JwtTokenBuilder AddAudience(string audience)
        {
            this.audience = audience;
            return this;
        }

        public JwtTokenBuilder AddClaim(string type, string value)
        {
            this.claims.Add(type, value);
            return this;
        }

        public JwtTokenBuilder AddClaims(Dictionary<string, string> claims)
        {
            Enumerable.Union<KeyValuePair<string, string>>((IEnumerable<KeyValuePair<string, string>>)this.claims, (IEnumerable<KeyValuePair<string, string>>)claims);
            return this;
        }

        public JwtTokenBuilder AddExpiry(int expiryInMinutes)
        {
            this.expiryInMinutes = expiryInMinutes;
            return this;
        }

        public JwtTokenBuilder AddIssuer(string issuer)
        {
            this.issuer = issuer;
            return this;
        }

        public JwtTokenBuilder AddSecurityKey(SecurityKey securityKey)
        {
            this.securityKey = securityKey;
            return this;
        }

        public JwtTokenBuilder AddSubject(string subject)
        {
            this.subject = subject;
            return this;
        }

        public JwtToken Build()
        {
            this.EnsureArguments();
            List<Claim> list1 = new List<Claim> {
            new Claim("sub", this.subject),
            new Claim("jti", Guid.NewGuid().ToString())
        };
            IEnumerable<Claim> enumerable = Enumerable.Union<Claim>((IEnumerable<Claim>)list1, Enumerable.Select<KeyValuePair<string, string>, Claim>((IEnumerable<KeyValuePair<string, string>>)this.claims, delegate (KeyValuePair<string, string> item) {
                return new Claim(item.Key, item.Value);
            }));
            return new JwtToken(new JwtSecurityToken(this.issuer, this.audience, enumerable, null, new DateTime?(DateTime.UtcNow.AddMinutes((double)this.expiryInMinutes)), new SigningCredentials(this.securityKey, "HS256")));
        }

        private void EnsureArguments()
        {
            if (this.securityKey == null)
            {
                throw new ArgumentNullException("Security Key");
            }
            if (string.IsNullOrEmpty(this.subject))
            {
                throw new ArgumentNullException("Subject");
            }
            if (string.IsNullOrEmpty(this.issuer))
            {
                throw new ArgumentNullException("Issuer");
            }
            if (string.IsNullOrEmpty(this.audience))
            {
                throw new ArgumentNullException("Audience");
            }
        }

    //    // Nested Types
    //    [Serializable, CompilerGenerated]
    //    private sealed class <>c
    //{
    //    // Fields
    //    public static readonly JwtTokenBuilder.<>c<>9 = new JwtTokenBuilder.<>c();
    //    public static Func<KeyValuePair<string, string>, Claim> <>9__13_0;

    //    // Methods
    //    internal Claim<Build> b__13_0(KeyValuePair<string, string> item) =>
    //        new Claim(item.Key, item.Value);
    //}
}

}
