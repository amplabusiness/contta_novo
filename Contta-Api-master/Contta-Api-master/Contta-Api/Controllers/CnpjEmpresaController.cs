using System;
using Contta.Inteligencia.Cnpj.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contta.Inteligencia.Cnpj.Controllers
{
    [ApiController]
    [Route("api/cnpj")]
    public class CnpjEmpresaController : Controller
    {
        [Route("empresas")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult  GetEmpresa(string cnpj)
        {
            try
            {
                DiretorioEmpresa diretorioEmpresa = new DiretorioEmpresa();
                ParceEmpresa parceEmpresa = new ParceEmpresa();
                var diretorio = diretorioEmpresa.GetDiretorioEmpresa(cnpj);
                var result = parceEmpresa.GetEmpresaContta(diretorio,cnpj);
                if(result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest($"Não foi possivel encontrar a empresa com dados de cnpj={cnpj}!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao pesquisar empresa={ex}");
            }
        }
    }
}
