using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace Corporate.Contta.Schedule.Infra.Tools
{
    public class ConvertTokenId
    {
        public string GetTokenUserMaster(string tokenUser)
        {
            var userId = string.Empty;
            var tokens = ReadToken(tokenUser);
            userId = tokens.Claims.First(claim => claim.Type == "nameid").Value;           
            return userId;
        }
        public bool TokenExperied(string tokenUser)
        {
            var tokens = ReadToken(tokenUser);
            return  DateTime.Now <= tokens.ValidTo; 
        }
        private JwtSecurityToken ReadToken(string tokenUser)
        {
            var handler = new JwtSecurityTokenHandler();            
            var jsonToken = handler.ReadToken(tokenUser);
            var tokenS = jsonToken as JwtSecurityToken;
            return tokenS;
        }
    }
}
