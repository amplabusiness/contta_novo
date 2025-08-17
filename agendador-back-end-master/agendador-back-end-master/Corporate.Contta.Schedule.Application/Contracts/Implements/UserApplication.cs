using Corporate.Contta.Schedule.Application.Contracts.Repositories;
using Corporate.Contta.Schedule.Application.Email;
using Corporate.Contta.Schedule.Application.Mapping.Result.GenerateAccessToken;
using Corporate.Contta.Schedule.Application.Resources;
using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Application.Contracts.Implements
{
    public class UserApplication : IUserApplication
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;
        private const int QuantityDaysForTokenExpiration = 1;
        private readonly ICompanyRepository _companyRepository;
        private readonly IEmailService _emailService;

        public UserApplication(IUserRepository userRepository, IConfiguration config, ICompanyRepository companyRepository, IEmailService emailService)
        {
            _userRepository = userRepository;
            _config = config;
            _companyRepository = companyRepository;
            _emailService = emailService;
        }

        public async Task<GenerateAccessTokenResponse> GenerateAccessToken(GenerateAccessTokenFilter generateAccessTokenFilter, bool encryptPassword = true)
        {
            var user = await _userRepository.Login(generateAccessTokenFilter.Email);
            if (user == null)
                throw new Exception(Message.UsuarioNaoEncontrado);
            if (encryptPassword)
            {
                var passwordVerifacation = user.ValidatePassword(generateAccessTokenFilter.Password);
                if (!passwordVerifacation)
                    return new GenerateAccessTokenResponse(false);
            }
           else
            {
                if(!user.Password.Equals(generateAccessTokenFilter.Password))
                    return new GenerateAccessTokenResponse(false);
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Group.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(QuantityDaysForTokenExpiration),
                SigningCredentials = creds,
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var writeToken = tokenHandler.WriteToken(token);

            return new GenerateAccessTokenResponse(user, true, writeToken);
        }

        public async void PasswordChangeRequest(string email)
        {
            var user = await _userRepository.Login(email);
            if (user == null)
                throw new Exception(Message.UsuarioNaoEncontrado);
            var company = await _companyRepository.GetCompanyInformationByCnpj(user.Document);
            var assunto = company!= null? $" {company.NameFantasy} | Redefinição de senha": "Contta | Redefinição de senha";
            var corpoEmail = CorpoEmail.RedefinirSenha;

            var token  = await GenerateAccessToken(new GenerateAccessTokenFilter(user.Email, user.Password), false);
            var link = $"https://portal-simples-git-develop-conttaampla.vercel.app/recuperacaoSenha?token={token.Token}";
            corpoEmail = corpoEmail.Replace("{Usuario}", user.Name);
            corpoEmail = corpoEmail.Replace("{Data}", DateTime.Now.ToString());
            corpoEmail = corpoEmail.Replace("{Link}", link);

            MailRequest request = new MailRequest
            {
                Body = corpoEmail,
                Subject = assunto,
                To = new List<string> { user.Email }
            };
            _emailService.SendMail(request);
        }
    }
}
