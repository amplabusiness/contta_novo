using System;
using Contta.Inteligencia.Cnpj.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contta.Inteligencia.Cnpj.Controllers
{
    [ApiController]
    [Route("api/socio")]
    public class SocioController : Controller
    {       
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult  GetSocioNome(string nome, string cpf, string cnpj)
        {
            try
            {
                DiretorioSocios diretoriosocios = new DiretorioSocios();                
                var result = diretoriosocios.GetDadosSocios(nome: "",cpf,cnpj);                
                if(result.Count > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest($"Não foi possivel encontrar os dados do socio={nome}-{cnpj}-{cpf}!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao pesquisar empresa={ex}");
            }
        }
    }
}
