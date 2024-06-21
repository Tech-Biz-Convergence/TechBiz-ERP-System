using BusinessEntities.HR.MasterModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
//JWT = https://www.youtube.com/watch?v=9O0vnOTUd9o
//JWT GET TOKEN = https://www.youtube.com/watch?v=kM1fPt1BcLc&t=155s
namespace Middleware
{
    public class Jwt
    {
        private IConfiguration m_Config { get; }
        public Jwt(IConfiguration config)
        {
            m_Config = config;
        }
        public string BuildToken(tbm_user_info_role model)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(m_Config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(Convert.ToDouble(m_Config["Jwt:Expires"]));

            var claims = new[]
            {
                /*new Claim(JwtRegisteredClaimNames.Sub,model.user_name),
                new Claim(JwtRegisteredClaimNames.Email,model.user_Email),
                new Claim("Codemobiles","Learn JWT BY Codemobiles"),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()), */

                new Claim(JwtRegisteredClaimNames.NameId,model.user_name),
                new Claim(JwtRegisteredClaimNames.Email,model.user_Email),
                new Claim(JwtRegisteredClaimNames.GivenName,model.first_name),
                new Claim(JwtRegisteredClaimNames.FamilyName,model.last_name),
                new Claim("role", model.role)
            };

            var token = new JwtSecurityToken(
                m_Config["Jwt:Issuer"],
                m_Config["Jwt:Audience"],
                claims,
                expires: expires,
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
